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
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for DayeriRequestReportUserControl.xaml
    /// </summary>
    public partial class DayeriRequestReportUserControl :Local.ReportBase
    {
        #region Constructor
        public DayeriRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Constructor

        #region Initializer
        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            RequestPhoneTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneType));
        }
        #endregion Initializer

        #region Event Handlers
        #endregion Event Handlers

        #region Methods

        public override void Search()
        {
            List<InstallRequestInfo> result = LoadData();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);        
            stiReport.Load(path);
            title = " گزارش درخواست دایری  ";
  
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromInstallationDatePicker.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToInstallationDatePicker.SelectedDate, Helper.DateStringType.Short).ToString();

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            //}
            //if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            //}


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }


        private List<InstallRequestInfo> LoadData()
        {

            List<InstallRequestInfo> result = 
            ReportDB.GetInstallProcessInfo(
                                     CityComboBox.SelectedIDs,
                                     CenterComboBox.SelectedIDs,
                                     NationalIDTextBox.Text,
                                     string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? (long?)null : long.Parse(RequestNoTextBox.Text),
                                     string.IsNullOrEmpty(RequestStepComboBox.Text) ? (int?)null : int.Parse(RequestStepComboBox.SelectedValue.ToString()),
                                     string.IsNullOrEmpty(RequestPhoneTypeComboBox.Text) ? (int?)null : int.Parse(RequestPhoneTypeComboBox.SelectedValue.ToString()),
                                     string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(PhoneNoTextBox.Text.Trim()),
                                     FromInstallationDatePicker.SelectedDate,
                                     ToInstallationDatePicker.SelectedDate
                                     );

            
            List<EnumItem> orderTypeList = Helper.GetEnumItem(typeof(DB.OrderType));
            List<EnumItem> chargingGroupList = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            List<EnumItem> possessionTypeList = Helper.GetEnumItem(typeof(DB.PossessionType));
            List<InvestigatePossibility> IP = InvestigatePossibilityDB.GetInvestigatePossibility();            

            foreach (InstallRequestInfo installRequestInfo in result)
            {

                EnumItem EOT = orderTypeList.Find(item => item.ID == int.Parse(installRequestInfo.OrderType));
                if (EOT != null)
                    installRequestInfo.OrderType = EOT.Name;

                EnumItem EPT = chargingGroupList.Find(item => item.ID == int.Parse(installRequestInfo.PosessionType));
                if (EPT != null)
                    installRequestInfo.PosessionType = EPT.Name;

                EnumItem ECT = chargingGroupList.Find(item => item.ID == int.Parse(installRequestInfo.ChargingType));
                if (ECT != null)
                    installRequestInfo.ChargingType = ECT.Name;

                installRequestInfo.InsertDate = Helper.GetPersianDate(DateTime.Parse(installRequestInfo.InsertDate), Helper.DateStringType.Short);
                installRequestInfo.LetterDateOfReinstall = string.IsNullOrEmpty(installRequestInfo.LetterDateOfReinstall) ? (string)null : Helper.GetPersianDate(DateTime.Parse(installRequestInfo.LetterDateOfReinstall), Helper.DateStringType.Short);
                installRequestInfo.LicenseDate = string.IsNullOrEmpty(installRequestInfo.LicenseDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(installRequestInfo.LicenseDate), Helper.DateStringType.Short);
                installRequestInfo.RequestDate = string.IsNullOrEmpty(installRequestInfo.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(installRequestInfo.RequestDate), Helper.DateStringType.Short);
                installRequestInfo.ModifyDate = string.IsNullOrEmpty(installRequestInfo.ModifyDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(installRequestInfo.ModifyDate), Helper.DateStringType.Short);
                installRequestInfo.RequestLetterDate = string.IsNullOrEmpty(installRequestInfo.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(installRequestInfo.RequestLetterDate), Helper.DateStringType.Short);
               // Address CustomerAdd = DB.SearchByPropertyName<Address>("ID", installRequestInfo.AddressID).SingleOrDefault();
                
                //if (CustomerAdd != null)
                //{
                //    InvestigatePossibility Invs = IP.Find(t => t.CustomerAddressID == CustomerAdd.ID);
                //    if (Invs != null)
                //        installRequestInfo.TelephoneNo = Invs.TelephoneNo.ToString();
                //}
            }
            //if (!string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()))
            //{
            //    result = FilterByNumber(result, PhoneNoTextBox.Text.Trim());
            //}
            return result;
        }

        private static List<InstallRequestInfo> FilterByNumber(List<InstallRequestInfo> resultList, string TelephoneNo)
        {
            if (resultList.First().InstallRequestType == ((int)DB.RequestType.Reinstall).ToString())
            {
                return resultList.Where(t => (TelephoneNo == null || t.PassTelephone == TelephoneNo || t.CurrentTelephone == TelephoneNo)).ToList();
            }
            else if (resultList.First().InstallRequestType == ((int)DB.RequestType.Dayri).ToString())
            {
                return resultList.Where(t => (TelephoneNo == null || t.TelephoneNo == TelephoneNo)).ToList();
            }

            return null;
        }

        #endregion  Methods

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        

        
    }
}
