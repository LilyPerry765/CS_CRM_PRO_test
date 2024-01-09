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
using System.Collections.ObjectModel;
using Enterprise;
using CRM.Application.UserControls;
using CRM.Application.Views;

namespace CRM.Application.Views
{
    public partial class SpecialServiceForm : Local.RequestFormBase
    {
        #region Properties

        private CRM.Data.SpecialService _SpecialService { get; set; }
        private SpecialServiceType _SpecialServiceType { get; set; }
        private List<CRM.Data.SpecialService> _SpecialServiceList { get; set; }
        private Request _Request { get; set; }
        private bool IsChecked { get; set; }

        #endregion

        #region Constructors

        public SpecialServiceForm(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _Request = Data.RequestDB.GetRequestByID( RequestID);

            _SpecialService = DB.SearchByPropertyName<CRM.Data.SpecialService>("RequestID", RequestID).FirstOrDefault();
            TeleNoLabel.Content = _Request.TelephoneNo;

            //_SpecialServiceList = DB.SearchByPropertyName<CRM.Data.SpecialService>("RequestID", _SpecialService.RequestID).ToList();

            foreach (Data.SpecialService currentService in _SpecialServiceList)
            {
                //if (currentService.Status == (byte)DB.StatusSpecialService.RequestInstal)
                //{
                //    _SpecialServiceType = DB.SearchByPropertyName<SpecialServiceType>("ID", currentService.SpecialServiceTypeID).SingleOrDefault();
                //    SpecialServiceListViewAdd.Items.Add(_SpecialServiceType);
                //}

                //if (currentService.Status == (byte)DB.StatusSpecialService.RequestUnInstal)
                //{
                //    _SpecialServiceType = DB.SearchByPropertyName<SpecialServiceType>("ID", currentService.SpecialServiceTypeID).SingleOrDefault();
                //    SpecialServiceListViewDel.Items.Add(_SpecialServiceType);
                //}
            }

            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }

        public override bool Forward()
        {
            //Request oldRequest = new Request();
            ////_SpecialServiceList = DB.SearchByPropertyName<CRM.Data.SpecialService>("RequestID", _RequestID).ToList();
            //List<Data.SpecialService> oldSpecialService = new List<Data.SpecialService>();

            //foreach (Data.SpecialService currentService in _SpecialServiceList)
            //{
            //    if (currentService.Status == (byte)DB.StatusSpecialService.RequestInstal)
            //    {
            //        currentService.Status = (byte)DB.StatusSpecialService.Instal;
            //        currentService.InstallDate = DB.GetServerDate();
            //    }
            //    if (currentService.Status == (byte)DB.StatusSpecialService.RequestUnInstal)
            //    {
            //        currentService.Status = (byte)DB.StatusSpecialService.UnInstal;
            //        currentService.UnInstalDate = DB.GetServerDate();
            //    }

            //    oldSpecialService = Data.SpecialServiceDB.GetLastSpecialServiceByTypeAndNo(currentService.SpecialServiceTypeID, _Request.TelephoneNo);
            //    foreach (Data.SpecialService oldService in oldSpecialService)
            //    {
            //        if (currentService.Status == (byte)DB.StatusSpecialService.Instal)
            //            oldService.Status = (byte)DB.StatusSpecialService.ArchiveInstal;

            //        if (currentService.Status == (byte)DB.StatusSpecialService.UnInstal)
            //            oldService.Status = (byte)DB.StatusSpecialService.ArchiveUnInstal;

            //        oldRequest = Data.RequestDB.GetRequestByID(oldService.RequestID);
            //        RequestForSpecialServiceDB.SaveSingleRequest(oldRequest, oldService, null, null, false);
            //    }
            //}

            RequestForSpecialServiceDB.SaveRequest(_Request, _SpecialServiceList, null, null, false);

            IsForwardSuccess = true;
            return IsForwardSuccess;
        }

        #endregion
    }
}
