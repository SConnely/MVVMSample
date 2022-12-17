using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVM_Sample.Rules
{
    public class CustomValidationRule : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int val = 0;

            try
            {
                if (((string)value).Length > 0)
                    val = Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Non-integer value");
            }

            if ((val < Min) || (val > Max))
            {
                return new ValidationResult(false,
                  $"Please enter an integer in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
