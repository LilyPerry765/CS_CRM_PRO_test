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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for LicenseUserControl.xaml
    /// </summary>
    public partial class LicenseUserControl : UserControl
    {

        #region Properties and constructor

        public string LisenseNumber
        {
            get { return LicenseNumber.Text; }
            set { LicenseNumber.Text = value; }
        }


        private object _lisenseFile;
        public object lisenseFile
        {
            get { return _lisenseFile; }
            set { _lisenseFile = value; }
        }

        public LicenseUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Event
        private void LicenseFile_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "All files (*.*)|*.*";

            if (dlg.ShowDialog() == true)
            {
                string PathFile = dlg.FileName;

                if (!string.IsNullOrEmpty(PathFile.Trim()))
                {
                    byte[] uploadFile = System.IO.File.ReadAllBytes(PathFile);
                    System.Data.Linq.Binary fileBinary = new System.Data.Linq.Binary(uploadFile);
                    lisenseFile = fileBinary;
                }
                else
                    lisenseFile = null;
            }

        }
        #endregion

    }
}
