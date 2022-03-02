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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployeesUserControl.xaml
    /// </summary>
    public partial class EmployeesUserControl : UserControl
    {
        private App.SelectedButton selectedButton;
        private Employee EmployeeData { get; set; }
        public EmployeesUserControl()
        {
            InitializeComponent();
            EmployeeData = new Employee();
            EmployeeData.Address = new CivicAddress();
            EmployeeData.BankAccount = new Bank();
            DataContext = EmployeeData;
            bankNameComboBox.ItemsSource = (from b in FactoryBL.BL_instance.getAllBanks() select b.BankName).Distinct();
            foreach (Employee e in FactoryBL.BL_instance.getAllEmployees())
            {
                IdComboBox.Items.Add(e.Id);
            }
            foreach (Specialization sp in FactoryBL.BL_instance.getAllSpecializations())
            {
                employeeSpecializationComboBox.Items.Add(sp);
            }
            foreach (Specialization.Education education in Enum.GetValues(typeof(Specialization.Education)))
            {
                personalEducationComboBox.Items.Add(education);
            }
            personalEducationComboBox.SelectedItem = null;
            selectedButton = App.SelectedButton.None;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((selectedButton == App.SelectedButton.Add && !App.isValidID(IdComboBox.Text)))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
                IdComboBox.SelectedItem = null;
            }
            else
            {
                if (selectedButton == App.SelectedButton.Add)
                {
                    EmployeeData.Id = IdComboBox.Text;
                }
                if ((selectedButton == App.SelectedButton.Edit || selectedButton == App.SelectedButton.Remove) && IdComboBox.SelectedItem != null)
                {
                    EmployeeData.Id = IdComboBox.SelectedItem.ToString();
                }
            }
        }

        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(LastNameTextBox.Text,false) && LastNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid last name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                LastNameTextBox.Text = "";
            }
        }

        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(FirstNameTextBox.Text,false) && FirstNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid first name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                FirstNameTextBox.Text = "";
            }
        }

        private void phoneNumberPrefixComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EmployeeData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void phoneNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidPhoneNumber(phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text) && phoneNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid phone number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                phoneNumberTextBox.Text = "";
            }
            EmployeeData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void addressCityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(addressCityTextBox.Text) && addressCityTextBox.Text != "")
            {
                MessageBox.Show("Invalid city", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressCityTextBox.Text = "";
            }
        }

        private void addressStreetNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(addressStreetNameTextBox.Text) && addressStreetNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid street name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressStreetNameTextBox.Text = "";
            }
        }

        private void addressHouseNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressHouseNumberTextBox.Text, 1) && addressHouseNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid house number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressHouseNumberTextBox.Text = "";
            }
        }

        private void addressApartmentNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressApartmentNumberTextBox.Text, 1) && addressApartmentNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid apartment number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressApartmentNumberTextBox.Text = "";
            }
        }

        private void bankAccountNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(bankAccountNumberTextBox.Text, 0) && bankAccountNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid bank account number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                bankAccountNumberTextBox.Text = "";
            }
        }

        private void bankBranchNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(bankBranchNumberTextBox.Text, 0) && bankBranchNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid bank branch number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                bankBranchNumberTextBox.Text = "";
            }
        }

        private void emailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidEmail(emailTextBox.Text) && emailTextBox.Text != "")
            {
                MessageBox.Show("Invalid email", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                emailTextBox.Text = "";
            }
        }

        private void yearsOfExperienceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(yearsOfExperienceTextBox.Text, 0) && yearsOfExperienceTextBox.Text != "")
            {
                MessageBox.Show("Invalid years of experience", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                yearsOfExperienceTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployeesPropertiesGrid,false,null,true,bankNumberTextBox,bankAddressTextBox);
            IdComboBox.IsEditable = true;
            selectedButton = App.SelectedButton.Add;
            Globals.setToToday(birthDatePicker);
            
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployeesPropertiesGrid,false,null,true,bankNumberTextBox,bankAddressTextBox);
            selectedButton = App.SelectedButton.Edit;
            Globals.setToToday(birthDatePicker);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployeesPropertiesGrid, true, IdComboBox);
            selectedButton = App.SelectedButton.Remove;
            Globals.setToToday(birthDatePicker);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add)
            {
                if (!Globals.isEverythingNotNull<Employee>(EmployeeData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employee addEmployee = new Employee();
                    Globals.copyObject(EmployeeData,addEmployee);
                    FactoryBL.BL_instance.addEmployee(addEmployee);
                    IdComboBox.Items.Clear();
                    foreach (Employee emp in FactoryBL.BL_instance.getAllEmployees())
                    {
                        IdComboBox.Items.Add(emp.Id);
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
                if (!Globals.isEverythingNotNull<Employee>(EmployeeData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employee oldEmployee = FactoryBL.BL_instance.getAllEmployees().Find(x => x.Id == EmployeeData.Id);
                    Employee editEmployee = new Employee();
                    Globals.copyObject<Employee>(EmployeeData, editEmployee);
                    FactoryBL.BL_instance.updateEmployee(editEmployee, oldEmployee);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (selectedButton == App.SelectedButton.Remove)
            {
                if (EmployeeData.Id == null)
                {
                    MessageBox.Show("Fill ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employee removeEmployee = new Employee();
                    Globals.copyObject(EmployeeData, removeEmployee);
                    FactoryBL.BL_instance.removeEmployee(removeEmployee);
                    IdComboBox.Items.Clear();
                    foreach (Employee emp in FactoryBL.BL_instance.getAllEmployees())
                    {
                        IdComboBox.Items.Add(emp.Id);
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
            Globals.enableFields(EmployeesPropertiesGrid, false, null, false);
            Globals.emptyAllFields(EmployeesPropertiesGrid);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = App.SelectedButton.None;
            Globals.swapGridsVisibility(SaveCancelGrid, AddEditRemoveGrid);
            Globals.enableFields(EmployeesPropertiesGrid, false, null, false);
            Globals.emptyAllFields(EmployeesPropertiesGrid);
            EmployeeData.BankAccount = new Bank();
            EmployeeData.Address = new CivicAddress();
            IdComboBox.IsEditable = false;
        }

        private void bankNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bankNameComboBox.SelectedItem != null)
            {
                Bank matchingBank = FactoryBL.BL_instance.getAllBanks().Find(bank => bank.BankName == bankNameComboBox.SelectedItem.ToString());
                if (matchingBank != null)
                {
                    bankNumberTextBox.Text = matchingBank.BankNumber.ToString();
                    EmployeeData.BankAccount.BankNumber = uint.Parse(bankNumberTextBox.Text);
                    bankBranchNumberTextBox.Items.Clear();
                    bankBranchNumberTextBox.Items.Add(matchingBank.BranchNumber);
                    bankAddressTextBox.Text = "";
                }
            }

        }

        private void bankBranchNumberTextBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bankNumberTextBox.Text != "" && bankBranchNumberTextBox.SelectedItem != null)
            {
                Bank matchingBank = (from b in FactoryBL.BL_instance.getAllBanks()
                                     where b.BankNumber == int.Parse(bankNumberTextBox.Text) && b.BranchNumber == int.Parse(bankBranchNumberTextBox.SelectedItem.ToString())
                                     select b).FirstOrDefault();
                if (matchingBank != null)
                {
                    bankAddressTextBox.Text = matchingBank.BranchAddress.ToString();
                }
            }
        }

        private void addressPrivateHouseCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            addressApartmentNumberTextBox.IsEnabled = false;
            addressApartmentNumberTextBox.Clear();
        }

        private void addressPrivateHouseCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (selectedButton != App.SelectedButton.None && selectedButton != App.SelectedButton.Remove)
            {
                addressApartmentNumberTextBox.IsEnabled = true;
            }
        }

        private void IdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedButton != App.SelectedButton.Add && IdComboBox.SelectedItem != null)
            {
                Employee selectedEmployee = FactoryBL.BL_instance.getAllEmployees().Find(em => em.Id == IdComboBox.SelectedItem.ToString());
                if (selectedEmployee != null)
                {
                    Globals.copyObject<Employee>(selectedEmployee, EmployeeData);
                    phoneNumberPrefixComboBox.Text = selectedEmployee.PhoneNumber.Substring(0, 3);
                    phoneNumberTextBox.Text = selectedEmployee.PhoneNumber.Substring(3);
                }
            }
        }
    }
}
