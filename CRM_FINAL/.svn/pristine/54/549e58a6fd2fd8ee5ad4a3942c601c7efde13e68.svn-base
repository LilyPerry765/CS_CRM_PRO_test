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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using CRM.Data;
using System.Transactions;
using Folder.Printing;
using System.Windows.Xps.Packaging;
using System.IO;
using System.Windows.Xps;
using System.Windows.Markup;
using System.Xml;
using System.Printing;
using CRM.Application.Views;
using System.Collections;
using Stimulsoft.Report.Wpf.XamlImages.ToolBox;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.UserControls
{
    public partial class Actions : UserControl
    {
        #region Properties

        private Data.Schema.ActionLogRequest actionLogRequest = new Data.Schema.ActionLogRequest();

        private Local.RequestFormBase _CurrentRequestWindow;
        private Local.TabWindow _CurrentTabWindow;

        private string _UserName;

        public List<byte> ActionIDs { get; set; }
        public List<long> ItemIDs { get; set; }
        public byte _ListType { get; set; }

        #endregion

        #region Constructors

        public Actions()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            ItemIDs = new List<long>();

            _CurrentRequestWindow = Helper.FindVisualParent<Local.RequestFormBase>(this);
            _CurrentTabWindow = Helper.FindVisualParent<Local.TabWindow>(this);

            if (_CurrentRequestWindow != null)
                ActionComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.NewAction)).Where(t => Helper.FindVisualParent<Local.RequestFormBase>(this).ActionIDs.Contains(t.ID));

            if (_CurrentTabWindow != null)
                ActionComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.NewAction)).Where(t => ActionIDs.Contains(t.ID));

            _UserName = Folder.User.Current.Username;
        }

        #endregion

        #region Event Handlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Initialize();
        }

        private void DoAction(object sender, SelectionChangedEventArgs e)
        {
            byte actionId = (byte)ActionComboBox.SelectedValue;

            switch (actionId)
            {
                case (byte)DB.NewAction.Save:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    SaveStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Delete:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    DeleteStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Print:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    PrintStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Confirm:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ConfirmStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Deny:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    DenyStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Forward:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ForwardStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Refund:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    RefundStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.ConfirmEnd:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ConfirmEndStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Cancelation:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    CancelationStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.SaveWaitingList:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    SaveWatingListStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.ExitWaitingList:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ExitWatingListStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.SaveBlackList:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    SaveBlackListStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.ExitBlackList:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ExitBlackListStackPanel.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.NewAction.Exit:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    ExitStackPanel.Visibility = Visibility.Visible;
                    break;
                case (byte)DB.NewAction.KickedBack:
                    ActionStackPanel.Visibility = Visibility.Collapsed;
                    KickedBackStackPanel.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            ActionComboBox.SelectedValue = 0;
            ActionStackPanel.Visibility = Visibility.Visible;
            ConfirmStackPanel.Visibility = Visibility.Collapsed;
            DenyStackPanel.Visibility = Visibility.Collapsed;
            SaveStackPanel.Visibility = Visibility.Collapsed;
            DeleteStackPanel.Visibility = Visibility.Collapsed;
            PrintStackPanel.Visibility = Visibility.Collapsed;
            ForwardStackPanel.Visibility = Visibility.Collapsed;
            RefundStackPanel.Visibility = Visibility.Collapsed;
            ConfirmEndStackPanel.Visibility = Visibility.Collapsed;
            CancelationStackPanel.Visibility = Visibility.Collapsed;
            SaveWatingListStackPanel.Visibility = Visibility.Collapsed;
            ExitWatingListStackPanel.Visibility = Visibility.Collapsed;
            SaveBlackListStackPanel.Visibility = Visibility.Collapsed;
            ExitBlackListStackPanel.Visibility = Visibility.Collapsed;
            ExitStackPanel.Visibility = Visibility.Collapsed;
            KickedBackStackPanel.Visibility = Visibility.Collapsed;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.Save())
                {
                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Save, _UserName, actionLogRequest);
                }
            }

            Reset(null, null);
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.Delete())
                {
                }
            }
            if (_CurrentTabWindow != null)
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("امکان حذف درخواست مورد نظر وجود ندارد", "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    //if (ItemIDs.Count != 0)
                    //{
                    //    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    //    if (result == MessageBoxResult.Yes)
                    //    {
                    //        foreach (long id in ItemIDs)
                    //        {
                    //            Request request = Data.RequestDB.GetRequestByID(id);
                    //            if ((request.StatusID == DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (request.CreatorUserID == DB.CurrentUser.ID))
                    //            {
                    //                DB.Delete<Data.Request>(request.ID);
                    //            }
                    //            else
                    //            {
                    //                throw new Exception("درخواست وارد چرخه کاری شده و امکان حذف آن وجود ندارد !");
                    //            }
                    //        }

                    //        _CurrentTabWindow.Load();
                    //        Views.RequestsInbox.ShowSuccessMessage("درخواست مورد نظر حذف شد.");
                    //    }
                    //}
                    //else
                    //{
                    //    throw new Exception("درخواستی برای حذف انتخاب نشده است !");
                    //}                    
                }
                catch (Exception ex)
                {
                    Views.RequestsInbox.ShowErrorMessage("خطا در حذف ، " + ex.Message + " !", ex);
                }
            }

            Reset(null, null);
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.Print())
                {
                    //actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    //actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    //ActionLogDB.AddActionLog((byte)DB.ActionLog.Save, _UserName, actionLogRequest);
                }
            }



            Reset(null, null);
            //if (_CurrentRequestWindow != null)
            //{
            //    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
            //    actionLogRequest.FormName = _CurrentRequestWindow.Title;
            //    ActionLogDB.AddActionLog((byte)DB.ActionLog.Print, _UserName, actionLogRequest);

            //    PrintSettings printSettings = PrintSettings.Default;

            //    UIElement container = _CurrentRequestWindow.Content as UIElement;


            //    ScrollViewer containerPanel = Helper.FindVisualChildren<ScrollViewer>(container).FirstOrDefault();



            //    List<System.Windows.Controls.Control> containerControl = Helper.FindVisualChildren<System.Windows.Controls.Control>(containerPanel).ToList();
            //    foreach (System.Windows.Controls.Control control in containerControl.Where(t => t is Expander || t is GroupBox))
            //     {

            //         if (control is Expander)
            //         {
            //             Expander templateExpander = control as Expander;
            //             templateExpander.IsExpanded = true;
            //             if (templateExpander.Style == (Style)FindResource("BlueExpander"))
            //                 templateExpander.Style = (Style)FindResource("PrintExpander");
            //         }
            //         else if (control is GroupBox)
            //         {
            //             GroupBox templateGropBox = control as GroupBox;
            //             if (templateGropBox.Style == (Style)FindResource("BlueGroupBox"))
            //                 templateGropBox.Style = (Style)FindResource("PrintGroupBox");
            //         }
            //     }


            //        var origParentDirection = containerPanel.FlowDirection;
            //        var origDirection = (containerPanel.Content as FrameworkElement).FlowDirection;

            //        if (containerPanel != null && containerPanel.FlowDirection == FlowDirection.RightToLeft)
            //        {
            //            containerPanel.FlowDirection = FlowDirection.LeftToRight;
            //            (containerPanel.Content as FrameworkElement).FlowDirection = FlowDirection.RightToLeft;
            //        }

            //        var window = new Window();

            //        //PrintDialog printDialog = new PrintDialog();
            //        //string tempFileName = System.IO.Path.GetTempFileName();
            //        //System.IO.File.Delete(tempFileName);

            //        //using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite, System.IO.Packaging.CompressionOption.Fast))
            //        //{
            //        //    XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

            //        //    PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
            //        //    double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / (containerPanel.Content as FrameworkElement).ActualWidth, capabilities.PageImageableArea.ExtentHeight / (containerPanel.Content as FrameworkElement).ActualHeight);
            //        //    (containerPanel.Content as FrameworkElement).LayoutTransform = new ScaleTransform(scale, scale);
            //        //    Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);
            //        //    (containerPanel.Content as FrameworkElement).Measure(sz);
            //        //    (containerPanel.Content as FrameworkElement).Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth, capabilities.PageImageableArea.OriginHeight), sz));

            //        //    writer.Write((containerPanel.Content as FrameworkElement), printSettings.PrintTicket);

            //        //    FixedDocumentSequence doc = xpsDocument.GetFixedDocumentSequence();
            //        //    doc.PrintTicket = printSettings.PrintTicket;

            //        //   window.FlowDirection = System.Windows.FlowDirection.RightToLeft;
            //        //   window.Content = new DocumentViewer { Document = doc };
            //        //   window.ShowDialog();
            //        //}

            //        string tempFileName = System.IO.Path.GetTempFileName();
            //        System.IO.File.Delete(tempFileName);
            //        using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite, System.IO.Packaging.CompressionOption.Fast))
            //        {
            //            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            //            (containerPanel.Content as FrameworkElement).Margin = new Thickness(20);
            //            //(containerPanel.Content as FrameworkElement).Measure(new System.Windows.Size(1000, 1000));
            //            //(containerPanel.Content as FrameworkElement).Arrange(new Rect(0, 0, 1000, 1000));
            //            writer.Write((containerPanel.Content as FrameworkElement), printSettings.PrintTicket);

            //            FixedDocumentSequence doc = xpsDocument.GetFixedDocumentSequence();
            //            doc.PrintTicket = printSettings.PrintTicket;

            //            window.FlowDirection = System.Windows.FlowDirection.RightToLeft;
            //            window.Content = new DocumentViewer { Document = doc };
            //            window.ShowDialog();
            //        }
            //        (containerPanel.Content as FrameworkElement).FlowDirection = origDirection;
            //        containerPanel.FlowDirection = origParentDirection;
            //}
            //Reset(null, null);
            IEnumerable result;
            CRM.Application.Views.ADSLMDFPortList _ADSLMDFPortList = new ADSLMDFPortList();
            switch (_ListType)
            {
                case ((byte)DB.ListType.ADSLMDFPortList):

                    result = ReportDB.GetADSLPortsInfo(_ADSLMDFPortList.CityComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.CenterComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.StatusComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.MDFComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.RowComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.ColumnComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.PortComboBox.SelectedIDs,
                                                      _ADSLMDFPortList.TelephoneNoTextBox.Text.Trim());

                    SendToPrint(result, (int)DB.UserControlNames.ADSLPortsReport);
                    break;
            }
        }

        private void SendToPrint(IEnumerable result,int UserControlName)
        {
            Stimulsoft.Report.StiReport stiReport = new Stimulsoft.Report.StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath(UserControlName);
            stiReport.Load(path);


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
 
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.Confirm())
                {
                    long requestID = _CurrentRequestWindow.RequestID;
                    Request request = Data.RequestDB.GetRequestByID(requestID);
                    Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Confirm, _UserName, actionLogRequest);

                    _CurrentRequestWindow.DialogResult = true;
                    _CurrentRequestWindow.Close();
                }
            }

            if (_CurrentTabWindow != null)
            {
            }

            Reset(null, null);
        }

        private void Deny(object sender, RoutedEventArgs e)
        {
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.RequiresNew))
            //{
                if (_CurrentRequestWindow != null)
                {
                    if (_CurrentRequestWindow.Deny())
                    {
                        if (_CurrentRequestWindow.RequestIDs != null && _CurrentRequestWindow.RequestIDs.Count() > 0)
                        {
                            Request request = Data.RequestDB.GetRequestByID(_CurrentRequestWindow.RequestIDs.Take(1).SingleOrDefault());
                            string description = null;
                            int? requestRejectReason = null;

                            bool IsConfirmDeny = true;
                            switch (request.RequestTypeID)
                            {
                                case (byte)DB.RequestType.ChangeLocationCenterInside:
                                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                                case (byte)DB.RequestType.ChangeAddress:
                                case (byte)DB.RequestType.ChangeName:
                                case (byte)DB.RequestType.ChangeNo:
                                case (byte)DB.RequestType.CutAndEstablish:
                                case (byte)DB.RequestType.Dayri:
                                case (byte)DB.RequestType.Dischargin:
                                case (byte)DB.RequestType.E1:
                                case (byte)DB.RequestType.E1Fiber:
                                case (byte)DB.RequestType.OpenAndCloseZero:
                                case (byte)DB.RequestType.SpecialWire:
                                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                                case (byte)DB.RequestType.VacateSpecialWire:
                                case (byte)DB.RequestType.RefundDeposit:
                                case (byte)DB.RequestType.Reinstall:
                                case (byte)DB.RequestType.SpecialService:
                                case (byte)DB.RequestType.TitleIn118:
                                case (byte)DB.RequestType.PCMToNormal:
                                case (byte)DB.RequestType.TranlationPostInput:
                                case (byte)DB.RequestType.ExchangePost:
                                case (byte)DB.RequestType.TranslationOpticalCabinetToNormal:
                                case (byte)DB.RequestType.BuchtSwiching:
                                    {
                                        RequestRejectDescriptionForm window = new RequestRejectDescriptionForm(request.StatusID);
                                        window.ShowDialog();
                                        description = window.Description;
                                        if (window.RequestRejectReason != -1)
                                            requestRejectReason = window.RequestRejectReason;
                                        else
                                            requestRejectReason = null;

                                        IsConfirmDeny = window.DialogResult ?? false;
                                    }
                                    break;
                            }

                            _CurrentRequestWindow.RequestIDs.ForEach(t =>
                            {
                                request = Data.RequestDB.GetRequestByID(t);
                                if (IsConfirmDeny)
                                {
                                    Data.WorkFlowDB.SetNextState(DB.Action.Reject, request.StatusID, request.ID, null, description, requestRejectReason);

                                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Reject, _UserName, actionLogRequest);


                                }

                            });

                            _CurrentRequestWindow.DialogResult = true;
                            _CurrentRequestWindow.Close();
                        }
                      
                        else
                        {
                            long requestID = _CurrentRequestWindow.RequestID;
                            Request request = Data.RequestDB.GetRequestByID(requestID);
                            string description = null;
                            int? requestRejectReason = null;

                            bool IsConfirmDeny = true;
                            switch (request.RequestTypeID)
                            {
                                case (byte)DB.RequestType.ChangeLocationCenterInside:
                                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                                case (byte)DB.RequestType.ChangeAddress:
                                case (byte)DB.RequestType.ChangeName:
                                case (byte)DB.RequestType.ChangeNo:
                                case (byte)DB.RequestType.CutAndEstablish:
                                case (byte)DB.RequestType.Dayri:
                                case (byte)DB.RequestType.Dischargin:
                                case (byte)DB.RequestType.E1:
                                case (byte)DB.RequestType.E1Fiber:
                                case (byte)DB.RequestType.OpenAndCloseZero:
                                case (byte)DB.RequestType.SpecialWire:
                                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                                case (byte)DB.RequestType.VacateSpecialWire:
                                case (byte)DB.RequestType.RefundDeposit:
                                case (byte)DB.RequestType.Reinstall:
                                case (byte)DB.RequestType.SpecialService:
                                case (byte)DB.RequestType.TitleIn118:
                                case (byte)DB.RequestType.PCMToNormal:
                                case (byte)DB.RequestType.TranlationPostInput:
                                case (byte)DB.RequestType.ExchangePost:
                                case (byte)DB.RequestType.TranslationOpticalCabinetToNormal:
                                case (byte)DB.RequestType.BuchtSwiching:
                                    {
                                        RequestRejectDescriptionForm window = new RequestRejectDescriptionForm(request.StatusID);
                                        window.ShowDialog();
                                        description = window.Description;
                                        if (window.RequestRejectReason != -1)
                                            requestRejectReason = window.RequestRejectReason;
                                        else
                                            requestRejectReason = null;

                                        IsConfirmDeny = window.DialogResult ?? false;
                                    }
                                    break;
                            }

                            if (IsConfirmDeny)
                            {
                                Data.WorkFlowDB.SetNextState(DB.Action.Reject, request.StatusID, request.ID, null, description, requestRejectReason);

                                actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                                actionLogRequest.FormName = _CurrentRequestWindow.Title;
                                ActionLogDB.AddActionLog((byte)DB.ActionLog.Reject, _UserName, actionLogRequest);

                                _CurrentRequestWindow.DialogResult = true;
                                _CurrentRequestWindow.Close();
                            }
                        }
                    }
                }

                if (_CurrentTabWindow != null)
                {
                    if (ItemIDs.Count > 0)
                        {
                            Request request = Data.RequestDB.GetRequestByID(ItemIDs.Take(1).SingleOrDefault());
                            string description = null;
                            int? requestRejectReason = null;

                            bool IsConfirmDeny = true;
                            switch (request.RequestTypeID)
                            {
                                case (byte)DB.RequestType.ChangeLocationCenterInside:
                                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                                case (byte)DB.RequestType.ChangeAddress:
                                case (byte)DB.RequestType.ChangeName:
                                case (byte)DB.RequestType.ChangeNo:
                                case (byte)DB.RequestType.CutAndEstablish:
                                case (byte)DB.RequestType.Dayri:
                                case (byte)DB.RequestType.Dischargin:
                                case (byte)DB.RequestType.E1:
                                case (byte)DB.RequestType.E1Fiber:
                                case (byte)DB.RequestType.OpenAndCloseZero:
                                case (byte)DB.RequestType.SpecialWire:
                                case (byte)DB.RequestType.ChangeLocationSpecialWire:
                                case (byte)DB.RequestType.VacateSpecialWire:
                                case (byte)DB.RequestType.RefundDeposit:
                                case (byte)DB.RequestType.Reinstall:
                                case (byte)DB.RequestType.SpecialService:
                                case (byte)DB.RequestType.TitleIn118:
                                case (byte)DB.RequestType.PCMToNormal:
                                case (byte)DB.RequestType.TranlationPostInput:
                                case (byte)DB.RequestType.ExchangePost:
                                case (byte)DB.RequestType.TranslationOpticalCabinetToNormal:
                                case (byte)DB.RequestType.BuchtSwiching:
                                    {
                                        RequestRejectDescriptionForm window = new RequestRejectDescriptionForm(request.StatusID);
                                        window.ShowDialog();
                                        description = window.Description;
                                        if (window.RequestRejectReason != -1)
                                            requestRejectReason = window.RequestRejectReason;
                                        else
                                            requestRejectReason = null;

                                        IsConfirmDeny = window.DialogResult ?? false;
                                    }
                                    break;
                            }

                            ItemIDs.ForEach(t =>
                            {
                                request = Data.RequestDB.GetRequestByID(t);
                                if (IsConfirmDeny)
                                {
                                    Data.WorkFlowDB.SetNextState(DB.Action.Reject, request.StatusID, request.ID, null, description, requestRejectReason);

                                    actionLogRequest.FormType = _CurrentTabWindow.GetType().FullName;
                                    actionLogRequest.FormName = _CurrentTabWindow.Name;
                                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Reject, _UserName, actionLogRequest);


                                }

                            });

                            _CurrentTabWindow.Load();
                        }
                }

            //   scope.Complete();
            //}

            Reset(null, null);
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            try
            {
                //using (TransactionScope Scope = new TransactionScope())
                //{
                if (_CurrentRequestWindow != null)
                {
                    if (_CurrentRequestWindow.Forward())
                    {
                        if (_CurrentRequestWindow.RequestIDs != null && _CurrentRequestWindow.RequestIDs.Count() > 0)
                        {
                            _CurrentRequestWindow.RequestIDs.ForEach(t =>
                            {
                                Request request = Data.RequestDB.GetRequestByID(t);
                                Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);
                                actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                                actionLogRequest.FormName = _CurrentRequestWindow.Title;
                                ActionLogDB.AddActionLog((byte)DB.ActionLog.Forward, _UserName, actionLogRequest);
                            });

                            _CurrentRequestWindow.DialogResult = true;
                            _CurrentRequestWindow.Close();
                        }
                        else
                        {
                            long requestID = _CurrentRequestWindow.RequestID;
                            Request request = Data.RequestDB.GetRequestByID(requestID);

                            Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                            actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                            actionLogRequest.FormName = _CurrentRequestWindow.Title;
                            ActionLogDB.AddActionLog((byte)DB.ActionLog.Forward, _UserName, actionLogRequest);

                            _CurrentRequestWindow.DialogResult = true;
                            _CurrentRequestWindow.Close();
                        }
                    }
                    else
                        Reset(null, null);
                }

                if (_CurrentTabWindow != null)
                {
                    if (ItemIDs.Count != 0)
                    {
                        MessageBoxResult result = MessageBox.Show("لطفا از طریق باز نمودن فرم مربوطه درخواست را ارجاع نمایید", "توجه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        //    MessageBoxResult result = MessageBox.Show("آیا از ارجاع مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        //    if (result == MessageBoxResult.Yes)
                        //    {
                        //        foreach (long id in ItemIDs)
                        //        {
                        //            Request request = Data.RequestDB.GetRequestByID(id);

                        //            RequestInfo requestInfo = Data.RequestDB.GetRequestInfoByID(id);
                        //            if (requestInfo == null) return;
                        //            //Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);
                        //            //اکشنهای مربوط به وضعیت جاری را استخراج میکند اگر در انها اکشن ارجاع اتو باشد فرم و روال مربوطه راا برسی میکند و متد مربوط به ارجاع اتو را اجرا میکند
                        //            // در غیر اینصورت ارجاع با اکشن تایید انجام میشود
                        //            List<int?> actions = Data.WorkFlowDB.GetActionsCurrentStatus(requestInfo.StatusID);
                        //            if (actions == null) { MessageBox.Show("وضعیت فعلی در چرخه کاری یافت نشد"); return; };

                        //            if (actions.Contains((int)DB.Action.AutomaticForward))
                        //            {
                        //                int? formID = Data.WorkFlowDB.GetProperForm(requestInfo.ID, requestInfo.StatusID);

                        //                if (formID != null && formID == (int)CRM.Data.DB.Form.Investigation)
                        //                {

                        //                    var investigatePossibilityForm = new InvestigatePossibilityForm(requestInfo.ID);
                        //                    investigatePossibilityForm.currentStep = requestInfo.StepID;
                        //                    investigatePossibilityForm.currentStat = requestInfo.StatusID;
                        //                    investigatePossibilityForm.Load();
                        //                    investigatePossibilityForm.Forward();
                        //                    if (investigatePossibilityForm.IsForwardSuccess)
                        //                    {
                        //                        Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestInfo.ID);
                        //                    }



                        //                }

                        //                if (formID != null && formID == (int)CRM.Data.DB.Form.ChooseNumber)
                        //                {
                        //                    var chooseNoForm = new ChooseNoForm(requestInfo.ID);
                        //                    chooseNoForm.currentStep = requestInfo.StepID;
                        //                    chooseNoForm.currentStat = requestInfo.StatusID;
                        //                    chooseNoForm.Load();
                        //                    chooseNoForm.Forward();
                        //                    if (chooseNoForm.IsForwardSuccess)
                        //                    {
                        //                        Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestInfo.ID);
                        //                    }

                        //                }



                        //            }
                        //            else if (actions.Contains((int)DB.Action.Confirm))
                        //            {
                        //                Data.WorkFlowDB.SetNextState(DB.Action.Confirm, requestInfo.StatusID, requestInfo.ID);
                        //            }
                        //        }

                        //        Views.RequestsInbox.ShowSuccessMessage("ارجاع انجام شد.");
                        //        _CurrentTabWindow.Load();
                        //    }
                    }

                    else
                        throw new Exception("درخواستی برای ارجاع انتخاب نشده است !");

                    Reset(null, null);
                }

                actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                actionLogRequest.FormName = _CurrentRequestWindow.Title;
                ActionLogDB.AddActionLog((byte)DB.ActionLog.Forward, _UserName, actionLogRequest);

                //    Scope.Complete();
                //}
            }
            catch (Exception ex)
            {
                Views.RequestsInbox.ShowErrorMessage(ex.Message, ex);
            }
        }

        private void Refund(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_CurrentRequestWindow != null)
                {
                    if (_CurrentRequestWindow.Refund())
                    {
                        long requestID = _CurrentRequestWindow.RequestID;
                        Request request = Data.RequestDB.GetRequestByID(requestID);

                        Data.WorkFlowDB.SetNextState(DB.Action.AutomaticForward, request.StatusID, request.ID);

                        actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                        actionLogRequest.FormName = _CurrentRequestWindow.Title;
                        ActionLogDB.AddActionLog((byte)DB.ActionLog.Refund, _UserName, actionLogRequest);

                        _CurrentRequestWindow.DialogResult = true;
                        _CurrentRequestWindow.Close();
                    }
                    else
                        Reset(null, null);
                }                              
            }
            catch (Exception ex)
            {
                Views.RequestsInbox.ShowErrorMessage(ex.Message, ex);
            }
        }
        
        private void ConfirmEnd(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_CurrentRequestWindow != null)
                {
                    if (_CurrentRequestWindow.ConfirmEnd())
                    {
                        long requestID = _CurrentRequestWindow.RequestID;
                        Request request = Data.RequestDB.GetRequestByID(requestID);
                        Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                        actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                        actionLogRequest.FormName = _CurrentRequestWindow.Title;
                        ActionLogDB.AddActionLog((byte)DB.ActionLog.Confirm, _UserName, actionLogRequest);

                        _CurrentRequestWindow.DialogResult = true;
                        _CurrentRequestWindow.Close();
                    }
                    else
                        Reset(null, null);
                }

                if (_CurrentTabWindow != null)
                {
                    if (ItemIDs.Count != 0)
                    {
                        MessageBoxResult result = MessageBox.Show("آیا از ارجاع مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                        }
                    }

                    else
                        throw new Exception("درخواستی برای تایید نهایی انتخاب نشده است !");

                    Reset(null, null);
                }
            }
            catch (Exception ex)
            {
                Views.RequestsInbox.ShowErrorMessage(ex.Message, ex);
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.Cancel())
                {
                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.Cancelation, _UserName, actionLogRequest);

                    _CurrentRequestWindow.DialogResult = true;
                    _CurrentRequestWindow.Close();
                }
                else
                    Reset(null, null);
            }
        }

        private void SaveWaitingList(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.SaveWaitingList())
                {
                    long requestID = _CurrentRequestWindow.RequestID;
                    Request request = Data.RequestDB.GetRequestByID(requestID);
                    //Data.WorkFlowDB.SetNextState(DB.Action.Confirm, request.StatusID, request.ID);

                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.SaveWaitingList, _UserName, actionLogRequest);

                    _CurrentRequestWindow.DialogResult = true;
                    _CurrentRequestWindow.Close();
                }
                else
                    Reset(null, null);
            }
        }

        private void ExitWaitingList(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_CurrentRequestWindow != null)
                {
                    Reset(null, null);
                }

                if (_CurrentTabWindow != null)
                {
                    if (ItemIDs.Count != 0)
                    {
                        MessageBoxResult result = MessageBox.Show("آیا از خروج درخواست ها از لیست انتظار مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            foreach (long id in ItemIDs)
                            {
                                WaitingList waitingList = WaitingListDB.GetWaitingListByID(id);
                                Request request = Data.RequestDB.GetRequestByID(waitingList.RequestID);

                                switch (request.RequestTypeID)
                                {

                                    case (byte)DB.RequestType.ADSL:
                                        RequestForADSL.ExitWaitingList(waitingList, request);
                                        break;

                                    default:
                                        Data.RequestDB.ExitWaitingList(waitingList, request);
                                        break;
                                }
                            }

                            Views.RequestsInbox.ShowSuccessMessage("خروج از لیست انتظار انجام شد.");
                            _CurrentTabWindow.Load();
                        }
                    }

                    else
                        throw new Exception("درخواستی برای خروج از لیست انتظار انتخاب نشده است !");

                    Reset(null, null);
                }

            }

            catch (Exception ex)
            {
                Views.RequestsInbox.ShowErrorMessage(ex.Message, ex);
            }
        }

        private void SaveBlackList(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.SaveBlackList())
                {
                    Views.RequestsInbox.ShowSuccessMessage("ذخیره در لیست انتظار انجام شد.");
                }
            }
        }

        private void ExitBlackList(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.ExitBlackList())
                {
                    Views.RequestsInbox.ShowSuccessMessage("خروج از لیست انتظار انجام شد.");
                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
                _CurrentRequestWindow.Close();
        }

        private void KickedBack(object sender, RoutedEventArgs e)
        {
            if (_CurrentRequestWindow != null)
            {
                if (_CurrentRequestWindow.KickedBack())
                {
                    actionLogRequest.FormType = _CurrentRequestWindow.GetType().FullName;
                    actionLogRequest.FormName = _CurrentRequestWindow.Title;
                    ActionLogDB.AddActionLog((byte)DB.ActionLog.KickedBack, _UserName, actionLogRequest);

                    _CurrentRequestWindow.DialogResult = true;
                    _CurrentRequestWindow.Close();
                }
                else
                {
                    Reset(null, null);
                }
            }
        }

        internal void InternalForward(object sender, RoutedEventArgs e)
        {
            Forward( sender,  e);
        }

        #endregion
    }
}
