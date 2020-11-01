using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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
    }
}
