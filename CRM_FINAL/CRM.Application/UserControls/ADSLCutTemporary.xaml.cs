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
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;

namespace CRM.Application.UserControls
{
    public partial class ADSLCutTemporary : UserControl
    {
        #region Properties

        private long _ReqID = 0;
        private long _TelephoneNo = 0;

        private Data.ADSLCutTemporary _ADSLCutTemporary { get; set; }
        private Request _Request { get; set; }
        public Data.ADSL _ADSL { get; set; }
        public TeleInfo TeleInfo { get; set; }
        public Customer ADSLCustomer { get; set; }
        public string UserID { get; set; }


        #endregion

        #region Constructors

        public ADSLCutTemporary()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLCutTemporary(long requestID, long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            _TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ADSLCustomer = new Customer();

            CutTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLCutType));
        }

        private void DisableControls()
        {
            CutTypeComboBox.IsEnabled = false;
            //CutDate.IsEnabled = false;
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

            //CRMWebService.CRMWebService webServive = new CRMWebService.CRMWebService();

            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();
            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", _TelephoneNo.ToString());
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {

            }
            foreach (DictionaryEntry User in userInfos)
            {
                userInfo = (XmlRpcStruct)User.Value;
            }

            UserID=ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
            try
            {
                
                CutTypeTextBox.Text=ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["lock"]);
                CutTypeComboBox.Visibility = Visibility.Hidden;
                CutReasonComboBoxLabel.Visibility = Visibility.Hidden;
                
            }

            catch (Exception ex)
                {
                     CutTypeComboBox.SelectedValue = (byte)DB.ADSLCutType.SubscriberRequest;
                    CutTypeTextBox.Visibility = Visibility.Hidden;
                    CutReasonTextBoxLabel.Visibility = Visibility.Hidden;
                    
                }

            _ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (_ADSL.ADSLPortID != null)
            {
                ADSLPortInfo portInfo = ADSLPortDB.GetADSlPortInfoByID((long)_ADSL.ADSLPortID);
                PortNoTextBox.Text = portInfo.Port;
                MDFDescriptionTextBox.Text = portInfo.MDFTitle;
            }
            if (_ReqID == 0)
            {
                TeleInfo = Data.TelephoneDB.GetTelephoneInfoByTelephoneNo(_TelephoneNo);
                _ADSL = Data.ADSLDB.GetADSLByTelephoneNo(  _TelephoneNo);


                //if (_ADSL.CustomerOwnerStatus!=null)
                //ADSLOwnerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSL.CustomerOwnerStatus);


                //ADSLCustomer = Data.CustomerDB.GetCustomerByID((long)_ADSL.CustomerOwnerID);

                //if(ADSLCustomer.NationalCodeOrRecordNo!=null)
                //NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();

                //CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;

                //if(_ADSL.ServiceType!=null)
                //ServiceTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), (int)_ADSL.ServiceType);


                TariffInfo.DataContext = DB.SearchByPropertyName<ADSLService>("ID", _ADSL.TariffID).SingleOrDefault();

                
                //CutDate.SelectedDate = DB.GetServerDate();
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID( _ReqID);
                _ADSLCutTemporary= DB.SearchByPropertyName<Data.ADSLCutTemporary>("ID", _ReqID).SingleOrDefault();
                //_ADSL = Data.ADSLDB.GetADSLByTelephoneNo((long)_ADSLCutTemporary.Request.TelephoneNo);

                TeleInfo = Data.TelephoneDB.GetTelephoneInfoByTelephoneNo(_TelephoneNo);


                //if(_ADSL.CustomerOwnerStatus!=null)
                //ADSLOwnerStatusTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLOwnerStatus), (int)_ADSL.CustomerOwnerStatus);


                ADSLCustomer = Data.CustomerDB.GetCustomerByID((long)_ADSL.CustomerOwnerID);


                //if (ADSLCustomer.NationalCodeOrRecordNo != null)
                //NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();

                //CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;

                //if (_ADSL.ServiceType != null)
                //ServiceTypeTextBox.Text = Helper.GetEnumDescriptionByValue(typeof(DB.ADSLServiceType), (int)_ADSL.ServiceType);


                TariffInfo.DataContext = DB.SearchByPropertyName<ADSLService>("ID", _ADSL.TariffID).SingleOrDefault();

                CutTypeComboBox.SelectedValue = _ADSLCutTemporary.CutType;
                



                //CutDate.SelectedDate = _ADSLCutTemporary.CutDate;

                //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                //{
                //    DisableControls();


                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CommentsGroupBox.Visibility = Visibility.Visible;                    
                //}

                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //    CutCommentLabel.Visibility = Visibility.Collapsed;
                //    CutCommentTextBox.Visibility = Visibility.Collapsed;
                //    FinalCommentLabel.Visibility = Visibility.Collapsed;
                //    FinalCommentTextBox.Visibility = Visibility.Collapsed;
                //}


                //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Completed).ID)
                //{
                //    DisableControls();


                //    CommentCustomersTextBox.IsReadOnly = true;
                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //}

                //    CommentCustomersTextBox.IsReadOnly = true;
                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CutCommentTextBox.Text = _ADSLCutTemporary.CutComment;
                //    FinalCommentLabel.Visibility = Visibility.Collapsed;
                //    FinalCommentTextBox.Visibility = Visibility.Collapsed;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //}


                //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Pending).ID)
                //{
                //    DisableControls();


                    //CommentCustomersTextBox.IsReadOnly = true;
                    //CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                    //CommentsGroupBox.Visibility = Visibility.Visible;
                //}

                //    CommentCustomersTextBox.IsReadOnly = true;
                //    CutCommentTextBox.IsReadOnly = true;
                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CutCommentTextBox.Text = _ADSLCutTemporary.CutComment;
                //    FinalCommentTextBox.Text = _ADSLCutTemporary.FinalComment;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //}


                //if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.End).ID)
                //{
                //    DisableControls


                    //CommentCustomersTextBox.IsReadOnly = true;
                    //CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                    //CommentsGroupBox.Visibility = Visibility.Visible;
                //}

                //    CommentCustomersTextBox.IsReadOnly = true;
                //    CutCommentTextBox.IsReadOnly = true;
                //    FinalCommentTextBox.IsReadOnly = true;
                //    CommentCustomersTextBox.Text = _ADSLCutTemporary.CommentCustomers;
                //    CutCommentTextBox.Text = _ADSLCutTemporary.CutComment;
                //    FinalCommentTextBox.Text = _ADSLCutTemporary.FinalComment;
                //    CommentsGroupBox.Visibility = Visibility.Visible;
                //}

            }
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        #endregion
    }
}
