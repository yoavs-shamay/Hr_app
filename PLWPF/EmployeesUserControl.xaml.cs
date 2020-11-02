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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for EmployeesUserControl.xaml
    /// </summary>
    public partial class EmployeesUserControl : UserControl
    {
        private Employee EmployeeData { get; set; }
        public EmployeesUserControl()
        {
            InitializeComponent();
            EmployeeData = new Employee();
            DataContext = EmployeeData;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
           if (!App.isValidID(IdComboBox.Text))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
            }
        }

        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(LastNameTextBox.Text))
            {
                MessageBox.Show("Invalid last name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                LastNameTextBox.Text = "";
            }
        }

        private void FirstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(FirstNameTextBox.Text))
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
            if (!App.isValidPhoneNumber(phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text))
            {
                MessageBox.Show("Invalid phone number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                phoneNumberTextBox.Text = "";
            }
            EmployeeData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void addressCityTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(addressCityTextBox.Text))
            {
                MessageBox.Show("Invalid city", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressCityTextBox.Text = "";
            }
        }

        private void addressStreetNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(addressStreetNameTextBox.Text))
            {
                MessageBox.Show("Invalid street name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressStreetNameTextBox.Text = "";
            }
        }

        private void addressHouseNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressHouseNumberTextBox.Text, 1))
            {
                MessageBox.Show("Invalid house number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressHouseNumberTextBox.Text = "";
            }
        }

        private void addressApartmentNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressHouseNumberTextBox.Text, 1))
            {
                MessageBox.Show("Invalid appartment number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressHouseNumberTextBox.Text = "";
            }
        }

        private void bankAccountNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(bankAccountNumberTextBox.Text, 0))
            {
                MessageBox.Show("Invalid bank account number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                bankAccountNumberTextBox.Text = "";
            }
        }

        private void bankBranchNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(bankBranchNumberTextBox.Text, 0))
            {
                MessageBox.Show("Invalid bank branch number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                bankBranchNumberTextBox.Text = "";
            }
        }

        private void emailTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidEmail(emailTextBox.Text))
            {
                MessageBox.Show("Invalid email", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                emailTextBox.Text = "";
            }
        }

        private void yearsOfExperienceTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(yearsOfExperienceTextBox.Text, 0))
            {
                MessageBox.Show("Invalid years of experience", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                yearsOfExperienceTextBox.Text = "";
            }
        }
    }
}
