using BE;
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
using System.Windows.Data;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public enum SelectedButton
        {
            Add,
            Edit,
            Remove,
            None
        }
        
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
            if (!isNumber(phoneNumber) || phoneNumber.Length != 10 || int.Parse(phoneNumber) < 0)
            {
                return false;
            }
            return true;
        }

        public static bool isValidID(string id)
        {
            if ((!isValidNumber(id,0) || id.Length != 9) && id != "" && id != null)
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
                if (!((allowSpaces && c == ' ') || (char.IsLetter(c))))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool isValidEmail(string email)
        {
            if (email.Split('@').Length != 2 || email.Split('@')[1].Split('.').Length < 2 || email.Count(x => x == ' ') > 0)
            {
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Converts null objects to paramter and not null values to null.
    /// </summary>
    public class NullToNotConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return new object();
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }
    }
    public class CivicAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is CivicAddress)
            {
                return (value as CivicAddress).ToString(); 
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if (value != null && value is string)
            {
                string valueString = value as string;
                if (valueString != "")
                {
                    string[] splitedString = valueString.Split(',');
                    string city = splitedString[0];
                    string street = splitedString[1].Split(' ')[2];
                    uint houseNumber = uint.Parse(splitedString[2].Split(' ')[2]);
                    uint? apartmentNumber = null;
                    bool isPrivateHouse = true;
                    if (splitedString.Length == 4)
                    {
                        isPrivateHouse = false;
                        apartmentNumber = uint.Parse(splitedString[3].Split(' ')[2]);
                    }
                    return new CivicAddress { ApartmentNumber = apartmentNumber, City = city, HouseNumber = houseNumber, IsPrivateHouse = isPrivateHouse, StreetName = street };
                }
            }
            return null;
        }
    }
    public class NullableBooleanToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? valueBool = (bool?)value;
            if (valueBool == null)
            {
                return false;
            }
            return (bool)value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DateTimeToAgeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime valueDateTime = (DateTime)value;
            TimeSpan substracted = DateTime.Today - valueDateTime;
            int days = substracted.Days;
            return (days / 365);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PhoneNumberToPhoneNumberWithHyphenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string valueString = (string)value;
            return valueString.Substring(0, 3) + "-" + valueString.Substring(3);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
