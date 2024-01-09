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
    /// Interaction logic for CabinetCentersInfoUserControl.xaml
    /// </summary>
    public partial class CabinetCentersInfoUserControl : Local.ReportBase
    {
        //List<int> centerInfoList = new List<int>();
        public CabinetCentersInfoUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
                   
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetInputStatus));
            DirectionComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetInputDirection));
        }
        public override void Search()
        {
            List<PCMDetails> pcmDetails = new List<PCMDetails>();
            List<CabinetInputReport> result = LoadData(ref pcmDetails);
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش اطلاعات فنی مرکزی های کافو";


            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;


            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("Details", "Details", pcmDetails);

              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();


        }
        private List<CabinetInputReport> LoadData(ref List<PCMDetails> pcmDetails)
        {
            pcmDetails.Clear();
            List<CabinetInputReport> Temp = new List<CabinetInputReport>();
           // List<int> InfoList = CenterComboBox.SelectedIDs.Count == 0 ? centerInfoList : CenterComboBox.SelectedIDs;


            List<CabinetInputReport> result = ReportDB.GetCabinetInput(CenterComboBox.SelectedIDs,
                                                                       CabinetComboBox.SelectedIDs,
                                                                       StatusComboBox.SelectedIDs,
                                                                       DirectionComboBox.SelectedIDs);

            List<EnumItem> Direction = Helper.GetEnumItem(typeof(DB.CabinetInputDirection));
            List<EnumItem> Status = Helper.GetEnumItem(typeof(DB.CabinetInputStatus));


            //List<CabinetInputReport> TempResult = result.Where(t => t.BuchtStatus == (byte)DB.BuchtStatus.AllocatedToInlinePCM).ToList();
            List<long?> CabinetInputIDs = result.Select(x=>x.CabinetInputID).ToList();
            List<long?> DetailsResult = 
                result.Where(t => (t.BuchtStatus != ((byte)DB.BuchtStatus.AllocatedToInlinePCM) && CabinetInputIDs.Contains(t.CabinetInputID))).Select(t=>t.CabinetInputID).ToList();
            if (DetailsResult.Count > 0)
                ReportDB.GetPCMDetails(DetailsResult, ref pcmDetails);
           

            foreach (CabinetInputReport item in result)
            {
                item.Direction = string.IsNullOrEmpty(item.Direction) ? (string)null : Direction.Find(t => t.ID == int.Parse(item.Direction)).Name;
                item.Status = string.IsNullOrEmpty(item.boolStatus.ToString()) ? (string)null : Status.Find(t => t.ID == byte.Parse(Convert.ToInt16(item.boolStatus).ToString())).Name;
                item.InsertDate = string.IsNullOrEmpty(item.InsertDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(item.InsertDate), Helper.DateStringType.Short);
               // item.Bucht = string.IsNullOrEmpty(item.Bucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(item.Bucht));

            }
            //foreach (PCMDetails p in pcmDetails)
            //{
            //    p.BuchtType = (p.BuchtType == ((int)DB.BuchtType.InLine).ToString()) ? "ورودی" : "خروجی";
            //    p.Bucht = string.IsNullOrEmpty(p.Bucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(p.Bucht));
            //}




            return result;
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
            List<CenterInfo> Temp = new List<CenterInfo>();
            Temp = Data.CenterDB.GetCenterByCityId(city.ID);
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId(city.ID);
            //centerInfoList.Clear();
            //foreach(CenterInfo i in Temp)
            //{
            //    centerInfoList.Add(i.ID);
            //}
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterID(city.ID); 
        }
       

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs); 
        }
    }
}
