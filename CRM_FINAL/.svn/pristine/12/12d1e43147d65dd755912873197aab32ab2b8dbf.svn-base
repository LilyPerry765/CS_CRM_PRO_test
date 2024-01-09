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
    public partial class CustomerTypeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CustomerTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerTypeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            CustomerType customerType = new CustomerType();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                customerType = Data.CustomerTypeDB.GetCustomerTypeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = customerType;
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
                CustomerType customerType = this.DataContext as CustomerType;
                if (customerType.IsReadOnly == true) { MessageBox.Show("امکان تغییر این رکورد نمی باشد"); return; }
                customerType.Detach();
                Save(customerType);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره نوع مشترک", ex);
            }
        }

        #endregion
    }
}
