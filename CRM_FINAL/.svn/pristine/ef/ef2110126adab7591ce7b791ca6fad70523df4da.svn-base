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
using System.ComponentModel;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for DayeriReportUserControl.xaml
    /// </summary>
    public partial class ADSLDayeriRequestReportUserControl :Local.ReportBase
    {
        #region Properties And Fields
        #endregion  Properties And Fields

        #region Constructor

        public ADSLDayeriRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                RegionIdComboBox.ItemsSource = RegionDB.GetRegions();
                CustomerOwnerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
                ServiceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLServiceType));
                TariffTypeComboBox.ItemsSource = ADSLServiceDB.GetADSLService();//ADSLTariffDB.GetADSLService();

                CurrentStepComboBox.ItemsSource = RequestStepDB.GetRequestStepCheckable();
            }
        }
        #endregion  Initializer

        #region Event Handlers

        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterIdComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)(sender as ComboBox).SelectedValue);
        }

        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {
            DateTime? ToRequestDate = null;
            if (RequestToDate.SelectedDate.HasValue)
            {
                ToRequestDate = RequestToDate.SelectedDate.Value.AddDays(1);
            }
            DateTime? ToRanjeDate = null;
            if (RanjeToDate.SelectedDate.HasValue)
            {
                ToRanjeDate = RanjeToDate.SelectedDate.Value.AddDays(1);
            }
            string title = string.Empty;
            string path;
            List<ADSLRequestInfo> Result = ReportDB.GetDayeriInfo(RequestFromDate.SelectedDate, ToRequestDate,
                                                                 RanjeFromDate.SelectedDate, ToRanjeDate,
                                                                 DayeriFromDate.SelectedDate, DayeriToDate.SelectedDate,
                                                                 string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(PhoneNoTextBox.Text.TrimEnd()),
                                                                 string.IsNullOrEmpty(ServiceTypeComboBox.Text) ? (int?)null : int.Parse(ServiceTypeComboBox.SelectedValue.ToString()),
                                                                 string.IsNullOrEmpty(TariffTypeComboBox.Text) ? (int?)null : int.Parse(TariffTypeComboBox.SelectedValue.ToString()),
                                                                 CurrentStepComboBox.SelectedIDs,
                                                                 string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString()),
                                                                 string.IsNullOrEmpty(NationalIdTextBox.Text.Trim()) ? (string)null : NationalIdTextBox.Text,
                                                                 string.IsNullOrEmpty(CustomerOwnerStatusComboBox.Text) ? (int?)null : int.Parse(CustomerOwnerStatusComboBox.SelectedValue.ToString()));


            List<EnumItem> adslServiceTypeList = Helper.GetEnumItem(typeof(DB.ADSLServiceType));
            List<EnumItem> adslOwnerStatus = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
            foreach (ADSLRequestInfo adslRequestInfo in Result)
            {
                //-----
                //adslRequestInfo.ServiceType = adslServiceTypeList.Find(item => item.ID == byte.Parse(adslRequestInfo.ServiceType)).Name;
                //-----
                adslRequestInfo.CustomerOwnerStatus = adslOwnerStatus.Find(item => item.ID == int.Parse(adslRequestInfo.CustomerOwnerStatus)).Name;
                adslRequestInfo.RequestDate = string.IsNullOrEmpty(adslRequestInfo.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslRequestInfo.RequestDate), Helper.DateStringType.Short);
                adslRequestInfo.DayeriDate = string.IsNullOrEmpty(adslRequestInfo.DayeriDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(adslRequestInfo.DayeriDate), Helper.DateStringType.Short);

                Center center = Data.CenterDB.GetCenterById(Convert.ToInt32(adslRequestInfo.Region));
                if (center != null)
                {
                    Region region =Data.RegionDB.GetRegionById(center.RegionID);
                    if (region != null)
                    {
                        adslRequestInfo.Region = region.Title;
                    }
                }
            }
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(RequestFromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(RequestToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            if (RegionIdComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            }

            if (CenterIdComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            }

            
            title = "گزارش درخواست دایری ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion Methods
        
    }
}
