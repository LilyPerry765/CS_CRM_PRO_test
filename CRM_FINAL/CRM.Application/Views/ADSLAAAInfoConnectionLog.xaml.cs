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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ADSLAAAInfoConnectionLog.xaml
    /// </summary>
    public partial class ADSLAAAInfoConnectionLog : Local.PopupWindow
    {
        #region Properties

        private string[][] _Detail { get; set; }

        #endregion

        #region Constructors

        public ADSLAAAInfoConnectionLog()
        {
            InitializeComponent();
        }

        public ADSLAAAInfoConnectionLog(string[][] detail)
            : this()
        {
            _Detail = detail;
        }

        #endregion

        #region Methods

        private void LoadData()
        {
            ADSLConnectionLogDetailsInfo detailInfo = new ADSLConnectionLogDetailsInfo();

            if (_Detail != null)
            {
                for (int index = 0; index < _Detail.Length; index++)
                {
                    if (_Detail[index][0] == "terminate_cause")
                        detailInfo.TerminateCause = _Detail[index][1];

                    if (_Detail[index][0] == "port")
                        detailInfo.Port = _Detail[index][1];

                    if (_Detail[index][0] == "mac")
                        detailInfo.Mac = _Detail[index][1];

                    if (_Detail[index][0] == "kill_reason")
                        detailInfo.KillReason = _Detail[index][1];

                    if (_Detail[index][0] == "ippool_assigned_ip")
                        detailInfo.IPpoolAssignedIP = _Detail[index][1];

                    if (_Detail[index][0] == "ippool")
                        detailInfo.Ippool = _Detail[index][1];

                    if (_Detail[index][0] == "acct_session_id")
                        detailInfo.AcctSessionID = _Detail[index][1];
                }
            }

            this.DataContext = detailInfo;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion
    }
}
