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
    /// Interaction logic for RefundDepositUserControl.xaml
    /// </summary>
    public partial class RefundDepositUserControl : Local.UserControlBase
    {

        #region Properties && fildes
        private long _RequestID = 0;
        private long _TelephoneNo;
        private Data.RefundDeposit _RefundDeposit { get; set; }

        #endregion
        public RefundDepositUserControl()
        {
            InitializeComponent();
        }



        public RefundDepositUserControl(long request, long telephoneNo):this()
        {
         
            this._RequestID = request;
            this._TelephoneNo = telephoneNo;

            Initialize();
            
        }

        private void Initialize()
        {
            _RefundDeposit = new RefundDeposit();
            CauseOfRefundDepositComboBox.ItemsSource = Data.CauseOfRefundDepositDB.GetCauseOfRefundDepositCheckableItem();

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
