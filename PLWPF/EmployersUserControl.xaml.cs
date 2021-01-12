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
    /// Interaction logic for EmployersUserControl.xaml
    /// </summary>
    public partial class EmployersUserControl : UserControl
    {
        private App.SelectedButton selectedButton;
        private Employer EmployerData { get; set; }

        public EmployersUserControl()
        {
            InitializeComponent();
            EmployerData = new Employer();
            EmployerData.Address = new CivicAddress();
            DataContext = EmployerData;
            foreach (Proffesion x in Enum.GetValues(typeof(BE.Proffesion)))
            {
                personalEducationComboBox.Items.Add(x);
            }
            foreach (Employer emp in FactoryBL.BL_instance.getAllEmployers())
            {
                IdComboBox.Items.Add(emp.Id);
            }
            personalEducationComboBox.SelectedItem = null;
            selectedButton = App.SelectedButton.None;
        }

        private void phoneNumberPrefixComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EmployerData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void phoneNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidPhoneNumber(phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text) && phoneNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid phone number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                phoneNumberTextBox.Text = "";
            }
            EmployerData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add && !App.isValidID(IdComboBox.Text))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
                IdComboBox.SelectedItem = null;
            }
            else
            {
                if (selectedButton == App.SelectedButton.Add)
                {
                    EmployerData.Id = IdComboBox.Text;
                }
                if ((selectedButton == App.SelectedButton.Edit || selectedButton == App.SelectedButton.Remove) && IdComboBox.SelectedItem != null)
                {
                    EmployerData.Id = IdComboBox.SelectedItem.ToString();
                }
            }
        }

        private void lastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(lastNameTextBox.Text) && lastNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid last name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                lastNameTextBox.Text = "";
            }
        }

        private void firstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(firstNameTextBox.Text) && firstNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid first name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                firstNameTextBox.Text = "";
            }
        }

        private void companyNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(companyNameTextBox.Text) && companyNameTextBox.Text != "")
            {
                MessageBox.Show("Invalid company name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                companyNameTextBox.Text = "";
            }
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
            if (!App.isValidNumber(addressHouseNumberTextBox.Text, 0) && addressHouseNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid house number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressHouseNumberTextBox.Text = "";
            }
        }

        private void addressApartmentNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressApartmentNumberTextBox.Text, 0) && addressApartmentNumberTextBox.Text != "")
            {
                MessageBox.Show("Invalid apartment number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressApartmentNumberTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployersPropertiesGrid, false, null, true);
            IdComboBox.IsEditable = true;
            selectedButton = App.SelectedButton.Add;
            Globals.setToToday(setupDateDatePicker);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployersPropertiesGrid, false, null, true);
            selectedButton = App.SelectedButton.Edit;
            Globals.setToToday(setupDateDatePicker);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(EmployersPropertiesGrid, true, IdComboBox);
            selectedButton = App.SelectedButton.Remove;
            Globals.setToToday(setupDateDatePicker);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add)
            {
                if (!Globals.isEverythingNotNull<Employer>(EmployerData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employer addEmployer = new Employer();
                    Globals.copyObject(EmployerData, addEmployer);
                    FactoryBL.BL_instance.addEmployer(addEmployer);
                    IdComboBox.Items.Clear();
                    foreach (Employer emp in FactoryBL.BL_instance.getAllEmployers())
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
                if (!Globals.isEverythingNotNull<Employer>(EmployerData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employer oldEmployer = FactoryBL.BL_instance.getAllEmployers().Find(x => x.Id == EmployerData.Id);
                    Employer editEmployer = new Employer();
                    Globals.copyObject<Employer>(EmployerData, editEmployer);
                    FactoryBL.BL_instance.updateEmployer(editEmployer, oldEmployer);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (selectedButton == App.SelectedButton.Remove)
            {
                if (EmployerData.Id == null)
                {
                    MessageBox.Show("Fill ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Employer removeEmployer = new Employer();
                    Globals.copyObject(EmployerData, removeEmployer);
                    FactoryBL.BL_instance.removeEmployer(removeEmployer);
                    IdComboBox.Items.Clear();
                    foreach (Employer emp in FactoryBL.BL_instance.getAllEmployers())
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
            Globals.enableFields(EmployersPropertiesGrid, false, null, false);
            Globals.emptyAllFields(EmployersPropertiesGrid);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = App.SelectedButton.None;
            Globals.swapGridsVisibility(SaveCancelGrid, AddEditRemoveGrid);
            Globals.enableFields(EmployersPropertiesGrid, false, null, false);
            Globals.emptyAllFields(EmployersPropertiesGrid);
            EmployerData.Address = new CivicAddress();
            IdComboBox.IsEditable = false;
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
                Employer selectedEmployer = FactoryBL.BL_instance.getAllEmployers().Find(em => em.Id == IdComboBox.SelectedItem.ToString());
                if (selectedEmployer != null)
                {
                    Globals.copyObject<Employer>(selectedEmployer, EmployerData);
                    phoneNumberPrefixComboBox.Text = selectedEmployer.PhoneNumber.Substring(0, 3);
                    phoneNumberTextBox.Text = selectedEmployer.PhoneNumber.Substring(3);
                }
            }
        }
    }
}
