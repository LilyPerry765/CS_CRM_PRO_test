using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CRM.Application.Views.Validation
{
    public partial class SetValidationWindow : Window
    {
        public SetValidationWindow()
        {
            InitializeComponent();
        }

        public string NameOfControl { get; set; }

        private Codes.Validation.Element _element;
        private Codes.Validation.XmlValidation _xmlValidation;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<TextBox> lstTextBox = Codes.Validation.Helper.FindVisualChildren<TextBox>(this).ToList();
            List<ComboBox> lstComboBox = Codes.Validation.Helper.FindVisualChildren<ComboBox>(this).ToList();
            
            lstTextBox.ForEach(textBox =>
                {
                    textBox.GotFocus += new RoutedEventHandler(textBox_GotFocus);
                });

            lstComboBox.ForEach(comboBox =>
                {
                    comboBox.GotFocus += comboBox_GotFocus;
                });

            _xmlValidation = Codes.Validation.ValidationWorking.GetValidationXml();
            _element = _xmlValidation.elements.FirstOrDefault(q => q.Name == NameOfControl);

            if (_element == null)
            {
                _element = new Codes.Validation.Element();
                _element.Name = NameOfControl;

                _xmlValidation.elements.Add(_element);
            }

            Codes.Validation.Helper.FindVisualChildren<Button>(this).ToList().ForEach(button =>
                {
                    button.Click += new RoutedEventHandler(button_Click);
                    button.Visibility = Visibility.Collapsed;
                });

            Codes.Validation.Helper.FindVisualChildren<DataGrid>(this).ToList().ForEach(grid =>
                {
                    grid.Visibility = Visibility.Collapsed;
                });

            this.Resources.Clear();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("شما در فرم اعتبار سنجی می باشید");
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (sender as TextBox);

            string textBoxName = textBox.Name;

            if (textBoxName == "PART_TextBox")
            {
                Control control = Codes.Validation.Helper.FindVisualParent<Control>(textBox);

                if (control != null)
                {
                    ValidThisControl(control, false);

                    return;
                }
            }

            ValidThisControl(textBox);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);

            ValidThisControl(comboBox, false);
        }

        private void ValidThisControl(Control control, bool regxEnable = true)
        {
            string comboName = control.Name;

            UserControl uc = Codes.Validation.Helper.FindVisualParent<UserControl>(control);
            if (uc != null)
            {
                MessageBox.Show(string.Format("این کنترل درون {0} قرار دارد و بایستی به صورت جداگانه اعتبار سنجی شود", uc.GetType().FullName));
                return;
            }

            if (comboName == string.Empty)
            {
                MessageBox.Show("برای اعتبار سنجی کنترل بایستی دارای نام باشد");
                return;
            }

            SetValidationDetailWindow setValidationDetailWindow =
                new SetValidationDetailWindow();
            setValidationDetailWindow.grdRegx.IsEnabled = regxEnable;

            if (_element.Controls.FirstOrDefault(q => q.Name == comboName) == null)
            {
                _element.Controls.Add(new Codes.Validation.Control() { Name = comboName });
            }

            setValidationDetailWindow.DataContext = _element.Controls.FirstOrDefault(q => q.Name == comboName);
            if (setValidationDetailWindow.ShowDialog() == true)
            {
                _element.Controls.FirstOrDefault(q => q.Name == comboName).IsRequire
                    = setValidationDetailWindow.chkIsRequired.IsChecked ?? false;

                _element.Controls.FirstOrDefault(q => q.Name == comboName).RegularExpression =
                    setValidationDetailWindow.comRegx.SelectedValue == null ?
                    string.Empty : setValidationDetailWindow.comRegx.SelectedValue.ToString();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("آیا مایل به ذخیره تغییرات هستید");

            switch (dialogResult)
            {
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Yes:
                    Codes.Validation.ValidationWorking.SaveValidationXml(_xmlValidation);
                    break;
                default:
                    break;
            }
        }
    }
}
