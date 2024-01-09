using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CRM.Application.Codes
{
    public static class UIElementExtension
    {
        public static readonly DependencyProperty ResourceNameEditableProperty = DependencyProperty.RegisterAttached("ResourceNameEditable", typeof(string), typeof(UIElement), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetResourceNameEditable(UIElement element, string value)
        {
            if (!Data.DB.CurrentUser.ResourceNames.Contains(value))
            {
                element.IsEnabled = false;
            }
            element.SetValue(ResourceNameEditableProperty, value);
        }

        public static string GetResourceNameEditable(UIElement element)
        {
            return (string)element.GetValue(ResourceNameEditableProperty);
        }

        public static readonly DependencyProperty ResourceNameVisibleProperty = DependencyProperty.RegisterAttached("ResourceNameVisible", typeof(string), typeof(UIElement), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits));

        public static void SetResourceNameVisible(UIElement element, string value)
        {
            if (!Data.DB.CurrentUser.ResourceNames.Contains(value))
            {
                element.Visibility = Visibility.Collapsed;
            }
            element.SetValue(ResourceNameVisibleProperty, value);
        }

        public static string GetResourceNameVisible(UIElement element)
        {
            return (string)element.GetValue(ResourceNameVisibleProperty);
        }
    }
}
