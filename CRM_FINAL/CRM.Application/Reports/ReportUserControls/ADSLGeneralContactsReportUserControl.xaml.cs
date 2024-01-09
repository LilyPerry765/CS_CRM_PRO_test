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
    /// Interaction logic for ADSLGeneralContactsReportUserControl.xaml
    /// </summary>
    public partial class ADSLGeneralContactsReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion

        #region Constructor
        public ADSLGeneralContactsReportUserControl()
        {
            InitializeComponent();
        }
        #endregion Constructor

        #region Methods

        public override void Search()
        {
           List<ADSLGeneralContactsInfo> Result = LoadData();
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

            List<ADSLGeneralContactsInfo> YearInstalled = ReportDB.YearADSLInstalledPorts(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekPrePaidInternet = ReportDB.WeekPrePaidInternet(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekPostpaidInternet = ReportDB.WeekPostpaidInternet(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekNumberOfInteranet = ReportDB.WeekNumberOfInteranet(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekTotalDayeri = ReportDB.WeekTotalDayeri(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekNumberOfDischarge = ReportDB.WeekNumberOfDischarge(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> ADSLInstalledPortsYear91 = ReportDB.ADSLInstalledPortsYear91(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> ADSLInstalledPortsYear92 = ReportDB.ADSLInstalledPortsYear92(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> WeekADSLAssignmentFilePorts = ReportDB.WeekADSLAssignmentFilePorts(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> NumberOfDayeriFromStartOfYear = ReportDB.NumberOfDayeriFromStartOfYear(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> NumberOfDischargedFromStartOfYear = ReportDB.NumberOfDischargedFromStartOfYear(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CurrentWeekADSLIDayeriPorts = ReportDB.CurrentWeekADSLIDayeriPorts(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CurrentWeekADSLInstalledPorts = ReportDB.CurrentWeekADSLInstalledPorts(CityCenterComboBox.CenterComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs);

            foreach (ADSLGeneralContactsInfo Info in Result)
            {
                //if (int.Parse(Info.WeekADSLAssignmentFilePorts )== 0)
                //    Info.DayeriInstalledPercentage = 0;

                for (int i = 0; i < YearInstalled.Count; i++)
                {
                    if (Info.CenterName == YearInstalled[i].CenterName)
                        Info.Installed = YearInstalled[i].Installed;
                }

                for (int i = 0; i < WeekPrePaidInternet.Count; i++)
                {
                    if (Info.CenterName == WeekPrePaidInternet[i].CenterName)
                    {
                        Info.WeekPrePaidInternet = WeekPrePaidInternet[i].WeekPrePaidInternet;
                        break;
                    }
                       
                }

                for (int i = 0; i < WeekPostpaidInternet.Count; i++)
                {
                    if (Info.CenterName == WeekPostpaidInternet[i].CenterName)
                    {
                        Info.WeekPostpaidInternet = WeekPostpaidInternet[i].WeekPostpaidInternet;
                        break;
                    }
                }

                for (int i = 0; i < WeekNumberOfInteranet.Count; i++)
                {
                    if (Info.CenterName == WeekNumberOfInteranet[i].CenterName)
                    {
                        Info.WeekInteranet = WeekNumberOfInteranet[i].WeekInteranet;
                        break;
                    }
                }

                for (int i = 0; i < WeekTotalDayeri.Count; i++)
                {
                    if (Info.CenterName == WeekTotalDayeri[i].CenterName)
                    {
                        Info.WeekTotalDayeri = WeekTotalDayeri[i].WeekTotalDayeri;
                        break;
                    }
                }

                for (int i = 0; i < WeekNumberOfDischarge.Count; i++)
                {
                    if (Info.CenterName == WeekNumberOfDischarge[i].CenterName)
                    {
                        Info.WeekDischarge = WeekNumberOfDischarge[i].WeekDischarge;
                        break;
                    }
                }

                for (int i = 0; i < ADSLInstalledPortsYear91.Count; i++)
                {
                    if (Info.CenterName == ADSLInstalledPortsYear91[i].CenterName)
                    {
                        Info.WeekADSLPorts91 = ADSLInstalledPortsYear91[i].WeekADSLPorts91;
                        break;
                    }
                }

                for (int i = 0; i < ADSLInstalledPortsYear92.Count; i++)
                {
                    if (Info.CenterName == ADSLInstalledPortsYear92[i].CenterName)
                    {
                        Info.WeekADSLPorts92 = ADSLInstalledPortsYear92[i].WeekADSLPorts92;
                        break;
                    }
                }
                for (int i = 0; i < WeekADSLAssignmentFilePorts.Count; i++)
                {
                    if (Info.CenterName == WeekADSLAssignmentFilePorts[i].CenterName)
                    {
                        Info.WeekADSLAssignmentFilePorts = WeekADSLAssignmentFilePorts[i].WeekADSLAssignmentFilePorts;
                        break;
                    }
                }

                for (int i = 0; i < NumberOfDayeriFromStartOfYear.Count; i++)
                {
                    if (Info.CenterName == NumberOfDayeriFromStartOfYear[i].CenterName)
                    {
                        Info.NumberOfDayeriStartOfYear = NumberOfDayeriFromStartOfYear[i].NumberOfDayeriStartOfYear;
                        break;
                    }
                }

                for (int i = 0; i < NumberOfDischargedFromStartOfYear.Count; i++)
                {
                    if (Info.CenterName == NumberOfDischargedFromStartOfYear[i].CenterName)
                    {
                        Info.NumberOfDischargedStartOfYear = NumberOfDischargedFromStartOfYear[i].NumberOfDischargedStartOfYear;
                        break;
                    }
                }

                for (int i = 0; i < CurrentWeekADSLIDayeriPorts.Count; i++)
                {
                    if (Info.CenterName == CurrentWeekADSLIDayeriPorts[i].CenterName)
                    {
                        Info.CurrentWeekNumberOfDayeriPorts = CurrentWeekADSLIDayeriPorts[i].CurrentWeekNumberOfDayeriPorts;
                        break;
                    }
                }

                for (int i = 0; i < CurrentWeekADSLInstalledPorts.Count; i++)
                {
                    if (Info.CenterName == CurrentWeekADSLInstalledPorts[i].CenterName)
                    {
                        Info.CurrentWeekNumberOfDInstalledPorts = CurrentWeekADSLInstalledPorts[i].CurrentWeekNumberOfDInstalledPorts;
                        break;
                    }
                }

                //Info.DayeriInstalledPercentage = (Info.CurrentWeekNumberOfDayeriPorts / Info.CurrentWeekNumberOfDInstalledPorts) * 100;
                //Info.WeekPureDayeri = (int.Parse(Info.WeekTotalDayeri) - int.Parse(Info.WeekDischarge)).ToString();

            }
            title = "گزارش کلی ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);
            stiReport.CompileStandaloneReport("", StiStandaloneReportType.ShowWithWpf);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

         }

        private List<ADSLGeneralContactsInfo> LoadData()
        {
            List<ADSLGeneralContactsInfo> result = ReportDB.GetGenrealContactsInfo
                                                    (CityCenterComboBox.CityComboBox.SelectedIDs,
                                                    CityCenterComboBox.CenterComboBox.SelectedIDs);
            return result;
        }
#endregion Methods
    }
}
