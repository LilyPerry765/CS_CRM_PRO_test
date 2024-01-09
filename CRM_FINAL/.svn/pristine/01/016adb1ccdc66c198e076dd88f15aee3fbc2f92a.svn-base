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
    /// Interaction logic for ADSLRequestRpt.xaml
    /// </summary>
    public partial class ADSLRequestRpt : Local.TabWindow
    {
        public ADSLRequestRpt()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = RequestDB.GetADSLRequestCheckable();
            List<Region> lstRegion = Data.RegionDB.GetRegions();
            RegionIDComboBox.ItemsSource = lstRegion;
            RegionIDComboBox.SelectedValuePath = "ID";
            RegionIDComboBox.DisplayMemberPath = "Title";
            
        }

        private void RegionIDComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<Center> lstCenter = Data.CenterDB.GetCenterByRegionId(int.Parse(RegionIDComboBox.SelectedValue.ToString()));
            CenterIdComboBox.ItemsSource = lstCenter;
            CenterIdComboBox.SelectedValuePath = "ID";
            CenterIdComboBox.DisplayMemberPath = "CenterName";
        }

        private void btnClick_Click(object sender, RoutedEventArgs e)
        {
            
            List<ADSLRequestInfo> Result = LoadData();
            
            

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();



            stiReport.Load(@"//CRM.Application//Reports//ADSLRequest_report.mrt");
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["TelephoneNO"].Value = txtTelephoneNO.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (RegionIDComboBox.Text != string.Empty)
            
                stiReport.Dictionary.Variables["Region"].Value = RegionIDComboBox.Text;
            if (CenterIdComboBox.Text != string.Empty)
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            
            string _Title = string.Empty;
            _Title = "ADSL گزارش درخواستهای ";
            stiReport.Dictionary.Variables["Header"].Value = _Title;
            
            // = report.GetComponents()["DataBand1"] as StiDataBand;
            //foreach (StiComponent comp in DataBand1.Components)
            //{
            //    if (comp.Enabled) width += comp.Width;
            //} 

            stiReport.RegData("Result", "Result", Result);
            stiReport.Show();


        }

        private List<ADSLRequestInfo> LoadData()
        {
            List<ADSLRequestInfo> result =
                     ReportDB.GetADSLRequests(FromDate.SelectedDate,
                                                     ToDate.SelectedDate,
                                                     string.IsNullOrEmpty(txtTelephoneNO.Text.Trim()) ? (int?)null : int.Parse(txtTelephoneNO.Text),
                                                     RequestTypeComboBox.SelectedIDs,
                                                     string.IsNullOrEmpty(txtRequestID.Text.Trim()) ? (int?)null : int.Parse(txtRequestID.Text),
                                                     string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString()),
                                                     txtIdentificationId.Text);



            List<ADSLRequestInfo> lstresult = FillField(result);
            if (!string.IsNullOrEmpty(txtTelephoneNO.Text.Trim()))
            {
                List<ADSLRequestInfo> lstFiltered = FilterByNumber(lstresult, txtTelephoneNO.Text.Trim());
                List<EnumItem> lstS = Helper.GetEnumItem(typeof(DB.ADSLStatus));

                foreach (ADSLRequestInfo i in lstFiltered)
                {
                    i.RequestDate = Helper.GetPersianDate(DateTime.Parse(i.RequestDate), Helper.DateStringType.Short);
                    //i.Status = lstS.Find(item => item.ID == byte.Parse(i.Status)).Name;
                }
                return lstFiltered;
            }
            return lstresult;
            
        }

        private static List<ADSLRequestInfo> FilterByNumber(List<ADSLRequestInfo> lstresult, string TelephoneNo)
        {
            return lstresult.Where(t => (TelephoneNo == null ||  t.TelephoneNoADSL == TelephoneNo)).ToList();
        }


        private static List<ADSLRequestInfo> FillField(List<ADSLRequestInfo> result)
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

            foreach (ADSLRequestInfo ARI in result)
            {
                
                ARI.Status = status.Find(item => item.ID == int.Parse(ARI.Status)).Title;
                ARI.RequestDate = string.IsNullOrEmpty(ARI.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(ARI.RequestDate), Helper.DateStringType.Short); 

                switch (ARI.RequestTypeID)
                {
                    case (int)Data.DB.RequestType.ADSL:
                        {
                            ARI.TelephoneNoADSL = lstAdslRequest.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                           
                            break;
                        }

                    case (int)Data.DB.RequestType.ChangePortADSL:
                        {
                            ARI.TelephoneNoADSL = lstADSLChangePort.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangeServiceType:
                        {
                            ARI.TelephoneNoADSL = lstADSLChangeService.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLChangeTariff:
                        {
                            ARI.TelephoneNoADSL = lstADSLChangeTariff.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();  
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLInstalPAPCompany:
                        {
                            ARI.TelephoneNoADSL = lstADSLPAPRequest.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.ADSLCutTemporary:
                        {
                            ARI.TelephoneNoADSL = lstADSLCutTemporary.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }

                    case (int)Data.DB.RequestType.DischargeADSL:
                        {
                            ARI.TelephoneNoADSL = lstADSLDischarge.Find(item => item.ID == ARI.RequestNo).TelephoneNo.ToString();
                            break;
                        }
                }
            }
            return result;

        }
    }
}
