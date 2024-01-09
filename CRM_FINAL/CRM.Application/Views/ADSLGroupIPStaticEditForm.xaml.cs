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
    public partial class ADSLGroupIPStaticEditForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;

        #endregion

        #region Constructors

        public ADSLGroupIPStaticEditForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLGroupIPStaticEditForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            IPGroupComboBox.ItemsSource = DB.GetAllEntity<ADSLIPType>();
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLIPStatus));
        }

        private void LoadData()
        {
            ADSLGroupIP aDSLGroupIP = new ADSLGroupIP();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                aDSLGroupIP = Data.ADSLIPDB.GetADSLGroupIPById(_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = aDSLGroupIP;
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
                ADSLGroupIP aDSLGroupIP = this.DataContext as ADSLGroupIP;
                
                aDSLGroupIP.Detach();
                Save(aDSLGroupIP);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت ، " + ex.Message, ex);
            }
        }

        private void PortNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c) || char.Equals(c, '.'))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }


        #endregion
    }
}
