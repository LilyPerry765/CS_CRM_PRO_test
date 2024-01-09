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
    public partial class ADSLCustomerGroupForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLCustomerGroupForm()
        {
            InitializeComponent();
        }

        public ADSLCustomerGroupForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            ADSLCustomerGroup aDSLServiceCustomerGroup = new ADSLCustomerGroup();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                aDSLServiceCustomerGroup = ADSLCustomerGroupDB.GetADSLCustomerGroupById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLServiceCustomerGroup;
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
                ADSLCustomerGroup ADSLCustomerGroup = this.DataContext as ADSLCustomerGroup;

                ADSLCustomerGroup.Detach();
                Save(ADSLCustomerGroup);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه مشتری ADSL", ex);
            }
        }

        #endregion
    }
}
