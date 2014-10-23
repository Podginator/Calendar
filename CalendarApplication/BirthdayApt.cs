using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Calendar
{
    public class BirthdayApt : Appointment
    {
        public BirthdayApt(DateTime start, int length=30, string person = "", string description = ""): base(start, length, description, Recurring.Yearly)
        {
            Person = person;
        }

        private BirthdayApt() { }

        public string Person { get; set; }

        public override Color Colour
        {
            get { return Color.CornflowerBlue; }
        }
    }    
}
