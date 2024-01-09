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
    /// Interaction logic for ADSLCityContactsGeneralReportUserControl.xaml
    /// </summary>
    public partial class ADSLCityContactsGeneralReportUserControl : Local.ReportBase
    {
        public ADSLCityContactsGeneralReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #region Initializer
        public void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion

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

            stiReport.Dictionary.Variables["ReportExplaination"].Value = ReportExplainationTextBox.Text;

            List<ADSLGeneralContactsInfo> CityYearADSLInstalledPorts = ReportDB.CityYearADSLInstalledPorts(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CityADSLAssignedPortsYear91 = ReportDB.CityADSLAssignedPortsYear91(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CityYearADSLAssignedPorts = ReportDB.CityYearADSLAssignedPorts(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CityNumberOfDayeriFromStartOfYear = ReportDB.CityNumberOfDayeriFromStartOfYear(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> CityNumberOfDischargedFromStartOfYear = ReportDB.CityNumberOfDischargedFromStartOfYear(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> ADSLInteranetPorts = ReportDB.GetCityNumberofInteranetPorts(CityComboBox.SelectedIDs);
            List<ADSLGeneralContactsInfo> ADSLInternetPorts = ReportDB.GetCityNumberofInternetPorts(CityComboBox.SelectedIDs);


            foreach (ADSLGeneralContactsInfo Info in Result)
            {
                for (int i = 0; i < CityYearADSLInstalledPorts.Count; i++)
                {
                    if (Info.CityName == CityYearADSLInstalledPorts[i].CityName)
                    {
                        Info.CityInstalled = CityYearADSLInstalledPorts[i].CityInstalled;
                        break;
                    }
                }

                for (int i = 0; i < CityADSLAssignedPortsYear91.Count; i++)
                {
                    if (Info.CityName == CityADSLAssignedPortsYear91[i].CityName)
                    {
                        Info.CityADSLAssignedPorts91 = CityADSLAssignedPortsYear91[i].CityADSLAssignedPorts91;
                        break;
                    }
                }

                for (int i = 0; i < CityYearADSLAssignedPorts.Count; i++)
                {
                    if (Info.CityName == CityYearADSLAssignedPorts[i].CityName)
                    {
                        Info.CityADSLAssignedPorts92 = CityYearADSLAssignedPorts[i].CityADSLAssignedPorts92;
                        break;
                    }
                }

                for (int i = 0; i < CityNumberOfDayeriFromStartOfYear.Count; i++)
                {
                    if (Info.CityName == CityNumberOfDayeriFromStartOfYear[i].CityName)
                    {
                        Info.CityADSLDayeriStartOfYear = CityNumberOfDayeriFromStartOfYear[i].CityADSLDayeriStartOfYear;
                        break;
                    }
                }

                for (int i = 0; i < CityNumberOfDischargedFromStartOfYear.Count; i++)
                {
                    if (Info.CityName == CityNumberOfDischargedFromStartOfYear[i].CityName)
                    {
                        Info.CityADSLDischargeStartOfYear = CityNumberOfDischargedFromStartOfYear[i].CityADSLDischargeStartOfYear;
                        break;
                    }
                }

                for (int i = 0; i < ADSLInternetPorts.Count; i++)
                {
                    if (Info.CityName == ADSLInternetPorts[i].CityName)
                    {
                        Info.ADSLInternetPorts = ADSLInternetPorts[i].ADSLInternetPorts;
                        break;
                    }
                }

                for (int i = 0; i < ADSLInteranetPorts.Count; i++)
                {
                    if (Info.CityName == ADSLInteranetPorts[i].CityName)
                    {
                        Info.ADSLInteranetPorts = ADSLInteranetPorts[i].ADSLInteranetPorts;
                        break;
                    }
                }

            }
            title = "گزارش کلي ADSL مخابرات شهرستان به شهرستان";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLGeneralContactsInfo> LoadData()
        {
            List<ADSLGeneralContactsInfo> result = ReportDB.GetCityGenrealContactsInfo
                                                    (CityComboBox.SelectedIDs);
            return result;
        }
        #endregion Methods
    }
}
