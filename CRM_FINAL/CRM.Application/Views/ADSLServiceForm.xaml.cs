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
    public partial class ADSLServiceForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLServiceForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLServiceForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CustomerGroupComboBox.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
        }

        private void LoadData()
        {
            ADSLServiceGroup aDSLServiceGroup = new ADSLServiceGroup();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                aDSLServiceGroup = ADSLServiceGroupDB.GetADSLServiceGroupById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLServiceGroup;
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
                ADSLServiceGroup ADSLService = this.DataContext as ADSLServiceGroup;

                ADSLService.Detach();
                Save(ADSLService);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره گروه سرویس ADSL", ex);
            }
        }

        #endregion
    }
}
