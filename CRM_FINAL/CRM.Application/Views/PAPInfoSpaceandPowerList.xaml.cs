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
    public partial class PAPInfoSpaceandPowerList : Local.TabWindow
    {
        #region Properties

        private int _PAPID = 0;

        #endregion

        #region Constructor

        public PAPInfoSpaceandPowerList()
        {
            InitializeComponent();
            Initialize();
        }

        public PAPInfoSpaceandPowerList(int papId)
            : this()
        {
            _PAPID = papId;
        }
        #endregion

        #region Methods

        private void Initialize()
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();
            PAPInfoColumn.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
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
            CenterComboBox.Reset();

            LoadData();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = PAPInfoDB.SearchPAPInfoSpace(_PAPID, CenterComboBox.SelectedIDs);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            PAPInfoSpaceandPowerForm window = new PAPInfoSpaceandPowerForm(_PAPID);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                PAPInfoSpaceandPower item = ItemsDataGrid.SelectedItem as Data.PAPInfoSpaceandPower;
                if (item == null) return;

                PAPInfoSpaceandPowerForm window = new PAPInfoSpaceandPowerForm(item.ID, _PAPID, item.CenterID);
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
                    PAPInfoSpaceandPower item = ItemsDataGrid.SelectedItem as PAPInfoSpaceandPower;

                    DB.Delete<PAPInfoSpaceandPower>(item.ID);

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
