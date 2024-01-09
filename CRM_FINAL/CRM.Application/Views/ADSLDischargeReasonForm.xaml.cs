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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class ADSLDischargeReasonForm : Local.PopupWindow
    {
        #region Properties

        private  int _ID = 0;

        #endregion

        #region Constructors

        public ADSLDischargeReasonForm()
        {
            InitializeComponent();
        }

        public ADSLDischargeReasonForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            ADSLDischargeReason _ADSLDischargeReason = new ADSLDischargeReason();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                _ADSLDischargeReason = ADSLDischargeDB.GetADSLDischargeReasonByID((int)_ID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = _ADSLDischargeReason;
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
                ADSLDischargeReason _ADSLDischargeReason = this.DataContext as ADSLDischargeReason;

                _ADSLDischargeReason.Detach();
                Save(_ADSLDischargeReason);
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره دلیل تخلیه ADSL", ex);
            }
        }

        #endregion
    }
}
