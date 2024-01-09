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
    public partial class AnnounceList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public AnnounceList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            //StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Status));
            //StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.Status));
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
            ItemsDataGrid.ItemsSource = AnnounceDB.SearchAnnounce(AnnounceTitleTextBox.Text, IssueNumberTextBox.Text, FromIssueDate.SelectedDate, ToIssueDate.SelectedDate, FromStartDate.SelectedDate, ToStartDate.SelectedDate, FromEndDate.SelectedDate, ToEndDatee.SelectedDate, StatusComboBox.SelectedIDs);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            AnnounceForm window = new AnnounceForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Announce item = ItemsDataGrid.SelectedItem as Announce;
                if (item == null) return;

                AnnounceForm window = new AnnounceForm(item.ID);
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
                    Announce item = ItemsDataGrid.SelectedItem as Announce;

                    DB.Delete<Announce>(item.ID);

                    ShowSuccessMessage("آیین نامه مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف آیین نامه", ex);
            }
        }

        #endregion
    }
}
