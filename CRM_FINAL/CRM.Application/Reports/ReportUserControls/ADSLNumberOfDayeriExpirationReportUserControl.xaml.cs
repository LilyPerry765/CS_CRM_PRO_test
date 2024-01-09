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
    /// Interaction logic for ADSLNumberOfDayeriExpirationReportUserControl.xaml
    /// </summary>
    public partial class ADSLNumberOfDayeriExpirationReportUserControl : Local.ReportBase
    {
        
        #region Constructor

        public ADSLNumberOfDayeriExpirationReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<ADSLChargedDischargedInfo> Result = LoadData();
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

            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();
          

            List<ADSLChargedDischargedInfo> NumberOfDayeriList = ReportDB.GetNumberofCharged(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, 
                                                                                            fromDate.SelectedDate, toDate.SelectedDate);

            foreach (ADSLChargedDischargedInfo info in Result)
            {
                for (int i = 0; i < NumberOfDayeriList.Count; i++)
                {
                    if (info.Center == NumberOfDayeriList[i].Center)
                    {
                        info.NumberPrePaidOfCharged = NumberOfDayeriList[i].NumberPrePaidOfCharged;
                        info.DayeriAmountSum = NumberOfDayeriList[i].DayeriAmountSum;
                    }
                }
            }
            title = " تعداد دايري-منقضي شده در هر مرکز";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLChargedDischargedInfo> LoadData()
        {
            DateTime? ToDate = null;
            if (toDate.SelectedDate.HasValue)
            {
                ToDate = toDate.SelectedDate.Value.AddDays(1);
            }
            int PassedDaysTX=(string.IsNullOrEmpty(PassedDaysTextBox.Text)?-1 :Convert.ToInt32(PassedDaysTextBox.Text)-1);
            DateTime? PassedDays;

            if (PassedDaysTX == -1)
            {
                PassedDays=null;
            }
            else
            {
                PassedDays=DB.GetServerDate().AddDays(0 - PassedDaysTX);
            }
             
            
            List<ADSLChargedDischargedInfo> result = ReportDB.GetNumberOfExpired(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                 CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                 fromDate.SelectedDate,ToDate,
                                                                                 (!PassedDays.HasValue ? PassedDays:PassedDays.Value.Date),
                                                                                 HasPortCheckBox.IsChecked);
            return result;
        }

        #endregion Methods
    }
}
