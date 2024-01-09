using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CRM.Data;
using CRM.Application;
using System.Drawing;
using System.Globalization;
using System.Web.Services;

namespace CRM.Website.UserControl
{
    public partial class ADSL : System.Web.UI.UserControl
    {
        #region Properties

        private int _CenterID = 0;
        private int _CityID = 0;
        private long _RequestID = 0;
        //private long _CustomerID = 0;
        private long _TelephoneNo;
        private CRM.Data.ADSLRequest _ADSLRequest { get; set; }
        private Request _Request { get; set; }
        //private Customer _Customer = new Customer();
        private Telephone _Telephone { get; set; }
        private Bucht _Bucht { get; set; }
        private Data.ADSL _ADSL { get; set; }
        private Service1 _Service = new Service1();
        private int _SellerAgentID { get; set; }
        private List<int> _ServiceAccessList { get; set; }
        private List<int> _ServiceGroupAccessList { get; set; }
        private ADSLServiceInfo _ServiceInfo { get; set; }
        private bool _HasCreditUser = true;
        private bool _HasCreditAgent = true;

        public bool IsWaitingList { get; set; }
        public Customer ADSLCustomer { get; set; }
        public List<Customer> ADSLCustomerList { get; set; }
        public ADSLGroupIP GroupIPStatic { get; set; }
        public System.Data.DataTable TelephoneInfo { get; set; }
        public long SumPriceService = 0;
        public long SumPriceIP = 0;
        public long SumPriceTraffic = 0;
        public long SumPriceModem = 0;

        public bool HasCreditAgent
        {
            get { return _HasCreditAgent; }
            set { _HasCreditAgent = value; }
        }

        public bool HasCreditUser
        {
            get { return _HasCreditUser; }
            set { _HasCreditUser = value; }
        }

        public ADSLIP IPStatic
        {
            //get { return _IPStatic; }
            //set { _IPStatic = value; }
            get { return Session["ADSLIPStatic"] == null ? null : Session["ADSLIPStatic"] as ADSLIP; }
            set { Session["ADSLIPStatic"] = value; }
        }

        public long RequestID
        {
            get { return _RequestID; }
            set { _RequestID = value; }
        }
        //public long CustomerID
        //{
        //    get { return _CustomerID; }
        //    set { _CustomerID = value; }
        //}
        public long TelephoneNo
        {
            get { return _TelephoneNo; }
            set { _TelephoneNo = value; }
        }

        #region Controls Content

        public string MobileNo
        {
            get
            {
                return MobileNoTextBox.Text;
            }
        }

        public string NationalCode
        {
            get
            {
                return NationalCodeTextBox.Text;
            }
        }

        public string CustomerGroupValue
        {
            get
            {
                return CustomerGroupDropDownList.SelectedValue;
            }
        }

        public string ADSLOwnerStatusValue
        {
            get
            {
                return ADSLOwnerStatusDropDownList.SelectedValue;
            }
        }

        public string JobGroupValue
        {
            get
            {
                return JobGroupDropDownList.SelectedValue;
            }
        }

        public string ReagentTelephoneNo
        {
            get
            {
                return ReagentTelephoneNoTextBox.Text;
            }
        }

        public string ServiceValue
        {
            get
            {
                return ServiceDropDownList.SelectedValue;
            }
        }

        public string LicenceLetterNo
        {
            get
            {
                return LicenceLetterNoTextBox.Text;
            }
        }

        public string AdditionalServiceValue
        {
            get
            {
                return AdditionalServiceDropDownList.SelectedValue;
            }
        }

        public bool RequiredInstalationIsChecked
        {
            get
            {
                return RequiredInstalationCheckBox.Checked;
            }
        }

        public bool? HasIPStatic
        {
            get
            {
                return HasIPStaticCheckBox.Checked;
            }
        }

        public string IPTypeValue
        {
            get
            {
                return IPTypeDropDownList.SelectedValue;
            }
        }

        public string IPTimeValue
        {
            get
            {
                return IPTimeDropDownList.SelectedValue;
            }
        }

        public bool? HasNeedModem
        {
            get
            {
                return NeedModemCheckBox.Checked;
            }
        }

        public string ModemTypeValue
        {
            get
            {
                return ModemTypeDropDownList.SelectedValue;
            }
        }

        public string ModemMACAddress
        {
            get
            {
                return ModemMACAddressTextBox.Text;
            }
        }

        public string ModemSerialNo
        {
            get
            {
                return ModemSerialNoDropDownList.SelectedValue;
            }
        }

        public string CommentCustomers
        {
            get
            {
                return CommentCustomersTextBox.Text;
            }
        }


        #endregion Controls Content

        #endregion

        #region Methods

        private void DisableControls()
        {
            ADSLOwnerStatusDropDownList.Enabled = false;
            NationalCodeTextBox.ReadOnly = true;
            CustomerNameTextBox.ReadOnly = true;
            //MobileNoTextBox.ReadOnly = true;
            SearchButton.Visible = false;
            CustomerGroupDropDownList.Enabled = false;
            JobGroupDropDownList.Enabled = false;
            ReagentTelephoneNoTextBox.ReadOnly = true;
            RequiredInstalationCheckBox.Enabled = false;
            NeedModemCheckBox.Enabled = false;
            ModemTypeDropDownList.Enabled = false;
            ServiceDropDownList.Enabled = false;
            LicenceLetterNoTextBox.ReadOnly = true;
            CustomerEndRentDate.Enabled = false;
        }

        public void LoadData()
        {
            if (ADSLCustomer == null)
                ADSLCustomer = new Customer();

            if (!IsPostBack)
            {
                ADSLOwnerStatusDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.ADSLOwnerStatus));
                ADSLOwnerStatusDropDownList.DataBind();
                ADSLOwnerStatusDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                CustomerGroupDropDownList.DataSource = DB.GetAllEntity<ADSLCustomerGroup>();
                CustomerGroupDropDownList.DataBind();
                CustomerGroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                JobGroupDropDownList.DataSource = DB.GetAllEntity<JobGroup>();
                JobGroupDropDownList.DataBind();
                JobGroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                ModemTypeDropDownList.DataSource = ADSLModemDB.GetSalableModemsTitle();
                ModemTypeDropDownList.DataBind();
                ModemTypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLService();
                ServiceDropDownList.DataBind();
                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckable();
                GroupDropDownList.DataBind();
                GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                TypeDropDownList.DataSource = Helper.GetEnumCheckable(typeof(DB.ADSLServiceCostPaymentType));
                TypeDropDownList.DataBind();
                TypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                BandWidthDropDownList.DataSource = ADSLServiceDB.GetADSLServiceBandWidthCheckable();
                BandWidthDropDownList.DataBind();
                BandWidthDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                TrafficDropDownList.DataSource = ADSLServiceDB.GetADSLServiceTrafficCheckable();
                TrafficDropDownList.DataBind();
                TrafficDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                DurationDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationCheckable();
                DurationDropDownList.DataBind();
                DurationDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                AdditionalServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLAdditionalService();
                AdditionalServiceDropDownList.DataBind();
                AdditionalServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty, true));

                IPTypeDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.ADSLIPType));
                IPTypeDropDownList.DataBind();
                IPTypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                GroupIPTypeDropDownList.DataSource = Helper.GetEnumItem(typeof(DB.ADSLGroupIPBlockCount));
                GroupIPTypeDropDownList.DataBind();
                GroupIPTypeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }

            System.Data.DataTable telephoneInfo = _Service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

            if (telephoneInfo.Rows.Count != 0)
                _CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
            else
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(TelephoneNo);
                if (telephone != null)
                    _CenterID = telephone.CenterID;
            }
            _CityID = CityDB.GetCityByCenterID(_CenterID).ID;
            List<Data.ADSLPort> emptyPorts = ADSLPortDB.GetFreePortListbyeCenterID(_CenterID);

            string mDFDescription = ADSLMDFDB.GetMDFDescriptionbyTelephoneNo(TelephoneNo, _CenterID);
            if (!string.IsNullOrWhiteSpace(mDFDescription))
                MDFInfoLabel.Text = "این شماره تلفن بر روی ام دی اف، " + mDFDescription + " قرار دارد.";
            else
                MDFInfoLabel.Text = "در اطلاعات فنی رنجی برای این شماره تلفن تعیین نشده است !";

            _SellerAgentID = ADSLSellerGroupDB.GetADSLSellerAgentIDByUserID(DB.CurrentUser.ID);

            if (_RequestID == 0)
            {
                try
                {
                    ADSLOwnerStatusDropDownList.ClearSelection();
                    ADSLOwnerStatusDropDownList.Items.FindByValue(DB.ADSLOwnerStatus.Owner.ToString()).Selected = true;
                    ADSLOwnerStatusDropDownList_SelectedIndexChanged(null, null);
                }
                catch { }
                if (ADSLCustomer.ID != 0)
                {
                    ADSLCustomer = Data.CustomerDB.GetCustomerByID(ADSLCustomer.ID);
                    if (ADSLCustomer.NationalCodeOrRecordNo != null)
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                    CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
                    MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();
                }
                else
                {
                    ADSLCustomer.CustomerID = "000000000000000";
                    ADSLCustomer.FirstNameOrTitle = telephoneInfo.Rows[0]["FIRSTNAME"].ToString();
                    ADSLCustomer.LastName = telephoneInfo.Rows[0]["LASTNAME"].ToString();
                    ADSLCustomer.FatherName = telephoneInfo.Rows[0]["FATHERNAME"].ToString();
                    ADSLCustomer.NationalCodeOrRecordNo = telephoneInfo.Rows[0]["MELLICODE"].ToString();
                    ADSLCustomer.BirthCertificateID = telephoneInfo.Rows[0]["SHENASNAME"].ToString();
                    ADSLCustomer.MobileNo = telephoneInfo.Rows[0]["MOBILE"].ToString();
                    ADSLCustomer.Email = telephoneInfo.Rows[0]["EMAIL"].ToString();

                    ADSLCustomer.Detach();
                    DB.Save(ADSLCustomer, true);

                }

                NationalCodeTextBox.ReadOnly = true;
                SearchButton.Enabled = false;

                ADSLServiceDiv.Visible = false;
                ADSLServiceAdditionalDiv.Visible = false;
                ADSLIPDiv.Visible = false;
                ADSLFacilitiesDiv.Visible = false;

                if (emptyPorts.Count == 0)
                {
                    PortErrorLabel.Visible = true;
                    IsWaitingList = true;
                }

                if (_SellerAgentID != 0)
                {
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(_SellerAgentID);
                    CustomerGroupDropDownList.DataSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                    CustomerGroupDropDownList.DataBind();
                    CustomerGroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                }
            }
            else
            {
                _Request = Data.RequestDB.GetRequestByID(_RequestID);
                _ADSLRequest = ADSLRequestDB.GetADSLRequestByID(_RequestID);

                if (!string.IsNullOrEmpty(GroupIPTypeDropDownList.SelectedValue))
                    GroupIPStatic = ADSLIPDB.GetADSLGroupIPFree(Convert.ToInt32(GroupIPTypeDropDownList.SelectedValue), (int)_ADSLRequest.CustomerGroupID).FirstOrDefault();

                if (_SellerAgentID != 0)
                {
                    _ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(_SellerAgentID);
                    _ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(_SellerAgentID);
                }
                if (!IsPostBack)
                {
                    if (_SellerAgentID == 0)
                    {
                        ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroup((int)_ADSLRequest.CustomerGroupID);
                        ServiceDropDownList.DataBind();
                        ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        /*  اگر کد زیر از کامنت خارج شود ، با انتخاب یک آیتم از گروه و سپس انتخاب سرویس، فیلدهای سرویس بایند نمیشوند
                         ******************************************************************************************************************************/
                        GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupID((int)_ADSLRequest.CustomerGroupID);
                        GroupDropDownList.DataBind();
                        GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }
                    else
                    {
                        //_ServiceAccessList = ADSLServiceSellerDB.GetADSLServiceIDsbySellerID(_SellerAgentID);
                        //_ServiceGroupAccessList = ADSLServiceSellerDB.GetADSLServiceGroupIDsbySellerID(_SellerAgentID);

                        ServiceDropDownList.DataSource = ADSLServiceDB.GetAllowedADSLServicebyCustomerGroupAgent((int)_ADSLRequest.CustomerGroupID, _ServiceAccessList, _ServiceGroupAccessList);
                        ServiceDropDownList.DataBind();
                        ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        /*  اگر کد زیر از کامنت خارج شود ، با انتخاب یک آیتم از گروه و سپس انتخاب سرویس، فیلدهای سرویس بایند نمیشوند
                        ******************************************************************************************************************************/
                        GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebyCustomerGroupIDAgent((int)_ADSLRequest.CustomerGroupID, _ServiceGroupAccessList);
                        GroupDropDownList.DataBind();
                        GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        CustomerGroupDropDownList.DataSource = ADSLCustomerGroupDB.GetCustomerGroupbyServiceGroupList(_ServiceGroupAccessList);
                        CustomerGroupDropDownList.DataBind();
                        CustomerGroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                    }

                    if (_ADSLRequest.CustomerOwnerStatus.HasValue)
                    {
                        ADSLOwnerStatusDropDownList.ClearSelection();
                        ADSLOwnerStatusDropDownList.Items.FindByValue(_ADSLRequest.CustomerOwnerStatus.ToString()).Selected = true;
                        ADSLOwnerStatusDropDownList_SelectedIndexChanged(null, null);
                    }

                    if (ADSLCustomer.ID != 0)
                    {
                        ADSLCustomer = Data.CustomerDB.GetCustomerByID(_ADSLRequest.CustomerOwnerID);
                        if (ADSLCustomer.NationalCodeOrRecordNo != null)
                            NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo.ToString();
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + " " + ADSLCustomer.LastName;
                        if (!string.IsNullOrWhiteSpace(ADSLCustomer.MobileNo))
                            MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                    }

                    NationalCodeTextBox.ReadOnly = true;
                    SearchButton.Enabled = false;
                    CustomerEndRentDate.Text = _ADSLRequest.CustomerEndRentDate.HasValue ? _ADSLRequest.CustomerEndRentDate.ToString() : string.Empty;

                    if (_ADSLRequest.CustomerGroupID.HasValue)
                    {
                        CustomerGroupDropDownList.ClearSelection();
                        CustomerGroupDropDownList.Items.FindByValue(_ADSLRequest.CustomerGroupID.ToString()).Selected = true;
                    }

                    if (_ADSLRequest.JobGroupID.HasValue)
                    {
                        JobGroupDropDownList.ClearSelection();
                        JobGroupDropDownList.Items.FindByValue(_ADSLRequest.JobGroupID.ToString()).Selected = true;
                    }

                    ReagentTelephoneNoTextBox.Text = _ADSLRequest.ReagentTelephoneNo;

                    if (_ADSLRequest.ServiceID.HasValue)
                    {
                        ServiceDropDownList.ClearSelection();
                        ServiceDropDownList.Items.FindByValue(_ADSLRequest.ServiceID.ToString()).Selected = true;
                        ServiceDropDownList_SelectedIndexChanged(null, null);
                    }

                    ADSLService currentService = new ADSLService();
                    if (_ADSLRequest.ServiceID != null)
                        currentService = ADSLServiceDB.GetADSLServiceById((int)_ADSLRequest.ServiceID);

                    if (_ADSLRequest.AdditionalServiceID != null)
                    {
                        HasAdditionalTrafficCheckBox.Checked = true;
                        AdditionalTrafficInfoDiv.Visible = true;
                        if (_ADSLRequest.AdditionalServiceID.HasValue)
                        {
                            AdditionalServiceDropDownList.ClearSelection();
                            AdditionalServiceDropDownList.Items.FindByValue(_ADSLRequest.AdditionalServiceID.ToString()).Selected = true;
                            AdditionalServiceDropDownList_SelectedIndexChanged(null, null);
                        }
                    }

                    if (!_ADSLRequest.HasIP.HasValue)
                    {
                        HasIPStaticCheckBox.Checked = false;
                        HasIPStaticCheckBox_CheckedChanged(null, null);
                        try
                        {
                            IPTypeDropDownList.ClearSelection();
                        }
                        catch { }
                        IPTypeDropDownList.Enabled = false;
                    }
                    else
                    {
                        HasIPStaticCheckBox.Checked = _ADSLRequest.HasIP.HasValue ? _ADSLRequest.HasIP.Value : false;
                        HasIPStaticCheckBox_CheckedChanged(null, null);

                        if ((bool)_ADSLRequest.HasIP.Value)
                        {
                            if (_ADSLRequest.IPStaticID.HasValue)
                            {

                                IPStatic = ADSLIPDB.GetADSLIPById((long)_ADSLRequest.IPStaticID);
                                try
                                {
                                    IPTypeDropDownList.ClearSelection();
                                    IPTypeDropDownList.Items.FindByValue(((int)DB.ADSLIPType.Single).ToString()).Selected = true;
                                    IPTypeDropDownList_SelectedIndexChanged(null, null);
                                }
                                catch { }

                                SingleIPLabel.Visible = true;
                                SingleIPTextBox.Visible = true;
                                GroupIPTypeLabel.Visible = false;
                                GroupIPTypeDropDownList.Visible = false;
                                GroupIPLabel.Visible = false;
                                GroupIPTextBox.Visible = false;
                                IPCostDT.Visible = true;
                                IPCostDD.Visible = true;
                                IPTimeDT.Visible = true;
                                IPTimeDD.Visible = true;
                                IPTaxDT.Visible = true;
                                IPTaxDD.Visible = true;
                                IPSumCostDT.Visible = true;
                                IPSumCostDD.Visible = true;

                                SingleIPTextBox.Text = IPStatic.IP;
                                BaseCost cost = BaseCostDB.GetIPCostForADSL();

                                IPTimeDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)currentService.DurationID);
                                IPTimeDropDownList.DataBind();
                                IPTimeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                                if (_ADSLRequest.IPDuration.HasValue)
                                {
                                    IPCostTextBox.Text = (cost.Cost * _ADSLRequest.IPDuration).ToString() + " ریا ل";
                                    if (cost.Tax != null && cost.Tax != 0)
                                    {
                                        IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                        IPSumCostTextBox.Text = ((cost.Cost * _ADSLRequest.IPDuration) + (cost.Cost * _ADSLRequest.IPDuration * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                    }
                                    else
                                    {
                                        IPTaxTextBox.Text = "0 درصد";
                                        IPSumCostTextBox.Text = IPCostTextBox.Text;
                                    }
                                    try
                                    {
                                        IPTimeDropDownList.ClearSelection();
                                        IPTimeDropDownList.Items.FindByValue(_ADSLRequest.IPDuration.ToString()).Selected = true;
                                        IPTimeDropDownList_SelectedIndexChanged(null, null);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    if (currentService.DurationID != null)
                                    {
                                        IPCostTextBox.Text = (cost.Cost * currentService.DurationID).ToString() + " ریا ل";
                                        if (cost.Tax != null && cost.Tax != 0)
                                        {
                                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                            IPSumCostTextBox.Text = ((cost.Cost * currentService.DurationID) + (cost.Cost * currentService.DurationID * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                        }
                                        else
                                        {
                                            IPTaxTextBox.Text = "0 درصد";
                                            IPSumCostTextBox.Text = IPCostTextBox.Text;
                                        }
                                        try
                                        {
                                            IPTimeDropDownList.ClearSelection();
                                            IPTimeDropDownList.Items.FindByValue(currentService.DurationID.ToString()).Selected = true;
                                            IPTimeDropDownList_SelectedIndexChanged(null, null);
                                        }
                                        catch { }
                                    }
                                }
                            }
                            if (_ADSLRequest.GroupIPStaticID != null)
                            {
                                GroupIPStatic = ADSLIPDB.GetADSLGroupIPById((long)_ADSLRequest.GroupIPStaticID);
                                try
                                {
                                    IPTypeDropDownList.ClearSelection();
                                    IPTypeDropDownList.Items.FindByValue(((int)DB.ADSLIPType.Group).ToString()).Selected = true;
                                    IPTypeDropDownList_SelectedIndexChanged(null, null);
                                }
                                catch { }

                                try
                                {
                                    GroupIPTypeDropDownList.ClearSelection();
                                    GroupIPTypeDropDownList.Items.FindByValue(GroupIPStatic.BlockCount.ToString()).Selected = true;
                                    GroupIPTypeDropDownList_SelectedIndexChanged(null, null);
                                }
                                catch { }

                                SingleIPLabel.Visible = false;
                                SingleIPTextBox.Visible = false;
                                GroupIPTypeLabel.Visible = true;
                                GroupIPTypeDropDownList.Visible = true;
                                GroupIPLabel.Visible = true;
                                GroupIPTextBox.Visible = true;
                                IPCostDT.Visible = true;
                                IPCostDD.Visible = true;
                                IPTimeDT.Visible = true;
                                IPTimeDD.Visible = true;
                                IPTaxDT.Visible = true;
                                IPTaxDD.Visible = true;
                                IPSumCostDT.Visible = true;
                                IPSumCostDD.Visible = true;

                                GroupIPTextBox.Text = GroupIPStatic.StartRange;
                                BaseCost cost = BaseCostDB.GetIPCostForADSL();

                                if (currentService.DurationID.HasValue)
                                {
                                    IPTimeDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)currentService.DurationID);
                                    IPTimeDropDownList.DataBind();
                                    IPTimeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
                                }

                                if (_ADSLRequest.IPDuration.HasValue)
                                {
                                    IPCostTextBox.Text = (cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                                    if (cost.Tax != null && cost.Tax != 0)
                                    {
                                        IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                        IPSumCostTextBox.Text = ((cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount) + (cost.Cost * _ADSLRequest.IPDuration * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                    }
                                    else
                                    {
                                        IPTaxTextBox.Text = "0 درصد";
                                        IPSumCostTextBox.Text = IPCostTextBox.Text;
                                    }
                                    try
                                    {
                                        IPTimeDropDownList.ClearSelection();
                                        IPTimeDropDownList.Items.FindByValue(_ADSLRequest.IPDuration.ToString()).Selected = true;
                                        IPTimeDropDownList_SelectedIndexChanged(null, null);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    if (currentService.DurationID != null)
                                    {
                                        IPCostTextBox.Text = (cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount).ToString() + " ریا ل";
                                        if (cost.Tax != null && cost.Tax != 0)
                                        {
                                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                                            IPSumCostTextBox.Text = ((cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount) + (cost.Cost * currentService.DurationID * GroupIPStatic.BlockCount * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                                        }
                                        else
                                        {
                                            IPTaxTextBox.Text = "0 درصد";
                                            IPSumCostTextBox.Text = IPCostTextBox.Text;
                                        }
                                        try
                                        {
                                            IPTimeDropDownList.ClearSelection();
                                            IPTimeDropDownList.Items.FindByValue(currentService.DurationID.ToString()).Selected = true;
                                            IPTimeDropDownList_SelectedIndexChanged(null, null);
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                IPTypeDropDownList.ClearSelection();
                                //??  IPTypeDropDownList_SelectedIndexChanged(null, null);
                            }
                            catch { }
                            IPTypeDropDownList.Enabled = false;
                        }
                    }
                }

                LicenceLetterNoTextBox.Text = _ADSLRequest.LicenseLetterNo;
                if (!_ADSLRequest.RequiredInstalation.HasValue)
                    RequiredInstalationCheckBox.Checked = false;
                else
                    RequiredInstalationCheckBox.Checked = _ADSLRequest.RequiredInstalation.Value;

                if (_ADSLRequest.NeedModem == null)
                {
                    NeedModemCheckBox.Checked = false;

                    ModemTypeDT.Visible = false;
                    ModemTypeDD.Visible = false;
                    ModemCostDT.Visible = false;
                    ModemCostDD.Visible = false;
                    ModemSerialNoDT.Visible = false;
                    ModemSerialNoDD.Visible = false;
                    ModemMACAddressDT.Visible = false;
                    ModemMACAddressDD.Visible = false;
                }
                else
                {
                    NeedModemCheckBox.Checked = _ADSLRequest.NeedModem.HasValue ? _ADSLRequest.NeedModem.Value : false;

                    if ((bool)_ADSLRequest.NeedModem)
                    {
                        try
                        {
                            ModemTypeDropDownList.ClearSelection();
                            ModemTypeDropDownList.Items.FindByValue(_ADSLRequest.ModemID.ToString()).Selected = true;
                            ModemTypeDropDownList_SelectedIndexChanged(null, null);
                        }
                        catch { }

                        if (_ADSLRequest.ModemSerialNoID != null)
                            try
                            {
                                ModemSerialNoDropDownList.ClearSelection();
                                ModemSerialNoDropDownList.Items.FindByValue(_ADSLRequest.ModemSerialNoID.ToString()).Selected = true;
                                ModemSerialNoDropDownList_SelectedIndexChanged(null, null);
                            }
                            catch { }
                        ModemMACAddressTextBox.Text = _ADSLRequest.ModemMACAddress;

                        ModemTypeDT.Visible = true;
                        ModemTypeDD.Visible = true;
                        ModemCostDT.Visible = true;
                        ModemCostDD.Visible = true;
                        ModemSerialNoDT.Visible = true;
                        ModemSerialNoDD.Visible = true;
                        ModemMACAddressDT.Visible = true;
                        ModemMACAddressDD.Visible = true;
                    }
                    else
                    {
                        ModemTypeDT.Visible = false;
                        ModemTypeDD.Visible = false;
                        ModemCostDT.Visible = false;
                        ModemCostDD.Visible = false;
                        ModemSerialNoDT.Visible = false;
                        ModemSerialNoDD.Visible = false;
                        ModemMACAddressDT.Visible = false;
                        ModemMACAddressDD.Visible = false;
                    }
                }
                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID))
                {
                    ADSLServiceDiv.Visible = false;
                    ADSLServiceAdditionalDiv.Visible = false;
                    ADSLIPDiv.Visible = false;
                    ADSLFacilitiesDiv.Visible = false;

                    if (emptyPorts.Count == 0)
                    {
                        PortErrorLabel.Visible = true;
                        IsWaitingList = true;
                    }
                }

                if ((_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Start).ID) && (_Request.PreviousAction == (byte)DB.Action.Reject))
                {
                    CommentsDiv.Visible = true;
                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentCustomersTextBox.ForeColor = System.Drawing.Color.Red;
                    CommentCustomersTextBox.ReadOnly = true;
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Confirm).ID)
                {
                    ADSLOwnerStatusDropDownList.Enabled = false;
                    NationalCodeTextBox.ReadOnly = true;
                    CustomerNameTextBox.ReadOnly = true;
                    //MobileNoTextBox.ReadOnly = true;
                    SearchButton.Visible = false;
                    CustomerGroupDropDownList.Enabled = false;
                    JobGroupDropDownList.Enabled = false;
                    ReagentTelephoneNoTextBox.ReadOnly = true;

                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentsDiv.Visible = true;

                    if (emptyPorts.Count == 0)
                    {
                        PortErrorLabel.Visible = true;
                        IsWaitingList = true;
                    }
                }

                if (_Request.StatusID == DB.GetStatus(_Request.RequestTypeID, (int)DB.RequestStatusType.Observation).ID)
                {
                    DisableControls();

                    CommentCustomersTextBox.Text = _ADSLRequest.CommentCustomers;
                    CommentCustomersTextBox.ReadOnly = true;
                    CommentsDiv.Visible = true;

                    ADSLPortInfo adslPortInfo = ADSLPortDB.GetADSlPortInfoByID((long)_ADSLRequest.ADSLPortID);
                    //EquipmentTextBox.Text = adslPortInfo.EquipmentName;
                    //PortNoTextBox.Text = adslPortInfo.PortNo;
                    //InputBuchtTextBox.Text = adslPortInfo.InputBuchtConnection;
                    //OutputBuchtTextBox.Text = adslPortInfo.OutputBuchtConnection;

                    ADSLPortDiv.Visible = true;
                }
            }

            Service1 aDSLService = new Service1();
            if (aDSLService.Phone_Is_PCM(_TelephoneNo.ToString()))
                PCMErrorrLabel.Visible = true;
        }

        #endregion

        #region Event Handlers

        protected void Page_Load(object sender, EventArgs e)
        {
            //  LoadData();
        }

        protected void ADSLOwnerStatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ADSLOwnerStatusDropDownList.SelectedValue))
            {
                if (byte.Parse(ADSLOwnerStatusDropDownList.SelectedValue) == (byte)DB.ADSLOwnerStatus.Owner)
                {
                    System.Data.DataTable telephoneInfo = _Service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                    NationalCodeTextBox.Text = telephoneInfo.Rows[0]["MelliCode"].ToString();
                    CustomerNameTextBox.Text = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                    MobileNoTextBox.Text = telephoneInfo.Rows[0]["MOBILE"].ToString();

                    NationalCodeTextBox.ReadOnly = true;
                    SearchButton.Enabled = false;

                    CustomerEndRentDateDT.Visible = false;
                    CustomerEndRentDateDD.Visible = false;
                }
                else
                {
                    NationalCodeTextBox.Text = string.Empty;
                    CustomerNameTextBox.Text = string.Empty;
                    MobileNoTextBox.Text = string.Empty;
                    //CustomerEndRentDate.Text = string.Empty;

                    NationalCodeTextBox.ReadOnly = false;
                    SearchButton.Enabled = true;

                    //CustomerEndRentDateDT.Visible = true;
                    //CustomerEndRentDateDD.Visible = true;

                    if (byte.Parse(ADSLOwnerStatusDropDownList.SelectedValue) == (byte)DB.ADSLOwnerStatus.Tenant)
                    {
                        CustomerEndRentDate.Text = string.Empty;
                        CustomerEndRentDateDT.Visible = true;
                        CustomerEndRentDateDD.Visible = true;
                    }
                    else
                    {
                        CustomerEndRentDate.Text = string.Empty;
                        CustomerEndRentDateDT.Visible = false;
                        CustomerEndRentDateDD.Visible = false;
                    }
                }
            }
        }


        protected void SearchServiceButton_Click(object sender, EventArgs e)
        {
            ServiceDropDownList.ClearSelection();
            MainADSLErrorCreditLabel.Text = string.Empty;

            BandWidthTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            TaxTextBox.Text = string.Empty;
            ADSLServiceTrafficTextBox.Text = string.Empty;
            AbonmanTextBox.Text = string.Empty;
            ServiceSumPriceTextBox.Text = string.Empty;

            int typeID = -1;
            if (!string.IsNullOrEmpty(TypeDropDownList.SelectedValue))
            {
                typeID = Convert.ToInt32(TypeDropDownList.SelectedValue);

                //GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
                //GroupDropDownList.DataBind();
                //GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }

            int groupID = -1;
            if (!string.IsNullOrEmpty(GroupDropDownList.SelectedValue))
                groupID = Convert.ToInt32(GroupDropDownList.SelectedValue);

            int bandWidthID = -1;
            if (!string.IsNullOrEmpty(BandWidthDropDownList.SelectedValue))
                bandWidthID = Convert.ToInt32(BandWidthDropDownList.SelectedValue);

            int durationID = -1;
            if (!string.IsNullOrEmpty(DurationDropDownList.SelectedValue))
                durationID = Convert.ToInt32(DurationDropDownList.SelectedValue);

            int trafficID = -1;
            if (!string.IsNullOrEmpty(TrafficDropDownList.SelectedValue))
                trafficID = Convert.ToInt32(TrafficDropDownList.SelectedValue);

            if (_SellerAgentID == 0)
            {
                ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID);
                ServiceDropDownList.DataBind();
                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
            else
            {
                ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID, _ServiceAccessList, _ServiceGroupAccessList);
                ServiceDropDownList.DataBind();
                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
           //88 ADSLServiceUpdatePanel.Update();
            
        }

        protected void MultipleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceDropDownList.ClearSelection();

            BandWidthTextBox.Text = string.Empty;
            DurationTextBox.Text = string.Empty;
            PriceTextBox.Text = string.Empty;
            TaxTextBox.Text = string.Empty;
            ADSLServiceTrafficTextBox.Text = string.Empty;
            AbonmanTextBox.Text = string.Empty;
            ServiceSumPriceTextBox.Text = string.Empty;

            int typeID = -1;
            if (!string.IsNullOrEmpty(TypeDropDownList.SelectedValue))
            {
                typeID = Convert.ToInt32(TypeDropDownList.SelectedValue);

                //GroupDropDownList.DataSource = ADSLServiceDB.GetADSLServiceGroupCheckablebytypeID(typeID);
                //GroupDropDownList.DataBind();
                //GroupDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }

            int groupID = -1;
            if (!string.IsNullOrEmpty(GroupDropDownList.SelectedValue))
                groupID = Convert.ToInt32(GroupDropDownList.SelectedValue);

            int bandWidthID = -1;
            if (!string.IsNullOrEmpty(BandWidthDropDownList.SelectedValue))
                bandWidthID = Convert.ToInt32(BandWidthDropDownList.SelectedValue);

            int durationID = -1;
            if (!string.IsNullOrEmpty(DurationDropDownList.SelectedValue))
                durationID = Convert.ToInt32(DurationDropDownList.SelectedValue);

            int trafficID = -1;
            if (!string.IsNullOrEmpty(TrafficDropDownList.SelectedValue))
                trafficID = Convert.ToInt32(TrafficDropDownList.SelectedValue);

            if (_SellerAgentID == 0)
            {
                ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIds((int)_ADSLRequest.CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID);
                ServiceDropDownList.DataBind();
                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
            else
            {
                ServiceDropDownList.DataSource = ADSLServiceDB.GetADSLServiceByPropertiesIdsAgent((int)_ADSLRequest.CustomerGroupID, typeID <= 0 ? -1 : typeID, groupID <= 0 ? -1 : groupID, bandWidthID <= 0 ? -1 : bandWidthID, trafficID <= 0 ? -1 : trafficID, durationID <= 0 ? -1 : durationID, _ServiceAccessList, _ServiceGroupAccessList);
                ServiceDropDownList.DataBind();
                ServiceDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
           //88 ADSLServiceUpdatePanel.Update();
 
        }

        protected void HasAdditionalTrafficCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (HasAdditionalTrafficCheckBox.Checked)
            {
                AdditionalTrafficInfoDiv.Visible = true;
            }
            else if (!HasAdditionalTrafficCheckBox.Checked)
            {
                AdditionalTrafficInfoDiv.Visible = false;

                AdditionalServiceDropDownList.ClearSelection();
                AdditionalServiceDropDownList_SelectedIndexChanged(null, null);

                TrafficTextBox.Text = string.Empty;
                AdditionalDurationTextBox.Text = string.Empty;
                AdditionalPriceTextBox.Text = string.Empty;
                AdditionalTaxTextBox.Text = string.Empty;
                PriceSumTextBox.Text = string.Empty;

                _ADSLRequest.AdditionalServiceID = null;
            }
        }

        protected void AdditionalServiceDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADSLServiceInfo additionalServiceInfo = null;
            try
            {
                if (!string.IsNullOrEmpty(AdditionalServiceDropDownList.SelectedValue))
                    additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById(int.Parse(AdditionalServiceDropDownList.SelectedValue));
                else
                {
                    if (_ADSLRequest.AdditionalServiceID != 0 && _ADSLRequest.AdditionalServiceID != null)
                        additionalServiceInfo = ADSLServiceDB.GetADSLAdditionalTrafficInfoById((int)_ADSLRequest.AdditionalServiceID);
                }

                if (additionalServiceInfo != null)
                {
                    SumPriceTraffic = Convert.ToInt64(additionalServiceInfo.PriceSum.Split(' ')[0]);
                    ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                    if (user != null)
                    {
                        long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                        if (creditAgentRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                        {
                            TrafficTextBox.Text = string.Empty;
                            AdditionalDurationTextBox.Text = string.Empty;
                            AdditionalPriceTextBox.Text = string.Empty;
                            AdditionalTaxTextBox.Text = string.Empty;
                            PriceSumTextBox.Text = string.Empty;

                            _HasCreditAgent = false;
                            ADSLServiceAdditionalErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                            ADSLServiceAdditionalUpdatePanel.Update();
                            return;
                        }

                        long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                        if (creditUserRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                        {
                            TrafficTextBox.Text = string.Empty;
                            AdditionalDurationTextBox.Text = string.Empty;
                            AdditionalPriceTextBox.Text = string.Empty;
                            AdditionalTaxTextBox.Text = string.Empty;
                            PriceSumTextBox.Text = string.Empty;

                            _HasCreditUser = false;
                            ADSLServiceAdditionalErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";
                            ADSLServiceAdditionalUpdatePanel.Update();
                            return;
                        }
                    }

                    ADSLServiceAdditionalErrorCreditLabel.Text = string.Empty;
                    _HasCreditAgent = true;
                    _HasCreditUser = true;

                    TrafficTextBox.Text = additionalServiceInfo.Traffic;
                    AdditionalDurationTextBox.Text = additionalServiceInfo.Duration;
                    AdditionalPriceTextBox.Text = additionalServiceInfo.Price;
                    AdditionalTaxTextBox.Text = additionalServiceInfo.Tax;
                    PriceSumTextBox.Text = additionalServiceInfo.PriceSum;

                    switch (additionalServiceInfo.IsRequiredLicense)
                    {
                        case true:
                            AdditionalLicenceLetterNoDT.Visible = true;
                            AdditionalLicenceLetterNoDD.Visible = true;
                            break;

                        case false:
                        case null:
                            AdditionalLicenceLetterNoDT.Visible = false;
                            AdditionalLicenceLetterNoDD.Visible = false;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    TrafficTextBox.Text = string.Empty;
                    AdditionalDurationTextBox.Text = string.Empty;
                    AdditionalPriceTextBox.Text = string.Empty;
                    AdditionalTaxTextBox.Text = string.Empty;
                    PriceSumTextBox.Text = string.Empty;
                }
                ADSLServiceAdditionalUpdatePanel.Update();
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("\'", "");
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myalert", "alert('خطا : " + message + "');", true);
            }

        }

        protected void HasIPStaticCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ErrorGroupIPLabel.Visible = false;

            if (HasIPStaticCheckBox.Checked)
            {
                try
                {
                    if (string.IsNullOrEmpty(ServiceDropDownList.SelectedValue) && IsPostBack)
                    {
                        HasIPStaticCheckBox.Checked = false;
                        throw new Exception("لطفا ابتدا سرویس مورد نظر را انتخاب نمایید");
                    }
                    IPTypeDropDownList.Enabled = true;
                }
                catch (Exception ex)
                {
                    string message = ex.Message.Replace("\'", "");
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "myalert", "alert('خطا : " + message + "');", true);
                }
            }
            else if (!HasIPStaticCheckBox.Checked)
            {
                IPTypeDropDownList.Enabled = false;
                IPTypeDropDownList.ClearSelection();

                SingleIPLabel.Visible = false;
                SingleIPTextBox.Visible = false;
                GroupIPTypeLabel.Visible = false;
                GroupIPTypeDropDownList.Visible = false;
                GroupIPLabel.Visible = false;
                GroupIPTextBox.Visible = false;
                IPCostDT.Visible = false;
                IPCostDD.Visible = false;
                IPTimeDT.Visible = false;
                IPTimeDD.Visible = false;
                IPTaxDT.Visible = false;
                IPTaxDD.Visible = false;
                IPSumCostDT.Visible = false;
                IPSumCostDD.Visible = false;
                IPDiscountDT.Visible = false;
                IPDiscountDD.Visible = false;
                IPCostDiscountDT.Visible = false;
                IPCostDiscountDD.Visible = false;

                //SingleIPLabel.Style.Add("display", "none");
            }
        }

        protected void IPTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ServiceDropDownList.SelectedIndex > 0)
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById(int.Parse(ServiceDropDownList.SelectedValue));
            else if (!IsPostBack)
            {
                if (_ADSLRequest.ServiceID.HasValue && _ADSLRequest.ServiceID != 0)
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);
            }

            if (_ServiceInfo == null) 
                return;

            if (!string.IsNullOrEmpty(IPTypeDropDownList.SelectedValue))
                if (Convert.ToInt16(IPTypeDropDownList.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    ErrorGroupIPLabel.Visible = false;
                    SingleIPLabel.Visible = true;
                    SingleIPTextBox.Visible = true;
                    GroupIPTypeLabel.Visible = false;
                    GroupIPTypeDropDownList.Visible = false;
                    GroupIPLabel.Visible = false;
                    GroupIPTextBox.Visible = false;
                    IPCostDT.Visible = true;
                    IPCostDD.Visible = true;
                    IPTimeDT.Visible = true;
                    IPTimeDD.Visible = true;
                    IPTaxDT.Visible = true;
                    IPTaxDD.Visible = true;
                    IPSumCostDT.Visible = true;
                    IPSumCostDD.Visible = true;
                    IPDiscountDT.Visible = false;
                    IPDiscountDD.Visible = false;
                    IPCostDiscountDT.Visible = false;
                    IPCostDiscountDD.Visible = false;

                    IPStatic = ADSLIPDB.GetADSLIPFree((int)_ADSLRequest.CustomerGroupID).FirstOrDefault();

                    if (IPStatic != null)
                    {
                        SingleIPTextBox.Text = IPStatic.IP;
                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        long baseCost = cost.Cost * Convert.ToInt64(_ServiceInfo.Duration.Split(' ')[0]);
                        IPCostTextBox.Text = baseCost.ToString() + " ریا ل";

                        if (_ServiceInfo.IPDiscount != 0)
                        {
                            IPDiscountDT.Visible = true;
                            IPDiscountDD.Visible = true;
                            IPCostDiscountDT.Visible = true;
                            IPCostDiscountDD.Visible = true;

                            IPDiscountTextBox.Text = _ServiceInfo.IPDiscount.ToString() + " درصد";
                            IPCostDiscountTextBox.Text = (baseCost - (baseCost * _ServiceInfo.IPDiscount * 0.01)).ToString() + " ریا ل";
                            baseCost = baseCost - Convert.ToInt64(baseCost * _ServiceInfo.IPDiscount * 0.01);
                        }

                        if (cost.Tax != null && cost.Tax != 0)
                        {
                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                            IPSumCostTextBox.Text = (baseCost + (baseCost * (int)cost.Tax * 0.01)).ToString() + " ریا ل";
                        }
                        else
                        {
                            IPTaxTextBox.Text = "0 درصد";
                            IPSumCostTextBox.Text = baseCost.ToString() + " ریا ل";
                        }

                        IPTimeDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)_ServiceInfo.DurationID);
                        IPTimeDropDownList.DataBind();
                        IPTimeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        try
                        {
                            IPTimeDropDownList.ClearSelection();
                            IPTimeDropDownList.Items.FindByValue(_ServiceInfo.DurationID.ToString()).Selected = true;
                            IPTimeDropDownList_SelectedIndexChanged(null, null);
                        }
                        catch { }

                        if (!string.IsNullOrEmpty(IPTimeDropDownList.SelectedValue))
                            SumPriceIP = cost.Cost * Convert.ToInt64(IPTimeDropDownList.SelectedValue);

                        //ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID((int)_Request.CreatorUserID);
                        ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                        if (user != null)
                        {
                            long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                            if (creditAgentRemain <= (SumPriceService + SumPriceIP + SumPriceModem + SumPriceTraffic + 100000))
                            {
                                SingleIPTextBox.Text = string.Empty;
                                IPCostTextBox.Text = string.Empty;
                                IPDiscountTextBox.Text = string.Empty;
                                IPCostDiscountTextBox.Text = string.Empty;
                                IPTaxTextBox.Text = string.Empty;
                                IPSumCostTextBox.Text = string.Empty;
                                IPTimeDropDownList.Items.Clear();
                                _HasCreditAgent = false;

                                ADSLIPErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                                ADSLIPUpdatePanel.Update();
                                return;
                            }

                            long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                            if (creditUserRemain <= (SumPriceService + SumPriceIP + SumPriceModem + SumPriceTraffic + 100000))
                            {
                                SingleIPTextBox.Text = string.Empty;
                                IPCostTextBox.Text = string.Empty;
                                IPDiscountTextBox.Text = string.Empty;
                                IPCostDiscountTextBox.Text = string.Empty;
                                IPTaxTextBox.Text = string.Empty;
                                IPSumCostTextBox.Text = string.Empty;
                                IPTimeDropDownList.Items.Clear();
                                _HasCreditUser = false;

                                ADSLIPErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";
                                ADSLIPUpdatePanel.Update();
                                return;
                            }

                        }
                        ADSLIPErrorCreditLabel.Text = string.Empty;
                        _HasCreditAgent = true;
                        _HasCreditUser = true;
                    }
                    else
                        SingleIPTextBox.Text = "IP تکی خالی موجود نمی باشد.";
                }
                else
                {
                    ErrorGroupIPLabel.Visible = false;
                    SingleIPLabel.Visible = false;
                    SingleIPTextBox.Visible = false;
                    GroupIPTypeLabel.Visible = true;
                    GroupIPTypeDropDownList.Visible = true;
                    GroupIPLabel.Visible = true;
                    GroupIPTextBox.Visible = true;
                    IPCostDT.Visible = true;
                    IPCostDD.Visible = true;
                    IPTimeDT.Visible = true;
                    IPTimeDD.Visible = true;
                    IPTaxDT.Visible = true;
                    IPTaxDD.Visible = true;
                    IPSumCostDT.Visible = true;
                    IPSumCostDD.Visible = true;

                    if (_ServiceInfo.IPDiscount != 0)
                    {
                        IPDiscountDT.Visible = true;
                        IPDiscountDD.Visible = true;
                        IPCostDiscountDT.Visible = true;
                        IPCostDiscountDD.Visible = true;
                    }

                    GroupIPTypeDropDownList.ClearSelection();
                    GroupIPTextBox.Text = string.Empty;
                    IPCostTextBox.Text = string.Empty;
                    IPTimeDropDownList.ClearSelection();
                    IPTaxTextBox.Text = string.Empty;
                    IPSumCostTextBox.Text = string.Empty;
                    IPDiscountTextBox.Text = string.Empty;
                    IPCostDiscountTextBox.Text = string.Empty;

                }
            ADSLIPUpdatePanel.Update();
        }

        protected void GroupIPTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(GroupIPTypeDropDownList.SelectedValue))
            {
                GroupIPStatic = ADSLIPDB.GetADSLGroupIPFree(Convert.ToInt32(GroupIPTypeDropDownList.SelectedValue), (int)_ADSLRequest.CustomerGroupID).FirstOrDefault();
                if (GroupIPStatic != null)
                {
                    GroupIPLabel.Visible = true;
                    GroupIPTextBox.Visible = true;
                    ErrorGroupIPLabel.Visible = false;

                    BaseCost cost = BaseCostDB.GetIPCostForADSL();
                    if (_ServiceInfo.DurationID != null)
                        SumPriceIP = cost.Cost * GroupIPStatic.BlockCount * _ServiceInfo.DurationID;

                    ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);
                    if (user != null)
                    {
                        long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                        if (creditAgentRemain <= (SumPriceService + SumPriceIP + SumPriceModem + SumPriceTraffic + 100000))
                        {
                            GroupIPLabel.Visible = false;
                            GroupIPTextBox.Visible = false;
                            IPCostTextBox.Text = string.Empty;
                            IPTimeDropDownList.ClearSelection();
                            IPTaxTextBox.Text = string.Empty;
                            IPSumCostTextBox.Text = string.Empty;
                            _HasCreditAgent = false;

                            ADSLIPErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                            ADSLIPUpdatePanel.Update();

                            return;
                        }

                        long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                        if (creditUserRemain <= (SumPriceService + SumPriceIP + SumPriceModem + SumPriceTraffic + 100000))
                        {
                            GroupIPLabel.Visible = false;
                            GroupIPTextBox.Visible = false;
                            IPCostTextBox.Text = string.Empty;
                            IPTimeDropDownList.ClearSelection();
                            IPTaxTextBox.Text = string.Empty;
                            IPSumCostTextBox.Text = string.Empty;
                            _HasCreditUser = false;

                            ADSLIPErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";
                            ADSLIPUpdatePanel.Update();

                            return;
                        }
                    }

                    ADSLIPErrorCreditLabel.Text = string.Empty;
                    _HasCreditAgent = true;
                    _HasCreditUser = true;
                    GroupIPTextBox.Text = GroupIPStatic.StartRange;

                    if (_ServiceInfo.DurationID != null)
                    {
                        IPTimeDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)_ServiceInfo.DurationID);
                        IPTimeDropDownList.DataBind();
                        IPTimeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                        long baseCost = cost.Cost * GroupIPStatic.BlockCount * _ServiceInfo.DurationID;
                        IPCostTextBox.Text = baseCost.ToString() + " ریا ل";

                        if (_ServiceInfo.IPDiscount != 0)
                        {
                            IPDiscountDT.Visible = true;
                            IPDiscountDD.Visible = true;
                            IPCostDiscountDT.Visible = true;
                            IPCostDiscountDD.Visible = true;

                            IPDiscountTextBox.Text = _ServiceInfo.IPDiscount.ToString() + " درصد";
                            IPCostDiscountTextBox.Text = (baseCost - (baseCost * _ServiceInfo.IPDiscount * 0.01)).ToString() + " ریا ل";
                            baseCost = baseCost - Convert.ToInt64(baseCost * _ServiceInfo.IPDiscount * 0.01);
                        }

                        if (cost.Tax != null && cost.Tax != 0)
                        {
                            IPTaxTextBox.Text = cost.Tax + " درصد";
                            IPSumCostTextBox.Text = (baseCost + (baseCost * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                        }
                        else
                        {
                            IPTaxTextBox.Text = "0 درصد";
                            IPSumCostTextBox.Text = IPCostTextBox.Text;
                        }

                        try
                        {
                            IPTimeDropDownList.ClearSelection();
                            IPTimeDropDownList.Items.FindByValue(_ServiceInfo.DurationID.ToString()).Selected = true;
                            IPTimeDropDownList_SelectedIndexChanged(null, null);
                        }
                        catch { }

                    }

                }
                else
                {
                    GroupIPLabel.Visible = false;
                    GroupIPTextBox.Visible = false;
                    ErrorGroupIPLabel.Visible = true;

                    IPCostTextBox.Text = string.Empty;
                    IPTimeDropDownList.ClearSelection();
                    IPTaxTextBox.Text = string.Empty;
                    IPSumCostTextBox.Text = string.Empty;
                }
                ADSLIPUpdatePanel.Update();
            }
        }

        protected void IPTimeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IPTimeDropDownList.SelectedValue))
            {
                BaseCost cost = BaseCostDB.GetIPCostForADSL();
                if (Convert.ToInt16(IPTypeDropDownList.SelectedValue) == (byte)DB.ADSLIPType.Single)
                {
                    IPCostTextBox.Text = (cost.Cost * Convert.ToInt64(IPTimeDropDownList.SelectedValue)).ToString() + " ریا ل";
                    IPSumCostTextBox.Text = ((cost.Cost * Convert.ToInt32(IPTimeDropDownList.SelectedValue)) + ((cost.Cost * Convert.ToInt32(IPTimeDropDownList.SelectedValue)) * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                }
                if (Convert.ToInt16(IPTypeDropDownList.SelectedValue) == (byte)DB.ADSLIPType.Group)
                {
                    IPCostTextBox.Text = (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeDropDownList.SelectedValue)).ToString() + " ریا ل";
                    IPSumCostTextBox.Text = ((cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt32(IPTimeDropDownList.SelectedValue)) + (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt32(IPTimeDropDownList.SelectedValue) * ((int)cost.Tax * 0.01))).ToString() + " ریا ل";
                }
            }
        }

        protected void NeedModemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (NeedModemCheckBox.Checked)
            {
                ModemTypeDT.Visible = true;
                ModemTypeDD.Visible = true;
                ModemCostDT.Visible = true;
                ModemCostDD.Visible = true;
                ModemSerialNoDT.Visible = true;
                ModemSerialNoDD.Visible = true;
                ModemMACAddressDT.Visible = true;
                ModemMACAddressDD.Visible = true;

                ModemTypeDropDownList.ClearSelection();
                ModemSerialNoDropDownList.ClearSelection();
                ModemSerialNoDropDownList.ClearSelection();
                ModemCostTextBox.Text = string.Empty;
            }
            else if (!NeedModemCheckBox.Checked)
            {

                ModemTypeDT.Visible = false;
                ModemTypeDD.Visible = false;
                ModemCostDT.Visible = false;
                ModemCostDD.Visible = false;
                ModemSerialNoDT.Visible = false;
                ModemSerialNoDD.Visible = false;
                ModemMACAddressDT.Visible = false;
                ModemMACAddressDD.Visible = false;

                ModemTypeDropDownList.ClearSelection();
                ModemSerialNoDropDownList.ClearSelection();
                ModemSerialNoDropDownList.ClearSelection();
                ModemCostTextBox.Text = string.Empty;

                SumPriceModem = 0;
            }
        }

        protected void ModemTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ADSLModem modem = null;
            if (!string.IsNullOrEmpty(ModemTypeDropDownList.SelectedValue))
            {
                modem = ADSLModemDB.GetADSLModemById(int.Parse(ModemTypeDropDownList.SelectedValue));

                ModemSerialNoDropDownList.DataSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);
                ModemSerialNoDropDownList.DataBind();
                ModemSerialNoDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                if (modem != null)
                {
                    ModemCostTextBox.Text = modem.Price + " ریا ل";
                    if (_ServiceInfo.ModemDiscount != 0)
                    {
                        ModemDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + " درصد";
                        ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * _ServiceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                    }
                }
            }
            else if (_ADSLRequest.ModemID.HasValue)
            {
                modem = ADSLModemDB.GetADSLModemById((int)_ADSLRequest.ModemID);
                ModemSerialNoDropDownList.DataSource = ADSLModemPropertyDB.GetADSLModemPropertiesList(modem.ID, _CityID);
                ModemSerialNoDropDownList.DataBind();
                ModemSerialNoDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));

                if (modem != null)
                {
                    ModemCostTextBox.Text = modem.Price + " ریا ل";
                    if (_ServiceInfo.ModemDiscount != 0)
                    {
                        ModemDiscountTextBox.Text = _ServiceInfo.ModemDiscount.ToString() + " درصد";
                        ModemCostDiscountTextBox.Text = (modem.Price - (modem.Price * _ServiceInfo.ModemDiscount * 0.01)).ToString() + " ریا ل";
                    }
                }
            }

            SumPriceModem = Convert.ToInt64(modem.Price);
            ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

            if (user != null)
            {
                long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                if (creditAgentRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                {
                    ModemTypeDropDownList.ClearSelection();
                    ModemSerialNoDropDownList.Items.Clear();
                    ModemCostTextBox.Text = string.Empty;
                    _HasCreditAgent = false;

                    ADSLFacilitiesErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                    ADSLFacilitiesUpdatePanel.Update();
                    return;
                }

                long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                if (creditUserRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                {
                    ModemTypeDropDownList.ClearSelection();
                    ModemSerialNoDropDownList.Items.Clear();
                    ModemCostTextBox.Text = string.Empty;
                    _HasCreditUser = false;

                    ADSLFacilitiesErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";
                    ADSLFacilitiesUpdatePanel.Update();
                    return;
                }
            }

            ADSLFacilitiesErrorCreditLabel.Text = string.Empty;
            _HasCreditAgent = true;
            _HasCreditUser = true;
            ADSLFacilitiesUpdatePanel.Update();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NationalCodeTextBox.Text.Trim()))
            {
                CustomerNameTextBox.Text = string.Empty;
                MobileNoTextBox.Text = string.Empty;

                ADSLCustomer = null;
                ADSLCustomerList = null;
                ADSLCustomerList = CustomerDB.GetCustomerListByNationalCode(NationalCodeTextBox.Text.Trim());

                if (ADSLCustomerList != null)
                {
                    if (ADSLCustomerList.Count == 1)
                    {
                        NationalCodeTextBox.Text = string.Empty;
                        NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                        CustomerNameTextBox.Text = string.Empty;
                        CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                        MobileNoTextBox.Text = string.Empty;
                        MobileNoTextBox.Text = ADSLCustomer.MobileNo;
                    }
                    else
                        ScriptManager.RegisterStartupScript(ADSLCustomerUpdatePanel, ADSLCustomerUpdatePanel.GetType(), "OpenSearchCustomerForm", string.Format("ShowSearchCustomer({0});", NationalCodeTextBox.Text.Trim()), true);
                }
                else
                    ScriptManager.RegisterStartupScript(ADSLCustomerUpdatePanel, ADSLCustomerUpdatePanel.GetType(), "OpenSearchCustomerForm", "ShowSearchCustomer();", true);
            }
            else
                ScriptManager.RegisterStartupScript(ADSLCustomerUpdatePanel, ADSLCustomerUpdatePanel.GetType(), "OpenSearchCustomerForm", "ShowSearchCustomer();", true);
        }

        protected void ServiceDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ServiceInfo = null;
            MainADSLErrorCreditLabel.Text = string.Empty;

            if (ServiceDropDownList.SelectedIndex > 0)
                _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById(int.Parse(ServiceDropDownList.SelectedValue));
            else if (!IsPostBack)
            {
                if (_ADSLRequest.ServiceID.HasValue && _ADSLRequest.ServiceID != 0)
                    _ServiceInfo = ADSLServiceDB.GetADSLServiceInfoById((int)_ADSLRequest.ServiceID);
            }

            if (_ServiceInfo != null)
            {
                ADSLSellerAgentUserInfo user = ADSLSellerGroupDB.GetSellerAgentUserByID(DB.CurrentUser.ID);

                if (user != null && _ServiceInfo.IsInstalment == false)
                {
                    SumPriceService = Convert.ToInt64(_ServiceInfo.PriceSum.Split(' ')[0]);

                    long creditAgentRemain = ADSLSellerGroupDB.GetADSLSellerAgentCreditRemainbyUserID(user.ID);
                    if (creditAgentRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                    {
                        BandWidthTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PriceTextBox.Text = string.Empty;
                        TaxTextBox.Text = string.Empty;
                        ADSLServiceTrafficTextBox.Text = string.Empty;
                        AbonmanTextBox.Text = string.Empty;
                        ServiceSumPriceTextBox.Text = string.Empty;

                        _HasCreditAgent = false;

                        MainADSLErrorCreditLabel.Text = "اعتبار نمایندگی شما تمام شده است!";
                      //88  ADSLServiceUpdatePanel.Update();
                        return;
                    }

                    long creditUserRemain = ADSLSellerGroupDB.GetADSLSellerAgentUserCreditRemainbyUserID(user.ID);
                    if (creditUserRemain <= (SumPriceService + SumPriceModem + SumPriceIP + SumPriceTraffic + 100000))
                    {
                        BandWidthTextBox.Text = string.Empty;
                        DurationTextBox.Text = string.Empty;
                        PriceTextBox.Text = string.Empty;
                        TaxTextBox.Text = string.Empty;
                        ADSLServiceTrafficTextBox.Text = string.Empty;
                        AbonmanTextBox.Text = string.Empty;
                        ServiceSumPriceTextBox.Text = string.Empty;

                        _HasCreditUser = false;
                        MainADSLErrorCreditLabel.Text = "اعتبار کاربری شما تمام شده است!";
                      //88  ADSLServiceUpdatePanel.Update();
                        return;
                    }
                }

                if (_ServiceInfo.IsInstalment == true)
                    SumPriceService = 0;

                MainADSLErrorCreditLabel.Text = string.Empty;
                _HasCreditAgent = true;
                _HasCreditUser = true;

                BandWidthTextBox.Text = _ServiceInfo.BandWidth;
                DurationTextBox.Text = _ServiceInfo.Duration;
                PriceTextBox.Text = _ServiceInfo.Price;
                TaxTextBox.Text = _ServiceInfo.Tax;
                ADSLServiceTrafficTextBox.Text = _ServiceInfo.Traffic;
                AbonmanTextBox.Text = _ServiceInfo.Abonman;
                ServiceSumPriceTextBox.Text = _ServiceInfo.PriceSum;

                switch (_ServiceInfo.IsRequiredLicense)
                {
                    case true:
                        LicenceLetterNoDT.Visible = true;
                        LicenceLetterNoDD.Visible = true;
                        break;

                    case false:
                    case null:
                        LicenceLetterNoDT.Visible = false;
                        LicenceLetterNoDD.Visible = false;
                        break;

                    default:
                        break;
                }

                if (HasIPStaticCheckBox.Checked != null)
                    if ((bool)HasIPStaticCheckBox.Checked)
                    {
                        BaseCost cost = BaseCostDB.GetIPCostForADSL();

                        IPTimeDropDownList.DataSource = ADSLServiceDB.GetADSLServiceDurationForIpTime((int)_ServiceInfo.DurationID);
                        IPTimeDropDownList.DataBind();
                        IPTimeDropDownList.Items.Insert(0, new ListItem(string.Empty, string.Empty));


                        IPTimeDropDownList.ClearSelection();
                        IPTimeDropDownList.Items.FindByValue(_ServiceInfo.DurationID.ToString()).Selected = true;

                        if (cost.Tax != null && cost.Tax != 0)
                            IPTaxTextBox.Text = cost.Tax.ToString() + " درصد";
                        else
                            IPTaxTextBox.Text = "0 درصد";

                        if (!string.IsNullOrEmpty(IPTypeDropDownList.SelectedValue) && Convert.ToInt16(IPTypeDropDownList.SelectedValue) == (byte)DB.ADSLIPType.Single)
                            IPCostTextBox.Text = string.IsNullOrEmpty(IPTimeDropDownList.SelectedValue) ? string.Empty : (cost.Cost * Convert.ToInt64(IPTimeDropDownList.SelectedValue)).ToString() + " ریا ل";
                        if (!string.IsNullOrEmpty(IPTypeDropDownList.SelectedValue) && Convert.ToInt16(IPTypeDropDownList.SelectedValue) == (byte)DB.ADSLIPType.Group)
                            IPCostTextBox.Text = string.IsNullOrEmpty(IPTimeDropDownList.SelectedValue) ? string.Empty : (cost.Cost * GroupIPStatic.BlockCount * Convert.ToInt64(IPTimeDropDownList.SelectedValue)).ToString() + " ریا ل";

                        if (_ServiceInfo.IPDiscount == 0)
                        {
                            IPDiscountDT.Visible = false;
                            IPDiscountDD.Visible = false;
                            IPCostDiscountDT.Visible = false;
                            IPCostDiscountDD.Visible = false;
                        }
                        else
                        {
                            IPDiscountDT.Visible = true;
                            IPDiscountDD.Visible = true;
                            IPCostDiscountDT.Visible = true;
                            IPCostDiscountDD.Visible = true;
                        }
                    }

                if (_ServiceInfo.IsModemMandatory)
                {
                    NeedModemCheckBox.Checked = true;
                    NeedModemCheckBox.Enabled = false;
                }

                if (_ServiceInfo.ModemDiscount == 0)
                {
                    ModemDiscountDT.Visible = false;
                    ModemDiscountDD.Visible = false;
                    ModemCostDiscountDT.Visible = false;
                    ModemCostDiscountDD.Visible = false;
                }
                else
                {
                    ModemDiscountDT.Visible = true;
                    ModemDiscountDD.Visible = true;
                    ModemCostDiscountDT.Visible = true;
                    ModemCostDiscountDD.Visible = true;
                }
            }

            else
            {
                BandWidthTextBox.Text = string.Empty;
                DurationTextBox.Text = string.Empty;
                PriceTextBox.Text = string.Empty;
                TaxTextBox.Text = string.Empty;
                ADSLServiceTrafficTextBox.Text = string.Empty;
                AbonmanTextBox.Text = string.Empty;
                ServiceSumPriceTextBox.Text = string.Empty;
            }

          //88  ADSLServiceUpdatePanel.Update();
        }

        protected void CustomerIDChanged(object sender, EventArgs e)
        {
            long customerId = 0;
            long.TryParse(DummyHidden.Value, out customerId);

            if (customerId > 0)
            {
                ADSLCustomer = Data.CustomerDB.GetCustomerByID(customerId);

                NationalCodeTextBox.Text = ADSLCustomer.NationalCodeOrRecordNo;
                CustomerNameTextBox.Text = ADSLCustomer.FirstNameOrTitle + ' ' + ADSLCustomer.LastName;
                MobileNoTextBox.Text = ADSLCustomer.MobileNo;
            }
        }

        protected void ModemSerialNoDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModemSerialNoDropDownList.SelectedValue != null)
            {
                ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById(int.Parse(ModemSerialNoDropDownList.SelectedValue));
                ModemMACAddressTextBox.Text = modemProperty.MACAddress;
            }
            else
                if (_ADSLRequest.ModemSerialNoID != null)
                {
                    ADSLModemProperty modemProperty = ADSLModemPropertyDB.GetADSLModemPropertiesById((int)_ADSLRequest.ModemSerialNoID);
                    ModemMACAddressTextBox.Text = modemProperty.MACAddress;
                }
        }

        #endregion Event Handlers

       
    }
}