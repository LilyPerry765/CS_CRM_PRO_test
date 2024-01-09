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
using Stimulsoft.Report;
using System.Windows.Media.Effects;
using CRM.Application.Reports.ReportUserControls;
using System.Reflection;
using Enterprise;

namespace CRM.Application.Reports.Viewer
{
    public partial class VersionOneReportList : Local.TabWindow
    {
        #region Properties And Fields

        private bool _isLoad;

        public string UserControlName
        {
            get;
            set;
        }

        public bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }

        public int SelectedReportTemplateId
        {
            get;
            set;
        }

        public UserControl SelectedReportUserControl
        {
            get;
            set;
        }

        public enum FormState
        {
            Nothing,
            Insert,
            Update
        }

        public FormState CurrentFormSate
        {
            get;
            set;
        }

        #endregion Properties And Fields

        #region Constructor

        public VersionOneReportList()
        {
            InitializeComponent();
            Initialize();

            StiOptions.Engine.GlobalEvents.SavingReportInDesigner -= new Stimulsoft.Report.Design.StiSavingObjectEventHandler(GlobalEvents_SavingReportInDesigner);
            StiOptions.Engine.GlobalEvents.SavingReportInDesigner += new Stimulsoft.Report.Design.StiSavingObjectEventHandler(GlobalEvents_SavingReportInDesigner);
            //TODO:this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("pack://application:,,,/HelpDesk.Application;component/Resources/Styles/ReportStyle.xaml") });
            this.CurrentFormSate = FormState.Nothing;

        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            try
            {
                List<ReportTemplateInfo> reportList = ReportDB.GetReportInfo();
                List<Expander> expanderList = new List<Expander>();
                List<StackPanel> stackPanelList = new List<StackPanel>();
                List<string> categoryList = new List<string>();
                StackPanel expanderSatckPanel;

                if (IsLoad)
                    return;
                else
                    IsLoad = true;

                //Initialize CategoryNames for creating Expander.Header
                foreach (ReportTemplateInfo report in reportList)
                {
                    if (DB.CurrentUser.ReportTemplateIDs.Contains(report.ID))
                    {
                        if (!categoryList.Contains(report.Category))
                        {
                            categoryList.Add(report.Category);
                        }
                    }
                }

                //Initialize expanderList
                foreach (string categoryName in categoryList)
                {
                    Expander expander = new Expander();
                    expander.Header = categoryName;
                    expander.Style = this.Resources["ExpanderStyle"] as Style;
                    expanderSatckPanel = new StackPanel();
                    expander.Content = expanderSatckPanel;
                    expanderList.Add(expander);
                }


                //Create MenuItem Instance as ReportList Item
                foreach (ReportTemplateInfo report in reportList)
                {
                    if (DB.CurrentUser.ReportTemplateIDs.Contains(report.ID))
                    {
                        MenuItem menuItem = new MenuItem();
                        menuItem.Header = report.Title;
                        menuItem.Style = this.Resources["MenuItemHeaderStyle"] as Style;
                        menuItem.BorderBrush = Brushes.Transparent;
                        menuItem.BorderThickness = new Thickness(0);
                        menuItem.Icon = new Image { Source = Helper.GetBitmapImage(report.IconName), Height = 24, Width = 24 };
                        menuItem.Tag = report.ID;
                        string category = report.Category;

                        //MenuItem Events:
                        menuItem.Click += new RoutedEventHandler(menuItem_Click);
                        menuItem.MouseMove += new MouseEventHandler(menuItem_MouseMove);
                        menuItem.MouseLeave += new MouseEventHandler(menuItem_MouseLeave);

                        //Set ExpanderList.Items.Content.Children
                        foreach (Expander expander in expanderList)
                        {
                            if (expander.Header.ToString() == category)
                            {
                                (expander.Content as StackPanel).Children.Add(menuItem);
                            }
                        }
                    }
                }
                foreach (Expander expander in expanderList)
                {
                    ReportStackPanel.Children.Add(expander);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در سازنده لیست گزارش ها");
            }
        }

        #endregion Initializer

        #region Event Handlers

        private void menuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            int temID = Convert.ToInt32((sender as MenuItem).Tag);
            if (temID == SelectedReportTemplateId)
            {
                return;
            }
            else
            {
                (sender as MenuItem).FontWeight = FontWeights.Normal;
            }
            (sender as MenuItem).Background = Brushes.Transparent;
        }

        private void menuItem_MouseMove(object sender, MouseEventArgs e)
        {
            (sender as MenuItem).FontWeight = FontWeights.SemiBold;
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            SelectedReportTypeBorder.Visibility = Visibility.Visible;
            Reset();
            (sender as MenuItem).Background = Brushes.DimGray;
            (sender as MenuItem).FontWeight = FontWeights.DemiBold;
            (sender as MenuItem).FontSize = 14;
            (sender as MenuItem).Foreground = Brushes.Yellow;
            SelectedReportTemplateId = Convert.ToInt32((sender as MenuItem).Tag);
            UserControlName = ReportDB.GetUserControlName(SelectedReportTemplateId);
            ReportHeaderTextBlock.Text = (sender as MenuItem).Header.ToString();
            ReportHeaderImage.Source = Helper.GetBitmapImage(ReportDB.GetReportIconName(SelectedReportTemplateId));
            //CreateParent(UserControlName);
            CreateParent(SelectedReportTemplateId);
            SelectedReportUserControl = SelectedReportTypeStackPanel.Children.OfType<UserControl>().First();

        }

        private void ViewReport_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (SelectedReportUserControl != null)
            {
                (SelectedReportUserControl as Local.ReportBase).UserControlID = SelectedReportTemplateId;
                (SelectedReportUserControl as Local.ReportBase).Search();
            }
            this.Cursor = Cursors.Arrow;
        }

        private void StopReport_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewReport_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFormSate == FormState.Nothing)
            {
                ReportDesignerForm reportDesignerForm = new ReportDesignerForm(true, -1);
                CurrentFormSate = FormState.Insert;
                reportDesignerForm.ShowDialog();
                CurrentFormSate = FormState.Nothing;
            }
            else
            {
                Folder.MessageBox.Show("یک الگوی گزارش باز موجود است", "", MessageBoxImage.Warning, MessageBoxButton.OK);
            }
        }

        private void EditTemplate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFormSate == FormState.Nothing)
            {
                ReportDesignerForm reportDesignerForm = new ReportDesignerForm(false, (int)SelectedReportTemplateId);
                CurrentFormSate = FormState.Update;
                reportDesignerForm.ShowDialog();
                CurrentFormSate = FormState.Nothing;
            }
            else
            {
                Folder.MessageBox.Show("یک الگوی گزارش باز موجود است", "", MessageBoxImage.Warning, MessageBoxButton.OK);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetReportMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GlobalEvents_SavingReportInDesigner(object sender, Stimulsoft.Report.Design.StiSavingObjectEventArgs e)
        {
            e.Processed = true;
            //ReportDesignerForm frm = ((ReportDesignerForm)(((System.Windows.Controls.ContentControl)(sender)).Parent));
            //((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray();
            Save(((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray());
        }

        #endregion  Event Handlers

        #region Methods

        private void Save(byte[] rep)
        {
            if (CurrentFormSate == FormState.Update)
            {
                int id = SelectedReportTemplateId;
                SaveReportForm frm = new SaveReportForm(rep, id);
                frm.ShowDialog();
            }
        }

        private void Reset()
        {

            foreach (Expander expander in ReportStackPanel.Children.OfType<Expander>())
            {
                foreach (MenuItem menuItem in (expander.Content as StackPanel).Children.OfType<MenuItem>())
                {
                    menuItem.FontSize = 12;
                    menuItem.Background = Brushes.Transparent;
                    menuItem.FontWeight = FontWeights.Normal;
                    menuItem.Foreground = Brushes.Black;
                }
            }
        }

        private void GlobalEvents_SavingFormInDesigner(object sender, Stimulsoft.Report.Design.StiSavingObjectEventArgs e)
        {
            e.Processed = true;
            CRM.Application.Reports.Viewer.FormDesignerForm frm = ((CRM.Application.Reports.Viewer.FormDesignerForm)(((System.Windows.Controls.ContentControl)(sender)).Parent));
            ((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray();
            Save(((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray());

        }

        private void CreateParent(int ReportTemplateId)//(string userControlName)
        {
            SelectedReportTypeStackPanel.Children.Clear();
            switch (ReportTemplateId)
            {
                case ((int)DB.UserControlNames.ADSL):
                    {
                        ADSLReportUserControl ReportUC = new ADSLReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLEquipment):
                    {
                        ADSLEquipmentReportUserControl ReportUC = new ADSLEquipmentReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLRequest):
                    {
                        ADSLRequestReportUserControl ReportUC = new ADSLRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeNumber):
                    {
                        ChangeNumberReportUserControl ReportUC = new ChangeNumberReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLDayeriRequest):
                    {
                        ADSLDayeriRequestReportUserControl ReportUC = new ADSLDayeriRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DisconnectAndConnectCount):
                    {
                        DisconnectAndConnectCountReportUserControl ReportUC = new DisconnectAndConnectCountReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PapADSLRequest):
                    {
                        PapADSLRequestReportUserControl ReportUC = new PapADSLRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLOfficialDelay):
                    {
                        ADSLOfficialDelayReportUserControl ReportUC = new ADSLOfficialDelayReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLStatistic):
                    {
                        ADSLStatisticReportUserControl ReportUC = new ADSLStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeName):
                    {
                        ChangeNameReportUserControl ReportUC = new ChangeNameReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DayeriRequest):
                    {
                        DayeriRequestReportUserControl ReportUC = new DayeriRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PapRequestOperation):
                    {
                        PapRequestOperationReportUserControl ReportUC = new PapRequestOperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InvestigatePossibility):
                    {
                        InvestigatePossibilityReportUserControl ReportUC = new InvestigatePossibilityReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ReDayeriRequest):
                    {
                        ReDayeriRequestReportUserControl ReportUC = new ReDayeriRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.IssueWiring):
                    {
                        IssueWiringReportUserControl ReportUC = new IssueWiringReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ZeroStatus):
                    {
                        ZeroStatusReportUserControl ReportUC = new ZeroStatusReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeTitleIn118):
                    {
                        ChangeTitleIn118ReportUserControl ReportUC = new ChangeTitleIn118ReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpaceAndPower):
                    {
                        SpaceAndPowerReportUserControl ReportUC = new SpaceAndPowerReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpecialService):
                    {
                        SpecialServiceReportUserControl ReportUC = new SpecialServiceReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeLocation):
                    {
                        ChangeLocationReportUserControl ReportUC = new ChangeLocationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalCenterCabinetInfo):
                    {
                        CenterCabinetInfoReportUserControl ReportUC = new CenterCabinetInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DetailsCenterCabinetInfo):
                    {
                        CenterCabinetInfoReportUserControl ReportUC = new CenterCabinetInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostInfoTotal):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoDetails):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetCentersInfo):
                    {
                        CabinetCentersInfoUserControl ReportUC = new CabinetCentersInfoUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                //case ((int)DB.UserControlNames.PostContacts):
                //    {
                //        PostContactsReportUserControl ReportUC = new PostContactsReportUserControl();
                //        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                //        break;
                //    }
                case ((int)DB.UserControlNames.PCMContactsStatistic):
                    {
                        PCMContactsStatisticReportUserControl ReportUC = new PCMContactsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMContactsPostStatistic):
                    {
                        PCMContactsPostStatisticReportUserControl ReportUC = new PCMContactsPostStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMsStatistic):
                    {
                        PCMsStatisticReportUserControl ReportUC = new PCMsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMStatisticEquipment):
                    {
                        PCMStatisticEquipmentReportUserControl ReportUC = new PCMStatisticEquipmentReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.AllPCMs):
                    {
                        PCMReportUserControl ReportUC = new PCMReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Failure117Requests):
                    {
                        Failure117RequestsReportUserControl ReportUC = new Failure117RequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Status_DateSendingFailure117Requests):
                    {
                        SendingFailure117RequestsReportUserControl ReportUC = new SendingFailure117RequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetInputFailure):
                    {
                        CabinetInputFailureReportUserControl ReportUC = new CabinetInputFailureReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InstallPCM):
                    {
                        InstallPCMReportUserControl ReportUC = new InstallPCMReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EmptyPCMs):
                    {
                        EmptyPCMsReportUserControl ReportUC = new EmptyPCMsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SendingFailure117RequestsToNetworkCable):
                    {
                        SendingFailure117ToNetworlAndCableReportUserControl ReportUC = new SendingFailure117ToNetworlAndCableReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalCabinetInputFailure):
                    {
                        TotalCabinetInputFailureReportUserControl ReportUC = new TotalCabinetInputFailureReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureTimeTable):
                    {
                        FailureTimeTableReportUserControl ReportUC = new FailureTimeTableReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetCapacity):
                    {
                        CabinetCapacityReportUserControl ReportUC = new CabinetCapacityReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalStatisticPCMPorts):
                    {
                        TotalStatisticPCMPortsReportUserControl ReportUC = new TotalStatisticPCMPortsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CenterCablesTotal):
                    {
                        CenterCableReportUserControl ReportUC = new CenterCableReportUserControl(false);
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CenterCablesDetails):
                    {
                        CenterCableReportUserControl ReportUC = new CenterCableReportUserControl(true);
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InputStatistic):
                    {
                        InputStatisticReportUserControl ReportUC = new InputStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.VerticalBuchtsStatistic):
                    {
                        VerticalBuchtsStatisticReportUserControl ReportUC = new VerticalBuchtsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.FailurePortsStatistic):
                    {
                        FailurePortsStatisticReportUserControl ReportUC = new FailurePortsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureCorrectPorts):
                    {
                        FailureCorrectPortsReportUserControl ReportUC = new FailureCorrectPortsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureNotCorrectPorts):
                    {
                        FailureNotCorrectPortsReportUserControl ReportUC = new FailureNotCorrectPortsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallAndDisChargePapCompany):
                    {
                        InstallAndDisChargePapCompanyReportUserControl ReportUC = new InstallAndDisChargePapCompanyReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failurePostContacts):
                    {
                        failurePostContactsReportUserControl ReportUC = new failurePostContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureCorrectedPostContacts):
                    {
                        failureCorrectedPostContactsReportUserControl ReportUC = new failureCorrectedPostContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureNotCorrectedPostContacts):
                    {
                        failureNotCorrectedPostContactsReportUserControl ReportUC = new failureNotCorrectedPostContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.failureCabinetInputReport):
                    {
                        failureCabinetInputsReportUserControl ReportUC = new failureCabinetInputsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureCorrectedCabinetInputs):
                    {
                        FailureCorrectCabinetInputReportUserControl ReportUC = new FailureCorrectCabinetInputReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureNotCorrectedCabinetInputs):
                    {
                        FailureNotCorrectCabinetInputReportUserControl ReportUC = new FailureNotCorrectCabinetInputReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpaceAndPowerPapCompany):
                    {
                        SpaceAndPowerPapCompanyReportUserControl ReportUC = new SpaceAndPowerPapCompanyReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CablePair):
                    {
                        CablePairUserControl ReportUC = new CablePairUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.HorizintalBuchtsStatistic):
                    {
                        HorizintalBuchtsStatisticReportUserControl ReportUC = new HorizintalBuchtsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EmptyTelephone):
                    {
                        EmptyTelephoneReportUserControl ReportUC = new EmptyTelephoneReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ResignationLines):
                    {
                        ResignationLinesStatisticReportUserControl ReportUC = new ResignationLinesStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TelephoneWithOutPCM):
                    {
                        TelephoneWithOutPCMReportUserControl ReportUC = new TelephoneWithOutPCMReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PCMInPost):
                    {
                        PCMInpostReportUserControl ReportUC = new PCMInpostReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ReleaseDocuments):
                    {
                        ReLeaseFileIDReportUserControl ReportUC = new ReLeaseFileIDReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PerformanceFailure117):
                    {
                        PerformanceFailure117ReportUserControl ReportUC = new PerformanceFailure117ReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallRequestForm):
                    {
                        DayeriRequestFormReportUserControl ReportUC = new DayeriRequestFormReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeBucht):
                    {
                        ChangeBuchtReportUserControl ReportUC = new ChangeBuchtReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EquipmentBilling):
                    {
                        EquipmentBillingReportUserControl ReportUC = new EquipmentBillingReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TinyBillingOptions):
                    {
                        TinyBillingOptionsReportUserControl ReportUC = new TinyBillingOptionsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PerformanceWiringNetwork):
                    {
                        PerformanceWiringNetworkReportUserControl ReportUC = new PerformanceWiringNetworkReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SendingFailure117Cable):
                    {
                        SendingFailure117CableReportUserControl ReportUC = new SendingFailure117CableReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoCable):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostInfoReserve):
                    {
                        PostInfoReserveReportUserControl ReportUC = new PostInfoReserveReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoFill):
                    {
                        PostInfoFillReportUserControl ReportUC = new PostInfoFillReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterCabinet_Subset):
                    {
                        CenterCabinetSubsetReportUserControl ReportUC = new CenterCabinetSubsetReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterCabinet_CabinetSyndeticOrder):
                    {
                        CenterCabinet_CabinetSyndeticOrderReportUserControl ReportUC = new CenterCabinet_CabinetSyndeticOrderReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RequestPayment):
                    {
                        RequestPaymentUserControl ReportUC = new RequestPaymentUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceReport):
                    {
                        ADSLServiceReportUserControl ReportUC = new ADSLServiceReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTelephoneNoHistoryReport):
                    {
                        ADSLTelephoneNoHistoryReportUserControl ReportUC = new ADSLTelephoneNoHistoryReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentReport):
                    {
                        ADSLSellerAgentReportUserControl ReportUC = new ADSLSellerAgentReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLHistoryReport):
                    {
                        ADSLHistoryReportUserControl ReportUC = new ADSLHistoryReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCustomerInfoReport):
                    {
                        ADSLCustomerInfoReportUserControl ReportUC = new ADSLCustomerInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPAPEquipmentInfo):
                    {
                        PAPEquipmentReportUserControl ReportUC = new PAPEquipmentReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPaymentReport):
                    {
                        ADSLPaymentReportUserControl ReportUC = new ADSLPaymentReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstallmentByExpertReport):
                    {
                        ADSLInstallmentCostByExpertReportUserControl ReportUC = new ADSLInstallmentCostByExpertReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDayeriDischargeReport):
                    {
                        ADSLDayeriDischargeReportUserControl ReportUC = new ADSLDayeriDischargeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLGeneralCustomerInforeport):
                    {
                        ADSLGeneralCustomerInfoReportUserControl ReportUC = new ADSLGeneralCustomerInfoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberSaleReport):
                    {
                        ADSLNumberSaleReportUserControl ReportUC = new ADSLNumberSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSaleReport):
                    {
                        ADSLSaleReportUserControl ReportUC = new ADSLSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLBandwidthReport):
                    {
                        ADSLBandwidthReportUserControl ReportUC = new ADSLBandwidthReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLReadyToInstallCustomersReport):
                    {
                        ADSLReadyToInstallCustomersReportUserControl ReportUC = new ADSLReadyToInstallCustomersReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCustomersDeliveredModemReport):
                    {
                        ADSLCustomersDeliveredModemReportUserControl ReportUC = new ADSLCustomersDeliveredModemReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityCenterSaleStatisticsReport):
                    {
                        ADSLCityCenterSaleStatisticsReportUserControl ReportUC = new ADSLCityCenterSaleStatisticsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCitySellerSaleStatisticsReport):
                    {
                        ADSLCitySellerSaleStatisticsReportUserControl ReportUC = new ADSLCitySellerSaleStatisticsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleReport):
                    {
                        ADSLSellerAgentSaleReportUserControl ReportUC = new ADSLSellerAgentSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCenterSaleReport):
                    {
                        ADSLCenterSaleReportUserControl ReportUC = new ADSLCenterSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCitySaleReport):
                    {
                        ADSLCitySaleReportUserControl ReportUC = new ADSLCitySaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLOnlineDailyCitySaleReport):
                    {
                        ADSLOnlineDailyCitySaleReportUserControl ReportUC = new ADSLOnlineDailyCitySaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityDailyDischargeReport):
                    {
                        ADSLCityDailyDischargeReportUserControl ReportUC = new ADSLCityDailyDischargeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCenterGeneralContactsReport):
                    {
                        ADSLCenterGeneralContactsReportUserControl ReportUC = new ADSLCenterGeneralContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLMonthlyComparisonDiagram91):
                    {
                        ADSLMonthlyComparisonDiagram91UserControl ReportUC = new ADSLMonthlyComparisonDiagram91UserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityContactsGeneralReport):
                    {
                        ADSLCityContactsGeneralReportUserControl ReportUC = new ADSLCityContactsGeneralReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCityWeeklyContactsReport):
                    {
                        ADSLCityWeeklyContactsReportUserControl ReportUC = new ADSLCityWeeklyContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLWeeklyComparisionDiagramContactsPAP):
                    {
                        ADSLWeeklyComparisionDiagramContactsPAPUserControl ReportUC = new ADSLWeeklyComparisionDiagramContactsPAPUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSoldModemReport):
                    {
                        ADSLSoldModemReportUserControl ReportUC = new ADSLSoldModemReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLDayeriRequest):
                    {
                        PAPDayeriRequestsReportUserControl ReportUC = new PAPDayeriRequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLDischargeRequest):
                    {
                        PAPDischargeRequestsReportUserControl ReportUC = new PAPDischargeRequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLChangePortRequest):
                    {
                        PAPChangePortRequestsReportUserControl ReportUC = new PAPChangePortRequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCityMonthlyReport):
                    {
                        ADSLCityMonthlyReportUserControl ReportUC = new ADSLCityMonthlyReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPortsCityCenterReport):
                    {
                        ADSLPortsCityCenterReportUserControl ReportUC = new ADSLPortsCityCenterReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSaleServiceAndADSLSellChanellReport):
                    {
                        ADSLSaleServiceAndADSLSellChanellReportUserControl ReportUC = new ADSLSaleServiceAndADSLSellChanellReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLIncomeSellChanellAndTimeReport):
                    {
                        ADSLIncomeSellChanellAndTimeReportUserControl ReportUC = new ADSLIncomeSellChanellAndTimeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLChangeServiceReportUserControl):
                    {
                        ADSLChangeServiceReportUserControl ReportUC = new ADSLChangeServiceReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLAdditionalServiceSaleReport):
                    {
                        ADSLAdditionalServiceSaleReportUserControl ReportUC = new ADSLAdditionalServiceSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstalledADSLReport):
                    {
                        ADSLInstalledADSLReportUserControl ReportUC = new ADSLInstalledADSLReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLMostSoldServicesReport):
                    {
                        ADSLMostSoldServicesReportUserControl ReportUC = new ADSLMostSoldServicesReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDischargeRequestsReport):
                    {
                        ADSLDischargeRequestsReportUserControl ReportUC = new ADSLDischargeRequestsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeReport):
                    {
                        DischargeReportUserControl ReportUC = new DischargeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.Report118):
                    {
                        Report118UserControl ReportUC = new Report118UserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeNoReport):
                    {
                        ChangeNoReportUserControl ReportUC = new ChangeNoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CutAndEstablishReport):
                    {
                        CutAndEstablishReportUserControl ReportUC = new CutAndEstablishReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstalmentRequestPaymentReport):
                    {
                        InstalmentRequestReportUserControl ReportUC = new InstalmentRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.GenrealCostReport):
                    {
                        GenrealCostReportUserControl ReportUC = new GenrealCostReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.FineToFineCostReport):
                    {
                        FineToFineCostReportUserControl ReportUC = new FineToFineCostReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallRequestReport):
                    {
                        InstallRequestReportUserControl ReportUC = new InstallRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RegisterReport):
                    {
                        RegisterReportUserControl ReportUC = new RegisterReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.SpecialServiceStatisticsReport):
                    {
                        SpecialServiceStatisticsReportUserControl ReportUC = new SpecialServiceStatisticsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.BlockingReport):
                    {
                        BlockingReportUserControl ReportUC = new BlockingReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPortsReport):
                    {
                        ADSLPortsReportUserControl ReportUC = new ADSLPortsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentADSLRequestSaleDetailesReport):
                    {
                        ADSLSellerAgentADSLRequestSaleDetailesReportUserControl ReportUC = new ADSLSellerAgentADSLRequestSaleDetailesReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLOnlineRegistrationReport):
                    {
                        ADSLOnlineRegistrationReportUserControl ReportUC = new ADSLOnlineRegistrationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSCityCenterDailyReport):
                    {
                        ADSCityCenterDailyReportUserControl ReportUC = new ADSCityCenterDailyReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLIntranetDailySaleReport):
                    {
                        ADSLIntranetDailySaleReportUserControl ReportUC = new ADSLIntranetDailySaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityDailyIncomeReport):
                    {
                        ADSLCityDailyIncomeReportUserControl ReportUC = new ADSLCityDailyIncomeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceSaleReport):
                    {
                        ADSLServiceSaleReportUserControl ReportUC = new ADSLServiceSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficSaleReport):
                    {
                        ADSLTrafficSaleReportUserControl ReportUC = new ADSLTrafficSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberOfDayeriDischargeReshargeReport):
                    {
                        ADSLNumberOfDayeriDischargeReshargeReportUserControl ReportUC = new ADSLNumberOfDayeriDischargeReshargeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLSellerAgentUsersSaleAmountReport):
                    {
                        ADSLSellerAgentUsersSaleAmountReportUserControl ReportUC = new ADSLSellerAgentUsersSaleAmountReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberOfDayeriExpirationReport):
                    {
                        ADSLNumberOfDayeriExpirationReportUserControl ReportUC = new ADSLNumberOfDayeriExpirationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLExpirationDateAndLastServiceTelephoneNoReport):
                    {
                        ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl ReportUC = new ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCenterPortStatusReport):
                    {
                        ADSLCenterPortStatusReportUserControl ReportUC = new ADSLCenterPortStatusReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentCashSaleReport):
                    {
                        ADSLSellerAgentCashSaleReportUserControl ReportUC = new ADSLSellerAgentCashSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleDetailsReport):
                    {
                        ADSLSellerAgentSaleDetailsReportUserControl ReportUC = new ADSLSellerAgentSaleDetailsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSeviceSaleBandwidthSeparationReport):
                    {
                        ADSLSeviceSaleBandwidthSeparationReportUserControl ReportUC = new ADSLSeviceSaleBandwidthSeparationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLTrafficSaleTrafficSeperationReport):
                    {
                        ADSLTrafficSaleTrafficSeperationReportUserControl ReportUC = new ADSLTrafficSaleTrafficSeperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceSaleCustomerAndServiceSperationReport):
                    {
                        ADSLServiceSaleCustomerAndServiceSperationReportUserControl ReportUC = new ADSLServiceSaleCustomerAndServiceSperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceAggragateSaleCenterSeperationReport):
                    {
                        ADSLServiceAggragateSaleCenterSeperationReportUserControl ReportUC = new ADSLServiceAggragateSaleCenterSeperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficSaleCustomerSeperationReport):
                    {
                        ADSLTrafficSaleCustomerSeperationReportUserControl ReportUC = new ADSLTrafficSaleCustomerSeperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficAggregateSaleCenterCostCodeSeperationReport):
                    {
                        ADSLTrafficAggregateSaleCenterCostCodeSeperationReportUserControl ReportUC = new ADSLTrafficAggregateSaleCenterCostCodeSeperationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLModemAggragateSaleReport):
                    {
                        ADSLModemAggragateSaleReportUserControl ReportUC = new ADSLModemAggragateSaleReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInformationReport):
                    {
                        ADSLInformationReportUserControl ReportUC = new ADSLInformationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLInstalmetTabReport):
                    {
                        ADSLInstalmetTabReportUserControl ReportUC = new ADSLInstalmetTabReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleDetailsGroupByTelephoneNoReport):
                    {
                        ADSLSellerAgentSaleDetailsGroupByTelephoneNoReportUserControl ReportUC = new ADSLSellerAgentSaleDetailsGroupByTelephoneNoReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RefundDepositReport):
                    {
                        RefundDepositReportUserControl ReportUC = new RefundDepositReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PersonTypeReport):
                    {
                        PersonTypeReportUserControl ReportUC = new PersonTypeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.EmptyTelephoneNoLisReport):
                    {
                        EmptyTelephoneNoLisReportUserControl ReportUC = new EmptyTelephoneNoLisReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeWiringNetworkReport):
                    {
                        DischargeWiringNetwork_ReportUserControl ReportUC = new DischargeWiringNetwork_ReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeCertificateReport):
                    {
                        //TODO:rad
                        DischargeCertificateReportUserControl reportUC = new DischargeCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeNoCertificateReport):
                    {
                        //TODO:rad
                        ChangeNoCertificateReportUserControl ReportUC = new ChangeNoCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport):
                    {
                        ChangeLocationCenterInsideWiringReportUserControl ReportUC = new ChangeLocationCenterInsideWiringReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInformationSystemReport):
                    {
                        ADSLInformationSystemReportUserControl ReportUC = new ADSLInformationSystemReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDischargeDetailsReport):
                    {
                        ADSLDischargeDetailsReportUserControl ReportUC = new ADSLDischargeDetailsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TelephoneRequestLogReport):
                    {
                        TelephoneRequestLogReportUserControl ReportUC = new TelephoneRequestLogReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLModemInformationReport):
                    {
                        ADSLModemInformationReportUserControl ReportUC = new ADSLModemInformationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentComissionReport):
                    {
                        ADSLSellerAgentComissionReportUserControl ReportUC = new ADSLSellerAgentComissionReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CustomerPersonalInformationReport):
                    {
                        CustomerPersonalInformationReportUserControl ReportUC = new CustomerPersonalInformationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentUsersCreditReport):
                    {
                        ADSLSellerAgentUsersCreditReportUserControl ReportUC = new ADSLSellerAgentUsersCreditReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstallmentRequestPaymentTelephoneNo):
                    {
                        ADSLInstallmentRequestPaymentTelephoneNo ReportUC = new ADSLInstallmentRequestPaymentTelephoneNo();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TranslationPostReport):
                    {
                        TranslationPostReportUserControl ReportUC = new TranslationPostReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TranslationPostInputReport):
                    {
                        TranslationPostInputReportUserControl ReportUC = new TranslationPostInputReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ExchangeCabinetInputReport):
                    {
                        ExchangeCabinetInputReportUserControl ReportUC = new ExchangeCabinetInputReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ExchangeCentralCableMDFReport):
                    {
                        ExchangeCentralCableMDFReportUserControl ReportUC = new ExchangeCentralCableMDFReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterToCenterTranslationReport):
                    {
                        CenterToCenterTranslationReportUserControl ReportUC = new CenterToCenterTranslationReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.BuchtSwitchingReport):
                    {
                        BuchtSwitchingReportUserControl ReportUC = new BuchtSwitchingReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.WorkingTelephone):
                    {
                        WorkingTelephone ReportUC = new WorkingTelephone();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.RoundTelephone):
                    {
                        RoundTelephone ReportUC = new RoundTelephone();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.WarningHistoryReport):
                    {
                        WarningHistoryReport ReportUC = new WarningHistoryReport();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CustomerNationalCodeReport):
                    {
                        CustomerNationalCodeReport ReportUC = new CustomerNationalCodeReport();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.WorkingTelephoneBaseDate):
                    {
                        WorkingTelephoneBaseDate ReportUC = new WorkingTelephoneBaseDate();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CustomerOffice):
                    {
                        CustomerOfficeReport ReportUC = new CustomerOfficeReport();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostContactTotal):
                case ((int)DB.UserControlNames.PostContactReserve):
                case ((int)DB.UserControlNames.PostContactFill):
                case ((int)DB.UserControlNames.PostContactFail):
                case ((int)DB.UserControlNames.PostContactEmpty):
                    {
                        PostContactsStatisticsReportUserControl ReportUC = new PostContactsStatisticsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetInputEmpty):
                case ((int)DB.UserControlNames.CabinetInputReserve):
                case ((int)DB.UserControlNames.CabinetInputFill):
                case ((int)DB.UserControlNames.CabinetInputFail):
                case ((int)DB.UserControlNames.CabinetInputTotal):
                    {
                        InputStatisticReportUserControl ReportUC = new InputStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.VerticalBuchtEmpty):
                case ((int)DB.UserControlNames.VerticalBuchtReserve):
                case ((int)DB.UserControlNames.VerticalBuchtFill):
                case ((int)DB.UserControlNames.VerticalBuchtFail):
                case ((int)DB.UserControlNames.VerticalBuchtTotal):
                    {
                        VerticalBuchtsStatisticReportUserControl ReportUC = new VerticalBuchtsStatisticReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CablePairEmpty):
                case ((int)DB.UserControlNames.CablePairReserve):
                case ((int)DB.UserControlNames.CablePairFill):
                case ((int)DB.UserControlNames.CablePairFail):
                case ((int)DB.UserControlNames.CablePairTotal):
                    {
                        CablePairUserControl ReportUC = new CablePairUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.TranslationOpticalCabinetToNormalRequestReport):
                    {
                        //TODO:rad
                        TranslationOpticalCabinetToNormalRequestUserControl reportUC = new TranslationOpticalCabinetToNormalRequestUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                //case (int)(DB.UserControlNames.SpecialWireCertificatePrintReport):
                //    {
                //        SpecialWireCertificatePrintUserControl reportUC = new SpecialWireCertificatePrintUserControl();
                //        SelectedReportTypeStackPanel.Children.Add(reportUC);
                //        break;
                //    }
                case (int)(DB.UserControlNames.SwapTelephoneReport):
                    {
                        //TODO:rad
                        SwapTelephoneReportUserControl reportUC = new SwapTelephoneReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.SwapPCMReport):
                    {
                        //TODO:rad
                        SwapPCMReportUserControl reportUC = new SwapPCMReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.ModifyProfileReport):
                    {
                        //TODO:rad
                        ModifyProfileReportUserControl reportUC = new ModifyProfileReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Failure117TotalInfoUserControl):
                    {
                        Failure117TotalInfoUserControl ReportUC = new Failure117TotalInfoUserControl();
                        SelectedReportTypeStackPanel.Children.Add(ReportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PostContacts:
                    {
                        //TODO:rad
                        PostContactsReportUserControl reportUC = new PostContactsReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.Failure117RequestRemaindInNetwork:
                    {
                        Failure117RequestRemaindInNetworkReportUserControl reportUC = new Failure117RequestRemaindInNetworkReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }


                case (int)DB.UserControlNames.ChangeLocationSpecialWireCertificate:
                    {
                        CertificateChangeLocationSpecialWireUserControl reportUC = new CertificateChangeLocationSpecialWireUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.VacateSpecialWireCertificate:
                    {
                        CertificateVacateSpecialWireUserControl reportUC = new CertificateVacateSpecialWireUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.SpecialWireCertificatePrintReport:
                    {
                        CertificateSpecialWirePrintReportUserControl reportUC = new CertificateSpecialWirePrintReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.MalfuctionHistoryReport:
                    {
                        //TODO:rad
                        MalfuctionHistoryReportUserControl reportUC = new MalfuctionHistoryReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.OutOfBoundRequestReport:
                    {
                        //TODO:rad
                        OutOfBoundRequestReportUserControl reportUC = new OutOfBoundRequestReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.RequestStateReport:
                    {

                        RequestStateUserControl reportUC = new RequestStateUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.WorkingTelephoneStatisticsBySwitchTypeReport:
                    {
                        //TODO:rad
                        WorkingTelephoneStatisticsBySwitchTypeReportUserControl reportUC = new WorkingTelephoneStatisticsBySwitchTypeReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPRequestMontlyReport:
                    {

                        PAPRequestMontlyReport reportUC = new PAPRequestMontlyReport();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListCustomer:
                    {

                        BlackListCustomerReportUserControl reportUC = new BlackListCustomerReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListAddress:
                    {

                        BlackListAddressReportUserControl reportUC = new BlackListAddressReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListTelephone:
                    {

                        BlackListTelephoneReportUserControl reportUC = new BlackListTelephoneReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.FilledCabinetReport:
                    {
                        //TODO:rad
                        FilledCabinetReportUserControl reportUC = new FilledCabinetReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeNameCertificateReport:
                    {
                        //TODO:rad
                        ChangeNameCertificateReportUserControl reportUC = new ChangeNameCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPTotalReport:
                    {
                        PAPTotalReport reportUC = new PAPTotalReport();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationCenterInsideCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationCenterInsideCertificateReportUserControl reportUC = new ChangeLocationCenterInsideCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationAndNameCenterInsideCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationAndNameCenterInsideCertificateReportUserControl reportUC = new ChangeLocationAndNameCenterInsideCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPTechnicalReport:
                    {
                        PAPTechnicalReport reportUC = new PAPTechnicalReport();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationCenterToCenterCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationCenterToCenterCertificateReportUserControl reportUC = new ChangeLocationCenterToCenterCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationAndNameCenterToCenterCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationAndNameCenterToCenterCertificateReportUserControl reportUC = new ChangeLocationAndNameCenterToCenterCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.RefundDepositCertificateReport:
                    {
                        //TODO:rad
                        RefundDepositCertificateReportUserControl reportUC = new RefundDepositCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeAddressCertificateReport:
                    {
                        //TODO:rad
                        ChangeAddressCertificateReportUserControl reportUC = new ChangeAddressCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.CutCertificateReport:
                    {
                        //TODO:rad
                        CutCertificateReportUserControl reportUC = new CutCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.EstablishCertificateReport:
                    {
                        EstablishCertificateReportUserControl reportUC = new EstablishCertificateReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(reportUC);
                        break;
                    }
                default:
                    {
                        PublicReportUserControl TempReport = new PublicReportUserControl();
                        SelectedReportTypeStackPanel.Children.Add(TempReport);
                        break;
                    }



            }
            #region
            #endregion
        }

        #endregion  Methods

    }
}
