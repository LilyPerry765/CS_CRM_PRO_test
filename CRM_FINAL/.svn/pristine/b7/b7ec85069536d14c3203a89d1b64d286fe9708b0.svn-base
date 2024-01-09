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
using CRM.Application.Reports.Viewer;
using System.Text.RegularExpressions;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for CenterCabinetInfoReportUserControl.xaml
    /// </summary>
    public partial class CenterCabinetInfoReportUserControl : Local.ReportBase
    {
        List<int> centerInfoList = new List<int>();
        public CenterCabinetInfoReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #region Load Methods
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAllCity();

        }
        public override void Search()
        {
            List<PCMDetails> pcmDetails = new List<PCMDetails>();
            List<CenterCabinetInfo> result = LoadData(ref pcmDetails);
           
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش اطلاعات فنی کافوهای مرکز کلی";

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();



            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("Details", "Details", pcmDetails);
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }
        public List<CenterCabinetInfo> LoadData(ref List<PCMDetails> pcmDetails)
        {
            List<CenterCabinetInfo> MainResult = new List<CenterCabinetInfo>();
            List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType));
            List<EnumItem> CabinetUsageType = Helper.GetEnumItem(typeof(DB.CabinetUsageType));
            List<EnumItem> BuchtStatus = Helper.GetEnumItem(typeof(DB.BuchtStatus));
            List<CenterCabinetInfo> result = new List<CenterCabinetInfo>();

            if (UserControlID == (int)DB.UserControlNames.TotalCenterCabinetInfo)
            {
           
               result = ReportDB.GetCabinetGenerallInfoReport(new List<int> { (int)CityComboBox.SelectedValue }, new List<int> { (int)CenterComboBox.SelectedValue }, string.IsNullOrEmpty(CabinetNumberTextBox.Text.Trim()) ? (string)null : CabinetNumberTextBox.Text.Trim());

                MainResult = result;
            }
            else
            {

                pcmDetails.Clear();
                result = ReportDB.GetCenterCabinetInfo(new List<int> { (int)CityComboBox.SelectedValue },  new List<int> { (int)CenterComboBox.SelectedValue }
                                                                             , string.IsNullOrEmpty(CabinetNumberTextBox.Text.Trim()) ? (string)null : CabinetNumberTextBox.Text.Trim()
                                                                             ,(bool)JustHeaderBuchtCheckBox.IsChecked);

                MainResult = result;
                //List<int> CabinetIDs = result.Where(t => (t.BuchtType != (int)DB.BuchtType.CustomerSide) && (t.BuchtType != (int)DB.BuchtType.PrivateWire)).Select(t => t.ID).ToList();

                //List<long?> CabinetInputIDs = result.Select(x => x.CabinetInputID).Distinct().ToList();
                //List<long?> DetailsResult = result.Where(t => (t.BuchtStatus == ((byte)DB.BuchtStatus.AllocatedToInlinePCM) &&
                //                                              (t.BuchtType == (int)DB.BuchtType.OutLine)) ||
                //                                              (t.BuchtType == (int)DB.BuchtType.OutLine)).Select(t => t.CabinetInputID).Distinct().ToList();

                //if (DetailsResult.Count > 0)
                //    ReportDB.GetPCMDetails(DetailsResult, ref pcmDetails);


                //result = result.Where(t => (t.BuchtType != (int)DB.BuchtType.InLine && t.BuchtType != (int)DB.BuchtType.OutLine)).ToList();
                //foreach (CenterCabinetInfo item in result)
                //{
                //    CenterCabinetInfo RecordTemp = new CenterCabinetInfo();
                //    RecordTemp = item;

                //    if (item.BuchtType == (int)DB.BuchtType.CustomerSide)
                //    {

                //        string BuchtOutput = pcmDetails.Where(t => (t.CabinetInputID == item.CabinetInputID) && (t.BuchtTypeID == (int)DB.BuchtType.OutLine)).Select(t => t.Bucht).FirstOrDefault();
                //        List<PCMDetails> DetailsList = pcmDetails.Where(t => (t.CabinetInputID == item.CabinetInputID) && (t.BuchtTypeID == (int)DB.BuchtType.InLine)).ToList();

                //        foreach (PCMDetails itemDetails in DetailsList)
                //        {
                //            CenterCabinetInfo Record = new CenterCabinetInfo();
                //            Record.CabinetNumber = item.CabinetNumber;
                //            Record.CabinetTypeID = item.CabinetTypeID;
                //            Record.City = item.City;
                //            Record.Center = item.Center;
                //            Record.CabinetInputNumber = item.CabinetInputNumber;
                //            Record.PostNumber = item.PostNumber;
                //            Record.PostContactNumber = item.PostContactNumber;

                //            Record.PostCount = item.PostCount;
                //            Record.ReservedPostCount = item.ReservedPostCount;
                //            Record.CabinetInputCount = item.CabinetInputCount;

                //            Record.PCM = itemDetails.MUID;
                //            Record.MDFHorizintalID = item.MDFHorizintalID;

                //            //Record.Bucht = item.Bucht;
                //            Record.Status = item.Status;
                //            Record.BuchtType = item.BuchtType;
                //            Record.BuchtIDConnectedOtherBucht = item.BuchtIDConnectedOtherBucht;

                //            Record.Bucht = item.Bucht;
                //            Record.BuchtInput = itemDetails.Bucht;
                //            Record.BuchtOutput = BuchtOutput;

                //            Record.Address = itemDetails.Address;
                //            Record.CustomerID = itemDetails.CustomerID;
                //            Record.CustomerName = itemDetails.CustomerName;
                //            Record.TelephoneNo = itemDetails.TelNo;
                //            MainResult.Add(Record);

                //        }
                //        if (BuchtOutput == null && pcmDetails.Where(t => (t.CabinetInputID == item.CabinetInputID) && (t.BuchtTypeID == (int)DB.BuchtType.InLine)).Count() == 0)
                //            MainResult.Add(RecordTemp);

                    //}
                    //else
                    //{
                    //    MainResult.Add(RecordTemp);
                    //}

                //}
            }

            foreach (CenterCabinetInfo item in MainResult)
            {
                item.Status = string.IsNullOrEmpty(item.Status)?"": BuchtStatus.Find(i => i.ID.ToString() == item.Status).Name;
                item.BuchtTypeName =  string.IsNullOrEmpty(item.BuchtType.ToString())?"":  BuchtType.Find(t => t.ID == item.BuchtType).Name;
            }
            pcmDetails.Clear();
            return MainResult;
        }
        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)CityComboBox.SelectedValue);
            CenterComboBox.SelectedIndex = 0;


        }
        private void Center_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        #endregion Load Methods
       
    }
}
