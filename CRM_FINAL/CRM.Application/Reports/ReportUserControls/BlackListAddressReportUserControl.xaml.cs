﻿using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for BlackListAddressReportUserControl.xaml
    /// </summary>
    public partial class BlackListAddressReportUserControl : Local.ReportBase
    {
         public BlackListAddressReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {

            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }
        public override void Search()
        {
            List<BlackListAddressReportInfo> result = ReportDB.GetBlackListAddressInfo(CityComboBox.SelectedIDs,CenterComboBox.SelectedIDs, FromDate.SelectedDate, ToDate.SelectedDate, PostallCodeTextBox.Text.Trim() , AllCheckBox.IsChecked);

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            string title = string.Empty;
            title = " لیست سیاه آدرس  ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}