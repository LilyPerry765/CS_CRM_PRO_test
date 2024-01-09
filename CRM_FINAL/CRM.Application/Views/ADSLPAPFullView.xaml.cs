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
    public partial class ADSLPAPFullView : Local.PopupWindow
    {
        #region Properties

        private long _RequestID=0;

        #endregion

        #region Constuctors

        public ADSLPAPFullView(long requestId)
        {
            InitializeComponent();
            
            _RequestID = requestId;

            Initialize();
            LoadData();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (Data.RequestDB.GetRequestByID( _RequestID).RequestTypeID == (byte)DB.RequestType.ADSLInstalPAPCompany)
                this.Title = "PAP - درخواست دایری";
            if (Data.RequestDB.GetRequestByID( _RequestID).RequestTypeID == (byte)DB.RequestType.ADSLDischargePAPCompany)
                this.Title = "PAP - درخواست تخلیه";
        }

        public void LoadData()
        {
            RequestInfoGrid.DataContext = Data.ADSLPAPRequestDB.GetADSLPAPRequestInfo(_RequestID);
        }

        #endregion
    }
}
