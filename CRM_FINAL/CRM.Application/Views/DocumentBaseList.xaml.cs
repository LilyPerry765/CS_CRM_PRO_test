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
    public partial class DocumentBaseList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public DocumentBaseList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            DocumentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DocumentType));
            DocumentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DocumentType));
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
            ItemsDataGrid.ItemsSource = Data.DocumentTypeDB.SearchDocumentType(DocumentTypeComboBox.SelectedIDs, DocumentNameTextBox.Text, ExistOnceCheckBox.IsChecked);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            DocumentBaseForm window = new DocumentBaseForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Data.DocumentType item = ItemsDataGrid.SelectedItem as Data.DocumentType;
                if (item == null) return;

                DocumentBaseForm window = new DocumentBaseForm(item.ID);
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
                    Data.DocumentType item = ItemsDataGrid.SelectedItem as Data.DocumentType;

                    DB.Delete<Data.DocumentType>(item.ID);

                    ShowSuccessMessage("مدرک مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مدرک", ex);
            }
        }

        #endregion
    }
}
