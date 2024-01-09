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
    public partial class RoleList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructors

        public RoleList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
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
            ItemsDataGrid.ItemsSource = RoleDB.SearchRoles(NameTextBox.Text);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            RoleForm window = new RoleForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RoleInfo item = ItemsDataGrid.SelectedItem as RoleInfo;
                if (item == null) return;

                bool isServiceRole = (item.IsServiceRole.HasValue) ? item.IsServiceRole.Value : false;

                RoleForm window = new RoleForm(item.ID, isServiceRole);
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
                    RoleInfo item = ItemsDataGrid.SelectedItem as RoleInfo;

                    DB.Delete<Data.Role>(item.ID);
                    ShowSuccessMessage("نقش مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف نقش", ex);
            }
        }

        private void AssignWebServiceMethodToRoleItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RoleInfo item = ItemsDataGrid.SelectedItem as RoleInfo;
                if (
                    (item == null)
                    ||
                    (item.IsServiceRole.HasValue && !item.IsServiceRole.Value) //چنانچه نقش انخاب شده برای وب سرویس ایجاد نشده باشد آنگاه شرط روبرو برقرار است
                   )
                {
                    return;
                }

                RoleForm window = new RoleForm(item.ID, true);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        #endregion

    }
}
