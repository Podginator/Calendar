using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public enum Recurring { Daily, Weekly, Monthly, Yearly };

    public class RecurringAppointment : Appointment
    {
        #region Members
        List<DateTime> _OccurDates;
        int _AmountRecurring;
        #endregion

        #region Constructors
        public RecurringAppointment(DateTime start):base(start)
        {
            _OccurDates = new List<DateTime>();
        }
        public RecurringAppointment(){}
        #endregion

        #region Properties
        public Recurring Recursion { get; set; }

        //Will set as a min of 1, or a max of 999 - Checked in form too. 
        public int AmountRecurring
        {
            get
            {
                return _AmountRecurring;
            }
            set
            {
                _AmountRecurring = value > 0 ? value : Math.Min(1, value > 999 ? 999 : value);
            }
        }

        //Originally just used OccursOnDate to display them, but figured this was better for deleting a single occurance of
        //an appointment, and allows for overlaps to be deleted in the form. 
        public List<DateTime> OccuringDates
        {
            get { return _OccurDates; }
            set { _OccurDates = value; }
        }

        public override string DisplayableDescription
        {
            get
            {
                return "{" + Recursion.ToString() + "} " + base.DisplayableDescription;
            }
        }
        #endregion

        #region Public Methods
        //Called in RecurringForm when saving out
        public void GetOccuringDates()
        {
            OccuringDates.Clear();
            DateTime timespan = this.Start;
            OccuringDates.Add(timespan.Date);
            int recurredcheck = 1;
            while (recurredcheck++ < AmountRecurring)
            {
                switch (this.Recursion)
                {
                    case Recurring.Daily:
                        timespan = timespan.AddDays(1);
                        break;
                    case Recurring.Weekly:
                        timespan = timespan.AddDays(7);
                        break;
                    case Recurring.Monthly:
                        timespan = timespan.AddMonths(1);
                        break;
                    case Recurring.Yearly:
                        timespan = timespan.AddYears(1);
                        break;
                }
                    OccuringDates.Add(timespan.Date);
                }
        }

        public override bool OccursOnDate(DateTime date)
        {
            return OccuringDates.Contains(date.Date);
        }
        #endregion
    }
}

