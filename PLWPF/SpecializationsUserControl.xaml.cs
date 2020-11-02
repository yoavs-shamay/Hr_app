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
    /// Interaction logic for SpecializationsUserControl.xaml
    /// </summary>
    public partial class SpecializationsUserControl : UserControl
    {
        private Specialization SpecializationData { get; set; }
        public SpecializationsUserControl()
        {
            InitializeComponent();
            SpecializationData = new Specialization();
            DataContext = SpecializationData;
            foreach (Proffesion x in Enum.GetValues(typeof(BE.Proffesion)))
            {
                areaComboBox.Items.Add(x);
            }
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(IdComboBox.Text, 0))
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
            }
        }

        private void minWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(minWageForHourTextBox.Text, 0))
            {
                MessageBox.Show("Invalid min wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                minWageForHourTextBox.Text = "";
            }
        }

        private void maxWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(maxWageForHourTextBox.Text, 0))
            {
                MessageBox.Show("Invalid max wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                maxWageForHourTextBox.Text = "";
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(nameTextBox.Text))
            {
                MessageBox.Show("Invalid name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                nameTextBox.Text = "";
            }
        }
    }
}
