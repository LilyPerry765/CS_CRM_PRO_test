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
using System.Windows.Shapes;
using CRM.Data;
using Stimulsoft.Report;

namespace CRM.Application.Reports
{
    /// <summary>
    /// Interaction logic for ADSLReportDelay.xaml
    /// </summary>
    public partial class ADSLReportDelay : Local.TabWindow
    {
        public ADSLReportDelay()
        {
            InitializeComponent();
            Initialize();
        }
        public void Initialize()
        {
            List<EnumItem> AS = Helper.GetEnumItem(typeof(DB.RequestType));
            AS = ADSLRequest(AS);
            RequestTypeComboBox.ItemsSource = AS;
            RequestTypeComboBox.SelectedValuePath = "ID";
            RequestTypeComboBox.DisplayMemberPath = "Name";
            RequestTypeComboBox.SelectedIndex = 0;

            List<Region> lstRegion = Data.RegionDB.GetRegions();
            RegionIdcomboBox.ItemsSource = lstRegion;
            RegionIdcomboBox.SelectedValuePath = "ID";
            RegionIdcomboBox.DisplayMemberPath = "Title";
        }

        

        private void RegionIdcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Center> lstCenter = Data.CenterDB.GetCenterByRegionId(int.Parse(RegionIdcomboBox.SelectedValue.ToString()));
            CenterIdComboBox.ItemsSource = lstCenter;
            CenterIdComboBox.SelectedValuePath = "ID";
            CenterIdComboBox.DisplayMemberPath = "CenterName";
        }

        private void RequestTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<RequestStep> RS = RequestStepDB.GetRequestStepByRequestTypeID(int.Parse(RequestTypeComboBox.SelectedValue.ToString()));
            DelayStatusComboBox.ItemsSource = RS;
            DelayStatusComboBox.SelectedValuePath = "ID";
            DelayStatusComboBox.DisplayMemberPath = "StepTitle";
            DelayStatusComboBox.SelectedIndex = 0;


        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            List<RequestInfo> result = ReportDB.GetAdslDelay(FromDate.SelectedDate, ToDate.SelectedDate
                                                        , string.IsNullOrEmpty(txtTelephoneNo.Text) ? (string)null : txtTelephoneNo.Text.Trim()
                                                        , string.IsNullOrEmpty(RegionIdcomboBox.Text) ? (int?)null : int.Parse(RegionIdcomboBox.SelectedValue.ToString())
                                                        , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                        , string.IsNullOrEmpty(RequestTypeComboBox.Text) ? (int?)null : int.Parse(RequestTypeComboBox.SelectedValue.ToString())
                                                        , string.IsNullOrEmpty(DelayStatusComboBox.Text) ? (int?)null : int.Parse(DelayStatusComboBox.SelectedValue.ToString())
                                                        , string.IsNullOrEmpty(txtIdentificationId.Text.Trim()) ? (string)null : txtIdentificationId.Text);

            List<EnumItem> PaymentType = Helper.GetEnumItem(typeof(DB.PaymentType));
            FillTelephoneNumber(result);
            foreach(RequestInfo RI in result)
            {
                RI.RequestPaymentTypeName = PaymentType.Find(item => item.ID == RI.RequestPaymentTypeID).Name;
                RI.InsertDate = Helper.GetPersianDate(DateTime.Parse(RI.InsertDate), Helper.DateStringType.Short).ToString();
                RI.ModifyDate = Helper.GetPersianDate(DateTime.Parse(RI.ModifyDate), Helper.DateStringType.Short).ToString();
                RI.strRequestDate = Helper.GetPersianDate(RI.RequestDate, Helper.DateStringType.Short).ToString();
                RI.strRequestLetterDate = Helper.GetPersianDate(RI.RequestLetterDate, Helper.DateStringType.Short);
                RI.strRequestDate = Helper.GetPersianDate(RI.RequestDate, Helper.DateStringType.Short);
                
            }
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();



            stiReport.Load(@"D:\\Project\\CRM.Application\\Reports\\ADSLDelay_report.mrt");
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            string _Title = "ADSL گزارش تاخیر";
            stiReport.Dictionary.Variables["Header"].Value = _Title;


            stiReport.RegData("result", "result", result);
            stiReport.Show();
        }
        private List<EnumItem> ADSLRequest(List<EnumItem> AS)
        {
            return AS.Where(t => (t.ID == (byte)DB.RequestType.ADSL)
                             || (t.ID == (byte)DB.RequestType.ADSLChangeServiceType)
                             || (t.ID == (byte)DB.RequestType.ADSLChangeTariff)
                             || (t.ID == (byte)DB.RequestType.ADSLCutTemporary)
                             || (t.ID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                             || (t.ID == (byte)DB.RequestType.DischargeADSL)
                             || (t.ID == (byte)DB.RequestType.ChangePortADSL)
                             ).ToList();
        }
        private static List<RequestInfo> FillTelephoneNumber(List<RequestInfo> result)
        {
            List<EnumItem> lstS = Helper.GetEnumItem(typeof(DB.ADSLStatus));
            List<Status> status = StatusDB.GetStatus();
            List<ADSLRequest> lstAdslRequest = DB.GetAllEntity<ADSLRequest>().ToList();
            List<ADSLChangePort> lstADSLChangePort = DB.GetAllEntity<ADSLChangePort>().ToList();
            List<ADSLChangeServiceType> lstADSLChangeService = DB.GetAllEntity<ADSLChangeServiceType>().ToList();
            List<ADSLChangeTariff> lstADSLChangeTariff = DB.GetAllEntity<ADSLChangeTariff>().ToList();
            List<ADSLPAPRequest> lstADSLPAPRequest = DB.GetAllEntity<ADSLPAPRequest>().ToList();
            List<ADSLCutTemporary> lstADSLCutTemporary = DB.GetAllEntity<ADSLCutTemporary>().ToList();
            List<ADSLDischarge> lstADSLDischarge = DB.GetAllEntity<ADSLDischarge>().ToList();

            foreach (RequestInfo ARI in result)
            {


                switch (ARI.RequestTypeID)
                {
                    case (int)Data.DB.RequestType.ADSL:
                        {
                            ARI.TelephoneNo = lstAdslRequest.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();

                            break;
                        }

                    case (int)Data.DB.RequestType.ChangePortADSL:
                        {
                            ARI.TelephoneNo = lstADSLChangePort.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangeServiceType:
                        {
                            ARI.TelephoneNo = lstADSLChangeService.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangeTariff:
                        {
                            ARI.TelephoneNo = lstADSLChangeTariff.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLInstalPAPCompany:
                        {
                            ARI.TelephoneNo = lstADSLPAPRequest.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLCutTemporary:
                        {
                            ARI.TelephoneNo = lstADSLCutTemporary.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.DischargeADSL:
                        {
                            ARI.TelephoneNo = lstADSLDischarge.Find(item => item.ID == ARI.ID).TelephoneNo.ToString();
                            break;
                        }
                }
            }
            return result;

        }

        
    }
}
