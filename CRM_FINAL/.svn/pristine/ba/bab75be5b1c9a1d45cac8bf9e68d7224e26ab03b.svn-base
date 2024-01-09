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
    /// Interaction logic for ADSLSellerAgentComissionReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentComissionReportUserControl : Local.ReportBase
    {
       #region Constructor

        public ADSLSellerAgentComissionReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
        }

        #endregion Initializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
            ADSLSellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            ADSLSellerAgentComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ADSLSellerAgentItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            ADSLSellerAgentUserComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(ADSLSellerAgentComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLSellerAgentcomissionInfo> ResultADSLRequestCash = LoadADSLRequestCashData();
            List<ADSLSellerAgentcomissionInfo> ResultADSLREquestInstalment = LoadADSLRequestInstalmentData();
            List<ADSLSellerAgentcomissionInfo> ResultADSLChangeServiceCash = LoadADSLChangeServiceCashData();
            List<ADSLSellerAgentcomissionInfo> ResultADSLChangeServiceInstalment = LoadADSLChangeServiceInstalmentData();
            List<ADSLSellerAgentcomissionInfo> ResultADSLRequestTraffic = LoadADSLRequestTrafficCashData();
            List<ADSLSellerAgentcomissionInfo> ResultADSLSellTraffic = LoadADSLSellTrafficCashData();

            List<ADSLSellerAgentcomissionInfo> Result = ResultADSLRequestCash.Union(ResultADSLREquestInstalment).Union(ResultADSLChangeServiceCash).Union(ResultADSLChangeServiceInstalment)
               .Union(ResultADSLRequestTraffic).Union(ResultADSLSellTraffic).ToList();

            for (int j = 0; j < Result.Count;j++ )
            {
                for (int i = 0; i < Result.Count; i++)
                {
                    bool add = false;
                    if (i!=j && Result[j].ADSLSellerAgentUserName == Result[i].ADSLSellerAgentUserName && Result[j].ADSLSellerAgentName == Result[i].ADSLSellerAgentName && Result[j].CityName == Result[i].CityName && Result[j].CenterName == Result[i].CenterName)
                    {

                        if (Result[j].CashServiceAmount != null && Result[i].CashServiceAmount != null)
                        {
                            Result[j].CashServiceAmount += Result[i].CashServiceAmount;
                            add = true;
                        }

                        if (Result[j].CashServiceAmount == null && Result[i].CashServiceAmount != null)
                        {
                            Result[j].CashServiceAmount = Result[i].CashServiceAmount;
                            add = true;
                        }

                        if (Result[j].CashServiceComission != null && Result[i].CashServiceComission != null)
                        {
                            Result[j].CashServiceComission += Result[i].CashServiceComission;
                            add = true;
                        }

                        if (Result[j].CashServiceComission == null && Result[i].CashServiceComission != null)
                        {
                            Result[j].CashServiceComission = Result[i].CashServiceComission;
                            add = true;
                        }
                        if (Result[j].InstalmentServiceComission != null && Result[i].InstalmentServiceComission != null)
                        {
                            Result[j].InstalmentServiceComission += Result[i].InstalmentServiceComission;
                            add = true;
                        }
                        if (Result[j].InstalmentServiceComission == null && Result[i].InstalmentServiceComission != null)
                        {
                            Result[j].InstalmentServiceComission = Result[i].InstalmentServiceComission;
                            add = true;
                        }

                        if (Result[j].InstalmentServiceAmount != null && Result[i].InstalmentServiceAmount != null)
                        {
                            Result[j].InstalmentServiceAmount += Result[i].InstalmentServiceAmount;
                            add = true;
                        }
                        if (Result[j].InstalmentServiceAmount == null && Result[i].InstalmentServiceAmount != null)
                        {
                            Result[j].InstalmentServiceAmount = Result[i].InstalmentServiceAmount;
                            add = true;
                        }

                        if (Result[j].TrafficAmount == null && Result[i].TrafficAmount != null)
                        {
                            Result[j].TrafficAmount = Result[i].TrafficAmount;
                            add = true;
                        }
                        if (Result[j].TrafficComissionAmount == null && Result[i].TrafficComissionAmount != null)
                        {
                            Result[j].TrafficComissionAmount = Result[i].TrafficComissionAmount;
                            add = true;
                        }
                       


                        if (add == true)
                        {
                            Result.Remove(Result[i]);
                            i--;
                            //break;
                        }
                    }
                }
            }
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

            if (FromPaymentDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromPaymentDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToPaymentDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToPaymentDate.SelectedDate, Helper.DateStringType.Short).ToString();

            title = "پورسانت نمایندگان فروش ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLRequestCashData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLRequestCashInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);
            
           
            return result;
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLRequestInstalmentData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLRequestInstalmentInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);


            return result;
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLChangeServiceInstalmentData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLChangeServiceInstalmentInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);


            return result;
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLChangeServiceCashData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLChangeServiceCashInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);


            return result;
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLRequestTrafficCashData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLRequestTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);


            return result;
        }

        private List<ADSLSellerAgentcomissionInfo> LoadADSLSellTrafficCashData()
        {
            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLSellerAgentcomissionInfo> result = ReportDB.GetADSLSellerAgentComissionADSLSellTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                CenterComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentComboBox.SelectedIDs,
                                                                                                ADSLSellerAgentUserComboBox.SelectedIDs,
                                                                                                FromPaymentDate.SelectedDate,
                                                                                                toPaymentDate);


            return result;
        }
        #endregion Methods
    }
}
