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
            foreach (Contract c in FactoryBL.BL_instance.getAllContracts())
            {
                IdComboBox.Items.Add(c.Id);
            }
            foreach(Employee emp in FactoryBL.BL_instance.getAllEmployees())
            {
                employeeIdComboBox.Items.Add(emp.Id);
            }
            foreach(Employer emp in FactoryBL.BL_instance.getAllEmployers())
            {
                employerIdComboBox.Items.Add(emp.Id);
            }
            selectedButton = App.SelectedButton.None;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add && !App.isValidNumber(IdComboBox.Text,0) && IdComboBox.Text != "")
            {
                MessageBox.Show("Id already exists", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
                return;
            }
            if (selectedButton == App.SelectedButton.Add)
            {
                ContractData.Id = IdComboBox.Text;
            }
            if ((selectedButton == App.SelectedButton.Edit || selectedButton == App.SelectedButton.Remove) && IdComboBox.SelectedItem != null)
            {
                ContractData.Id = IdComboBox.SelectedItem.ToString();
            }
        }

        private void grossWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(grossWageForHourTextBox.Text,0) && grossWageForHourTextBox.Text != "")
            {
                MessageBox.Show($"Invalid gross wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                grossWageForHourTextBox.Text = "";
            }
        }

        private void maxWorkHoursForMonthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(maxWorkHoursForMonthTextBox.Text, 0) && maxWorkHoursForMonthTextBox.Text != "")
            {
                MessageBox.Show("Invalid max work hours for month", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                maxWorkHoursForMonthTextBox.Text = "";
            }
        }

        private void minWorkHoursForMonthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(minWorkHoursForMonthTextBox.Text, 0) && minWorkHoursForMonthTextBox.Text != "")
            {
                MessageBox.Show("Invalid min work hours for month", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                minWorkHoursForMonthTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, true,netW_ageForHourTextBox);
            IdComboBox.IsEditable = true;
            selectedButton = App.SelectedButton.Add;
            Globals.setToToday(contractEstablishedDateDatePicker);
            Globals.setToToday(terminateDateDatePicker);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, true,netW_ageForHourTextBox);
            selectedButton = App.SelectedButton.Edit;
            Globals.setToToday(contractEstablishedDateDatePicker);
            Globals.setToToday(terminateDateDatePicker);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
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
                    Contract addContract = new Contract();
                    Globals.copyObject(ContractData, addContract);
                    FactoryBL.BL_instance.addContract(addContract);
                    IdComboBox.Items.Clear();
                    foreach (Contract contract in FactoryBL.BL_instance.getAllContracts())
                    {
                        IdComboBox.Items.Add(contract.Id);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
                    Contract editContract = new Contract();
                    Globals.copyObject<Contract>(ContractData, editContract);
                    FactoryBL.BL_instance.updateContract(editContract, oldContract);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
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
                    Contract removeContract = new Contract();
                    Globals.copyObject(ContractData, removeContract);
                    FactoryBL.BL_instance.removeContract(removeContract);
                    IdComboBox.Items.Clear();
                    foreach (Contract contract in FactoryBL.BL_instance.getAllContracts())
                    {
                        IdComboBox.Items.Add(contract.Id);
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            selectedButton = App.SelectedButton.None;
            IdComboBox.IsEditable = false;
            Globals.swapGridsVisibility(SaveCancelGrid, AddEditRemoveGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, false);
            Globals.emptyAllFields(ContractsPropertiesGrids);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = App.SelectedButton.None;
            Globals.swapGridsVisibility(SaveCancelGrid, AddEditRemoveGrid);
            Globals.enableFields(ContractsPropertiesGrids, false, null, false);
            Globals.emptyAllFields(ContractsPropertiesGrids);
            IdComboBox.IsEditable = false;
        }

        private void IdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedButton != App.SelectedButton.Add && IdComboBox.SelectedItem != null)
            {
                Contract selectedContract = FactoryBL.BL_instance.getAllContracts().Find(c => c.Id == IdComboBox.SelectedItem.ToString());
                if (selectedContract != null)
                {
                    Globals.copyObject<Contract>(selectedContract, ContractData);
                }
            }
        }
    }
}
