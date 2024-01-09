using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CRM.Application.Codes.Validation
{
	public class RequireValidation : ValidationRule
	{
		public string ErrorMessage { get; set; }

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if (value == null || value.ToString().Trim() == string.Empty)
			{
				return new ValidationResult(false, ErrorMessage);
			}

			return new ValidationResult(true, null);
		}
	}
}
