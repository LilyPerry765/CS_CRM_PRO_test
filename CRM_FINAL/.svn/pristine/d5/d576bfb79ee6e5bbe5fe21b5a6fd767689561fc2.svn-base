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
    /// Interaction logic for DischargeADSL.xaml
    /// </summary>
    public partial class DischargeADSL : Local.UserControlBase
    {
        private long _TelephoneNo;


        public CRM.Data.ADSL _ADSL { get; set; }
        public long _Request { get; set; }
        public DischargeADSL()
        {
            InitializeComponent();
        }

        void Initialize()
        {

        }


        public DischargeADSL(long request,long telephoneNo)
            : this()
        {
            this._TelephoneNo = telephoneNo;
            this._Request = request;
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {

            _ADSL = Data.ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            ADSLService aDSLService = DB.SearchByPropertyName<ADSLService>("ID", _ADSL.TariffID).SingleOrDefault();
            TariffInfo.DataContext = aDSLService;

            PAPInfo pAPInfo = DB.SearchByPropertyName<PAPInfo>("ID", _ADSL.PAPInfoID ?? -1).SingleOrDefault();
            if (pAPInfo != null)
                PapInfoTextBox.Text = pAPInfo.Title;


            if (_Request != 0)
            {
                ADSLDischarge aDSLDischarge = Data.ADSLDischargeDB.GetADSLDischargeByID(_Request);
                CommentTextBox.Text = aDSLDischarge.Comment ?? string.Empty;

               
            }

        }
    }
}