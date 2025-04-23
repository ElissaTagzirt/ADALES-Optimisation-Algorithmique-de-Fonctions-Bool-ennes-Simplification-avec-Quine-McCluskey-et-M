using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Models
{
    internal class StepsList : List<Step>
    {
        public string FinalResult { get; set; }
    }
}
