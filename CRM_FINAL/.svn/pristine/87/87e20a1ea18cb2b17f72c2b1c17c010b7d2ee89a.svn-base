using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace CRM.Application.Codes
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand PrintCommand = new RoutedUICommand("Print", "PrintCommand", typeof(CustomCommands));

        public static readonly RoutedUICommand SaveCommand = new RoutedUICommand("Save", "SaveCommand", typeof(CustomCommands));

        static RoutedUICommand _focusCommand = new RoutedUICommand("Set focus to FontFamiliesFilterTextBox!", "FocusRoutedCommand", typeof(CustomCommands));

        public static RoutedUICommand FocusCommand
        {
            get
            {
                return _focusCommand;
            }
        }

    }

    public static class CustomFocusManager
    {
        public static bool GetIsFocused(UIElement element)
        {
            return (bool)element.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(UIElement element, bool value)
        {
            element.SetValue(IsFocusedProperty, value);
        }
        /// <summary>
        /// .این ویژگی به کاربر امکان تعیین مقدار را با استفاده از کیبورد میدهد . به طور مثال در داخل کوبو باکسی که چک باکس دارد ، کاربر میتواند تیک را با استفاده از کیبورد بزند
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(CustomFocusManager), new PropertyMetadata
        {
            PropertyChangedCallback = (obj, e) =>
            {
                if ((bool)e.NewValue &&
                    !(bool)e.OldValue & obj is IInputElement)
                {
                    ((IInputElement)obj).Focus();
                }
            }
        });
    }

}
