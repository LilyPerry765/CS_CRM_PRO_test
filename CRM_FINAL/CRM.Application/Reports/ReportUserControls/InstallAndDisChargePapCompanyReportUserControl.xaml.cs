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
using System.Collections;
using System.ComponentModel;
using CRM.Data;
using CRM.Application.Reports.Viewer;


namespace CRM.Application.Reports.ReportUserControls
{
    public partial class InstallAndDisChargePapCompanyReportUserControl : Local.ReportBase
    {
        public InstallAndDisChargePapCompanyReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PapCompanyComboBox.ItemsSource = PAPInfoDB.GetPapInfo();
        }

        public override void Search()
        {
            if (PapCompanyComboBox.SelectedValue == null)
                MessageBox.Show("نام شرکت را انتخاب کنید");
            if (FromDateDP.SelectedDate == null || ToDateDP.SelectedDate == null)
                MessageBox.Show("لطفاَ بازه زمانی را مشخص کنید");

            DateTime toDate = ToDateDP.SelectedDate.Value.AddDays(1);

            IEnumerable result = LoadData(toDate);
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


            stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
            stiReport.Dictionary.Variables["Region"].Value = RegionIds;
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDateDP.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }
        public IEnumerable LoadData(DateTime toDate)
        {
            List<PapInfoReport> result = new List<PapInfoReport>();

            result = PAPInfoDB.GetPAPInfoReport(CityCenterUC.CenterCheckableComboBox.SelectedIDs, (DateTime)FromDateDP.SelectedDate, toDate, (int)PapCompanyComboBox.SelectedValue);

            PAPInfoCostHistory CostHistoryTemp = PAPInfoCostDB.GetPAPInfoCostByIdDateTime(1, (DateTime)FromDateDP.SelectedDate, (DateTime)toDate);

            double AbonmanCostValue = (CostHistoryTemp != null) ? double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(1, (DateTime)FromDateDP.SelectedDate, (DateTime)toDate).Value) : 0;


            foreach (PapInfoReport item in result)
            {
                item.DuringDay = (item.EndDate - item.StartDate).Days + 1;
                //item.PersianDischargeDate = (!item.DischargeDate.HasValue) ? "ندارد" : Helper.GetPersianDate(item.DischargeDate, Helper.DateStringType.Short);
                //item.PersianInstallDate = Helper.GetPersianDate(item.InstallDate, Helper.DateStringType.Short);
                //item.Money = (long)Math.Round((AbonmanCostValue * item.DuringDay) / DB.GetDuration((DateTime)FromDateDP.SelectedDate, (DateTime)toDate));
            }

            return result;
        }
    }
}
