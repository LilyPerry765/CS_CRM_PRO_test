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
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class SettingForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public SettingForm()
        {
            InitializeComponent();
        }

        public SettingForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            Setting setting = new Setting();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                setting = Data.SettingDB.GetSettingById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = setting;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                Setting setting = this.DataContext as Setting;

                setting.Detach();
                Save(setting);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره بانک", ex);
            }
        }

        private void SettingValueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
