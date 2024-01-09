using System;
using System.Collections;
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
using System.Text.RegularExpressions;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PostInfoReportUserControl.xaml
    /// </summary>
    public partial class EquipmentBillingReportUserControl : Local.ReportBase
    {
        string FromDate = null;
        string ToDate = null;
        public EquipmentBillingReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PapCompanyComboBox.ItemsSource = PAPInfoDB.GetPapInfo();
            CycleIDComboBox.ItemsSource = CycleDB.GetCycle(Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Year));
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public override void Search()
        {
            if (PapCompanyComboBox.SelectedValue == null)

                MessageBox.Show("نام شرکت را انتخاب کنید");
            else
            {

               List<TinyBillingOptions> result = LoadData();
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();
                string title = string.Empty;
                string path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                string CenterIds = string.Empty;
                string RegionIds = string.Empty;
                List<Center> centerList = CenterDB.GetAllCenter();
                List<int> CenterIDs = CityCenterUC.CenterCheckableComboBox.SelectedIDs;
                List<int> CityIDs = CityCenterUC.CityComboBox.SelectedIDs;
                if (CenterIDs.Count != 0)
                {
                    foreach (int _centerIds in CenterIDs)
                    {
                        CenterIds += centerList.Find(item => item.ID == _centerIds).CenterName + " ,";
                    }
                    CenterIds = CenterIds.Substring(0, CenterIds.Length - 1);
                }


                List<City> Citylist = CityDB.GetAllCity();
                if (CityIDs.Count != 0)
                {
                    foreach (int _cityrIds in CityIDs)
                    {
                        RegionIds += Citylist.Find(item => item.ID == _cityrIds).Name + " ,";
                    }
                    RegionIds = RegionIds.Substring(0, RegionIds.Length - 1);
                }


                stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
                stiReport.Dictionary.Variables["CompanyName"].Value = PapCompanyComboBox.Text;
                stiReport.Dictionary.Variables["Region"].Value = RegionIds;
                

                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(DateTime.Parse(FromDate), Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(DateTime.Parse(ToDate), Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                stiReport.Dictionary.Variables["TaxPercent"].Value = Math.Round(double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(6, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value)).ToString();

                stiReport.Dictionary.Variables["Debt"].Value = string.IsNullOrEmpty(DebtAmount.Text) ? "0" : DebtAmount.Text;
                stiReport.Dictionary.Variables["Credit"].Value = string.IsNullOrEmpty(CreditAmount.Text) ? "0" : CreditAmount.Text;
                stiReport.Dictionary.Variables["Report_PaymentDate"].Value = Helper.GetPersianDate(DateDP.SelectedDate, Helper.DateStringType.Short).ToString();

                stiReport.CacheAllData = true;
                stiReport.RegData("result", "result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }
        public List<TinyBillingOptions> LoadData()
        {
            Dictionary<int, int> ADSLRequestDic = new Dictionary<int, int>();
            List<TinyBillingOptions> result = new List<TinyBillingOptions>();
            List<PapInfoReport> PapInfoResult = new List<PapInfoReport>();
            Cycle CycleMe = new Cycle();

            if (CycleIDComboBox.SelectedValue == null)
            {
                MessageBox.Show("لطفاَ بازه زمانی را مشخص کنید");
                return result;
            }
            else
            {
                CycleMe = CycleDB.GetCycleById((int)CycleIDComboBox.SelectedValue);
                FromDate = CycleMe.FromDate.Value.ToShortDateString();
                ToDate = CycleMe.ToDate.Value.ToShortDateString();
            }
            //Space&Power
            result = PAPInfoDB.GetTinyBillingOptionsReportGroupByCenter(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                                   FromDate, ToDate,
                                                                   (int)PapCompanyComboBox.SelectedValue);
            //Abonman
            PapInfoResult = PAPInfoDB.GetPAPInfoReport(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                                   DateTime.Parse(FromDate), DateTime.Parse(ToDate),
                                                                   (int)PapCompanyComboBox.SelectedValue);

            PAPInfoCostHistory CostHistoryTemp = PAPInfoCostDB.GetPAPInfoCostByIdDateTime(1, DateTime.Parse(FromDate), DateTime.Parse(ToDate));

            double AbonmanCostValue = (CostHistoryTemp != null) ? double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(1, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value) : 0;


            foreach (PapInfoReport item in PapInfoResult)
            {
                item.DuringDay = (item.EndDate - item.StartDate).Days + 1;
                item.Money = (long)Math.Round((AbonmanCostValue * item.DuringDay) / DB.GetDuration(DateTime.Parse(FromDate), DateTime.Parse(ToDate)));

            }
            PapInfoGroupByPapInfoID PapInfoGroupByPapInfoTemp = PAPInfoDB.GetPAPInfoGroupByPapInfoReport(PapInfoResult);
            //***************************
            ADSLRequestDic = PAPInfoDB.GetADSLPAPRequestGroupByPapInfo(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                                        DateTime.Parse(FromDate), DateTime.Parse(ToDate),
                                                                        (int)PapCompanyComboBox.SelectedValue);



            PAPInfo PapInfoTemp = PAPInfoDB.GetPAPInfoByID((int)PapCompanyComboBox.SelectedValue);

            List<TinyBillingOptions> _resultTemp = new List<TinyBillingOptions>();
            List<TinyBillingOptions> _result = new List<TinyBillingOptions>();
            List<Center> CenterTemp = CenterDB.GetAllCenter();

            TinyBillingOptions _record = new TinyBillingOptions();

            _resultTemp.Clear();


            _record.ACPowerCost = result.Where(t => t.CostID == 3).Sum(t => t.ACPowerCost).Value;
            _record.DCPowerCost = result.Where(t => t.CostID == 4).Sum(t => t.DCPowerCost).Value;
            _record.SpaceCost = result.Where(t => t.CostID == 2).Sum(t => t.SpaceCost).Value;
            _record.SpaceField = result.Where(t => t.CostID == 2).Sum(t => t.SpaceField).Value;
            _record.ACPowerField = result.Where(t => t.CostID == 3).Sum(t => t.ACPowerField).Value;
            _record.DCPowerFiels = result.Where(t => t.CostID == 4).Sum(t => t.DCPowerFiels).Value;

            
            _record.ADSLCustomerCount = (PapInfoGroupByPapInfoTemp == null) ? 0 : PapInfoGroupByPapInfoTemp.ADSLCUstomerCount;
            _record.ADSLCustomerCost = (PapInfoGroupByPapInfoTemp == null) ? 0 : PapInfoGroupByPapInfoTemp.ADSLCustomerCost;

            _record.CustomerAddress = PapInfoTemp.Address;
            _record.CustomerName = PapInfoTemp.Title;
            _record.CustomerAddress = PapInfoTemp.Address;
            _record.CycleName = CycleIDComboBox.Text;
            

            if (ADSLRequestDic.ContainsKey((int)PapCompanyComboBox.SelectedValue))
                _record.ADSLRequestCount = ADSLRequestDic[(int)PapCompanyComboBox.SelectedValue];
            double RequestCostValue = double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(5, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value);

            _record.ADSLRequestCost = RequestCostValue * _record.ADSLRequestCount;
            _record.TaxPercent = double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(6, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value);
            _resultTemp.Add(_record);
            return _resultTemp;
        }
    }
}
