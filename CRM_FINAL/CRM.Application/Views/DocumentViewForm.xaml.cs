using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for DocumentViewForm.xaml
    /// </summary>
    public partial class DocumentViewForm : Local.PopupWindow
    {
        #region Properties and Fields

        public byte[] FileBytes { get; set; }
        public string FileType { get; set; }

        #endregion

        #region Constructor

        public DocumentViewForm()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (FileBytes != null && !string.IsNullOrEmpty(this.FileType))
            {
                string fileType = this.FileType.ToLower();

                //اگر فایل عکس باشد در خود برنامه باز میشود
                if (fileType.Contains("png") || fileType.Contains("jpg") || fileType.Contains("jpeg") || fileType.Contains("bmp"))
                {
                    MemoryStream stream = new MemoryStream(FileBytes);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    ViewImage.Source = image;
                }
                else
                {
                    //هر فایلی غیر از نوع عکس با نرم افزار پیش فرض ویندوز باز میشود
                    string tempFilePah = string.Format("{0}.{1}", System.IO.Path.GetTempFileName(), fileType);
                    using (BinaryWriter writer = new BinaryWriter(File.Open(tempFilePah, FileMode.Create)))
                    {
                        writer.Write(FileBytes);
                    }
                    System.Diagnostics.Process.Start(tempFilePah);
                    this.Close();
                }

            }

            ResizeWindow();
        }

        #endregion

    }
}
