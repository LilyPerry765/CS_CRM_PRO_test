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
    /// Interaction logic for ADSLNumberOfDayeriDischargeReshargeReportUserControl.xaml
    /// </summary>
    public partial class ADSLNumberOfDayeriDischargeReshargeReportUserControl : Local.ReportBase
    {
       #region Properties

        #endregion

        #region Constructor

        public ADSLNumberOfDayeriDischargeReshargeReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            
        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
           List< ADSLChargedDischargedInfo> Result = LoadData();
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

            List<int> Temp = new List<int>();
            DateTime? _todate = null;
            if(toDate.SelectedDate!=null)
            {
                _todate = toDate.SelectedDate.Value.AddDays(1);
            }

            List<ADSLChargedDischargedInfo> NumberofChargedList= ReportDB.GetNumberofCharged(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs,fromDate.SelectedDate,_todate);
            List<ADSLChargedDischargedInfo> NumberofDisChargedList = ReportDB.GetNumberofDisCharged(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate.SelectedDate, _todate, Temp, Temp, Temp, Temp, Temp, Temp, Temp, Temp);
            List<ADSLChargedDischargedInfo> NumberOfRechargedList= ReportDB.GetNumberOfRecharged(CityCenterComboBox.CityComboBox.SelectedIDs,CityCenterComboBox.CenterComboBox.SelectedIDs,fromDate.SelectedDate,_todate);
            List<ADSLChargedDischargedInfo> PrePaidIncomList = ReportDB.GetPrePaidIncome(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate.SelectedDate, _todate);
            List<ADSLChargedDischargedInfo> PostPaidIncomList = ReportDB.GetPostPaidIncome(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate.SelectedDate, _todate);
            //List<ADSLChargedDischargedInfo> ExpiredList = ReportDB.GetNumberOfExpired(CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs, fromDate.SelectedDate, toDate.SelectedDate);

            foreach (ADSLChargedDischargedInfo Info in Result)
            {
                for (int i = 0; i <NumberofChargedList.Count; i++)
                {
                    if (Info.Center == NumberofChargedList[i].Center)
                        Info.NumberPrePaidOfCharged = NumberofChargedList[i].NumberPrePaidOfCharged;
                }

                for (int i = 0; i < NumberofDisChargedList.Count; i++)
                {
                    if (Info.Center == NumberofDisChargedList[i].Center)
                        Info.NumberOfDischarge = NumberofDisChargedList[i].NumberOfDischarge;
                }

                for (int i = 0; i < NumberOfRechargedList.Count; i++)
                {
                    if (Info.Center == NumberOfRechargedList[i].Center)
                        Info.NumberOfADSLChangeService = NumberOfRechargedList[i].NumberOfADSLChangeService;
                }

                for (int i = 0; i < PrePaidIncomList.Count; i++)
                {
                    if (Info.Center == PrePaidIncomList[i].Center)
                        Info.PrePaidIncome = PrePaidIncomList[i].PrePaidIncome;
                }

                for (int i = 0; i < PostPaidIncomList.Count; i++)
                {
                    if (Info.Center == PostPaidIncomList[i].Center)
                        Info.PostPaidIncome = PostPaidIncomList[i].PostPaidIncome;
                }
            }
            title = "تعداد تخلیه/دایری/تمدید ADSL";
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
            List<ADSLChargedDischargedInfo> result = ReportDB.GetADSLDyeriDischargeInfo(CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                        CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                        fromDate.SelectedDate,
                                                                                        ToDate);
            return result;
        }
        #endregion Methods
    }
}
