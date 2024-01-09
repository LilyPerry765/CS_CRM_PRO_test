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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{

    public partial class RequestPaymentShowForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        #endregion

        #region Constructor

        public RequestPaymentShowForm()
        {
            InitializeComponent();
            Initialize();
        }

        public RequestPaymentShowForm(long id)
            : this()
        {
            _ID = id;
            ShowRecord();
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            BankColumn.ItemsSource = Data.BankDB.GetBanksCheckable();
            BaseCostColumn.ItemsSource = Data.BaseCostDB.GetBaseCostCheckable();
            PaymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentType));
            PaymentWayColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PaymentWay));
        }

        public void ShowRecord()
        {
            ItemsDataGrid.Items.Clear();
            //ItemsDataGrid.ItemsSource = RequestPaymentDB.getrequestpa(_ID);           
        }

        #endregion

        #region EventHandlers

        private void FileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
                Extension = System.IO.Path.GetExtension(dlg.FileName);
            }

            if (FileBytes != null && Extension != string.Empty)
                _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        }

        private void ScannerLisBox_Selected(object sender, RoutedEventArgs e)
        {

            Scanner oScanner = new Scanner();
            string extension;

            FileBytes = oScanner.ScannWithExtension(out extension);
            Extension = extension;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (FileBytes != null && Extension != string.Empty)
                {
                    FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
                    FileBytes = fileInfo.Content;
                    CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
                    window.FileBytes = FileBytes;
                    window.FileType = fileInfo.FileType;
                    window.ShowDialog();
                }
                else
                    throw new Exception("فایل موجود نمی باشد !");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

        }

        #endregion
    }
}
