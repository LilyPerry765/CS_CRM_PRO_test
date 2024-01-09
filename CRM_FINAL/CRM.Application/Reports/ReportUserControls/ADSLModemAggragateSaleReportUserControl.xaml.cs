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
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;
using CRM.Data;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLModemAggragateSaleReportUserControl.xaml
    /// </summary>
    public partial class ADSLModemAggragateSaleReportUserControl : Local.ReportBase
    {
       #region properties
        #endregion

        #region Constructor

        public ADSLModemAggragateSaleReportUserControl()
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
            PaymentTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPaymentType));
            ServiceGroupComboBox.ItemsSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
            //SaleWayComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLChangeServiceType));

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


        #endregion

        #region Methods
        public override void Search()
        {
            List<ADSLServiceAggragationSaleCenterSeperation> ResultADSLRequest = LoadADSLRequest();
            List<ADSLServiceAggragationSaleCenterSeperation> ResultADSLChangeService = LoadADSlChangeService();
            List<ADSLServiceAggragationSaleCenterSeperation> Result = new List<ADSLServiceAggragationSaleCenterSeperation>();
            for (int j = 0; j < ResultADSLChangeService.Count; j++)
            {  bool add = true;
            for (int i = 0; i < ResultADSLRequest.Count; i++)
                {
                  
                    if (ResultADSLRequest[i].Center == ResultADSLChangeService[j].Center 
                        && ResultADSLRequest[i].CenterCostCode == ResultADSLChangeService[j].CenterCostCode
                        && ResultADSLRequest[i].City == ResultADSLChangeService[j].City)
                    {
                        ResultADSLRequest[i].NumberOfSold += ResultADSLChangeService[j].NumberOfSold;
                        ResultADSLRequest[i].Cost += ResultADSLChangeService[j].Cost;
                        add = false;
                    }
                }
                if (add == true)
                {
                    ResultADSLRequest.Add(ResultADSLChangeService[j]);
                }
            }
            Result = ResultADSLRequest;
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

            title = "خريد مودم تجميعي به تفکيک حجم-جهت ورود به سيستم مالي";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public List<ADSLServiceAggragationSaleCenterSeperation> LoadADSLRequest()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceAggragationSaleCenterSeperation> Result = ReportDB.GetADSLRequestModemSaleAggregateSaleCenterSeperationInfo(CityComboBox.SelectedIDs,
                                                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                                                           PaymentTypeCombBox.SelectedIDs,
                                                                                                                                           ServiceGroupComboBox.SelectedIDs,
                                                                                                                                           CustomerGroupComboBox.SelectedIDs,
                                                                                                                                           FromDate.SelectedDate,
                                                                                                                                           toDate);
            return Result;
        }

        public List<ADSLServiceAggragationSaleCenterSeperation> LoadADSlChangeService()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLServiceAggragationSaleCenterSeperation> Result = ReportDB.GetADSLChangeModemSaleAggregateSaleCenterSeperationInfo(CityComboBox.SelectedIDs,
                                                                                                                                           CenterComboBox.SelectedIDs,
                                                                                                                                           PaymentTypeCombBox.SelectedIDs,
                                                                                                                                           ServiceGroupComboBox.SelectedIDs,
                                                                                                                                           CustomerGroupComboBox.SelectedIDs,
                                                                                                                                           FromDate.SelectedDate,
                                                                                                                                           toDate);
            return Result;
        }
        #endregion
    }
}
