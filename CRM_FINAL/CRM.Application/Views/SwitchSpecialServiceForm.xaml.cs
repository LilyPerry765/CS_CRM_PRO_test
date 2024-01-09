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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class SwitchSpecialServiceForm : Local.PopupWindow
    {
        private int _ID = 0;

        public SwitchSpecialServiceForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SwitchSpecialServiceForm(int id)
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
            SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchSpecialServicesStatus));
            SpecialServiceTypeComboBox.ItemsSource = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();
            SpecialServiceTypeComboBox.ItemsSource = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();

        }

        private void LoadData()
        {
            SwitchSpecialService item = new SwitchSpecialService();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.SwitchSpecialServiceDB.GetSwitchSpecialServiceByID(_ID);

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
                SwitchSpecialService item = this.DataContext as SwitchSpecialService;

                item.Detach();
                Save(item);

                ShowSuccessMessage("سرویس ویژه ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره سرویس ویژه", ex);
            }
        }
    }
}
