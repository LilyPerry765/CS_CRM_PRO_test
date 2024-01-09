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
    public partial class Failure117RowList : Local.TabWindow
    {
        #region Propperties

        private string city = string.Empty;

        #endregion

        #region Constructors

        public Failure117RowList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }

        private void LoadData()
        {
            if (city == "kermanshah")
                SearchExpender.IsExpanded = false;

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
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            int fromRowNo = -1;
            if (!string.IsNullOrWhiteSpace(FromRowNoTextBox.Text))
                fromRowNo = Convert.ToInt32(FromRowNoTextBox.Text);

            int toRowNo = -1;
            if (!string.IsNullOrWhiteSpace(ToRowNoTextBox.Text))
                toRowNo = Convert.ToInt32(ToRowNoTextBox.Text);

            //long requestID = -1;
            //if (!string.IsNullOrWhiteSpace(RequestIDTextBox.Text))
            //    requestID = Convert.ToInt64(RequestIDTextBox.Text);

            //long telephoneNo = -1;
            //if (!string.IsNullOrWhiteSpace(telephoneNoTextBox.Text))
            //    telephoneNo = Convert.ToInt64(telephoneNoTextBox.Text);

            if (DB.City == "semnan")
            {
                Pager.TotalRecords = Data.Failure117DB.SearchFailureFormInfoCount(fromRowNo, toRowNo, RequestIDTextBox.Text, telephoneNoTextBox.Text, FromDate.SelectedDate, ToDate.SelectedDate, null);
                ItemsDataGrid.ItemsSource = Data.Failure117DB.SearchFailureFormInfo(fromRowNo, toRowNo, RequestIDTextBox.Text, telephoneNoTextBox.Text, FromDate.SelectedDate, ToDate.SelectedDate, null, startRowIndex, pageSize);
            }

            if (DB.City == "kermanshah")
            {
                Pager.TotalRecords = Data.Failure117DB.SearchFailureFormInfoCount(fromRowNo, toRowNo, RequestIDTextBox.Text, telephoneNoTextBox.Text, FromDate.SelectedDate, ToDate.SelectedDate, null);
                ItemsDataGrid.ItemsSource = Data.Failure117DB.SearchFailureFormInfo(fromRowNo, toRowNo, RequestIDTextBox.Text, telephoneNoTextBox.Text, FromDate.SelectedDate, ToDate.SelectedDate, null, startRowIndex, pageSize);
            }
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            FromRowNoTextBox.Text = string.Empty;
            ToRowNoTextBox.Text = string.Empty;
            RequestIDTextBox.Text = string.Empty;
            telephoneNoTextBox.Text = string.Empty;
            FromDate.SelectedDate = null;
            ToDate.SelectedDate = null;

            Search(null, null);
        }

        private void ShowForm(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                FailureFormRowInfo item = ItemsDataGrid.SelectedItem as FailureFormRowInfo;

                if (item == null)
                    return;

                Failure117NetworkForm Window = new Failure117NetworkForm(item.RequestID, true);
                Window.ShowDialog();

                if (Window.DialogResult == true)
                    Search(null, null);
            }

        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        private void ImageView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            long requestID = (long)(sender as Image).Tag;

            try
            {
                Request currentRequest = Data.RequestDB.GetRequestByID(requestID);

                Failure117FullView failure117FullView = new Failure117FullView(requestID);
                failure117FullView.ShowDialog();

            }
            catch (Exception ex)
            {
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر با خطا رخ داده است ! ", ex);
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        #endregion
    }
}
