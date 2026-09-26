using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoCareApp.PresentationLayer.Helper
{
    public class InputValidator
    {
        public static bool ValidateInteger(string input, out int number)
        {
            return int.TryParse(input, out number);
        }

        public static bool ValidateDouble(string input, out double number)
        {
            return double.TryParse(input, out number);
        }

        public static bool ValidateString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.All(ch => char.IsLetter(ch));
        }

        public static bool ValidateVehicleNumber(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"^[A-Z]{2}\d{2}[A-Z]{2}\d{4}$");
        }
    }
}
