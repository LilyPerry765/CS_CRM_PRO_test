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
    public partial class CutAndEstablishForm : Local.RequestFormBase
    {
        #region Properties

        CRM.Data.CutAndEstablish _CutAndEstablish { get; set; }
        Request _Request { get; set; }
        Telephone _Telephone { get; set; }

        #endregion

        #region Constructors

        public CutAndEstablishForm(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CutTypesCombo.ItemsSource = Data.CauseOfCutDB.GetCauseOfCutCheckableItem();
             TypesColumn.ItemsSource = Data.CauseOfCutDB.GetCauseOfCutCheckableItem();

            _CutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID( RequestID);
            _Request = Data.RequestDB.GetRequestByID( RequestID);

            switch (_CutAndEstablish.Status)
            {
                case (byte)DB.RequestType.CutAndEstablish:
                    CutTeleNoTextBox.Text = _Request.TelephoneNo.ToString();
                    CutInfo.DataContext = _CutAndEstablish;
                    CutTypesCombo.SelectedValue = _CutAndEstablish.Status.Value;
                    CutInfo.Visibility = Visibility.Visible;
                    break;

                case (byte)DB.RequestType.Connect:
                    EstablishTeleNoTextBox.Text = _Request.TelephoneNo.ToString();
                    EstablishCommentTextBox.Text = _CutAndEstablish.EstablishComment;
                    //EstablishInfo.DataContext = _CutAndEstablish;
                    List<CRM.Data.CutAndEstablish> cutRequestList = Data.CutAndEstablishDB.GetCutAndEstablishByTelephoneNo(_Request.TelephoneNo ?? 0 , (byte)DB.RequestType.CutAndEstablish);
                    CutReasonDataGrid.DataContext = cutRequestList;
                    EstablishInfo.Visibility = Visibility.Visible;
                    break;

                default:
                    break;
            }

            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }

        public override bool Forward()
        {
            Counter counter = new Counter();

            switch (_CutAndEstablish.Status)
            {
                case (int)DB.RequestType.CutAndEstablish:
                    _CutAndEstablish.Counter = CounterTextBox.Text;
                    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    _Telephone.Status = (byte)DB.TelephoneStatus.Cut;
                    _CutAndEstablish.CutDate = DB.GetServerDate();

                  
                    counter.TelephoneNo = _Request.TelephoneNo ?? 0;
                    counter.ID = 0;
                    counter.InsertDate = DB.GetServerDate();
                    counter.CounterReadDate = DB.GetServerDate();
                    counter.CounterNo = CounterTextBox.Text.Trim();
                    counter.Detach();
                    DB.Save(counter,true);

                    RequestForCutAndEstablishDB.SaveRequest(_Request, _CutAndEstablish, null, _Telephone, false);

                    break;

                case (int)DB.RequestType.Connect:
                    _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)_Request.TelephoneNo);
                    _Telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                    _CutAndEstablish.EstablishDate = DB.GetServerDate();


                    counter.TelephoneNo = _Request.TelephoneNo ?? 0;
                    counter.ID = 0;
                    counter.InsertDate = DB.GetServerDate();
                    counter.CounterReadDate = DB.GetServerDate();
                    counter.CounterNo = CounterTextBox.Text.Trim();
                    counter.Detach();
                    DB.Save(counter,true);

                    RequestForCutAndEstablishDB.SaveRequest(_Request, _CutAndEstablish, null, _Telephone, false);
                    break;
            }

            IsForwardSuccess = true;
            return IsForwardSuccess;
        }

        #endregion
    }
}
