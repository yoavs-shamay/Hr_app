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
    /// Interaction logic for EmployersUserControl.xaml
    /// </summary>
    public partial class EmployersUserControl : UserControl
    {
        private Employer EmployerData { get; set; }

        public EmployersUserControl()
        {
            InitializeComponent();
            EmployerData = new Employer();
            DataContext = EmployerData;
        }

        private void phoneNumberPrefixComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            EmployerData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private bool isNumber(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void phoneNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!isNumber(phoneNumberTextBox.Text) || phoneNumberTextBox.Text.Length != 8 || int.Parse(phoneNumberTextBox.Text) < 0)
            {
                MessageBox.Show("Invalid phone number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                phoneNumberTextBox.Text = "";
            }
            EmployerData.PhoneNumber = phoneNumberPrefixComboBox.Text + phoneNumberTextBox.Text;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidID(IdComboBox.Text))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
            }
        }

        private void lastNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(lastNameTextBox.Text))
            {
                MessageBox.Show("Invalid last name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void firstNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(firstNameTextBox.Text))
            {
                MessageBox.Show("Invalid first name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                firstNameTextBox.Text = "";
            }
        }

        private void companyNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(firstNameTextBox.Text))
            {
                MessageBox.Show("Invalid company name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                firstNameTextBox.Text = "";
            }
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
            if (!App.isValidNumber(addressHouseNumberTextBox.Text, 0))
            {
                MessageBox.Show("Invalid house number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressHouseNumberTextBox.Text = "";
            }
        }

        private void addressApartmentNumberTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(addressApartmentNumberTextBox.Text, 0))
            {
                MessageBox.Show("Invalid apartment number", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                addressApartmentNumberTextBox.Text = "";
            }
        }
    }
}
