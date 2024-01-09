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
    public partial class ADSLOMC : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private ADSLRequest _ADSLRequest { get; set; }
        private ADSLPortsInfo _ADSLPortInfo { get; set; }
        private ADSLServiceInfo _ADSLTariff { get; set; }

        #endregion

        #region Constructors

        public ADSLOMC(long requestID)
        {
            RequestID = requestID;

            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            _ADSLRequest = DB.SearchByPropertyName<ADSLRequest>("ID", RequestID).SingleOrDefault();
            _ADSLPortInfo = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
            _ADSLTariff = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);

            TelephoneTextBox.Text = _ADSLRequest.TelephoneNo.ToString();
            EquipmentNameTextBox.Text = _ADSLPortInfo.EquipmentName;
            PortNoTextBox.Text = _ADSLPortInfo.PortNo;
            //-----
            //ServiceTypeTetxBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), (byte)_ADSLRequest.ServiceType);
            //ServiceCostTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLServiceCostPaymentType), (byte)_ADSLRequest.ServiceCostPaymentType);
            //-----
            CustomerPriorityTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLCustomerPriority), (byte)_ADSLRequest.CustomerPriority);
            //RegisterProjectTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLRegistrationProjectType), (byte)_ADSLRequest.RegistrationProjectType);
            RequiredInstalationCheckBox.IsChecked = _ADSLRequest.RequiredInstalation;
            NeedModemCheckBox.IsChecked = _ADSLRequest.NeedModem;

            TariffInfo.DataContext = _ADSLTariff;

            //if (!string.IsNullOrEmpty(_ADSLRequest.UserName))
            //    UserNameTextBox.Text = _ADSLRequest.UserName;

            //if (!string.IsNullOrEmpty(_ADSLRequest.Password))
            //    PasswordTextBox.Text = _ADSLRequest.Password;

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
        }

        public override bool Save()
        {
            try
            {
                //_ADSLRequest.UserName = UserNameTextBox.Text;
                //_ADSLRequest.Password = PasswordTextBox.Text;

                //_ADSLRequest.Detach();
                //DB.Save(_ADSLRequest, false);

                //ShowSuccessMessage("نام کاربری و رمز عبور با موفقیت ذخیره شد.");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {
            try
            {
                //_ADSLRequest.UserName = UserNameTextBox.Text;
                //_ADSLRequest.Password = PasswordTextBox.Text;

                _ADSLRequest.OMCCommnet = CommentTextBox.Text;
                _ADSLRequest.OMCDate = DB.GetServerDate();
                _ADSLRequest.OMCUserID = DB.CurrentUser.ID;

                _ADSLRequest.Detach();
                DB.Save(_ADSLRequest, false);

                IsForwardSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsForwardSuccess;   
        }

        public override bool Deny()
        {
            try
            {
                _ADSLRequest.OMCCommnet = CommentTextBox.Text;

                _ADSLRequest.Detach();
                DB.Save(_ADSLRequest, false);

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsRejectSuccess;
        }

        #endregion

        #region Event Handlers

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserNameTextBox.Text = _ADSLRequest.TelephoneNo.ToString();
            PasswordTextBox.Text = _ADSLRequest.TelephoneNo.ToString();
        }

        #endregion
    }
}
