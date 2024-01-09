using System;
using System.Collections.Generic;
using System.Windows.Documents;
using CRM.Data;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    public partial class ADSLTrafficSaleTrafficSeperationReportUserControl : Local.ReportBase
    {
        #region properties
        #endregion

        #region Constructor

        public ADSLTrafficSaleTrafficSeperationReportUserControl()
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
            ServicePaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            //TypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSaleTypeDetails));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            ServiceGroupComboBox.ItemsComboBox.DropDownClosed += new EventHandler(GroupItemsComboBox_DropDownClosed);
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLSellChanellLimited));
            PaymentTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
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
            BandWidthComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceBandWithCheckablebyGroupIDsAndTypeIds(ServiceGroupComboBox.SelectedIDs, ServicePaymentTypeCombBox.SelectedIDs);
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

        #region Methods

        public override void Search()
        {
            List<ADSLTrafficSaleTrafficSeperation> ResultADSRequest = new List<ADSLTrafficSaleTrafficSeperation>();
            if (SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2 || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
            {
                ResultADSRequest = LoadDataADSLRequest();
            }
            List<ADSLTrafficSaleTrafficSeperation> ResultADSSellTraffic = LoadDataADSLSellTraffic();
            //List<ADSLTrafficSaleTrafficSeperation> ResultADSLRequestModemCost = LoadDataModemCost();
            //List<ADSLTrafficSaleTrafficSeperation> ResultADSLRequestIPCost= LoadDataIPCost();
            //List<ADSLTrafficSaleTrafficSeperation> ResultADSLRequestRanje = LoadDataADSLRanjeCost();
            //List<ADSLTrafficSaleTrafficSeperation> ResultADSLRequestInstallment = LoadDataInstallment();

            //for (int i = 0; i < ResultADSRequest.Count;i++ )
            //{
            //    for (int j = 0; j < ResultADSLRequestModemCost.Count; j++)
            //    {
            //        if (ResultADSRequest[i].CenterCostCode == ResultADSLRequestModemCost[j].CenterCostCode)
            //        {
            //            ResultADSRequest[i].ModemCost = ResultADSLRequestModemCost[j].ModemCost;
            //        }
            //    }
            //}

            //for (int i = 0; i < ResultADSRequest.Count; i++)
            //{
            //    for (int j = 0; j < ResultADSLRequestIPCost.Count; j++)
            //    {
            //        if (ResultADSRequest[i].CenterCostCode == ResultADSLRequestIPCost[j].CenterCostCode)
            //        {
            //            ResultADSRequest[i].IPCost = ResultADSLRequestIPCost[j].IPCost;
            //        }
            //    }
            //}

            //for (int i = 0; i < ResultADSRequest.Count; i++)
            //{
            //    for (int j = 0; j < ResultADSLRequestRanje.Count; j++)
            //    {
            //        if (ResultADSRequest[i].CenterCostCode == ResultADSLRequestRanje[j].CenterCostCode)
            //        {
            //            ResultADSRequest[i].RanjeCost = ResultADSLRequestRanje[j].RanjeCost;
            //        }
            //    }
            //}

            //for (int i = 0; i < ResultADSRequest.Count; i++)
            //{
            //    for (int j = 0; j < ResultADSLRequestInstallment.Count; j++)
            //    {
            //        if (ResultADSRequest[i].CenterCostCode == ResultADSLRequestInstallment[j].CenterCostCode)
            //        {
            //            ResultADSRequest[i].InstallmentCost = ResultADSLRequestInstallment[j].InstallmentCost;
            //        }
            //    }
            //}

            for (int j = 0; j < ResultADSSellTraffic.Count; j++)
            {
                bool add = false;

                for (int i = 0; i < ResultADSRequest.Count; i++)
                {
                    if (ResultADSRequest[i].CenterCostCode == ResultADSSellTraffic[j].CenterCostCode)
                    {
                        if (ResultADSRequest[i].Cost_Unlimited != null && ResultADSSellTraffic[j].Cost_Unlimited != null)
                        {
                            ResultADSRequest[i].Cost_Unlimited += ResultADSSellTraffic[j].Cost_Unlimited;
                        }
                        if (ResultADSRequest[i].Cost_Unlimited == null && ResultADSSellTraffic[j].Cost_Unlimited != null)
                        {
                            ResultADSRequest[i].Cost_Unlimited = ResultADSSellTraffic[j].Cost_Unlimited;
                        }

                        if (ResultADSRequest[i].Cost_0 != null && ResultADSSellTraffic[j].Cost_0 != null)
                        {
                            ResultADSRequest[i].Cost_0 += ResultADSSellTraffic[j].Cost_0;
                        }
                        if (ResultADSRequest[i].Cost_0 == null && ResultADSSellTraffic[j].Cost_0 != null)
                        {
                            ResultADSRequest[i].Cost_0 = ResultADSSellTraffic[j].Cost_0;
                        }

                        if (ResultADSRequest[i].Cost_0_5 != null && ResultADSSellTraffic[j].Cost_0_5 != null)
                        {
                            ResultADSRequest[i].Cost_0_5 += ResultADSSellTraffic[j].Cost_0_5;
                        }
                        if (ResultADSRequest[i].Cost_0_5 == null && ResultADSSellTraffic[j].Cost_0_5 != null)
                        {
                            ResultADSRequest[i].Cost_0_5 = ResultADSSellTraffic[j].Cost_0_5;
                        }

                        if (ResultADSRequest[i].Cost_1 != null && ResultADSSellTraffic[j].Cost_1 != null)
                        {
                            ResultADSRequest[i].Cost_1 += ResultADSSellTraffic[j].Cost_1;
                        }
                        if (ResultADSRequest[i].Cost_1 == null && ResultADSSellTraffic[j].Cost_1 != null)
                        {
                            ResultADSRequest[i].Cost_1 = ResultADSSellTraffic[j].Cost_1;
                        }

                        if (ResultADSRequest[i].Cost_2 != null && ResultADSSellTraffic[j].Cost_2 != null)
                        {
                            ResultADSRequest[i].Cost_2 += ResultADSSellTraffic[j].Cost_2;
                        }
                        if (ResultADSRequest[i].Cost_2 == null && ResultADSSellTraffic[j].Cost_2 != null)
                        {
                            ResultADSRequest[i].Cost_2 = ResultADSSellTraffic[j].Cost_2;
                        }

                        if (ResultADSRequest[i].Cost_3 != null && ResultADSSellTraffic[j].Cost_3 != null)
                        {
                            ResultADSRequest[i].Cost_3 += ResultADSSellTraffic[j].Cost_3;
                        }
                        if (ResultADSRequest[i].Cost_3 == null && ResultADSSellTraffic[j].Cost_3 != null)
                        {
                            ResultADSRequest[i].Cost_3 = ResultADSSellTraffic[j].Cost_3;
                        }

                        if (ResultADSRequest[i].Cost_4 != null && ResultADSSellTraffic[j].Cost_4 != null)
                        {
                            ResultADSRequest[i].Cost_4 += ResultADSSellTraffic[j].Cost_4;
                        }
                        if (ResultADSRequest[i].Cost_4 == null && ResultADSSellTraffic[j].Cost_4 != null)
                        {
                            ResultADSRequest[i].Cost_4 = ResultADSSellTraffic[j].Cost_4;
                        }

                        if (ResultADSRequest[i].Cost_5 != null && ResultADSSellTraffic[j].Cost_5 != null)
                        {
                            ResultADSRequest[i].Cost_5 += ResultADSSellTraffic[j].Cost_5;
                        }
                        if (ResultADSRequest[i].Cost_5 == null && ResultADSSellTraffic[j].Cost_5 != null)
                        {
                            ResultADSRequest[i].Cost_5 = ResultADSSellTraffic[j].Cost_5;
                        }

                        if (ResultADSRequest[i].Cost_6 != null && ResultADSSellTraffic[j].Cost_6 != null)
                        {
                            ResultADSRequest[i].Cost_6 += ResultADSSellTraffic[j].Cost_6;
                        }
                        if (ResultADSRequest[i].Cost_6 == null && ResultADSSellTraffic[j].Cost_6 != null)
                        {
                            ResultADSRequest[i].Cost_6 = ResultADSSellTraffic[j].Cost_6;
                        }

                        if (ResultADSRequest[i].Cost_7 != null && ResultADSSellTraffic[j].Cost_7 != null)
                        {
                            ResultADSRequest[i].Cost_7 += ResultADSSellTraffic[j].Cost_7;
                        }
                        if (ResultADSRequest[i].Cost_7 == null && ResultADSSellTraffic[j].Cost_7 != null)
                        {
                            ResultADSRequest[i].Cost_7 = ResultADSSellTraffic[j].Cost_7;
                        }

                        if (ResultADSRequest[i].Cost_8 != null && ResultADSSellTraffic[j].Cost_8 != null)
                        {
                            ResultADSRequest[i].Cost_8 += ResultADSSellTraffic[j].Cost_8;
                        }
                        if (ResultADSRequest[i].Cost_8 == null && ResultADSSellTraffic[j].Cost_8 != null)
                        {
                            ResultADSRequest[i].Cost_8 = ResultADSSellTraffic[j].Cost_8;
                        }

                        if (ResultADSRequest[i].Cost_9 != null && ResultADSSellTraffic[j].Cost_9 != null)
                        {
                            ResultADSRequest[i].Cost_9 += ResultADSSellTraffic[j].Cost_9;
                        }
                        if (ResultADSRequest[i].Cost_9 == null && ResultADSSellTraffic[j].Cost_9 != null)
                        {
                            ResultADSRequest[i].Cost_9 = ResultADSSellTraffic[j].Cost_9;
                        }

                        if (ResultADSRequest[i].Cost_10 != null && ResultADSSellTraffic[j].Cost_10 != null)
                        {
                            ResultADSRequest[i].Cost_10 += ResultADSSellTraffic[j].Cost_10;
                        }
                        if (ResultADSRequest[i].Cost_10 == null && ResultADSSellTraffic[j].Cost_10 != null)
                        {
                            ResultADSRequest[i].Cost_10 = ResultADSSellTraffic[j].Cost_10;
                        }

                        if (ResultADSRequest[i].Cost_11 != null && ResultADSSellTraffic[j].Cost_11 != null)
                        {
                            ResultADSRequest[i].Cost_11 += ResultADSSellTraffic[j].Cost_11;
                        }
                        if (ResultADSRequest[i].Cost_11 == null && ResultADSSellTraffic[j].Cost_11 != null)
                        {
                            ResultADSRequest[i].Cost_11 = ResultADSSellTraffic[j].Cost_11;
                        }

                        if (ResultADSRequest[i].Cost_12 != null && ResultADSSellTraffic[j].Cost_12 != null)
                        {
                            ResultADSRequest[i].Cost_12 += ResultADSSellTraffic[j].Cost_12;
                        }
                        if (ResultADSRequest[i].Cost_12 == null && ResultADSSellTraffic[j].Cost_12 != null)
                        {
                            ResultADSRequest[i].Cost_12 = ResultADSSellTraffic[j].Cost_12;
                        }

                        if (ResultADSRequest[i].Cost_13 != null && ResultADSSellTraffic[j].Cost_13 != null)
                        {
                            ResultADSRequest[i].Cost_13 += ResultADSSellTraffic[j].Cost_13;
                        }
                        if (ResultADSRequest[i].Cost_13 == null && ResultADSSellTraffic[j].Cost_13 != null)
                        {
                            ResultADSRequest[i].Cost_13 = ResultADSSellTraffic[j].Cost_13;
                        }

                        if (ResultADSRequest[i].Cost_14 != null && ResultADSSellTraffic[j].Cost_14 != null)
                        {
                            ResultADSRequest[i].Cost_14 += ResultADSSellTraffic[j].Cost_14;
                        }
                        if (ResultADSRequest[i].Cost_14 == null && ResultADSSellTraffic[j].Cost_14 != null)
                        {
                            ResultADSRequest[i].Cost_14 = ResultADSSellTraffic[j].Cost_14;
                        }

                        if (ResultADSRequest[i].Cost_15 != null && ResultADSSellTraffic[j].Cost_15 != null)
                        {
                            ResultADSRequest[i].Cost_15 += ResultADSSellTraffic[j].Cost_15;
                        }
                        if (ResultADSRequest[i].Cost_15 == null && ResultADSSellTraffic[j].Cost_15 != null)
                        {
                            ResultADSRequest[i].Cost_15 = ResultADSSellTraffic[j].Cost_15;
                        }

                        if (ResultADSRequest[i].Cost_16 != null && ResultADSSellTraffic[j].Cost_16 != null)
                        {
                            ResultADSRequest[i].Cost_16 += ResultADSSellTraffic[j].Cost_16;
                        }
                        if (ResultADSRequest[i].Cost_16 == null && ResultADSSellTraffic[j].Cost_16 != null)
                        {
                            ResultADSRequest[i].Cost_16 = ResultADSSellTraffic[j].Cost_16;
                        }

                        if (ResultADSRequest[i].Cost_17 != null && ResultADSSellTraffic[j].Cost_17 != null)
                        {
                            ResultADSRequest[i].Cost_17 += ResultADSSellTraffic[j].Cost_17;
                        }
                        if (ResultADSRequest[i].Cost_17 == null && ResultADSSellTraffic[j].Cost_17 != null)
                        {
                            ResultADSRequest[i].Cost_17 = ResultADSSellTraffic[j].Cost_17;
                        }

                        if (ResultADSRequest[i].Cost_18 != null && ResultADSSellTraffic[j].Cost_18 != null)
                        {
                            ResultADSRequest[i].Cost_18 += ResultADSSellTraffic[j].Cost_18;
                        }
                        if (ResultADSRequest[i].Cost_18 == null && ResultADSSellTraffic[j].Cost_18 != null)
                        {
                            ResultADSRequest[i].Cost_18 = ResultADSSellTraffic[j].Cost_18;
                        }

                        if (ResultADSRequest[i].Cost_19 != null && ResultADSSellTraffic[j].Cost_19 != null)
                        {
                            ResultADSRequest[i].Cost_19 += ResultADSSellTraffic[j].Cost_19;
                        }
                        if (ResultADSRequest[i].Cost_19 == null && ResultADSSellTraffic[j].Cost_19 != null)
                        {
                            ResultADSRequest[i].Cost_19 = ResultADSSellTraffic[j].Cost_19;
                        }

                        if (ResultADSRequest[i].Cost_20 != null && ResultADSSellTraffic[j].Cost_20 != null)
                        {
                            ResultADSRequest[i].Cost_20 += ResultADSSellTraffic[j].Cost_20;
                        }
                        if (ResultADSRequest[i].Cost_20 == null && ResultADSSellTraffic[j].Cost_20 != null)
                        {
                            ResultADSRequest[i].Cost_20 = ResultADSSellTraffic[j].Cost_20;
                        }

                        if (ResultADSRequest[i].Cost_24 != null && ResultADSSellTraffic[j].Cost_24 != null)
                        {
                            ResultADSRequest[i].Cost_24 += ResultADSSellTraffic[j].Cost_24;
                        }
                        if (ResultADSRequest[i].Cost_24 == null && ResultADSSellTraffic[j].Cost_24 != null)
                        {
                            ResultADSRequest[i].Cost_24 = ResultADSSellTraffic[j].Cost_24;
                        }

                        if (ResultADSRequest[i].Cost_30 != null && ResultADSSellTraffic[j].Cost_30 != null)
                        {
                            ResultADSRequest[i].Cost_30 += ResultADSSellTraffic[j].Cost_30;
                        }
                        if (ResultADSRequest[i].Cost_30 == null && ResultADSSellTraffic[j].Cost_30 != null)
                        {
                            ResultADSRequest[i].Cost_30 = ResultADSSellTraffic[j].Cost_30;
                        }

                        if (ResultADSRequest[i].Cost_40 != null && ResultADSSellTraffic[j].Cost_40 != null)
                        {
                            ResultADSRequest[i].Cost_40 += ResultADSSellTraffic[j].Cost_40;
                        }
                        if (ResultADSRequest[i].Cost_40 == null && ResultADSSellTraffic[j].Cost_40 != null)
                        {
                            ResultADSRequest[i].Cost_40 = ResultADSSellTraffic[j].Cost_40;
                        }

                        if (ResultADSRequest[i].Cost_46 != null && ResultADSSellTraffic[j].Cost_46 != null)
                        {
                            ResultADSRequest[i].Cost_46 += ResultADSSellTraffic[j].Cost_46;
                        }
                        if (ResultADSRequest[i].Cost_46 == null && ResultADSSellTraffic[j].Cost_46 != null)
                        {
                            ResultADSRequest[i].Cost_46 = ResultADSSellTraffic[j].Cost_46;
                        }

                        if (ResultADSRequest[i].Cost_48 != null && ResultADSSellTraffic[j].Cost_48 != null)
                        {
                            ResultADSRequest[i].Cost_48 += ResultADSSellTraffic[j].Cost_48;
                        }
                        if (ResultADSRequest[i].Cost_48 == null && ResultADSSellTraffic[j].Cost_48 != null)
                        {
                            ResultADSRequest[i].Cost_48 = ResultADSSellTraffic[j].Cost_48;
                        }

                        if (ResultADSRequest[i].Cost_50 != null && ResultADSSellTraffic[j].Cost_50 != null)
                        {
                            ResultADSRequest[i].Cost_50 += ResultADSSellTraffic[j].Cost_50;
                        }
                        if (ResultADSRequest[i].Cost_50 == null && ResultADSSellTraffic[j].Cost_50 != null)
                        {
                            ResultADSRequest[i].Cost_50 = ResultADSSellTraffic[j].Cost_50;
                        }

                        if (ResultADSRequest[i].Cost_100 != null && ResultADSSellTraffic[j].Cost_100 != null)
                        {
                            ResultADSRequest[i].Cost_100 += ResultADSSellTraffic[j].Cost_100;
                        }
                        if (ResultADSRequest[i].Cost_100 == null && ResultADSSellTraffic[j].Cost_100 != null)
                        {
                            ResultADSRequest[i].Cost_100 = ResultADSSellTraffic[j].Cost_100;
                        }

                        if (ResultADSRequest[i].Cost_200 != null && ResultADSSellTraffic[j].Cost_200 != null)
                        {
                            ResultADSRequest[i].Cost_200 += ResultADSSellTraffic[j].Cost_200;
                        }
                        if (ResultADSRequest[i].Cost_200 == null && ResultADSSellTraffic[j].Cost_200 != null)
                        {
                            ResultADSRequest[i].Cost_200 = ResultADSSellTraffic[j].Cost_200;
                        }

                        if (ResultADSRequest[i].AmountSum != null && ResultADSSellTraffic[j].AmountSum != null)
                        {
                            ResultADSRequest[i].AmountSum += ResultADSSellTraffic[j].AmountSum;
                        }
                        if (ResultADSRequest[i].AmountSum == null && ResultADSSellTraffic[j].AmountSum != null)
                        {
                            ResultADSRequest[i].AmountSum = ResultADSSellTraffic[j].AmountSum;
                        }

                        if (ResultADSRequest[i].RanjeCost != null && ResultADSSellTraffic[j].RanjeCost != null)
                        {
                            ResultADSRequest[i].RanjeCost += ResultADSSellTraffic[j].RanjeCost;
                        }
                        if (ResultADSRequest[i].RanjeCost == null && ResultADSSellTraffic[j].RanjeCost != null)
                        {
                            ResultADSRequest[i].RanjeCost = ResultADSSellTraffic[j].RanjeCost;
                        }

                        if (ResultADSRequest[i].InstallmentCost != null && ResultADSSellTraffic[j].InstallmentCost != null)
                        {
                            ResultADSRequest[i].InstallmentCost += ResultADSSellTraffic[j].InstallmentCost;
                        }
                        if (ResultADSRequest[i].InstallmentCost == null && ResultADSSellTraffic[j].InstallmentCost != null)
                        {
                            ResultADSRequest[i].InstallmentCost = ResultADSSellTraffic[j].InstallmentCost;
                        }

                        if (ResultADSRequest[i].ModemCost != null && ResultADSSellTraffic[j].ModemCost != null)
                        {
                            ResultADSRequest[i].ModemCost += ResultADSSellTraffic[j].ModemCost;
                        }
                        if (ResultADSRequest[i].ModemCost == null && ResultADSSellTraffic[j].ModemCost != null)
                        {
                            ResultADSRequest[i].ModemCost = ResultADSSellTraffic[j].ModemCost;
                        }

                        if (ResultADSRequest[i].IPCost != null && ResultADSSellTraffic[j].IPCost != null)
                        {
                            ResultADSRequest[i].IPCost += ResultADSSellTraffic[j].IPCost;
                        }
                        if (ResultADSRequest[i].IPCost == null && ResultADSSellTraffic[j].IPCost != null)
                        {
                            ResultADSRequest[i].IPCost = ResultADSSellTraffic[j].IPCost;
                        }

                        add = true;
                    }
                }

                if (add == false)
                {
                    ResultADSRequest.Add(ResultADSSellTraffic[j]);
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
            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            long? fromCostText = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCostText = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);


            title = "خريد ترافيک ADSL به تفکيک حجم جهت ورود به سيستم مالي";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", ResultADSRequest);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLTrafficSaleTrafficSeperation> LoadDataADSLRequest()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

            List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLRequestTrafficSaleTrafficSeperation(FromDate.SelectedDate,
                                                                                        toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        fromCost,
                                                                                        toCost,
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs);

            return result;

        }

        private List<ADSLTrafficSaleTrafficSeperation> LoadDataADSLSellTraffic()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            
            long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

            List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLSellTrafficTrafficSaleTrafficSeperation(FromDate.SelectedDate,
                                                                                        toDate
                                                                                        , CityComboBox.SelectedIDs,
                                                                                        CenterComboBox.SelectedIDs,
                                                                                        ServiceComboBox.SelectedIDs,
                                                                                        ServiceGroupComboBox.SelectedIDs,
                //TypeComboBox.SelectedIDs,
                                                                                        BandWidthComboBox.SelectedIDs,
                                                                                        TrafficComboBox.SelectedIDs,
                                                                                        DurationComboBox.SelectedIDs,
                                                                                        SaleWayComboBox.SelectedIDs,
                                                                                        PaymentTypeComboBox.SelectedIDs,
                                                                                        CustomerGroupComboBox.SelectedIDs,
                                                                                        fromCost,
                                                                                        toCost,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs);

            return result;

        }

        //private List<ADSLTrafficSaleTrafficSeperation> LoadDataModemCost()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
        //    long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

        //    List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLSQLTrafficSaleADSLRequestModemCost(FromDate.SelectedDate,
        //                                                                                toDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                ServiceGroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);

        //    return result;

        //}

        //private List<ADSLTrafficSaleTrafficSeperation> LoadDataIPCost()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
        //    long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

        //    List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLSQLTrafficSaleADSLRequestIPCost(FromDate.SelectedDate,
        //                                                                                toDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                ServiceGroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);

        //    return result;

        //}

        //private List<ADSLTrafficSaleTrafficSeperation> LoadDataADSLRanjeCost()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
        //    long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

        //    List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLSQLTrafficSaleADSLRequestRanjeCost(FromDate.SelectedDate,
        //                                                                                toDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                ServiceGroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);

        //    return result;

        //}

        //private List<ADSLTrafficSaleTrafficSeperation> LoadDataInstallment()
        //{
        //    DateTime? toDate = null;
        //    if (ToDate.SelectedDate.HasValue)
        //    {
        //        toDate = ToDate.SelectedDate.Value.AddDays(1);
        //    }
        //    long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
        //    long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

        //    List<ADSLTrafficSaleTrafficSeperation> result = ReportDB.GetADSLSQLTrafficSaleADSLRequestInstallmentCost(FromDate.SelectedDate,
        //                                                                                toDate
        //                                                                                , CityComboBox.SelectedIDs,
        //                                                                                CenterComboBox.SelectedIDs,
        //                                                                                ServiceComboBox.SelectedIDs,
        //                                                                                ServiceGroupComboBox.SelectedIDs,
        //                                                                                TypeComboBox.SelectedIDs,
        //                                                                                BandWidthComboBox.SelectedIDs,
        //                                                                                TrafficComboBox.SelectedIDs,
        //                                                                                DurationComboBox.SelectedIDs,
        //                                                                                SaleWayComboBox.SelectedIDs,
        //                                                                                PaymentTypeCombBox.SelectedIDs,
        //                                                                                CustomerGroupComboBox.SelectedIDs);

        //    return result;

        //}


        #endregion Methods
    }
}
