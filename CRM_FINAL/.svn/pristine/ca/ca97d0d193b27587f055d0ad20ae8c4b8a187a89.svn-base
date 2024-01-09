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
    public partial class PAPInfoUserForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int _PAPInfoID;

        #endregion

        #region Constructors

        public PAPInfoUserForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoUserForm(int papInfoID)
            : this()
        {
            _PAPInfoID = papInfoID;
        }

        public PAPInfoUserForm(int id, int papInfoID)
            : this()
        {
            _ID = id;
            _PAPInfoID = papInfoID;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
        }

        private void LoadData()
        {
            PAPInfoUserInfo item = new PAPInfoUserInfo();

            if (_ID == 0)
            {
                PAPInfoTextBox.Text = PAPInfoDB.GetPAPInfoByID(_PAPInfoID).Title;
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.PAPInfoUserDB.GetPAPInfoUserByID(_ID);
                PAPInfoTextBox.Text = item.PAPInfo;

                this.DataContext = item;
                SaveButton.Content = "بروزرسانی";
            }
        }

        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                PAPInfoUserInfo papUserInfo = this.DataContext as PAPInfoUserInfo;
                PAPInfoUser papUser = new PAPInfoUser();

                if (papUserInfo != null)
                    papUser.ID = papUserInfo.ID;
                else
                    papUser.ID = 0;

                papUser.PAPInfoID = _PAPInfoID;
                papUser.CityID = (int)CityComboBox.SelectedValue;
                papUser.Password = PasswordTextBox.Text;
                if (EmailTextBox.Text.Contains('@') || string.IsNullOrEmpty(EmailTextBox.Text))
                    papUser.Email = EmailTextBox.Text;
                else
                    throw new Exception("لطفا آدرس الکترونیکی معتبر وارد نمایید");

                if (!string.IsNullOrEmpty(InstallRequestNoTextBox.Text))
                    papUser.InstallRequestNo = Convert.ToInt32(InstallRequestNoTextBox.Text);
                else
                    papUser.InstallRequestNo = 20;
                if (!string.IsNullOrEmpty(DischargeRequestNoTextBox.Text))
                    papUser.DischargeRequestNo = Convert.ToInt32(DischargeRequestNoTextBox.Text);
                else
                    papUser.DischargeRequestNo = 20;
                if (!string.IsNullOrEmpty(ExchangeRequestNoTextBox.Text))
                    papUser.ExchangeRequestNo = Convert.ToInt32(ExchangeRequestNoTextBox.Text);
                else
                    papUser.ExchangeRequestNo = 20;
                if (!string.IsNullOrEmpty(FeasibilityNoTextBox.Text))
                    papUser.FeasibilityNo = Convert.ToInt32(FeasibilityNoTextBox.Text);
                else
                    papUser.FeasibilityNo = 20;

                papUser.IsEnable = (bool)IsEnableCheckBox.IsChecked;

                User user = new User();
                user.FirstName = FullnameTextBox.Text;
                user.LastName = "";
                user.UserName = UsernameTextBox.Text;
                user.RoleID = DB.SearchByPropertyName<Role>("Name", "کاربر شرکت PAP").SingleOrDefault().ID;
                user.TimeStamp = "000000000000000";

                PAPInfoUserDB.SavePAPInfoUser(papUser, user);


                ShowSuccessMessage("کاربر شرکت PAP ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کاربر شرکت PAP" + " ، " + ex.Message + " !", ex);
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
