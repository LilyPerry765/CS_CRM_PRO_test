using CRM.Application.Local;
using CRM.Data;
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
    /// Interaction logic for PowerTypeForm.xaml
    /// </summary>
    public partial class PowerTypeForm : PopupWindow
    {
        #region Properties and Fields

        private int _ID = 0;

        #endregion

        #region Constructor

        public PowerTypeForm()
        {
            InitializeComponent();
        }

        public PowerTypeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region EventHandlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                
                if (string.IsNullOrEmpty(TitleTextBox.Text.Trim()))
                {
                    throw new ArgumentException(".تعیین عنوان پاور الزامی است");
                }
                if (string.IsNullOrEmpty(RateTextBox.Text.Trim()))
                {
                    throw new ArgumentException(".تعیین میزان پاور الزامی است");
                }

                PowerType powerType = this.DataContext as PowerType;
                powerType.Detach();
                Save(powerType);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                {
                    ShowErrorMessage(".مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره نوع پاور",ex);
                }
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            PowerType item = new PowerType();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = PowerTypeDB.GetPowerTypeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }
            this.DataContext = item;
        }

        #endregion

    }
}
