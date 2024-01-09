using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using CRM.Data;
using System.Linq;
using System;
using System.Windows.Controls;
using Enterprise;
using System.Threading.Tasks;
using CRM.Application.Local;
using System.Windows.Media;
using System.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ForeignSupportList.xaml
    /// </summary>
    public partial class ForeignSupportList : Local.TabWindow
    {
        #region Properties

        public int SelectedStepID { get; set; }
        public int RequestTypeID { get; set; }
        public bool IsInquiryMode { get; set; }
        //public bool IsArchived { get; set; }

        public Views.ADSLSetup _ADSLSetUp { get; set; }

        #endregion

        #region Constructor
        public ForeignSupportList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypesComboBox.ItemsSource = Data.TypesDB.GetHaveForeignSupportStepRequestTypeCheckable();
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();

            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Delete, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward };
        }
        public void LoadData()
        {
            if (IsInquiryMode)
            {
                InquiryPanel.Visibility = Visibility.Visible;
                ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print };
            }
            else
                InquiryPanel.Visibility = Visibility.Collapsed;

            //if (IsArchived)
            //{
            //    ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print };
            //}
            //else
            //{
            //}

            Search(null, null);
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

        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;

            List<int> RequestSteps = new List<int>();
            RequestSteps.Add((int)DB.ADSLRequestStep.ForeignSupport);
            RequestSteps.Add((int)DB.ADSLInstallRequest.ForeignSupport);

            Pager.TotalRecords = Data.RequestDB.SearchRequestsHaveSCount(IDTextBox.Text, TelephoneNoTextBox.Text, RequestStartDate.SelectedDate, RequestEndDate.SelectedDate, ModifyStartDate.SelectedDate, ModifyEndDate.SelectedDate, RequestTypesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, CustomerNameTextBox.Text, RequesterNameTextBox.Text, RequestSteps, RequestLetterNoTextBox.Text, LetterDate.SelectedDate, IsInquiryMode);
            ItemsDataGrid.ItemsSource = Data.RequestDB.SearchRequestsHaveForeigSupportStep(IDTextBox.Text, TelephoneNoTextBox.Text, RequestStartDate.SelectedDate, RequestEndDate.SelectedDate, ModifyStartDate.SelectedDate, ModifyEndDate.SelectedDate, RequestTypesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, CustomerNameTextBox.Text, RequesterNameTextBox.Text, RequestSteps, RequestLetterNoTextBox.Text, LetterDate.SelectedDate, IsInquiryMode, null, pageSize, startRowIndex);
            ItemsDataGrid.SelectedItem = null;

            FooterStatusBar.Visibility = Visibility.Visible;
            FooterStatusLine.Visibility = Visibility.Collapsed;
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            IDTextBox.Text = string.Empty;
            TelephoneNoTextBox.Text = string.Empty;
            RequestStartDate.SelectedDate = null;
            RequestEndDate.SelectedDate = null;
            ModifyStartDate.SelectedDate = null;
            ModifyEndDate.SelectedDate = null;

            //if (IsInquiryMode || IsArchived)
            //{
            //    RequestTypesComboBox.Reset();
            //    //StepsComboBox.Reset();
            //}

            CentersComboBox.Reset();
            CustomerNameTextBox.Text = string.Empty;
            RequesterNameTextBox.Text = string.Empty;
            RequestLetterNoTextBox.Text = string.Empty;
            LetterDate.SelectedDate = null;
            RequestTypesComboBox.Reset();

            Search(null, null);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
        }

        private void ItemEdit(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                if (requestInfo == null) return;

                try
                {
                    _ADSLSetUp = new ADSLSetup(requestInfo.ID);

                    _ADSLSetUp.SavedValueLabel.Visibility = Visibility.Collapsed;
                    _ADSLSetUp.SavedValueImage.Visibility = Visibility.Collapsed;
                    _ADSLSetUp.ModemSerilaNoComboBox.IsEnabled = false;
                    _ADSLSetUp.ModemTypeTextBox.IsReadOnly = true;
                    _ADSLSetUp.ModemMACAddressTextBox.IsReadOnly = true;
                    _ADSLSetUp.ActionIDs = new List<byte> { (byte)DB.NewAction.Exit };
                    
                    _ADSLSetUp.ShowDialog();

                    LoadData();
                }
                catch (Exception ex)
                {
                    FooterStatusBar.Visibility = Visibility.Visible;
                    FooterStatusLine.Visibility = Visibility.Collapsed;
                    ShowErrorMessage(ex.Message, ex);
                }
            }
        }

        private void ItemsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActionUserControl.ItemIDs.Clear();

            if (ItemsDataGrid.SelectedItem != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                FooterStatusLine.RequestStepID = requestInfo.StepID;
                FooterStatusLine.DrawStates(requestInfo.ID);
                FooterStatusBar.Visibility = Visibility.Collapsed;
                FooterStatusLine.Visibility = Visibility.Visible;

                ActionUserControl.ItemIDs.Clear();

                foreach (object currentItem in ItemsDataGrid.SelectedItems)
                {
                    requestInfo = currentItem as RequestInfo;
                    ActionUserControl.ItemIDs.Add(requestInfo.ID);
                }
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

        private void ShowFlowchart(object sender, RoutedEventArgs e)
        {
            RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
            RequestFlowchart tabWindow = new RequestFlowchart();
            tabWindow.RequestID = requestInfo.ID;
            Folder.Console.Navigate(tabWindow, "سابقه عملیات");
        }

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        private void ItemsDataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                ItemEdit(null, null);
                return;
            }
        }

        private void NumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر خطا رخ داده است ! ", ex);
            }
        }

        private void ShowRequestInfo(object sender, RoutedEventArgs e)
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

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر خطا رخ داده است ! ", ex);
            }
        }

        private void GroupingForwardEdit(object sender, RoutedEventArgs e)
        {
            foreach (RequestInfo SelectedRequestInfo in ItemsDataGrid.SelectedItems)
            {
                try
                {
                    ForwardByRequestID(SelectedRequestInfo.ID);
                }
                catch
                {
                    Folder.MessageBox.ShowInfo("خطا در ارجاع در خواست :" + SelectedRequestInfo.ID);
                }
            }

        }

        public void ForwardByRequestID(long requestID)
        {
            Request request = Data.RequestDB.GetRequestByID(requestID);

            //اکشنهای مربوط به وضعیت جاری را استخراج میکند اگر در انها اکشن ارجاع اتو باشد فرم و روال مربوطه راا برسی میکند و متد مربوط به ارجاع اتو را اجرا میکند
            // در غیر اینصورت ارجاع با اکشن تایید انجام میشود
            List<int?> actions = Data.WorkFlowDB.GetActionsCurrentStatus(request.StatusID);
            if (actions == null) { MessageBox.Show("وضعیت فعلی در چرخه کاری یافت نشد"); return; };

            if (actions.Contains((int)DB.Action.AutomaticForward))
            {
                int? formID = Data.WorkFlowDB.GetProperForm(request.ID, request.StatusID);

                if (formID != null && formID == (int)CRM.Data.DB.Form.Investigation)
                {

                    var investigatePossibilityForm = new InvestigatePossibilityForm(request.ID , null);
                    investigatePossibilityForm.Load();
                    investigatePossibilityForm.Forward();
                }
                if (formID != null && formID == (int)CRM.Data.DB.Form.Investigation)
                {
                    var investigatePossibilityForm = new InvestigatePossibilityForm(request.ID, null);
                    investigatePossibilityForm.Load();
                    investigatePossibilityForm.Forward();
                }

                if (formID != null && formID == (int)CRM.Data.DB.Form.ChooseNumber)
                {
                    var investigatePossibilityForm = new InvestigatePossibilityForm(request.ID, null);
                    investigatePossibilityForm.Load();
                    investigatePossibilityForm.Forward();
                }
            }
            else if (actions.Contains((int)DB.Action.Confirm))
            {
                Status Status = Data.StatusDB.GetStatueByStatusID(request.StatusID);
                if (Status.StatusType == (byte)DB.RequestStatusType.Completed || Status.StatusType == (byte)DB.RequestStatusType.Start)
                {
                    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);
                }
            }
        }

        #endregion
    }
}
