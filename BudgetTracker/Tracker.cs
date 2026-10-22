using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker
{
    class Tracker
    {
        private List<LineItem> lineItems;
        private DataLayer dataLayer;

        public Tracker()
        {
            dataLayer = new DataLayer();
            RefreshDB();
        }
        public List<LineItem> LineItems
            { get { return lineItems; } }

        private void RefreshDB()
        {
            lineItems = dataLayer.GetLineItems();
        }
        public void AddLineItem(string name, uint credit, uint debit) { 
            LineItem newLineItem = new LineItem(null, name, credit, debit);
            dataLayer.SaveLineItem(newLineItem); 
            RefreshDB();
        }

        public void SearchLineItem(LineItem lineItem) 
        {
            dataLayer.SearchLineItem(lineItem);
            RefreshDB();
        }

        public void UpdateLineItem(LineItem lineItem)
        {
            dataLayer.UpdateLineItem(lineItem); 
            RefreshDB();
        }

        public void DeleteLineItem(LineItem lineItem)
        {
            dataLayer.DeleteLineItem(lineItem); 
            RefreshDB();
        }
        public uint TotalCredit() {
            // TODO: Do something with your lineItems
            uint totalCredit = 0;
            foreach (LineItem item in this.lineItems)
            {
                totalCredit = totalCredit + item.Credit;
            }
            return totalCredit;
        }

        public uint TotalDebit()
        {
            // TODO: Do something with your lineItems
            return 0;
        }
    }
}
