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
        private App.SelectedButton selectedButton;
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
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, true); //TODO do this also in other user controls
            IdComboBox.IsEditable = true;
            selectedButton = App.SelectedButton.Add;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            Globals.enableFields(ContractsPropertiesGrids);
            selectedButton = App.SelectedButton.Edit;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            Globals.enableFields(ContractsPropertiesGrids, true, IdComboBox);
            selectedButton = App.SelectedButton.Remove;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add)
            {
                if (!Globals.isEverythingNotNull<Contract>(ContractData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    FactoryBL.BL_instance.addContract(ContractData);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (selectedButton == App.SelectedButton.Edit)
            {
                if (!Globals.isEverythingNotNull<Contract>(ContractData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Contract oldContract = FactoryBL.BL_instance.getAllContracts().Find(x => x.Id == ContractData.Id);
                    FactoryBL.BL_instance.updateContract(ContractData, oldContract);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (selectedButton == App.SelectedButton.Remove)
            {
                if (ContractData.Id == null)
                {
                    MessageBox.Show("Fill ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    FactoryBL.BL_instance.removeContract(ContractData);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            //TODO also do everything to other user controls
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, false);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid); //TODO do this also in other user controls
            Globals.enableFields(ContractsPropertiesGrids, false, null, false);
            Globals.emptyAllFields(ContractsPropertiesGrids);
        }
    }
}
