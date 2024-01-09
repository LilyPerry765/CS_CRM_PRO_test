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
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for TaskOfCustomerForm.xaml
    /// </summary>
    public partial class TaskOfCustomerForm : Local.RequestFormBase
    {

        #region fileds && properties
        private long _RequestID = 0;
        private Request _Request;
        Data.ChangeLocation ChangeLocation;
        Data.ChangeAddress _ChangeAddress;
        private UserControls.CustomerAddressUserControl _CustomerAddress { get; set; }
        #endregion

        public TaskOfCustomerForm()
        {
            InitializeComponent();
        }
        public TaskOfCustomerForm(long request)
            : this()
        {
            _RequestID = request;
            _Request = Data.RequestDB.GetRequestByID( _RequestID);
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm , (byte)DB.NewAction.Print};
            base.RequestID = _RequestID;
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            _Request = Data.RequestDB.GetRequestByID( _RequestID);
            ChangeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID( _RequestID);
           // _ChangeAddress = new UserControls.ChangeAddressUserControl(_RequestID, _Request.TelephoneNo);

            _CustomerAddress = new UserControls.CustomerAddressUserControl(_Request.ID);
            ChangeAddressLable.Content = _CustomerAddress;
            ChangeAddressLable.DataContext = _CustomerAddress;

            if (ChangeLocation != null)
            {
                NewTelephoneTextBox.Text = ChangeLocation.NewTelephone.ToString();
                OldTelephoneTextBox.Text = ChangeLocation.OldTelephone.ToString();
            }

        }

        public override bool Confirm()
        {
            try
            {

            switch(_Request.RequestTypeID)
            {
                case  (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                    ChangeLocationSave();
                    break;
                case (int)DB.RequestType.ChangeAddress:
                    ChangeAddressSave();
                    break;
            }

               

                ShowSuccessMessage("تایید انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در تایید",ex);
            }

            IsConfirmSuccess = true;
            return base.Confirm();
        }

        private void ChangeAddressSave()
        {

            _ChangeAddress.ConfirmCustomer = true;
            _ChangeAddress.Detach();
            DB.Save(ChangeLocation);

            RequestLog requestLog = new RequestLog();
            requestLog.RequestID = _RequestID;
            requestLog.RequestTypeID = _Request.RequestTypeID;
            requestLog.TelephoneNo = _Request.TelephoneNo;

            Data.Schema.ChangeAddress changeAddressLog = new Data.Schema.ChangeAddress();
            requestLog.Description = XElement.Parse(LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeNo>(changeAddressLog, true));

            requestLog.Date = DB.GetServerDate();
            requestLog.Detach();
            DB.Save(requestLog);
        }

        private void ChangeLocationSave()
        {
            if (_Request.CenterID == ChangeLocation.TargetCenter)
            {
                ChangeLocation.ConfirmTheTargetCenter = true;
                ChangeLocation.Detach();
                DB.Save(ChangeLocation);

            }
            else if (_Request.CenterID == ChangeLocation.SourceCenter)
            {
                ChangeLocation.ConfirmTheSourceCenter = true;
                ChangeLocation.Detach();
                DB.Save(ChangeLocation);
            }
            else
            {
                ChangeLocation.ConfirmTheSourceCenter = true;
                ChangeLocation.Detach();
                DB.Save(ChangeLocation);
            }
        }

    }
}
