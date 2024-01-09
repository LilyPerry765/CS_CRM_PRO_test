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
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PCMStatisticEquipmentReportUserControl.xaml
    /// </summary>
    public partial class PCMStatisticEquipmentReportUserControl : Local.ReportBase
    {
        private bool update;
        public bool Update
        {
            set
            {
                update = value;
                UpdateElemans();
            }
            get
            {
                return update;
            }
        }
        public PCMStatisticEquipmentReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PCMUC.PCMTypelbl.Visibility = System.Windows.Visibility.Collapsed;
            PCMUC.PCMTypeComboBox.Visibility = System.Windows.Visibility.Collapsed;
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityComboBox.SelectedIndex = 0;
            UpdateElemans();
        }
        private void UpdateElemans()
        {
            if (CenterComboBox.SelectedValue != null)
            PCMUC.Rock_Update((int)CenterComboBox.SelectedValue);
        }
        public  override void Search()
        {
            TechnicalInfoReport TechnicalRecord = new TechnicalInfoReport();
            OutputBuchtInfoReport OutputBuchtRecord = new OutputBuchtInfoReport();

            List<PCMStatisticDetails> result = LoadData(ref TechnicalRecord, ref OutputBuchtRecord);

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);

            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);
            stiReport.RegData("TechnicalInfoReport", "TechnicalInfoReport", TechnicalRecord);
            stiReport.RegData("OutputBuchtInfoReport", "OutputBuchtInfoReport", OutputBuchtRecord);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private List<PCMStatisticDetails> LoadData(ref TechnicalInfoReport TechnicalRecord, ref OutputBuchtInfoReport OutputBuchtRecord)
        {
            //List<PCMCardInfo> PCMCardInfos = ReportDB.GetPCMStatisticEquipment((int?)CenterComboBox.SelectedValue,
            //                                                                    (int?)PCMUC.PCMBrandComboBox.SelectedValue,
            //                                                                    (int?)PCMUC.RockComboBox.SelectedValue,
            //                                                                    (int?)PCMUC.ShelfComboBox.SelectedValue,
            //                                                                    (int?)PCMUC.CardComboBox.SelectedValue);

            List<PCMStatisticDetails> result = ReportDB.GetPCMStatisticDetails( (int?)CityComboBox.SelectedValue,
                                                                                (int?)CenterComboBox.SelectedValue,
                                                                                (int?)PCMUC.PCMBrandComboBox.SelectedValue,
                                                                                (int?)PCMUC.RockComboBox.SelectedValue,
                                                                                (int?)PCMUC.ShelfComboBox.SelectedValue,
                                                                                (int?)PCMUC.CardComboBox.SelectedValue,
                                                                                new List<int> { },
                                                                                (int?)null);
            List<EnumItem> ps = Helper.GetEnumItem(typeof(DB.PCMStatus));
            foreach (PCMStatisticDetails item in result)
            {
                if (item.BuchtType == (int)DB.BuchtType.OutLine) // ورودی
                    item.InputBucht = DB.GetConnectionByBuchtID(item.BuchtID);
                else if (item.BuchtType == (int)DB.BuchtType.InLine) // خروجی
                {
                    string OutputBucht = DB.GetConnectionByBuchtID(item.BuchtID);

                    string ConnectionIDBucht = string.IsNullOrEmpty(item.ConnectionIDBucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(item.ConnectionIDBucht));
                    List<PCMStatisticDetails> Temp = result.Where(t => (t.RockID == item.RockID && t.ShelfID == item.ShelfID && t.CardID == item.CardID && t.BuchtType == (int)DB.BuchtType.OutLine)).ToList();
                    foreach (PCMStatisticDetails i in Temp)
                    {
                        i.OutputBucht = OutputBucht;
                        i.ConnectionIDBucht = ConnectionIDBucht;
                    }


                }
                item.PCMStatus = string.IsNullOrEmpty(item.PCMStatus) ? (string)null : ps.Find(t => t.ID == int.Parse(item.PCMStatus)).Name;

            }

            
            TechnicalRecord = result.Select(t => new TechnicalInfoReport
            {
                CabinetInputNumber = t.CabinetInputID,
                CabinetNumber = t.Cabinet,
                ConnectionNo = t.Connectionno,
                PostNumber = t.Postno
            }).FirstOrDefault();

            OutputBuchtRecord = result.Select(t => new OutputBuchtInfoReport
            {
                 EtesaliBucht = t.BuchtNo,
                 RadifBucht = t.Radif,
                 TabagheBucht = t.Tabaghe 
            }).FirstOrDefault();

            return result.Where(t => t.BuchtType == (int)DB.BuchtType.CustomerSide || t.BuchtType == (int)DB.BuchtType.OutLine).ToList(); 
        }

        

      
        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityComboBox.SelectedValue != null)
            {
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
                CenterComboBox.SelectedIndex = 0;
            }
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
                UpdateElemans();
        }
    }
}
