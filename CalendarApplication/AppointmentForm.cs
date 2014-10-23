using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class AppointmentForm : Form
    {
        #region Members
        Appointment _Apt;
        List<string> _AppropriateTimes;
        /// These are used to store the old times and Date incase an EditException is triggered
        /// These would then set the value that triggered the event back to the previous value.
        int _OldLength;
        DateTime _OldDateTime;
        //This will be shown when there's an error, outlining what the error is.
        protected ToolTip _ExplanationTooltip;
        #endregion

        #region Constructor
        public AppointmentForm()
        {
            _ExplanationTooltip = new ToolTip();
            InitializeComponent();
        }
        #endregion

        #region Properties
        public IAppointment Apt
        {
            get { return _Apt; }
            set { _Apt = (Appointment)value; }
        }
        #endregion

        #region Protected Methods
        protected virtual bool Validation()
        {
            //This allows us to cycle through an highlight all the incorrect values
            bool isValid = true;
            //Reset all ForeColors.
            subj_Label.ForeColor = Color.Empty;
            subj_TextBox.BackColor = Color.Empty;
            type_Label.ForeColor = Color.Empty;
            variable_Label.ForeColor = Color.Empty;
            date_Label.ForeColor = Color.Empty;
            length_Label.ForeColor = Color.Empty;
            start_Label.ForeColor = Color.Empty;
            _ExplanationTooltip.Hide(this);

            DateTime time = DateTimePicker.Value.Add((Convert.ToDateTime(starttime_ComboBox.SelectedItem != null ? starttime_ComboBox.SelectedItem.ToString() : _Apt.Start.ToShortTimeString()).TimeOfDay));
            if (subj_TextBox.Text.Length == 0)
            {
                subj_Label.ForeColor = Color.Red;
                subj_TextBox.BackColor = Color.Beige;
                _ExplanationTooltip.Show("Subject is mandatory", this, subj_TextBox.Location.X, subj_TextBox.Location.Y + 50, 3000);
                isValid = false;
            }
            if (type_ComboBox.SelectedIndex < 0)
            {
                type_Label.ForeColor = Color.Red;
                isValid = false;
            }
            //Location can be empty in certain situations.
            if ((type_ComboBox.SelectedItem.ToString() != "Other" && type_ComboBox.SelectedItem.ToString() != "HR" && type_ComboBox.SelectedItem.ToString() != "Birthday") && variable_TextBox.Text.Length == 0)
            {
                type_Label.ForeColor = Color.Red;
                variable_Label.ForeColor = Color.Red;
                _ExplanationTooltip.Show("Location is mandatory", this, variable_TextBox.Location.X, variable_TextBox.Location.Y + 50, 3000);
                isValid = false;
            }
            //Assume Manual
            if(length_ComboBox.SelectedIndex == -1)
            {
                int lengthNumber;
                if(int.TryParse(length_ComboBox.Text, out lengthNumber) &&  lengthNumber % 30 == 0)
                {
                    length_ComboBox.SelectedIndex = (lengthNumber / 30) - 1;
                }
                else
                {
                    isValid = false;
                    _ExplanationTooltip.Show("Length must be a multiple of 30", this, length_ComboBox.Location.X, length_ComboBox.Location.Y + 50, 3000);
                    length_Label.ForeColor = Color.Red;
                }

            }
            if (time.AddMinutes((length_ComboBox.SelectedIndex) * 30).Date > DateTimePicker.Value.Date)
            {
                DateTimePicker.CalendarForeColor = Color.Red;
                date_Label.ForeColor = Color.Red;
                length_Label.ForeColor = Color.Red;
                start_Label.ForeColor = Color.Red;
                _ExplanationTooltip.Show("Date Exceeds todays date", this, length_Label.Location.X, length_Label.Location.Y + 50, 3000);
                isValid = false;
            }
            if (_Apt != null && isValid)
            {
                try
                {
                    _Apt.Start = DateTimePicker.Value.Add((Convert.ToDateTime(starttime_ComboBox.SelectedItem != null ? starttime_ComboBox.SelectedItem.ToString() : _Apt.Start.ToShortTimeString()).TimeOfDay));
                    _Apt.Length = (length_ComboBox.SelectedIndex + 1) * 30;
                    _Apt.Type = (Type)Enum.Parse(typeof(Type), type_ComboBox.SelectedValue.ToString());
                    _Apt.Subject = subj_TextBox.Text;
                    _Apt.Location = variable_TextBox.Text;
                }catch (EditException e)
                {
                    isValid = false;
                    if (_Apt.Start != _OldDateTime)
                    {
                        _Apt.Start = _OldDateTime;
                    }
                    else
                    {
                        _Apt.Length = _OldLength;
                    }
                    length_ComboBox.SelectedIndex = Math.Max(0, (_Apt.Length / 30) - 1);
                    starttime_ComboBox.SelectedIndex = starttime_ComboBox.Items.IndexOf(_Apt.Start.ToShortTimeString());
                    MessageBox.Show(e.Message, "Edit Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                isValid = false;
            }

            return isValid;
        }
        #endregion

        #region Private Methods
        //This is still useful, limits the amount of time.
        private List<string> GetAppropriateLengths()
        {
            #region local Members
            List<string> allLengths = new List<string>();
            int length = 30;
            string time = starttime_ComboBox.SelectedItem != null ? starttime_ComboBox.SelectedItem.ToString() : _Apt.Start.ToShortTimeString();
            DateTime currentDate = DateTimePicker.Value.Date + DateTime.ParseExact(time, "HH:mm", null).TimeOfDay;
            DateTime tomorrow = currentDate.AddDays(1).Date;
            allLengths.Add("30min");
            #endregion
            while (currentDate != tomorrow.AddMinutes(-30))
            {
                length += 30;
                string convertedLength = length % 60 == 0 ? (length / 60).ToString() + "hrs" : (length / 60).ToString() + "hrs " + (length % 60) + "min";
                allLengths.Add(convertedLength);
                currentDate = currentDate.AddMinutes(30);
            }
            return allLengths;
        }

        private List<string> GetAppropriateTimes(DateTime currentDate)
        {
            currentDate = Utility.RoundToThirty(currentDate);
            DateTime tomorrow = currentDate.AddDays(1);
            List<string> allTimes = new List<string>();
            while (currentDate != tomorrow.Date)
            {
                allTimes.Add(currentDate.ToShortTimeString());
                currentDate = currentDate.AddMinutes(30);
            }
            return allTimes;
        }
        #endregion

        #region Events
        private void AddEditForm_Load(object sender, EventArgs e)
        {
            type_ComboBox.DataSource = Enum.GetNames(typeof(Type)).Where(a => a != "Birthday").ToArray();
            DateTimePicker.MinDate = DateTime.Now.Date;

            //Initialises all the labels.
            if (Apt != null)
            {
                _OldLength = _Apt.Length;
                _OldDateTime = _Apt.Start;
                subj_TextBox.Text = _Apt.Subject;
                DateTimePicker.MinDate = DateTime.Now.Date;
                DateTimePicker.Value = _Apt.Start.Date;
                starttime_ComboBox.DataSource = _AppropriateTimes = GetAppropriateTimes(DateTimePicker.Value == DateTime.Now.Date ? DateTime.Now : DateTimePicker.Value);
                type_ComboBox.SelectedIndex = type_ComboBox.Items.IndexOf(_Apt.Type.ToString());
                variable_TextBox.Text = _Apt.Location;
                starttime_ComboBox.SelectedIndex = starttime_ComboBox.Items.IndexOf(_Apt.Start.ToShortTimeString());
                length_ComboBox.SelectedIndex = Math.Max(0, (_Apt.Length / 30) - 1);
            }
        }

        private void save_Button_Click(object sender, EventArgs e)
        {
            if(Validation())
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void close_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            starttime_ComboBox.DataSource = _AppropriateTimes = GetAppropriateTimes(DateTimePicker.Value);
        }

        private void starttime_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            length_ComboBox.DataSource = GetAppropriateLengths();
            length_ComboBox.SelectedIndex = Math.Max(0, (_Apt.Length / 30) - 1);
        }

        //Ensures users know when Location is and isn't Mandatory.
        private void type_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(type_ComboBox.SelectedItem.ToString() != "Other" && type_ComboBox.SelectedItem.ToString() != "HR" && type_ComboBox.SelectedItem.ToString() != "Birthday")
            {
                variable_Label.Text = "Location*";
            }
            else
            {
                variable_Label.Text = "Location";
            }
        }

        //Only allow length to take numerical keypresses
        private void length_ComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != 8;
        }
        #endregion
    }
}
