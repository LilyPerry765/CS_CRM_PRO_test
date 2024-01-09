using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Data;

namespace CRM.Application.Codes.Validation
{
    public class WindowIsValid
    {
        public static bool IsValid(DependencyObject parent)
        {
            // Validate all the bindings on the parent
            bool valid = true;
            LocalValueEnumerator localValues = parent.GetLocalValueEnumerator();
            while (localValues.MoveNext())
            {
                LocalValueEntry entry = localValues.Current;
                if (BindingOperations.IsDataBound(parent, entry.Property))
                {
                    Binding binding = BindingOperations.GetBinding(parent, entry.Property);

                    if (binding != null)
                        foreach (ValidationRule rule in binding.ValidationRules)
                        {
                            ValidationResult result = rule.Validate(parent.GetValue(entry.Property), null);

                            Visibility visibility = (Visibility)parent.GetValue(UIElement.VisibilityProperty);
                            bool isEnable = (bool)parent.GetValue(UIElement.IsEnabledProperty);

                            if (!result.IsValid && visibility == Visibility.Visible && isEnable)
                            {
                                BindingExpression expression = BindingOperations.GetBindingExpression(parent, entry.Property);
                                System.Windows.Controls.Validation.MarkInvalid(expression, new ValidationError(rule, expression, result.ErrorContent, null));
                                valid = false;
                            }
                        }
                }
            }

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                UIElement element = child as UIElement;
                if (element != null)
                {
                    if (element.IsEnabled == false || element.Visibility != Visibility.Visible)
                    {
                        continue;
                    }
                }

                if (!IsValid(child))
                {
                    valid = false;
                }
            }

            return valid;
        }
    }
}
