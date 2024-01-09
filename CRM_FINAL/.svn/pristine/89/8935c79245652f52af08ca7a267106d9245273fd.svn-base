using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace CRM.Application.Codes.Validation
{
	public class CloneBinding
	{
		public static Binding Clone(Binding binding)
		{
			try
			{
				Binding returnBinding =
					new Binding();

				returnBinding.AsyncState = binding.AsyncState;

				returnBinding.BindingGroupName = binding.BindingGroupName;

				returnBinding.BindsDirectlyToSource = binding.BindsDirectlyToSource;

				returnBinding.Converter = binding.Converter;

				returnBinding.ConverterCulture = binding.ConverterCulture;

				returnBinding.ConverterParameter = binding.ConverterParameter;

				returnBinding.ElementName = binding.ElementName;

				//returnBinding.FallbackValue = binding.FallbackValue;

				returnBinding.IsAsync = binding.IsAsync;

				returnBinding.Mode = binding.Mode;

				returnBinding.NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated;

				returnBinding.NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated;

				returnBinding.NotifyOnValidationError = binding.NotifyOnValidationError;

				returnBinding.Path = binding.Path;

				if (binding.RelativeSource != null)
				{
					returnBinding.RelativeSource = binding.RelativeSource;
				}

				if (binding.Source != null)
				{
					returnBinding.Source = binding.Source;
				}

				returnBinding.StringFormat = binding.StringFormat;

				returnBinding.TargetNullValue = binding.TargetNullValue;

				returnBinding.UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter;

				returnBinding.UpdateSourceTrigger = binding.UpdateSourceTrigger;

				returnBinding.ValidatesOnDataErrors = binding.ValidatesOnDataErrors;

				returnBinding.ValidatesOnExceptions = binding.ValidatesOnExceptions;

				//returnBinding.ValidationRules = binding.ValidationRules;

				returnBinding.XPath = binding.XPath;

				return (returnBinding);
			}
			catch
			{
				return (null);
			}
			finally
			{

			}
		}
	}
}
