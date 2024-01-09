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
    /// Interaction logic for ADSLServiceSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLServiceSaleReportUserControl : Local.ReportBase
    {
        #region Properties
       
        #endregion
        #region Constructor

        public ADSLServiceSaleReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor 

        #region Initializer

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceType));
            GroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            GroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            //SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanellLimited));

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


        void GroupItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (GroupComboBox.SelectedIDs.Count > 0)
                AllChecked = true;

            CustomerGroupComboBox.ItemsSource = ADSLCustomerGroupDB.GetCustomerGroupsCheckableByADSlServiceGroupIds(GroupComboBox.SelectedIDs);
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(GroupComboBox.SelectedIDs, PaymentTypeCombBox.SelectedIDs);
            BandWidthComboBox.ItemsComboBox.DropDownClosed += new EventHandler(BandWidthItemsComboBox_DropDownClosed);
        }

        void ADSLSellerAgentItemsComboBox_DropDownClosed(object sender, EventArgs e)
        {
            bool AllChecked = false;
            if (BandWidthComboBox.SelectedIDs.Count > 0)
                AllChecked = true;
            SellerAgentUsersComboBox.ItemsSource = ADSLSellerGroupDB.GetADSLSellerAgentUsersCheckableByADSlSellerAgentID(SellerAgentComboBox.SelectedIDs);
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
            ServiceComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceCheckableByTrafficIDs(TrafficComboBox.SelectedIDs, GroupComboBox.SelectedIDs, DurationComboBox.SelectedIDs, BandWidthComboBox.SelectedIDs);
        }
        #endregion

        #endregion Initializer

        #region Methods

        public override void Search()
        {

            List<ADSLServiceInfo> Result = LoadData();
            List<ADSLServiceInfo> ResultModem = LoadModemData();
            List<ADSLServiceInfo> ResultIP = LoadIPData();
            List<ADSLServiceInfo> ResultInstallment = LoadInstallmentData();
            List<ADSLServiceInfo> ResultRanje = LoadRanjeData();
            List<ADSLServiceInfo> ResultAmountSum = LoadAmountSumData();
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

                foreach (ADSLServiceInfo info in Result)
                {
                    for (int i = 0; i < ResultModem.Count;i++ )
                    {
                        if (info.City == ResultModem[i].City && info.CenterName == ResultModem[i].CenterName && info.Title == ResultModem[i].Title && info.TelephoneNo == ResultModem[i].TelephoneNo && info.CustomerName== ResultModem[i].CustomerName)
                        {
                            info.ModemPrice = ResultModem[i].ModemPrice;
                        }
                    }

                    for (int i = 0; i < ResultIP.Count; i++)
                    {
                        if (info.City == ResultIP[i].City && info.Center == ResultIP[i].Center && info.Title == ResultIP[i].Title && info.TelephoneNo == ResultIP[i].TelephoneNo && info.CustomerName==ResultIP[i].CustomerName)
                        {
                            info.IPPrice = ResultIP[i].IPPrice;
                        }
                    }

                    for (int i = 0; i < ResultInstallment.Count; i++)
                    {
                        if (info.City == ResultInstallment[i].City && info.Center == ResultInstallment[i].Center && info.Title == ResultInstallment[i].Title && info.TelephoneNo == ResultInstallment[i].TelephoneNo && info.CustomerName==ResultInstallment[i].CustomerName)
                        {
                            info.InstallmentPrice = ResultInstallment[i].InstallmentPrice;
                        }
                    }

                    for (int i = 0; i < ResultRanje.Count; i++)
                    {
                        if (info.City == ResultRanje[i].City && info.Center == ResultRanje[i].Center && info.Title == ResultRanje[i].Title && info.TelephoneNo == ResultRanje[i].TelephoneNo && info.CustomerName==ResultRanje[i].CustomerName)
                        {
                            info.RanjePrice = ResultRanje[i].RanjePrice;
                        }
                    }

                    for (int i = 0; i < ResultAmountSum.Count; i++)
                    {
                        if (info.City == ResultAmountSum[i].City && info.Center == ResultAmountSum[i].Center && info.Title == ResultAmountSum[i].Title && info.TelephoneNo == ResultAmountSum[i].TelephoneNo && info.CustomerName == ResultAmountSum[i].CustomerName)
                        {
                            info.SumAmount = ResultAmountSum[i].SumAmount;
                        }
                    }
                        //if (info.RequiredInstallation == true)
                        //{//هزینه ها رو بر 0/06تقسیم کردم تا خالص به دست بیاد installmetnprice and Ranje and sumprice in reportdb
                            //info.InstallmentPrice = "53000";
                            //info.RanjePrice = "53000";
                            //info.SumAmount = (Convert.ToInt64(info.PriceSum) + 53000 + 53000).ToString();
                            //info.Tax = (Convert.ToInt64(info.SumAmount) * 0.06).ToString();
                            info.TotalProceeds = ((Convert.ToInt64(info.SumAmount)) + Convert.ToDouble(info.Tax)).ToString();
                        //}

                        //else
                        //{
                        //    info.InstallmentPrice = "0";
                        //    info.RanjePrice = "53000";
                        //    info.SumAmount = (Convert.ToInt64(info.PriceSum) + 53000).ToString();
                        //    info.Tax = (Convert.ToInt64(info.SumAmount) * 0.06).ToString();
                        //    info.TotalProceeds = ((Convert.ToInt64(info.SumAmount)) + Convert.ToDouble(info.Tax)).ToString();
                        //}
                    if (info.IsPaid == true)
                    {
                        info.IsPaidString = "پرداخت شده";
                    }
                    else
                    {
                        info.IsPaidString = "پرداخت نشده";
                    }
                }


                title = "خرید سرویس";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            
        }

        public List<ADSLServiceInfo> LoadData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {
                
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;
        }

        public List<ADSLServiceInfo> LoadAmountSumData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {

                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceADSLRequestAmountSumSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;
        }
        public List<ADSLServiceInfo> LoadModemData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {

                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceADSlRequestModemSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;


        }

        public List<ADSLServiceInfo> LoadIPData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {

                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceADSlRequestIPSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;


        }

        public List<ADSLServiceInfo> LoadInstallmentData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {

                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceADSlRequestInstallmentSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;


        }

        public List<ADSLServiceInfo> LoadRanjeData()
        {
            DateTime? toDate = null;
            DateTime? toPaymentDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            if (ToPaymentDate.SelectedDate != null)
            {

                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceInfo> Result = ReportDB.GetADSLServiceADSlRequestRanjeSaleInfo(SellerAgentComboBox.SelectedIDs,
                                                                                        FromDate.SelectedDate, toDate,
                                                                                        CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        GroupComboBox.SelectedIDs,
                                                                                        TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                //SaleWayComboBox.SelectedIDs,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        PaymentTypeCombBox.SelectedIDs,
                                                                                        SellerAgentUsersComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return Result;


        }
        #endregion Methods
    }
}
