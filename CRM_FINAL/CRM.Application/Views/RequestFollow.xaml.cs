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
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class RequestFollow : Local.TabWindow
    {


        #region Properties

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        #endregion

        #region Constructors

        public RequestFollow()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypesComboBox.ItemsSource = RequestTypeDB.GetRequestTypeCheckable();
        }

        public void LoadData()
        {

        }

        public override void Load()
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
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long requestID = 0;
            long telephoneNo = 0;

            List<RequestFollowInfo> requestList = new List<RequestFollowInfo>();

            if (!string.IsNullOrEmpty(RequestIDTextBox.Text))
                requestID = Convert.ToInt64(RequestIDTextBox.Text);
            
            if (!string.IsNullOrEmpty(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            int count = default(int);
            ItemsDataGrid.ItemsSource = Data.RequestDB.SearchFollowRequest(requestID, telephoneNo, NationalCodeTextBox.Text.Trim(), RequestTypesComboBox.SelectedIDs, CustomerNameTextBox.Text.Trim(), CustomerLastNameTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(), BirthCertificateIDTextBox.Text.Trim(), RequestDateComboBox.SelectedDate, out count, pageSize, startRowIndex);
            Pager.TotalRecords = count;

            this.Cursor = Cursors.Arrow;
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                FooterStatusLine.RequestStepID = requestInfo.StepID;
                FooterStatusLine.DrawStates(requestInfo.ID);
                FooterStatusBar.Visibility = Visibility.Collapsed;
                FooterStatusLine.Visibility = Visibility.Visible;

                foreach (object currentItem in ItemsDataGrid.SelectedItems)
                {
                    requestInfo = currentItem as RequestInfo;
                }
            }
        }

        private void RequestIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
                switch (currentRequest.RequestTypeID)
                {
                    case (byte)DB.RequestType.ADSL:
                        ADSLFullView aDSLFullViewWindow = new ADSLFullView(requestID);
                        aDSLFullViewWindow.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ADSLChangeService:
                        ADSLChangeTariffFullView aDSLChangeTariffFullViewWindow = new ADSLChangeTariffFullView(requestID);
                        aDSLChangeTariffFullViewWindow.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ADSLCutTemporary:
                        ADSLCutTemporaryFullView aDSLCutTemporaryFullView = new ADSLCutTemporaryFullView(requestID);
                        aDSLCutTemporaryFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ADSLInstalPAPCompany:
                        ADSLPAPFullView aDSLInstalPAPFullView = new ADSLPAPFullView(requestID);
                        aDSLInstalPAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ADSLDischargePAPCompany:
                        ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ChangeName:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ChangeAddress:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.CutAndEstablish:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.ChangeNo:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.Dayri:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.OpenAndCloseZero:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.SpecialService:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.TitleIn118:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.Dischargin:
                        //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestID);
                        //aDSLDischargePAPFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.SpaceandPower:
                        SpaceAndPowerFullView spaceAndPowerFullView = new SpaceAndPowerFullView(requestID);
                        spaceAndPowerFullView.ShowDialog();
                        break;

                    case (byte)DB.RequestType.Failure117:
                        Failure117FullView failure117FullView = new Failure117FullView(requestID);
                        failure117FullView.ShowDialog();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر با خطا رخ داده است ! ", ex);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemsDataGrid.SelectedIndex >= 0)
                {
                    RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;

                    Request currentRequest = Data.RequestDB.GetRequestByID(requestInfo.ID);

                    switch (currentRequest.RequestTypeID)
                    {
                        case (byte)DB.RequestType.ADSL:
                            ADSLFullView aDSLFullViewWindow = new ADSLFullView(requestInfo.ID);
                            aDSLFullViewWindow.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ADSLChangeService:
                            ADSLChangeTariffFullView aDSLChangeTariffFullViewWindow = new ADSLChangeTariffFullView(requestInfo.ID);
                            aDSLChangeTariffFullViewWindow.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ADSLCutTemporary:
                            ADSLCutTemporaryFullView aDSLCutTemporaryFullView = new ADSLCutTemporaryFullView(requestInfo.ID);
                            aDSLCutTemporaryFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ADSLInstalPAPCompany:
                            ADSLPAPFullView aDSLInstalPAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            aDSLInstalPAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ADSLDischargePAPCompany:
                            ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ChangeName:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ChangeLocationCenterInside:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ChangeAddress:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.CutAndEstablish:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.ChangeNo:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.Dayri:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.OpenAndCloseZero:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.SpecialService:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.TitleIn118:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.Dischargin:
                            //ADSLPAPFullView aDSLDischargePAPFullView = new ADSLPAPFullView(requestInfo.ID);
                            //aDSLDischargePAPFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.SpaceandPower:
                            SpaceAndPowerFullView spaceAndPowerFullView = new SpaceAndPowerFullView(requestInfo.ID);
                            spaceAndPowerFullView.ShowDialog();
                            break;

                        case (byte)DB.RequestType.Failure117:
                            Failure117FullView failure117FullView = new Failure117FullView(requestInfo.ID);
                            failure117FullView.ShowDialog();
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر با خطا رخ داده است ! ", ex);
            }
        }

        private void ItemsDataGrid_LoadingRow(object sender, System.Windows.Controls.DataGridRowEventArgs e)
        {
            RequestInfo item = e.Row.Item as RequestInfo;

            if ((bool)!item.IsViewed)
            {
                e.Row.FontWeight = FontWeights.Bold;
                e.Row.Background = Brushes.Aquamarine;
            }

            switch (item.RequestTypeID)
            {
                case (byte)DB.RequestType.ADSL:
                case (byte)DB.RequestType.ADSLChangeIP:
                case (byte)DB.RequestType.ADSLChangeService:
                case (byte)DB.RequestType.ADSLDischarge:
                case (byte)DB.RequestType.ADSLSupport:
                case (byte)DB.RequestType.ADSLInstall:
                case (byte)DB.RequestType.ADSLInstalPAPCompany:
                case (byte)DB.RequestType.ADSLDischargePAPCompany:
                case (byte)DB.RequestType.ADSLExchangePAPCompany:
                case (byte)DB.RequestType.Failure117:

                    break;

                default:
                    if (item.isValidTime)
                    {
                        e.Row.FontWeight = FontWeights.Bold;
                        e.Row.Background = Brushes.Red;

                    }
                    break;
            }
        }

        #endregion

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void AddressContextMenu_Click(object sender, RoutedEventArgs e)
        {
            RequestFollowInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestFollowInfo;
            if (requestInfo == null) return;

            if (DB.IsFixRequest(requestInfo.RequestTypeID))
            {
                long addressID = DB.GetRequestAddress(requestInfo.ID, requestInfo.RequestTypeID);

                if (addressID == -1)
                    Folder.MessageBox.ShowInfo("این روال آدرس ندارد");
                else
                {
                    CustomerAddressForm address = new CustomerAddressForm(addressID);
                    address.IsEnabled = false;
                    address.Show();

                }
            }
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {

            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            long requestID = 0;
            long telephoneNo = 0;

            List<RequestFollowInfo> requestList = new List<RequestFollowInfo>();

            if (!string.IsNullOrEmpty(RequestIDTextBox.Text))
                requestID = Convert.ToInt64(RequestIDTextBox.Text);

            if (!string.IsNullOrEmpty(TelephoneNoTextBox.Text))
                telephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);

            int count = default(int);



            DataSet data = new DataSet();

            data = Data.RequestDB.SearchFollowRequest(requestID, telephoneNo, NationalCodeTextBox.Text.Trim(), RequestTypesComboBox.SelectedIDs, CustomerNameTextBox.Text.Trim(), CustomerLastNameTextBox.Text.Trim(), FatherNameTextBox.Text.Trim(), BirthCertificateIDTextBox.Text.Trim(), RequestDateComboBox.SelectedDate, out count, pageSize, startRowIndex).ToDataSet("Result", ItemsDataGrid);

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            this.Cursor = Cursors.Arrow;

        }
    }
}
