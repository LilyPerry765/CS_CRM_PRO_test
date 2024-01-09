using CRM.Application.Local;
using CRM.Data;
using Enterprise;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for WarningMessageForm.xaml
    /// </summary>
    public partial class WarningMessageForm : PopupWindow
    {
        #region Properties And Fields

        private int _ID;

        #endregion

        #region Constructor
        public WarningMessageForm()
        {
            InitializeComponent();
            Initialize();
        }

        public WarningMessageForm(int id)
            : this()
        {
            this._ID = id;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            WarningTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WarningHistory));
        }

        public void LoadData()
        {
            WarningMessage item = new WarningMessage();
            if (this._ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = WarningMessageDB.GetWarningMessageById(this._ID);
                SaveButton.Content = "بروزرسانی";
            }
            this.DataContext = item;
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                return;
            }
            try
            {
                WarningMessage item = this.DataContext as WarningMessage;
                if (item == null) return;
                if (string.IsNullOrEmpty(WarningMessageTextBox.Text.Trim()) || WarningTypeComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show(".لطفاً تمام مقادیر را تعیین نمائید", "توجه", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                DB.Save(item);
                ShowSuccessMessage(".پیام اخطار با موفقیت ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
                ShowErrorMessage("خطا در ذخیره", ex);
            }
        }

        private void WarningTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WarningTypeComboBox.SelectedValue != null)
            {
                int selectedWarningTypeId = Convert.ToInt32(WarningTypeComboBox.SelectedValue);
                if (WarningMessageDB.HasMessage(selectedWarningTypeId))
                {
                    MessageBox.Show(".نوع اخطار انتخاب شده دارای پیغام میباشد ", "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        #endregion

    }
}
