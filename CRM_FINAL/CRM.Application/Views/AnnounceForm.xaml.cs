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
using Microsoft.Win32;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class AnnounceForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        private int _anounceId = 0;
        public Dictionary<string, byte> docType = EnumTypeNameHelper.DocumentTypeEnumValues;
     //   public Dictionary<string, int> docName =  DB.GetAllEntity<DocumentType>().ToDictionary(d => d.DocumentName, d => d.ID);
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        #endregion

        #region Constructors

        public AnnounceForm()
        {
            InitializeComponent();

        }

        public AnnounceForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
        }

        private void LoadData()
        {
            Announce announce = new Announce();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                docsDataGrid.IsEnabled = false;
            }
            else
            {
                announce = Data.AnnounceDB.GetAnnounceById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = announce;
            LoadDocumentRequest();
        }

        private void LoadDocumentRequest()
        {
           // docsDataGrid.ItemsSource = DocumentRequestTypeDB.GetDocumentInfo().Where(a => a.announce.ID == _ID);
            docsDataGrid.ItemsSource = DocumentRequestTypeDB.GetDocumentInfoByAnnouncesID(_ID);
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Selectfile_Click(object sender, RoutedEventArgs e)
        {
            DocumentInputForm window = new DocumentInputForm();
            window.ShowDialog();

            FileBytes = window.FileBytes;
            Extension = window.Extension;
            if (FileBytes != null)
                PathTextBox.Text = "فایل دریافت شد.";

            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "All files (*.*)|*.*";
            
            //if (dlg.ShowDialog() == true)
            //    PathTextBox.Text = dlg.FileName;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {

                using (TransactionScope parentTransactionScope = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Announce announce = this.DataContext as Announce;

                    if (FileBytes != null && Extension != string.Empty)
                    {
                        announce.DocumentsFileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension); ;
                    }
                    announce.Detach();
                    Save(announce);
                    _ID = announce.ID;

                    docsDataGrid.IsEnabled = true;
                    parentTransactionScope.Complete();

                    ShowSuccessMessage("ذخیره آیین نامه با موفقیت انجام شد.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره آیین نامه", ex);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            DocumentForRequestForm window = new DocumentForRequestForm(0, _ID);
            window.ShowDialog();

            LoadDocumentRequest();
        }

        public void EditItem(object sender, RoutedEventArgs e)
        {
            if (docsDataGrid.SelectedIndex >= 0)
            {
                DocumentRequestTypeInfo docInfo = docsDataGrid.SelectedItem as DocumentRequestTypeInfo;

                if (docInfo == null) return;

                _anounceId = docInfo.announce.ID;

                DocumentForRequestForm window = new DocumentForRequestForm(docInfo.doc.ID, _ID);
                window.ShowDialog();

                LoadDocumentRequest();
            }
        }

        #endregion
    }
}
