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
    /// Interaction logic for SpecializationsUserControl.xaml
    /// </summary>
    public partial class SpecializationsUserControl : UserControl
    {
        private App.SelectedButton selectedButton;
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
            foreach (Specialization.Education edu in Enum.GetValues(typeof(Specialization.Education)))
            {
                degreeComboBox.Items.Add(edu);
            }
            foreach (Specialization sp in FactoryBL.BL_instance.getAllSpecializations())
            {
                IdComboBox.Items.Add(sp.Id);
            }
            degreeComboBox.SelectedItem = null;
            selectedButton = App.SelectedButton.None;
        }

        private void IdComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add && !App.isValidNumber(IdComboBox.Text,0) && IdComboBox.Text != "")
            {
                MessageBox.Show("Invalid ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                IdComboBox.Text = "";
                IdComboBox.SelectedItem = null;
            }
            else
            {
                if (selectedButton == App.SelectedButton.Add)
                {
                    SpecializationData.Id = IdComboBox.Text;
                }
                if ((selectedButton == App.SelectedButton.Edit || selectedButton == App.SelectedButton.Remove) && IdComboBox.SelectedItem != null)
                {
                    SpecializationData.Id = IdComboBox.SelectedItem.ToString();
                }
            }
        }

        private void minWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(minWageForHourTextBox.Text, 0) && minWageForHourTextBox.Text != "")
            {
                MessageBox.Show("Invalid min wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                minWageForHourTextBox.Text = "";
            }
        }

        private void maxWageForHourTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.isValidNumber(maxWageForHourTextBox.Text, 0) && maxWageForHourTextBox.Text != "")
            {
                MessageBox.Show("Invalid max wage for hour", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                maxWageForHourTextBox.Text = "";
            }
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!App.containsOnlyLetters(nameTextBox.Text) && nameTextBox.Text != "")
            {
                MessageBox.Show("Invalid name", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                nameTextBox.Text = "";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(SpecializationsPropertiesGrid, false, null, true);
            IdComboBox.IsEditable = true;
            selectedButton = App.SelectedButton.Add;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(SpecializationsPropertiesGrid, false, null, true);
            selectedButton = App.SelectedButton.Edit;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Globals.swapGridsVisibility(AddEditRemoveGrid, SaveCancelGrid);
            Globals.enableFields(SpecializationsPropertiesGrid, true, IdComboBox);
            selectedButton = App.SelectedButton.Remove;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedButton == App.SelectedButton.Add)
            {
                if (!Globals.isEverythingNotNull<Specialization>(SpecializationData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Specialization addSpecialization = new Specialization();
                    Globals.copyObject(SpecializationData, addSpecialization);
                    FactoryBL.BL_instance.addSpecialization(addSpecialization);
                    IdComboBox.Items.Clear();
                    foreach (Specialization spec in FactoryBL.BL_instance.getAllSpecializations())
                    {
                        IdComboBox.Items.Add(spec.Id);
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
                if (!Globals.isEverythingNotNull<Specialization>(SpecializationData))
                {
                    MessageBox.Show("Fill all fields", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Specialization oldSpecialization = FactoryBL.BL_instance.getAllSpecializations().Find(x => x.Id == SpecializationData.Id);
                    Specialization editSpecialization = new Specialization();
                    Globals.copyObject<Specialization>(SpecializationData, editSpecialization);
                    FactoryBL.BL_instance.updateSpecialization(editSpecialization, oldSpecialization);
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (selectedButton == App.SelectedButton.Remove)
            {
                if (SpecializationData.Id == null)
                {
                    MessageBox.Show("Fill ID", "Don't mess with me!!!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    Specialization removeSpecialization = new Specialization();
                    Globals.copyObject(SpecializationData, removeSpecialization);
                    FactoryBL.BL_instance.removeSpecialization(removeSpecialization);
                    IdComboBox.Items.Clear();
                    foreach (Specialization spec in FactoryBL.BL_instance.getAllSpecializations())
                    {
                        IdComboBox.Items.Add(spec.Id);
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
            Globals.enableFields(SpecializationsPropertiesGrid, false, null, false);
            Globals.emptyAllFields(SpecializationsPropertiesGrid);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            selectedButton = App.SelectedButton.None;
            Globals.swapGridsVisibility(SaveCancelGrid, AddEditRemoveGrid);
            Globals.enableFields(SpecializationsPropertiesGrid, false, null, false);
            Globals.emptyAllFields(SpecializationsPropertiesGrid);
            IdComboBox.IsEditable = false;
        }

        private void IdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedButton != App.SelectedButton.Add && IdComboBox.SelectedItem != null)
            {
                Specialization selectedSpecialization = FactoryBL.BL_instance.getAllSpecializations().Find(spec => spec.Id == IdComboBox.SelectedItem.ToString());
                if (selectedSpecialization != null)
                {
                    Globals.copyObject<Specialization>(selectedSpecialization, SpecializationData);
                }
            }
        }
    }
}
