using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Collections.ObjectModel;

namespace CRM.Application.Views
{
    public partial class IssueWiringList : Local.TabWindow
    {
        #region Constructor & Fields
        ObservableCollection<CheckableObject> issueWiringList;
        bool mode = false;
        public IssueWiringList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor & Fields

        #region Load Methods
        private void Initialize()
        {
            WiringTypeColumn.ItemsSource = Helper.GetEnumItem (typeof(DB.WiringType));
            WiringTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WiringType));
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.StatusIssueWiring));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.StatusIssueWiring));
            issueWiringList = new ObservableCollection<CheckableObject>();
        }

        public void LoadData()
        {
            Search(null, null);
        }
        #endregion Load Methods

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
            int printCount = -1;
            if (!string.IsNullOrWhiteSpace(PrintCountTextBox.Text)) printCount = Convert.ToByte(PrintCountTextBox.Text);

            issueWiringList = Data.IssueWiringDB.SearchIssueWiring(WiringTypeComboBox.SelectedIDs, printCount, WiringIssueDateDate.SelectedDate, IsPrintedCheckBox.IsChecked, WiringNoTextBox.Text.Trim(), CommentStatusTextBox.Text.Trim(), StatusComboBox.SelectedIDs);
           ItemsDataGrid.ItemsSource = issueWiringList;
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //    if (result == MessageBoxResult.Yes)
            //    {
            //        IssueWiring item = ItemsDataGrid.SelectedItem as IssueWiring;

            //        DB.Delete<Data.IssueWiring>(item.ID);
            //        ShowSuccessMessage("IssueWiring مورد نظر حذف شد");
            //        LoadData();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف IssueWiring", ex);
            //}
         

        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex >= 0)
            //{
            //    IssueWiring item = ItemsDataGrid.SelectedItem as Data.IssueWiring;
            //    if (item == null) return;

            //    IssueWiringForm window = new IssueWiringForm(item.ID);
            //    window.ShowDialog();

            //    if (window.DialogResult == true)
            //        LoadData();
            //}
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //try
            //{
            //    Data.IssueWiring item = e.Row.Item as Data.IssueWiring;

            //    item.Detach();
            //    DB.Save(item);

            //    Search(null, null);

            //    ShowSuccessMessage("IssueWiring مورد نظر ذخیره شد");
            //}
            //catch (Exception ex)
            //{
            //    e.Cancel = true;
            //    ShowErrorMessage("خطا در ذخیره IssueWiring", ex);
            //}
        }
        private void SelectAll(object sender, RoutedEventArgs e)
        {
            if (mode == false)
                foreach (CheckableObject obj in issueWiringList)
                    obj.IsChecked = true;

            else if (mode == true)
                foreach (CheckableObject obj in issueWiringList)
                    obj.IsChecked = false;

            ItemsDataGrid.Items.Refresh();
            mode = !mode;
        }
        #endregion Event Handlers

       
    }
}
