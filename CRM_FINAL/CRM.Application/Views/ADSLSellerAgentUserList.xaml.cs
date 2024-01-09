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
    public partial class ADSLSellerAgentUserList : Local.TabWindow
    {
        #region Properties

        private int _SellerID = 0;
        public List<int> _CityIDS = new List<int>();

        #endregion

        #region Constructor

        public ADSLSellerAgentUserList()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLSellerAgentUserList(int sellerID, List<int> CityIDs)
            : this()
        {
            _SellerID = sellerID;
            _CityIDS = CityIDs;
        }
        #endregion

        #region Load Methods

        private void Initialize()
        {
            SellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckable();
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            SellerAgentComboBox.Reset();
            FullnameTextBox.Text = string.Empty;
            UsernameTextBox.Text = string.Empty;

            _SellerID = 0;

            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (_SellerID != 0)
            {
                if (SellerAgentComboBox.SelectedIDs.Count == 0)
                    ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUserbySellerAgentID(_SellerID, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
                else
                    ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUser(SellerAgentComboBox.SelectedIDs, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
            }
            else
            {
                ADSLSellerAgentUser sellerUser = ADSLSellerGroupDB.GetADSLSellerAgentUserByID(DB.CurrentUser.ID);

                if (sellerUser != null)
                {
                    if (sellerUser.IsAdmin != null)
                    {
                        if ((bool)sellerUser.IsAdmin)
                            ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUserbySellerAgentID(sellerUser.SellerAgentID, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
                        else
                            ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUserbyUserID(sellerUser.ID);
                    }
                    else
                        ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUserbyUserID(sellerUser.ID);
                }
                else
                {
                    List<int> centerIDs = DB.CurrentUser.CenterIDs;
                    List<int> cityIDs =CityDB.GetAvailableCity();

                    ItemsDataGrid.ItemsSource = ADSLSellerGroupDB.SearchSellerAgentUserbyCityIDs(cityIDs, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
                }
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLSellerAgentUserForm window = new ADSLSellerAgentUserForm(_SellerID);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgentUserInfo item = ItemsDataGrid.SelectedItem as Data.ADSLSellerAgentUserInfo;
                if (item == null) return;

                ADSLSellerAgentUserForm window = new ADSLSellerAgentUserForm(item.ID, _SellerID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ADSLSellerAgentUser item = ItemsDataGrid.SelectedItem as ADSLSellerAgentUser;

                    DB.Delete<ADSLSellerAgentUser>(item.ID);

                    ShowSuccessMessage("کاربر نماینده فروش مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کاربر نماینده فروش", ex);
            }
        }

        private void GrantAccess(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgentUserInfo item = ItemsDataGrid.SelectedItem as ADSLSellerAgentUserInfo;

                if (item == null)
                    return;

                ADSLSellerAgentUserAccessForm Window = new ADSLSellerAgentUserAccessForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ShowSoldDetail(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgentUserInfo item = ItemsDataGrid.SelectedItem as Data.ADSLSellerAgentUserInfo;
                if (item == null) return;

                ADSLSellerAgentUserReport Window = new ADSLSellerAgentUserReport(item.ID, _CityIDS);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void ShowCredit(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgentUserInfo item = ItemsDataGrid.SelectedItem as Data.ADSLSellerAgentUserInfo;
                if (item == null) return;

                ADSLSellerAgentUserCreditList Window = new ADSLSellerAgentUserCreditList(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }
        
        private void RechargeCredit(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLSellerAgentUserInfo item = ItemsDataGrid.SelectedItem as Data.ADSLSellerAgentUserInfo;
                if (item == null) return;

                ADSLSellerAgentUserRechargeList Window = new ADSLSellerAgentUserRechargeList(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        #endregion
    }
}
