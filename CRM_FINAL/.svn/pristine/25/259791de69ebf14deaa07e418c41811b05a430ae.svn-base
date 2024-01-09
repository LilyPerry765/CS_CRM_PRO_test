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
    public partial class UserList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public UserList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RoleComboBox.ItemsSource = RoleDB.GetRolesCheckable();
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
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = UserDB.SearchUsers(UserNameTextBox.Text, FullNameTextBox.Text, RoleComboBox.SelectedIDs);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            UserForm window = new UserForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                UserInfo item = ItemsDataGrid.SelectedItem as UserInfo;
                if (item == null) return;

                UserForm window = new UserForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
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
                    User item = UserDB.GetUserByID((ItemsDataGrid.SelectedItem as UserInfo).ID);
                    item.IsDelete = true;
                    item.Detach();
                    DB.Save(item);
                    LoadData();

                    ShowSuccessMessage("کاربر مورد نظر حذف شد");
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کاربر", ex);
            }
        }

        #endregion
    }
}
