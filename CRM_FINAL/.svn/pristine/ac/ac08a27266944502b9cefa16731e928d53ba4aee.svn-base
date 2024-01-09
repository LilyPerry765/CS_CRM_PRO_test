using CRM.Data;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for TranslationPostInfo.xaml
    /// </summary>
    public partial class TranslationPostInfo : Local.UserControlBase
    {
        private long reqeustID = 0;

        TranslationPostDetails _translationPostDetails { get; set; }
        Request _reqeust { get; set; }

        public TranslationPostInfo()
        {
            InitializeComponent();
            Initialize();

        }
        public TranslationPostInfo(long requestID)
            : this()
        {
            this.reqeustID = requestID;
        }
        private void Initialize()
        {
            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (_IsLoaded)
                return;
            else
                _IsLoaded = true;

            if (reqeustID != 0)
            {

                _translationPostDetails = Data.TranslationPostDB.GetTranslationPostDetailsByID(reqeustID);
                _reqeust = Data.RequestDB.GetRequestByID(reqeustID);

                if (_translationPostDetails.OverallTransfer == true) AllTransferButton.IsSelected = true; else PartialTransferButtom.IsSelected = true;

                this.DataContext = _translationPostDetails;
            }


        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PartialTransferGrid != null)
            {
                if (AllTransferButton.IsSelected == true)
                {
                    PartialTransferGrid.Visibility = Visibility.Hidden;
                }
                else if (PartialTransferButtom.IsSelected == true)
                {

                    PartialTransferGrid.Visibility = Visibility.Visible;
                }
            }
        }

        
    }
}
