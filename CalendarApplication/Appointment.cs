using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Calendar
{
    public enum Type { Other, Meeting, Birthday, Interview, HR, }

    [Serializable]
    [XmlInclude(typeof(RecurringAppointment))]
    public class Appointment : IAppointment, INotifyPropertyChanged
    {
        #region Members
        int _Length;
        string _Location;
        DateTime _Start;

        #endregion

        #region Constructors
        public Appointment(DateTime start)
        {
            Start = start;
            Location = "";
        }

        //Necessary for XMLSerialization
        public Appointment(){}
        #endregion

        #region Properties
        public string Subject { get; set; }

        public string Location 
        {
            get
            {
                return _Location;
            }
            set
            {
                _Location = value;
            }
        }

        public Type Type { get; set; }

        public DateTime Start
        {
            get
            {
                return _Start;
            }

            set
            {
                DateTime old = _Start;
                //Ensures that time is in a 30 minute interval, if it isn't then round up to nearest 30!
                _Start = value.Minute % 30 == 0 ? value : Utility.RoundToThirty(value);
                //Basically, only trigger a PropertyChange if the value has actually changed.
                //Send the old as a string to do a comparison to avoid any unneeded checking
                if (old != _Start)
                {
                    PropertyChange("Start");
                }
            }
        }

        public int Length
        {
            get 
            { 
                return _Length; 
            }
            set
            {
 
                int old = _Length;
                //If Mod 30 = 0 then it's correct, otherwise round it to the nearest 30 value.
                //Should be stopped in Form, but another form of protection against it.
                _Length = value % 30 == 0 && value > 0 ?  value : 30 * (int)Math.Max(1,Math.Round(value / 30.0));
                
                if (old != Length)
                {
                    PropertyChange("Length");
                }
            }
        }

        //No need to include this. It doesn't need to be set 
        [XmlIgnore]
        public virtual string DisplayableDescription
        {
            get 
            {
                string convertedLocation = _Location.Length > 0 ? "(" + _Location + ")" : "";
                return "[" + Type.ToString() + "] " + convertedLocation + " " + Subject;
            }
            private set { }
        }
        #endregion

        #region Public Methods
        public virtual bool OccursOnDate(DateTime date)
        {
            return Start.Date == date.Date;
        }
        #endregion

        #region Private Method (Prop Change)
        private void PropertyChange(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
