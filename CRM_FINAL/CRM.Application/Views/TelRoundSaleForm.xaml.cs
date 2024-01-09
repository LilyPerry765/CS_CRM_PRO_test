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
using Microsoft.Win32;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ContractForm.xaml
    /// </summary>
    public partial class TelRoundSaleForm : Local.PopupWindow
    {

        private long _id = 0;
        private long _customerID { get; set; }
        private byte _personType { get; set; }
        private int _requestType { get; set; }
        private DateTime _requestDate { get; set; }
        private long _requestID { get; set; }
        private long _centerID { get; set; }
        public Request _request { get; set; }
        public DocumentRequestType contractType { get; set; }
        public Contract contract { get; set; }
        public RequestDocument reqDoc { get; set; }
        public List<RoundListNo> roundNo { get; set; }
        public static List<RoundListNo> telList = new List<RoundListNo>();

        public static List<RoundListNo> AlltelList { get; set; }
        public List<RelatedContracts> relatedContracts { get; set; }
        public static RelatedContracts[] relatedContractlist { get; set; }
        private TelRoundSale previousTelRoundSale { get; set; } 
        public List<RoundSaleInfo> _sales { get; set; }
        public RoundSaleInfo _sale { get; set; }
        public bool loadFlag { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        public TelRoundSaleForm()
        {
            InitializeComponent();
            contract = new Contract();
            reqDoc = new RequestDocument();
            //sale = new RoundSaleInfo();
            //saleInfo.DataContext = sale;
            loadFlag = false;

        }

        public TelRoundSaleForm(Customer customer)
            : this()
        {

            _customerID = customer.ID;
            _personType = customer.PersonType;
            AlltelList = RoundListDB.GetRoundListByCenterID(_centerID, null).Where(t => t.IsRound == true).ToList();
            Initialize();

        }

        public TelRoundSaleForm(Request request)
            : this()
        {

            _customerID = request.CustomerID ?? -1;
            _personType = Data.CustomerDB.GetCustomerByID(_customerID).PersonType;
            _requestDate = request.RequestDate;
            _requestType = request.RequestTypeID;
            _requestID = request.ID;
            _centerID = request.CenterID;
            _request = request;

            AlltelList = RoundListDB.GetRoundListByCenterID(_centerID, null).Where(t => t.IsRound == true).ToList();

            Initialize();

        }

        public TelRoundSaleForm(long contractID)
            : this()
        {
            contract = Data.ContractDB.GetContractsByID(contractID);
            reqDoc = Data.RequestDocumnetDB.GetRequestDocumentByID(contract.RequestDocumentID);
            _request = Data.RequestDB.GetRequestByID((long)contract.RequestID);
            _id = contractID;
            _centerID = _request.CenterID;
            _requestID = _request.ID;
            _requestDate = _request.RequestDate;
            _requestType = _request.RequestTypeID;
            _customerID = _request.CustomerID ?? -1;
            _personType = Data.CustomerDB.GetCustomerByID(_customerID).PersonType;
            AlltelList = RoundListDB.GetRoundListByCenterID(_centerID, null)
                                    .Where(t => t.IsRound == true)
                                    .ToList();

            relatedContracts = ContractDB.GetRelatedContracts().Where(t => t.requestDocument.CustomerID == _customerID).ToList();

            relatedContractlist = ContractDB.GetRelatedContracts().Where(t => t.requestDocument.CustomerID == _customerID).ToArray();

            telList = AlltelList.Where(t =>
                                                 _request.RequestDate >= t.StartDate &&
                                                 _request.RequestDate <= t.EndDate &&
                                                 t.saleStatus == 0 &&
                                                 t.IsSelectable == true
                                       )
                                .Union(RoundListDB.GetRoundListByCenterID(_centerID, null).Where(s => s.RoundID == contract.TelRoundSaleID).ToList()).ToList();

            Initialize();

        }

        private void Initialize()
        {
            var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_requestType, _requestDate, _personType);
            var list = NeededDocs.Where(t => t.TypeID == (byte)DB.DocumentType.Contract).ToList();
            DocumentTypeIDcomboBox.ItemsSource = list;
            DocumentTypeIDcomboBox.SelectedIndex = list.FindIndex(t => t.IsRelatedToRoundContract == true);
            RoundTypecomboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RoundType));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (_id != 0)
            {
                saveButton.Content = "بروز رسانی";
                Title = "بروز رسانی قرارداد ";

                if (contract.TelRoundSaleID != null)
                {
                    loadFlag = true;
                    previousTelRoundSale = Data.TelRoundSaleDB.GetTelRoundSaleByID((long)contract.TelRoundSaleID);

                    RoundTypecomboBox.SelectedValue = Data.RoundSaleInfoDB.GetRoundSaleInfoByID(previousTelRoundSale.RoundSaleInfoID).RoundType;
                    RoundTypecomboBox_SelectionChanged(null, null);

                    TelephoneNoComboBox.SelectedValue = previousTelRoundSale.TelephoneNo;
                    TelephoneNoComboBox_SelectionChanged(null, null);

                    DocumentDatePicker.SelectedDate = reqDoc.DocumentDate;
                    DocumentNoTextBox.Text = reqDoc.DocumentNo;
                    PriceTextBox.Text = contract.Price.ToString()??_sale.BasePrice.ToString();
                }

            }

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {

                    Guid? oldGuid = new Guid();
                    oldGuid = reqDoc.DocumentsFileID;

                    if (!(FileBytes == null || Extension == string.Empty))
                    {
                        reqDoc.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                    }

                    reqDoc.InsertDate = DB.GetServerDate();
                    reqDoc.CustomerID = _customerID;
                    reqDoc.DocumentDate = DocumentDatePicker.SelectedDate;
                    reqDoc.DocumentNo = DocumentNoTextBox.Text;
                    DocumentsByCustomer item = DocumentTypeIDcomboBox.SelectedItem as DocumentsByCustomer;

                    reqDoc.DocumentRequestTypeID = item.DocumentRequestTypeID;
                    TelRoundSale roundInfo = new TelRoundSale();
                    if (TelephoneNoComboBox.SelectedValue != null)
                    {
                        roundInfo = Data.TelRoundSaleDB.GetTelRoundSaleByTelephoneNo((long)this.TelephoneNoComboBox.SelectedValue);
                        contract.TelRoundSaleID = roundInfo.ID;
                    }
                    contract.RequestID = _requestID;

                    long price = 0;
                    if (long.TryParse(PriceTextBox.Text, out price))
                        contract.Price = price;
                    else
                        throw new Exception("خطا در ورود قیمت");

                    ContractDB.SaveRequestContract(reqDoc, contract, roundInfo, previousTelRoundSale);

                    if (oldGuid != null && FileBytes != null)
                        DocumentsFileDB.DeleteDocumentsFileTable((Guid)oldGuid);

                    ts.Complete();
                }
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات" ,ex);
            }
            this.Close();
        }

        private void selectFile_Click(object sender, RoutedEventArgs e)
        {
            DocumentInputForm window = new DocumentInputForm();
            window.ShowDialog();

            FileBytes = window.FileBytes;
            Extension = window.Extension;
            if (FileBytes != null && Extension != string.Empty)
                SavedValueLabel.Visibility = Visibility.Visible;

        }

        private void DocumentTypeIDcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DocumentsByCustomer docInfo = DocumentTypeIDcomboBox.SelectedItem as DocumentsByCustomer;
            if (docInfo == null) return;
            if (Convert.ToBoolean(docInfo.IsRelatedToRoundContract))
            {
                RoundLabel.Visibility = Visibility.Visible;
                RoundTypeLabel.Visibility = Visibility.Visible;
                TelephoneNoComboBox.Visibility = Visibility.Visible;
                RoundTypecomboBox.Visibility = Visibility.Visible;
            }
            else
            {
                TelephoneNoComboBox.Text = string.Empty;
                RoundLabel.Visibility = Visibility.Collapsed;
                RoundTypeLabel.Visibility = Visibility.Collapsed;
                TelephoneNoComboBox.Visibility = Visibility.Collapsed;
                RoundTypecomboBox.Visibility = Visibility.Collapsed;
                TelephoneNoComboBox.IsEnabled = false;
            }

        }

        private void RoundTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EnumItem item = RoundTypecomboBox.SelectedItem as EnumItem;
            if(_request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter)
                _sales = Data.RoundSaleInfoDB.GetRoundSaleInfoByRoundTypeID(item.ID, (int)Data.ChangeLocationDB.GetChangeLocationByRequestID(_request.ID).TargetCenter);
            else
                _sales = Data.RoundSaleInfoDB.GetRoundSaleInfoByRoundTypeID(item.ID , _request.CenterID);

            TelephoneNoComboBox.ItemsSource = Data.TelRoundSaleDB.GetTelRoundTelephoneByRoundSaleInfoID(_sales.Select(t => t.ID).ToList()).ToList();

            TelephoneNoComboBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            DocumentNoTextBox.Text = string.Empty;
            DocumentDatePicker.SelectedDate = null;

            // saleInfo.DataContext = sale;
            //if (sale != null && sale.IsAuction == false)
            //{
            //    PriceTextBox.Text = sale.BasePrice.ToString();
            //    PriceTextBox.IsEnabled = false;
            //}

            //else if (sale != null)
            //{
            //    PriceTextBox.Text = sale.BasePrice.ToString();
            //    PriceTextBox.IsEnabled = true;

            //    if (item.ID != (byte)DB.RoundType.Express)
            //        telList = RoundListDB.GetRoundTelInfo(_centerID, item.ID).ToList();
            //    else
            //        telList = RoundListDB.GetRoundListByCenterID(_centerID, null).Where(t => t.IsRound == false && t.telStatus == 0).ToList();

            //    if (contract != null && contract.ID != 0)
            //        telList = telList.Union(RoundListDB.GetRoundListByCenterID(_centerID, null).Where(s => s.RoundID == contract.TelRoundSaleID).ToList()).ToList();


            //}

            //this.TelephoneNoComboBox.ItemsSource = telList;
            //CRM.Application.UserControls.AutoComplete.AutoCompleteArgs rondArg = new UserControls.AutoComplete.AutoCompleteArgs(Data.TelRoundSaleDB.GetTelRoundSaleByID((long)contract.TelRoundSaleID).TelephoneNo.ToString());
            //rondArg.DataSource = telList;
            //this.TelephoneNoComboBox_PatternChanged(null, rondArg);
            //this.TelephoneNoComboBox.ItemsSource = rondArg.DataSource;                    
            //this.TelephoneNoComboBox.Text = rondArg.Pattern;                   
            //this.TelephoneNoComboBox.SelectedValue = (rondArg.DataSource).Cast<CRM.Data.RoundListNo>().FirstOrDefault().TelephoneNo;                   
            //this.TelephoneNoComboBox.SelectedIndex = 0;
            //loadFlag=false;
        }

        private void SavedValueLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (FileBytes != null)
            {
                CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                window.FileBytes = FileBytes;
                window.FileType = Extension;
                window.ShowDialog();
            }
            else
            {
                MessageBox.Show("فایل موجود نمیباشد.");
            }
        }

        private void TelephoneNoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneNoComboBox.SelectedValue != null)
            {

                _sale = Data.RoundSaleInfoDB.GetRoundSaleInfoByTelephoneNo((long)TelephoneNoComboBox.SelectedValue, _sales.Select(t => t.ID).ToList());
                saleInfo.DataContext = _sale;
            }
        }

    }
}
