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
    /// Interaction logic for ADSLPortsCityCenterReportUserControl.xaml
    /// </summary>
    public partial class ADSLPortsCityCenterReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ADSLPortsCityCenterReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Initializer
        public void Initialize()
        {
        }
        #endregion

        #region Methods

        public override void Search()
        {
            List<ADSLPortsInfo> Result = LoadData();
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

            List<int> MDF = new List<int>();
            
            List<ADSLPortsInfo> NumberOfUsedPorts = ReportDB.GetNumberOfUsedADSLPorts(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs,MDF);
            List<ADSLPortsInfo> NumberOfPorts = ReportDB.GetNumberOfADSLPorts(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
            foreach (ADSLPortsInfo info in Result)
            {
                //for (int i = 0; i < NumberOfFreePorts.Count; i++)
                //{
                //    if (NumberOfFreePorts[i].CenterName == info.CenterName)
                //        info.NumberOfFreePorts = NumberOfFreePorts[i].NumberOfFreePorts;
                //}

                for (int i = 0; i < NumberOfUsedPorts.Count; i++)
                {
                    if (NumberOfUsedPorts[i].CenterName == info.CenterName)
                        info.NumberOfUsedPorts = NumberOfUsedPorts[i].NumberOfUsedPorts;
                }

                for (int i = 0; i < NumberOfPorts.Count; i++)
                {
                    if (NumberOfPorts[i].CenterName == info.CenterName)
                        info.NumberOfPorts = NumberOfPorts[i].NumberOfPorts;
                }
            }
           
            title = "گزارش تعداد پورت های قابل واگذاری به تفکیک شهر و مرکز";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLPortsInfo> LoadData()
        {
            //List<ADSLPortsInfo> result = ReportDB.GetADSLTransferablePortsInfo(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
            List<int> MDF = new List<int>();
            List<ADSLPortsInfo> result = ReportDB.GetNumberOfFreeADSLPorts(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, MDF);                             
            
            return result;
        }

        #endregion Methods
    }
}
