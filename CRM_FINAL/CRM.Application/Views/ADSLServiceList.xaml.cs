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
    public partial class ADSLServiceList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public ADSLServiceList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CustomerGroupComboBox.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            CustomerGroupColumn.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
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
            TitleTextBox.Text = string.Empty;
            CustomerGroupComboBox.Reset();

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = ADSLServiceGroupDB.SearchADSLServiceGroups(TitleTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLServiceForm window = new ADSLServiceForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLServiceGroup item = ItemsDataGrid.SelectedItem as ADSLServiceGroup;
                if (item == null) return;

                ADSLServiceForm window = new ADSLServiceForm(item.ID);
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
                    ADSLServiceGroup item = ItemsDataGrid.SelectedItem as ADSLServiceGroup;

                    DB.Delete<ADSLServiceGroup>(item.ID);
                    ShowSuccessMessage("گروه سرویس مورد نظر حذف شد");
                    LoadData();
                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد !", ex);
            }

            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف گروه سرویس", ex);
            }
        }

        private void GetCenterAvalibility(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLServiceGroup serviceGroup = ItemsDataGrid.SelectedItem as ADSLServiceGroup;

                if (serviceGroup == null)
                    return;

                ADSLServiceCenterForm window = new ADSLServiceCenterForm(serviceGroup.ID, (byte)DB.ADSLServiceCenterMode.ServiceGroup);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void GetADSLSellerAvalibility(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLServiceGroup serviceGroup = ItemsDataGrid.SelectedItem as ADSLServiceGroup;

                if (serviceGroup == null)
                    return;

                ADSLServiceSellerForm window = new ADSLServiceSellerForm(serviceGroup.ID, (byte)DB.ADSLServiceCenterMode.ServiceGroup);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        #endregion
    }
}
