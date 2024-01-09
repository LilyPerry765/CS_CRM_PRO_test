using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for GetFileUserControl.xaml
    /// </summary>
    /// 

    public partial class GetFileUserControl : UserControl
    {

        public string AppendixFilePath { get; set; }
        public string GetFilePath { get; set; }
        public string DefaultFileName { get; set; }
        public Guid FileGuid { get; set; }

        public GetFileUserControl()
        {
            InitializeComponent();
        }
        private void AppendixFileHyperlink_Click(object sender, RoutedEventArgs e)
        {

        }


        private void AppendixFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                 AppendixFileLabel.Content = dlg.FileName;
                 AppendixFilePath = dlg.FileName;
            }
        }

        private void AppendixFileLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AppendixFilePath);
        }

        private void GetFileLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(GetFilePath);
        }

        private void GetFileButton_Click(object sender, RoutedEventArgs e)
        {

            if (FileGuid == Guid.Empty) { MessageBox.Show("برای این آیتم فایلی ذخیره نشده است."); return; }

            CRM.Data.FileInfo fileInfo = Data.DocumentsFileDB.GetDocumentsFileTable(FileGuid);

            if (fileInfo.Content == null) { MessageBox.Show("فایل پیدا نشد"); return; }
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = DefaultFileName +'.' + fileInfo.FileType;
            
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                File.WriteAllBytes(dlg.FileName, fileInfo.Content);
                GetFilePath = dlg.FileName;
                GetFileLabel.Content = dlg.FileName;
               
            }
        
        }
    }
}
