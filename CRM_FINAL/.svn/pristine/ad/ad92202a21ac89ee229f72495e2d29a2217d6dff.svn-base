
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
using System.Collections;
using System.ComponentModel;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for FailureCorrectPortsReportUserControl.xaml
    /// </summary>
    public partial class DayeriRequestFormReportUserControl : Local.ReportBase
    {
        public DayeriRequestFormReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            }
        }
        public override void Search()
        {
            long telephoneNo = 0;
            if(!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim())) long.TryParse(TelephoneNoTextBox.Text.Trim(), out telephoneNo);

            List<RequestPaymentReport> result_Temp = ReportDB.GetRequestPayment(new List<long> { }, FromDate.SelectedDate, ToDate.SelectedDate, CityCenterUC.CenterCheckableComboBox.SelectedIDs, telephoneNo);
            IEnumerable result = LoadData(ref result_Temp);
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            string CenterIds = string.Empty;
            string RegionIds = string.Empty;
            List<Center> centerList = CenterDB.GetAllCenter();
            List<int> CenterIDs = CityCenterUC.CenterCheckableComboBox.SelectedIDs;
            List<int> CityIDs = CityCenterUC.CityComboBox.SelectedIDs;
            if (CenterIDs.Count != 0)
            {
                foreach (int _centerIds in CenterIDs)
                {
                    CenterIds += centerList.Find(item => item.ID == _centerIds).CenterName + " ,";
                }
                CenterIds = CenterIds.Substring(0, CenterIds.Length - 1);
            }


            List<City> Citylist = CityDB.GetAllCity();
            if (CityIDs.Count != 0)
            {
                foreach (int _cityrIds in CityIDs)
                {
                    RegionIds += Citylist.Find(item => item.ID == _cityrIds).Name + " ,";
                }
                RegionIds = RegionIds.Substring(0, RegionIds.Length - 1);
            }


            //stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
            //stiReport.Dictionary.Variables["Region"].Value = RegionIds;
            //stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("DetailsResult", "DetailsResult", result_Temp);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public List<InstallRequestReport> LoadData(ref List<RequestPaymentReport> result_Temp)
        {
            List<InstallRequestReport> result = new List<InstallRequestReport>();
            long? telephoneNo = !string.IsNullOrEmpty(TelephoneNoTextBox.Text) ? Convert.ToInt64(TelephoneNoTextBox.Text) : -1;
            result = ReportDB.GetInstallProcessReport(FromDate.SelectedDate, ToDate.SelectedDate, null, new List<long> { }, CityCenterUC.CenterCheckableComboBox.SelectedIDs, null, null, telephoneNo);
            List<EnumItem> ChargingGroup = Helper.GetEnumItem(typeof(DB.ChargingGroup));
            List<EnumItem> OrderType = Helper.GetEnumItem(typeof(DB.OrderType));
            List<EnumItem> PossessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
            List<EnumItem> PaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));

            foreach (InstallRequestReport item in result)
            {
                item.ChargingType = string.IsNullOrEmpty(item.ChargingType) ? "" : ChargingGroup.Find(i => i.ID == byte.Parse(item.ChargingType)).Name;
                item.OrderType = string.IsNullOrEmpty(item.ChargingType) ? "" : OrderType.Find(i => i.ID == byte.Parse(item.OrderType)).Name;
                item.PosessionType = string.IsNullOrEmpty(item.PosessionType) ? "" : PossessionType.Find(i => i.ID == byte.Parse(item.PosessionType)).Name;
                item.RequestPaymentType = string.IsNullOrEmpty(item.RequestPaymentType) ? "" : PaymentType.Find(i => i.ID == byte.Parse(item.RequestPaymentType)).Name;
                item.PersianInstallInsertDate = item.InstallInsertDate.HasValue ? Helper.GetPersianDate(item.InstallInsertDate, Helper.DateStringType.Short) : "";
                item.PersianInsertDate = item.InsertDate.HasValue ? Helper.GetPersianDate(item.InsertDate, Helper.DateStringType.Short) : "";
                item.PersianInstallationDate = item.InstallationDate.HasValue ? Helper.GetPersianDate(item.InstallationDate, Helper.DateStringType.Short) : "";
            }
            //List<RequestPaymentReport> result_Temp = ReportDB.GetRequestPayment(new List<long> {});
            foreach (RequestPaymentReport item in result_Temp)
            {
                item.PersianFicheDate = (item.FicheDate.HasValue) ? Helper.GetPersianDate(item.FicheDate, Helper.DateStringType.Short) : "";
            }
            return (result);
        }



    }
}


