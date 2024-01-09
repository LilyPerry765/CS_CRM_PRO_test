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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ADSLReportUserControl.xaml
    /// </summary>
    public partial class ADSLReportUserControl :Local.ReportBase
    {
        #region Properties And Fields

        string path;

        #endregion Properties And Fields

        #region Constructor

        public ADSLReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            CustomerOwnerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
            ServiceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLServiceType));
            PapInfoIdComboBox.ItemsSource = DB.GetAllEntity<PAPInfo>();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLStatus));
            TariffIdComboBox.ItemsSource = ADSLServiceDB.GetADSLService();
        }

        #endregion Initializer

        #region Event Handlers

        private void ReportButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO:ADSLReportUserControl.ReportButton
        }

        #endregion Event Handlers

        #region Methods

        public override void Search()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            string title = string.Empty;
            
            List<ADSLInfo> result = CRM.Data.ReportDB.GetADSLInfo(FromDate.SelectedDate, toDate
                                                             , string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (int?)null : int.Parse(TelephoneNoTextBox.Text.Trim())
                                                             , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text
                                                             , string.IsNullOrEmpty(CustomerOwnerStatusComboBox.Text) ? (int?)null : int.Parse(CustomerOwnerStatusComboBox.SelectedValue.ToString())
                                                             , string.IsNullOrEmpty(TariffIdComboBox.Text) ? (int?)null : int.Parse(TariffIdComboBox.SelectedValue.ToString())
                                                             , string.IsNullOrEmpty(ServiceTypeComboBox.Text) ? (int?)null : int.Parse(ServiceTypeComboBox.SelectedValue.ToString())
                                                             , string.IsNullOrEmpty(StatusComboBox.Text) ? (int?)null : int.Parse(StatusComboBox.SelectedValue.ToString())
                                                             , string.IsNullOrEmpty(PapInfoIdComboBox.Text) ? (int?)null : int.Parse(PapInfoIdComboBox.SelectedValue.ToString()));

            List<EnumItem> serviceTypeList = Helper.GetEnumItem(typeof(DB.ServiceType));
            List<EnumItem> adslStatusList = Helper.GetEnumItem(typeof(DB.ADSLStatus));
            List<EnumItem> adslOwnerStatusList= Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));

            foreach (ADSLInfo AI in result)
            {
                if (AI.ServiceType != null)
                {
                    AI.ServiceType = serviceTypeList.Find(item => item.ID == byte.Parse(AI.ServiceType)).Name;
                }

                if (AI.CustomerOwnerStatus != null)
                {
                    AI.CustomerOwnerStatus = adslOwnerStatusList.Find(item => item.ID == byte.Parse(AI.CustomerOwnerStatus)).Name;
                }

                AI.Status = adslStatusList.Find(item => item.ID == byte.Parse(AI.Status)).Name;
            }

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path); 
            //stiReport.Load(@"D:\Develop\CRM\CRM.Application\Reports\ADSL_report.mrt");
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            title = "گزارش ADSL";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        #endregion Methods
     
    }
}
