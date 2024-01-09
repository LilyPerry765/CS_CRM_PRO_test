using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace CRM.Application.Codes.Validation
{
	public class HasContentToCBrushConvertor : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value is Codes.Validation.ValidationUserControlAssociate)
			{
				var validationUserControlAssociate = value as Codes.Validation.ValidationUserControlAssociate;

				if (validationUserControlAssociate.HasContent)
				{
					return (Brushes.Blue);
				}
				else
				{
					return (Brushes.Orange);
				}
			}
			else if (value is Codes.Validation.ValidationWindowAssociate)
			{
                var validationWindowAssociate = value as Codes.Validation.ValidationWindowAssociate;

				if (validationWindowAssociate.HasContent)
				{
					return (Brushes.Blue);
				}
				else
				{
					return (Brushes.Orange);
				}
			}

			return (null);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
