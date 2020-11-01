using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for ContractsUserControl.xaml
    /// </summary>
    public partial class ContractsUserControl : UserControl
    {
        private Contract ContractData { get; set; }
        public ContractsUserControl()
        {
            InitializeComponent();
            ContractData = new Contract();
            DataContext = ContractData;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(IdComboBox.Text,0))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
            }
        }

        private void grossWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(grossWageForHourTextBox.Text,0))
            {
                MessageBox.Show("Invalid gross wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                grossWageForHourTextBox.Text = "";
            }
        }

        private void maxWorkHoursForMonthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(maxWorkHoursForMonthTextBox.Text, 0))
            {
                MessageBox.Show("Invalid max work hours for month", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                maxWorkHoursForMonthTextBox.Text = "";
            }
        }

        private void minWorkHoursForMonthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(minWorkHoursForMonthTextBox.Text, 0))
            {
                MessageBox.Show("Invalid min work hours for month", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                minWorkHoursForMonthTextBox.Text = "";
            }
        }
    }
}
