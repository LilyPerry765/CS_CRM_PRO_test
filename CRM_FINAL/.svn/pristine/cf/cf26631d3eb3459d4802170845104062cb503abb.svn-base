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
using Microsoft.Win32;
using System.ComponentModel;

namespace CRM.Application.Views
{
    public partial class CenterForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public CenterForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CenterForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.RegionDB.GetRegionsCheckable();
            SubsidiaryCodeTelephoneComboBox.ItemsSource = SubsidiaryCodeDB.GetSubsidiaryCodesTelephoneCheckable();
            SubsidiaryCodeServiceComboBox.ItemsSource = SubsidiaryCodeDB.GetSubsidiaryCodesServiceCheckable();
            SubsidiaryCodeADSLComboBox.ItemsSource = SubsidiaryCodeDB.GetSubsidiaryCodesADSLCheckable();
        }

        private void LoadData()
        {
            Center center = new Center();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                center = Data.CenterDB.GetCenterById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = center;
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
                Center center = this.DataContext as Center;
                center.Detach();
                Save(center);

                ShowSuccessMessage("ذخیره مرکز انجام شد");

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره مرکز ", ex);
            }
        }

        #endregion
    }
}
