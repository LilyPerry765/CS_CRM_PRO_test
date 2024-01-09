using System;
using System.Collections.Generic;
using System.Windows.Documents;
using CRM.Data;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report.Dictionary;
using System.Data;
using Microsoft.Reporting.WinForms;
using System.Reflection;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLSeviceSaleBandwidthSeparationReportUserControl.xaml
    /// </summary>
    public partial class ADSLSeviceSaleBandwidthSeparationReportUserControl : Local.ReportBase
    {
        #region properties

        #endregion

        #region Constructor

        public ADSLSeviceSaleBandwidthSeparationReportUserControl()
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
            SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));
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

            List<ADSLServiceSaleBandWidthSeperation> ResultADSLRequest = new List<ADSLServiceSaleBandWidthSeperation>();
            
            if ((SaleWayComboBox.SelectedIDs.Count == 0 || SaleWayComboBox.SelectedIDs.Count == 2) || (SaleWayComboBox.SelectedIDs.Count == 1 && SaleWayComboBox.SelectedIndex != 1))
                ResultADSLRequest = LoadDataADSLRequest();
            
            List<ADSLServiceSaleBandWidthSeperation> ResultADSLChangeService = LoadDataChangeService();

            for (int j = 0; j < ResultADSLChangeService.Count; j++)
            {
                bool add = false;

                for (int i = 0; i < ResultADSLRequest.Count; i++)
                {
                    if (ResultADSLRequest[i].CenterCostCode == ResultADSLChangeService[j].CenterCostCode)
                    {
                        if (ResultADSLRequest[i].Cost_Unlimited != null && ResultADSLChangeService[j].Cost_Unlimited != null)
                        {
                            ResultADSLRequest[i].Cost_Unlimited += ResultADSLChangeService[j].Cost_Unlimited;
                        }
                        if (ResultADSLRequest[i].Cost_Unlimited == null && ResultADSLChangeService[j].Cost_Unlimited != null)
                        {
                            ResultADSLRequest[i].Cost_Unlimited = ResultADSLChangeService[j].Cost_Unlimited;
                        }

                        if (ResultADSLRequest[i].Cost_64 != null && ResultADSLChangeService[j].Cost_64 != null)
                        {
                            ResultADSLRequest[i].Cost_64 += ResultADSLChangeService[j].Cost_64;
                        }
                        if (ResultADSLRequest[i].Cost_64 == null && ResultADSLChangeService[j].Cost_64 != null)
                        {
                            ResultADSLRequest[i].Cost_64 = ResultADSLChangeService[j].Cost_64;
                        }

                        if (ResultADSLRequest[i].Cost_128 != null && ResultADSLChangeService[j].Cost_128 != null)
                        {
                            ResultADSLRequest[i].Cost_128 += ResultADSLChangeService[j].Cost_128;
                        }
                        if (ResultADSLRequest[i].Cost_128 == null && ResultADSLChangeService[j].Cost_128 != null)
                        {
                            ResultADSLRequest[i].Cost_128 = ResultADSLChangeService[j].Cost_128;
                        }

                        if (ResultADSLRequest[i].Cost_256 != null && ResultADSLChangeService[j].Cost_256 != null)
                        {
                            ResultADSLRequest[i].Cost_256 += ResultADSLChangeService[j].Cost_256;
                        }
                        if (ResultADSLRequest[i].Cost_256 == null && ResultADSLChangeService[j].Cost_256 != null)
                        {
                            ResultADSLRequest[i].Cost_256 = ResultADSLChangeService[j].Cost_256;
                        }

                        if (ResultADSLRequest[i].Cost_512 != null && ResultADSLChangeService[j].Cost_512 != null)
                        {
                            ResultADSLRequest[i].Cost_512 += ResultADSLChangeService[j].Cost_512;
                        }
                        if (ResultADSLRequest[i].Cost_512 == null && ResultADSLChangeService[j].Cost_512 != null)
                        {
                            ResultADSLRequest[i].Cost_512 = ResultADSLChangeService[j].Cost_512;
                        }

                        if (ResultADSLRequest[i].Cost_1024 != null && ResultADSLChangeService[j].Cost_1024 != null)
                        {
                            ResultADSLRequest[i].Cost_1024 += ResultADSLChangeService[j].Cost_1024;
                        }
                        if (ResultADSLRequest[i].Cost_1024 == null && ResultADSLChangeService[j].Cost_1024 != null)
                        {
                            ResultADSLRequest[i].Cost_1024 = ResultADSLChangeService[j].Cost_1024;
                        }

                        if (ResultADSLRequest[i].Cost_2048 != null && ResultADSLChangeService[j].Cost_2048 != null)
                        {
                            ResultADSLRequest[i].Cost_2048 += ResultADSLChangeService[j].Cost_2048;
                        }
                        if (ResultADSLRequest[i].Cost_2048 == null && ResultADSLChangeService[j].Cost_2048 != null)
                        {
                            ResultADSLRequest[i].Cost_2048 = ResultADSLChangeService[j].Cost_2048;
                        }

                        if (ResultADSLRequest[i].Cost_5120 != null && ResultADSLChangeService[j].Cost_5120 != null)
                        {
                            ResultADSLRequest[i].Cost_5120 += ResultADSLChangeService[j].Cost_5120;
                        }
                        if (ResultADSLRequest[i].Cost_5120 == null && ResultADSLChangeService[j].Cost_5120 != null)
                        {
                            ResultADSLRequest[i].Cost_5120 = ResultADSLChangeService[j].Cost_5120;
                        }

                        if (ResultADSLRequest[i].Cost_10240 != null && ResultADSLChangeService[j].Cost_10240 != null)
                        {
                            ResultADSLRequest[i].Cost_10240 += ResultADSLChangeService[j].Cost_10240;
                        }
                        if (ResultADSLRequest[i].Cost_10240 == null && ResultADSLChangeService[j].Cost_10240 != null)
                        {
                            ResultADSLRequest[i].Cost_10240 = ResultADSLChangeService[j].Cost_10240;
                        }

                        if (ResultADSLRequest[i].AmountSum != null && ResultADSLChangeService[j].AmountSum != null)
                        {
                            ResultADSLRequest[i].AmountSum += ResultADSLChangeService[j].AmountSum;
                        }
                        if (ResultADSLRequest[i].AmountSum == null && ResultADSLChangeService[j].AmountSum != null)
                        {
                            ResultADSLRequest[i].AmountSum = ResultADSLChangeService[j].AmountSum;
                        }

                        if (ResultADSLRequest[i].RanjeCost == null)
                        {
                            ResultADSLRequest[i].RanjeCost = 0;
                        }
                        if (ResultADSLChangeService[j].RanjeCost == null)
                        {
                            ResultADSLChangeService[j].RanjeCost = 0;
                        }
                        if (ResultADSLRequest[i].InstallmentCost == null)
                        {
                            ResultADSLRequest[i].InstallmentCost = 0;
                        }

                        if (ResultADSLChangeService[j].InstallmentCost == null)
                        {
                            ResultADSLChangeService[j].InstallmentCost = 0;
                        }

                        ResultADSLRequest[i].RanjeCost += ResultADSLChangeService[j].RanjeCost;
                        ResultADSLRequest[i].InstallmentCost += ResultADSLChangeService[j].InstallmentCost;
                        add = true;
                    }
                }

                if (add == false)
                {
                    ResultADSLRequest.Add(ResultADSLChangeService[j]);
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

            title = "خريد سرويس جهت ورود به سيستم مالي ADSL به تفکيک سرعت ";
            stiReport.Dictionary.Variables["Header"].Value = title;





            //Stimulsoft.Report.Dictionary.StiDataSource db = new Stimulsoft.Report.Dictionary.StiDataSource("DataSource", "");
            //Stimulsoft.Report.Dictionary.StiDataSourcesCollection.userSource = new Stimulsoft.Report.Dictionary.stidata();
            //userSource.Name = "UserDataSourse";
            //userSource.Alias = "UserDataSourse";
            //userSource.NameInSource = "UserDataInSource";
            //userSource.DataTable = dataTable;
            //DataSet userSource = new DataSet();
            //userSource.Tables.Add(dataTable);

            //if (Cost_64 == true)
            //{
            //    //stiReport.Dictionary.Restrictions.Add("Result", StiDataType.DataSource, StiRestrictionTypes.DenyShow);
            //    //stiReport.Dictionary.Restrictions.Add("Result.Cost_64", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //    //stiReport.Dictionary.Restrictions.Add("Header", StiDataType.Variable, StiRestrictionTypes.DenyShow);
            //userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_64", Type.GetType("System.String")));

            //}
            //    if (Cost_128 == true)
            //    {
            //        //stiReport.Dictionary.Restrictions.Add("Result.Cost_128", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_128", Type.GetType("System.String")));
            //    }

            //    if (Cost_256 == true)
            //    {
            //        //stiReport.Dictionary.Restrictions.Add("Result.Cost_256", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_256", Type.GetType("System.String")));
            //    }

            //    if (Cost_512 == true)
            //    {
            //        //stiReport.Dictionary.Restrictions.Add("Result.Cost_512", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_1512", Type.GetType("System.String")));
            //    }

            //    if (Cost_1024 == true)
            //    {
            //        //stiReport.Dictionary.Restrictions.Add("Result.Cost_1024", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_1024", Type.GetType("System.String")));
            //    }

            //    if (Cost_2048 == true)
            //    {
            //        //stiReport.Dictionary.Restrictions.Add("Result.Cost_2048", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_2048", Type.GetType("System.String")));
            //    }

            //    if (Cost_Unlimited == true)
            //    {
            //    //    stiReport.Dictionary.Restrictions.Add("Result", StiDataType.DataSource, StiRestrictionTypes.DenyShow);
            //    //    stiReport.Dictionary.Restrictions.Add("Result.Cost_Unlimited", StiDataType.DataColumn, StiRestrictionTypes.DenyShow);
            //        userSource.Columns.Add(new Stimulsoft.Report.Dictionary.StiDataColumn("Cost_Unlimited", Type.GetType("System.String")));
            //    }


            //stiReport.Dictionary.DataSources.Add(userSource);
            DataSet data = new DataSet();
            DataTable dataTable = new DataTable("ResultADSLRequest");
            dataTable = ToDataTable(ResultADSLRequest);
            data.Tables.Add(dataTable);
            stiReport.RegData("Result", "Result", ResultADSLRequest);
            stiReport.Dictionary.Synchronize();
            //stiReport.Design();

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<ADSLServiceSaleBandWidthSeperation> LoadDataADSLRequest()
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
            long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

            List<ADSLServiceSaleBandWidthSeperation> result = ReportDB.GetADSLServiceSaleBandwidthSeperation(FromDate.SelectedDate,
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
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);

            return result;

        }

        private List<ADSLServiceSaleBandWidthSeperation> LoadDataChangeService()
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
            long? fromCost = (string.IsNullOrEmpty(FromCostTextBox.Text)) ? -1 : Convert.ToInt64(FromCostTextBox.Text);
            long? toCost = (string.IsNullOrEmpty(ToCostTextBox.Text)) ? -1 : Convert.ToInt64(ToCostTextBox.Text);

            List<ADSLServiceSaleBandWidthSeperation> result = ReportDB.GetADSLChangeServiCeServiceSaleBandwidthSeperation(FromDate.SelectedDate,
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
                                                                                        HasModemCheckBox.IsChecked,
                                                                                        ServicePaymentTypeCombBox.SelectedIDs,
                                                                                        FromPaymentDate.SelectedDate,
                                                                                        toPaymentDate);


            return result;

        }


        public static DataTable ToDataTable(List<ADSLServiceSaleBandWidthSeperation> items)
        {
            DataTable dataTable = new DataTable(typeof(ADSLServiceSaleBandWidthSeperation).Name);

            bool CheckCost_64 = false;
            bool CheckCost_128 = false;
            bool CheckCost_256 = false;
            bool CheckCost_512 = false;
            bool CheckCost_1024 = false;
            bool CheckCost_2048 = false;
            bool CheckCost_Unlimited = false;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Cost_64 != null && CheckCost_64 == false)
                {
                    CheckCost_64 = true;
                }
                if (items[i].Cost_128 != null && CheckCost_128 == false)
                {
                    CheckCost_128 = true;
                }

                if (items[i].Cost_256 != null && CheckCost_256 == false)
                {
                    CheckCost_256 = true;
                }

                if (items[i].Cost_512 != null && CheckCost_512 == false)
                {
                    CheckCost_512 = true;
                }

                if (items[i].Cost_1024 != null && CheckCost_1024 == false)
                {
                    CheckCost_1024 = true;
                }

                if (items[i].Cost_2048 != null && CheckCost_2048 == false)
                {
                    CheckCost_2048 = true;
                }

                if (items[i].Cost_Unlimited != null && CheckCost_Unlimited == false)
                {
                    CheckCost_Unlimited = true;
                }
            }
            //Get all the properties
            PropertyInfo[] Props = typeof(ADSLServiceSaleBandWidthSeperation).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                if (prop.Name == "Cost_64" && CheckCost_64 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                if (prop.Name == "Cost_128" && CheckCost_128 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "Cost_256" && CheckCost_256 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "Cost_512" && CheckCost_512 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "Cost_1024" && CheckCost_1024 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "Cost_2048" && CheckCost_2048 == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "Cost_Unlimited" && CheckCost_Unlimited == true)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                if (prop.Name == "CenterCostCode" || prop.Name == "FromDate" || prop.Name == "ToDate" || prop.Name == "RanjeCost" || prop.Name == "InstallmentCost")
                {
                    dataTable.Columns.Add(prop.Name);
                }
            }

            PropertyInfo[] NewProps = new PropertyInfo[dataTable.Columns.Count];
            int count = 0;
            foreach (ADSLServiceSaleBandWidthSeperation item in items)
            {
                var values = new object[dataTable.Columns.Count];
                for (int i = 0; i < Props.Length; i++)
                {
                    bool Colnameexists = false;
                    // foreach (System.Data.DataColumn col in dataTable.Columns)
                    //{

                    //    if (col.ColumnName == Props[i].Name)
                    //    {
                    //        Colnameexists = true;
                    //        break;
                    //    }
                    //}
                    // if (Colnameexists == true && count<10)
                    // {
                    //     NewProps[count] = Props[i];
                    //     count++;
                    // }
                }
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    bool ColNameExits = false;
                    foreach (System.Data.DataColumn col in dataTable.Columns)
                    {
                        if (Props[i].Name == col.ColumnName)
                        {
                            ColNameExits = true;
                            break;
                        }

                    }
                    if (ColNameExits == true)
                    {
                        //inserting property values to datatable rows
                        //values[i] = NewProps[i].GetValue(item, null);

                    }
                }

                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #endregion Methods
    }
}
