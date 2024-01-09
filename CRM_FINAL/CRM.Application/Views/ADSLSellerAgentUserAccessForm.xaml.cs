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
using System.Text.RegularExpressions;

namespace CRM.Application.Views
{
    public partial class ADSLSellerAgentUserAccessForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public ADSLSellerAgentUserAccessForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerAgentUserAccessForm(int id)
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
            ADSLSellerAgentUser aDSLSellerAgentUser = new ADSLSellerAgentUser();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                this.DataContext = aDSLSellerAgentUser;
            }
            else
            {
                aDSLSellerAgentUser = Data.ADSLSellerGroupDB.GetADSLSellerAgentUserByID(_ID);
                aDSLSellerAgent = ADSLSellerGroupDB.GetADSLSellerAgentByID(aDSLSellerAgentUser.SellerAgentID);
                User user = UserDB.GetUserByID(aDSLSellerAgentUser.ID);

                SaveButton.Content = "بروز رسانی";

                this.DataContext = aDSLSellerAgentUser;

                TitleTextbox.Text = aDSLSellerAgent.Title;
                UserNameTextbox.Text = user.FirstName + " " + user.LastName;
            }
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
                ADSLSellerAgentUser aDSLSellerAgentUser = this.DataContext as ADSLSellerAgentUser;
                ADSLSellerAgent agent = ADSLSellerGroupDB.GetADSLSellerAgentByID(aDSLSellerAgentUser.SellerAgentID);
                List<ADSLSellerAgentUser> userList = ADSLSellerGroupDB.GetADSLSellerAgentUsersbyAgentID(aDSLSellerAgentUser.SellerAgentID);
                long sumCreditRemain = 0;

                if (string.IsNullOrWhiteSpace(CreditAddTextBox.Text.Trim()))
                    throw new Exception("لطفا مبلغ افزایش را وارد نمایید");

                if (Convert.ToInt64(CreditAddTextBox.Text.Trim()) + aDSLSellerAgentUser.CreditCashRemain > aDSLSellerAgentUser.CreditCash)
                    throw new Exception("اعتبار باقی مانده باید کوچکتر یا مساوی اعتبار کل باشد");

                foreach (ADSLSellerAgentUser currentUser in userList)
                {
                    if (currentUser.CreditCashRemain != null)
                        sumCreditRemain = sumCreditRemain + (long)currentUser.CreditCashRemain;
                }               

                if (Convert.ToInt64(CreditAddTextBox.Text.Trim()) + sumCreditRemain > agent.CreditCashRemain)
                    throw new Exception("مجموع اعتبار باقیمانده کاربران بیش از اعتبار باقیمانده نمایندگی می باشد");

                if (aDSLSellerAgentUser.CreditCash == null)
                    aDSLSellerAgentUser.CreditCash = 0;

                if (aDSLSellerAgentUser.CreditCashUse == null)
                    aDSLSellerAgentUser.CreditCashUse = 0;

                if (aDSLSellerAgentUser.CreditCashRemain == null)
                    aDSLSellerAgentUser.CreditCashRemain = 0;

                aDSLSellerAgentUser.CreditCashRemain = aDSLSellerAgentUser.CreditCashRemain + Convert.ToInt64(CreditAddTextBox.Text.Trim());

                aDSLSellerAgentUser.Detach();
                Save(aDSLSellerAgentUser);

                ADSLSellerAgentUserRecharge recharge = new ADSLSellerAgentUserRecharge();

                recharge.SellerAgentUserID = aDSLSellerAgentUser.ID;
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
