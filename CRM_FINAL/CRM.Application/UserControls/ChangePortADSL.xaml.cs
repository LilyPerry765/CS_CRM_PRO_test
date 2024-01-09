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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for ChangePortADSL.xaml
    /// </summary>
    public partial class ChangePortADSL : Local.UserControlBase
    {

        private long _TelephoneNo;


        public CRM.Data.ADSL _ADSL { get; set; }
        public long _Request { get; set; }

        public ChangePortADSL()
        {
            InitializeComponent();
        }
        public ChangePortADSL(long request, long telephoneNo)
            : this()
        {
            this._TelephoneNo = telephoneNo;
            this._Request = request;
        }
        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            _ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);
            Data.ADSLPort aDSLPort = DB.SearchByPropertyName<CRM.Data.ADSLPort>("ID", _ADSL.ADSLPortID).SingleOrDefault();
            ADSLEquipment aDSLEquipment = DB.SearchByPropertyName<CRM.Data.ADSLEquipment>("ID", aDSLPort.ADSLEquipmentID).SingleOrDefault();
            PortInfoTextBox.Text = aDSLEquipment.Equipment +" پورت:"+ aDSLPort.PortNo;


            ADSLService aDSLService = DB.SearchByPropertyName<ADSLService>("ID", _ADSL.TariffID).SingleOrDefault();
            TariffInfo.DataContext = aDSLService;

            PAPInfo pAPInfo = DB.SearchByPropertyName<PAPInfo>("ID", _ADSL.PAPInfoID ?? -1).SingleOrDefault();
            if (pAPInfo != null)
                PapInfoTextBox.Text = pAPInfo.Title;


            if (_Request != 0)
            {
                ADSLChangePort aDSLDischarge = DB.SearchByPropertyName<ADSLChangePort>("ID", _Request).SingleOrDefault();
               if (aDSLDischarge != null)
               CommentTextBox.Text = aDSLDischarge.CommentTextBox.Text ?? string.Empty;
                //****** in bood avazesh kadam bayad niki bebine!!!!!***************
               //CommentTextBox.Text = aDSLDischarge.Comment ?? string.Empty;

            }

        }
    }
}
