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
    public partial class SpecialServiceTypeForm : Local.PopupWindow
    {
        #region Propertis

        private int _ID = 0;

        #endregion

        #region Constructors

        public SpecialServiceTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SpecialServiceTypeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PaymentType));
        }

        private void LoadData()
        {
            SpecialServiceType item = new SpecialServiceType();



            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                item.Cost = 0;
            }
            else
            {
                item = Data.SpecialServiceTypeDB.GetSpecialServiceTypeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                SpecialServiceType item = this.DataContext as SpecialServiceType;

                if (item.Cost != 0 && item.PaymentType == null)
                    throw new Exception("لطفا نحوه پرداخت هزینه را مشخص کنید");

                if (item.Cost == 0)
                    item.PaymentType = null;

                
                item.Detach();
                Save(item);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع سرویس ویژه", ex);
            }
        }

        #endregion
    }
}
