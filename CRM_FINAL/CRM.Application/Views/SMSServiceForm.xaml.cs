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
    public partial class SMSServiceForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public SMSServiceForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SMSServiceForm(int id)
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
            Data.SMSService sms = new Data.SMSService();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                sms = Data.SMSServiceDB.GetSMSServiceByID(_ID);
                SaveButton.Content = "بروزرسانی";                
            }

            this.DataContext = sms;
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
                Data.SMSService sms = this.DataContext as Data.SMSService;

                sms.Detach();
                Save(sms);
                                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پیام", ex);
            }
        }

        #endregion
    }
}
