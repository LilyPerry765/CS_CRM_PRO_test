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
    public partial class ProvinceForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ProvinceForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ProvinceForm(int id)
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
            Province province = new Province();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                province = Data.ProvinceDB.GetProvinceByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = province;
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
                Province province = this.DataContext as Province;

                province.Detach();
                Save(province);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره استان", ex);
            }
        }

        #endregion
    }
}
