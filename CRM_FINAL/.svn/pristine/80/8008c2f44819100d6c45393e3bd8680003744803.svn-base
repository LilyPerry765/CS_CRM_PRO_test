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
using System.IO;
using Microsoft.Win32;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for DocumentTypeForm.xaml
    /// </summary>
    public partial class DocumentTypeForm : Local.PopupWindow
    {
        private int _id=54;
        private int _anounceId = 0;
        public Dictionary<string,byte> docType=EnumTypeNameHelper.DocumentTypeEnumValues;
        public Dictionary<string, int> docName = DB.GetAllEntity<DocumentType>().ToDictionary(d => d.DocumentName, d => d.ID);

        public Announce announcement { get; set; }

        public DocumentTypeForm docs { get; set; }

        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        public DocumentTypeForm()
        {
            InitializeComponent();                
         
        }

        public DocumentTypeForm(int docID): this()
           
        {
            _id = docID;            
        }

        private void Initialize()
        {
            announcement = new Announce();
            announcement.IssueDate = DB.GetServerDate();
            announcement.StartDate = DB.GetServerDate();
            announcement.EndDate = DB.GetServerDate().AddMonths(1);
            this.AnnounceGrid.DataContext = announcement;
            Title = "ثبت آیین نامه ";
        }

        private void Selectfile_Click(object sender, RoutedEventArgs e)
        {
            DocumentInputForm window = new DocumentInputForm();
            window.ShowDialog();

            FileBytes = window.FileBytes;
            Extension = window.Extension;
            if(FileBytes != null)
            PathTextBox.Text = "فایل دریافت شد.";

            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "All files (*.*)|*.*";
            //if (dlg.ShowDialog() == true)
            //{
            //    PathTextBox.Text = dlg.FileName;
            //}
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {

            using (TransactionScope parentTransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                announcement.Status = 1;
                if (FileBytes != null && Extension != string.Empty)
                {
                    announcement.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                }
                DB.Save(announcement);
                PathTextBox.Text = string.Empty;
                _anounceId = announcement.ID;
                Load();

                parentTransactionScope.Complete();
            }
            //byte[] uploadFile= System.IO.File.ReadAllBytes(PathTextBox.Text);
            //System.Data.Linq.Binary fileBinary = new System.Data.Linq.Binary(uploadFile);
            //announcement.ContentFile = fileBinary;
            //announcement.Status = 1;
            //DB.Save(announcement);
            //PathTextBox.Text=string.Empty;
            //_anounceId = announcement.ID;            
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            if (_anounceId == 0)
            {
                Initialize();
                Savebtn.Content = "ذخیره";                
            }
            else
            {
                Savebtn.Content = "بروز رسانی";
                announcement = DB.GetEntitybyID<Announce>(_anounceId);
                PathTextBox.Text = string.Empty;
            }
        }

        private void ItemInsert(object sender, RoutedEventArgs e)
        {

            DocumentForm frm = new DocumentForm(0, _anounceId);
            frm.ShowDialog();
            docsDataGrid.ItemsSource = DocumentRequestTypeDB.GetDocumentInfo().Where(a => a.announce.ID == _anounceId);                
            this.docsDataGrid.Items.Refresh();
                    
       }

        public void ItemEdit(object sender, RoutedEventArgs e)
        {
            if (docsDataGrid.SelectedIndex >= 0)
            {
                DocumentRequestTypeInfo docInfo = docsDataGrid.SelectedItem as DocumentRequestTypeInfo; 
              
                if (docInfo == null) return;

                _anounceId = docInfo.announce.ID;               
                DocumentForm window = new DocumentForm(docInfo.doc.ID, _anounceId);
                window.ShowDialog();
                docsDataGrid.ItemsSource = DocumentRequestTypeDB.GetDocumentInfo().Where(a => a.announce.ID == _anounceId);                
                this.docsDataGrid.Items.Refresh();

                   
            }
        }

        private void DocReqTypetab_Loaded(object sender, RoutedEventArgs e)
        {
            docsDataGrid.ItemsSource = DocumentRequestTypeDB.GetDocumentInfo().Where(a => a.announce.ID == _anounceId);                
        }
           
    
    }
        
}
