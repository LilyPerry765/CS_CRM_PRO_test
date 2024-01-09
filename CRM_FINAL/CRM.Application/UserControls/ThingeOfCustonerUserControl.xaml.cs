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
    /// Interaction logic for ThingeOfCustonerUserControl.xaml
    /// </summary>
    public partial class ThingeOfCustonerUserControl : Local.UserControlBase
    {

        #region fields && Properties
        private long _RequestID = 0;
        private RefundDeposit _RefundDeposit { get; set; }

        #endregion
        public ThingeOfCustonerUserControl()
        {
            InitializeComponent();
        }
        public ThingeOfCustonerUserControl(long request):this()
        {
            _RequestID = request;
            Initialize();
        }

        private void Initialize()
        {

           // _RefundDeposit = new RefundDeposit();
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            if (_RequestID != 0)
            {
                _RefundDeposit = Data.RefundDepositDB.GetRefundDepositByID( _RequestID);
            }
            this.DataContext = _RefundDeposit;
        }

    }
}
