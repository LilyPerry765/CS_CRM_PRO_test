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
    /// Interaction logic for MDFChangeNoUC.xaml
    /// </summary>
    public partial class MDFChangeNoUC : Local.UserControlBase
    {
        #region Properties && filde
        private long _RequestID;
        private ChangeNo _ChangeNo { get; set; }
        #endregion
        public MDFChangeNoUC()
        {
            InitializeComponent();
        }

        public MDFChangeNoUC(long request):this()
        {
            this._RequestID = request;
            
            
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            _ChangeNo = new ChangeNo();
            if (_RequestID != 0)
            {
                _ChangeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_RequestID);
                NewSwitchPortID.Text = Data.SwitchPortDB.GetSwitchPortByID((int) _ChangeNo.NewSwitchPortID).PortNo;
                OldSwitchPortID.Text = Data.SwitchPortDB.GetSwitchPortByID((int) _ChangeNo.OldSwitchPortID).PortNo;

                ConnectionInfo connectionInfo = DB.GetBuchtInfoByID((long)_ChangeNo.OldBuchtID);
                RowTextBox.Text = connectionInfo.VerticalRowNo.ToString();
                ColumnTextBox.Text = connectionInfo.VerticalColumnNo.ToString();
                ConnectionTextBox.Text = connectionInfo.BuchtNo.ToString();
                MDFONUInfoTextBox.Text = connectionInfo.MDF;
            }

            this.DataContext = _ChangeNo;
        }
    }
}
