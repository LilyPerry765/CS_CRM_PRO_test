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
using System.Collections;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using System.Collections.ObjectModel;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Components;
using Stimulsoft.Base.Drawing;
using CRM.Application.Codes;
using System.Reflection;

namespace CRM.Application.Views
{
    public partial class RequestsInbox : Local.ExtendedTabWindowBase
    {
        #region Properties

        public int SelectedStepID { get; set; }
        public int RequestTypeID { get; set; }
        public bool IsInquiryMode { get; set; }
        public bool IsArchived { get; set; }
        public List<int> SelectedRequestTypesID { get; set; }

        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        DataGrid SubE1ItemsDataGrid;
        #endregion

        #region Constructors

        public RequestsInbox()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RequestTypesComboBox.ItemsSource = Data.TypesDB.GetRequestTypesCheckable();
            CentersComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            PaymentTypesComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            StepsComboBox.ItemsSource = Data.WorkFlowDB.GetRequestStepsCheckable();
            this.SelectedRequestTypesID = new List<int>();
            ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Delete, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny };
        }

        public void LoadData()
        {
            if (SelectedStepID != 0)
            {
                foreach (CheckableItem item in StepsComboBox.Items)
                {
                    if (item.ID == SelectedStepID)
                    {
                        StepsComboBox.SelectedIndex = StepsComboBox.Items.IndexOf(item);
                        item.IsChecked = true;
                        break;
                    }
                }

                StepsComboBox.IsEnabled = false;
            }

            if (RequestTypeID != 0)
            {
                foreach (CheckableItem item in RequestTypesComboBox.Items)
                {
                    if (item.ID == RequestTypeID)
                    {
                        RequestTypesComboBox.SelectedIndex = RequestTypesComboBox.Items.IndexOf(item);
                        item.IsChecked = true;
                        break;
                    }
                }

                RequestTypesComboBox.IsEnabled = false;
            }

            if (IsInquiryMode)
            {
                InquiryPanel.Visibility = Visibility.Visible;
                ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print };
            }
            else
                InquiryPanel.Visibility = Visibility.Collapsed;

            if (IsArchived)
                ActionUserControl.ActionIDs = new List<byte> { (byte)DB.NewAction.Print };

            if (DB.City == "kermanshah")
            {
                if (RequestTypeID == (byte)DB.RequestType.Failure117)
                {
                    GroupItemsEditMenu.Visibility = Visibility.Visible;

                    if (SelectedStepID == (byte)DB.RequestStepFailure117.Network)
                    {
                        FailurePrintMenu.Visibility = Visibility.Visible;
                        //FailureReplicaPrintMenu.Visibility = Visibility.Visible; 
                    }
                }
                else if (DB.IsFixRequest(RequestTypeID))
                {
                    AddressContextMenu.Visibility = Visibility.Visible;
                }
            }

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

        //milad doran
        //private void Search(object sender, RoutedEventArgs e)
        //{
        //    this.Cursor = Cursors.Wait;
        //    int startRowIndex = Pager.StartRowIndex;
        //    int pageSize = Pager.PageSize;

        //    //Pager.TotalRecords = Data.RequestDB.SearchRequestsCount(IDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(), RequestStartDate.SelectedDate, RequestEndDate.SelectedDate, ModifyStartDate.SelectedDate, ModifyEndDate.SelectedDate, RequestTypesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, CustomerNameTextBox.Text, RequesterNameTextBox.Text, PaymentTypesComboBox.SelectedIDs, StepsComboBox.SelectedIDs, RequestLetterNoTextBox.Text, LetterDate.SelectedDate, IsInquiryMode, IsArchived);
        //    int totalRecords = default(int);

        //    ItemsDataGrid.ItemsSource = Data.RequestDB.SearchRequests(
        //                                                                IDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(),
        //                                                                RequestStartDate.SelectedDate, RequestEndDate.SelectedDate,
        //                                                                ModifyStartDate.SelectedDate, ModifyEndDate.SelectedDate,
        //                                                                RequestTypesComboBox.SelectedIDs, CentersComboBox.SelectedIDs,
        //                                                                CustomerNameTextBox.Text, RequesterNameTextBox.Text,
        //                                                                PaymentTypesComboBox.SelectedIDs, StepsComboBox.SelectedIDs,
        //                                                                RequestLetterNoTextBox.Text, LetterDate.SelectedDate,
        //                                                                IsInquiryMode, IsArchived,
        //                                                                RequestRejectReasonDescriptionTextBox.Text.Trim(), out totalRecords,
        //                                                                null, pageSize, startRowIndex
        //                                                              );
        //    Pager.TotalRecords = totalRecords;
        //    ItemsDataGrid.SelectedItem = null;

        //    FooterStatusBar.Visibility = Visibility.Visible;
        //    FooterStatusLine.Visibility = Visibility.Collapsed;
        //    this.Cursor = Cursors.Arrow;
        //}

        //TODO:rad 139500711
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = default(int);

            string id = IDTextBox.Text.Trim();
            string telephoneNo = TelephoneNoTextBox.Text.Trim();
            DateTime? requestStartDate = RequestStartDate.SelectedDate;
            DateTime? requestEndDate = RequestEndDate.SelectedDate;
            DateTime? modifyStartDate = ModifyStartDate.SelectedDate;
            DateTime? modifyEndDate = ModifyEndDate.SelectedDate;
            List<int> requestTypesId = RequestTypesComboBox.SelectedIDs;
            List<int> centersId = CentersComboBox.SelectedIDs;
            string customerName = CustomerNameTextBox.Text.Trim();
            string requesterName = RequesterNameTextBox.Text.Trim();
            List<int> paymentTypesId = PaymentTypesComboBox.SelectedIDs;
            List<int> stepsId = StepsComboBox.SelectedIDs;
            string requestLetterNo = RequestLetterNoTextBox.Text.Trim();
            DateTime? letterDate = LetterDate.SelectedDate;
            string rejectReason = RequestRejectReasonDescriptionTextBox.Text.Trim();

            Action mainAction = new Action(() =>
            {
                List<RequestInfo> result = Data.RequestDB.SearchRequests(
                                                                        id, telephoneNo,
                                                                        requestStartDate, requestEndDate,
                                                                        modifyStartDate, modifyEndDate,
                                                                        requestTypesId, centersId,
                                                                        customerName, requesterName,
                                                                        paymentTypesId, stepsId,
                                                                        requestLetterNo, letterDate,
                                                                        IsInquiryMode, IsArchived,
                                                                        rejectReason, out count,
                                                                        null, pageSize, startRowIndex
                                                                      );

                Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                    {
                                                                                        ItemsDataGrid.ItemsSource = result;
                                                                                        Pager.TotalRecords = count;
                                                                                        ItemsDataGrid.SelectedItem = null;

                                                                                        FooterStatusBar.Visibility = Visibility.Visible;
                                                                                        FooterStatusLine.Visibility = Visibility.Collapsed;

                                                                                    }
                                                                              )
                                      );
            });


            //مقداردهی عملیات اطلاع رسانی از وضعیت اجرای عملیات اصلی
            Action duringOperationAction = new Action(() =>
            {
                FooterStatusBar.ShowProgressBar = true;
                FooterStatusBar.MessageLabel.FontSize = 13;
                FooterStatusBar.MessageLabel.FontWeight = FontWeights.Bold;
                FooterStatusBar.MessageLabel.Text = "درحال بارگذاری...";
                Pager.IsEnabled = false;
                SearchExpander.IsEnabled = false;
                ItemsDataGrid.IsEnabled = false;
                this.Cursor = Cursors.Wait;
            });

            //مقداردهی عملیاتی که باید بعد از اتمام عملیات اصلی اجرا شود 
            Action afterOperationAction = new Action(() =>
            {
                FooterStatusBar.ShowProgressBar = false;
                FooterStatusBar.MessageLabel.FontSize = 8;
                FooterStatusBar.MessageLabel.FontWeight = FontWeights.Normal;
                FooterStatusBar.MessageLabel.Text = string.Empty;
                Pager.IsEnabled = true;
                SearchExpander.IsEnabled = true;
                ItemsDataGrid.IsEnabled = true;
                this.Cursor = Cursors.Arrow;
            });

            CRM.Application.Local.TimeConsumingOperation timeConsumingOperation = new Local.TimeConsumingOperation
            {
                MainOperationAction = mainAction,
                DuringOperationAction = duringOperationAction,
                AfterOperationAction = afterOperationAction
            };

            //اجرای عملیات
            this.RunTimeConsumingOperation(timeConsumingOperation);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {

            //if (IsInquiryMode || IsArchived)
            //{
            //    RequestTypesComboBox.Reset();
            //    StepsComboBox.Reset();
            //}

            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
        }

        private void ItemEdit(object sender, RoutedEventArgs e)
        {
            try
            {

                if (ItemsDataGrid.SelectedIndex >= 0)
                {
                    if (IsArchived) return;

                    List<RequestInfo> requestInfos = ItemsDataGrid.SelectedItems.Cast<RequestInfo>().ToList();
                    if (requestInfos == null) return;

                    if (IsInquiryMode == true) return;
                    if (IsArchived == true) return;

                    try
                    {

                        Local.PopupWindow window = Local.FormSelector.Select(requestInfos.Select(t => t.ID).ToList(), requestInfos.Take(1).SingleOrDefault().StatusID);
                        window.currentStep = requestInfos.Take(1).SingleOrDefault().StepID;
                        window.currentStat = requestInfos.Take(1).SingleOrDefault().StatusID;

                        if (requestInfos.Any(t => t.IsViewed == false))
                        {
                            List<Request> currenrRequests = RequestDB.GetRequestByIDs(requestInfos.Select(t => t.ID).ToList());

                            currenrRequests.ForEach(t => { t.IsViewed = true; t.Detach(); });

                            DB.UpdateAll(currenrRequests);
                        }

                        window.ShowDialog();

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
            catch (NullReferenceException ex)
            {
                ShowErrorMessage("خطا در نمایش فرم، لطفا با پشتیبانی تماس حاصل فرمایید. ", ex);
            }
            catch (InvalidOperationException ex)
            {
                ShowErrorMessage("خطا در نمایش فرم، لطفا با پشتیبانی تماس حاصل فرمایید.", ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در نمایش فرم، " + ex.Message + " !", ex);
            }
        }

        private void GroupItemsEdit(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItems[0] as RequestInfo;

                if (DB.IsFixRequest(this.RequestTypeID))
                {
                    foreach (RequestInfo SelectedRequestInfo in ItemsDataGrid.SelectedItems)
                    {
                        try
                        {
                            ForwardByRequestID(SelectedRequestInfo.ID, SelectedRequestInfo);
                        }
                        catch
                        {
                            MessageBox.Show("خطا در ارجاع در خواست :" + SelectedRequestInfo.ID, "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    List<long> RequestIDs = new List<long>();

                    foreach (RequestInfo SelectedRequestInfo in ItemsDataGrid.SelectedItems)
                    {
                        RequestIDs.Add(SelectedRequestInfo.ID);

                        if ((bool)!SelectedRequestInfo.IsViewed)
                        {
                            Request currenrRequest = RequestDB.GetRequestByID(SelectedRequestInfo.ID);

                            currenrRequest.IsViewed = true;

                            currenrRequest.Detach();
                            DB.Save(currenrRequest);
                        }
                    }

                    Local.PopupWindow window = null;
                    if (requestInfo.StepID == (byte)DB.RequestStepFailure117.MDFAnalysis || requestInfo.StepID == (byte)DB.RequestStepFailure117.MDFConfirm)
                        window = new Failure117Form(RequestIDs);
                    //if(requestInfo.StepID == (byte)DB.RequestStepFailure117.Network)
                    //    window = new Failure117NetworkForm(RequestIDs);

                    window.ShowDialog();
                }

                LoadData();
            }
        }

        public void ForwardByRequestID(long requestID, RequestInfo requestInfo)
        {
            try
            {
                Data.Schema.ActionLogRequest actionLogRequest = new Data.Schema.ActionLogRequest();
                // int? formID = Data.WorkFlowDB.GetProperForm(requestID, requestInfo.StatusID);

                Local.PopupWindow popupWindow = Local.FormSelector.Select(new List<long> { requestID }, requestInfo.StatusID);
                popupWindow.currentStep = requestInfo.StepID;
                popupWindow.currentStat = requestInfo.StatusID;

                Type popupWindowType = popupWindow.GetType();
                MethodInfo Method = popupWindowType.GetMethod("LoadData");
                Method.Invoke(popupWindow, null);

                popupWindowType = popupWindow.GetType();
                Method = popupWindowType.GetMethod("Forward");
                bool isForwardSuccess = (bool)Method.Invoke(popupWindow, null);

                if (isForwardSuccess)
                {
                    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);
                    actionLogRequest.FormType = popupWindow.GetType().FullName;
                    actionLogRequest.FormName = popupWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Forward, Folder.User.Current.Username, actionLogRequest);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



            //if (formID != null && formID == (int)CRM.Data.DB.Form.MDFWiring)
            //{
            //    MDFWiringForm window = new MDFWiringForm(requestID, null);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationPCMToNormalMDFForm)
            //{
            //    TranslationPCMToNormalMDFForm window = new TranslationPCMToNormalMDFForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationOpticalToNormalForm)
            //{
            //    TranslationOpticalCabinetToNormalForm window = new TranslationOpticalCabinetToNormalForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationOpticalToNormalMDFForm)
            //{
            //    TranslationOpticalToNormalMDFForm window = new TranslationOpticalToNormalMDFForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationOpticalToNormalNetworkForm)
            //{
            //    TranslationOpticalToNormalNetworkForm window = new TranslationOpticalToNormalNetworkForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationCabinetNetworkFrom)
            //{
            //    ExchangeCabinetInputNetworkForm window = new ExchangeCabinetInputNetworkForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.Wiring)
            //{
            //    WiringForm window = new WiringForm(requestID, null);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.Install)
            //{
            //    Views.RequestForm window = new Views.RequestForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationPCMToNormalForm)
            //{
            //    Views.TranslationPCMToNormalForm window = new Views.TranslationPCMToNormalForm(requestID);
            //    window.currentStep = requestInfo.StepID;
            //    window.currentStat = requestInfo.StatusID;
            //    window.LoadData();
            //    window._exchangeRequestInfo.Load();
            //    window.Forward();

            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    actionLogRequest.FormType = window.GetType().FullName;
            //    actionLogRequest.FormName = window.Title;
            //}
            //else if (formID != null && formID == (int)CRM.Data.DB.Form.TranslationPCMToNormalNetworkForm)
            //{

            //    Type popupWindowType = popupWindow.GetType();
            //    MethodInfo LoadDataMethod = popupWindowType.GetMethod("LoadData");
            //    LoadDataMethod.Invoke(popupWindow, null);

            //    popupWindowType = popupWindow.GetType();
            //    LoadDataMethod = popupWindowType.GetMethod("Forward");
            //    LoadDataMethod.Invoke(popupWindow, null);


            //    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);
            //    actionLogRequest.FormType = popupWindow.GetType().FullName;
            //    actionLogRequest.FormName = popupWindow.Title;

            //    //Views.TranslationPCMToNormalNetworkForm window = new Views.TranslationPCMToNormalNetworkForm(requestID);
            //    //window.currentStep = requestInfo.StepID;
            //    //window.currentStat = requestInfo.StatusID;
            //    //window.LoadData();
            //    //window.Forward();

            //    //Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestID);

            //    //actionLogRequest.FormType = window.GetType().FullName;
            //    //actionLogRequest.FormName = window.Title;
            //}
            //else
            //{
            //    Folder.MessageBox.ShowInfo("امکان ارجاع گروهی برای این قسمت ممکن نمی باشد");
            //}



        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                if (requestInfo == null) return;
                if (IsInquiryMode == true) return;
                if (IsArchived == true) return;

                try
                {
                    MessageBoxResult result = MessageBox.Show("آیا از ارجاع مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        ForwardByRequestID(requestInfo.ID, requestInfo);
                        Search(sender, e);

                        FooterStatusBar.Visibility = Visibility.Visible;
                        FooterStatusLine.Visibility = Visibility.Collapsed;
                        ShowSuccessMessage("ارجاع انجام شد");
                    }
                }
                catch (Exception ex)
                {
                    FooterStatusBar.Visibility = Visibility.Visible;
                    FooterStatusLine.Visibility = Visibility.Collapsed;
                    ShowErrorMessage("خطا در ارجاع", ex);
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
                //FooterStatusBar.Visibility = Visibility.Collapsed;
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
                case (byte)DB.RequestType.ADSLChangePort:
                case (byte)DB.RequestType.ADSLChangeService:
                case (byte)DB.RequestType.ADSLSellTraffic:
                case (byte)DB.RequestType.ADSLDischarge:
                case (byte)DB.RequestType.ADSLCutTemporary:
                case (byte)DB.RequestType.ADSLChangePlace:
                case (byte)DB.RequestType.ADSLSupport:
                case (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
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
                        //SpaceAndPowerFullView spaceAndPowerFullView = new SpaceAndPowerFullView(requestID);
                        //spaceAndPowerFullView.ShowDialog();
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
                ShowErrorMessage("در نمایش اطلاعات درخواست مورد نظر خطا رخ داده است ! ", ex);
            }
        }

        private void GroupingForwardEdit(object sender, RoutedEventArgs e)
        {


        }


        private void SubItemEdit(object sender, RoutedEventArgs e)
        {
            if (SubE1ItemsDataGrid != null && SubE1ItemsDataGrid.SelectedItem != null && ItemsDataGrid.SelectedItem != null)
            {
                E1LinkInfo e1LinkInfo = SubE1ItemsDataGrid.SelectedItem as E1LinkInfo;
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;

                Local.PopupWindow window = Local.FormSelector.Select(new List<long>() { requestInfo.ID }, requestInfo.StatusID, e1LinkInfo.ID);
                window.currentStep = requestInfo.StepID;
                window.currentStat = requestInfo.StatusID;

                window.ShowDialog();
            }
        }

        private void RequestStatusClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
                RequestStatusViewForm window = new RequestStatusViewForm(requestInfo);
                window.ShowDialog();
            }
        }


        private void SubE1ItemsDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            SubE1ItemsDataGrid = sender as DataGrid;
        }


        private List<AssignmentInfo> LoadPCMInfo(Bucht bucht)
        {
            PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
            List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
            List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

            List<AssignmentInfo> assingmentInfo = DB.GetAllInformationByBuchtIDs(buchtList.Select(b => b.ID).ToList(), (byte)DB.BuchtStatus.AllocatedToInlinePCM);
            return assingmentInfo;
        }

        private List<BuchtNoInfo> GetBuchtNoCentralInfo(Bucht bucht)
        {
            PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
            List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
            List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();
            List<BuchtNoInfo> BuchtnoInfoList = new List<BuchtNoInfo>();
            BuchtNoInfo BuchtnoInfo = new BuchtNoInfo();

            Bucht buchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
            BuchtnoInfo.BuchNoInput = DB.GetConnectionByBuchtID(buchtConnectToInputCabinet.ID);
            BuchtnoInfo.BuchtNoInputPCM = DB.GetConnectionByBuchtID(buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID);
            BuchtnoInfo.Radif = DB.GetBuchtInfoByBuchtIDInSeperation(buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID)[0].Radif;
            BuchtnoInfo.Tabaghe = DB.GetBuchtInfoByBuchtIDInSeperation(buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID)[0].Tabaghe;
            BuchtnoInfo.CabinetInputID = DB.GetBuchtInfoByBuchtIDInSeperation(buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID)[0].CabinetInputID;
            BuchtnoInfoList.Add(BuchtnoInfo);

            return BuchtnoInfoList;
        }

        public static bool ShowReport(IEnumerable result, string Title, int ReportTemplateID, bool ShowOldAddressInPrint = false)
        {
            try
            {
                DateTime currentDateTime = DB.GetServerDate();

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                string path = Data.ReportDB.GetReportPath(ReportTemplateID);
                stiReport.Load(path);
                int FindVariable = stiReport.Dictionary.Variables.Items.Where(t => t.Name == "ShowOldAddress").Count();
                if (FindVariable > 0)
                    stiReport.Dictionary.Variables["ShowOldAddress"].ValueObject = ShowOldAddressInPrint;

                stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

                stiReport.RegData("result", "result", result);


                if (stiReport != null)
                {
                    var frm = new ReportViewerForm(stiReport);
                    frm.ShowDialog();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return false;
        }

        public static IEnumerable GetReportByRequestIDAndStep(List<long> RequestIDs, int? ReportTemplateID, ref string HeaderTitle)
        {

            List<EnumItem> AllSpecialWireType = Helper.GetEnumItem(typeof(DB.SpecialWireType));
            switch (ReportTemplateID)
            {
                case (int)DB.UserControlNames.SpecialWiringWiringNetworkReport:
                    return ReportDB.GetSpecialWireWiringNetwork(RequestIDs);
                    break;

                case (int)DB.UserControlNames.VisitAddressReport:
                    return ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                    break;

                case (int)DB.UserControlNames.SpecialWireCertificatePrintReport:
                    HeaderTitle = "چاپ گواهی سیم خصوصی";
                    List<Report_SpecialWireCertificatePrintResult> res = new List<Report_SpecialWireCertificatePrintResult>();
                    res = ReportDB.GetSpecialWireCertificatePrint(new List<int> { }, new List<int> { }, RequestIDs.Cast<long?>().ToList(), null, null, null, null, null);
                    res.ForEach(t => t.SpecialType = ((t.SpecialType == "0") ? "" : AllSpecialWireType.Find(i => i.ID == Convert.ToByte(t.SpecialType)).Name));
                    return res;
                    break;

                case (int)DB.UserControlNames.MDFVacateSpecialWireReport:
                    return ReportDB.GetMDFVacatePrivateWire(RequestIDs);
                    break;

                case (int)DB.UserControlNames.NetworkVacateSpecialWireReport:
                    return ReportDB.GetNetworkVacateSpecialWire(RequestIDs);
                    break;

                case (int)DB.UserControlNames.VacateSpecialWireCertificate:

                    return ReportDB.GetVacateSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, RequestIDs.Cast<long?>().ToList());
                    break;

                case (int)DB.UserControlNames.ChangeLocationSpecialWireCertificate:
                    return ReportDB.GetChangeLocationSpecialwireCertificateInfo(null, null, new List<int> { }, new List<int> { }, RequestIDs.Cast<long?>().ToList());
                    break;

                case (int)DB.UserControlNames.ChangeLocationNetworkSpecialWire:
                    return ReportDB.GetChangeLocationNetworkSpecialWire(RequestIDs);
                    break;

                case (int)DB.UserControlNames.ChangeLocationMDFSpecialWire:

                    List<Report_ChangeLocationMDFSpecialWireResult> resSpecialWire = ReportDB.GetChangeLocationMDFSpecialwire(RequestIDs);
                    //resSpecialWire.ForEach(t =>
                    //{
                    //    AssignmentInfo oasi = DB.GetAllInformationByBuchtID(string.IsNullOrEmpty(t.OldOtherBuchtID) ? 0 : Convert.ToInt64(t.OldOtherBuchtID));
                    //    t.OldOtherBuchtID = ((oasi != null) ? oasi.Connection : "");


                    //    AssignmentInfo nasi = DB.GetAllInformationByBuchtID(string.IsNullOrEmpty(t.NewOtherBuchtID) ? 0 : Convert.ToInt64(t.NewOtherBuchtID));
                    //    t.NewOtherBuchtID = ((nasi != null) ? nasi.Connection : "");
                    //});
                    return resSpecialWire;
                    break;

                case (int)DB.UserControlNames.MDFTranslationPostInput:
                    return ReportDB.GetTranslationPostInputMDF(RequestIDs);
                    break;

                case (int)DB.UserControlNames.NetworkWireExchangeCentralPostReport:
                    return ReportDB.GetNetworkWireExchangeCentralPost(RequestIDs);
                    break;

            }
            return null;
        }

        private void FailurePrintClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItems[0] as RequestInfo;

                List<long> RequestIDs = new List<long>();

                foreach (RequestInfo SelectedRequestInfo in ItemsDataGrid.SelectedItems)
                {
                    RequestIDs.Add(SelectedRequestInfo.ID);
                }

                List<Failure117NetworkReport> result = new List<Failure117NetworkReport>();
                Failure117NetworkReport RecordTemp = new Failure117NetworkReport();
                List<FailureHistoryInfo> historyListTemp = new List<FailureHistoryInfo>();

                foreach (long requestID in RequestIDs)
                {
                    FailureForm form = Failure117DB.GetFailureForm(requestID);

                    if (form.GiveNetworkFormDate == null)
                    {
                        form.GiveNetworkFormDate = DB.GetServerDate();
                        form.GiveNetworkFormTime = form.GiveNetworkFormDate.Value.Hour.ToString() + ":" + form.GiveNetworkFormDate.Value.Minute.ToString();

                        form.Detach();
                        DB.Save(form);
                    }

                    Request _Request = Data.RequestDB.GetRequestByID(requestID);
                    RequestInfo _RequestInfo = RequestDB.GetRequestInfoByID(requestID);
                    Data.Failure117 _Failure117 = Failure117DB.GetFailureRequestByID(requestID);
                    FailureFormInfo _FormInfo = Failure117DB.GetFailureFormInfo(requestID);
                    TelephoneInfoForRequest telephoneInfo = TelephoneDB.GetTelephoneInfoForFailure((long)_Request.TelephoneNo);
                    TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);
                    TechnicalInfoFailure117 aDSLInfo = Failure117DB.ADSLPAPbyTelephoneNo((long)_Request.TelephoneNo);

                    RecordTemp = new Failure117NetworkReport();

                    RecordTemp.MobileNo = telephoneInfo.Mobile;
                    RecordTemp.CallingNo = _Failure117.CallingNo.ToString();
                    RecordTemp.Address = telephoneInfo.Address;
                    RecordTemp.Radif = (technicalInfo != null) ? technicalInfo.RADIF : "";
                    RecordTemp.CabinetinputNo = (technicalInfo != null) ? technicalInfo.CabinetInputNumber : "";
                    RecordTemp.CabinetNo = (technicalInfo != null) ? technicalInfo.CabinetNo : "";
                    RecordTemp.CardPCM = (technicalInfo != null) ? technicalInfo.PCMCard : "";
                    RecordTemp.Center = telephoneInfo.Center;
                    RecordTemp.CustomerName = telephoneInfo.CustomerName;
                    RecordTemp.EtesaliBucht = (technicalInfo != null) ? technicalInfo.ETESALII : "";
                    RecordTemp.EtesaliInputBucht = (technicalInfo != null) ? technicalInfo.PCMInEtesali : "";
                    RecordTemp.EtesaliOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutEtesali : "";
                    RecordTemp.MDFDate = Helper.GetPersianDate(_Failure117.MDFDate, Helper.DateStringType.DateTime);
                    RecordTemp.MDFUser = UserDB.GetUserFullName(_Failure117.MDFPesonnelID);
                    RecordTemp.ModelPCM = (technicalInfo != null) ? technicalInfo.PCMModel : "";
                    RecordTemp.PhoneNo = _Request.TelephoneNo.ToString();
                    RecordTemp.PortPCM = (technicalInfo != null) ? technicalInfo.PCMPort : "";
                    RecordTemp.PostalCode = (technicalInfo != null) ? telephoneInfo.PostalCode : "";
                    RecordTemp.PostEtesaliNo = (technicalInfo != null) ? technicalInfo.ConnectionNo : "";
                    RecordTemp.PostNo = (technicalInfo != null) ? technicalInfo.PostNo : "";
                    RecordTemp.RadifBucht = (technicalInfo != null) ? technicalInfo.RADIF : "";
                    RecordTemp.RadifInputBucht = (technicalInfo != null) ? technicalInfo.PCMInRadif : "";
                    RecordTemp.RadifOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutRadif : "";
                    //RecordTemp.RequestDate = RequestDateTextBox.Text;
                    RecordTemp.RequestDate1 = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);
                    RecordTemp.RequestNo = requestID;
                    RecordTemp.RockPCM = (technicalInfo != null) ? technicalInfo.PCMRock : "";
                    RecordTemp.ShelfPCM = (technicalInfo != null) ? technicalInfo.PCMShelf : "";
                    RecordTemp.TabagheBucht = (technicalInfo != null) ? technicalInfo.TABAGHE : "";
                    RecordTemp.TabagheInputBucht = (technicalInfo != null) ? technicalInfo.PCMInTabaghe : "";
                    RecordTemp.TabagheOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutTabaghe : "";
                    RecordTemp.TypePCM = (technicalInfo != null) ? technicalInfo.PCMType : "";

                    Failure117LineStatus lineStatus = DB.SearchByPropertyName<Failure117LineStatus>("ID", (int)_Failure117.LineStatusID).SingleOrDefault();
                    RecordTemp.LineStatus = lineStatus.Title + "_" + Helper.GetEnumDescriptionByValue(typeof(DB.Failure117LineStatus), lineStatus.Type);
                    if (technicalInfo != null)
                        RecordTemp.IsPCM = (technicalInfo.IsPCM) ? "دارد" : "ندارد";
                    else
                        RecordTemp.IsPCM = "";
                    RecordTemp.ColorCable = "";// _FormInfo.CableColor1 + " - " + Color2ComboBox.Text;
                    RecordTemp.CableType = "";// _FormInfo.CableType;
                    RecordTemp.Description = _Failure117.NetworkComment;
                    RecordTemp.AdjacentTelephoneNo = _Failure117.AdjacentTelephoneNo.ToString();
                    RecordTemp.EndMDFComment = _Failure117.MDFCommnet;
                    RecordTemp.SendToCabelDate = Helper.GetPersianDate(_FormInfo.SendToCabelDate, Helper.DateStringType.Short);
                    RecordTemp.CabelDate = Helper.GetPersianDate(_Failure117.CableDate, Helper.DateStringType.Short);

                    double compareResult = ((DateTime)_Failure117.MDFDate - _Request.InsertDate).TotalMinutes;
                    double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
                    double min = Math.Round(compareResult % 60, 2);
                    RecordTemp.MDFSpeedMin = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());

                    RecordTemp.NetworkOfficer = "";// _FormInfo.Failure117NetworkContractorOfficerID;
                    RecordTemp.GiveNetworkFormDate = Helper.GetPersianDate(_FormInfo.GiveNetworkFormDate, Helper.DateStringType.Short);
                    RecordTemp.GiveNetworkFormTime = _FormInfo.GiveNetworkFormTime;
                    RecordTemp.GetNetworkFormDate = Helper.GetPersianDate(_FormInfo.GetNetworkFormDate, Helper.DateStringType.Short);
                    RecordTemp.GetNetworkFormTime = _FormInfo.GetNetworkFormTime;
                    if (aDSLInfo != null)
                    {
                        RecordTemp.ADSLRadif = aDSLInfo.ADSLRadif;
                        RecordTemp.ADSLTabaghe = aDSLInfo.ADSLTabaghe;
                        RecordTemp.ADSLEtesali = aDSLInfo.ADSLEtesali;
                    }

                    RecordTemp.Rowno = Failure117DB.GetFailureFormInfo(requestID).RowNo.ToString();

                    List<FailureHistoryInfo> historyList = new List<FailureHistoryInfo>();
                    historyList = Failure117DB.SearchFailureHistory(_Request.ID, (long)_Request.TelephoneNo);

                    List<FailureHistoryInfo> historyList1 = new List<FailureHistoryInfo>();
                    if (historyList != null && historyList.Count != 0)
                    {
                        foreach (FailureHistoryInfo item in historyList)
                        {
                            FailureForm form1 = Failure117DB.GetFailureForm(item.ID);
                            if (form1 != null)
                                item.GetNetworkFormDate = Helper.GetPersianDate(form1.GetNetworkFormDate, Helper.DateStringType.DateTime);

                            historyList1.Add(item);
                        }
                    }

                    historyList1 = historyList1.OrderByDescending(t => t.ID).Take(2).ToList();
                    historyListTemp.AddRange(historyList1);

                    result.Add(RecordTemp);
                }

                string title = string.Empty;
                string path;

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath((int)DB.UserControlNames.Failure117Network);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

                //stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("xx", "xx", result);
                stiReport.RegData("result", "historyList", historyListTemp);
                stiReport.CacheAllData = true;


                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        private void FailureReplicaPrintClick(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItems != null)
            {
                RequestInfo requestInfo = ItemsDataGrid.SelectedItems[0] as RequestInfo;

                List<long> RequestIDs = new List<long>();

                foreach (RequestInfo SelectedRequestInfo in ItemsDataGrid.SelectedItems)
                {
                    RequestIDs.Add(SelectedRequestInfo.ID);
                }

                List<Failure117NetworkReport> result = new List<Failure117NetworkReport>();
                Failure117NetworkReport RecordTemp = new Failure117NetworkReport();
                List<FailureHistoryInfo> historyListTemp = new List<FailureHistoryInfo>();

                foreach (long requestID in RequestIDs)
                {
                    Request _Request = Data.RequestDB.GetRequestByID(requestID);
                    RequestInfo _RequestInfo = RequestDB.GetRequestInfoByID(requestID);
                    Data.Failure117 _Failure117 = Failure117DB.GetFailureRequestByID(requestID);
                    FailureFormInfo _FormInfo = Failure117DB.GetFailureFormInfo(requestID);
                    TelephoneInfoForRequest telephoneInfo = TelephoneDB.GetTelephoneInfoForFailure((long)_Request.TelephoneNo);
                    TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);
                    TechnicalInfoFailure117 aDSLInfo = Failure117DB.ADSLPAPbyTelephoneNo((long)_Request.TelephoneNo);

                    RecordTemp = new Failure117NetworkReport();

                    RecordTemp.MobileNo = telephoneInfo.Mobile;
                    RecordTemp.CallingNo = _Failure117.CallingNo.ToString();
                    RecordTemp.Address = telephoneInfo.Address;
                    RecordTemp.Radif = (technicalInfo != null) ? technicalInfo.RADIF : "";
                    RecordTemp.CabinetinputNo = (technicalInfo != null) ? technicalInfo.CabinetInputNumber : "";
                    RecordTemp.CabinetNo = (technicalInfo != null) ? technicalInfo.CabinetNo : "";
                    RecordTemp.CardPCM = (technicalInfo != null) ? technicalInfo.PCMCard : "";
                    RecordTemp.Center = telephoneInfo.Center;
                    RecordTemp.CustomerName = telephoneInfo.CustomerName;
                    RecordTemp.EtesaliBucht = (technicalInfo != null) ? technicalInfo.ETESALII : "";
                    RecordTemp.EtesaliInputBucht = (technicalInfo != null) ? technicalInfo.PCMInEtesali : "";
                    RecordTemp.EtesaliOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutEtesali : "";
                    RecordTemp.MDFDate = Helper.GetPersianDate(_Failure117.MDFDate, Helper.DateStringType.Short);
                    RecordTemp.MDFUser = UserDB.GetUserFullName(_Failure117.MDFPesonnelID);
                    RecordTemp.ModelPCM = (technicalInfo != null) ? technicalInfo.PCMModel : "";
                    RecordTemp.PhoneNo = _Request.TelephoneNo.ToString();
                    RecordTemp.PortPCM = (technicalInfo != null) ? technicalInfo.PCMPort : "";
                    RecordTemp.PostalCode = (technicalInfo != null) ? telephoneInfo.PostalCode : "";
                    RecordTemp.PostEtesaliNo = (technicalInfo != null) ? technicalInfo.ConnectionNo : "";
                    RecordTemp.PostNo = (technicalInfo != null) ? technicalInfo.PostNo : "";
                    RecordTemp.RadifBucht = (technicalInfo != null) ? technicalInfo.RADIF : "";
                    RecordTemp.RadifInputBucht = (technicalInfo != null) ? technicalInfo.PCMInRadif : "";
                    RecordTemp.RadifOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutRadif : "";
                    //RecordTemp.RequestDate = RequestDateTextBox.Text;
                    RecordTemp.RequestDate1 = Helper.GetPersianDate(_Request.InsertDate, Helper.DateStringType.DateTime);
                    RecordTemp.RequestNo = requestID;
                    RecordTemp.RockPCM = (technicalInfo != null) ? technicalInfo.PCMRock : "";
                    RecordTemp.ShelfPCM = (technicalInfo != null) ? technicalInfo.PCMShelf : "";
                    RecordTemp.TabagheBucht = (technicalInfo != null) ? technicalInfo.TABAGHE : "";
                    RecordTemp.TabagheInputBucht = (technicalInfo != null) ? technicalInfo.PCMInTabaghe : "";
                    RecordTemp.TabagheOutputBucht = (technicalInfo != null) ? technicalInfo.PCMOutTabaghe : "";
                    RecordTemp.TypePCM = (technicalInfo != null) ? technicalInfo.PCMType : "";

                    Failure117LineStatus lineStatus = DB.SearchByPropertyName<Failure117LineStatus>("ID", (int)_Failure117.LineStatusID).SingleOrDefault();
                    RecordTemp.LineStatus = lineStatus.Title + "_" + Helper.GetEnumDescriptionByValue(typeof(DB.Failure117LineStatus), lineStatus.Type);
                    if (technicalInfo != null)
                        RecordTemp.IsPCM = (technicalInfo.IsPCM) ? "دارد" : "ندارد";
                    else
                        RecordTemp.IsPCM = "";
                    RecordTemp.ColorCable = "";// _FormInfo.CableColor1 + " - " + Color2ComboBox.Text;
                    RecordTemp.CableType = "";// _FormInfo.CableType;
                    RecordTemp.Description = _Failure117.NetworkComment;
                    RecordTemp.AdjacentTelephoneNo = _Failure117.AdjacentTelephoneNo.ToString();
                    RecordTemp.EndMDFComment = _Failure117.MDFCommnet;
                    RecordTemp.SendToCabelDate = Helper.GetPersianDate(_FormInfo.SendToCabelDate, Helper.DateStringType.Short);
                    RecordTemp.CabelDate = Helper.GetPersianDate(_Failure117.CableDate, Helper.DateStringType.Short);

                    double compareResult = ((DateTime)_Failure117.MDFDate - _Request.InsertDate).TotalMinutes;
                    double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
                    double min = Math.Round(compareResult % 60, 2);
                    RecordTemp.MDFSpeedMin = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());

                    RecordTemp.NetworkOfficer = "";// _FormInfo.Failure117NetworkContractorOfficerID;
                    RecordTemp.GiveNetworkFormDate = Helper.GetPersianDate(_FormInfo.GiveNetworkFormDate, Helper.DateStringType.Short);
                    RecordTemp.GiveNetworkFormTime = _FormInfo.GiveNetworkFormTime;
                    RecordTemp.GetNetworkFormDate = Helper.GetPersianDate(_FormInfo.GetNetworkFormDate, Helper.DateStringType.Short);
                    RecordTemp.GetNetworkFormTime = _FormInfo.GetNetworkFormTime;
                    if (aDSLInfo != null)
                    {
                        RecordTemp.ADSLRadif = aDSLInfo.ADSLRadif;
                        RecordTemp.ADSLTabaghe = aDSLInfo.ADSLTabaghe;
                        RecordTemp.ADSLEtesali = aDSLInfo.ADSLEtesali;
                    }
                    List<FailureHistoryInfo> historyList = new List<FailureHistoryInfo>();
                    historyList = Failure117DB.SearchFailureHistory(_Request.ID, (long)_Request.TelephoneNo);
                    historyList = historyList.OrderByDescending(t => t.ID).Take(2).ToList();
                    RecordTemp.Rowno = Failure117DB.GetFailureFormInfo(requestID).RowNo.ToString();
                    historyListTemp.AddRange(historyList);

                    result.Add(RecordTemp);
                }

                string title = string.Empty;
                string path;

                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath((int)DB.UserControlNames.Failure117Network);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

                //stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("xx", "xx", result);
                stiReport.RegData("result", "historyList", historyListTemp);
                stiReport.CacheAllData = true;

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        private void AddressContextMenu_Click(object sender, RoutedEventArgs e)
        {
            RequestInfo requestInfo = ItemsDataGrid.SelectedItem as Data.RequestInfo;
            if (requestInfo == null) return;

            if (DB.IsFixRequest(RequestTypeID))
            {
                long addressID = DB.GetRequestAddress(requestInfo.ID, requestInfo.RequestTypeID);
                if (addressID == -1)
                    MessageBox.Show("این روال آدرس ندارد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                {
                    CustomerAddressForm address = new CustomerAddressForm(addressID);
                    address.IsEnabled = false;
                    address.Show();

                }
            }
        }



        #endregion

        #region print

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;
            int totalRecords = default(int);
            DataSet data = Data.RequestDB.SearchRequests(IDTextBox.Text.Trim(), TelephoneNoTextBox.Text.Trim(), RequestStartDate.SelectedDate, RequestEndDate.SelectedDate, ModifyStartDate.SelectedDate, ModifyEndDate.SelectedDate, RequestTypesComboBox.SelectedIDs, CentersComboBox.SelectedIDs, CustomerNameTextBox.Text, RequesterNameTextBox.Text, PaymentTypesComboBox.SelectedIDs, StepsComboBox.SelectedIDs, RequestLetterNoTextBox.Text, LetterDate.SelectedDate, IsInquiryMode, IsArchived, RequestRejectReasonDescriptionTextBox.Text.Trim(), out totalRecords, null, pageSize, startRowIndex).ToDataSet("Result", ItemsDataGrid);
            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);


            this.Cursor = Cursors.Arrow;

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

        private void PrintClick(object sender, RoutedEventArgs e)
        {
            List<long> RequestIDs = new List<long>();
            List<RequestInfo> RequestList = new List<RequestInfo>();
            List<ConnectionInfo> ResultTemp = new List<ConnectionInfo>();
            List<AssignmentInfo> ResultTemp_PCM = new List<AssignmentInfo>();
            List<BuchtNoInfo> BuchtnoInoCentral = new List<BuchtNoInfo>();
            List<BuchtNoInfo> TempBuchtNo = new List<BuchtNoInfo>();

            DateTime currentDateTime = DB.GetServerDate();
            StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", DB.GetServerDate().ToPersian(Date.DateStringType.Short));
            StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time));
            StiVariable cityVariable = new StiVariable("City", "City", DB.PersianCity);

            long RequestID;

            if (IsInquiryMode == true) return;
            if (IsArchived == true) return;

            this.Cursor = Cursors.Wait;

            RequestIDs = ItemsDataGrid.SelectedItems.Cast<RequestInfo>().Where(t => t.ID != null).Select(t => t.ID).ToList();
            RequestList = ItemsDataGrid.SelectedItems.Cast<RequestInfo>().Where(t => t.ID != null).ToList();


            int? reportTemplateId = ReportTemplateDB.GetReportTemplateIdByRequestStepId(SelectedStepID);


            switch (RequestTypeID)
            {

                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    if (reportTemplateId != null)
                    {
                        string HeaderTitle = string.Empty;
                        IEnumerable result = GetReportByRequestIDAndStep(RequestIDs, reportTemplateId, ref HeaderTitle);
                        ShowReport(result, string.Empty, reportTemplateId ?? 0);
                    }
                    else
                    {
                    }
                    break;

                case (int)DB.RequestType.E1:
                    try
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.MDFE1DayeriReport)
                        {
                            IEnumerable result = ReportDB.GetMDFE1Dayeri(RequestIDs);
                            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.MDFE1DayeriReport, dateVariable, timeVariable);
                        }
                        if (reportTemplateId == (int)DB.UserControlNames.VisitAddressReport)
                        {
                            IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                            ShowReport(result, string.Empty, reportTemplateId ?? 0);
                        }
                        if (reportTemplateId == (int)DB.UserControlNames.E1InvoiceIssuanceCertificate) //کواهی صدورصورتحساب دوماهه
                        {
                            RequestID = (RequestIDs.Count == 1) ? RequestIDs.FirstOrDefault() : -1;

                            //دیتای مورد نیاز برای چاپ
                            CustomerReportInfo customer = new CustomerReportInfo();
                            E1InvoiceIssuanceCertificateInfo e1 = ReportDB.SearchE1InvoiceByRequestID(out customer, RequestID);

                            List<E1InvoiceIssuanceCertificateInfo> e1s = new List<E1InvoiceIssuanceCertificateInfo> { e1 };
                            List<CustomerReportInfo> customers = new List<CustomerReportInfo> { customer };

                            Dictionary<string, IEnumerable> dictionary = new Dictionary<string, IEnumerable>();
                            dictionary.Add("customerInfo", customers);
                            dictionary.Add("e1Info", e1s);

                            //تنظیمات برای نمایش
                            ReportBase.SendToPrint((int)DB.UserControlNames.E1InvoiceIssuanceCertificate, dictionary, dateVariable);
                        }
                        else
                        {
                            IEnumerable result = ReportDB.GetE1NetworkWire(RequestIDs);
                            ShowReport(result, string.Empty, (int)DB.UserControlNames.E1WiringNetworkReport);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "خطا در چاپ - روال ایوان");
                        MessageBox.Show("خطا", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        try
                        {
                            if (reportTemplateId == (int)DB.UserControlNames.SpaceAndPowerInvoiceIssuanceCertificate)
                            {
                                RequestID = (RequestIDs.Count == 1) ? RequestIDs.FirstOrDefault() : -1;
                                //دیتای مورد نیاز برای چاپ گزارش
                                CustomerReportInfo customer = new CustomerReportInfo();
                                SpaceAndPowerInvoiceIssuanceCertificateInfo spaceAndPower = ReportDB.SearchSpaceAndPowerInvoiceByRequestID(out customer, RequestID);

                                List<SpaceAndPowerInvoiceIssuanceCertificateInfo> spaceAndPowers = new List<SpaceAndPowerInvoiceIssuanceCertificateInfo>() { spaceAndPower };
                                List<CustomerReportInfo> customers = new List<CustomerReportInfo>() { customer };

                                Dictionary<string, IEnumerable> dictionary = new Dictionary<string, IEnumerable>();
                                dictionary.Add("customerInfo", customers);
                                dictionary.Add("spaceAndPowerInfo", spaceAndPowers);

                                //تنظیمات برای نمایش
                                ReportBase.SendToPrint((int)DB.UserControlNames.SpaceAndPowerInvoiceIssuanceCertificate, dictionary, dateVariable);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex, "خطا در چاپ - روال فضا و پاور");
                            MessageBox.Show("خطا", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    break;
                //case (int)DB.RequestType.E1Link:
                //    if (ReportTemplateID != null)
                //    {
                //        IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                //        ShowReport(result, string.Empty, ReportTemplateID ?? 0);
                //    }
                //    else
                //    {
                //        IEnumerable result = ReportDB.GetE1LinkNetworkWire(RequestIDs);
                //        ShowReport(result, string.Empty, ReportTemplateID ?? 0);
                //    }
                //    break;
                case (int)DB.RequestType.Dayri:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.VisitAddressReport)
                        {
                            IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                            ShowReport(result, string.Empty, reportTemplateId ?? 0);
                        }
                        else if (reportTemplateId == (int)DB.UserControlNames.DayeriWiringNetwork)
                        {   //TODO:rad
                            IEnumerable result = ReportDB.GetDayeriWiringNetwork(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.DayeriWiringNetwork, dateVariable, timeVariable);
                        }
                        else if (reportTemplateId == (int)DB.UserControlNames.DayeriMDFWiringReport)
                        {   //TODO:rad
                            IEnumerable result = ReportDB.GetDayeriMDFWiring(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.DayeriMDFWiringReport, dateVariable, timeVariable);
                        }
                        else
                        {
                            List<InvestigatePossibility> Result_Investigate = ReportDB.GetInvestigatePossibilityByRequestIDs(RequestIDs);
                            List<ConnectionInfo> Result = new List<ConnectionInfo>();
                            List<AssignmentInfo> DayeriPCMResult = new List<AssignmentInfo>();

                            foreach (InvestigatePossibility Info in Result_Investigate)
                            {
                                Result.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.BuchtID, Info.RequestID, null, null));
                            }
                            foreach (ConnectionInfo info in Result)
                            {
                                info.TelephoneNo = (RequestDB.GetTeleponeNoByRequestID(info.RequestID));
                                AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)info.TelephoneNo);

                                if (assignmentInfo != null)
                                {
                                    info.MUID = assignmentInfo.MUID;
                                    info.ADSLBucht = assignmentInfo.ADSLBucht;
                                }
                            }

                            foreach (ConnectionInfo Info in Result)
                            {
                                Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                                if (bucht != null)
                                {
                                    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                    if (postContact != null && bucht.PCMPortID != null && (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote))
                                    {
                                        IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                        IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                        foreach (AssignmentInfo info in AssignmentList)
                                        {
                                            DayeriPCMResult.Add(info);
                                        }

                                        foreach (BuchtNoInfo info in BuchtList)
                                        {
                                            BuchtnoInoCentral.Add(info);
                                        }

                                    }
                                }
                            }

                            SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(Result, DayeriPCMResult, BuchtnoInoCentral);
                        }
                    }
                    break;
                case (int)DB.RequestType.Dischargin:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.DischargeWiringNetworkReport)
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetDischargeWiringNetwork(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.DischargeWiringNetworkReport, dateVariable, timeVariable);
                        }
                        else if (reportTemplateId == (int)DB.UserControlNames.DischargeConfirmByMDFReport)
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetDischargeConfirmByMDF(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.DischargeConfirmByMDFReport, dateVariable, timeVariable);
                        }
                        else
                        {
                            List<TakePossession> takePossession = Data.TakePossessionDB.GetTakePossessionByIDs(RequestIDs);
                            //   List<AssignmentInfo> takePossessionResult = DB.GetAllInformationByBuchtIDs(takePossession.Select(t=>(long)t.BuchtID).ToList());
                            //   DB.GetAllInformationByTelephoneNo()
                            //List<TakePossession> takePossession = Data.TakePossessionDB.GetTakePossessionByIDs(RequestIDs);
                            List<ConnectionInfo> DischargeResult = new List<ConnectionInfo>();
                            List<AssignmentInfo> DischargePCMResult = new List<AssignmentInfo>();

                            foreach (TakePossession Info in takePossession)
                            {
                                DischargeResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.BuchtID, Info.ID, null, null));

                            }

                            foreach (ConnectionInfo info in DischargeResult)
                            {
                                info.TelephoneNo = (RequestDB.GetTeleponeNoByRequestID(info.RequestID));
                                AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)info.TelephoneNo);

                                if (assignmentInfo != null)
                                {
                                    info.MUID = assignmentInfo.MUID;
                                    info.ADSLBucht = assignmentInfo.ADSLBucht;
                                }


                            }
                            foreach (ConnectionInfo Info in DischargeResult)
                            {
                                Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                                if (bucht != null)
                                {
                                    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                    {
                                        IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                        IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                        foreach (AssignmentInfo info in AssignmentList)
                                        {
                                            DischargePCMResult.Add(info);
                                        }

                                        foreach (BuchtNoInfo info in BuchtList)
                                        {
                                            BuchtnoInoCentral.Add(info);
                                        }
                                    }
                                }
                            }

                            SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(DischargeResult, DischargePCMResult, BuchtnoInoCentral);
                            // SendToPrintDischargeMDFWiring(takePossessionResult);

                        }
                    }
                    break;

                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    //TODO:rad
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfSourceCenterReport)
                        {
                            IEnumerable result = ReportDB.GetChangeLocationCenterToCenterMdfWriringOfSourceCenter(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfSourceCenterReport, dateVariable, timeVariable);
                        }
                        else if (reportTemplateId == (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfTargetCenterReport)
                        {
                            IEnumerable result = ReportDB.GetChangeLocationCenterToCenterMdfWriringOfTargetCenter(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationCenterToCenterMdfWriringOfTargetCenterReport, dateVariable, timeVariable);
                        }
                        else
                        {
                            List<ChangeLocation> changeLocation = ChangeLocationDB.GetChangeLocationByRequestIDs(RequestIDs);
                            List<InvestigatePossibility> investigatePossibilites = InvestigatePossibilityDB.GetInvestigatePossibilityByRequestIDs(RequestIDs);
                            List<ConnectionInfo> ChangeLocationResult = new List<ConnectionInfo>();
                            List<AssignmentInfo> Result_PCM = new List<AssignmentInfo>();
                            List<AssignmentInfo> ResultCenterToCenter_PCM = new List<AssignmentInfo>();


                            //درخواست در مبدا
                            if (changeLocation[0].SourceCenter != null && changeLocation[0].SourceCenter == RequestList[0].CenterID)
                            {
                                foreach (ChangeLocation Info in changeLocation)
                                {
                                    ChangeLocationResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, (long)Info.OldBuchtID, -1, Info.ID, null, Info.OldTelephone));
                                    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)Info.OldTelephone);
                                    if (assignmentInfo != null)
                                    {
                                        ChangeLocationResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().MUID = assignmentInfo.MUID;
                                        ChangeLocationResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().ADSLBucht = assignmentInfo.ADSLBucht;
                                    }

                                }

                                foreach (ConnectionInfo Info in ChangeLocationResult)
                                {
                                    Info.TelephoneNo = Convert.ToInt64(Info.OldTelephoneNo);
                                    Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.OldBuchtID);

                                    if (bucht != null)
                                    {
                                        PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                        // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                        if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                        {
                                            IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                            IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                            foreach (AssignmentInfo info in AssignmentList)
                                            {
                                                Result_PCM.Add(info);
                                            }

                                            foreach (BuchtNoInfo info in BuchtList)
                                            {
                                                BuchtnoInoCentral.Add(info);
                                            }

                                        }
                                    }
                                }



                                SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(ChangeLocationResult, Result_PCM, BuchtnoInoCentral);
                            }



                                //درخواست در مقصد
                            else if (changeLocation[0].SourceCenter != null && changeLocation[0].TargetCenter == RequestList[0].CenterID)
                            {

                                foreach (ChangeLocation Info in changeLocation)
                                {
                                    ChangeLocationResult.Add(ReportDB.GetBuchtInfoByBuchtIDs((long)investigatePossibilites.Where(t3 => t3.RequestID == Info.ID).Take(1).SingleOrDefault().BuchtID, -1, -1, Info.ID, Info.NewTelephone, null));

                                }
                                foreach (ConnectionInfo Info in ChangeLocationResult)
                                {
                                    Info.TelephoneNo = Convert.ToInt64(Info.NewTelephoneNo);
                                    Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.NewBuchtID);
                                    if (bucht != null)
                                    {
                                        PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                        // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                        if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                        {

                                            IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                            IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                            foreach (AssignmentInfo info in AssignmentList)
                                            {
                                                ResultCenterToCenter_PCM.Add(info);
                                            }

                                            foreach (BuchtNoInfo info in BuchtList)
                                            {
                                                BuchtnoInoCentral.Add(info);
                                            }

                                        }
                                    }

                                }


                                SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(ChangeLocationResult, ResultCenterToCenter_PCM, BuchtnoInoCentral);
                            }
                        }
                        break;
                    }
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    //if (ReportTemplateID != null)
                    //{
                    //    IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                    //    ShowReport(result, string.Empty, ReportTemplateID ?? 0);
                    //}
                    //else
                    //{
                    // }
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport)
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetChangeLocationInsideCenterInfo(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport, dateVariable, timeVariable);
                        }
                        else if (reportTemplateId == (int)DB.UserControlNames.ChangeLocationInsideCenterMDFWiringReport)
                        {//TODO:rad
                            IEnumerable result = ReportDB.GetChangeLocationInsideCenterMDFWiringInfo(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeLocationInsideCenterMDFWiringReport, dateVariable, timeVariable);
                        }
                        else
                        {
                            List<ChangeLocation> changeLocationInside = ChangeLocationDB.GetChangeLocationByRequestIDs(RequestIDs);
                            List<InvestigatePossibility> investigatePossibilites = InvestigatePossibilityDB.GetInvestigatePossibilityByRequestIDs(RequestIDs);
                            List<ConnectionInfo> ChangeLocationInsideOldResult = new List<ConnectionInfo>();
                            List<ConnectionInfo> ChangeLocationInsideNewResult = new List<ConnectionInfo>();
                            List<ConnectionInfo> ChangeLocationInsideResult = new List<ConnectionInfo>();
                            List<AssignmentInfo> OldPCMResult = new List<AssignmentInfo>();
                            List<AssignmentInfo> NewPCMResult = new List<AssignmentInfo>();
                            List<BuchtNoInfo> OldBuchtnoInoCentral = new List<BuchtNoInfo>();
                            List<BuchtNoInfo> NewBuchtnoInoCentral = new List<BuchtNoInfo>();

                            foreach (ChangeLocation Info in changeLocationInside)
                            {
                                ChangeLocationInsideOldResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.OldBuchtID, Info.ID, null, Info.OldTelephone));
                                AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)Info.OldTelephone);
                                if (assignmentInfo != null)
                                {
                                    ChangeLocationInsideOldResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().MUID = assignmentInfo.MUID;
                                    ChangeLocationInsideOldResult.Where(t => t.OldTelephoneNo == Info.OldTelephone).Take(1).SingleOrDefault().ADSLBucht = assignmentInfo.ADSLBucht;
                                }
                            }

                            foreach (ChangeLocation Info in changeLocationInside)
                            {
                                ChangeLocationInsideNewResult.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)investigatePossibilites.Where(t3 => t3.RequestID == Info.ID).Take(1).SingleOrDefault().BuchtID, Info.ID, Info.NewTelephone, null));



                            }

                            for (int i = 0; i < ChangeLocationInsideOldResult.Count; i++)
                            {
                                for (int j = 0; j < ChangeLocationInsideNewResult.Count; j++)

                                    if (ChangeLocationInsideOldResult[i].RequestID == ChangeLocationInsideNewResult[j].RequestID)
                                    {
                                        ChangeLocationInsideResult.Add(ChangeLocationInsideOldResult[i]);
                                        ChangeLocationInsideResult[i].NewBuchtNo = ChangeLocationInsideNewResult[j].BuchtNo;
                                        ChangeLocationInsideResult[i].NewTelephoneNo = ChangeLocationInsideNewResult[j].NewTelephoneNo;
                                        ChangeLocationInsideResult[i].NewVerticalColumnNo = ChangeLocationInsideNewResult[j].VerticalColumnNo;
                                        ChangeLocationInsideResult[i].NewVerticalRowNo = ChangeLocationInsideNewResult[j].VerticalRowNo;
                                        ChangeLocationInsideResult[i].NewMDF = ChangeLocationInsideNewResult[j].MDF;
                                        ChangeLocationInsideResult[i].NewBuchtID = ChangeLocationInsideNewResult[j].BuchtID;
                                        ChangeLocationInsideResult[i].NewCabinetInputID = ChangeLocationInsideNewResult[j].CabinetInputID;

                                        ChangeLocationInsideResult[i].OldBuchtNo = ChangeLocationInsideOldResult[j].BuchtNo;
                                        ChangeLocationInsideResult[i].OldTelephoneNo = ChangeLocationInsideOldResult[j].OldTelephoneNo;
                                        ChangeLocationInsideResult[i].OldVerticalColumnNo = ChangeLocationInsideOldResult[j].VerticalColumnNo;
                                        ChangeLocationInsideResult[i].OldVerticalRowNo = ChangeLocationInsideOldResult[j].VerticalRowNo;
                                        ChangeLocationInsideResult[i].OldMDF = ChangeLocationInsideOldResult[j].MDF;
                                        ChangeLocationInsideResult[i].OldBuchtID = ChangeLocationInsideNewResult[j].BuchtID;
                                        ChangeLocationInsideResult[i].OldCabinetInputID = ChangeLocationInsideNewResult[j].CabinetInputID;
                                    }
                            }

                            foreach (ConnectionInfo Info in ChangeLocationInsideResult)
                            {
                                Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.OldBuchtID);
                                if (bucht != null)
                                {
                                    PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                    {

                                        IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                        IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                        foreach (AssignmentInfo info in AssignmentList)
                                        {
                                            OldPCMResult.Add(info);
                                        }

                                        foreach (BuchtNoInfo info in BuchtList)
                                        {
                                            OldBuchtnoInoCentral.Add(info);
                                        }

                                    }
                                }

                                Bucht Newbucht = Data.BuchtDB.GetBuchtByID((long)Info.NewBuchtID);
                                if (bucht != null)
                                {
                                    PostContact postContact = Data.PostContactDB.GetPostContactByID(Newbucht.ConnectionID ?? 0);
                                    // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                    if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                    {

                                        IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                        IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                        foreach (AssignmentInfo info in AssignmentList)
                                        {
                                            NewPCMResult.Add(info);
                                        }

                                        foreach (BuchtNoInfo info in BuchtList)
                                        {
                                            NewBuchtnoInoCentral.Add(info);
                                        }
                                    }
                                }

                            }
                            SendToPrintChangeLocationCenterInsideMDFWiring(ChangeLocationInsideResult, NewPCMResult, OldPCMResult, NewBuchtnoInoCentral, OldBuchtnoInoCentral);
                        }
                    }
                    break;

                case (int)DB.RequestType.Reinstall:

                    List<InvestigatePossibility> ResultReInstall_Investigate = ReportDB.GetInvestigatePossibilityByRequestIDs(RequestIDs);
                    List<ConnectionInfo> ResultReInstall = new List<ConnectionInfo>();
                    List<AssignmentInfo> ReInsatllPCMResult = new List<AssignmentInfo>();

                    foreach (InvestigatePossibility Info in ResultReInstall_Investigate)
                    {
                        ResultReInstall.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, -1, (long)Info.BuchtID, Info.RequestID, null, null));
                    }
                    foreach (ConnectionInfo info in ResultReInstall)
                    {
                        info.TelephoneNo = (RequestDB.GetTeleponeNoByRequestID(info.RequestID));
                    }

                    foreach (ConnectionInfo Info in ResultReInstall)
                    {
                        Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                        if (bucht != null)
                        {
                            PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                            // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                            if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                            {

                                IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                foreach (AssignmentInfo info in AssignmentList)
                                {
                                    ReInsatllPCMResult.Add(info);
                                }

                                foreach (BuchtNoInfo info in BuchtList)
                                {
                                    BuchtnoInoCentral.Add(info);
                                }
                            }
                        }
                    }
                    SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(ResultReInstall, ReInsatllPCMResult, BuchtnoInoCentral);
                    break;


                case (int)DB.RequestType.ChangeNo:
                    if (reportTemplateId == (int)DB.UserControlNames.ChangeNoMDFWiringReport)
                    {
                        //TODO:rad
                        IEnumerable result = ReportDB.GetChangeNoMDFWiring(RequestIDs);
                        CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.ChangeNoMDFWiringReport, dateVariable, timeVariable);
                    }
                    else
                    {
                        List<ChangeNo> changeNo = ChangeNoDB.GetChangeNoDBByIDs(RequestIDs);
                        List<ConnectionInfo> ResultChangeNo = new List<ConnectionInfo>();
                        List<AssignmentInfo> ResultPCMChangeNO = new List<AssignmentInfo>();

                        foreach (ChangeNo Info in changeNo)
                        {
                            ResultChangeNo.Add(ReportDB.GetBuchtInfoByBuchtIDs(-1, (long)Info.OldBuchtID, -1, Info.ID, Info.NewTelephoneNo, Info.OldTelephoneNo));
                            AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo((long)Info.OldTelephoneNo);
                            if (assignmentInfo != null)
                            {
                                ResultChangeNo.Where(t => t.OldTelephoneNo == Info.OldTelephoneNo).Take(1).SingleOrDefault().MUID = assignmentInfo.MUID;
                                ResultChangeNo.Where(t => t.OldTelephoneNo == Info.OldTelephoneNo).Take(1).SingleOrDefault().ADSLBucht = assignmentInfo.ADSLBucht;
                            }

                        }
                        foreach (ConnectionInfo Info in ResultChangeNo)
                        {
                            Bucht bucht = Data.BuchtDB.GetBuchtByID((long)Info.BuchtID);
                            if (bucht != null)
                            {
                                PostContact postContact = Data.PostContactDB.GetPostContactByID(bucht.ConnectionID ?? 0);
                                // اگر اتصالی انتخاب شده متعلق به پی سی ام باشد اطلاعات پی سی ام را نمایش می دهد
                                if (postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMNormal || postContact.ConnectionType == (int)DB.PostContactConnectionType.PCMRemote)
                                {
                                    IEnumerable AssignmentList = LoadPCMInfo(bucht);
                                    IEnumerable BuchtList = GetBuchtNoCentralInfo(bucht);
                                    foreach (AssignmentInfo info in AssignmentList)
                                    {
                                        ResultPCMChangeNO.Add(info);
                                    }

                                    foreach (BuchtNoInfo info in BuchtList)
                                    {
                                        BuchtnoInoCentral.Add(info);
                                    }
                                }
                            }
                        }

                        SendToPrintChangeNoMDFWiring(ResultChangeNo, ResultPCMChangeNO, BuchtnoInoCentral);
                    }
                    break;

                case (int)DB.RequestType.E1Link:
                    if (reportTemplateId != null)
                    {
                        IEnumerable result = ReportDB.GetVisitTheSiteByRequestID(RequestIDs);
                        ShowReport(result, string.Empty, reportTemplateId ?? 0);
                    }
                    else
                    {
                        List<E1LinkReportInfo> ResultE1LinkDayeri = new List<E1LinkReportInfo>();
                        ResultE1LinkDayeri = ReportDB.GetMDFE1Dayeri(RequestIDs);

                        foreach (E1LinkReportInfo info in ResultE1LinkDayeri)
                        {
                            info.TelephoneNo = (RequestDB.GetTeleponeNoByRequestID(info.RequestID)).ToString();
                        }

                        SendToPrintMDFE1Dayeri(ResultE1LinkDayeri);
                    }
                    break;

                case (int)DB.RequestType.CenterToCenterTranslation:
                    ObservableCollection<CenterToCenterTranslationChooseNumberInfo> telphonNumbers;
                    ObservableCollection<CenterToCenterTranslationChooseNumberInfo> ResultCenterToCenterTranslationDayeri;
                    ObservableCollection<CenterToCenterTranslationChooseNumberInfo> ResultCenterToCenterTranslationDischarge;
                    ObservableCollection<CheckableItem> _preCodes = new ObservableCollection<CheckableItem>();
                    Request request = new Request();

                    //RequestInfo SelectedRequestInfo = ItemsDataGrid.SelectedItems as RequestInfo;
                    RequestID = RequestIDs[0];

                    CenterToCenterTranslation _centerToCenterTranslation = new CenterToCenterTranslation();
                    _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(RequestID);

                    request = Data.RequestDB.GetRequestByID(RequestID);
                    Status StatusPlace = StatusDB.GetStatusByID(request.StatusID);
                    RequestStep requestStep = RequestStepDB.GetRequestStepByID(StatusPlace.RequestStepID);

                    if (requestStep.StepTitle.Contains("سالن سوئیچ -دایری"))
                    {
                        telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                        _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

                        List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(RequestID).ToList();
                        centerToCenterTranslationTelephones.ForEach(item =>
                        {
                            if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                            {
                                telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                                telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID = item.NewSwitchPrecodeID;
                                telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().Name));
                            }
                        }
                                                                       );

                        ResultCenterToCenterTranslationDayeri = telphonNumbers;

                        SendToPrintCenterToCenterTranslation_SwitchDayeri(ResultCenterToCenterTranslationDayeri);
                    }

                    if (requestStep.StepTitle.Contains("سالن سوئیچ - تخلیه"))
                    {
                        telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                        _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.SourceCenterID));

                        List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(RequestID).ToList();
                        centerToCenterTranslationTelephones.ForEach(item =>
                        {
                            if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                            {
                                telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().TelephonNo = item.TelephoneNo;
                                telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().OldPreCodeNumber = TelephoneDB.GetSwitchPreCodeNumberTelephoneByTelephoneNo(item.TelephoneNo);
                            }
                        });

                        ResultCenterToCenterTranslationDischarge = telphonNumbers;

                        SendToPrintCenterToCenterTranslation_SwitchDischarge(ResultCenterToCenterTranslationDischarge);
                    }
                    break;
                case (int)DB.RequestType.TranslationOpticalCabinetToNormal:
                    try
                    {
                        //TODO:rad
                        if (reportTemplateId == (int)DB.UserControlNames.TranslationOpticalCabinetToNormallMDFWiringReport)
                        {
                            var result = TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionInfoSummariesByRequestIDs(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.TranslationOpticalCabinetToNormallMDFWiringReport, true, dateVariable, timeVariable, cityVariable);
                        }
                        else if (reportTemplateId != null)
                        {
                            var result = ReportDB.GetTranslationOpticalCabinetToNormalReport(RequestIDs);
                            ReportBase.SendToPrint(result, reportTemplateId ?? 0, dateVariable, timeVariable);
                        }
                        else
                        {
                            ObservableCollection<TranslationOpticalCabinetToNormalInfo> TranslationOpticalCabinetToNormalInfo = new ObservableCollection<TranslationOpticalCabinetToNormalInfo>();
                            TranslationOpticalCabinetToNormalInfo = new ObservableCollection<Data.TranslationOpticalCabinetToNormalInfo>(Data.TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionInfoByRequestIDs(RequestIDs));
                            SendToPrintTranslationOpticalCabinetToNormallNetworkWiringReport(TranslationOpticalCabinetToNormalInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "خطا درچاپ - برگردان کافو نوری به کافو معمولی در لیست درخواستها ");
                        MessageBox.Show("خطا در چاپ", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    if (reportTemplateId != null)
                    {
                        string HeaderTitle = string.Empty;
                        IEnumerable res = GetReportByRequestIDAndStep(RequestIDs, reportTemplateId, ref HeaderTitle);
                        ShowReport(res, HeaderTitle, reportTemplateId ?? 0);
                    }
                    else
                    {
                        ObservableCollection<SpecialWireReportInfo> specialWireReportInfo = new ObservableCollection<SpecialWireReportInfo>();
                        RequestIDs.ForEach(t =>
                        {
                            MDFWiringForm MDFWiringForm = new MDFWiringForm(t, null);
                            try
                            {
                                MDFWiringForm.LoadData();
                            }
                            catch (Exception ex)
                            { }
                            specialWireReportInfo.Add(MDFWiringForm._SpecialWireReportInfo);
                        }
                         );


                        SendToPrintSpecialWireReport(specialWireReportInfo);
                    }

                    break;
                case (int)DB.RequestType.VacateSpecialWire:
                    if (reportTemplateId != null)
                    {
                        string HeaderTitle = string.Empty;
                        IEnumerable res = GetReportByRequestIDAndStep(RequestIDs, reportTemplateId, ref HeaderTitle);
                        ShowReport(res, HeaderTitle, reportTemplateId ?? 0);
                    }


                    break;



                case (int)DB.RequestType.PCMToNormal:

                    if (SelectedStepID == 2385)
                    {
                        ObservableCollection<TranslationPCMToNormalMDFReportInfo> translationPCMToNormalMDFReportInfo = new ObservableCollection<TranslationPCMToNormalMDFReportInfo>();
                        RequestIDs.ForEach(t =>
                        {
                            TranslationPCMToNormalMDFForm translationPCMToNormalMDFForm = new TranslationPCMToNormalMDFForm(t);
                            try
                            {

                                translationPCMToNormalMDFForm.LoadData();
                            }
                            catch
                            { }
                            translationPCMToNormalMDFReportInfo.Add(translationPCMToNormalMDFForm.translationPCMToNormalMDFReportInfo);

                        }
                        );


                        SendToPrintTranslationPCMToNormallMDFReport(translationPCMToNormalMDFReportInfo);
                    }
                    else if (SelectedStepID == 2386)
                    {
                        ObservableCollection<TranslationPCMToNormalNetworkReportInfo> translationPCMToNormalNetworkReportInfo = new ObservableCollection<TranslationPCMToNormalNetworkReportInfo>();
                        RequestIDs.ForEach(t =>
                        {
                            TranslationPCMToNormalNetworkForm translationPCMToNormalNetworkForm = new TranslationPCMToNormalNetworkForm(t);
                            try
                            {

                                translationPCMToNormalNetworkForm.LoadData();
                            }
                            catch
                            { }
                            translationPCMToNormalNetworkReportInfo.Add(translationPCMToNormalNetworkForm.translationPCMToNormalNetworkReportInfo);

                        }
                        );

                        SendToPrintTranslationPCMToNormallNetworkReport(translationPCMToNormalNetworkReportInfo);
                    }

                    break;
                case (byte)DB.RequestType.Failure117:

                    List<Failure117RequestPrintInfo> FailureRequestLists = new List<Failure117RequestPrintInfo>();
                    Failure117RequestPrintInfo failureRequestInfo = null;

                    foreach (RequestInfo requestInfo in ItemsDataGrid.SelectedItems)
                    {
                        failureRequestInfo = Failure117DB.GetFailure117RequestPrintbyTelephoneNos((long)requestInfo.TelephoneNo);

                        if (failureRequestInfo == null)
                            failureRequestInfo = new Failure117RequestPrintInfo();

                        failureRequestInfo.ID = requestInfo.ID.ToString();
                        failureRequestInfo.TelephoneNo = requestInfo.TelephoneNo.ToString();
                        failureRequestInfo.Date = requestInfo.ModifyDate;

                        FailureRequestLists.Add(failureRequestInfo);
                    }

                    SendToPrintFailure117RequestInfo(FailureRequestLists);

                    break;

                case (byte)DB.RequestType.ADSLInstalPAPCompany:
                case (byte)DB.RequestType.ADSLDischargePAPCompany:

                    List<PAPRequestPrintInfo> pAPRequestLists = new List<PAPRequestPrintInfo>();
                    PAPRequestPrintInfo pAPRequestInfo = null;

                    foreach (RequestInfo requestInfo in ItemsDataGrid.SelectedItems)
                    {
                        pAPRequestInfo = ADSLPAPRequestDB.GetPAPRequestPrintbyTelephoneNos((long)requestInfo.TelephoneNo, requestInfo.RequestTypeID);

                        if (pAPRequestInfo == null)
                            pAPRequestInfo = new PAPRequestPrintInfo();

                        pAPRequestInfo.Bucht = ADSLPAPRequestDB.GetTechnicalInfobyTelephoneNo((long)requestInfo.TelephoneNo);
                        pAPRequestLists.Add(pAPRequestInfo);
                    }

                    SendToPrintPAPRequestInfo(pAPRequestLists);
                    break;

                case (byte)DB.RequestType.ADSLExchangePAPCompany:

                    List<PAPRequestPrintInfo> pAPRequesExchangetLists = new List<PAPRequestPrintInfo>();
                    PAPRequestPrintInfo pAPRequestExchangeInfo = null;

                    foreach (RequestInfo requestInfo in ItemsDataGrid.SelectedItems)
                    {
                        pAPRequestExchangeInfo = ADSLPAPRequestDB.GetPAPRequestPrintbyTelephoneNos((long)requestInfo.TelephoneNo, requestInfo.RequestTypeID);

                        if (pAPRequestExchangeInfo == null)
                            pAPRequestExchangeInfo = new PAPRequestPrintInfo();

                        pAPRequestExchangeInfo.Bucht = ADSLPAPRequestDB.GetTechnicalInfobyTelephoneNo((long)requestInfo.TelephoneNo);
                        pAPRequesExchangetLists.Add(pAPRequestExchangeInfo);
                    }

                    SendToPrintPAPRequestExchangeInfo(pAPRequesExchangetLists);
                    break;
                case (byte)DB.RequestType.BuchtSwiching:
                    {//TODO:rad
                        if (reportTemplateId == (int)DB.UserControlNames.BuchtSwitchingMDF)
                        {
                            IEnumerable result = ReportDB.GetBuchtSwitchingMDF(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.BuchtSwitchingMDF, dateVariable, timeVariable);
                        }
                        break;
                    }
                case (byte)DB.RequestType.SwapTelephone:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.SwapTelephoneMDFWiringReport)
                        {
                            //TODO:rad
                            ObservableCollection<SwapTelephoneRequestLog> result = new ObservableCollection<SwapTelephoneRequestLog>();
                            RequestIDs.ForEach(reqId =>
                                                    {
                                                        SwapTelephoneMDFForm form = new SwapTelephoneMDFForm(reqId);
                                                        try
                                                        {
                                                            form.LoadData();
                                                            result.Add(form.SwapTelephoneRequestLogReportInfo);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Write(ex, "خطا در چاپ گروهی - RequestsInbox - SwapTelephone");
                                                        }
                                                    }
                                              );
                            ReportBase.SendToPrint(result.ToList(), (int)DB.UserControlNames.SwapTelephoneMDFWiringReport, true, dateVariable, timeVariable, cityVariable);
                        }
                        break;
                    }
                case (byte)DB.RequestType.SwapPCM:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.SwapPCMMDFWiringReport)
                        {
                            //TODO:rad
                            ObservableCollection<SwapPCMRequestLog> result = new ObservableCollection<SwapPCMRequestLog>();
                            RequestIDs.ForEach(reqId =>
                                                    {
                                                        SwapPCMMDFFrom form = new SwapPCMMDFFrom(reqId);
                                                        try
                                                        {
                                                            form.LoadData();
                                                            result.Add(form.SwapPCMRequestLogReportInfo);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Logger.Write(ex, "خطا در چاپ گروهی - RequestsInbox - SwapPCM");
                                                        }
                                                    }
                                              );
                            ReportBase.SendToPrint(result.ToList(), (int)DB.UserControlNames.SwapPCMMDFWiringReport, true, dateVariable, timeVariable, cityVariable);
                        }
                        break;
                    }
                case (byte)DB.RequestType.ExchangeCabinetInput:
                    try
                    {

                        switch (SelectedStepID)
                        {
                            case (byte)DB.RequestStep.WiringOfExchangeInputCabinet:
                                ReportBase.SendToPrint(ReportDB.GetMDFExchangeCabinuteInput(RequestIDs), reportTemplateId.Value, dateVariable, timeVariable);
                                break;
                            case (byte)DB.RequestStep.EndOfExchangeInputCabinet:
                                ReportBase.SendToPrint(ReportDB.GetNetworkWireExchangeCabinuteInput(RequestIDs), reportTemplateId.Value, dateVariable, timeVariable);
                                break;
                        }



                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "RequestsInbox");
                        MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;

                case (byte)DB.RequestType.ExchangePost:
                    try
                    {
                        var result = ReportDB.GetTranslationPost(RequestIDs);
                        ReportBase.SendToPrint(result, reportTemplateId.Value, dateVariable, timeVariable);

                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "RequestsInbox");
                        MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;

                case (byte)DB.RequestType.TranlationPostInput:
                    try
                    {
                        if (reportTemplateId != null)
                        {
                            string HeaderTitle = string.Empty;
                            IEnumerable result = GetReportByRequestIDAndStep(RequestIDs, reportTemplateId, ref HeaderTitle);
                            ShowReport(result, string.Empty, reportTemplateId ?? 0);
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(ex, "RequestsInbox");
                        MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case (byte)DB.RequestType.CutAndEstablish:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.CuttedTelephonesInformationReport)
                        {
                            var result = ReportDB.GetCuttedTelephoneInfosByRequestsID(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.CuttedTelephonesInformationReport, true, dateVariable, timeVariable, cityVariable);
                        }
                    }
                    break;
                case (byte)DB.RequestType.Connect:
                    {
                        if (reportTemplateId == (int)DB.UserControlNames.EstablishedTelephoneInformation)
                        {
                            var result = ReportDB.GetEstablishedTelephoneInfosByRequestsID(RequestIDs);
                            ReportBase.SendToPrint(result, (int)DB.UserControlNames.EstablishedTelephoneInformation, true, dateVariable, timeVariable, cityVariable);
                        }
                    }
                    break;
            }

            this.Cursor = Cursors.Arrow;
        }

        private void SendToPrintSpecialWireReport(ObservableCollection<SpecialWireReportInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.SpecialWirelMDFReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintTranslationPCMToNormallNetworkReport(ObservableCollection<TranslationPCMToNormalNetworkReportInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationPCMToNormallNetworkReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintTranslationPCMToNormallMDFReport(ObservableCollection<TranslationPCMToNormalMDFReportInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            DateTime currentDateTime = DB.GetServerDate();
            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationPCMToNormallMDFReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short);
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time);
            stiReport.Dictionary.Variables["CityName"].Value = DB.PersianCity;
            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintDayeriDischargeChangeLocationCenterTocenterReInstallMDFWiring(IEnumerable result, List<AssignmentInfo> ResultPCM, List<BuchtNoInfo> BuchtNoInfoResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();



            string path = ReportDB.GetReportPath((int)DB.UserControlNames.DayeriDischargeChangeLocationCenterTocenterReInstallMDFReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            //if (ResultPCM.Count != 0)
            //stiReport.RegData("ResultPCM", "ResultPCM", ResultPCM);

            stiReport.RegData("BuchtNoInfoResult", "BuchtNoInfoResult", BuchtNoInfoResult);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintChangeLocationCenterInsideMDFWiring(IEnumerable ChangeLocationCenterInsideresult, List<AssignmentInfo> NewResultPCM, List<AssignmentInfo> OldPCMResult, List<BuchtNoInfo> NewBuchtNoInfoResult, List<BuchtNoInfo> OldBuchtNoInfoResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeLocationcenterTocenterMDFWiringReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();



            stiReport.CacheAllData = true;
            stiReport.RegData("ChangeLocationCenterInsideresult", "ChangeLocationCenterInsideresult", ChangeLocationCenterInsideresult);

            //if (NewResultPCM.Count != 0)
            //stiReport.RegData("NewResultPCM", "NewResultPCM", NewResultPCM);

            //if (OldPCMResult.Count != 0)
            //stiReport.RegData("OldPCMResult", "OldPCMResult", OldPCMResult);

            stiReport.RegData("NewBuchtNoInfoResult", "NewBuchtNoInfoResult", NewBuchtNoInfoResult);
            stiReport.RegData("OldBuchtNoInfoResult", "OldBuchtNoInfoResult", OldBuchtNoInfoResult);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintChangeNoMDFWiring(IEnumerable ResultChangeNo, List<AssignmentInfo> ResultPCM, List<BuchtNoInfo> BuchtNoInfoResult)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.ChangeNoMDFWiringReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

            stiReport.CacheAllData = true; stiReport.RegData("ResultChangeNo", "ResultChangeNo", ResultChangeNo);

            //if (ResultPCM.Count != 0)
            //stiReport.RegData("ResultPCM", "ResultPCM", ResultPCM);

            stiReport.RegData("BuchtNoInfoResult", "BuchtNoInfoResult", BuchtNoInfoResult);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintMDFE1Dayeri(List<E1LinkReportInfo> ResultE1LinkDayeri)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.MDFE1DayeriReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", ResultE1LinkDayeri);



            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintCenterToCenterTranslation_SwitchDayeri(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> ResultDayeri)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationSwitchDayeri);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

            stiReport.CacheAllData = true; stiReport.RegData("ResultDayeri", "ResultDayeri", ResultDayeri);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintCenterToCenterTranslation_SwitchDischarge(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> ResultDischarge)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();


            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationSwitchDischarge);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("ResultDischarge", "ResultDischarge", ResultDischarge);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintTranslationOpticalCabinetToNormallNetworkWiringReport(ObservableCollection<TranslationOpticalCabinetToNormalInfo> Result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            DateTime currentDateTime = DB.GetServerDate();



            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationOpticalCabinetToNormallNetwrokWiringReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintFailure117RequestInfo(List<Failure117RequestPrintInfo> failureList)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.Failure117RequestPrint);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.Dictionary.Variables["PrintDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime);
            stiReport.RegData("result", "result", failureList);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintPAPRequestInfo(List<PAPRequestPrintInfo> pAPRequestLists)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.PAPRequestPrint);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.Dictionary.Variables["PrintDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime);
            stiReport.Dictionary.Variables["RequesType"].Value = pAPRequestLists[0].RequestType;
            stiReport.RegData("result", "result", pAPRequestLists);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintPAPRequestExchangeInfo(List<PAPRequestPrintInfo> pAPRequestLists)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.PAPRequestExchangePrint);
            stiReport.Load(path);

            stiReport.CacheAllData = true;
            stiReport.Dictionary.Variables["PrintDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime);
            stiReport.Dictionary.Variables["RequesType"].Value = pAPRequestLists[0].RequestType;
            stiReport.RegData("result", "result", pAPRequestLists);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion print

        private void RequestTypesComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            StepsComboBox.ItemsSource = Data.WorkFlowDB.GetRequestStepsCheckable(RequestTypesComboBox.SelectedIDs);
        }
    }
}
