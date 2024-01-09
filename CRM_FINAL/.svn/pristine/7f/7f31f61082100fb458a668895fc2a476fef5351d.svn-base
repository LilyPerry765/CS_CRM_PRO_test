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
using System.Windows.Forms;

namespace CRM.Application.Views
{
    public partial class SpaceAndPowerFullView : Local.PopupWindow
    {
        #region Properties

        private long _RequestID { get; set; }

        #endregion

        #region Constructors

        public SpaceAndPowerFullView()
        {
            InitializeComponent();
        }

        public SpaceAndPowerFullView(long requestID)
            : this()
        {
            _RequestID = requestID;
            LoadData();
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            SpaceAndPowerInfo spaceAndPowerInfo = Data.SpaceAndPowerDB.GetSpaceAndPowerInfoByID(_RequestID);
            SpaceAnfPowerInfo.DataContext = spaceAndPowerInfo;

            RequestInfo requestInfo = Data.RequestDB.GetRequestInfoByID(_RequestID);
            RequestInfo.DataContext = requestInfo;

            ResizeWindow();
        }

        #endregion

        #region Event Handlers

        //TODO:rad
        private void PreviewFileButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button originalSource = e.OriginalSource as System.Windows.Controls.Button;
            if (originalSource != null && originalSource.Tag != null)
            {
                var enteghalFileID = originalSource.Tag;
                if (enteghalFileID is Guid)
                {
                    FileInfo currentFileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)enteghalFileID);
                    if ((currentFileInfo.Content != null && currentFileInfo.Content.Length > 0) && !string.IsNullOrEmpty(currentFileInfo.FileType))
                    {
                        try
                        {
                            string temFilePath = string.Format("{0}{1}.{2}", System.IO.Path.GetTempPath(), "sample", currentFileInfo.FileType);
                            System.IO.File.WriteAllBytes(temFilePath, currentFileInfo.Content);
                            System.Diagnostics.Process.Start(temFilePath);
                        }
                        catch (Exception ex)
                        {
                            Enterprise.Logger.Write(ex, "خطا در باز کردن فایل طرح - روال فضا و  پاور");
                            System.Windows.MessageBox.Show("خطا در بازکردن فایل طرح", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        #endregion

    }
}
