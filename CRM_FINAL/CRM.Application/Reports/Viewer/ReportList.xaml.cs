using CRM.Application.Codes.CustomControls;
using CRM.Application.Local;
using CRM.Application.Reports.ReportUserControls;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CRM.Application.Reports.Viewer
{
    /// <summary>
    /// Interaction logic for TestReportList.xaml
    /// </summary>
    public partial class ReportList : TabWindow
    {
        #region Properties And Fields

        private bool _isLoad;
        public bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }

        public int SelectedReportTemplateId { get; set; }

        public enum FormState
        {
            Nothing, Insert, Update
        }

        public FormState CurrentFormState { get; set; }

        public UserControl SelectedReportUserControl
        {
            get;
            set;
        }

        private ObservableCollection<ReportTemplateInfo> _reportTemplateInfos = new ObservableCollection<ReportTemplateInfo>();
        /// <summary>
        /// پراپرتی زیر به این علت ایجاد شد که لیست جنریک امکان گروه بندی بدون کوئری نمیباشد
        /// و ازآنجایی که لازم بود تا بدون هیچ کوئری لیست گزارشات بر اساس گروهشان دسته بندی شوند
        ///  view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        ///  لازم بود در زمل هم یک استایل برای گروه بندی در لیست باکس مربوطه ایجاد شود
        ///  GroupItemStyle
        /// </summary>
        public ObservableCollection<ReportTemplateInfo> ReportTemplateInfos
        {
            get { return _reportTemplateInfos; }
            set { _reportTemplateInfos = value; }
        }

        #endregion

        #region Constructor

        public ReportList()
        {
            InitializeComponent();

            this.CurrentFormState = FormState.Nothing;
            StiOptions.Engine.GlobalEvents.SavingReportInDesigner -= GlobalEvents_SavingReportInDesigner;
            StiOptions.Engine.GlobalEvents.SavingReportInDesigner += GlobalEvents_SavingReportInDesigner;
        }

        #endregion

        #region ImageButton Click EventHandlers

        private void ViewReportImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReportUserControl != null)
            {
                (SelectedReportUserControl as ReportBase).UserControlID = this.SelectedReportTemplateId;
                (SelectedReportUserControl as ReportBase).Search();
            }
        }

        private void StopReportImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewReportImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFormState == FormState.Nothing)
            {
                ReportDesignerForm reportDesignerForm = new ReportDesignerForm(true, -1);
                CurrentFormState = FormState.Insert;
                reportDesignerForm.ShowDialog();
                CurrentFormState = FormState.Nothing;
            }
            else
            {
                MessageBox.Show(".یک الگوی گزارش باز موجود است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditReportImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentFormState == FormState.Nothing)
            {
                ReportDesignerForm reportDesignerForm = new ReportDesignerForm(false, (int)SelectedReportTemplateId);
                CurrentFormState = FormState.Update;
                reportDesignerForm.ShowDialog();
                CurrentFormState = FormState.Nothing;
            }
            else
            {
                MessageBox.Show(".یک الگوی گزارش باز موجود است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteReportImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetReportImageButton_Click(object sender, RoutedEventArgs e)
        {
            UIElement container = SelectedReportTypeGrid as UIElement;
            Helper.ResetSearch(container);
            SearchTextBox.Clear();
            ReportTitleTextBlock.Text = string.Empty;
            ReportIcon.Source = null;
            SelectedReportTypeGrid.Children.Clear();
            this.SelectedReportTemplateId = 0;
            this.Reset();
        }

        #endregion

        #region EventHandlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.FillAvailableReportsListBox();
        }

        private void GlobalEvents_SavingReportInDesigner(object sender, Stimulsoft.Report.Design.StiSavingObjectEventArgs e)
        {
            e.Processed = true;
            Save(((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray());
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            NoItemFoundTextBlock.Visibility = Visibility.Collapsed;

            AvailableReportsListBox.Items.Filter += delegate(object currentItem)
            {
                ReportTemplateInfo currentReportItem = currentItem as ReportTemplateInfo;
                int index = currentReportItem.Title.IndexOf(SearchTextBox.Text.Trim(), 0);
                return (index > -1);
            };

            if (AvailableReportsListBox.Items.Count == 0)
            {
                NoItemFoundTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void ReportOperationsGrid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (ImageButton child in ReportOperationsGrid.Children.OfType<ImageButton>())
            {
                //چنانچه هر کدام از باتن ها فعال نباشند میبایست افکت خاکستری بگیرد - برای نمایش بهنر غیر فعال بودن
                if (!child.IsEnabled)
                {
                    child.Effect = new GreyscaleEffect();
                }
            }
        }

        private void ExtendedMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Reset();
            this.SelectedReportTemplateId = Convert.ToInt32((sender as ExtendedMenuItem).Tag);

            ExtendedMenuItem currentMenuItem = sender as ExtendedMenuItem;
            currentMenuItem.Background = Brushes.Gray;
            currentMenuItem.Foreground = Brushes.Yellow;
            currentMenuItem.FontWeight = FontWeights.DemiBold;
            currentMenuItem.FontSize = 14;

            ReportTitleTextBlock.Text = (sender as ExtendedMenuItem).Text;
            ReportIcon.Source = Helper.GetBitmapImage(ReportDB.GetReportIconName(this.SelectedReportTemplateId));

            ProvideSelectedReport(this.SelectedReportTemplateId);
            this.SelectedReportUserControl = SelectedReportTypeGrid.Children.OfType<UserControl>().First();
        }

        private void ExtendedMenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            ExtendedMenuItem currentMenuItem = (sender as ExtendedMenuItem);
            if (this.SelectedReportTemplateId == Convert.ToInt32(currentMenuItem.Tag))
            {
                currentMenuItem.Background = Brushes.Gray;
            }
            else
            {
                currentMenuItem.FontWeight = FontWeights.SemiBold;
            }
        }

        private void ExtendedMenuItem_MouseLeave(object sender, MouseEventArgs e)
        {
            ExtendedMenuItem currentMenuItem = (sender as ExtendedMenuItem);
            if (this.SelectedReportTemplateId == Convert.ToInt32(currentMenuItem.Tag))
            {
                return;
            }
            else
            {
                currentMenuItem.FontWeight = FontWeights.Normal;
            }
        }

        #endregion

        #region Methods

        private void FillAvailableReportsListBox()
        {
            try
            {
                if (this.IsLoad)
                {
                    return;
                }
                else
                {
                    IsLoad = true;
                }

                //در بلاک زیر گزارش های مربوط به هر گروه ایجاد شده و در داخل گروه مربوطه قرار میگیرند
                foreach (ReportTemplateInfo report in ReportDB.GetReportInfo())
                {
                    if (DB.CurrentUser.ReportTemplateIDs.Contains(report.ID))
                    {
                        this.ReportTemplateInfos.Add(report);
                    }
                }

                ICollectionView view = CollectionViewSource.GetDefaultView(this.ReportTemplateInfos);
                view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
                AvailableReportsListBox.ItemsSource = view;
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در ایجاد لیست گزارش ها ");
            }
        }

        private void ProvideSelectedReport(int reportTemplateId)
        {
            SelectedReportTypeGrid.Children.Clear();
            switch (reportTemplateId)
            {
                case ((int)DB.UserControlNames.ADSL):
                    {
                        ADSLReportUserControl ReportUC = new ADSLReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLEquipment):
                    {
                        ADSLEquipmentReportUserControl ReportUC = new ADSLEquipmentReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLRequest):
                    {
                        ADSLRequestReportUserControl ReportUC = new ADSLRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeNumber):
                    {
                        ChangeNumberReportUserControl ReportUC = new ChangeNumberReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLDayeriRequest):
                    {
                        ADSLDayeriRequestReportUserControl ReportUC = new ADSLDayeriRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DisconnectAndConnectCount):
                    {
                        DisconnectAndConnectCountReportUserControl ReportUC = new DisconnectAndConnectCountReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PapADSLRequest):
                    {
                        PapADSLRequestReportUserControl ReportUC = new PapADSLRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLOfficialDelay):
                    {
                        ADSLOfficialDelayReportUserControl ReportUC = new ADSLOfficialDelayReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLStatistic):
                    {
                        ADSLStatisticReportUserControl ReportUC = new ADSLStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeName):
                    {
                        ChangeNameReportUserControl ReportUC = new ChangeNameReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DayeriRequest):
                    {
                        DayeriRequestReportUserControl ReportUC = new DayeriRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PapRequestOperation):
                    {
                        PapRequestOperationReportUserControl ReportUC = new PapRequestOperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InvestigatePossibility):
                    {
                        InvestigatePossibilityReportUserControl ReportUC = new InvestigatePossibilityReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ReDayeriRequest):
                    {
                        ReDayeriRequestReportUserControl ReportUC = new ReDayeriRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.IssueWiring):
                    {
                        IssueWiringReportUserControl ReportUC = new IssueWiringReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ZeroStatus):
                    {
                        ZeroStatusReportUserControl ReportUC = new ZeroStatusReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeTitleIn118):
                    {
                        ChangeTitleIn118ReportUserControl ReportUC = new ChangeTitleIn118ReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpaceAndPower):
                    {
                        SpaceAndPowerReportUserControl ReportUC = new SpaceAndPowerReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpecialService):
                    {
                        SpecialServiceReportUserControl ReportUC = new SpecialServiceReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeLocation):
                    {
                        ChangeLocationReportUserControl ReportUC = new ChangeLocationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalCenterCabinetInfo):
                    {
                        CenterCabinetInfoReportUserControl ReportUC = new CenterCabinetInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.DetailsCenterCabinetInfo):
                    {
                        CenterCabinetInfoReportUserControl ReportUC = new CenterCabinetInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostInfoTotal):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoDetails):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetCentersInfo):
                    {
                        CabinetCentersInfoUserControl ReportUC = new CabinetCentersInfoUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                //case ((int)DB.UserControlNames.PostContacts):
                //    {
                //        PostContactsReportUserControl ReportUC = new PostContactsReportUserControl();
                //        SelectedReportTypeGrid.Children.Add(ReportUC);
                //        break;
                //    }
                case ((int)DB.UserControlNames.PCMContactsStatistic):
                    {
                        PCMContactsStatisticReportUserControl ReportUC = new PCMContactsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMContactsPostStatistic):
                    {
                        PCMContactsPostStatisticReportUserControl ReportUC = new PCMContactsPostStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMsStatistic):
                    {
                        PCMsStatisticReportUserControl ReportUC = new PCMsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PCMStatisticEquipment):
                    {
                        PCMStatisticEquipmentReportUserControl ReportUC = new PCMStatisticEquipmentReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.AllPCMs):
                    {
                        PCMReportUserControl ReportUC = new PCMReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Failure117Requests):
                    {
                        Failure117RequestsReportUserControl ReportUC = new Failure117RequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Status_DateSendingFailure117Requests):
                    {
                        SendingFailure117RequestsReportUserControl ReportUC = new SendingFailure117RequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetInputFailure):
                    {
                        CabinetInputFailureReportUserControl ReportUC = new CabinetInputFailureReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InstallPCM):
                    {
                        InstallPCMReportUserControl ReportUC = new InstallPCMReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EmptyPCMs):
                    {
                        EmptyPCMsReportUserControl ReportUC = new EmptyPCMsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SendingFailure117RequestsToNetworkCable):
                    {
                        SendingFailure117ToNetworlAndCableReportUserControl ReportUC = new SendingFailure117ToNetworlAndCableReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalCabinetInputFailure):
                    {
                        TotalCabinetInputFailureReportUserControl ReportUC = new TotalCabinetInputFailureReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureTimeTable):
                    {
                        FailureTimeTableReportUserControl ReportUC = new FailureTimeTableReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetCapacity):
                    {
                        CabinetCapacityReportUserControl ReportUC = new CabinetCapacityReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TotalStatisticPCMPorts):
                    {
                        TotalStatisticPCMPortsReportUserControl ReportUC = new TotalStatisticPCMPortsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CenterCablesTotal):
                    {
                        CenterCableReportUserControl ReportUC = new CenterCableReportUserControl(false);
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CenterCablesDetails):
                    {
                        CenterCableReportUserControl ReportUC = new CenterCableReportUserControl(true);
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.InputStatistic):
                    {
                        InputStatisticReportUserControl ReportUC = new InputStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.VerticalBuchtsStatistic):
                    {
                        VerticalBuchtsStatisticReportUserControl ReportUC = new VerticalBuchtsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.FailurePortsStatistic):
                    {
                        FailurePortsStatisticReportUserControl ReportUC = new FailurePortsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureCorrectPorts):
                    {
                        FailureCorrectPortsReportUserControl ReportUC = new FailureCorrectPortsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.FailureNotCorrectPorts):
                    {
                        FailureNotCorrectPortsReportUserControl ReportUC = new FailureNotCorrectPortsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallAndDisChargePapCompany):
                    {
                        InstallAndDisChargePapCompanyReportUserControl ReportUC = new InstallAndDisChargePapCompanyReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failurePostContacts):
                    {
                        failurePostContactsReportUserControl ReportUC = new failurePostContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureCorrectedPostContacts):
                    {
                        failureCorrectedPostContactsReportUserControl ReportUC = new failureCorrectedPostContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureNotCorrectedPostContacts):
                    {
                        failureNotCorrectedPostContactsReportUserControl ReportUC = new failureNotCorrectedPostContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.failureCabinetInputReport):
                    {
                        failureCabinetInputsReportUserControl ReportUC = new failureCabinetInputsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureCorrectedCabinetInputs):
                    {
                        FailureCorrectCabinetInputReportUserControl ReportUC = new FailureCorrectCabinetInputReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.failureNotCorrectedCabinetInputs):
                    {
                        FailureNotCorrectCabinetInputReportUserControl ReportUC = new FailureNotCorrectCabinetInputReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SpaceAndPowerPapCompany):
                    {
                        SpaceAndPowerPapCompanyReportUserControl ReportUC = new SpaceAndPowerPapCompanyReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CablePair):
                    {
                        CablePairUserControl ReportUC = new CablePairUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.HorizintalBuchtsStatistic):
                    {
                        HorizintalBuchtsStatisticReportUserControl ReportUC = new HorizintalBuchtsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EmptyTelephone):
                    {
                        EmptyTelephoneReportUserControl ReportUC = new EmptyTelephoneReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ResignationLines):
                    {
                        ResignationLinesStatisticReportUserControl ReportUC = new ResignationLinesStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TelephoneWithOutPCM):
                    {
                        TelephoneWithOutPCMReportUserControl ReportUC = new TelephoneWithOutPCMReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PCMInPost):
                    {
                        PCMInpostReportUserControl ReportUC = new PCMInpostReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ReleaseDocuments):
                    {
                        ReLeaseFileIDReportUserControl ReportUC = new ReLeaseFileIDReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PerformanceFailure117):
                    {
                        PerformanceFailure117ReportUserControl ReportUC = new PerformanceFailure117ReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallRequestForm):
                    {
                        DayeriRequestFormReportUserControl ReportUC = new DayeriRequestFormReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ChangeBucht):
                    {
                        ChangeBuchtReportUserControl ReportUC = new ChangeBuchtReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.EquipmentBilling):
                    {
                        EquipmentBillingReportUserControl ReportUC = new EquipmentBillingReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.TinyBillingOptions):
                    {
                        TinyBillingOptionsReportUserControl ReportUC = new TinyBillingOptionsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PerformanceWiringNetwork):
                    {
                        PerformanceWiringNetworkReportUserControl ReportUC = new PerformanceWiringNetworkReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.SendingFailure117Cable):
                    {
                        SendingFailure117CableReportUserControl ReportUC = new SendingFailure117CableReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoCable):
                    {
                        PostInfoReportUserControl ReportUC = new PostInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostInfoReserve):
                    {
                        PostInfoReserveReportUserControl ReportUC = new PostInfoReserveReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PostInfoFill):
                    {
                        PostInfoFillReportUserControl ReportUC = new PostInfoFillReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterCabinet_Subset):
                    {
                        CenterCabinetSubsetReportUserControl ReportUC = new CenterCabinetSubsetReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterCabinet_CabinetSyndeticOrder):
                    {
                        CenterCabinet_CabinetSyndeticOrderReportUserControl ReportUC = new CenterCabinet_CabinetSyndeticOrderReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RequestPayment):
                    {
                        RequestPaymentUserControl ReportUC = new RequestPaymentUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceReport):
                    {
                        ADSLServiceReportUserControl ReportUC = new ADSLServiceReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTelephoneNoHistoryReport):
                    {
                        ADSLTelephoneNoHistoryReportUserControl ReportUC = new ADSLTelephoneNoHistoryReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentReport):
                    {
                        ADSLSellerAgentReportUserControl ReportUC = new ADSLSellerAgentReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLHistoryReport):
                    {
                        ADSLHistoryReportUserControl ReportUC = new ADSLHistoryReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCustomerInfoReport):
                    {
                        ADSLCustomerInfoReportUserControl ReportUC = new ADSLCustomerInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPAPEquipmentInfo):
                    {
                        PAPEquipmentReportUserControl ReportUC = new PAPEquipmentReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPaymentReport):
                    {
                        ADSLPaymentReportUserControl ReportUC = new ADSLPaymentReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstallmentByExpertReport):
                    {
                        ADSLInstallmentCostByExpertReportUserControl ReportUC = new ADSLInstallmentCostByExpertReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDayeriDischargeReport):
                    {
                        ADSLDayeriDischargeReportUserControl ReportUC = new ADSLDayeriDischargeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLGeneralCustomerInforeport):
                    {
                        ADSLGeneralCustomerInfoReportUserControl ReportUC = new ADSLGeneralCustomerInfoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberSaleReport):
                    {
                        ADSLNumberSaleReportUserControl ReportUC = new ADSLNumberSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSaleReport):
                    {
                        ADSLSaleReportUserControl ReportUC = new ADSLSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLBandwidthReport):
                    {
                        ADSLBandwidthReportUserControl ReportUC = new ADSLBandwidthReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLReadyToInstallCustomersReport):
                    {
                        ADSLReadyToInstallCustomersReportUserControl ReportUC = new ADSLReadyToInstallCustomersReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCustomersDeliveredModemReport):
                    {
                        ADSLCustomersDeliveredModemReportUserControl ReportUC = new ADSLCustomersDeliveredModemReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityCenterSaleStatisticsReport):
                    {
                        ADSLCityCenterSaleStatisticsReportUserControl ReportUC = new ADSLCityCenterSaleStatisticsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCitySellerSaleStatisticsReport):
                    {
                        ADSLCitySellerSaleStatisticsReportUserControl ReportUC = new ADSLCitySellerSaleStatisticsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleReport):
                    {
                        ADSLSellerAgentSaleReportUserControl ReportUC = new ADSLSellerAgentSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCenterSaleReport):
                    {
                        ADSLCenterSaleReportUserControl ReportUC = new ADSLCenterSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCitySaleReport):
                    {
                        ADSLCitySaleReportUserControl ReportUC = new ADSLCitySaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLOnlineDailyCitySaleReport):
                    {
                        ADSLOnlineDailyCitySaleReportUserControl ReportUC = new ADSLOnlineDailyCitySaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityDailyDischargeReport):
                    {
                        ADSLCityDailyDischargeReportUserControl ReportUC = new ADSLCityDailyDischargeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCenterGeneralContactsReport):
                    {
                        ADSLCenterGeneralContactsReportUserControl ReportUC = new ADSLCenterGeneralContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLMonthlyComparisonDiagram91):
                    {
                        ADSLMonthlyComparisonDiagram91UserControl ReportUC = new ADSLMonthlyComparisonDiagram91UserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityContactsGeneralReport):
                    {
                        ADSLCityContactsGeneralReportUserControl ReportUC = new ADSLCityContactsGeneralReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCityWeeklyContactsReport):
                    {
                        ADSLCityWeeklyContactsReportUserControl ReportUC = new ADSLCityWeeklyContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLWeeklyComparisionDiagramContactsPAP):
                    {
                        ADSLWeeklyComparisionDiagramContactsPAPUserControl ReportUC = new ADSLWeeklyComparisionDiagramContactsPAPUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSoldModemReport):
                    {
                        ADSLSoldModemReportUserControl ReportUC = new ADSLSoldModemReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLDayeriRequest):
                    {
                        PAPDayeriRequestsReportUserControl ReportUC = new PAPDayeriRequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLDischargeRequest):
                    {
                        PAPDischargeRequestsReportUserControl ReportUC = new PAPDischargeRequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PAPADSLChangePortRequest):
                    {
                        PAPChangePortRequestsReportUserControl ReportUC = new PAPChangePortRequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLCityMonthlyReport):
                    {
                        ADSLCityMonthlyReportUserControl ReportUC = new ADSLCityMonthlyReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPortsCityCenterReport):
                    {
                        ADSLPortsCityCenterReportUserControl ReportUC = new ADSLPortsCityCenterReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSaleServiceAndADSLSellChanellReport):
                    {
                        ADSLSaleServiceAndADSLSellChanellReportUserControl ReportUC = new ADSLSaleServiceAndADSLSellChanellReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLIncomeSellChanellAndTimeReport):
                    {
                        ADSLIncomeSellChanellAndTimeReportUserControl ReportUC = new ADSLIncomeSellChanellAndTimeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLChangeServiceReportUserControl):
                    {
                        ADSLChangeServiceReportUserControl ReportUC = new ADSLChangeServiceReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLAdditionalServiceSaleReport):
                    {
                        ADSLAdditionalServiceSaleReportUserControl ReportUC = new ADSLAdditionalServiceSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstalledADSLReport):
                    {
                        ADSLInstalledADSLReportUserControl ReportUC = new ADSLInstalledADSLReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLMostSoldServicesReport):
                    {
                        ADSLMostSoldServicesReportUserControl ReportUC = new ADSLMostSoldServicesReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDischargeRequestsReport):
                    {
                        ADSLDischargeRequestsReportUserControl ReportUC = new ADSLDischargeRequestsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeReport):
                    {
                        DischargeReportUserControl ReportUC = new DischargeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.Report118):
                    {
                        Report118UserControl ReportUC = new Report118UserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeNoReport):
                    {
                        ChangeNoReportUserControl ReportUC = new ChangeNoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CutAndEstablishReport):
                    {
                        CutAndEstablishReportUserControl ReportUC = new CutAndEstablishReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstalmentRequestPaymentReport):
                    {
                        InstalmentRequestReportUserControl ReportUC = new InstalmentRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.GenrealCostReport):
                    {
                        GenrealCostReportUserControl ReportUC = new GenrealCostReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.FineToFineCostReport):
                    {
                        FineToFineCostReportUserControl ReportUC = new FineToFineCostReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.InstallRequestReport):
                    {
                        InstallRequestReportUserControl ReportUC = new InstallRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RegisterReport):
                    {
                        RegisterReportUserControl ReportUC = new RegisterReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.SpecialServiceStatisticsReport):
                    {
                        SpecialServiceStatisticsReportUserControl ReportUC = new SpecialServiceStatisticsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.BlockingReport):
                    {
                        BlockingReportUserControl ReportUC = new BlockingReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLPortsReport):
                    {
                        ADSLPortsReportUserControl ReportUC = new ADSLPortsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentADSLRequestSaleDetailesReport):
                    {
                        ADSLSellerAgentADSLRequestSaleDetailesReportUserControl ReportUC = new ADSLSellerAgentADSLRequestSaleDetailesReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLOnlineRegistrationReport):
                    {
                        ADSLOnlineRegistrationReportUserControl ReportUC = new ADSLOnlineRegistrationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSCityCenterDailyReport):
                    {
                        ADSCityCenterDailyReportUserControl ReportUC = new ADSCityCenterDailyReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLIntranetDailySaleReport):
                    {
                        ADSLIntranetDailySaleReportUserControl ReportUC = new ADSLIntranetDailySaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCityDailyIncomeReport):
                    {
                        ADSLCityDailyIncomeReportUserControl ReportUC = new ADSLCityDailyIncomeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceSaleReport):
                    {
                        ADSLServiceSaleReportUserControl ReportUC = new ADSLServiceSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficSaleReport):
                    {
                        ADSLTrafficSaleReportUserControl ReportUC = new ADSLTrafficSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberOfDayeriDischargeReshargeReport):
                    {
                        ADSLNumberOfDayeriDischargeReshargeReportUserControl ReportUC = new ADSLNumberOfDayeriDischargeReshargeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLSellerAgentUsersSaleAmountReport):
                    {
                        ADSLSellerAgentUsersSaleAmountReportUserControl ReportUC = new ADSLSellerAgentUsersSaleAmountReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLNumberOfDayeriExpirationReport):
                    {
                        ADSLNumberOfDayeriExpirationReportUserControl ReportUC = new ADSLNumberOfDayeriExpirationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLExpirationDateAndLastServiceTelephoneNoReport):
                    {
                        ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl ReportUC = new ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLCenterPortStatusReport):
                    {
                        ADSLCenterPortStatusReportUserControl ReportUC = new ADSLCenterPortStatusReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentCashSaleReport):
                    {
                        ADSLSellerAgentCashSaleReportUserControl ReportUC = new ADSLSellerAgentCashSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleDetailsReport):
                    {
                        ADSLSellerAgentSaleDetailsReportUserControl ReportUC = new ADSLSellerAgentSaleDetailsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSeviceSaleBandwidthSeparationReport):
                    {
                        ADSLSeviceSaleBandwidthSeparationReportUserControl ReportUC = new ADSLSeviceSaleBandwidthSeparationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.ADSLTrafficSaleTrafficSeperationReport):
                    {
                        ADSLTrafficSaleTrafficSeperationReportUserControl ReportUC = new ADSLTrafficSaleTrafficSeperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceSaleCustomerAndServiceSperationReport):
                    {
                        ADSLServiceSaleCustomerAndServiceSperationReportUserControl ReportUC = new ADSLServiceSaleCustomerAndServiceSperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLServiceAggragateSaleCenterSeperationReport):
                    {
                        ADSLServiceAggragateSaleCenterSeperationReportUserControl ReportUC = new ADSLServiceAggragateSaleCenterSeperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficSaleCustomerSeperationReport):
                    {
                        ADSLTrafficSaleCustomerSeperationReportUserControl ReportUC = new ADSLTrafficSaleCustomerSeperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLTrafficAggregateSaleCenterCostCodeSeperationReport):
                    {
                        ADSLTrafficAggregateSaleCenterCostCodeSeperationReportUserControl ReportUC = new ADSLTrafficAggregateSaleCenterCostCodeSeperationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLModemAggragateSaleReport):
                    {
                        ADSLModemAggragateSaleReportUserControl ReportUC = new ADSLModemAggragateSaleReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInformationReport):
                    {
                        ADSLInformationReportUserControl ReportUC = new ADSLInformationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.ADSLInstalmetTabReport):
                    {
                        ADSLInstalmetTabReportUserControl ReportUC = new ADSLInstalmetTabReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentSaleDetailsGroupByTelephoneNoReport):
                    {
                        ADSLSellerAgentSaleDetailsGroupByTelephoneNoReportUserControl ReportUC = new ADSLSellerAgentSaleDetailsGroupByTelephoneNoReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.RefundDepositReport):
                    {
                        RefundDepositReportUserControl ReportUC = new RefundDepositReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.PersonTypeReport):
                    {
                        PersonTypeReportUserControl ReportUC = new PersonTypeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.EmptyTelephoneNoLisReport):
                    {
                        EmptyTelephoneNoLisReportUserControl ReportUC = new EmptyTelephoneNoLisReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeWiringNetworkReport):
                    {
                        DischargeWiringNetwork_ReportUserControl ReportUC = new DischargeWiringNetwork_ReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.DischargeCertificateReport):
                    {
                        //TODO:rad
                        DischargeCertificateReportUserControl reportUC = new DischargeCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeNoCertificateReport):
                    {
                        //TODO:rad
                        ChangeNoCertificateReportUserControl ReportUC = new ChangeNoCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ChangeLocationCenterInsideWiringReport):
                    {
                        ChangeLocationCenterInsideWiringReportUserControl ReportUC = new ChangeLocationCenterInsideWiringReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInformationSystemReport):
                    {
                        ADSLInformationSystemReportUserControl ReportUC = new ADSLInformationSystemReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLDischargeDetailsReport):
                    {
                        ADSLDischargeDetailsReportUserControl ReportUC = new ADSLDischargeDetailsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TelephoneRequestLogReport):
                    {
                        TelephoneRequestLogReportUserControl ReportUC = new TelephoneRequestLogReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLModemInformationReport):
                    {
                        ADSLModemInformationReportUserControl ReportUC = new ADSLModemInformationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentComissionReport):
                    {
                        ADSLSellerAgentComissionReportUserControl ReportUC = new ADSLSellerAgentComissionReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CustomerPersonalInformationReport):
                    {
                        CustomerPersonalInformationReportUserControl ReportUC = new CustomerPersonalInformationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLSellerAgentUsersCreditReport):
                    {
                        ADSLSellerAgentUsersCreditReportUserControl ReportUC = new ADSLSellerAgentUsersCreditReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ADSLInstallmentRequestPaymentTelephoneNo):
                    {
                        ADSLInstallmentRequestPaymentTelephoneNo ReportUC = new ADSLInstallmentRequestPaymentTelephoneNo();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TranslationPostReport):
                    {
                        TranslationPostReportUserControl ReportUC = new TranslationPostReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.TranslationPostInputReport):
                    {
                        TranslationPostInputReportUserControl ReportUC = new TranslationPostInputReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ExchangeCabinetInputReport):
                    {
                        ExchangeCabinetInputReportUserControl ReportUC = new ExchangeCabinetInputReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.ExchangeCentralCableMDFReport):
                    {
                        ExchangeCentralCableMDFReportUserControl ReportUC = new ExchangeCentralCableMDFReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.CenterToCenterTranslationReport):
                    {
                        CenterToCenterTranslationReportUserControl ReportUC = new CenterToCenterTranslationReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.BuchtSwitchingReport):
                    {
                        BuchtSwitchingReportUserControl ReportUC = new BuchtSwitchingReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.WorkingTelephone):
                    {
                        WorkingTelephone ReportUC = new WorkingTelephone();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }


                case ((int)DB.UserControlNames.RoundTelephone):
                    {
                        RoundTelephone ReportUC = new RoundTelephone();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.WarningHistoryReport):
                    {
                        WarningHistoryReport ReportUC = new WarningHistoryReport();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CustomerNationalCodeReport):
                    {
                        CustomerNationalCodeReport ReportUC = new CustomerNationalCodeReport();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.WorkingTelephoneBaseDate):
                    {
                        WorkingTelephoneBaseDate ReportUC = new WorkingTelephoneBaseDate();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CustomerOffice):
                    {
                        CustomerOfficeReport ReportUC = new CustomerOfficeReport();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.PostContactTotal):
                case ((int)DB.UserControlNames.PostContactReserve):
                case ((int)DB.UserControlNames.PostContactFill):
                case ((int)DB.UserControlNames.PostContactFail):
                case ((int)DB.UserControlNames.PostContactEmpty):
                    {
                        PostContactsStatisticsReportUserControl ReportUC = new PostContactsStatisticsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CabinetInputEmpty):
                case ((int)DB.UserControlNames.CabinetInputReserve):
                case ((int)DB.UserControlNames.CabinetInputFill):
                case ((int)DB.UserControlNames.CabinetInputFail):
                case ((int)DB.UserControlNames.CabinetInputTotal):
                    {
                        InputStatisticReportUserControl ReportUC = new InputStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }

                case ((int)DB.UserControlNames.VerticalBuchtEmpty):
                case ((int)DB.UserControlNames.VerticalBuchtReserve):
                case ((int)DB.UserControlNames.VerticalBuchtFill):
                case ((int)DB.UserControlNames.VerticalBuchtFail):
                case ((int)DB.UserControlNames.VerticalBuchtTotal):
                    {
                        VerticalBuchtsStatisticReportUserControl ReportUC = new VerticalBuchtsStatisticReportUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.CablePairEmpty):
                case ((int)DB.UserControlNames.CablePairReserve):
                case ((int)DB.UserControlNames.CablePairFill):
                case ((int)DB.UserControlNames.CablePairFail):
                case ((int)DB.UserControlNames.CablePairTotal):
                    {
                        CablePairUserControl ReportUC = new CablePairUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.TranslationOpticalCabinetToNormalRequestReport):
                    {
                        //TODO:rad
                        TranslationOpticalCabinetToNormalRequestUserControl reportUC = new TranslationOpticalCabinetToNormalRequestUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                //case (int)(DB.UserControlNames.SpecialWireCertificatePrintReport):
                //    {
                //        SpecialWireCertificatePrintUserControl reportUC = new SpecialWireCertificatePrintUserControl();
                //        SelectedReportTypeGrid.Children.Add(reportUC);
                //        break;
                //    }
                case (int)(DB.UserControlNames.SwapTelephoneReport):
                    {
                        //TODO:rad
                        SwapTelephoneReportUserControl reportUC = new SwapTelephoneReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.SwapPCMReport):
                    {
                        //TODO:rad
                        SwapPCMReportUserControl reportUC = new SwapPCMReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)(DB.UserControlNames.ModifyProfileReport):
                    {
                        //TODO:rad
                        ModifyProfileReportUserControl reportUC = new ModifyProfileReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case ((int)DB.UserControlNames.Failure117TotalInfoUserControl):
                    {
                        Failure117TotalInfoUserControl ReportUC = new Failure117TotalInfoUserControl();
                        SelectedReportTypeGrid.Children.Add(ReportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PostContacts:
                    {
                        //TODO:rad
                        PostContactsReportUserControl reportUC = new PostContactsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.Failure117RequestRemaindInNetwork:
                    {
                        Failure117RequestRemaindInNetworkReportUserControl reportUC = new Failure117RequestRemaindInNetworkReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }


                case (int)DB.UserControlNames.ChangeLocationSpecialWireCertificate:
                    {
                        CertificateChangeLocationSpecialWireUserControl reportUC = new CertificateChangeLocationSpecialWireUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.VacateSpecialWireCertificate:
                    {
                        CertificateVacateSpecialWireUserControl reportUC = new CertificateVacateSpecialWireUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.SpecialWireCertificatePrintReport:
                    {
                        CertificateSpecialWirePrintReportUserControl reportUC = new CertificateSpecialWirePrintReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.MalfuctionHistoryReport:
                    {
                        //TODO:rad
                        MalfuctionHistoryReportUserControl reportUC = new MalfuctionHistoryReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.OutOfBoundRequestReport:
                    {
                        //TODO:rad
                        OutOfBoundRequestReportUserControl reportUC = new OutOfBoundRequestReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.RequestStateReport:
                    {

                        RequestStateUserControl reportUC = new RequestStateUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.WorkingTelephoneStatisticsBySwitchTypeReport:
                    {
                        //TODO:rad
                        WorkingTelephoneStatisticsBySwitchTypeReportUserControl reportUC = new WorkingTelephoneStatisticsBySwitchTypeReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPRequestMontlyReport:
                    {

                        PAPRequestMontlyReport reportUC = new PAPRequestMontlyReport();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListCustomer:
                    {

                        BlackListCustomerReportUserControl reportUC = new BlackListCustomerReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListAddress:
                    {

                        BlackListAddressReportUserControl reportUC = new BlackListAddressReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.BlackListTelephone:
                    {

                        BlackListTelephoneReportUserControl reportUC = new BlackListTelephoneReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.FilledCabinetReport:
                    {
                        //TODO:rad
                        FilledCabinetReportUserControl reportUC = new FilledCabinetReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeNameCertificateReport:
                    {
                        //TODO:rad
                        ChangeNameCertificateReportUserControl reportUC = new ChangeNameCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPTotalReport:
                    {
                        PAPTotalReport reportUC = new PAPTotalReport();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationCenterInsideCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationCenterInsideCertificateReportUserControl reportUC = new ChangeLocationCenterInsideCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationAndNameCenterInsideCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationAndNameCenterInsideCertificateReportUserControl reportUC = new ChangeLocationAndNameCenterInsideCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.PAPTechnicalReport:
                    {
                        PAPTechnicalReport reportUC = new PAPTechnicalReport();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationCenterToCenterCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationCenterToCenterCertificateReportUserControl reportUC = new ChangeLocationCenterToCenterCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeLocationAndNameCenterToCenterCertificateReport:
                    {
                        //TODO:rad
                        ChangeLocationAndNameCenterToCenterCertificateReportUserControl reportUC = new ChangeLocationAndNameCenterToCenterCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.RefundDepositCertificateReport:
                    {
                        //TODO:rad
                        RefundDepositCertificateReportUserControl reportUC = new RefundDepositCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.ChangeAddressCertificateReport:
                    {
                        //TODO:rad
                        ChangeAddressCertificateReportUserControl reportUC = new ChangeAddressCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.CutCertificateReport:
                    {
                        //TODO:rad
                        CutCertificateReportUserControl reportUC = new CutCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.EstablishCertificateReport:
                    {
                        EstablishCertificateReportUserControl reportUC = new EstablishCertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.Failure117TotalInfoSemnanUserControl:
                    {
                        Failure117TotalInfoSemnanUserControl reportUC = new Failure117TotalInfoSemnanUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }

                case (int)DB.UserControlNames.TelecomminucationServicePaymentStatisticsReport:
                    {
                        //TODO:rad
                        TelecomminucationServicePaymentStatisticsReportUserControl reportUC = new TelecomminucationServicePaymentStatisticsReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.E1Certificate:
                    {
                        //TODO:rad
                        E1CertificateReportUserControl reportUC = new E1CertificateReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                case (int)DB.UserControlNames.GeneralRequestPaymentsDividedByCityCenterBaseCostReport:
                    {
                        //TODO:rad
                        GeneralRequestPaymentsDividedByCityCenterBaseCostReportUserControl reportUC = new GeneralRequestPaymentsDividedByCityCenterBaseCostReportUserControl();
                        SelectedReportTypeGrid.Children.Add(reportUC);
                        break;
                    }
                default:
                    {
                        PublicReportUserControl TempReport = new PublicReportUserControl();
                        SelectedReportTypeGrid.Children.Add(TempReport);
                        break;
                    }
            }
        }

        private void Reset()
        {
            foreach (var item in AvailableReportsListBox.Items)
            {
                var currentItem = AvailableReportsListBox.ItemContainerGenerator.ContainerFromItem(item);
                if (currentItem != null)
                {
                    ExtendedMenuItem emi = Helper.FindFirstElementInVisualTree<ExtendedMenuItem>(currentItem);
                    emi.FontSize = 12;
                    emi.FontWeight = FontWeights.Normal;
                    emi.Background = Brushes.Transparent;
                    emi.Foreground = Brushes.Black;

                    //در هنگام ریست شدن صفحه گزارش ها باید کلیه ی اکسپندر ها بسته شوند 
                    //Expander parentOfCurrentExtendedMenuItem = Helper.FindVisualParent<Expander>(emi);
                    //if (parentOfCurrentExtendedMenuItem != null)
                    //{
                    //    parentOfCurrentExtendedMenuItem.IsExpanded = false;
                    //}
                }
            }
        }
        
        private void Save(byte[] reportByteArray)
        {
            if (CurrentFormState == FormState.Update)
            {
                SaveReportForm form = new SaveReportForm(reportByteArray, SelectedReportTemplateId);
                form.ShowDialog();
            }
        }

        #endregion

    }
}
