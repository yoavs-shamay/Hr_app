using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool isNumber(string str)
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
        public static bool isValidPhoneNumber(string phoneNumber)
        {
            if (!isNumber(phoneNumber) || phoneNumber.Length != 8 || int.Parse(phoneNumber) < 0)
            {
                return false;
            }
            return true;
        }

        public static bool isValidID(string id)
        {
            if (!isValidNumber(id,0) || id.Length != 9)
            {
                return false;
            }
            return true;
        }

        public static bool isValidNumber(string number, double min = double.MinValue, double max = double.MaxValue)
        {
            if (!isNumber(number) || int.Parse(number) < min || int.Parse(number) > max)
            {
                return false;
            }
            return true;
        }

        public static bool containsOnlyLetters(string str, bool allowSpaces = true)
        {
            foreach(char c in str)
            {
                if (!char.IsLetter(c) && (!allowSpaces || c == ' '))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isValidEmail(string email)
        {
            if (email.Split('@').Length != 2 || email.Split('@')[1].Split('.').Length < 2)
            {
                return false;
            }
            return true;
        }

        public static void swapGridsVisibility(Grid gridToHide, Grid gridToShow)
        {
            gridToHide.Visibility = Visibility.Collapsed;
            gridToShow.Visibility = Visibility.Visible;
        }

        public static void enableFields(Grid grid, bool enableOnlyId = false, ComboBox idComboBox = null, bool enable = true)
        {
            if (!enableOnlyId)
            {
                foreach (var child in grid.Children)
                {
                    if (child is Control)
                    {
                        (child as Control).IsEnabled = enable;
                    }
                    if (child is Grid)
                    {
                        enableFields(child as Grid, enableOnlyId, idComboBox, enable);
                    }
                }
            }
            else
            {
                idComboBox.IsEnabled = enable;
            }
        }

        private static void setPropertyToNullIfExists(string propertyName, object obj)
        {
            var property = obj.GetType().GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(obj, null);
            }
        }

        public static void emptyAllFields(Grid grid)
        {
            foreach (var child in grid.Children)
            {
                if (child is Control)
                {
                    /*setPropertyToNullIfExists("Content", child);
                    setPropertyToNullIfExists("SelectedItem", child);
                    setPropertyToNullIfExists("Text", child);
                    setPropertyToNullIfExists("IsChecked", child);
                    setPropertyToNullIfExists("SelectedDate", child);*/
                    if (child is TextBox)
                    {
                        (child as TextBox).Text = "";
                    }
                    if (child is ComboBox)
                    {
                        (child as ComboBox).Text = "";
                    }
                    if (child is DatePicker)
                    {
                        (child as DatePicker).SelectedDate = null;
                    }
                    if (child is CheckBox)
                    {
                        (child as CheckBox).IsChecked = false;
                    }
                }
                if (child is Grid)
                {
                    emptyAllFields(child as Grid);
                }
            }
        }
    }
}
