using CRM.Application.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ColorFontChooser.xaml
    /// </summary>
    public partial class FontChooser : UserControl
    {
        #region Properties and Fields

        public FontInfo SelectedFont
        {
            get
            {
                FontInfo result = new FontInfo();
                result.Family = txtSampleText.FontFamily;
                result.Size = txtSampleText.FontSize;
                result.Style = txtSampleText.FontStyle;
                result.TextDecorations = txtSampleText.TextDecorations;
                result.Weight = txtSampleText.FontWeight;
                return result;
            }
        }

        #endregion

        #region Constructor
        //TODO:rad
        public FontChooser()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                lstFamily.Focus();
            };
        }

        #endregion

        #region EventHandler

        private void FontFamiliesFilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstFamily.Items.Filter = new Predicate<object>(f => (f as FontFamily).Source.ToLower().Contains(FontFamiliesFilterTextBox.Text.Trim()));
        }

        private void FocusCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (!FontFamiliesFilterTextBox.IsFocused);
        }

        private void FocusCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            FontFamiliesFilterTextBox.Focus();
        }

        #endregion

    }
}
