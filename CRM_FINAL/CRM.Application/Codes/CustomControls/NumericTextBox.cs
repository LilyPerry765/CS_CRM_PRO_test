using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRM.Application.Codes.CustomControls
{
    //TODO:rad
    /// <summary>
    /// .در این کنترل کاربر فقط اجازه وارد کردن اعداد را دارد. حتی کاراکترهای غیر عددی را با کپی - پیست هم نمیتواند وارد نماید
    /// </summary>
    public class NumericTextBox : TextBox
    {
        #region Properties and Fields

        new public string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = LeaveOnlyNumbers(value);
            }
        }

        #endregion

        #region Constructor

        public NumericTextBox()
        {
            TextChanged += new TextChangedEventHandler(NumericTextBox_TextChanged);
            KeyDown += new System.Windows.Input.KeyEventHandler(NumericTextBox_KeyDown);
        }

        #endregion

        #region EventHandlers

        private void NumericTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            e.Handled = !IsNumberKey(e.Key) && !IsActionKey(e.Key);
        }

        private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            base.Text = LeaveOnlyNumbers(Text);
        }

        #endregion

        #region Methods

        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsActionKey(Key inKey)
        {
            return (inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab || inKey == Key.Return || Keyboard.Modifiers.HasFlag(ModifierKeys.Alt));
        }

        private string LeaveOnlyNumbers(string value)
        {
            string temp = value;
            foreach (char c in value.ToArray())
            {
                if (!IsDigit(c))
                {
                    temp = temp.Replace(c.ToString(), "");
                }
            }
            return temp;
        }

        public bool IsDigit(Char c)
        {
            return (c >= '0' && c <= '9');
        }

        #endregion
    }
}
