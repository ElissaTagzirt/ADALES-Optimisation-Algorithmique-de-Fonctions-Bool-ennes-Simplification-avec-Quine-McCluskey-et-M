using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Models
{
    class StepEntry
    {
        public IReadOnlyList<int> Decimals { get; }
        public byte Check { get; }
        public int Groupe { get; }
        public string Minterme { get;}

        public StepEntry(string mineterme, IEnumerable<int> decimals, byte check, int groupe)
        {
            var d = new List<int>();
            d.AddRange(decimals);
            Minterme = mineterme;
            Decimals = d;
            Check = check;
            Groupe = groupe;
        }
    }

    internal class Step
    {
        public IReadOnlyList<StepEntry> Entries { get; }

        public Step(IEnumerable<TabTermes> tabTermes)
        {
            var entries = new List<StepEntry>();

            foreach (var tb in tabTermes)
            {
                var entry = new StepEntry(tb.getminterme(), tb.Decimals, tb.Check, tb.Groupe);
                entries.Add(entry);
            }

            this.Entries = entries;
        }
    }
}
