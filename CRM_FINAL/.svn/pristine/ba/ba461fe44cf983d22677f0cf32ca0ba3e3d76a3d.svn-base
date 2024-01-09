using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CRM.Application.Codes.Validation
{
	public class RegularExpressionValidation : ValidationRule
	{
		public string RegularExperssion { get; set; }

		public string ErrorMessage { get; set; }

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if ((value.ToString().Trim() != string.Empty) && System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), RegularExperssion) == false)
			{
				return new ValidationResult(false, ErrorMessage);
			}
			else
			{
				return new ValidationResult(true, null);
			}
		}
	}
}
