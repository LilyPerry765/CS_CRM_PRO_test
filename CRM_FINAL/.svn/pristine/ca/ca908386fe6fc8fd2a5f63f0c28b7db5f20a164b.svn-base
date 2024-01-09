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
using System.Transactions;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ChangeLocationThingeOfCustonerForm.xaml
    /// </summary>
    public partial class ChangeLocationThingeOfCustonerForm : Local.RequestFormBase
    {

        #region fields && Properties
        private long _RequestID;
        private Request request;
        private ChangeLocation changeLocation;
        private TakePossession  takePossession;
        public  RefundDeposit _RefundDeposit;
        public UserControls.ThingeOfCustonerUserControl _ThingeOfCustonerUserControl;

        #endregion

        #region Method

        public ChangeLocationThingeOfCustonerForm()
        {
            InitializeComponent();
        }

        public ChangeLocationThingeOfCustonerForm(long requestID):this()
        {
            this._RequestID = requestID;
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm };
            request = Data.RequestDB.GetRequestByID(_RequestID);
            base.RequestID = _RequestID;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            request = Data.RequestDB.GetRequestByID(_RequestID);

            switch (request.RequestTypeID)
            {
                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                case (byte)DB.RequestType.ChangeLocationCenterInside:
                     ChangeLocationLoad();
                    break;

                case (byte)DB.RequestType.Dischargin:
                    Dischargin();
                    break;
                case (byte)DB.RequestType.RefundDeposit:
                    RefundDepositLoad();
                    break;
            }
        }
        #endregion

        #region SaveMethode
        void ChangeLocationSave()
        {
            if (changeLocation.TargetCenter != null && request.CenterID == changeLocation.SourceCenter)
            {
                Status Status = Data.StatusDB.GetStatuesByStatusID(request.StatusID).Where(t => t.StatusType == (byte)DB.RequestStatusType.Completed).SingleOrDefault();
                if (Status.ID == request.StatusID)
                {
                    request.CenterID = (int)changeLocation.TargetCenter;
                    request.Detach();
                    DB.Save(request);
                }


                changeLocation.ConfirmTheSourceCenter = true;
                changeLocation.Detach();
                DB.Save(changeLocation);


            }
            else
            {


                changeLocation.Status = (byte)DB.ChangeLocationStatus.Confirm;
                changeLocation.Detach();
                DB.Save(changeLocation);


            }
        }

        void DischarginSave()
        {
            takePossession.Status = (byte)DB.TakePossessionStatus.Confirm;

            
            using (TransactionScope ts = new TransactionScope())
            {
                takePossession.Detach();
                DB.Save(takePossession);


                ts.Complete();
            }
            

          

        }

        void RefundDepositSave()
        {
            base.RequestID = _RequestID;
            _RefundDeposit = _ThingeOfCustonerUserControl.DataContext as RefundDeposit;
            _RefundDeposit.thingesOfCustomer = true;
            _RefundDeposit.Detach();
            DB.Save(_RefundDeposit);


             RefundDeposit refundDesposit = Data.RefundDepositDB.GetRefundDepositByID(_RequestID);

            RequestLog requestLog = new RequestLog();
            requestLog.RequestID = request.ID;
            requestLog.RequestTypeID = request.RequestTypeID;
            requestLog.TelephoneNo = refundDesposit.TelephoneNo;

            CRM.Data.Schema.RefundDesposit refundDespositLog = new Data.Schema.RefundDesposit();
            refundDespositLog.SwitchPort = refundDesposit.SwitchPortID ?? 0;
            refundDespositLog.TelephoneNo = refundDesposit.TelephoneNo ?? 0;

            requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.RefundDesposit>(refundDespositLog, true));

            requestLog.Date = DB.GetServerDate();
            requestLog.Detach();
            DB.Save(requestLog);




        }

        #endregion

        #region LoadMethode
        void ChangeLocationLoad()
        {
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_RequestID);

            OldTelTextBox.Text = changeLocation.OldTelephone.ToString();
            NewTelTextBox.Text = changeLocation.NewTelephone.ToString();

            CustomerInfoSummaryUserControl.IsExpandedProperty = true;
            CustomerInfoSummaryUserControl.Mode = true;
            CustomerInfoSummaryUserControl = new UserControls.CustomerInfoSummary(request.CustomerID);
            OldCustomerAddressUserControl = new UserControls.CustomerAddressUserControl(request.ID);
            if (changeLocation.Status == (byte)(byte)DB.ChangeLocationStatus.Confirm)
                ActionIDs.Remove((byte)DB.Action.Confirm);

            
        }
        void Dischargin()
        {

            takePossession = Data.TakePossessionDB.GetTakePossessionByID(_RequestID);
            OldTelTextBox.Text = takePossession.OldTelephone.ToString();
            NewTelTextBox.Visibility = Visibility.Collapsed;
            CustomerInfoSummaryUserControl.IsExpandedProperty = true;
            CustomerInfoSummaryUserControl.Mode = true;
            CustomerInfoSummaryUserControl = new UserControls.CustomerInfoSummary(request.CustomerID);
            OldCustomerAddressUserControl = new UserControls.CustomerAddressUserControl(request.ID);
            if (takePossession.Status == (byte)DB.TakePossessionStatus.Confirm)
                ActionIDs.Remove((byte)DB.Action.Confirm);
          
        }
        void RefundDepositLoad()
        {
            OldCustomerAddressUserControl.Visibility = Visibility.Collapsed;
            CustomerInfoSummaryUserControl.Visibility = Visibility.Collapsed;
            NewTelTextBox.Visibility = Visibility.Collapsed;
            NewTelLable.Visibility = Visibility.Collapsed;
            ThingeOfCustonerUserControlGroupBox.Visibility = Visibility.Visible;

            _RefundDeposit = Data.RefundDepositDB.GetRefundDepositByID( _RequestID);
            OldTelTextBox.Text = _RefundDeposit.TelephoneNo.ToString();

            _ThingeOfCustonerUserControl =new UserControls.ThingeOfCustonerUserControl(_RequestID);
            ThingeOfCustonerUserControlGroupBox.Content = _ThingeOfCustonerUserControl;
            ThingeOfCustonerUserControlGroupBox.DataContext = _ThingeOfCustonerUserControl;
        }
     
        
        #endregion

        #region ActionMethode
        public override bool Confirm()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {


                    // اگر تغییر مکان مرکز به مرکز میباشد و در مبدا باشد به سالن مقصد انتقال داده میشود
                    if (request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside || request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter)
                    {
                        ChangeLocationSave();
                    }
                    else if (request.RequestTypeID == (byte)DB.RequestType.Dischargin)
                    {
                        DischarginSave();
                    }
                    else if (request.RequestTypeID == (byte)DB.RequestType.RefundDeposit)
                    {
                        RefundDepositSave();
                    }

                    ts.Complete();

                }
                ShowSuccessMessage("تایید انجام شد");
                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("تایید انجام نشد", ex);
            }

            return base.Save();
        }
        #endregion
       


    }
}
