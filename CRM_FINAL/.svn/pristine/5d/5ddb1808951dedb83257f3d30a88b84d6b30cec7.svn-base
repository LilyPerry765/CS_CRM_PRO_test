using Microsoft.Win32;
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
using System.IO;
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for DocumentInputForm.xaml
    /// </summary>
    public partial class DocumentInputForm : Local.PopupWindow
    {

        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }
        public Data.RequestForm reqForm { get; set; }
        private long _id { get; set; }
        private byte _docType { get; set; }
        private int _isFrom { get; set; }
        public int _formID { get; set; }
        public long _requestID { get; set; }
        public DocumentInputForm()
        {
            InitializeComponent();
            //Load();
        }

        public void Load(byte[] FileBytes, string Extension,int fromID, long requestID)
        {
            DocumentInputForm window = new DocumentInputForm();
            
            if (_isFrom == 1)
            {
                window.SaveButton.Visibility = Visibility.Visible;
            }
            window.FileBytes = FileBytes;
            window.Extension = Extension;
            window._formID = fromID;
           window._requestID = requestID;
            window.ShowDialog();
            
           
        }

        public DocumentInputForm(int FormID, byte docType,int IsForm , long RequestID)
            : this()
        {
            DocumentInputForm window = new DocumentInputForm();
            _isFrom = IsForm;
            if (_isFrom == 1)
            {
                window.SaveButton.Visibility = Visibility.Visible;
            }
            window.ShowDialog();
            
            FileBytes = window.FileBytes;
            Extension = window.Extension;
            Load(FileBytes, Extension,FormID,RequestID);
        
            _formID = FormID;
            _requestID = RequestID;

        }


        private void FromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                FileBytes = File.ReadAllBytes(dlg.FileName);
                Extension = System.IO.Path.GetExtension(dlg.FileName);
            }
            if(_isFrom!=1)
            this.Close();
        }

        private void FromScanner_Click(object sender, RoutedEventArgs e)
        {

            Scanner oScanner = new Scanner();
            string extension;

            FileBytes = oScanner.ScannWithExtension(out extension);
            Extension = extension;
            this.Close();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
           using(MainDataContext context= new MainDataContext())
           {
               long sequensFileName = context.GetMaxFileName() ?? 0;
               sequensFileName++;

               Data.RequestForm reqForm = new Data.RequestForm();
               string name = sequensFileName.ToString() + Extension;
               reqForm.name = name;
               reqForm.FormID = _formID;
               reqForm.file_stream = FileBytes;
               reqForm.RequestID = _requestID;

               context.RequestForms.InsertOnSubmit(reqForm);
               context.SubmitChanges();

           }
           this.Close();
        }
    }
}
