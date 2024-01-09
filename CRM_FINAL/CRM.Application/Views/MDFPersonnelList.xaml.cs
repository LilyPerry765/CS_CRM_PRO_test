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
    public partial class MDFPersonnelList : Local.TabWindow
    {
        #region Properties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public MDFPersonnelList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();

            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
        }

        public void LoadData()
        {
            if (city == "kermanshah")
                SearchExpander.IsExpanded = false;

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
            ItemsDataGrid.ItemsSource = MDFPersonnelDB.SearchMDFPersonnels(FirstNameTextBox.Text.Trim(), LastNameTextBox.Text.Trim(), CenterComboBox.SelectedIDs);
        }
        
        private void AddItem(object sender, RoutedEventArgs e)
        {
            MDFPersonnelForm window = new MDFPersonnelForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                MDFPersonnel item = ItemsDataGrid.SelectedItem as MDFPersonnel;
                if (item == null) return;

                MDFPersonnelForm window = new MDFPersonnelForm(item.ID);
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
                    MDFPersonnel item = ItemsDataGrid.SelectedItem as MDFPersonnel;

                    DB.Delete<Data.MDFPersonnel>(item.ID);
                    ShowSuccessMessage("کارمند MDF مورد نظر حذف شد");

                    LoadData();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف کارمند MDF", ex);
            }
        }

        #endregion
    }
}
