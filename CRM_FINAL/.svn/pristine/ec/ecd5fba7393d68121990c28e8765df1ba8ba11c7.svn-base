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
using System.Reflection;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl.xaml
    /// </summary>
    public partial class ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl : Local.ReportBase
    {

        #region Constructor

        public ADSLExpirationDateAndLastServiceTelephoneNoReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            //ProvinceComboBox.ItemsSource = ProvinceDB.GetProvincesCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLTelephoneExpirationeDate> Result = new List<ADSLTelephoneExpirationeDate>();
            
            if (string.IsNullOrEmpty(PassedDaysTextBox.Text) && string.IsNullOrEmpty(RemainDaysTextBox.Text))
                Result = LoadPassedData();
            else
            {
                if (!string.IsNullOrEmpty(PassedDaysTextBox.Text))
                    Result = LoadPassedData();
                if (!string.IsNullOrEmpty(RemainDaysTextBox.Text))
                    Result = LoadRemainData();
            }
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

            title = "گزارش تاريخ انقضا براي هر تلفن";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLTelephoneExpirationeDate> LoadPassedData()
        {
            int PassedDaysTX = (string.IsNullOrEmpty(PassedDaysTextBox.Text) ? -1 : Convert.ToInt32(PassedDaysTextBox.Text) - 1);
            DateTime? PassedDays;

            if (PassedDaysTX == -1)
                PassedDays = null;
            else
                PassedDays = DB.GetServerDate().AddDays(0 - PassedDaysTX);

            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text.Trim()) ? -1 : Convert.ToInt64(TelNoTextBox.Text);

            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
                ToDate = toDate.SelectedDate.Value.AddDays(1);

            List<ADSLTelephoneExpirationeDate> result = ReportDB.GetADSLPassedExpirationDateAndLastServiceTelephoneNoInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                                                    CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                                                    TelNo, fromDate.SelectedDate, ToDate,
                                                                                                                    (!PassedDays.HasValue ? PassedDays : PassedDays.Value.Date),
                                                                                                                     HasPortCheckBox.IsChecked);
            return result;
        }

        private List<ADSLTelephoneExpirationeDate> LoadRemainData()
        {

            int RemainDaysTX = (string.IsNullOrEmpty(RemainDaysTextBox.Text) ? -1 : Convert.ToInt32(RemainDaysTextBox.Text) - 1);
            DateTime? RemainDays;

            if (RemainDaysTX == -1)
            {
                RemainDays = null;
            }
            else
            {
                RemainDays = DB.GetServerDate().AddDays(RemainDaysTX);
            }
            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text.Trim()) ? -1 : Convert.ToInt64(TelNoTextBox.Text);

            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLTelephoneExpirationeDate> result = ReportDB.GetADSLRemainExpirationDateAndLastServiceTelephoneNoInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                                                    CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                                                    TelNo, fromDate.SelectedDate, ToDate,
                                                                                                                     (!RemainDays.HasValue ? RemainDays : RemainDays.Value.Date),
                                                                                                                     HasPortCheckBox.IsChecked);
            return result;
        }

        #endregion Methods
    }
}
