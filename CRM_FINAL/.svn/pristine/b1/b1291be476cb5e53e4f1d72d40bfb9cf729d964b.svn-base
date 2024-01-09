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
    public partial class ADSLSellerAgentAccessForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLSellerAgentAccessForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerAgentAccessForm(int id)
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
            ADSLSellerAgent aDSLSellerAgent = new ADSLSellerAgent();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                aDSLSellerAgent = Data.ADSLSellerGroupDB.GetADSLSellerAgentByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = aDSLSellerAgent;
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
                ADSLSellerAgent aDSLSellerAgent = this.DataContext as ADSLSellerAgent;

                if (string.IsNullOrWhiteSpace(CreditAddTextBox.Text.Trim()))
                    throw new Exception("لطفا مبلغ افزایش را وارد نمایید");

                if (Convert.ToInt64(CreditAddTextBox.Text.Trim()) + aDSLSellerAgent.CreditCashRemain > aDSLSellerAgent.CreditCash)
                    throw new Exception("اعتبار باقی مانده باید کوچکتر یا مساوی اعتبار کل باشد");

                if (aDSLSellerAgent.CreditCash == null)
                    aDSLSellerAgent.CreditCash = 0;

                if (aDSLSellerAgent.CreditCashUse == null)
                    aDSLSellerAgent.CreditCashUse = 0;

                if (aDSLSellerAgent.CreditCashRemain == null)
                    aDSLSellerAgent.CreditCashRemain = 0;

                aDSLSellerAgent.CreditCashRemain = aDSLSellerAgent.CreditCashRemain + Convert.ToInt64(CreditAddTextBox.Text.Trim());

                aDSLSellerAgent.Detach();
                Save(aDSLSellerAgent);

                ADSLSellerAgentRecharge recharge = new ADSLSellerAgentRecharge();

                recharge.SellerAgentID = aDSLSellerAgent.ID;
                recharge.Cost = Convert.ToInt64(CreditAddTextBox.Text.Trim());
                recharge.UserID = DB.CurrentUser.ID;

                recharge.Detach();
                DB.Save(recharge);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره امکانات نماینده فروش، " + ex.Message + " !", ex);
            }
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }

        #endregion
    }
}
