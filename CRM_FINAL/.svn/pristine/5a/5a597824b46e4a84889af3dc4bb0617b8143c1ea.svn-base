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
using Enterprise;
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for RequestReasonRejectList.xaml
    /// </summary>
    public partial class RequestReasonRejectList :Local.TabWindow
    {
        public RequestReasonRejectList()
        {
            InitializeComponent();
            Initialize();
        }
        #region Methods

        private void Initialize()
        {
            RequestStepComboBox.ItemsSource = RequestStepDB.GetRequestStepCheckable();
            RequestStepColumn.ItemsSource = RequestStepDB.GetRequestStepCheckable();
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

            ItemsDataGrid.ItemsSource = Data.RequestRejectReasonDB.SearchRequesrRejectReason(DescriptionTextBox.Text,
                                                                                              RequestStepComboBox.SelectedIDs,
                                                                                              IsActiveCheckBox.IsChecked);

        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            RequestRejectReasonForm Window = new RequestRejectReasonForm();
            Window.ShowDialog();

            if (Window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RequestRejectReason item = ItemsDataGrid.SelectedItem as RequestRejectReason;

                if (item == null)
                    return;

                RequestRejectReasonForm Window = new RequestRejectReasonForm(item.ID);
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
                    RequestRejectReason item = ItemsDataGrid.SelectedItem as RequestRejectReason;
                    DB.Delete<RequestRejectReason>(item.ID);

                    ShowSuccessMessage("دلیل مورد نظر حذف شد");

                    Search(null, null);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                ShowErrorMessage("مقادیر این رکورد در پایگاه داده با جداول دیگری در ارتباط است و قابل حذف نمی باشد", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف شهر", ex);
            }
        }

        #endregion
    }
}
