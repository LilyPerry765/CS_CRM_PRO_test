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
    public partial class ContractForm : Local.PopupWindow
    {
        
        private long  _id=0;
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
        //public static RoundListNo[] telList { get; set; }
        //public static RoundListNo[] AlltelList { get; set; } 
        public  List<RelatedContracts> relatedContracts { get; set; }
        public static RelatedContracts[] relatedContractlist { get; set; }
        private RoundListNo previousRoundNo { get; set; }

        public byte[] FileBytes { get; set; }

        public string Extension { get; set; }

        long _documentType = 0;

                                                               
        public ContractForm()
        {
            DataContext = this;
            InitializeComponent();
            Initialize();

          
            
        }

        public ContractForm(Request request , long documentType):this()
        {
           
            _customerID = request.CustomerID ?? -1;
            _documentType = documentType;

           
            _requestDate = request.RequestDate;
            _requestType = request.RequestTypeID;
            _requestID=request.ID;
            _centerID = request.CenterID;
  
           
        }

        public ContractForm(long contractID, long documentType): this()                     
        {
            contract = Data.ContractDB.GetContractsByID(contractID);          
            reqDoc = Data.RequestDocumnetDB.GetRequestDocumentByID(contract.RequestDocumentID);
            _request = Data.RequestDB.GetRequestByID((long)contract.RequestID);
            _documentType = documentType;

            _id = contractID;           
            _centerID = _request.CenterID;
            _requestID = _request.ID;
            _requestDate = _request.RequestDate;
            _requestType = _request.RequestTypeID;
            _customerID = _request.CustomerID ?? -1;

     
        }

        private void Initialize()
        {
            contract = new Contract();
            reqDoc = new RequestDocument(); 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            _personType = Data.CustomerDB.GetCustomerByID(_customerID).PersonType;

            relatedContracts = ContractDB.GetRelatedContracts().Where(t => t.requestDocument.CustomerID == _customerID).ToList();
            relatedContractlist = ContractDB.GetRelatedContracts().Where(t => t.requestDocument.CustomerID == _customerID).ToArray();


            var NeededDocs = DocumentRequestTypeDB.GetNeedDocumentsForRequest(_requestType, _requestDate, _personType);
            DocumentTypeIDcomboBox.ItemsSource = NeededDocs.Where(t => t.TypeID == (byte)DB.DocumentType.Contract).ToList();
            DocumentTypeIDcomboBox.SelectedValue = _documentType;

            if (_id != 0)
            {
                saveButton.Content = "بروز رسانی";
                Title = "بروز رسانی قرارداد ";

                if (contract.RelatedContractID != null)
                {
                    CRM.Application.UserControls.AutoComplete.AutoCompleteArgs relatedArg = new UserControls.AutoComplete.AutoCompleteArgs(contract.RelatedContractID.ToString());
                    relatedArg.DataSource = relatedContractlist.Where(r=>r.contract.RelatedContractID==contract.RelatedContractID).ToList();
                    this.RelatedContractComboBox_PatternChanged(null, relatedArg);
                    this.RelatedContractComboBox.SelectedValue = relatedArg.Pattern;
                    this.RelatedContractComboBox.Text = relatedArg.Pattern;
                    this.RelatedContractComboBox.ItemsSource = relatedArg.DataSource;                    
                    this.RelatedContractComboBox.SelectedIndex = 0;
                }
            
            }            
           
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            using (TransactionScope maints = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                Guid? oldGuid = new Guid();
                oldGuid = reqDoc.DocumentsFileID;

                if (!(FileBytes == null || Extension == string.Empty))
                {
                    reqDoc.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                }
            
                reqDoc.ValidToDate = contract.ContractEndDate;
                reqDoc.InsertDate = DB.GetServerDate();
                reqDoc.CustomerID = _customerID;

                //Collect Contract information
                if (CopyradioButton.IsSelected)
                {
                    contract.IsExtended = 1;
                }
                else
                {
                    contract.IsExtended = 0;
                }

                contract.RequestID = _requestID;

                reqDoc.Detach();
                DB.Save(reqDoc);

                //save referencedocument    
                ReferenceDocument refDoc = Data.ReferenceDocumentDB.GetReferenceDocumentByRequestDocumentIDByRequestID(reqDoc.ID, (long)contract.RequestID);
                if (refDoc == null)
                {
                    refDoc = new ReferenceDocument();
                    refDoc.RequestDocumentID = reqDoc.ID;
                    refDoc.RequestID = contract.RequestID;
                    refDoc.Detach();
                    DB.Save(refDoc);
                }

                if (RelatedContractComboBox.SelectedValue != null)
                    contract.RelatedContractID = (long)RelatedContractComboBox.SelectedValue;
                else
                    contract.RelatedContractID = null;

                //save contract
                contract.RequestDocumentID = reqDoc.ID;
                contract.Detach();
                DB.Save(contract);

                if (oldGuid != null && FileBytes != null)
                    DocumentsFileDB.DeleteDocumentsFileTable((Guid)oldGuid);

                maints.Complete();
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
            //DocumentsByCustomer docInfo = DocumentTypeIDcomboBox.SelectedItem as DocumentsByCustomer;
            //if (docInfo == null) return;
            //if (Convert.ToBoolean(docInfo.IsRelatedToRoundContract))
            //{
            //    RoundLabel.Visibility = Visibility.Visible;
            //    RoundTypeLabel.Visibility = Visibility.Visible;
            //    TelephoneNoComboBox.Visibility = Visibility.Visible;
            //    RoundTypecomboBox.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    TelephoneNoComboBox.Text = string.Empty;
            //    RoundLabel.Visibility = Visibility.Collapsed;
            //    RoundTypeLabel.Visibility = Visibility.Collapsed;
            //    TelephoneNoComboBox.Visibility = Visibility.Collapsed;
            //    RoundTypecomboBox.Visibility = Visibility.Collapsed;
            //    TelephoneNoComboBox.IsEnabled = false;
            //}

        }

   
        //protected void TelephoneNoComboBox_PatternChanged(object sender, CRM.Application.UserControls.AutoComplete.AutoCompleteArgs args)
        //{
         
        //    if (string.IsNullOrEmpty(args.Pattern))
        //        args.CancelBinding = true;
        //    else
        //    {               
        //        args.DataSource = ContractForm.GetTelNo(args.Pattern);
        //        TextSearch.SetTextPath(this.TelephoneNoComboBox, "TelephoneNo");
        //    }
        //}

        protected void RelatedContractComboBox_PatternChanged(object sender, CRM.Application.UserControls.AutoComplete.AutoCompleteArgs args)
        {

            if (string.IsNullOrEmpty(args.Pattern))
                args.CancelBinding = true;
            else
            {
                args.DataSource = ContractForm.GetContractNo(args.Pattern);
                TextSearch.SetTextPath(this.RelatedContractComboBox, "requestDocument.DocumentNo");
            }
        }

        //private static ObservableCollection<RoundListNo> GetTelNo(string Pattern)
        //{
        //    return new ObservableCollection<RoundListNo>
        //          (

        //          telList.Where((tel, match) => tel.TelephoneNo.ToString().ToLower().Contains(Pattern.ToLower()))

        //          );
        //}

        private static ObservableCollection<RelatedContracts> GetContractNo(string Pattern)
        {
                      
            return new ObservableCollection<RelatedContracts>(

                relatedContractlist.Where
                (
                        (con, match) => con.contract.ID.ToString().Contains(Pattern.ToLower())
                                        || con.requestDocument.DocumentNo.ToString().Contains(Pattern.ToLower())
                            
                )

                );
        }

        //private void RoundTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    EnumItem item = RoundTypecomboBox.SelectedItem as EnumItem;
        //    if (item.ID!=(byte)DB.RoundType.Express)
        //    telList = RoundListDB.GetRoundListByCenterID(_centerID, item.ID).Where(t => t.IsRound == true && t.IsSelectable==true && t.saleStatus==0 && t.telStatus==0).ToArray();
        //    else
        //    telList = RoundListDB.GetRoundListByCenterID(_centerID, null).Where(t => t.IsRound == false && t.telStatus==0).ToArray();
        //}

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
    }
}
