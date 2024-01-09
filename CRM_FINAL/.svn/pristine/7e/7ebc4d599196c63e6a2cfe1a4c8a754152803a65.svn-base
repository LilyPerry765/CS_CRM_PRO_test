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
    public partial class ADSLSellerAgentUserForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int _SellerID;
        private static List<CheckableItem> _CenterIDs;

        #endregion

        #region Constructors

        public ADSLSellerAgentUserForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerAgentUserForm(int sellerID)
            : this()
        {
            _SellerID = sellerID;
        }

        public ADSLSellerAgentUserForm(int id, int sellerID)
            : this()
        {
            _ID = id;
            _SellerID = sellerID;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CenterListBox.ItemsSource = _CenterIDs = Data.CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {
            ADSLSellerAgentUserInfo item = new ADSLSellerAgentUserInfo();

            if (_ID == 0)
            {
                SellerAgentTextBox.Text = ADSLSellerGroupDB.GetADSLSellerAgentByID(_SellerID).Title;
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.ADSLSellerGroupDB.GetSellerAgentUserByID(_ID);
                SellerAgentTextBox.Text = item.SellerAgentTitle;

                CenterListBox.ItemsSource = null;

                List<UserCenter> userCenters = UserDB.GetUserCentersByUserId(_ID);
                foreach (UserCenter userCenter in userCenters)
                {
                    _CenterIDs.Where(t => (int)t.ID == userCenter.CenterID).SingleOrDefault().IsChecked = true;
                }

                CenterListBox.ItemsSource = _CenterIDs;

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                ADSLSellerAgentUserInfo sellesrUserInfo = this.DataContext as ADSLSellerAgentUserInfo;
                ADSLSellerAgentUser sellerUser = new ADSLSellerAgentUser();

                if (sellesrUserInfo != null && sellesrUserInfo.ID != 0)
                    sellerUser = ADSLSellerGroupDB.GetADSLSellerAgentUserByID(sellesrUserInfo.ID);
                else
                    sellerUser.ID = 0;

                sellerUser.SellerAgentID = _SellerID;
                sellerUser.Password = PasswordTextBox.Text;
                if (EmailTextBox.Text.Contains('@') || string.IsNullOrEmpty(EmailTextBox.Text))
                    sellerUser.Email = EmailTextBox.Text;
                else
                    throw new Exception("لطفا آدرس الکترونیکی معتبر وارد نمایید");

                sellerUser.IsEnable = (bool)IsEnableCheckBox.IsChecked;
                sellerUser.IsAdmin = (bool)IsAdminCheckBox.IsChecked;

                List<int> selectedCenterIds = CenterListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                User user = new User();
                user.FirstName = FullnameTextBox.Text;
                user.LastName = "";
                user.UserName = UsernameTextBox.Text;
                user.RoleID = DB.SearchByPropertyName<Role>("Name", "کاربر نماینده فروش").SingleOrDefault().ID;
                user.TimeStamp = "000000000000000";

                ADSLSellerGroupDB.SaveSellerAgentUser(sellerUser, user, selectedCenterIds);

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کاربر نماینده فروش" + " ، " + ex.Message + " !", ex);
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                switch (e.Key)
                {
                    case Key.A:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = true;
                        listBox.Items.Refresh();
                        break;

                    case Key.N:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = false;
                        listBox.Items.Refresh();
                        break;

                    case Key.R:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = !item.IsChecked;
                        listBox.Items.Refresh();
                        break;

                    default:
                        break;
                }
            }
        }

        private void CenterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CenterListBox.ItemsSource = _CenterIDs.Where(t => t.Name.Contains(CenterTextBox.Text)).ToList();
        }

        private void ShowADSLSellerAgentAccessButton_Click(object sender, RoutedEventArgs e)
        {
            ADSLSellerAgent aDSLSellerAgent = this.DataContext as ADSLSellerAgent;

            if (aDSLSellerAgent == null)
                return;

            ADSLSellerAgentUserAccessForm Window = new ADSLSellerAgentUserAccessForm(aDSLSellerAgent.ID);
            Window.ShowDialog();
        }

        #endregion
    }
}
