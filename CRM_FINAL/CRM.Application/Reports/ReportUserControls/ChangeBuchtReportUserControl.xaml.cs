﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ResignationLinesStatisticReportUserControl.xaml
    /// </summary>
    public partial class ChangeBuchtReportUserControl : Local.ReportBase
    {
        public ChangeBuchtReportUserControl()
        {
            InitializeComponent();
            Initialize();

        }
        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                CityCenterUC.CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
                ReasonTypeCkeckableComboBox.ItemsSource = WaitingListReasonDB.GetWaitingListReasonCheckableByRequestTypeID();
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
            List<ReleaseDocuments> result =
                ReportDB.GetChangeBuchts(CityCenterUC.CenterCheckableComboBox.SelectedIDs, ReasonTypeCkeckableComboBox.SelectedIDs
                                         ,FromEnterDate.SelectedDate, ToEnterDate.SelectedDate);
            foreach (ReleaseDocuments item in result)
            {
                item.ExitDate = string.IsNullOrEmpty(item.ExitDateTime.ToString()) ? "" : Helper.GetPersianDate(item.ExitDateTime, Helper.DateStringType.Short);
                item.ExitHour = string.IsNullOrEmpty(item.ExitDateTime.ToString()) ? "" : Helper.GetPersianDate(item.ExitDateTime, Helper.DateStringType.Time);

                item.EnterDate = string.IsNullOrEmpty(item.EnterDateTime.ToString()) ? "" : Helper.GetPersianDate(item.EnterDateTime, Helper.DateStringType.Short);
                item.EnterHour = string.IsNullOrEmpty(item.EnterDateTime.ToString()) ? "" : Helper.GetPersianDate(item.EnterDateTime, Helper.DateStringType.Time);
            }


            return result;
        }


    }
}