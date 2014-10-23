using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    //This exception fires if an appointment existing in a collection is edited. It uses INotify and ObservableCollection
    class EditException : Exception
    {
        public override string Message
        {
            get
            {
                return "Edited values for this item overlap, reverting appointment variables to previously set.";
            }
        }
    }
}
