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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for IssueWiringUserControl.xaml
    /// </summary>
    public partial class IssueWiringReportUserControl : Local.ReportBase
    {
         #region Properties And Fields
        #endregion Properties And Fields

       
         #region Constructor

        public IssueWiringReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            WiringTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WiringType));
            WiringTypeComboBox.SelectedIndex = 0;
            List<Region> lstRegion = Data.RegionDB.GetRegions();
            RegionIdComboBox.ItemsSource = lstRegion;
            RegionIdComboBox.SelectedValuePath = "ID";
            RegionIdComboBox.DisplayMemberPath = "Title";

        }

        #endregion Initializer

        #region Event Handlers

       
        #endregion  Event Handlers

        

        #region Mehtods
        public override void Search()
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);


            stiReport.Dictionary.Variables["data_a"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["data_b"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            
            if (RegionIdComboBox.Text != string.Empty)

                stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            if (CenterIdComboBox.Text != string.Empty)
                stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            string _title = string.Empty;




            _title = "گزارش فرمهای سیم بندی";
            stiReport.Dictionary.Variables["ci_report_header"].Value = _title;
            stiReport.CacheAllData = true;
            List<IssueWiringInfo> result_Open = new List<IssueWiringInfo>();
            List<ExchangeInfo> result_ExchangeInfo = new List<ExchangeInfo>();
            List<ExchangeMDFInfo> result_ExchangeMDFInfo = new List<ExchangeMDFInfo>();
            List<ExchangeCabinetInfo> result_ExchangeCabinetInfo = new List<ExchangeCabinetInfo>();
            List<DischargeInfo> result_DischargeInfo = new List<DischargeInfo>();
            List<WiringListInfo> Open = new List<WiringListInfo>();
           
            
           
           
            switch (int.Parse(WiringTypeComboBox.SelectedValue.ToString()))
            {
                case (int)DB.WiringType.Open://دایری
                    {
                        result_Open = LoadData_IssuewiringInfo();
                        Open = ReportDB.GetWiringDetails(FromDate.SelectedDate, ToDate.SelectedDate
                                                                             , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim()
                                                                             , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                             , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                             , string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(TelephoneNoTextBox.Text.Trim()));
                       
                        stiReport.RegData("result_Open", "result_open", result_Open);
                        stiReport.RegData("Details_Open", "Details_Open", Open);
                        stiReport.Dictionary.Variables["ReportType"].Value = "دایری";                
                        break;
                    }
                case (int)DB.WiringType.Reinstall://دایری مجدد
                    {
                        result_Open = LoadData_ReDayeriInfo();
                        Open = ReportDB.GetWiringDetails(FromDate.SelectedDate, ToDate.SelectedDate
                                                                             , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim()
                                                                             , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                             , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                             , string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(TelephoneNoTextBox.Text.Trim()));

                        stiReport.RegData("result_Open", "result_open", result_Open);
                        stiReport.RegData("Details_Open", "Details_Open", Open);
                        stiReport.Dictionary.Variables["ReportType"].Value = "دایری مجدد";
                        break;
                    }
                case (int)DB.WiringType.CableBack://کابل برگردان
                    {
                        result_ExchangeInfo = LoadData_ExchangeInfo();
                        result_ExchangeMDFInfo = LoadData_ExchangeMDFInfo();
                        result_ExchangeCabinetInfo = LoadData_ExchangeCabinetInfo();
                        List<ExchangeInfoDetails> EP = ReportDB.GetExchangeBackDetails(FromDate.SelectedDate, ToDate.SelectedDate
                                                                             , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim()
                                                                             , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                             , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                             , string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(TelephoneNoTextBox.Text.Trim()));


                        List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType)).ToList();
                        foreach (ExchangeInfoDetails i in EP)
                        {
                            i.OldBucht = string.IsNullOrEmpty(i.OldBucht)? (string)null : DB.GetConnectionByBuchtID(long.Parse( i.OldBucht));
                            i.NewBucht = string.IsNullOrEmpty(i.NewBucht) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.NewBucht));
                            i.OldBuchtType = string.IsNullOrEmpty(i.OldBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.OldBuchtType)).Name;
                            i.NewBuchtType = string.IsNullOrEmpty(i.NewBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.NewBuchtType)).Name;
                        }
                        stiReport.RegData("result_Exchange_Post", "result_Exchange_Post", result_ExchangeInfo);
                        stiReport.RegData("result_Exchange_MDF", "result_Exchange_MDF", result_ExchangeMDFInfo);
                        stiReport.RegData("result_Exchange_Cabinet", "result_Exchange_Cabinet", result_ExchangeCabinetInfo);
                        stiReport.RegData("Exchange_POst_Details", "Exchange_POst_Details", EP);
                        stiReport.Dictionary.Variables["ReportType"].Value = "کابل برگردان";
                        break;
                    }

                case (int)DB.WiringType.Discharge://تخلیه
                    {
                        List<DischargeInfo> result_DisCharge = new List<DischargeInfo>();
                        result_DisCharge = LoadData_DischargeInfo();

                        List<CheckableItem> EI = Data.CauseOfTakePossessionDB.GetCauseOfTakePossessionCheckableItem();
                        List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType)).ToList();
                        foreach (DischargeInfo i in result_DisCharge)
                        {
                            i.BuchtID = string.IsNullOrEmpty(i.BuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.BuchtID));
                            i.TakePossessionReason = string.IsNullOrEmpty(i.TakePossessionReason) ? (string)null : EI.Find(item => item.ID == byte.Parse(i.TakePossessionReason)).Name;

                        }
                        Open = ReportDB.GetWiringDetails(FromDate.SelectedDate, ToDate.SelectedDate
                                                                             , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim()
                                                                             , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                             , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                             , string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(TelephoneNoTextBox.Text.Trim()));

                        stiReport.RegData("result_Open", "result_open", result_Open);
                      
                        stiReport.RegData("result_Discharge", "result_Discharge", result_DisCharge);
                        stiReport.Dictionary.Variables["ReportType"].Value = "تخلیه";
                        break;
                    }

                case (int)DB.WiringType.ChangeLocation://تغییر مکان
                    {
                        List<ChangeLocationInfo> result_changeLocation = LoadaData_ChangeLocation();
                        stiReport.RegData("result_Open", "result_open", result_Open);
                        stiReport.RegData("result_ChangeLocation", "result_ChangeLocation", result_changeLocation);
                        stiReport.Dictionary.Variables["ReportType"].Value = "تغییر مکان";
                        break;
                    }
                case (int)DB.WiringType.ChangeNo://تغییر شماره
                    {
                        List<Counter> counter = CounterDB.GetAllCounter();
                        List<ChangeNoInfo> result_ChangeNo = LoadData_ChangeNO();
                        stiReport.RegData("result_Open", "result_open", result_Open);
                        stiReport.RegData("result_ChangeNo", "result_ChangeNo", result_ChangeNo);
                        stiReport.Dictionary.Variables["ReportType"].Value = "تغییر شماره";
                        break;
                    }
                case (int)DB.WiringType.RefundDeposit:
                    {
                        List<RefundDespositInfo> result_RefundDesposit = LoadData_RefundDesposit();
                        stiReport.RegData("result_RefundDesposit", "result_RefundDesposit", result_RefundDesposit);
                        stiReport.RegData("result_Open", "result_open", result_Open);
                        stiReport.Dictionary.Variables["ReportType"].Value = "استرداد ودیعه تلفن";
                        break;
                    }
            }

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<RefundDespositInfo> LoadData_RefundDesposit()
        {
            List<RefundDespositInfo> result_RefundDesposit = new List<RefundDespositInfo>();
            result_RefundDesposit = ReportDB.GetRefundDespositInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());

            List<Counter> lstCounter = CounterDB.GetAllCounter();


            foreach (RefundDespositInfo i in result_RefundDesposit)
            {

                i.MDFWiringDate = string.IsNullOrEmpty(i.MDFWiringDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.MDFWiringDate), Helper.DateStringType.Short);
                i.WiringDate = string.IsNullOrEmpty(i.WiringDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.WiringDate), Helper.DateStringType.Short);
                i.TelCounterNo = string.IsNullOrEmpty(i.TelCounterNo) ? (string)null : lstCounter.Find(item => item.TelephoneNo == long.Parse(i.TelCounterNo)).CounterNo;
               
                i.InsertDate = string.IsNullOrEmpty(i.InsertDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.InsertDate), Helper.DateStringType.Short);
                i.IsPrinted = (bool.Parse(i.IsPrinted)) ? "دارد" : "ندارد";
                i.ConfirmRecord = (bool.Parse(i.ConfirmRecord)) ? "تایید شده " : "تایید نشده";
                i.WiringIssueDate = string.IsNullOrEmpty(i.WiringIssueDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.WiringIssueDate), Helper.DateStringType.Short);
                i.BuchtID = string.IsNullOrEmpty(i.BuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.BuchtID));
            }
            return result_RefundDesposit;
        }

        private List<IssueWiringInfo> LoadData_ReDayeriInfo()
        {
            List<IssueWiringInfo> issueWiringInfo = new List<IssueWiringInfo>();

            List<IssueWiringRaw> issueWiringTemp = ReportDB.GetReDayeriInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());



            List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
            for (int i = 0; i < issueWiringTemp.Count; i++)
            {
                IssueWiringInfo Record = new IssueWiringInfo();
                Record.InsertDate = string.IsNullOrEmpty(issueWiringTemp[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].InsertDate, Helper.DateStringType.Short);
                Record.RequestID = issueWiringTemp[i].RequestID.ToString();
                Record.WiringIssueDate = string.IsNullOrEmpty(issueWiringTemp[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].WiringIssueDate, Helper.DateStringType.Short);
                Record.WiringNo = issueWiringTemp[i].WiringNo;
                Record.IsPrinted = (issueWiringTemp[i].IsPrinted) ? "دارد" : "ندارد";
                Record.PrintCount = issueWiringTemp[i].PrintCount.ToString();
                Record.WiringTypeID = wiringType.Find(item => item.ID == issueWiringTemp[i].WiringTypeID).Name;
                Record.CabinetNumber = issueWiringTemp[i].CabinetNumber.ToString();
                Record.ConnectionID = DB.GetConnectionByBuchtID(issueWiringTemp[i].ConnectionID);
                Record.ConnectionNo = issueWiringTemp[i].ConnectionNo.ToString();
                Record.InputNumber = issueWiringTemp[i].InputNumber.ToString();
                Record.Number = issueWiringTemp[i].Number.ToString();
                Record.TelephoneNo = issueWiringTemp[i].TelephoneNo.ToString();
                Record.Center = issueWiringTemp[i].Center;
                Record.Region = issueWiringTemp[i].Region;
                Record.CustomerName = issueWiringTemp[i].CustomerName;
                Record.SwitchPreNo = issueWiringTemp[i].SwitchPreNo.ToString();
                Record.SwitchCode = issueWiringTemp[i].SwitchCode.ToString();
                Record.PortNo = issueWiringTemp[i].PortNo;                
                Record.LastPrintDate = string.IsNullOrEmpty(issueWiringTemp[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].LastPrintDate, Helper.DateStringType.Short);
                issueWiringInfo.Add(Record);
            }
            return issueWiringInfo;    
        }

        private List<ChangeNoInfo> LoadData_ChangeNO()
        {
            List<ChangeNoInfo> result_ChangeNo = new List<ChangeNoInfo>();
            result_ChangeNo = ReportDB.GetChangeNoWiringInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());
            List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
            List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType)).ToList();
            List<Counter> lstCounter = CounterDB.GetAllCounter();
            List<CheckableItem> _changeNoReason = Data.CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
            List<SwitchPort> _switchPort = SwitchPortDB.GetSwitchPorts(); 
            
            foreach (ChangeNoInfo i in result_ChangeNo)
            {

                i.MDFWiringDate = string.IsNullOrEmpty(i.MDFWiringDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.MDFWiringDate), Helper.DateStringType.Short);
                i.OldTelCounterNo = string.IsNullOrEmpty(i.OldTelCounterNo) ? (string)null : lstCounter.Find(item => item.TelephoneNo == long.Parse(i.OldTelCounterNo)).CounterNo;
                i.NewTelCounterNo = string.IsNullOrEmpty(i.NewTelCounterNo) ? (string)null : lstCounter.Find(item => item.TelephoneNo == long.Parse(i.NewTelCounterNo)).CounterNo;
                i.InsertDate = string.IsNullOrEmpty(i.InsertDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.InsertDate), Helper.DateStringType.Short);
                i.IsPrinted = (bool.Parse(i.IsPrinted)) ? "دارد" : "ندارد";
                i.WiringIssueDate = string.IsNullOrEmpty(i.WiringIssueDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.WiringIssueDate), Helper.DateStringType.Short);
                i.OldBuchtID = string.IsNullOrEmpty(i.OldBuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.OldBuchtID));
                i.NewBuchtID = string.IsNullOrEmpty(i.NewBuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.NewBuchtID));
                i.OldBuchtType = string.IsNullOrEmpty(i.OldBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.OldBuchtType)).Name;
                i.NewBuchtType = string.IsNullOrEmpty(i.NewBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.NewBuchtType)).Name;
                if (i.NewSwitchPortID != null)
                {
                    SwitchPort sp = _switchPort.Find(item => item.ID == int.Parse(i.NewSwitchPortID));
                    i.NewPortNo = string.IsNullOrEmpty(sp.PortNo) ? (string)null : sp.PortNo;
                    i.NewMDFHorizentalID = string.IsNullOrEmpty(sp.MDFHorizentalID) ? (string)null : sp.MDFHorizentalID;
                }
                if (i.OldSwitchPortID != null)
                {
                    SwitchPort sp = _switchPort.Find(item => item.ID == int.Parse(i.OldSwitchPortID));
                    i.OldPortNo = string.IsNullOrEmpty(sp.PortNo) ? (string)null : sp.PortNo;
                    i.OldMDFHorizentalID = string.IsNullOrEmpty(sp.MDFHorizentalID) ? (string)null : sp.MDFHorizentalID;
                }

                i.ChangeReasonID = string.IsNullOrEmpty(i.ChangeReasonID) ? (string)null : _changeNoReason.Find(item => item.ID == int.Parse(i.ChangeReasonID)).Name;

            }


            return result_ChangeNo;
        }

        private List<ChangeLocationInfo> LoadaData_ChangeLocation()
        {
            List<ChangeLocationInfo> result_ChangeLocation = new List<ChangeLocationInfo>();
            result_ChangeLocation = ReportDB.GetChangeLocationInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());

            List<EnumItem> BuchtType = Helper.GetEnumItem(typeof(DB.BuchtType)).ToList();
            foreach (ChangeLocationInfo i in result_ChangeLocation)
            {
                i.WiringDate = string.IsNullOrEmpty(i.WiringDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.WiringDate), Helper.DateStringType.Short);
                i.MDFWiringDate = string.IsNullOrEmpty(i.MDFWiringDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.MDFWiringDate), Helper.DateStringType.Short);
                i.OldCustomerAddress = string.IsNullOrEmpty(i.OldCustomerAddress) ? (string)null : DB.SearchByPropertyName<Address>("ID",Convert.ToInt64(i.OldCustomerAddress)).SingleOrDefault().AddressContent;
                i.NewCustomerAddress = string.IsNullOrEmpty(i.NewCustomerAddress) ? (string)null : DB.SearchByPropertyName<Address>("ID", Convert.ToInt64(i.NewCustomerAddress)).SingleOrDefault().AddressContent;
                i.InsertDate = string.IsNullOrEmpty(i.InsertDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.InsertDate), Helper.DateStringType.Short);
                i.IsPrinted = (bool.Parse(i.IsPrinted)) ? "دارد" : "ندارد";
                i.WiringIssueDate = string.IsNullOrEmpty(i.WiringIssueDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(i.WiringIssueDate), Helper.DateStringType.Short);
                i.OldBuchtID = string.IsNullOrEmpty(i.OldBuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.OldBuchtID));
                i.NewBuchtID = string.IsNullOrEmpty(i.NewBuchtID) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(i.NewBuchtID));
                i.OldBuchtType = string.IsNullOrEmpty(i.OldBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.OldBuchtType)).Name;
                i.NewBuchtType = string.IsNullOrEmpty(i.NewBuchtType) ? (string)null : BuchtType.Find(item => item.ID == byte.Parse(i.NewBuchtType)).Name;

            }

            return result_ChangeLocation;
        }

        private List<DischargeInfo> LoadData_DischargeInfo()
        {
            List<DischargeRaw> DisChargeRaw = ReportDB.GetDischargeInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());
            List<DischargeInfo> DischargeInfo = new List<DischargeInfo>();
            for (int i = 0; i < DisChargeRaw.Count; i++)
            {
                DischargeInfo Record = new DischargeInfo();
                Record.InsertDate = string.IsNullOrEmpty(DisChargeRaw[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(DisChargeRaw[i].InsertDate, Helper.DateStringType.Short);
                Record.ID = DisChargeRaw[i].ID.ToString();
                Record.WiringIssueDate = string.IsNullOrEmpty(DisChargeRaw[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(DisChargeRaw[i].WiringIssueDate, Helper.DateStringType.Short);
                Record.WiringNo = DisChargeRaw[i].WiringNo;
                Record.IsPrinted = (DisChargeRaw[i].IsPrinted) ? "دارد" : "ندارد";
                Record.PrintCount = DisChargeRaw[i].PrintCount.ToString();




                Record.MDFWiringDate = string.IsNullOrEmpty(DisChargeRaw[i].MDFWiringDate.ToString()) ? (string)null : Helper.GetPersianDate(DisChargeRaw[i].MDFWiringDate, Helper.DateStringType.Short);
                Record.WiringDate = string.IsNullOrEmpty(DisChargeRaw[i].WiringDate.ToString()) ? (string)null : Helper.GetPersianDate(DisChargeRaw[i].WiringDate, Helper.DateStringType.Short);
                Record.MDFWiringHour = DisChargeRaw[i].MDFWiringHour;
                Record.WiringHour = DisChargeRaw[i].WiringHour;
                Record.TelephoneNo = DisChargeRaw[i].TelephoneNo.ToString();
                Record.Center = DisChargeRaw[i].Center;
                Record.Region = DisChargeRaw[i].Region;
                Record.CustomerName = DisChargeRaw[i].CustomerName;
                Record.MDFHorizentalID = string.IsNullOrEmpty(DisChargeRaw[i].MDFHorizentalID) ? (string)null : DisChargeRaw[i].MDFHorizentalID;
                Record.LastPrintDate = string.IsNullOrEmpty(DisChargeRaw[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(DisChargeRaw[i].LastPrintDate, Helper.DateStringType.Short);
                Record.BuchtID = DisChargeRaw[i].BuchtID.ToString();
                Record.TakePossessionReason = DisChargeRaw[i].TakePossessionReason.ToString();
                DischargeInfo.Add(Record);
            }
            return DischargeInfo;
        }

        private List<IssueWiringInfo> LoadData_IssuewiringInfo()
        {
            List<IssueWiringInfo> issueWiringInfo = new List<IssueWiringInfo>();
           
            List<IssueWiringRaw> issueWiringTemp = ReportDB.GetDayeriInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                            , FromDate.SelectedDate, ToDate.SelectedDate
                                                            , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                            , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                            , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());

                        

            List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
            for (int i = 0; i < issueWiringTemp.Count; i++)
            {
                IssueWiringInfo Record = new IssueWiringInfo();
                Record.InsertDate = string.IsNullOrEmpty(issueWiringTemp[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].InsertDate, Helper.DateStringType.Short);
                Record.RequestID = issueWiringTemp[i].RequestID.ToString();
                Record.WiringIssueDate = string.IsNullOrEmpty(issueWiringTemp[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].WiringIssueDate, Helper.DateStringType.Short);
                Record.WiringNo = issueWiringTemp[i].WiringNo;
                Record.IsPrinted = (issueWiringTemp[i].IsPrinted) ? "دارد" : "ندارد";
                Record.PrintCount = issueWiringTemp[i].PrintCount.ToString();
                Record.WiringTypeID = wiringType.Find(item => item.ID == issueWiringTemp[i].WiringTypeID).Name;
                Record.CabinetNumber = issueWiringTemp[i].CabinetNumber.ToString();
                Record.ConnectionID = DB.GetConnectionByBuchtID(issueWiringTemp[i].ConnectionID);
                Record.ConnectionNo = issueWiringTemp[i].ConnectionNo.ToString();





                Record.InputNumber = issueWiringTemp[i].InputNumber.ToString();
                Record.Number = issueWiringTemp[i].Number.ToString();
                Record.TelephoneNo = issueWiringTemp[i].TelephoneNo.ToString();
                Record.Center = issueWiringTemp[i].Center;
                Record.Region = issueWiringTemp[i].Region;
                Record.CustomerName = issueWiringTemp[i].CustomerName;
                Record.SwitchPreNo  = issueWiringTemp[i].SwitchPreNo.ToString();
                Record.SwitchCode = issueWiringTemp[i].SwitchCode.ToString();
                Record.PortNo = issueWiringTemp[i].PortNo;
                Record.LastPrintDate = string.IsNullOrEmpty(issueWiringTemp[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(issueWiringTemp[i].LastPrintDate, Helper.DateStringType.Short);
                issueWiringInfo.Add(Record);
            }
            return issueWiringInfo;
        }

        public List<ExchangeInfo> LoadData_ExchangeInfo()
        {

                    List<ExchangeRaw> RawTemp = ReportDB.GetExchangeInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                                        , FromDate.SelectedDate, ToDate.SelectedDate
                                                                        , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                        , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                                        , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                        , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());


                    List<ExchangeInfo> ListEX = new List<ExchangeInfo>();
                        List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
                        for (int i = 0; i < RawTemp.Count; i++)
                        {
                            ExchangeInfo Record = new ExchangeInfo();
                            Record.InsertDate = string.IsNullOrEmpty(RawTemp[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].InsertDate, Helper.DateStringType.Short);
                            Record.RequestID = RawTemp[i].ID.ToString();
                            Record.WiringIssueDate = string.IsNullOrEmpty(RawTemp[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].WiringIssueDate, Helper.DateStringType.Short);
                            Record.IsPrinted = (RawTemp[i].IsPrinted) ? "دارد" : "ندارد";
                            Record.PrintCount = RawTemp[i].PrintCount.ToString();


                            Record.OldCabinetID = RawTemp[i].OldCabinetID.ToString();
                            Record.NewCabinetID = RawTemp[i].NewCabinetID.ToString();
                            Record.OldPostID = RawTemp[i].OldPostID.ToString();
                            Record.TelephoneNo = RawTemp[i].TelephoneNo.ToString();

                            Record.NewPostID = RawTemp[i].NewPostID.ToString();
                            Record.AccomplishmentDate = string.IsNullOrEmpty(RawTemp[i].AccomplishmentDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].AccomplishmentDate, Helper.DateStringType.Short);
                            Record.WiringNo = RawTemp[i].WiringNo;
                            Record.WiringTypeID = wiringType.Find(item => item.ID == RawTemp[i].WiringTypeID).Name;
                            Record.Center = RawTemp[i].Center;
                            Record.Region = RawTemp[i].Region;
                            Record.CustomerName = RawTemp[i].CustomerName;
                            Record.LastPrintDate = string.IsNullOrEmpty(RawTemp[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].LastPrintDate, Helper.DateStringType.Short);
                            ListEX.Add(Record);
                        }
                        return ListEX; 
        }

        public List<ExchangeMDFInfo> LoadData_ExchangeMDFInfo()
        {

            List<ExchangeMDFRaw> RawTemp = ReportDB.GetExchangeMDFInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                                , FromDate.SelectedDate, ToDate.SelectedDate
                                                                , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                                , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());


            List<ExchangeMDFInfo> ListEX = new List<ExchangeMDFInfo>();
            List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
            for (int i = 0; i < RawTemp.Count; i++)
            {
                ExchangeMDFInfo Record = new ExchangeMDFInfo();
                Record.WiringNo = RawTemp[i].WiringNo;
                Record.WiringTypeID = wiringType.Find(item => item.ID == RawTemp[i].WiringTypeID).Name;
                Record.InsertDate = string.IsNullOrEmpty(RawTemp[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].InsertDate, Helper.DateStringType.Short);
                Record.RequestID = RawTemp[i].ID.ToString();
                Record.WiringIssueDate = string.IsNullOrEmpty(RawTemp[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].WiringIssueDate, Helper.DateStringType.Short);
                Record.IsPrinted = (RawTemp[i].IsPrinted) ? "دارد" : "ندارد";
                Record.PrintCount = RawTemp[i].PrintCount.ToString();
                Record.Center = RawTemp[i].Center;
                Record.Region = RawTemp[i].Region;
                Record.CustomerName = RawTemp[i].CustomerName;
                Record.LastPrintDate = string.IsNullOrEmpty(RawTemp[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].LastPrintDate, Helper.DateStringType.Short);

                Record.CabinetNumberF = RawTemp[i].CabinetNumberF.ToString();
                Record.CabinetNumberT = RawTemp[i].CabinetNumberT.ToString();
                Record.FirstNewBuchtID = RawTemp[i].FirstNewBuchtID.ToString();
                Record.LastNewBuchtID = RawTemp[i].LastNewBuchtID.ToString();
                Record.AccomplishmentDate = string.IsNullOrEmpty(RawTemp[i].AccomplishmentDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].AccomplishmentDate, Helper.DateStringType.Short);
                Record.AccomplishmentTime = RawTemp[i].AccomplishmentTime;
                Record.InputNumberF = RawTemp[i].InputNumberF.ToString();
                Record.InputNumberT = RawTemp[i].InputNumberT.ToString();
                ListEX.Add(Record);
            }
            return ListEX;
        }
        public List<ExchangeCabinetInfo> LoadData_ExchangeCabinetInfo()
        {

            List<ExchangeCabinetRaw> RawTemp = ReportDB.GetExchangeCabinetInfo(string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim()
                                                                , FromDate.SelectedDate, ToDate.SelectedDate
                                                                , string.IsNullOrEmpty(CenterIdComboBox.Text) ? (int?)null : int.Parse(CenterIdComboBox.SelectedValue.ToString())
                                                                , string.IsNullOrEmpty(WiringTypeComboBox.Text) ? (int?)null : int.Parse(WiringTypeComboBox.SelectedValue.ToString())
                                                                , string.IsNullOrEmpty(txtRequestNoComboBox.Text.Trim()) ? (long?)null : long.Parse(txtRequestNoComboBox.Text.Trim())
                                                                , string.IsNullOrEmpty(NationalIDTextBox.Text.Trim()) ? (string)null : NationalIDTextBox.Text.Trim());


            List<ExchangeCabinetInfo> ListEX = new List<ExchangeCabinetInfo>();
            List<EnumItem> wiringType = Helper.GetEnumItem(typeof(DB.WiringType));
            for (int i = 0; i < RawTemp.Count; i++)
            {
                ExchangeCabinetInfo Record = new ExchangeCabinetInfo();
                Record.WiringNo = RawTemp[i].WiringNo;
                Record.WiringTypeID = wiringType.Find(item => item.ID == RawTemp[i].WiringTypeID).Name;
                Record.InsertDate = string.IsNullOrEmpty(RawTemp[i].InsertDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].InsertDate, Helper.DateStringType.Short);
                Record.ID = RawTemp[i].ID.ToString();
                Record.WiringIssueDate = string.IsNullOrEmpty(RawTemp[i].WiringIssueDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].WiringIssueDate, Helper.DateStringType.Short);
                Record.IsPrinted = (RawTemp[i].IsPrinted) ? "دارد" : "ندارد";
                Record.PrintCount = RawTemp[i].PrintCount.ToString();
                Record.Center = RawTemp[i].Center;
                Record.Region = RawTemp[i].Region;
                Record.CustomerName = RawTemp[i].CustomerName;
                Record.LastPrintDate = string.IsNullOrEmpty(RawTemp[i].LastPrintDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].LastPrintDate, Helper.DateStringType.Short);

                Record.OldCabinetNumber = RawTemp[i].OldCabinetNumber.ToString();
                Record.NewCabinetNumber = RawTemp[i].NewCabinetNumber.ToString();

                Record.NewFirstBuchtID = string.IsNullOrEmpty(RawTemp[i].NewFirstBuchtID.ToString()) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(RawTemp[i].NewFirstBuchtID.ToString()));
                Record.NewLastBuchtID = string.IsNullOrEmpty(RawTemp[i].NewLastBuchtID.ToString()) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(RawTemp[i].NewLastBuchtID.ToString())); 
                Record.OldFirstBuchtID = string.IsNullOrEmpty(RawTemp[i].OldFirstBuchtID.ToString()) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(RawTemp[i].OldFirstBuchtID.ToString()));
                Record.OldLastBuchtID = string.IsNullOrEmpty(RawTemp[i].OldLastBuchtID.ToString()) ? (string)null : DB.GetConnectionByBuchtID(long.Parse(RawTemp[i].OldLastBuchtID.ToString())); 

                Record.FromNewInputNumber = RawTemp[i].FromNewInputNumber.ToString();
                Record.FromOldInputNumber = RawTemp[i].FromOldInputNumber.ToString();
                Record.ToNewInputNumber = RawTemp[i].ToNewInputNumber.ToString();
                Record.ToOldInputNumber = RawTemp[i].ToOldInputNumber.ToString();

                Record.AccomplishmentDate = string.IsNullOrEmpty(RawTemp[i].AccomplishmentDate.ToString()) ? (string)null : Helper.GetPersianDate(RawTemp[i].AccomplishmentDate, Helper.DateStringType.Short);
                Record.AccomplishmentTime = RawTemp[i].AccomplishmentTime;

                ListEX.Add(Record);
            }
            return ListEX;
        }
        #endregion Methods

        private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterIdComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)((sender as ComboBox).SelectedValue));
        }
    }
}
