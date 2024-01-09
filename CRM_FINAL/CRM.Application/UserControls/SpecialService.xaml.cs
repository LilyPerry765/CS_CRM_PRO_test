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
using CRM.Data;
using CRM.Application.Views;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace CRM.Application.UserControls
{
    public partial class SpecialService : UserControl
    {
        #region Properties

        private long _ReqID = 0;     

        private Request _Request { get; set; }
        private CRM.Data.SpecialService _SpecialService { get; set; }
        private List<CRM.Data.SpecialService> _SpecialServiceList { get; set; }
        private SpecialServiceType _SpecialServiceType { get; set; }

        public long TelephoneNo { get; set; }        

        #endregion

        #region Constructors

        public SpecialService()
        {
            InitializeComponent();
        }

        public SpecialService(long requestID, long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            TelephoneNo = telephoneNo;

            Initialize();

        }

        #endregion

        #region Methods

        private void Initialize()
        {
           
        }


        #endregion

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           

            List<CheckableItem> specialServiceTypeListAll = Data.SpecialServiceTypeDB.GetSpecialServiceTypeCheckableForTelephone(TelephoneNo);


            if (_ReqID == 0)
            {
                _SpecialService = new Data.SpecialService();


            }
            else
            {

                _Request = Data.RequestDB.GetRequestByID(_ReqID);
                _SpecialService = Data.SpecialServiceDB.GetSpecialServiceByID(_ReqID);
                Status Status = Data.StatusDB.GetStatueByStatusID(_Request.StatusID);
                if (Status.StatusType != (byte)DB.RequestStatusType.Observation)
                {
                    CRM.Data.Schema.SequenceIDs InstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(_SpecialService.InstallSpecialService.ToString());
                    specialServiceTypeListAll.ForEach(item => { if (InstallSpecialServiceSequenceIDs.Ids.Contains(item.ID)) item.IsChecked = true; });

                    CRM.Data.Schema.SequenceIDs UnInstallSpecialServiceSequenceIDs = LogSchemaUtility.Deserialize<CRM.Data.Schema.SequenceIDs>(_SpecialService.UninstallSpecialService.ToString());
                    specialServiceTypeListAll.ForEach(item => { if (UnInstallSpecialServiceSequenceIDs.Ids.Contains(item.ID)) item.IsChecked = false; });
                }

            }
            SpecialServiceGroupBox.Visibility = Visibility.Visible;
            SpecialServiceDetailsGroupBox.Visibility = Visibility.Visible;
            SpecialServiceListView.ItemsSource = specialServiceTypeListAll;
        }

        CheckBox SpecialServiceListViewCheckBox = new CheckBox();
        private void SpecialServiceListViewCheckBox_Loaded(object sender, RoutedEventArgs e)
        {
            SpecialServiceListViewCheckBox = sender as CheckBox;
            (SpecialServiceListViewCheckBox.Template.FindName("BorderTemplate", SpecialServiceListViewCheckBox) as Border).Background = Brushes.Transparent;
        }


    }
}
