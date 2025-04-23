using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Models
{
    public class triliste : IComparer<TabTermes>
    {
        public int Compare(TabTermes a, TabTermes b)
        {
            return a.getgroupe().CompareTo(b.getgroupe());
        }

    }
}
