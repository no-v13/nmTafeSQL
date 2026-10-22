using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker
{
    class LineItem
    {
        private uint? id;
        private uint credit;
        private uint debit;
        private string name;

        public uint? Id { get { return id; } set { id = value; } }
        public string Name { get { return name; } set { name = value; } }
        public uint Credit { get { return credit; } set { credit = value; } }
        public uint Debit { get { return debit; } set { debit = value; } }

        public LineItem(uint? id, string name, uint credit, uint debit)
        {
            this.id = id;
            this.name = name;
            this.credit = credit;
            this.debit = debit;
        }

        public override string ToString()
        {
            return $"{Name}, {Credit}, {Debit}";
        }

    }
}
