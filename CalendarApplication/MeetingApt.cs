using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Calendar
{
    public class MeetingApt : Appointment
    {
        public MeetingApt(DateTime start, int length=30, string[] persons = null, string str = "", string location = ""):base(start, length,str)
        {
            Location = location;
            persons = persons ?? new string[5];
            Persons = persons;
        }

        private MeetingApt() { }

        public override Color Colour
        {
            get { return Color.CadetBlue; }
        }

        public string[] Persons { get; set; }

        public string Location { get; set; }
    }
}
