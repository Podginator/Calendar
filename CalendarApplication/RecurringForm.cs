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
    public partial class RecurringForm : AppointmentForm
    {
        #region Members
        RecurringAppointment _Apt;
        #endregion

        #region Constructor
        public RecurringForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Protected Methods
        protected override bool Validation()
        {
            bool isValid = true;
            int result;
            int.TryParse(amount_TextBox.Text, out result);

            if (freq_ComboBox.SelectedIndex < 0)
            {
                reccur_Label.ForeColor = Color.Red;
                _ExplanationTooltip.Show("This value is unacceptable for Frequency.", this, freq_ComboBox.Location.X, freq_ComboBox.Location.Y + 50, 3000);
                isValid = false;
            }
            else if(result < 0 || result > 999)
            {
                amountRecurring_Label.ForeColor = Color.Red;
                _ExplanationTooltip.Show("Cannot be less than 0.", this, amount_TextBox.Location.X, amount_TextBox.Location.Y + 50, 3000);

                isValid = false; 
            }
            else if (_Apt != null && isValid)
            {
                _Apt.AmountRecurring = int.Parse(amount_TextBox.Text);
                _Apt.Recursion = (Recurring)Enum.Parse(typeof(Recurring), freq_ComboBox.SelectedValue.ToString());
                _Apt.GetOccuringDates();
            }
            else
            {
                return false;
            }
            return isValid && base.Validation();
        }
        #endregion

        #region Events
        private void RecurringForm_Load(object sender, EventArgs e)
        {
            _Apt = base.Apt as RecurringAppointment;
            type_ComboBox.DataSource = Enum.GetNames(typeof(Type));
            freq_ComboBox.DataSource = Enum.GetNames(typeof(Recurring));
            amount_TextBox.MaxLength = 3;
            if (_Apt != null)
            {
                freq_ComboBox.SelectedIndex = freq_ComboBox.Items.IndexOf(_Apt.Recursion.ToString());
                amount_TextBox.Text = _Apt.AmountRecurring.ToString(); 
            }
        }

        private void type_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type_ComboBox.SelectedItem != null && type_ComboBox.SelectedItem.ToString() == "Birthday")
            {
                freq_ComboBox.SelectedIndex = 3;
                freq_ComboBox.Enabled = false;
            }
            else
            {
                freq_ComboBox.Enabled = true;
            }
        }

        private void amount_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != 8;
        }
        #endregion
    }
}
