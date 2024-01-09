using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TranslationPCMToNormalMDFForm.xaml
    /// </summary>
    public partial class TranslationPCMToNormalMDFForm : Local.RequestFormBase
    {
        TranslationPCMToNormal _translationPCMToNormal { get; set; }
        Request _reqeust { get; set; }

        bool error = false;

        public TranslationPCMToNormalMDFReportInfo translationPCMToNormalMDFReportInfo = new TranslationPCMToNormalMDFReportInfo();

        private long _RequestID;
        public TranslationPCMToNormalMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        public TranslationPCMToNormalMDFForm(long reqeustID)
            : this()
        {
            this._RequestID = reqeustID;

        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            _translationPCMToNormal = Data.TranslationPCMToNormalDB.GetTranslationPCMToNormalByID(_RequestID);
            translationPCMToNormalMDFReportInfo.Type = TypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.TranslationPCMToNormalType), _translationPCMToNormal.Type);

            if (_translationPCMToNormal.MDFAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationPCMToNormal.MDFAccomplishmentDate = dateTime.Date;
                _translationPCMToNormal.MDFAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            _reqeust = Data.RequestDB.GetRequestByID(_RequestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;



            TelephoneTextBox.Text = _reqeust.TelephoneNo.ToString();

            

            AssignmentInfo assingmentInfo = DB.GetAllInformationByTelephoneNo((long)_reqeust.TelephoneNo);
            translationPCMToNormalMDFReportInfo.CustomerName = assingmentInfo.CustomerName.ToString();
            translationPCMToNormalMDFReportInfo.CustomerAddress = assingmentInfo.Address.ToString();
            translationPCMToNormalMDFReportInfo.Telephone = assingmentInfo.TelePhoneNo;
            translationPCMToNormalMDFReportInfo.PostalCode = assingmentInfo.PostallCode.ToString();

            try
            {
                if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToNormal)
                {
                    Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.OldPostContactID);
                    PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();


                    translationPCMToNormalMDFReportInfo.PCMBucht = PCMBuchtTextBox.Text = assingmentInfo.Connection;
                    translationPCMToNormalMDFReportInfo.MUD = MUDTextBox.Text = assingmentInfo.MUID;



                    bucht = Data.BuchtDB.GetBuchtByCabinetInputIDs(new List<long> { _translationPCMToNormal.CabinetInputID ?? -1 }).Take(1).SingleOrDefault();
                    if (bucht != null)
                    {
                        translationPCMToNormalMDFReportInfo.ToBucht = ToBuchtTextBox.Text = DB.GetConnectionByBuchtID(bucht.ID);
                    }

                    ToMUDLabel.Visibility = Visibility.Collapsed;
                    ToMUDTextBox.Visibility = Visibility.Collapsed;
                    ToPCMBuchtLabel.Visibility = Visibility.Collapsed;
                    ToPCMBuchtTextBox.Visibility = Visibility.Collapsed;

                    Bucht buchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                    translationPCMToNormalMDFReportInfo.Bucht = BuchtTextBox.Text = DB.GetConnectionByBuchtID(buchtConnectToInputCabinet.ID);




                }
                else if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.NormalToPCM)
                {

                    Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                    PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();

                    MUDLabel.Visibility = Visibility.Collapsed;
                    MUDTextBox.Visibility = Visibility.Collapsed;
                    PCMBuchtLabel.Visibility = Visibility.Collapsed;
                    PCMBuchtTextBox.Visibility = Visibility.Collapsed;

                    translationPCMToNormalMDFReportInfo.Bucht = BuchtTextBox.Text = assingmentInfo.Connection;


                    AssignmentInfo newAssingmentInfo = DB.GetAllInformationPostContactID((long)_translationPCMToNormal.NewPostContactID, (byte)DB.BuchtType.InLine);
                    translationPCMToNormalMDFReportInfo.ToPCMBucht = ToPCMBuchtTextBox.Text = newAssingmentInfo.Connection;
                    translationPCMToNormalMDFReportInfo.ToMUD = ToMUDTextBox.Text = newAssingmentInfo.MUID;


                    Bucht buchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                    translationPCMToNormalMDFReportInfo.ToBucht = ToBuchtTextBox.Text = DB.GetConnectionByBuchtID(buchtConnectToInputCabinet.ID);
                }
                if (_translationPCMToNormal.Type == (byte)DB.TranslationPCMToNormalType.PCMToPCM)
                {

                    Bucht bucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.OldPostContactID);
                    PCMPort pCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                    List<PCMPort> pCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(pCMPort.PCMID).ToList();
                    List<Bucht> buchtList = Data.BuchtDB.getBuchtByPCMPortID(pCMPortList.Select(t => t.ID).ToList()).ToList();


                    translationPCMToNormalMDFReportInfo.PCMBucht = PCMBuchtTextBox.Text = assingmentInfo.Connection;
                    translationPCMToNormalMDFReportInfo.MUD = MUDTextBox.Text = assingmentInfo.MUID;

                    Bucht newBucht = Data.BuchtDB.GetBuchtByConnectionID(_translationPCMToNormal.NewPostContactID);
                    PCMPort newPCMPort = Data.PCMPortDB.GetPCMPortByID((long)newBucht.PCMPortID);
                    List<PCMPort> newPCMPortList = Data.PCMPortDB.GetAllPCMPortByPCMID(newPCMPort.PCMID).ToList();
                    List<Bucht> newBuchtList = Data.BuchtDB.getBuchtByPCMPortID(newPCMPortList.Select(t => t.ID).ToList()).ToList();


                    AssignmentInfo newAssingmentInfo = DB.GetAllInformationPostContactID((long)_translationPCMToNormal.NewPostContactID, (byte)DB.BuchtType.InLine);
                    translationPCMToNormalMDFReportInfo.ToPCMBucht = ToPCMBuchtTextBox.Text = newAssingmentInfo.Connection;
                    translationPCMToNormalMDFReportInfo.ToMUD = ToMUDTextBox.Text = newAssingmentInfo.MUID;

                    Bucht buchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)buchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                    translationPCMToNormalMDFReportInfo.Bucht = BuchtTextBox.Text = DB.GetConnectionByBuchtID(buchtConnectToInputCabinet.ID);


                    ////////

                    Bucht newBuchtConnectToInputCabinet = Data.BuchtDB.GetBuchtByID((long)newBuchtList.Where(t => t.BuchtTypeID == (byte)DB.BuchtType.OutLine).SingleOrDefault().BuchtIDConnectedOtherBucht);
                    translationPCMToNormalMDFReportInfo.ToBucht = ToBuchtTextBox.Text = DB.GetConnectionByBuchtID(newBuchtConnectToInputCabinet.ID);


                }
            }
            catch(Exception ex)
            {
                error = true;
                Folder.MessageBox.ShowError("در بازیابی اطلاعات خطای رخ داد. لطفا با پشتیبان سیستم تماس بگیرید");
            }


           AccomplishmentGroupBox.DataContext = _translationPCMToNormal;
        }
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {

                if (error == true)
                    throw new Exception("اطلاعات پی سی ام دارای خطا می باشد");
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _translationPCMToNormal = AccomplishmentGroupBox.DataContext as TranslationPCMToNormal;
                    _translationPCMToNormal.Detach();
                    DB.Save(_translationPCMToNormal, false);

                    IsSaveSuccess = true;
                    ts2.Complete();
                }

            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Save();
                    this.RequestID = _reqeust.ID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsForwardSuccess;

        }

        public override bool Print()
        {
                        try
            {
            ObservableCollection<TranslationPCMToNormalMDFReportInfo> resulat = new ObservableCollection<TranslationPCMToNormalMDFReportInfo>(new List<TranslationPCMToNormalMDFReportInfo> { translationPCMToNormalMDFReportInfo });
            SendToPrintTranslationPCMToNormallMDFReport(resulat);
            }
                        catch
                        {
                            IsPrintSuccess = false;

                        }

                        return IsPrintSuccess;
        }



        private void SendToPrintTranslationPCMToNormallMDFReport(ObservableCollection<TranslationPCMToNormalMDFReportInfo> Result)
        {
            DateTime currentDateTime = DB.GetServerDate();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationPCMToNormallMDFReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Short);
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(currentDateTime, Helper.DateStringType.Time);
            stiReport.Dictionary.Variables["CityName"].Value = DB.PersianCity;

            stiReport.CacheAllData = true;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public override bool Deny()
        {

            try
            {
                base.RequestID = _reqeust.ID;
                if (_translationPCMToNormal.CompletionDate == null)
                {
                    IsRejectSuccess = true;
                }
                else
                {
                    IsRejectSuccess = false;
                    Folder.MessageBox.ShowWarning("بعد از تایید نهایی امکان رد درخواست نمی باشد.");
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }
    }
}
