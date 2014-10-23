using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar
{
    
    public interface IAppointment
    {
        DateTime Start { get; }
        int Length { get; }
        string DisplayableDescription { get; }
        bool OccursOnDate(DateTime date);
    }
}
