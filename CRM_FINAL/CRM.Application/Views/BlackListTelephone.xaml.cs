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
    public partial class BlackListTelephone : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public BlackListTelephone()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Load Methods

        private void Initialize()
        {
            SwitchPrecodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckable();
            SwitchPortComboBox.ItemsSource = Data.SwitchPortDB.GetSwitchPortCheckable();
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));
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
            long telephoneNo = -1;
            if (!string.IsNullOrWhiteSpace(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            ItemsDataGrid.ItemsSource = Data.BlackListDB.SearchBlackListTelephone(telephoneNo, StatusComboBox.SelectedIDs, SwitchPrecodeComboBox.SelectedIDs, SwitchPortComboBox.SelectedIDs, CenterComboBox.SelectedIDs, IsVIPCheckBox.IsChecked, IsRoundCheckBox.IsChecked, (byte)DB.BlackListType.TelephoneNo);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            BlackListForm window = new BlackListForm((byte)DB.BlackListType.TelephoneNo);
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                BlackListTelephoneInfo item = ItemsDataGrid.SelectedItem as BlackListTelephoneInfo;
                if (item == null) return;

                BlackListForm window = new BlackListForm((byte)DB.BlackListType.TelephoneNo, item.ID);
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
                    CRM.Data.Telephone item = ItemsDataGrid.SelectedItem as CRM.Data.Telephone;

                    DB.Delete<Data.Telephone>(item.TelephoneNo);
                    ShowSuccessMessage("تلفن مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف تلفن", ex);
            }
        }

        #endregion
    }
}
