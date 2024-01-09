using System;
using System.Collections.Generic;
using System.Windows.Documents;
using CRM.Data;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSellerAgentCashSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLSellerAgentCashSaleReportUserControl : Local.ReportBase
    {
        #region properties

        #endregion

        #region Constructor

        public ADSLSellerAgentCashSaleReportUserControl()
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
            IsAcceptedComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.IsAccepted));
        }

        #endregion Intitializer

        #region EventHAndler

        void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (CityComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
            SellerAgentComboBox.ItemsSource = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityIDs(CityComboBox.SelectedIDs);
            SellerAgentComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ADSLSellerAgentItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;

            SellerAgentUsersComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(SellerAgentComboBox.SelectedIDs);
        }
        #endregion

        #region Methods

        public override void Search()
        {

            List<ADSLSellerAgentCashIncomeInfo> ResultADSlRequest = LoadDataADSLRequest();
            List<ADSLSellerAgentCashIncomeInfo> ResultChangeService = LoadDataADSLChangeService();
            List<ADSLSellerAgentCashIncomeInfo> ResultChangeIP = LoadDataADSLChangeIP();
            List<ADSLSellerAgentCashIncomeInfo> ResultSellTraffic = LoadDataADSLSellTraffic();
            List<ADSLSellerAgentCashIncomeInfo> ResultADSLChangeNo = LoadDataADSLChangeNo();
            List<ADSLSellerAgentCashIncomeInfo> ResultADSLModemCost = LoadDataADSLModemCost();
            List<ADSLSellerAgentCashIncomeInfo> ResultADSLRequestInstament = LoadDataADSLRequestInstallment();
            List<ADSLSellerAgentCashIncomeInfo> ResultADSLRequestRanje = LoadDataADSLRequestRanje();
            List<ADSLSellerAgentCashIncomeInfo> Result = new List<ADSLSellerAgentCashIncomeInfo>();

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

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            if (ToDate.SelectedDate != null)
            {
                ToDate.SelectedDate.Value.AddDays(1);
            }

            for (int c = 0; c < ResultChangeService.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultChangeService[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultChangeService[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLChangeServiceCost = ResultChangeService[c].ADSLChangeServiceCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultChangeService[c]);
                }
            }

            for (int c = 0; c < ResultChangeIP.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultChangeIP[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultChangeIP[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLChangeIPCost = ResultChangeIP[c].ADSLChangeIPCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultChangeIP[c]);
                }
            }

            for (int c = 0; c < ResultSellTraffic.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultSellTraffic[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultSellTraffic[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLSellTrafficCost = ResultSellTraffic[c].ADSLSellTrafficCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultSellTraffic[c]);
                }


            }

            for (int c = 0; c < ResultADSLChangeNo.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultADSLChangeNo[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultADSLChangeNo[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLChangeNoCost = ResultADSLChangeNo[c].ADSLChangeNoCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultADSLChangeNo[c]);
                }
            }

            for (int c = 0; c < ResultADSLModemCost.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultADSLModemCost[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultADSLModemCost[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLModemCost = ResultADSLModemCost[c].ADSLModemCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultADSLModemCost[c]);
                }
            }

            for (int c = 0; c < ResultADSLRequestInstament.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultADSLRequestInstament[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultADSLRequestInstament[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLRequestInstallmentCost = ResultADSLRequestInstament[c].ADSLRequestInstallmentCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                {
                    ResultADSlRequest.Add(ResultADSLRequestInstament[c]);
                }


            }

            for (int c = 0; c < ResultADSLRequestRanje.Count; c++)
            {
                bool add = false;
                for (int i = 0; i < ResultADSlRequest.Count; i++)
                {
                    if (ResultADSlRequest[i].ADSlSellerAgnetName == ResultADSLRequestRanje[c].ADSlSellerAgnetName
                        && ResultADSlRequest[i].ADSLSEllerAgnetUserName == ResultADSLRequestRanje[c].ADSLSEllerAgnetUserName)
                    {
                        ResultADSlRequest[i].ADSLRequestRanjeCost = ResultADSLRequestRanje[c].ADSLRequestRanjeCost;
                        add = true;
                        break;
                    }
                }
                if (add == false)
                    ResultADSlRequest.Add(ResultADSLRequestRanje[c]);                
            }

            title = "گزارش ريز فروش نقدي نمايندگان فروش ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", ResultADSlRequest);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLRequest()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;

            if (ToDate.SelectedDate.HasValue)
                toDate = ToDate.SelectedDate.Value.AddDays(1);

            if (ToPaymentDate.SelectedDate.HasValue)
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLRequestCashInfo(CityComboBox.SelectedIDs,
                                                                        CenterComboBox.SelectedIDs,
                                                                        SellerAgentComboBox.SelectedIDs,
                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                        FromDate.SelectedDate,
                                                                        toDate,
                                                                        true,
                                                                        FromPaymentDate.SelectedDate,
                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLRequestCashInfo(CityComboBox.SelectedIDs,
                                                                        CenterComboBox.SelectedIDs,
                                                                        SellerAgentComboBox.SelectedIDs,
                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                        FromDate.SelectedDate,
                                                                        toDate,
                                                                        false,
                                                                        FromPaymentDate.SelectedDate,
                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSlSellerAgentADSLRequestCashInfo(CityComboBox.SelectedIDs,
                                                                        CenterComboBox.SelectedIDs,
                                                                        SellerAgentComboBox.SelectedIDs,
                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                        FromDate.SelectedDate,
                                                                        toDate,
                                                                        null,
                                                                        FromPaymentDate.SelectedDate,
                                                                        toPaymentDate);
            }
            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSlSellerAgentADSLRequestCashInfo(CityComboBox.SelectedIDs,
                                                                        CenterComboBox.SelectedIDs,
                                                                        SellerAgentComboBox.SelectedIDs,
                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                        FromDate.SelectedDate,
                                                                        toDate,
                                                                        null,
                                                                        FromPaymentDate.SelectedDate,
                                                                        toPaymentDate);
            }

            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLChangeService()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeServiceCashInfo(CityComboBox.SelectedIDs,
                                                                                                             CenterComboBox.SelectedIDs,
                                                                                                             SellerAgentComboBox.SelectedIDs,
                                                                                                             SellerAgentUsersComboBox.SelectedIDs,
                                                                                                             FromDate.SelectedDate,
                                                                                                             toDate,
                                                                                                             true,
                                                                                                             FromPaymentDate.SelectedDate,
                                                                                                             toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeServiceCashInfo(CityComboBox.SelectedIDs,
                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                           SellerAgentComboBox.SelectedIDs,
                                                                                                           SellerAgentUsersComboBox.SelectedIDs,
                                                                                                           FromDate.SelectedDate,
                                                                                                           toDate,
                                                                                                           false,
                                                                                                           FromPaymentDate.SelectedDate,
                                                                                                           toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {

                result = ReportDB.GetADSlSellerAgentADSLChangeServiceCashInfo(CityComboBox.SelectedIDs,
                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                           SellerAgentComboBox.SelectedIDs,
                                                                                                           SellerAgentUsersComboBox.SelectedIDs,
                                                                                                           FromDate.SelectedDate,
                                                                                                           toDate,
                                                                                                           null,
                                                                                                           FromPaymentDate.SelectedDate,
                                                                                                           toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeServiceCashInfo(CityComboBox.SelectedIDs,
                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                           SellerAgentComboBox.SelectedIDs,
                                                                                                           SellerAgentUsersComboBox.SelectedIDs,
                                                                                                           FromDate.SelectedDate,
                                                                                                           toDate,
                                                                                                           null,
                                                                                                           FromPaymentDate.SelectedDate,
                                                                                                           toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLSellTraffic()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLSellTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                            CenterComboBox.SelectedIDs,
                                                                                                            SellerAgentComboBox.SelectedIDs,
                                                                                                            SellerAgentUsersComboBox.SelectedIDs,
                                                                                                            FromDate.SelectedDate,
                                                                                                            toDate,
                                                                                                            true,
                                                                                                            FromPaymentDate.SelectedDate,
                                                                                                            toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLSellTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                          CenterComboBox.SelectedIDs,
                                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                                          FromDate.SelectedDate,
                                                                                                          toDate,
                                                                                                          false,
                                                                                                          FromPaymentDate.SelectedDate,
                                                                                                          toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSlSellerAgentADSLSellTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                          CenterComboBox.SelectedIDs,
                                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                                          FromDate.SelectedDate,
                                                                                                          toDate,
                                                                                                          null,
                                                                                                          FromPaymentDate.SelectedDate,
                                                                                                          toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSlSellerAgentADSLSellTrafficCashInfo(CityComboBox.SelectedIDs,
                                                                                                          CenterComboBox.SelectedIDs,
                                                                                                          SellerAgentComboBox.SelectedIDs,
                                                                                                          SellerAgentUsersComboBox.SelectedIDs,
                                                                                                          FromDate.SelectedDate,
                                                                                                          toDate,
                                                                                                          null,
                                                                                                          FromPaymentDate.SelectedDate,
                                                                                                          toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLChangeIP()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }


            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeIPCashInfo(CityComboBox.SelectedIDs,
                                                                                                       CenterComboBox.SelectedIDs,
                                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                                       FromDate.SelectedDate,
                                                                                                       toDate,
                                                                                                       true,
                                                                                                       FromPaymentDate.SelectedDate,
                                                                                                       toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeIPCashInfo(CityComboBox.SelectedIDs,
                                                                                                       CenterComboBox.SelectedIDs,
                                                                                                       SellerAgentComboBox.SelectedIDs,
                                                                                                       SellerAgentUsersComboBox.SelectedIDs,
                                                                                                       FromDate.SelectedDate,
                                                                                                       toDate,
                                                                                                       false,
                                                                                                       FromPaymentDate.SelectedDate,
                                                                                                       toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeIPCashInfo(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSlSellerAgentADSLChangeIPCashInfo(CityComboBox.SelectedIDs,
                                                                                                         CenterComboBox.SelectedIDs,
                                                                                                         SellerAgentComboBox.SelectedIDs,
                                                                                                         SellerAgentUsersComboBox.SelectedIDs,
                                                                                                         FromDate.SelectedDate,
                                                                                                         toDate,
                                                                                                         null,
                                                                                                         FromPaymentDate.SelectedDate,
                                                                                                         toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLChangeNo()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgentCashSaleChangeNoInfo(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        true,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLSellerAgentCashSaleChangeNoInfo(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        false,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLSellerAgentCashSaleChangeNoInfo(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLSellerAgentCashSaleChangeNoInfo(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLModemCost()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
                toDate = ToDate.SelectedDate.Value.AddDays(1);

            if (ToPaymentDate.SelectedDate.HasValue)
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLModemCost(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        true,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLModemCost(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        false,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLModemCost(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLModemCost(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLRequestInstallment()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestInstallment(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        true,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestInstallment(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        false,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestInstallment(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestInstallment(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }
            return result;
        }

        private List<ADSLSellerAgentCashIncomeInfo> LoadDataADSLRequestRanje()
        {
            List<ADSLSellerAgentCashIncomeInfo> result = new List<ADSLSellerAgentCashIncomeInfo>();
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            if (IsAcceptedComboBox.SelectedIndex == 0 && IsAcceptedComboBox.SelectedIndex != 1 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestRanje(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        true,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex == 1 && IsAcceptedComboBox.SelectedIndex != 0 && IsAcceptedComboBox.SelectedItems.Count == 1)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestRanje(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        false,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedItems.Count == 2)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestRanje(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }

            if (IsAcceptedComboBox.SelectedIndex < 0)
            {
                result = ReportDB.GetADSLsellerAgentCashADSLRequestRanje(CityComboBox.SelectedIDs,
                                                                                                        CenterComboBox.SelectedIDs,
                                                                                                        SellerAgentComboBox.SelectedIDs,
                                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                                        FromDate.SelectedDate,
                                                                                                        toDate,
                                                                                                        null,
                                                                                                        FromPaymentDate.SelectedDate,
                                                                                                        toPaymentDate);
            }
            return result;
        }

        #endregion Methods
    }
}
