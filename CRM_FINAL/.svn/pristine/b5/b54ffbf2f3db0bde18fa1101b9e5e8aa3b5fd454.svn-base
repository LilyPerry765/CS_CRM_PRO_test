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
    public partial class DocumentForRequestList : Local.TabWindow
    {
        #region Properties

        #endregion

        #region Constructor

        public DocumentForRequestList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            DocumentTypeComboBox.ItemsSource = Data.DocumentTypeDB.GetDocumentTypeCheckable();
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            AnnounceComboBox.ItemsSource = Data.AnnounceDB.GetAnnounceCheckable();
            NeedForCustomerTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
            ChargingTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            TelephoneTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneType));
            TelephonePosessionTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PossessionType));
            OrderTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.OrderType));
            //StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DocumentStatus));

            DocumentTypeColumn.ItemsSource = Data.DocumentTypeDB.GetDocumentTypeCheckable();
            RequestTypeColumn.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();
            AnnounceColumn.ItemsSource = Data.AnnounceDB.GetAnnounceCheckable();
            NeedForCustomerTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
            ChargingTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChargingGroup));
            TelephoneTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneType));
            TelephonePosessionTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PossessionType));
            OrderTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.OrderType));
            //StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DocumentStatus));
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
            ItemsDataGrid.ItemsSource = Data.DocumentRequestTypeDB.SearchDocumentRequestType(DocumentTypeComboBox.SelectedIDs, RequestTypeComboBox.SelectedIDs, AnnounceComboBox.SelectedIDs, NeedForCustomerTypeComboBox.SelectedIDs, ChargingTypeComboBox.SelectedIDs, TelephoneTypeComboBox.SelectedIDs, TelephonePosessionTypeComboBox.SelectedIDs, OrderTypeComboBox.SelectedIDs, StatusComboBox.SelectedIDs);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            DocumentForRequestForm window = new DocumentForRequestForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                Data.DocumentRequestType item = ItemsDataGrid.SelectedItem as Data.DocumentRequestType;
                if (item == null) 
                    return;

                DocumentForRequestForm window = new DocumentForRequestForm(item.ID , item.AnnounceID ?? 0);
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
                    Data.DocumentRequestType item = ItemsDataGrid.SelectedItem as Data.DocumentRequestType;

                    DB.Delete<Data.DocumentRequestType>(item.ID);

                    ShowSuccessMessage("مدرک مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("مدرک در حذف فیش", ex);
            }
        }

        #endregion
    }
}
