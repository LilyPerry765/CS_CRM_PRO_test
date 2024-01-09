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
    /// Interaction logic for ADSLModemInformationReportUserControl.xaml
    /// </summary>
    public partial class ADSLModemInformationReportUserControl : Local.ReportBase
    {
        #region Constructor
        public ADSLModemInformationReportUserControl()
        {
            InitializeComponent();
             Initialize();
        }
        #endregion Consructor

        #region Initializer

        private void Initialize()
        {
            ServiceGroupComboBox.ItemsSource = ADSLServiceGroupDB.GetADSLServiceGroupCheckable();
            ModelComboBox.ItemsSource = ADSLModemDB.GetAllModemMOdelsCheckable();
            ModemStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLModemStatus));

        }

        #endregion Intitializer

        #region Methods

        public override void Search()
        {
            List<ADSLModemInformation> ResultADSLRequest = LoadDataADSLRequest();
            List<ADSLModemInformation> ResultADSLChangeService = LoadDataADSLChangeService();
            List<ADSLModemInformation> Result = new List<ADSLModemInformation>();

            Result = ResultADSLChangeService.Union(ResultADSLRequest).ToList();
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

            title = "اطلاعات مودم ADSL ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<ADSLModemInformation> LoadDataADSLRequest()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text) ? -1 : Convert.ToInt64(TelNoTextBox.Text);
            List<ADSLModemInformation> result = ReportDB.GetADSLRequestModemInformation(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                 CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                 ModemStatusComboBox.SelectedIDs,
                                                                                 ModelComboBox.SelectedIDs,
                                                                                 ServiceGroupComboBox.SelectedIDs,
                                                                                 PhysicalAddressTextBox.Text,
                                                                                 SerialNoText.Text,
                                                                                 TelNo,
                                                                                 FromDate.SelectedDate,
                                                                                 toDate,
                                                                                 FromPaymentDate.SelectedDate,
                                                                                 toPaymentDate);
           return result;
        }

        private List<ADSLModemInformation> LoadDataADSLChangeService()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }

            DateTime? toPaymentDate = null;
            if (ToPaymentDate.SelectedDate.HasValue)
            {
                toPaymentDate = ToPaymentDate.SelectedDate.Value.AddDays(1);
            }

            long TelNo = string.IsNullOrEmpty(TelNoTextBox.Text) ? -1 : Convert.ToInt64(TelNoTextBox.Text);
            List<ADSLModemInformation> result = ReportDB.GetADSLChangeServiceModemInformation(CityCenterComboBox.CityComboBox.SelectedIDs,
                                                                                 CityCenterComboBox.CenterComboBox.SelectedIDs,
                                                                                 ModemStatusComboBox.SelectedIDs,
                                                                                 ModelComboBox.SelectedIDs,
                                                                                 ServiceGroupComboBox.SelectedIDs,
                                                                                 PhysicalAddressTextBox.Text,
                                                                                 SerialNoText.Text,
                                                                                 TelNo,
                                                                                 FromDate.SelectedDate,
                                                                                 toDate,
                                                                                 FromPaymentDate.SelectedDate,
                                                                                 toPaymentDate);
            return result;
        }
        #endregion Methods
    }
}
