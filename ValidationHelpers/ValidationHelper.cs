using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace lirksi.Mobile.ValidationHelpers
{
    public static class ValidationHelper
    {

        public static bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
                else
                {
                    if (pi.GetValue(myObject) == null)
                        return true;
                }
            }
            return false;
        }

        public static bool CheckIsNumberic(string val)
        {
            return Regex.IsMatch(val, @"^\d+$");
        }
    }
}
