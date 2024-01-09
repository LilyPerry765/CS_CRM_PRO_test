using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    ///
    /// </summary>
    public partial class InvestigatePossibilityReportUserControl : Local.ReportBase
    {
        #region Properties And Fields
        #endregion Properties And Fields

       
         #region Constructor

        public InvestigatePossibilityReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            List<Region> lstRegion = Data.RegionDB.GetRegions();
            RegionIdComboBox.ItemsSource = lstRegion;
            RegionIdComboBox.SelectedValuePath = "ID";
            RegionIdComboBox.DisplayMemberPath = "Title";

            List<EnumItem> lst = Helper.GetEnumItem(typeof(DB.TelephoneStatus));
            TelephoneStatusComboBox.ItemsSource = lst;

        }

        #endregion Initializer

        #region Event Handlers

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            //TODO:DayeriReportUserControl.ReportButton
            Search();
        }
        #endregion  Event Handlers
                
        #region Mehtods
        private  List<InvestigatePossibilityInfo> LoadData()
        {

            List<InvestigatePossibilityRaw> list = ReportDB.GetInvestigatePossibility(string.IsNullOrEmpty(txtTelephoneNo.Text.Trim()) ? (string)null : txtTelephoneNo.Text.Trim()
                                                                                    , FromDate.SelectedDate,ToDate.SelectedDate
                                                                                    , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                                    ,string.IsNullOrEmpty(TelephoneStatusComboBox.Text) ? (int?)null : int.Parse(TelephoneStatusComboBox.SelectedValue.ToString()));

            List<InvestigatePossibilityInfo> Temp = new List<InvestigatePossibilityInfo>();
            
            List<EnumItem> EI = Helper.GetEnumItem(typeof(DB.PostContactConnectionType));
            Address address = new Address();
           

            foreach (InvestigatePossibilityRaw item in list )
            {
                InvestigatePossibilityInfo IPI = new InvestigatePossibilityInfo();
                IPI.WiringNo = item.WiringNo.ToString();
                IPI.WiringHour = item.WiringHour;
                IPI.WiringDate = string.IsNullOrEmpty(item.WiringDate.ToString()) ? (string)null : Helper.GetPersianDate(item.WiringDate, Helper.DateStringType.Short);
                IPI.WiringIssueDate = string.IsNullOrEmpty(item.WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(item.WiringIssueDate, Helper.DateStringType.Short);
                IPI.CabinetNumber = item.CabinetNumber.ToString();
                IPI.ConnectionID = DB.GetConnectionByBuchtID(item.ConnectionID);
                IPI.ConnectionNo = item.ConnectionNo.ToString();
                IPI.ConnectionReserveDate = string.IsNullOrEmpty(item.ConnectionReserveDate.ToString()) ? (string)null : Helper.GetPersianDate(item.ConnectionReserveDate, Helper.DateStringType.Short);


                IPI.ConnectionType = (item.ConnectionType != null) ? EI.Find(i => i.ID == item.ConnectionType).Name : " ";
                IPI.CounterNo = item.CounterNo;
                IPI.HasReport = (item.HasReport.ToString() == "1") ? "گزارش دارد" : "گزارش ندارد";
                IPI.HasWaitingInfo = (item.HasWaitingInfo.ToString() == "1") ? "سابقه ثبت در لیست انتظار دارد" : "سابقه ثبت در لیست انتظار ندارد";
                IPI.InputNumber = item.InputNumber.ToString();
                IPI.MDFWiringDate = string.IsNullOrEmpty(item.MDFWiringDate.ToString()) ? (string)null : Helper.GetPersianDate(item.MDFWiringDate, Helper.DateStringType.Short);
                IPI.MDFWiringHour = item.MDFWiringHour;
                IPI.Number = item.Number.ToString();
                IPI.InsertDate = string.IsNullOrEmpty(item.InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short);
                IPI.TelephoneNo = item.TelephoneNo.ToString();
                IPI.RequestID = item.RequestID.ToString();
                IPI.Center = item.Center;
                IPI.Region = item.Region;
                //IPI.AddressContact = item.Address;
                //IPI.PostCodeContact = item.PostalCode;
                IPI.InstallAddress = item.InstallAddress;
                IPI.InstallPostalcode = item.InstallPostalcode;
                IPI.CorrespondenceAddress = item.CorrespondenceAddress;
                IPI.CorrespondencePostalCode = item.CorrespondencePostalCode;
                IPI.CustomerName = item.CustomerName;
               

                //address=DB.SearchByPropertyName<Address>("ID", IPI.RequestID)

                //IPI.AddressInstall = CA.Find(item => (item.RequestID.ToString() == IPI.RequestID && item.AddressTypeID == (int)DB.AddressType.Install)).Address;
                //IPI.PostCodeinstall = CA.Find(item => (item.RequestID.ToString() == IPI.RequestID && item.AddressTypeID == (int)DB.AddressType.Install)).PostalCode;

                Temp.Add(IPI);
            }
            return Temp;
            
 	         
        }
        public override void  Search()
        {
            List<InvestigatePossibilityInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

           
            stiReport.Dictionary.Variables["data_a"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["data_b"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            if (RegionIdComboBox.Text != string.Empty)

                stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            if (CenterIdComboBox.Text != string.Empty)
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            string _title = string.Empty;

            _title = string.IsNullOrEmpty(TelephoneStatusComboBox.Text) ? "گزارش کامل تجهیزات فنی" : "گزارش تجهیزات فنی خطوط " + Helper.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), int.Parse(TelephoneStatusComboBox.SelectedValue.ToString()));
                      
            
            
            stiReport.Dictionary.Variables["ci_report_header"].Value = _title;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

            
        }
        #endregion  Mehtods

        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<CenterInfo> lstCenter = Data.CenterDB.GetCenterByCityId(int.Parse(RegionIdComboBox.SelectedValue.ToString()));
            CenterIdComboBox.ItemsSource = lstCenter;
            CenterIdComboBox.SelectedValuePath = "ID";
            CenterIdComboBox.DisplayMemberPath = "CenterName";
        }
        

       
    }
}
