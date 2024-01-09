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
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLCityCenterSaleStatisticsReportUserControl.xaml
    /// </summary>
    public partial class ADSLCityCenterSaleStatisticsReportUserControl :Local.ReportBase
    {
         #region Properties
         #endregion

        #region Constructor

        public ADSLCityCenterSaleStatisticsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            ServiceGroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);

        }

        #endregion Intitializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
        }


        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (ServiceGroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(ServiceGroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndPaymentTypeID(ServiceGroupComboBox.SelectedIDs, null);
            BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
        }


        void BandWidthItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            DurationComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceDurationByBandwidtIDs(BandWidthComboBox.SelectedIDs);
            DurationComboBox.ItemsComboBox.DropDownClosed += new EventHandler(DurationItemsComboBox_DropDownClosed);
        }

        void DurationItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (DurationComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            TrafficComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceTrafficCheckablebyDurationIDs(DurationComboBox.SelectedIDs);
            TrafficComboBox.ItemsComboBox.DropDownClosed += new EventHandler(TrafficItemsComboBox_DropDownClosed);
        }
        void TrafficItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (TrafficComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, ServiceGroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }
        #endregion

        public override void Search()
        {
            List<ADSLRequestInfo> Result = LoadData();
            List<ADSLRequestInfo> ResultADSLRequestPrePaid = LoadPrePaidData();
            List<ADSLRequestInfo> ResultADSLRequestPostPaid = LoadPostPaidData();
            List<ADSLRequestInfo> ResultADSLChangeSErvice = LoadALLADSlChangeServiceData();
            List<ADSLRequestInfo> ResultADSLChangeSErvicePrePaid = LoadPrePaidADSlChangeServiceData();
            List<ADSLRequestInfo> ResultADSLChangeServicePostPaid = LoadPostPaidADSlChangeServiceData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            for (int i = 0; i < Result.Count;i++ )
            {
                for (int j = 0; j < ResultADSLChangeSErvice.Count; j++)
                {
                    if(ResultADSLChangeSErvice[j].Center==Result[i].Center && ResultADSLChangeSErvice[j].CityName==Result[i].CityName)
                    {
                        Result[i].NumberOfSaledADSLService += ResultADSLChangeSErvice[j].NumberOfSaledADSLService;
                        Result[i].ServiceSaleAmount += ResultADSLChangeSErvice[j].ServiceSaleAmount;
                    }
                }
            }

            for (int i = 0; i < ResultADSLRequestPrePaid.Count; i++)
            {
                for (int j = 0; j < ResultADSLChangeSErvicePrePaid.Count; j++)
                {
                    if (ResultADSLChangeSErvicePrePaid[j].Center == ResultADSLRequestPrePaid[i].Center && ResultADSLChangeSErvicePrePaid[j].CityName == ResultADSLRequestPrePaid[i].CityName)
                    {
                        ResultADSLRequestPrePaid[i].NumberOfSoldADSLPrePaid += ResultADSLChangeSErvicePrePaid[j].NumberOfSoldADSLPrePaid;
                        ResultADSLRequestPrePaid[i].PrePaidAmountSum += ResultADSLChangeSErvicePrePaid[j].PrePaidAmountSum;
                    }
                }
            }

            for (int i = 0; i < ResultADSLRequestPostPaid.Count; i++)
            {
                for (int j = 0; j < ResultADSLChangeServicePostPaid.Count; j++)
                {
                    if (ResultADSLChangeServicePostPaid[j].Center == ResultADSLRequestPostPaid[i].Center && ResultADSLChangeServicePostPaid[j].CityName == ResultADSLRequestPostPaid[i].CityName)
                    {
                        ResultADSLRequestPostPaid[i].NumberOfSoldADSLPostPaid += ResultADSLChangeServicePostPaid[j].NumberOfSoldADSLPostPaid;
                        ResultADSLRequestPostPaid[i].PostPaidAmountSum += ResultADSLChangeServicePostPaid[j].PostPaidAmountSum;
                    }
                }
            }

            foreach (ADSLRequestInfo info in Result)
            {
                foreach(ADSLRequestInfo infoprepaid in ResultADSLRequestPrePaid)
                {
                    if(info.CityName==infoprepaid.CityName && info.Center==infoprepaid.Center)
                    {
                        info.NumberOfSoldADSLPrePaid = infoprepaid.NumberOfSoldADSLPrePaid;
                        info.PrePaidAmountSum = infoprepaid.PrePaidAmountSum;
                    }
                }
                foreach (ADSLRequestInfo infopostpaid in ResultADSLRequestPostPaid)
                {
                    if (info.CityName == infopostpaid.CityName && info.Center == infopostpaid.Center)
                    {
                        info.NumberOfSoldADSLPostPaid = infopostpaid.NumberOfSoldADSLPostPaid;
                        info.PostPaidAmountSum = infopostpaid.PostPaidAmountSum;
                    }
                }
            }


                title = "آمار فروش سرویس شهرستان مرکز به مرکز";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetALLADSLRequestCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }

        private List<ADSLRequestInfo> LoadPrePaidData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetPrePaidADSLRequestCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }

        private List<ADSLRequestInfo> LoadPostPaidData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetPostPaidADSLRequestCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }
        private List<ADSLRequestInfo> LoadALLADSlChangeServiceData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetALLADSLChangeServiceCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }

        private List<ADSLRequestInfo> LoadPrePaidADSlChangeServiceData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetPrePaidADSLChangeServiceCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }

        private List<ADSLRequestInfo> LoadPostPaidADSlChangeServiceData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result = ReportDB.GetPostPaidADSLChangeServiceCityCenterSaleStatistics(FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);
            return result;

        }
    }
}
