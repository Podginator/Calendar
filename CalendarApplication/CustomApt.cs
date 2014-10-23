using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    public class CustomApt : Appointment
    {
        public CustomApt(DateTime start, int length=30, string str = "", string location = ""):base(start, length,str){}

        public override Color Colour
        {
            get { return Color.CornflowerBlue; }
            set { }
        }

        private CustomApt() { }
    }
}
