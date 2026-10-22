using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tracker budgetTracker;
        public MainWindow()
        {
            InitializeComponent();
            budgetTracker = new Tracker();
            RefreshView();
        }

        public void RefreshView()
        {
            budgetItems.ItemsSource = budgetTracker.LineItems;
            totalCredit.Text = budgetTracker.TotalCredit().ToString();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // itemName
            int amount = int.Parse(itemAmount.Text);
            uint[] result = getAmount(amount);
            budgetTracker.AddLineItem(itemName.Text, result[0], result[1]);

            RefreshView();
        }

        private uint[] getAmount(int amount)
        {
            
            uint credit = 0;
            uint debit = 0;
            if (amount > 0)
            {
                credit = (uint)amount;
            }
            else
            {
                debit = (uint)-amount;
            }
            return [credit, debit];
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = budgetItems.SelectedIndex;
            LineItem lineItem = budgetTracker.LineItems[index];
            int amount = int.Parse(itemAmount.Text);
            uint[] result = getAmount(amount);
            lineItem.Name = itemName.Text;
            lineItem.Debit = result[1];
            lineItem.Credit = result[0];

            budgetTracker.UpdateLineItem(lineItem);
            RefreshView();
                //, result[0], result[1]
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int index = budgetItems.SelectedIndex;
            if (index < 0) {
                MessageBox.Show("Please select the object to delete");
            }

            LineItem item = budgetTracker.LineItems[index];
            budgetTracker.DeleteLineItem(item);
            RefreshView();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {


        }
    }
}