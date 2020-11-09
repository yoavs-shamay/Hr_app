using BE;
using BL;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            App.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            App.enableFields(ContractsPropertiesGrids); //TODO do this also in other user controls
            IdComboBox.IsEditable = true;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            App.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            App.enableFields(ContractsPropertiesGrids);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            App.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            App.enableFields(ContractsPropertiesGrids, true, IdComboBox);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            FactoryBL.BL_instance.addContract(ContractData); //TODO verifications that all properties are filled, also do this to other user controls
            //TODO also do edit and remove cases
            //TODO try catch
            App.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            App.enableFields(ContractsPropertiesGrids, false, null, false);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            App.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            App.enableFields(ContractsPropertiesGrids, false, null, false);
            App.emptyAllFields(ContractsPropertiesGrids);
        }
    }
}
