using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace Calendar
{
    /// <summary>
    /// Following a Conversation with Tommy this was changed to allow me to monitor when 
    /// an item in the collection is modified, this allows me to retain the integrity of the 
    /// Appointment list (No need to add/remove to collection to test appropriate times) and allows me
    /// to trigger an exception if an item is edited and overlaps with items already in the list.
    /// </summary>
    public class Appointments : ObservableCollection<IAppointment>, IAppointments
    {
        #region Members
        XmlSerializer _Serializer;
        string _Path;
        #endregion

        #region Constructor
        //Used for Serialization.
        public Appointments()
        {
            _Serializer = new XmlSerializer(typeof(List<Appointment>));
            //Serializes out as an xml obfuscated as .Cal file. This is to avoid editing.
            _Path = Path.Combine(Application.StartupPath, "saved.Cal");
        }
        #endregion

        #region Public Methods
        public bool Load()
        {
            IEnumerable xmlFiles;
            FileInfo xml = new FileInfo(_Path);

            if(!xml.Exists)
            {
                return false; 
            }
            else
            {
                using (Stream s = xml.OpenRead())
                {
                    try
                    {
                        xmlFiles = (IEnumerable)_Serializer.Deserialize(s);
                    }
                    catch(System.Xml.XmlException)
                    {
                        //Invalid XML, return false
                        return false;
                    }
                }
                foreach(Appointment apt in xmlFiles)
                {
                    try
                    {
                        Add(apt);
                    }
                    catch(ItemsException)
                    {
                        continue;
                    }
                }
            }
            return true;
        }
        
        public bool Save()
        {
            try
            {
                List<Appointment> serializingList = this.Items.Cast<Appointment>().ToList();
                using (StreamWriter writer = new StreamWriter(_Path))
                {
                    _Serializer.Serialize(writer, serializingList);
                    writer.Close();
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<IAppointment> GetAppointmentsOnDate(DateTime date)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].OccursOnDate(date.Date))
                {
                    yield return this[i];
                }
            }
        }

        //This is used in the add in collection.
        protected override void InsertItem(int index, IAppointment newApt)
        {
            if (DateOverlapping(newApt, newApt.Start.Date) >=3 )
            {
                throw new ItemsException();
            }
            foreach (IAppointment apt in GetAppointmentsOnDate(newApt.Start.Date))
            {
                if (CheckOverlap(apt, newApt))
                {
                    if (DateOverlapping(apt, newApt.Start.Date) >= 3)
                    {
                        throw new ItemsException();
                    }
                }
            }
            base.InsertItem(index, newApt);
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Appointment item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= TestModifiedItem;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Appointment item in e.NewItems)
                {
                    item.PropertyChanged += TestModifiedItem;
                }
            }       
        }

        #endregion

        #region Private Methods
        //This code was repeated a lot, putting it in a method shortens the repeat.
        private bool CheckOverlap(IAppointment apt, IAppointment newapt)
        {
            DateTime aptEnd = apt.Start.AddMinutes(apt.Length);
            DateTime newAptEnd = newapt.Start.AddMinutes(newapt.Length);
            if ((apt.Start.TimeOfDay < newapt.Start.TimeOfDay && newapt.Start.TimeOfDay < aptEnd.TimeOfDay)
                    || (apt.Start.TimeOfDay < newAptEnd.TimeOfDay && newAptEnd.TimeOfDay <= aptEnd.TimeOfDay)
                    || (newapt.Start.TimeOfDay < apt.Start.TimeOfDay && apt.Start.TimeOfDay < newAptEnd.TimeOfDay)
                    || (newapt.Start.TimeOfDay < aptEnd.TimeOfDay && aptEnd.TimeOfDay <= newAptEnd.TimeOfDay))
            {
                return true;
            }
            return false;
        }

        //This method checks how many overlaps are there for any given appointment.
        //If it exceeds 3 then it returns true, as only 3 appointments can exist at a given slot
        private int DateOverlapping(IAppointment newApt, DateTime date)
        {
            int exceedsMax = 0;
            foreach (IAppointment apt in GetAppointmentsOnDate(date))
            {
                if (CheckOverlap(apt, newApt))
                {
                    exceedsMax++;
                }
                if(exceedsMax > 3)
                {
                    return exceedsMax;
                }
            }
            return exceedsMax;
        }
         
        private void TestModifiedItem(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var aptSender = sender as Appointment;
            if (DateOverlapping(aptSender, aptSender.Start.Date) > 3)
            {
                throw new EditException();
            }
        }
        #endregion
    }
}
