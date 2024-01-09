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

namespace CRM.Application.Views
{
    public partial class ADSLChangeTariffFullView : Local.PopupWindow
    {
        #region Properties

        private long _RequestID=0;

        #endregion

        #region Constuctors

        public ADSLChangeTariffFullView(long requestId)
        {
            InitializeComponent();
            
            _RequestID = requestId;

            LoadData();            
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            RequestInfoGrid.DataContext = Data.ADSLChangeTariffDB.GetADSLChangeServiceInfo(_RequestID);
            ResizeWindow();
        }

        #endregion
    }
}
