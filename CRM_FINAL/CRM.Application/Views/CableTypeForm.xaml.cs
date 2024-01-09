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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class CableTypeForm : Local.PopupWindow
    {
        private int _ID = 0;

        public CableTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CableTypeForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {

        }

        private void LoadData()
        {
            CableType item = new CableType();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.CableTypeDB.GetCableTypeByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                CableType item = this.DataContext as CableType;
                
                item.Detach();
                if (item.IsReadOnly == false)
                {
                    Save(item);
                    ShowSuccessMessage("نوع کابل ذخیره شد");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("امکان تغییر برای این نوع کابل موجود نمی باشد");
                }
                
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره نوع کابل", ex);
            }
        }
    }
}
