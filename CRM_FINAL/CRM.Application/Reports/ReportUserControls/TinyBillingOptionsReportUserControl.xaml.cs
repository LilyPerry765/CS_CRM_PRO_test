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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PostInfoReportUserControl.xaml
    /// </summary>
    public partial class TinyBillingOptionsReportUserControl : Local.ReportBase
    {
        string FromDate = null;
        string ToDate = null;
        public TinyBillingOptionsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            PapCompanyComboBox.ItemsSource = PAPInfoDB.GetPapInfo();
            CycleIDComboBox.ItemsSource = CycleDB.GetCycle(Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Year));

        }

        public override void Search()
        {
            if (PapCompanyComboBox.SelectedValue == null)

                MessageBox.Show("نام شرکت را انتخاب کنید");
            else
            {

                IEnumerable result = LoadData();
                if (result != null)
                {
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

                    stiReport.Dictionary.Variables["CompanyName"].Value = PapCompanyComboBox.Text;
                    stiReport.Dictionary.Variables["CenterName"].Value = CenterIds;
                    stiReport.Dictionary.Variables["Region"].Value = RegionIds;
                    stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

                    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(DateTime.Parse(FromDate), Helper.DateStringType.Short).ToString();
                    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(DateTime.Parse(ToDate), Helper.DateStringType.Short).ToString();
                    stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
                    stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
                    stiReport.Dictionary.Variables["TaxPercent"].Value = Math.Round(double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(6, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value)).ToString();

                    stiReport.CacheAllData = true;
                    stiReport.RegData("result", "result", result);

                    ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                    reportViewerForm.ShowDialog();
                }
            }
        }
        public IEnumerable LoadData()
        {
            Dictionary<int, int> ADSLRequestDic = new Dictionary<int, int>();
            List<TinyBillingOptions> result = new List<TinyBillingOptions>();
            List<PapInfoReport> PapInfoResult = new List<PapInfoReport>();
            Cycle CycleMe = new Cycle();

            if ((CycleIDComboBox.SelectedValue == null) &&
                 (FromDateDP.SelectedDate == null || ToDateDP.SelectedDate == null))
            {
                MessageBox.Show("لطفاَ بازه زمانی را مشخص کنید");
                return result;
            }
            else
            {

                if (CycleIDComboBox.Text != null)
                {
                    CycleMe = CycleDB.GetCycleById((int)CycleIDComboBox.SelectedValue);
                    FromDate = CycleMe.FromDate.Value.ToShortDateString();
                    ToDate = CycleMe.ToDate.Value.ToShortDateString();
                }
                else
                {
                    FromDate = FromDateDP.SelectedDate.Value.ToShortDateString();
                    ToDate = ToDateDP.SelectedDate.Value.ToShortDateString();
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
                List<PapInfoGroupByCenterID> PapInfoGroupByCenterIDTemp = PAPInfoDB.GetPAPInfoGroupByCenterIDReport(PapInfoResult);

                ADSLRequestDic= PAPInfoDB.GetADSLPAPRequest(CityCenterUC.CenterCheckableComboBox.SelectedIDs,
                                                                       DateTime.Parse(FromDate), DateTime.Parse(ToDate),
                                                                       (int)PapCompanyComboBox.SelectedValue);


                List<int> CenteridTemp = result.Select(t => t.CenterID).Distinct().ToList();

                foreach (PapInfoReport item in PapInfoResult)
                {
                    CenteridTemp.Add(item.CenterID);
                }
                foreach (KeyValuePair<int, int> item in ADSLRequestDic)
                {
                    CenteridTemp.Add(item.Key);
                }

                List<TinyBillingOptions> _resultTemp = new List<TinyBillingOptions>();
                List<TinyBillingOptions> _result = new List<TinyBillingOptions>();
                List<Center> CenterTemp = CenterDB.GetAllCenter();
                foreach (int item in CenteridTemp.Distinct())
                {

                    TinyBillingOptions _record = new TinyBillingOptions();

                    _resultTemp.Clear();
                    _resultTemp = result.Where(t => t.CenterID == item).ToList();
                    if (_resultTemp.Count() > 0)
                    {
                        _record.CenterName = _resultTemp.First().CenterName;
                        _record.City = _resultTemp.First().City;
                        _record.ACPowerCost = _resultTemp.Where(t => t.CostID == 3).SingleOrDefault().ACPowerCost;
                        _record.DCPowerCost = _resultTemp.Where(t => t.CostID == 4).SingleOrDefault().DCPowerCost;
                        _record.SpaceCost = _resultTemp.Where(t => t.CostID == 2).SingleOrDefault().SpaceCost;
                        _record.SpaceField = _resultTemp.Where(t => t.CostID == 2).SingleOrDefault().SpaceField;
                        _record.ACPowerField = _resultTemp.Where(t => t.CostID == 3).SingleOrDefault().ACPowerField;
                        _record.DCPowerFiels = _resultTemp.Where(t => t.CostID == 4).SingleOrDefault().DCPowerFiels;
                        
                    }
                    else
                    {
                        _record.CenterName = CenterTemp.Find(t => t.ID == item).CenterName;
                        _record.City = CityDB.GetCityByCenterID(item).Name;

                    }
                    PapInfoGroupByCenterID Temp = PapInfoGroupByCenterIDTemp.Where(t => t.CenterID == item).SingleOrDefault();
                    _record.ADSLCustomerCount = (Temp == null) ? 0 : Temp.ADSLCUstomerCount;
                    _record.ADSLCustomerCost =  (Temp == null) ? 0: Temp.ADSLCustomerCost;
                    if (ADSLRequestDic.ContainsKey(item))
                        _record.ADSLRequestCount = ADSLRequestDic[item];

                    _record.TaxPercent = double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(6, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value);

                    _result.Add(_record);
                }

                
                     //PAPInfoCostHistory CostHistoryRequestCost = PAPInfoCostDB.GetPAPInfoCostByIdDateTime(5, DateTime.Parse(FromDate), DateTime.Parse(ToDate));
                double RequestCostValue = double.Parse(PAPInfoCostDB.GetPAPInfoCostByIdDateTime(5, DateTime.Parse(FromDate), DateTime.Parse(ToDate)).Value);
                     foreach (TinyBillingOptions item in _result)
                     {
                         //item.ADSLCustomerCost = AbonmanCostValue * item.ADSLCustomerCount;
                         item.ADSLRequestCost = RequestCostValue * item.ADSLRequestCount;
                     }
                return _result;
                }
                
            }
        }
    }

       