
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
    public partial class PerformanceFailure117ReportUserControl : Local.ReportBase
    {
        public PerformanceFailure117ReportUserControl()
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
            IEnumerable result = LoadData();
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

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public IEnumerable LoadData()
        {
            DateTime now = DB.GetServerDate();
            DateTime Today = new DateTime(now.Year, now.Month, now.Day, 01, 0, 0);


            List<Failure117PerformanceTotal> resultTotal = ReportDB.GetFailure117Performance(CityCenterUC.CenterCheckableComboBox.SelectedIDs);
            List<Failure117PerformanceTotal> resultFromTotal = ReportDB.GetFailure117FormPerformance(CityCenterUC.CenterCheckableComboBox.SelectedIDs);


            List<Failure117Performancereport> Result = new List<Failure117Performancereport>();
            Failure117Performancereport Record = new Failure117Performancereport();

            Record.Request_Today = resultTotal.Where(t => t.InsertDate_Request >= Today).Count();
            Record.ConfirmRequest_Today = resultTotal.Where(t => (t.MDFDate_Request.HasValue && t.MDFDate_Request >= Today)).Count();
            Record.RequestInCartabl = resultTotal.Where(t => t.Status_Request == 1363).Count();
            Record.RequestRemaindFromYesterday = resultTotal.Where(t => (t.Status_Request == 1363) && (t.InsertDate_Request < Today)).Count();

            Record.Network_Request_Today = resultFromTotal.Where(t => (t.InserDate_FailureForm.HasValue && t.InserDate_FailureForm >= Today)).Count();
            Record.Network_ConfirmRequest_Today = resultTotal.Where(t => (t.NetworkDate_Failure117.HasValue && t.NetworkDate_Failure117 >= Today)).Count();
            Record.Network_RequestInCartabl = resultTotal.Where(t => t.Status_Request == 1364).Count();
            Record.Network_RequestRemaindFromYesterday = resultTotal.Where(t => (t.Status_Request == 1364) && (t.MDFDate_Request.HasValue && t.MDFDate_Request < Today)).Count();

            Record.SaloonRequest_Today = 0;
            Record.SaloonConfirmRequest_Today = resultTotal.Where(t => (t.SaloonDate_Failure117.HasValue && t.SaloonDate_Failure117 >= Today)).Count();
            Record.SaloonRequestInCartabl = resultTotal.Where(t => t.Status_Request == 1366).Count();
            Record.SaloonRequestRemaindFromYesterday = resultTotal.Where(t => (t.Status_Request == 1366) && (t.InsertDate_Request < Today)).Count();

            Record.MDF_Request_Today = resultTotal.Where(t => (t.NetworkDate_Failure117.HasValue && t.NetworkDate_Failure117 >= Today) || (t.SaloonDate_Failure117.HasValue && t.SaloonDate_Failure117 >= Today)).Count();
            Record.MDF_ConfirmRequest_Today = resultTotal.Where(t => (t.EndMDFDate.HasValue && t.EndMDFDate >= Today)).Count();
            Record.MDF_RequestInCartabl = resultTotal.Where(t => t.Status_Request == 1367).Count();
            Record.MDF_RequestRemaindFromYesterday = resultTotal.Where(t => (t.Status_Request == 1367) && ((t.NetworkDate_Failure117.HasValue && t.NetworkDate_Failure117 < Today)
                                                                         || (t.SaloonDate_Failure117.HasValue && t.SaloonDate_Failure117 < Today))).Count();

            Result.Add(Record);

            return Result;
        }


    }
}


