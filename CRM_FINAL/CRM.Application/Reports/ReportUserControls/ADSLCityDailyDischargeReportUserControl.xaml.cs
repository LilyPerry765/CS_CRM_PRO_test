﻿using System;
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
using System.Reflection;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLCityDailyDischargeReportUserControl.xaml
    /// </summary>
    public partial class ADSLCityDailyDischargeReportUserControl : Local.ReportBase
    {

        #region Constructor
        public ADSLCityDailyDischargeReportUserControl()
        {
            InitializeComponent();
             Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            //CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            //if (CityComboBox.SelectedIndex < 0)
            //{
            //    MessageBox.Show("لطفا شهرستان مورد نظر خود را انتخاب کنید");
            //}
            //else
            //{
                List<RequestInfo> Result = LoadData();
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
                stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

                if (FromDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
                if (ToDate.SelectedDate != null)
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

                title = "گزارش روزانه تخليه شهرستان";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            //}

        }

        private List<RequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<RequestInfo> result = ReportDB.GetCityDischargeStatistics(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                           CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                FromDate.SelectedDate,
                                                                                toDate);
            return result;
        }
        #endregion Methods
    }
}
