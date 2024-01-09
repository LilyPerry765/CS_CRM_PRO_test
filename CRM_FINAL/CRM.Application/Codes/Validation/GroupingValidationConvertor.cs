using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CRM.Application.Codes.Validation
{
	public class GroupingValidationConvertor : IValueConverter
	{
		private static Codes.Validation.XmlValidation varXmlValidation;
        private static Codes.Validation.XmlValidation _xmlValidation
		{
			get
			{
				if (varXmlValidation == null)
				{
                    varXmlValidation = Codes.Validation.ValidationWorking.GetValidationXml();
				}

				return (varXmlValidation);
			}
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var element = _xmlValidation.elements.FirstOrDefault(q => q.Name == value.ToString());

			if (element == null)
			{
				return ("عدم اعتبارسنجی");
			}

			return ("اعتبار سنجی");
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
