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

namespace CRM.Application.Views
{
    public partial class ADSLPAPRequest : Local.RequestFormBase
    {
        #region Properties

        private Request _Request { get; set; }
        private Data.ADSLPAPRequest _ADSLPAPRequest { get; set; }
        private PAPInfo _PAPInfo { get; set; }

        private long _TelephoneNo { get; set; }
        private int CityID = 0;

        #endregion

        #region Constructors

        public ADSLPAPRequest(long telephoneNo)
        {
            _TelephoneNo = telephoneNo;

            InitializeComponent();
            Initialize();

            LoadData();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PAPInfoComboBox.ItemsSource = PAPInfoDB.GetPAPInfoCheckable();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CustomerStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));

            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            TelephoneNoTextBox.Text = _TelephoneNo.ToString();
            CityComboBox.SelectedIndex = 0;
        }

        public override bool Forward()
        {
            try
            {
                _ADSLPAPRequest = new Data.ADSLPAPRequest();
                _Request = new Request();

                if (PAPInfoComboBox.SelectedValue == null)
                    throw new Exception("لطفا شرکت PAP مورد نظر را انتخاب نمایید !");
                else
                {
                    _PAPInfo = DB.SearchByPropertyName<PAPInfo>("ID", PAPInfoComboBox.SelectedValue).SingleOrDefault();

                    if (CenterComboBox.SelectedValue == null)
                        throw new Exception("لطفا مرکز مورد نظر را انتخاب نمایید !");
                    else
                    {
                        if (string.IsNullOrEmpty(PortTextBox.Text))
                            throw new Exception("لطفا پورت مورد نظر را انتخاب نمایید !");
                        else
                        {
                            if (!Data.ADSLPAPPortDB.HasPAPPort(Convert.ToInt32(_PAPInfo.ID), Convert.ToInt64(PortTextBox.Text), Convert.ToInt32(CenterComboBox.SelectedValue)))
                                throw new Exception("* پورت مورد نظر موجود نمی باشد !");
                            else
                            {
                                if (!Data.ADSLPAPPortDB.GetPortStatus(Convert.ToInt32(_PAPInfo.ID), Convert.ToInt64(PortTextBox.Text), Convert.ToInt32(CenterComboBox.SelectedValue)))
                                    throw new Exception("* پورت مورد نظر خالی نمی باشد !");
                                else
                                {
                                    _Request.TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);
                                    _Request.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                                    _Request.CenterID = Convert.ToInt32(CenterComboBox.SelectedValue);
                                    _Request.RequestDate = DB.GetServerDate();
                                    _Request.RequesterName = _PAPInfo.Title;
                                    _Request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
                                    _Request.StatusID = Data.WorkFlowDB.GetNextStates(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLInstalPAPCompany, (byte)DB.RequestStatusType.Start).ID).SingleOrDefault().nextState;
                                    _Request.InsertDate = DB.GetServerDate();
                                    _Request.ModifyDate = DB.GetServerDate();
                                    _Request.PreviousAction = 1;
                                    _Request.IsViewed = false;

                                    _ADSLPAPRequest.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                                    _ADSLPAPRequest.PAPInfoID = _PAPInfo.ID;
                                    _ADSLPAPRequest.TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text);
                                    _ADSLPAPRequest.Customer = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
                                    if (CustomerStatusComboBox.SelectedValue != null)
                                        _ADSLPAPRequest.CustomerStatus = (byte)Convert.ToUInt16(CustomerStatusComboBox.SelectedValue);
                                    else
                                        _ADSLPAPRequest.CustomerStatus =(byte)(DB.ADSLOwnerStatus.Owner);
                                    _ADSLPAPRequest.SplitorBucht = PortTextBox.Text;
                                    _ADSLPAPRequest.LineBucht = "";
                                    _ADSLPAPRequest.InstalTimeOut = 1;
                                    _ADSLPAPRequest.Status = (byte)DB.ADSLPAPRequestStatus.Pending;

                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        if (_Request != null)
                                        {
                                            _Request.ID = DB.GenerateRequestID();
                                            _Request.Detach();
                                            DB.Save(_Request, true);
                                        }

                                        if (_ADSLPAPRequest != null)
                                        {
                                            if (_Request != null)
                                                _ADSLPAPRequest.ID = _Request.ID;

                                            _ADSLPAPRequest.Detach();
                                            DB.Save(_ADSLPAPRequest, true);
                                        }

                                        scope.Complete();
                                    }
                                }
                            }
                        }
                    }
                }

                IsConfirmSuccess = true;
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            return IsConfirmSuccess;
        }

        #endregion

        #region Event Handler

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
