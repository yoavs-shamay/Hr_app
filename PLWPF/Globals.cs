using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace PLWPF
{
    class Globals
    {
        public static void swapGridsVisibility(Grid gridToHide, Grid gridToShow)
        {
            gridToHide.Visibility = Visibility.Collapsed;
            gridToShow.Visibility = Visibility.Visible;
        }

        public static void enableFields(Grid grid, bool enableOnlyId = false, ComboBox idComboBox = null, bool enable = true, params Control[] notEnable)
        {
            if (!enableOnlyId)
            {
                foreach (var child in grid.Children)
                {
                    if (child is Control && !notEnable.Contains(child))
                    {
                        (child as Control).IsEnabled = enable;
                    }
                    if (child is Grid)
                    {
                        enableFields(child as Grid, enableOnlyId, idComboBox, enable,notEnable);
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

        public static bool isEverythingNotNull<T>(T obj)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.GetValue(obj) == null)
                {
                    return false;
                }
            }
            return true;
        }

        public static void copyObject<T>(T source, T target)
        {
            var props = typeof(T).GetProperties();
            foreach (var p in props)
            {
                if (p.PropertyType == typeof(CivicAddress))
                {
                    CivicAddress instance = new CivicAddress();
                    CivicAddress propertySource = p.GetValue(source) as CivicAddress;
                    copyObject<CivicAddress>(propertySource, instance);
                    p.SetValue(target, instance);
                }
                else if (p.PropertyType == typeof(Bank))
                {
                    Bank instance = new Bank();
                    Bank propertySource = p.GetValue(source) as Bank;
                    copyObject<Bank>(propertySource, instance);
                    p.SetValue(target, instance);
                }
                else {
                    p.SetValue(target, p.GetValue(source));
                }
            }
        }

        public static void setToToday(DatePicker datePicker)
        {
            datePicker.SelectedDate = DateTime.Today; //TODO use this to all date pickers on init
        }
    }
}
