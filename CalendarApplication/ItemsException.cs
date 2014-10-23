using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class ItemsException : Exception
    {
        public override string Message
        {
            get
            {
                return "Too many Appointments exist in that time range, the limit is 3.";
            }
        }
    }
}
