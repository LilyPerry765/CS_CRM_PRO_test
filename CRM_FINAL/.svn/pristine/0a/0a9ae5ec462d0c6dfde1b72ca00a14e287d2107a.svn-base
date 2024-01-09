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
    /// Interaction logic for ADSLRequestReportUserControl.xaml
    /// </summary>
    public partial class ADSLRequestReportUserControl : Local.ReportBase
    {
        #region Properties And Fields
        #endregion Properties And Fields

        #region Constructor
        public ADSLRequestReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = RequestDB.GetADSLRequestCheckable();
            RegionIdComboBox.ItemsSource = RegionDB.GetRegions();
        }
        #endregion  Initializer

        #region Event Handler

        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterIdComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)(sender as ComboBox).SelectedValue);
        }

        #endregion  Event Handler

        #region Methods

        public override void Search()
        {
            List<ADSLRequestInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (RegionIdComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            }
            if (CenterIdComboBox.Text != string.Empty)
            {
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            }

            title = "ADSL گزارش درخواستهای ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ADSLRequestInfo> LoadData()
        {
            DateTime? toDate = null;
            if (ToDate.SelectedDate.HasValue)
            {
                toDate = ToDate.SelectedDate.Value.AddDays(1);
            }
            List<ADSLRequestInfo> result =
                     ReportDB.GetADSLRequests(FromDate.SelectedDate,
                                              toDate,
                                              string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (int?)null : int.Parse(PhoneNoTextBox.Text),
                                              RequestTypeComboBox.SelectedIDs,
                                              string.IsNullOrEmpty(RequestIdTextBox.Text.Trim()) ? (int?)null : int.Parse(RequestIdTextBox.Text),
                                              string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString()),
                                              NationalIdTextBox.Text);

            // List<ADSLRequestInfo> resultList = FillField(result);
            if (!string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()))
            {
                //List<ADSLRequestInfo> FilteredList = FilterByNumber(resultList,PhoneNoTextBox.Text.Trim());
                List<EnumItem> lstS = Helper.GetEnumItem(typeof(DB.ADSLStatus));
                foreach (ADSLRequestInfo requestInfo in result)
                {
                    requestInfo.RequestDate = Helper.GetPersianDate(DateTime.Parse(requestInfo.RequestDate), Helper.DateStringType.Short);
                }
                return result;
            }

            return result;
        }

        private static List<ADSLRequestInfo> FilterByNumber(List<ADSLRequestInfo> resultList, string phoneNo)
        {
            return resultList.Where(t => (phoneNo == null || t.TelephoneNo == phoneNo)).ToList();
        }


        private static List<ADSLRequestInfo> FillField(List<ADSLRequestInfo> result)
        {
            List<EnumItem> adslStatusList = Helper.GetEnumItem(typeof(DB.ADSLStatus));
            List<Status> statusList = StatusDB.GetStatus();
            List<ADSLRequest> adslRequestList = DB.GetAllEntity<ADSLRequest>().ToList();
            List<ADSLChangePort1> adslChangePortList = DB.GetAllEntity<ADSLChangePort1>().ToList();
            List<ADSLChangeService> adslChangeTariffList = DB.GetAllEntity<ADSLChangeService>().ToList();
            List<ADSLPAPRequest> adslPAPRequestList = DB.GetAllEntity<ADSLPAPRequest>().ToList();
            List<ADSLCutTemporary> adslCutTemporaryList = DB.GetAllEntity<ADSLCutTemporary>().ToList();
            List<ADSLDischarge> adslDischargeList = DB.GetAllEntity<ADSLDischarge>().ToList();

            foreach (ADSLRequestInfo ARI in result)
            {

                ARI.Status = statusList.Find(item => item.ID == int.Parse(ARI.Status)).Title;
                ARI.RequestDate = string.IsNullOrEmpty(ARI.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(ARI.RequestDate), Helper.DateStringType.Short);

                switch (ARI.RequestTypeID)
                {
                    case (int)Data.DB.RequestType.ADSL:
                        {
                            ARI.TelephoneNo = adslRequestList.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();

                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangePort:
                        {
                           // ARI.TelephoneNo = adslChangePortList.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangeService:
                        {
                            //ARI.TelephoneNoADSL = adslChangeTariffList.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLInstalPAPCompany:
                        {
                            ARI.TelephoneNo = adslPAPRequestList.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLCutTemporary:
                        {
                            ARI.TelephoneNo = adslCutTemporaryList.Find(item => item.ID == ARI.ID).Request.TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLDischarge:
                        {
                            //ARI.TelephoneNo = adslDischargeList.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }
                }
            }
            return result;

        }
        #endregion  Methods

    }
}
