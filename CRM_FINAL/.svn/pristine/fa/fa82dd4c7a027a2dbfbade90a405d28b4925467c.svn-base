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
    /// Interaction logic for ChangeNoDetailUserControl.xaml
    /// </summary>
    public partial class ChangeNoDetailUserControl : Local.UserControlBase
    {
        private long _RequestID;
        private ChangeNo _ChangeNo { get; set; }
        public ChangeNoDetailUserControl()
        {
            InitializeComponent();
        }

        public ChangeNoDetailUserControl(long requestID):this()
        {
            this._RequestID = requestID;
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            ADSLStatusCheckBox.IsEnabled = false;
            RoundTelephoneCheckBox.IsEnabled = false;

            if (_RequestID != 0)
            {
                _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_RequestID);

                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_ChangeNo.OldTelephoneNo);

                Bucht bucht = Data.BuchtDB.GetBuchtBySwitchPortID((int)telephone.SwitchPortID);

                this.DataContext = new { ChangeNo = _ChangeNo, Bucht = bucht };
            }
        }
    }
}
