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
    public partial class ADSLChangePlaceUserControl : UserControl
    {
        #region Properties

        private long _ReqID = 0;
        private long _TelephoneNo = 0;

        private Data.ADSLChangePlace _ADSLChangePlace { get; set; }
        private Request _Request { get; set; }
        public Data.ADSL _ADSL { get; set; }
        public TeleInfo TeleInfo { get; set; }
        public Customer ADSLCustomer { get; set; }
        private Service1 service = new Service1();

        private int _CenterID = 0;
        //public int counter = 0;

        public bool _HasPort = true;
        public bool _IsNotADSL = true;

        #endregion

        #region Constructor

        public ADSLChangePlaceUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLChangePlaceUserControl(long requestID, long telephoneNo)
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
        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_ReqID == 0)
            {
                CostCheckBox.IsChecked = true;
            }
            else
            {
                int centerid;
                _ADSLChangePlace = ADSLChangePlaceDB.GetADSLChangePlaceById(_ReqID);

                if (_ADSLChangePlace != null)
                {
                    NewTelephoneNoInfoGrid.Visibility = Visibility.Visible;

                    NewTelNoTextBox.Text = _ADSLChangePlace.NewTelephoneNo.ToString();

                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _ADSLChangePlace.NewTelephoneNo.ToString());
                    if (telephoneInfo.Rows.Count == 0)
                    {
                        NewCenterNameTextBox.Text = TelephoneDB.GetCenterNameByTelephoneNo(Convert.ToInt64(NewTelNoTextBox.Text));
                        AddressTextBox.Text = TelephoneDB.GetAddressContentByTelephoneNo(Convert.ToInt64(NewTelNoTextBox.Text));
                        PostalCodeTextBox.Text = TelephoneDB.GetPostalCodeByTelephoneNo(Convert.ToInt64(NewTelNoTextBox.Text));
                        centerid = CenterDB.GetCenterIDbyTelephoneNo(Convert.ToInt64(NewTelNoTextBox.Text));
                    }
                    else
                    {
                        centerid = CenterDB.GetCenterIDByName(CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).CenterName);
                        NewCenterNameTextBox.Text = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).CenterName;
                        //NewCenterNameTextBoxShow.Text = CityDB.GetCityByCenterID(centerid).Name + ":" + CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).CenterName;
                        AddressTextBox.Text = telephoneInfo.Rows[0]["address"].ToString();
                        if (telephoneInfo.Rows[0]["CODE_POSTI"] != null)
                            PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                    }

                    if (_ADSLChangePlace.HasCost != null)
                        if ((bool)_ADSLChangePlace.HasCost)
                            CostCheckBox.IsChecked = true;
                }
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long telephoneNo = Convert.ToInt64(NewTelNoTextBox.Text);

                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", NewTelNoTextBox.Text);
                if (telephoneInfo.Rows.Count == 0)
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                    if (telephone == null)
                    {
                        TelephoneErrorLabel.Visibility = Visibility.Visible;
                        NewTelephoneNoInfoGrid.Visibility = Visibility.Collapsed;

                        return;
                    }
                }

                TelephoneErrorLabel.Visibility = Visibility.Collapsed;
                NewTelephoneNoInfoGrid.Visibility = Visibility.Visible;

                if (telephoneInfo.Rows.Count == 0)
                {
                    NewCenterNameTextBox.Text = TelephoneDB.GetCenterNameByTelephoneNo(telephoneNo);
                    AddressTextBox.Text = TelephoneDB.GetAddressContentByTelephoneNo(telephoneNo);
                    PostalCodeTextBox.Text = TelephoneDB.GetPostalCodeByTelephoneNo(telephoneNo);
                    _CenterID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                }
                else
                {
                    _CenterID = CenterDB.GetCenterIDByName(CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).CenterName);

                    NewCenterNameTextBox.Text = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).CenterName;
                    AddressTextBox.Text = telephoneInfo.Rows[0]["address"].ToString();

                    if (telephoneInfo.Rows[0]["CODE_POSTI"] != null)
                        PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();

                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);

                    if (telephone == null)
                    {
                        telephone = new Telephone();
                        telephone.TelephoneNo = telephoneNo;
                        telephone.TelephoneNoIndividual = Convert.ToInt64(telephoneNo.ToString().Substring(2));
                        telephone.CenterID = _CenterID;
                        telephone.Status = 2;
                        telephone.ClassTelephone = 1;

                        telephone.Detach();
                        DB.Save(telephone, true);
                    }
                }

                Service1 aDSLService = new Service1();
                if (aDSLService.Phone_Is_PCM(telephoneNo.ToString()))
                {
                    PCMLabel.Content = "شماره جدید بر روی PCM می باشد !";
                    PCMLabel.Visibility = Visibility.Visible;
                }
                else
                {
                    PCMLabel.Content = string.Empty;
                    PCMLabel.Visibility = Visibility.Collapsed;
                }

                int currentMDFID = ADSLMDFRangeDB.GetMDFinRangebyTelephoneNo(telephoneNo, _CenterID);
                List<Data.ADSLPort> portFreeList = ADSLMDFDB.GetFreeADSLPortByCenterID(_CenterID, currentMDFID);

                if (portFreeList.Count == 0)
                {
                    NotFreePortLabel.Content = "در مرکز جدید تجهیزات فنی وجود ندارد !";
                    NotFreePortLabel.Visibility = Visibility.Visible;
                    _HasPort = false;
                }
                else
                {
                    NotFreePortLabel.Content = string.Empty;
                    NotFreePortLabel.Visibility = Visibility.Collapsed;
                    _HasPort = true;
                }

                if (ADSLDB.GetADSLByTelephoneNo(telephoneNo) != null && ADSLDB.GetADSLByTelephoneNo(telephoneNo).Status == (byte)DB.ADSLStatus.Connect)
                {
                    NewPhoneHasADSLLabel.Content = "شماره جدید دارای ای دی اس ال می باشد !";
                    NewPhoneHasADSLLabel.Visibility = Visibility.Visible;
                    _IsNotADSL = false;
                }
                else
                {
                    string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(telephoneNo);
                    if (!string.IsNullOrWhiteSpace(papName))
                    {
                        NewPhoneHasADSLLabel.Content = "شماره جدید از شرکت " + papName + " دارای ای دی اس ال می باشد";
                        NewPhoneHasADSLLabel.Visibility = Visibility.Visible;
                        _IsNotADSL = false;
                    }
                    else
                    {
                        NewPhoneHasADSLLabel.Content = string.Empty;
                        NewPhoneHasADSLLabel.Visibility = Visibility.Collapsed;
                        _IsNotADSL = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ذخیره شماره جدید با مشکل روبه رو شده است !");
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;
        }
       
        #endregion       
    }
}
