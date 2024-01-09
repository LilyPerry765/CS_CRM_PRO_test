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
    /// <summary>
    /// Interaction logic for ExchangePostList.xaml
    /// </summary>
    public partial class ExchangePostList : Local.TabWindow 
    {
        public ExchangePostList()
        {
            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();

        }
        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Search(null, null);
        }
    

        private void NewClick(object sender, RoutedEventArgs e)
        {
            ExchangePostForm window = new ExchangePostForm((int)Data.DB.RequestType.ExchangePost);
            window.ShowDialog();
            if (window.DialogResult == true)
                Search(null, null);
        }
        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.ExchangePostDB.ExchangePostInfo item = ItemsDataGrid.SelectedItem as CRM.Data.ExchangePostDB.ExchangePostInfo;
                if (item == null) return;

                ExchangePostForm window = new ExchangePostForm(item.ID);
                window.ShowDialog();

                if(window.DialogResult==true)
                Search(null, null);
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int fromCabinetInputID = -1;
            if (!string.IsNullOrWhiteSpace(FromCabinetInputIDTextBox.Text)) fromCabinetInputID = Convert.ToInt32(FromCabinetInputIDTextBox.Text);

            int FromOldConnectionNo = -1;
            if (!string.IsNullOrWhiteSpace(FromOldConnectionNoTextBox.Text)) FromOldConnectionNo = Convert.ToInt32(FromOldConnectionNoTextBox.Text);

            long toOldConnectionNo = -1;
            if (!string.IsNullOrWhiteSpace(ToOldConnectionNoTextBox.Text)) toOldConnectionNo = Convert.ToInt64(ToOldConnectionNoTextBox.Text);

            long AccomplishmentTime= -1;
            if (!string.IsNullOrWhiteSpace(AccomplishmentTimeTextBox.Text)) AccomplishmentTime= Convert.ToInt64(AccomplishmentTimeTextBox.Text);

            Pager.TotalRecords = Data.ExchangePostDB.SearchExchangePostCount(
                                                                                CityComboBox.SelectedIDs,
                                                                                CenterComboBox.SelectedIDs,
                                                                                OldCabinetComboBox.SelectedIDs,
                                                                                NewCabinetComboBox.SelectedIDs,
                                                                                OldPostComboBox.SelectedIDs,
                                                                                NewPostComboBox.SelectedIDs,
                                                                                AccomplishmentDateDate.SelectedDate,
                                                                                fromCabinetInputID,
                                                                                FromOldConnectionNo,
                                                                                toOldConnectionNo,
                                                                                AccomplishmentTime,
                                                                                RequestLetterNoTextBox.Text

                                                                                );

            ItemsDataGrid.ItemsSource = Data.ExchangePostDB.SearchExchangePost(
            CityComboBox.SelectedIDs,
            CenterComboBox.SelectedIDs,
            OldCabinetComboBox.SelectedIDs,
            NewCabinetComboBox.SelectedIDs,
            OldPostComboBox.SelectedIDs,
            NewPostComboBox.SelectedIDs,
            AccomplishmentDateDate.SelectedDate,
            fromCabinetInputID,
            FromOldConnectionNo,
            toOldConnectionNo,
            AccomplishmentTime,
            RequestLetterNoTextBox.Text,
            startRowIndex,
            pageSize

            );
 
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }



        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            //if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            //try
            //{
            //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //    if (result == MessageBoxResult.Yes)
            //    {
            //        CRM.Data.ExchangePostDB.ExchangePostInfo item = ItemsDataGrid.SelectedItem as CRM.Data.ExchangePostDB.ExchangePostInfo;
            //        DB.Delete<ExchangePost>(item.ID);
            //        Search(null, null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorMessage("خطا در حذف شهر", ex);
            //}
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                CRM.Data.ExchangePostDB.ExchangePostInfo ExchangePostInfo = ItemsDataGrid.SelectedItem as CRM.Data.ExchangePostDB.ExchangePostInfo;
                if (ExchangePostInfo == null) return;

                try
                {
                    MessageBoxResult result = MessageBox.Show("آیا از ارجاع مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {

                        Request request = Data.RequestDB.GetRequestByID(ExchangePostInfo.RequestID);
                        Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                        Search(sender, e);
                        ShowSuccessMessage("ارجاع انجام شد");
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ارجاع", ex);
                }
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void CenterCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            NewCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
            OldCabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void OldCabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            OldPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID(OldCabinetComboBox.SelectedIDs);
        }

        private void NewCabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            NewPostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID(NewCabinetComboBox.SelectedIDs);
        }

    }
}
