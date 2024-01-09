﻿using System;
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
    public partial class CustomerGroupForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CustomerGroupForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CustomerGroupForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CustomerTypeComboBox.ItemsSource = DB.GetAllEntity<CustomerType>();
        }
        
        private void LoadData()
        {
            CustomerGroup customerGroup = new CustomerGroup();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                customerGroup = Data.CustomerGroupDB.GetCustomerGroupById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = customerGroup;
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
                CustomerGroup customerGroup = this.DataContext as CustomerGroup;
                customerGroup.Detach();
                DB.Save(customerGroup);

                ShowSuccessMessage("گروه مشترکین ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه مشترکین", ex);
            }
        }

        #endregion
    }
}
