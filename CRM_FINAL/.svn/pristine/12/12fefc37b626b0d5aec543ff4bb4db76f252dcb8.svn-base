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
    public partial class PAPInfoUserList : Local.TabWindow
    {
        #region Properties

        private int _PAPID = 0;

        #endregion

        #region Constructor

        public PAPInfoUserList()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoUserList(int papId)
            : this()
        {
            _PAPID = papId;
        }
        #endregion

        #region Load Methods

        private void Initialize()
        {
            PAPInfoComboBox.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
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
            PAPInfoComboBox.Reset();
            FullnameTextBox.Text = string.Empty;
            UsernameTextBox.Text = string.Empty;

            _PAPID = 0;

            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            if (_PAPID != 0)
            {
                if (PAPInfoComboBox.SelectedIDs.Count == 0)
                    ItemsDataGrid.ItemsSource = PAPInfoUserDB.SearchPAPInfoUserbyPAPID(_PAPID, CityComboBox.SelectedIDs, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
                else
                    ItemsDataGrid.ItemsSource = PAPInfoUserDB.SearchPAPInfoUser(PAPInfoComboBox.SelectedIDs, CityComboBox.SelectedIDs, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
            }
            else
                ItemsDataGrid.ItemsSource = PAPInfoUserDB.SearchPAPInfoUser(PAPInfoComboBox.SelectedIDs, CityComboBox.SelectedIDs, FullnameTextBox.Text.Trim(), UsernameTextBox.Text.Trim());
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PAPInfoUserForm window = new PAPInfoUserForm(_PAPID);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPInfoUserInfo item = ItemsDataGrid.SelectedItem as Data.PAPInfoUserInfo;
                if (item == null) return;

                PAPInfoUserForm window = new PAPInfoUserForm(item.ID, _PAPID);
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
                    PAPInfoUser item = ItemsDataGrid.SelectedItem as PAPInfoUser;

                    DB.Delete<PAPInfoUser>(item.ID);

                    ShowSuccessMessage("کاربر شرکت PAP مورد نظر حذف شد");
                    Search(null, null);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کاربر شرکت PAP", ex);
            }
        }

        #endregion
    }
}
