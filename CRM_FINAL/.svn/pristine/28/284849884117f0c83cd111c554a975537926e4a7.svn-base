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
    /// Interaction logic for ColorFontDialog.xaml
    /// </summary>
    public partial class PersianFontDialog : Window
    {
        #region Properties and Fields

        private FontInfo selectedFont;

        public FontInfo Font
        {
            get
            {
                return this.selectedFont;
            }
            set
            {
                FontInfo fi = value;
                this.selectedFont = fi;
            }
        }

        #endregion

        #region Constructor

        public PersianFontDialog()
        {
            this.selectedFont = null;
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Font != null)
            {
                this.SyncFontName();
                this.SyncFontSize();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Font = this.MainFontChooser.SelectedFont;
            this.DialogResult = true;
        }

        #endregion

        #region Methods

        private void SyncFontName()
        {
            string fontFamilyName = this.selectedFont.Family.Source;
            int index = 0;
            foreach (var item in this.MainFontChooser.lstFamily.Items)
            {
                string itemName = item.ToString();
                if (fontFamilyName == itemName)
                {
                    break;
                }
                index++;
            }
            this.MainFontChooser.lstFamily.SelectedIndex = index;
            this.MainFontChooser.lstFamily.ScrollIntoView(this.MainFontChooser.lstFamily.Items[index]);
        }

        private void SyncFontSize()
        {
            double fontSize = this.selectedFont.Size;
            this.MainFontChooser.fontSizeSlider.Value = fontSize;
        }

        #endregion

    }
}
