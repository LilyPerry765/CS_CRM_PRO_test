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
    public partial class ADSLCustomerTypeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLCustomerTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLCustomerTypeForm(int id)
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
            ADSLCustomerType aDSLCustomerType = new ADSLCustomerType();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLCustomerType = Data.ADSLCustomerTypeDB.GetADSLCustomerTypeByID(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLCustomerType;
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
                ADSLCustomerType aDSLCustomerType = this.DataContext as ADSLCustomerType;

                aDSLCustomerType.Detach();
                Save(aDSLCustomerType);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع مشتری", ex);
            }
        }

        #endregion
    }
}
