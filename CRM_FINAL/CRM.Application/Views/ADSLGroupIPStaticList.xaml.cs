﻿using System;
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
    public partial class ADSLGroupIPStaticList : Local.TabWindow
    {
        #region Propperties

        #endregion

        #region Constructors

        public ADSLGroupIPStaticList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            IPTypeComboBox.ItemsSource = Data.ADSLIPDB.GetADSLIPTypeCheckable();
            CustomerGroupComboBox.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            CustomerGroupColumn.ItemsSource = Data.ADSLCustomerGroupDB.GetADSLCustomerGroupCheckable();
            IPTypeColumn.ItemsSource = Data.ADSLIPDB.GetADSLIPTypeCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLIPStatus));
            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLIPStatus));
        }

        private void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            Pager.TotalRecords = Data.ADSLIPDB.SearchADSLGroupIPCount(IPStartTextBox.Text.Trim(), VirtualRangeTextBox.Text.Trim(), BlockCountTextBox.Text.Trim(), IPTypeComboBox.SelectedIDs, CustomerGroupComboBox.SelectedIDs, telephoneNo, StatusComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = Data.ADSLIPDB.SearchADSLGroupIP(IPStartTextBox.Text.Trim(), VirtualRangeTextBox.Text.Trim(), BlockCountTextBox.Text.Trim(), IPTypeComboBox.SelectedIDs, CustomerGroupComboBox.SelectedIDs, telephoneNo, StatusComboBox.SelectedIDs, startRowIndex, pageSize);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            IPStartTextBox.Text = string.Empty;
            VirtualRangeTextBox.Text = string.Empty;
            BlockCountTextBox.Text = string.Empty;
            IPTypeComboBox.Reset();
            TelephoneNoTextBox.Text = string.Empty;
            FromDateDate.SelectedDate = null;
            ToDateDate.SelectedDate = null;
            StatusComboBox.Reset();

            Search(null, null);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLGroupIPStaticInsertForm Window = new ADSLGroupIPStaticInsertForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLGroupIP item = ItemsDataGrid.SelectedItem as ADSLGroupIP;

                if (item == null)
                    return;

                ADSLGroupIPStaticEditForm Window = new ADSLGroupIPStaticEditForm(item.ID);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}")
                return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ADSLIP item = ItemsDataGrid.SelectedItem as ADSLIP;
                    DB.Delete<ADSLIP>(item.ID);

                    ShowSuccessMessage("IP مورد نظر حذف شد");

                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف IP", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void ShowHistory(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLGroupIP item = ItemsDataGrid.SelectedItem as ADSLGroupIP;

                if (item == null)
                    return;

                ADSLIPHistoryForm Window = new ADSLIPHistoryForm(item.ID, (byte)DB.ADSLIPType.Group);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }
        }

        #endregion        
    }
}