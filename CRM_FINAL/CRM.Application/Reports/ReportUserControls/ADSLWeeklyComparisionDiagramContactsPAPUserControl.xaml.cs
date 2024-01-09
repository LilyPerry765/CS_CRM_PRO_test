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
    /// Interaction logic for ADSLWeeklyComparisionDiagramContactsPAPUserControl.xaml
    /// </summary>
    public partial class ADSLWeeklyComparisionDiagramContactsPAPUserControl : Local.ReportBase
    {
        public ADSLWeeklyComparisionDiagramContactsPAPUserControl()
        {
            InitializeComponent();
        }

        #region Method
        public override void Search()
        {
            List<ADSLContactsPAPComparisionInfo> Result = LoadData();
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
            //List<ADSLMonthlyComparisionInfo> ADSLContactsDayeriInfos = ReportDB.GetContactsDayeriInfo(CityCenterComboBox.CityComboBox.SelectedIDs,CityCenterComboBox.CenterComboBox.SelectedIDs);
            //List<ADSLMonthlyComparisionInfo> ADSLContactsDischargeInfos = ReportDB.GetContactsDischargeInfo(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
            //List<ADSLMonthlyComparisionInfo> ADSLPAPDayeriInfos = ReportDB.GetPAPDayeriInfo(CityCenterComboBox.CityComboBox.SelectedIDs,CityCenterComboBox.CenterComboBox.SelectedIDs);
            //List<ADSLMonthlyComparisionInfo> ADSLPAPDischargeInfos = ReportDB.GetPAPDischargeInfo(CityCenterComboBox.CityComboBox.SelectedIDs,CityCenterComboBox.CenterComboBox.SelectedIDs);
            //foreach (ADSLMonthlyComparisionInfo info in Result)
            //{
            //   for(int i=0;i<ADSLContactsDayeriInfos.Count;i++)
            //       if (info.Month == ADSLContactsDayeriInfos[i].Month)
            //       {
            //           info.ContactsDayeri = ADSLContactsDayeriInfos[i].ContactsDayeri;
            //           break;
            //       }

            //   for (int i = 0; i < ADSLContactsDischargeInfos.Count; i++)
            //       if (info.Month == ADSLContactsDischargeInfos[i].Month)
            //       {
            //           info.ContactsDischarge = ADSLContactsDischargeInfos[i].ContactsDischarge;
            //           break;
            //       }

            //   for (int i = 0; i < ADSLPAPDayeriInfos.Count; i++)
            //       if (info.Month == ADSLPAPDayeriInfos[i].Month)
            //       {
            //           info.PAPDayeri = ADSLPAPDayeriInfos[i].PAPDayeri;
            //           break;
            //       }

            //   for (int i = 0; i < ADSLPAPDischargeInfos.Count; i++)
            //       if (info.Month == ADSLPAPDischargeInfos[i].Month)
            //       {
            //           info.PAPDischarge = ADSLPAPDischargeInfos[i].PAPDischarge;
            //           break;
            //       }
            //   info.PurePAPDayeri = info.PAPDayeri - info.PAPDischarge;
            //   info.PureContacsDayeri = info.ContactsDayeri - info.ContactsDischarge;
            //}

            title = "نمودار مقایسه هفتگی مخابرات و شرکت های PAP";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLContactsPAPComparisionInfo> LoadData()
        {
            List<ADSLContactsPAPComparisionInfo> result = ReportDB.GetADSLWeeklyComparisionInfo(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                             CityCenterComboBox.CenterComboBox.SelectedIDs);
            return result;
        }
        #endregion Methods
    }
}
