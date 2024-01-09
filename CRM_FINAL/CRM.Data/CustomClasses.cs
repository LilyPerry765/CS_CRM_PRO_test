using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace CRM.Data
{
    public class ResourceInfo
    {
        public string Description
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }
    }

    public class SwitchPrecodeInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string SwitchTypeCommercialName { get; set; }
        public long? SwitchPreNo { get; set; }
        public long? FromNumber { get; set; }
        public long? ToNumber { get; set; }
    }

    public class TelecomminucationServiceInfo
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public long UnitPrice { get; set; }
        public int Tax { get; set; }
        public string IsActiveStatus { get; set; }
        public string RequestTypeTitle { get; set; }
        public string UnitMeasureName { get; set; }
    }

    public class TelecomminucationServicePaymentStatisticsInfo
    {
        public string TelecomminucationServiceTitle { get; set; }
        public string UnitMeasure { get; set; }
        public long UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmountWithDiscount { get; set; }
        public decimal TaxAndTollAmount { get; set; }
        public decimal AmountSum { get; set; }
    }

    public class TelecomminucationServicePaymentReportInfo
    {
        public long CustomerID { get; set; }
        public string TelecomminucationServiceTitle { get; set; }
        public decimal Quantity { get; set; }
        public string UnitMeasureName { get; set; }
        public long UnitPrice { get; set; }
        public decimal NetAmount { get; set; }
        public int Discount { get; set; }
        public decimal NetAmountWithDiscount { get; set; }
        public int TaxAndTollAmount { get; set; }
        public decimal AmountSum { get; set; }
        public long RequestID { get; set; }
        public string AddressContent { get; set; }

        /// <summary>
        /// آدرس مبدا
        /// </summary>
        public string InstallAddressContent { get; set; }

        /// <summary>
        /// آدرس مقصد
        /// </summary>
        public string TargetAddressContent { get; set; }
    }

    public class TelecomminucationServicePaymentInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private long _iD;
        public long ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private long _requestID;
        public long RequestID
        {
            get { return _requestID; }
            set { _requestID = value; }
        }

        private long _telecomminucationServiceID;
        public long TelecomminucationServiceID
        {
            get { return _telecomminucationServiceID; }
            set
            {
                _telecomminucationServiceID = value;
            }
        }


        private string _telecomminucationServiceTitle;
        public string TelecomminucationServiceTitle
        {
            get { return _telecomminucationServiceTitle; }
            set
            {
                _telecomminucationServiceTitle = value;
            }
        }

        private decimal _quantity;
        public decimal Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                this.NotifyPropertyChanged("Quantity");
            }
        }

        private decimal _netAmount;
        public decimal NetAmount
        {
            get { return _netAmount; }
            set
            {
                _netAmount = value;
                this.NotifyPropertyChanged("NetAmount");
            }
        }

        private int _discount;
        public int Discount
        {
            get { return _discount; }
            set
            {
                _discount = value;
                this.NotifyPropertyChanged("Discount");
            }
        }

        private decimal _netAmountWithDiscount;
        public decimal NetAmountWithDiscount
        {
            get { return _netAmountWithDiscount; }
            set
            {
                _netAmountWithDiscount = value;
                this.NotifyPropertyChanged("NetAmountWithDiscount");
            }
        }

        private int _taxAndTollAmount;
        public int TaxAndTollAmount
        {
            get { return _taxAndTollAmount; }
            set
            {
                _taxAndTollAmount = value;
                this.NotifyPropertyChanged("TaxAndTollAmount");
            }
        }

        private decimal _amountSum;
        public decimal AmountSum
        {
            get { return _amountSum; }
            set
            {
                _amountSum = value;
                this.NotifyPropertyChanged("AmountSum");
            }
        }


        private long _unitPrice;
        public long UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                this.NotifyPropertyChanged("UnitPrice");
            }
        }
    }

    public class RequestInfo
    {
        public long ID { get; set; }
        public long? TelephoneNo { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public string RegionName { get; set; }
        public string CustomerName { get; set; }
        public string RequestDate { get; set; }
        public string strRequestDate { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }
        public string strRequestLetterDate { get; set; }
        public string RequesterName { get; set; }
        public int RequestPaymentTypeID { get; set; }
        public string RequestPaymentTypeName { get; set; }
        public string InsertDate { get; set; }
        public string CreatorUser { get; set; }
        public string ModifyDate { get; set; }
        public string RequestModifyDate { get; set; }
        public string RequestInsertDate { get; set; }
        public string ModifyUser { get; set; }
        public string EndDate { get; set; }
        public string StatusName { get; set; }
        public string CurrentStep { get; set; }
        public int StatusID { get; set; }
        public int StepID { get; set; }
        public byte? PreviousAction { get; set; }
        public List<SubRequestInfo> Children { get; set; }
        public string TelephoneNostr { set; get; }
        public string CustomerNationalCode { set; get; }
        public bool? IsViewed { get; set; }
        public bool? IsCanceled { get; set; }
        public bool? IsWatting { get; set; }
        public int? NumberOfSaledADSL { get; set; }
        public string CityName { get; set; }
        public long? SaleCost { get; set; }
        public string DayDate { get; set; }
        public int? NumberOfDischarged { get; set; }
        public string RequesRejectDescription { get; set; }
        public SubRequesRejectReason RequesRejectReason { get; set; }
        public string ActionName { get; set; }
        public bool isValidTime { get; set; }
        public List<E1LinkInfo> E1Links { get; set; }
    }

    public class RequestFollowInfo
    {
        public long ID { get; set; }
        public string TelephoneNo { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public string CustomerName { get; set; }
        public string RequesterName { get; set; }
        public string InsertDate { get; set; }
        public string CreatorUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StatusName { get; set; }
        public string CurrentStep { get; set; }
        public string CurrentStatus { get; set; }
        public string FatherName { get; set; }
        public string BirthCertificateID { get; set; }
        public string PreviousStatus { get; set; }
        public int StatusID { get; set; }
        public int StepID { get; set; }
        public bool? IsViewed { get; set; }
        public bool? IsCanceled { get; set; }
        public bool? IsWatting { get; set; }
        public SubRequesRejectReason RequesRejectReason { get; set; }
    }

    public class E1LinkInfo
    {
        public long ID { get; set; }
        public int LinkNumber { get; set; }
    }

    public class RequestPaymentInfo
    {
        public int? BaseCostID { get; set; }
        public long ID { get; set; }
        public RequestPayment RequestPayment { get; set; }
        public string BaseCostTitle { get; set; }
        public string OtherCostTitle { get; set; }
        public Int64? AmountSum { get; set; }
        public string BillID { get; set; }
        public string PaymentID { get; set; }
        public string FicheNumber { get; set; }
        public bool? IsPaid { get; set; }
        public string TelephoneNo { get; set; }
        public string Center { get; set; }
        //public string PaymentTypeTitle { get; set; }
        public string CostType { get; set; }
        public string Cost { get; set; }

        public string City { get; set; }

        public long RequestID { get; set; }

        public string PaymentWayTitle { get; set; }

        public string PaymentTypeTitle { get; set; }

        public string BankName { get; set; }

        public int? Tax { get; set; }

        public string FicheDate { get; set; }

        public bool? IsAccepted { get; set; }
        public string InsertDate { get; set; }
        public string CustomerName { get; set; }
    }
    public class SubRequesRejectReason
    {
        public string Description { get; set; }
        public string Reason { get; set; }
    }

    public class TelephoneInfo
    {
        public long TelephoneNo { get; set; }
        public long SwitchPrecode { get; set; }
        public string PreCodeType { get; set; }
        public string SwitchPort { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string DischargeDate { get; set; }

        public string CauseOfTakePossession { get; set; }

        public string InitialInstallationDate { get; set; }
        public string OldCustomer { get; set; }
        public string ClassTelephone { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsRound { get; set; }
        public string RoundType { get; set; }
        public string CustomerTypeTitle { get; set; }
        public string CustomerTypeGroupTitle { get; set; }
        public int? CustomerTypeID { get; set; }
        public int? CustomerGroupID { get; set; }
        public string InstallationDate { get; set; }
        public string InitialDischargeDate { get; set; }
        public string TelDischargeDate { get; set; }
        public string CutDate { get; set; }
        public string ConnectDate { get; set; }
        public string CauseOfCutName { get; set; }
        public long Price { get; set; }
        public long DepositPrice { get; set; }
        public string SwitchPrecodeString { get; set; }
        public string SpecialService { get; set; }
        public string ChargingTypeName { get; set; }
        public string PosessionTypeName { get; set; }
        public string UsageType { get; set; }

        public string FatherName { get; set; }
        public string NationalID { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string PostalCode { get; set; }

    }

    public class ElkaInfo
    {
        public long RequestID { get; set; }
        public int CenterID { get; set; }
        public int UserID { get; set; }
        public int CabinetID { get; set; }
        public int CabinetNo { get; set; }
        public List<long> TelephoneNos { get; set; }
    }

    public class CustomerTypeInfo
    {
        public int CustomerTypeID { get; set; }
        public string CustomerTypeTitle { get; set; }

        public int CustomerTypeGroupID { get; set; }
        public string CustomerTypeGroupTitle { get; set; }
    }

    public class TelephoneSummenryInfo
    {
        public long TelephoneNo { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string CustomerName { get; set; }
        public string Center { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }

    public class CenterInfo
    {
        public int ID { get; set; }
        public string CenterName { get; set; }
    }

    public class InstallRequestInfo
    {
        public long ID { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeID { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string CustomerName { get; set; }
        public string RequestDate { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }
        public string RequesterName { get; set; }
        public string InsertDate { get; set; }
        public string CreatorUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StatusName { get; set; }
        public string CurrentStep { get; set; }
        public int StatusID { get; set; }
        public int StepID { get; set; }
        public string TelephoneNo { set; get; }
        public string CustomerNationalCode { set; get; }
        public string SaleFicheID { get; set; }
        public string ChargingType { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneTypeGroup { get; set; }
        public string PosessionType { get; set; }
        public string OrderType { get; set; }
        public string InstallRequestType { get; set; }
        public string LetterNumberOfReinstalling { set; get; }
        public string LetterDateOfReinstall { set; get; }
        public string ReasonReinstall { set; get; }
        public string LicenseNumber { set; get; }
        public string LicenseDate { set; get; }
        public string Authorized { set; get; }
        public string PassTelephone { set; get; }
        public string CurrentTelephone { set; get; }
        public long? AddressID { get; set; }
        public string Address { get; set; }
        public string FieldID { get; set; }
        public string InstallationDate { get; set; }
        public DateTime? InstallationDatedate { get; set; }
        public string CustomerType { get; set; }
        public DateTime? InsertDatedate { get; set; }
        public string FichNumber { get; set; }
        public string CounterNo { get; set; }
        public string RequestID { get; set; }
        public long? DepositAmount { get; set; }
        public long? OtherCostsAmount { get; set; }

    }
    public class InstallRequestReport
    {
        public long RequestID { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeID { get; set; }
        public string CenterName { get; set; }
        public string RegionName { get; set; }
        public string CustomerName { get; set; }
        public DateTime? RequestDate { get; set; }
        public string PersianRequestDate { get; set; }
        public string RequestLetterNo { get; set; }
        public DateTime? RequestLetterDate { get; set; }
        public string PersianRequestLetterDate { get; set; }
        public string RequesterName { get; set; }
        public DateTime? InsertDate { get; set; }
        public string PersianInsertDate { get; set; }
        public DateTime? InstallInsertDate { get; set; }

        public DateTime? InstallationDate { get; set; }

        public string PersianInstallationDate { get; set; }


        public string PersianInstallInsertDate { get; set; }
        public string CreatorUser { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyUser { get; set; }
        public string StatusName { get; set; }
        public string CurrentStep { get; set; }
        public string TelephoneNo { set; get; }
        public string CustomerNationalCode { set; get; }
        public string ChargingType { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneTypeGroup { get; set; }
        public string PosessionType { get; set; }
        public string OrderType { get; set; }
        public int? InstallRequestType { get; set; }
        public string LetterNumberOfReinstalling { set; get; }
        public string LetterDateOfReinstall { set; get; }
        public DateTime? DateOfReinstall { set; get; }
        public string PersianDateOfReinstall { set; get; }
        public string ReasonReinstall { set; get; }
        public string PostalCode { set; get; }
        public string LicenseDate { set; get; }
        public string Authorized { set; get; }
        public long? PassTelephone { set; get; }
        public long? CurrentTelephone { set; get; }
        public long? AddressID { get; set; }
        public string Address { get; set; }
        public string FieldID { get; set; }
        public string NearestTelephone { get; set; }
        public string RequestPaymentType { set; get; }
        public string Report_Date { get; set; }
        public string Report_Time { set; get; }
        public string Trust { get; set; }


        public string WorkedCity { set; get; }
        public string WorkedBetweenCity { set; get; }
        public string WorkedOutside { set; get; }
        public string OldTelephoneNo { get; set; }
    }

    public class RequestWaitTimeInfo
    {
        string format = @"dd\.hh\:mm\:ss";
        public TimeSpan waitTime;
        public long RequestID { get; set; }
        public string CustomerName { get; set; }
        public string RequestName { get; set; }
        public string NowStatus { get; set; }
        public string CreateTime { get; set; }
        public string WaitTime
        {
            get
            {
                TimeSpan time;
                if (TimeSpan.TryParse(waitTime.ToString(), out time))
                {

                    return time.Duration().ToString(format);

                }
                else
                {
                    return TimeSpan.Zero.ToString();
                }
            }
            set
            {
                TimeSpan.TryParse(value.ToString(), out waitTime);
            }
        }

    }
    public class RequestsTime
    {
        string format = @"dd\.hh\:mm\:ss";
        public TimeSpan minTime;
        public TimeSpan maxTime;
        public TimeSpan averageTime;

        public string RequestName { get; set; }
        public string MinTime
        {
            get
            {
                TimeSpan time;
                if (TimeSpan.TryParse(minTime.ToString(), out time))
                {

                    return time.Duration().ToString(format);

                }
                else
                {
                    return TimeSpan.Zero.ToString();
                }
            }
            set
            {
                TimeSpan.TryParse(value.ToString(), out minTime);
            }
        }
        public string MaxTime
        {
            get
            {
                TimeSpan time;
                if (TimeSpan.TryParse(maxTime.ToString(), out time))
                {

                    return time.Duration().ToString(format);

                }
                else
                {
                    return TimeSpan.Zero.ToString();
                }
            }
            set
            {
                TimeSpan.TryParse(value.ToString(), out maxTime);
            }
        }
        public string AverageTime
        {
            get
            {
                TimeSpan time;
                if (TimeSpan.TryParse(averageTime.ToString(), out time))
                {

                    return time.Duration().ToString(format);

                }
                else
                {
                    return TimeSpan.Zero.ToString();
                }
            }
            set
            {

                TimeSpan.TryParse(value.ToString(), out averageTime);
            }
        }
    }


    public class SubRequestInfo
    {
        public long ID { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class ChangeNameInfo
    {
        public string RequestNo { get; set; }
        public string RequestID { get; set; }
        public string RequestDate { get; set; }
        public string TelephoneNo { get; set; }
        public string OldCustomerNationalCode { get; set; }
        public string OldFirstNameOrTitle { get; set; }
        public string NewCustomerNationalCode { get; set; }
        public string NewFirstNameOrTitle { get; set; }
        public string RequestTypeName { get; set; }
        public string CenterName { get; set; }
        public string Region { get; set; }
        public string RequesterName { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }

        public string OldCustomer_FirstName { get; set; }
        public string OldCustomer_LastName { get; set; }
        public string OldCustomer_FatherName { get; set; }
        public string OldCustomer_BirthCertificateID { get; set; }
        public string OldCustomer_BirthDateOrRecordDate { get; set; }
        public string OldCustomer_IssuePlace { get; set; }
        public byte OldCustomer_PersonType { get; set; }
        public string OldCustomer_Address { get; set; }
        public string OldCustomer_PostCode { get; set; }
        public string OldCustomer_RecordNo { get; set; }//شماره ثبت
        public string OldCustomer_Agency { get; set; }
        public string OldCustomer_AgencyNumber { get; set; }
        public bool OldCustomer_HasCountLetter { get; set; }
        public string OldCustomer_Buyername { get; set; }
        public string OldCustomer_Name { get; set; }

        public string OldCustomer_PersonTypeTitle { get; set; }


        public string NewCustomer_FirstName { get; set; }
        public string NewCustomer_LastName { get; set; }
        public string NewCustomer_FatherName { get; set; }
        public string NewCustomer_BirthCertificateID { get; set; }
        public string NewCustomer_BirthDateOrRecordDate { get; set; }
        public string NewCustomer_IssuePlace { get; set; }
        public byte NewCustomer_PersonType { get; set; }
        public string NewCustomer_Address { get; set; }
        public string NewCustomer_PostCode { get; set; }
        public string NewCustomer_RecordNo { get; set; }
        public string NewCustomer_Agency { get; set; }
        public string NewCustomer_AgencyNumber { get; set; }
        public string NewCustomer_Name { get; set; }

        public string NewCustomer_PersonTypeTitle { get; set; }

        public string InstallAddress { get; set; }
        public string PostalCode { get; set; }

    }
    public class ChangeNumberInfo
    {
        public long RequestNo { get; set; }
        public string RequestDate { get; set; }
        public long? OldTelephoneNo { get; set; }
        public long? NewTelephoneNo { get; set; }
        public string ChangeReason { get; set; }
        public string Customer { get; set; }
        public string CenterName { get; set; }
        public string Region { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }
        public string CustomerNationalCode { get; set; }


        public string NumberType { get; set; }
        public string ChangeDate { get; set; }
        public string OldSwitchPortID { get; set; }
        public string NewSwitchPortID { get; set; }
        public string Status { get; set; }
    }
    public class ZeroStatusInfo
    {
        public long intRequestNo { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string TelephoneNo { get; set; }
        public string Customer { get; set; }
        public string CenterName { get; set; }
        public string Region { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }
        public string CustomerNationalCode { get; set; }


        public string ZeroStatus { get; set; }
        public byte ClassTelephone { get; set; }
        public string ClassTelephoneName { get; set; }
        public string BlockingDate { get; set; }
        public string UnBlockingDate { get; set; }
        public DateTime? DayeriDate { get; set; }
        public string InstallationPlaceType { get; set; }
        public DateTime? InstallDate { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneTypeGroup { get; set; }
        public string MelliCode { get; set; }
        public string InstallDatePersian { get; set; }
        public string OrderType { get; set; }
        public string ChargingType { get; set; }

        public string InsertDate { get; set; }

        public string InstallHour { get; set; }
    }

    public class TitleIn118Info
    {
        public long intRequestNo { get; set; }
        public string RequestNo { get; set; }
        public string RequestDate { get; set; }
        public string TelephoneNo { get; set; }
        public string Customer { get; set; }
        public string CenterName { get; set; }
        public string Region { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequestLetterDate { get; set; }
        public string CustomerNationalCode { get; set; }
        public string ChangeTitleStatus { get; set; }
        public string NameTitleAt118 { get; set; }
        public string LastNameAt118 { get; set; }
        public string TitleAt118 { get; set; }

    }
    public class PCMBindProperty
    {
        public PCMCardInfo PCM { get; set; }
    }

    public class WiringInfo
    {
        public Wiring Wiring { get; set; }
        public IssueWiring IssueWiring { get; set; }
    }



    public class RequestLogReport
    {
        public long? RequestID { get; set; }
        public string RequestType { get; set; }
        public long? TelephoneNo { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string CustomerID { get; set; }
        public long? ToTelephone { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public object LogEntityDetails { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
    }

    public class ActionLogReport
    {
        public long ID { get; set; }
        public byte ActionID { get; set; }
        public string UserName { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
    }

    public class PcmDropActionLog
    {
        public long ID { get; set; }
        public string CityName { get; set; }

        public string CenterName { get; set; }

        public int? Rock { get; set; }

        public int? Shelf { get; set; }

        public int? Card { get; set; }

        public string Date { get; set; }

    }

    public class WaitingListInfo
    {
        public long ID { get; set; }
        public long RequestID { get; set; }
        public long? TelephoneNo { get; set; }
        public string RequestType { get; set; }
        public int ReasonID { get; set; }
        public string InsertDate { get; set; }
        public string CreatorUser { get; set; }
        public string ExitDate { get; set; }
        public string ExitUser { get; set; }
        public bool Status { get; set; }
    }

    public class CancelationListInfo
    {
        public long ID { get; set; }
        public long TelephoneNo { get; set; }
        public string RequestType { get; set; }
        public string Center { get; set; }
        public string Customer { get; set; }
        public string InsertRequestDate { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
        public string Step { get; set; }
        public string Reason { get; set; }
    }

    public class RequestKaraj
    {
        public long ID { get; set; }
        public long TelephonenNo { get; set; }
        public string CallerID { get; set; }
        public string Cellphone { get; set; }
    }

    public class PAPPortInfo
    {
        public long ID { get; set; }
        public string PAPInfo { get; set; }
        public string Center { get; set; }
        public string PortNo { get; set; }
        public string RowNo { get; set; }
        public string ColumnNo { get; set; }
        public string BuchtNo { get; set; }
        public string TelephoneNo { get; set; }
        public string InstallDate { get; set; }
        public string Status { get; set; }
    }

    public class PAPPortGroupInfo
    {
        public string PAPInfo { get; set; }
        public string Center { get; set; }
        public string RowNo { get; set; }
        public string ColumnNo { get; set; }
        public string FromBuchtNo { get; set; }
        public string ToBuchtNo { get; set; }
        public string Free { get; set; }
        public string Reserve { get; set; }
        public string Connect { get; set; }
        public string Broken { get; set; }
    }

    public class ADSLPAPFeasibilityInfo
    {
        public long ID { get; set; }
        public string PAPInfo { get; set; }
        public string CityName { get; set; }
        public string TelephoneNo { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }

    public class ADSLRequestFullViewInfo
    {
        public long ID { get; set; }
        public long TelephoneNo { get; set; }
        public string CustomerNationalCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerStatus { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CustomerPriority { get; set; }
        public string ADSLRegistrationProjectType { get; set; }
        public bool? ISRequiredInstalation { get; set; }
        public bool? ISNeedModem { get; set; }
        public string ServiceTitle { get; set; }
        public string BandWidth { get; set; }
        public string LicenseLetterNo { get; set; }
        public string TrafficLimitation { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string CreatorUser { get; set; }
        public string InsertDate { get; set; }
        public string Center { get; set; }
        public string RequestDate { get; set; }
        public string CommentCustomers { get; set; }
        public string EquipmentTitle { get; set; }
        public string PortNo { get; set; }
        public string BuchtSpliter { get; set; }
        public string BuchtLine { get; set; }
        public string AssignmentLineUser { get; set; }
        public string AssignmentLineDate { get; set; }
        public string AssignmentLineCommnet { get; set; }
        public string MDFUser { get; set; }
        public string MDFDate { get; set; }
        public string MDFCommnet { get; set; }
        public string OMCUser { get; set; }
        public string OMCDate { get; set; }
        public string OMCCommnet { get; set; }
        public string Contractor { get; set; }
        public string ModemModel { get; set; }
        public string SetupUser { get; set; }
        public string SetupDate { get; set; }
        public string SetupComment { get; set; }
        public string CustomerSatisfaction { get; set; }
        public string InstallDate { get; set; }
    }

    public class WirelessRequestFullViewInfo
    {
        public long ID { get; set; }
        public long TelephoneNo { get; set; }
        public string CustomerNationalCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerStatus { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CustomerPriority { get; set; }
        public string ADSLRegistrationProjectType { get; set; }
        public bool? ISRequiredInstalation { get; set; }
        public bool? ISNeedModem { get; set; }
        public string ServiceTitle { get; set; }
        public string BandWidth { get; set; }
        public string LicenseLetterNo { get; set; }
        public string TrafficLimitation { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string CreatorUser { get; set; }
        public string InsertDate { get; set; }
        public string Center { get; set; }
        public string RequestDate { get; set; }
        public string CommentCustomers { get; set; }
        public string EquipmentTitle { get; set; }
        public string PortNo { get; set; }
        public string BuchtSpliter { get; set; }
        public string BuchtLine { get; set; }
        public string AssignmentLineUser { get; set; }
        public string AssignmentLineDate { get; set; }
        public string AssignmentLineCommnet { get; set; }
        public string MDFUser { get; set; }
        public string MDFDate { get; set; }
        public string MDFCommnet { get; set; }
        public string OMCUser { get; set; }
        public string OMCDate { get; set; }
        public string OMCCommnet { get; set; }
        public string Contractor { get; set; }
        public string ModemModel { get; set; }
        public string SetupUser { get; set; }
        public string SetupDate { get; set; }
        public string SetupComment { get; set; }
        public string CustomerSatisfaction { get; set; }
        public string InstallDate { get; set; }
    }

    public class ADSLChangeServiceFullViewInfo
    {
        public long ID { get; set; }
        public long? TelephoneNo { get; set; }
        public string CustomerNationalCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerStatus { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ServiceType { get; set; }
        public string ADSLRegistrationProjectType { get; set; }
        public string OldTariffTitle { get; set; }
        public string OldBandWidth { get; set; }
        public string OldLicenseLetterNo { get; set; }
        public string OldTrafficLimitation { get; set; }
        public string OldDuration { get; set; }
        public string OldPrice { get; set; }
        public string NewTariffTitle { get; set; }
        public string NewBandWidth { get; set; }
        public string NewLicenseLetterNo { get; set; }
        public string NewTrafficLimitation { get; set; }
        public string NewDuration { get; set; }
        public string NewPrice { get; set; }
        public string CreatorUser { get; set; }
        public string InsertDate { get; set; }
        public string Center { get; set; }
        public string RequestDate { get; set; }
        public string CommentCustomers { get; set; }
        public string OMCUser { get; set; }
        public string OMCDate { get; set; }
        public string OMCCommnet { get; set; }
        public string FinalUser { get; set; }
        public string FinalDate { get; set; }
        public string FinalComment { get; set; }
    }

    public class ADSLCutTemporaryFullViewInfo
    {
        public long ID { get; set; }
        public long? TelephoneNo { get; set; }
        public string CustomerNationalCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerStatus { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CutType { get; set; }
        public string ServiceType { get; set; }
        public string RegistrationProjectType { get; set; }
        public string TariffTitle { get; set; }
        public string BandWidth { get; set; }
        public string LicenseLetterNo { get; set; }
        public string TrafficLimitation { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string CreatorUser { get; set; }
        public string InsertDate { get; set; }
        public string Center { get; set; }
        public string RequestDate { get; set; }
        public string CommentCustomers { get; set; }
        public string CutUser { get; set; }
        public string CutDate { get; set; }
        public string CutCommnet { get; set; }
        public string FinalUser { get; set; }
        public string FinalDate { get; set; }
        public string FinalComment { get; set; }
    }

    public class ADSLPAPRequestInfo
    {
        public long ID { get; set; }
        public long? TelephoneNo { get; set; }
        public string PAPName { get; set; }
        public string Center { get; set; }
        public string Customer { get; set; }
        public string CustomerStatus { get; set; }
        public string ADSLPAPPort { get; set; }
        public string SplitorBucht { get; set; }
        public string NewPort { get; set; }
        public string LineBucht { get; set; }
        public string CreatorUser { get; set; }
        public string InsertDate { get; set; }
        public string RequestDate { get; set; }
        public string InstalTimeOut { get; set; }
        public string CommentCustomers { get; set; }
        public string CommnetReject { get; set; }
        public string MDFUser { get; set; }
        public string MDFDate { get; set; }
        public string MDFComment { get; set; }
        public string EndDate { get; set; }
        public string FinalUser { get; set; }
        public string FinalDate { get; set; }
        public string FinalComment { get; set; }
        public string Step { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public string RequestType { get; set; }
        public string PapInfoID { set; get; }
        public string PapInfoStatus { set; get; }
        public string PapInfoTitle { set; get; }
        public string Region { set; get; }
        public int DateDiff { set; get; }
        public string strDateDiff { set; get; }
        public int Color { get; set; }
        public string PortNo { get; set; }
        public string CompanyName { get; set; }

    }

    public class ADSLEquipmentInfo
    {
        public int ID { get; set; }
        public int CenterID { get; set; }
        public string RockShelf { get; set; }
        public int PAPInfoID { get; set; }
        public int PortTypeID { get; set; }
        public int AAAType { get; set; }
        public string Equipment { get; set; }
        public string Name { get; set; }
        public string Site { get; set; }
        public byte? EquipmentType { get; set; }
        public byte? LocationInstall { get; set; }
        public byte? Product { get; set; }
        public string Region { set; get; }
        public string Center { set; get; }
        public string Status { set; get; }
        public string FreePortCount { set; get; }
        public string FailedPortCount { set; get; }
        public string ReservePortCount { set; get; }
        public string InstallPortCount { set; get; }
        public int Count { set; get; }
        public int ADSLEquipmentIDCount { set; get; }
        public string ADSLEquipmentID { set; get; }
    }

    public class ADSLPortsInfo
    {
        public long ID { get; set; }
        public int CenterID { get; set; }
        public string Center { get; set; }
        public int EquipmentID { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentName { get; set; }
        public string MDFTitle { get; set; }
        public string CenterName { get; set; }
        public string LocationInstall { get; set; }
        public string Product { get; set; }
        public string Address { get; set; }
        public long PortID { get; set; }
        public string PortNo { get; set; }
        public long? InputBucht { get; set; }
        public long? OutBucht { get; set; }
        public string InputConnection { get; set; }
        public string OutputConnection { get; set; }
        public long? TelephoneNo { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public string InstalADSLDate { get; set; }
        public string CityName { get; set; }
        public string Province { get; set; }
        public string NumberOfPorts { get; set; }
        public string Radif { get; set; }
        public string Tabaghe { get; set; }
        public string Etesali { get; set; }
        public int NumberOfDestructionPorts { get; set; }
        public int NumberOfReservedPorts { get; set; }
        public int NumberOfClosedPorts { get; set; }
        public int NumberOfFreePorts { get; set; }
        public int NumberOfUsedPorts { get; set; }

    }

    public class ADSLHistoryInfo
    {
        public long ID { get; set; }
        public string TelephoneNo { get; set; }
        public long? ServiceID { get; set; }
        public string ServiceTitle { get; set; }
        public string ServiceType { get; set; }
        public string ServiceGroup { get; set; }
        public string ServicePrice { get; set; }
        public string User { get; set; }
        public string InsertDate { get; set; }
    }

    public class TechnicalSpecificationsOfADSLInfo
    {
        public string PortNo { get; set; }
        public string InputBucht { get; set; }
        public string OutPutBucht { get; set; }
        public string CustomerBucht { get; set; }
        public string CustomerPort { get; set; }
    }

    public class AssignmentInfo
    {
        public int? InputNumber { get; set; }

        public long? InputNumberID { get; set; }
        public int? PCMStatus { get; set; }
        public int? PCMPortStatus { get; set; }
        public string Connection { get; set; }

        public string OtherBucht { get; set; }
        public int? PCMBuchtTypeID { get; set; }
        public int? BuchtTypeID { get; set; }
        public string BuchtTypeName { get; set; }
        public string ConnectionInput { get; set; }
        public long? BuchtID { get; set; }
        public long? OtherBuchtID { get; set; }
        public string OtherBuchtTypeName { get; set; }
        public string PAPName { get; set; }
        public int? BuchtType { get; set; }
        public byte? BuchtStatus { get; set; }
        public string PortNo { get; set; }
        public int? SwitchPortID { get; set; }
        public string SwitchCode { get; set; }
        public int? SwitchID { get; set; }
        public long? TelePhoneNo { get; set; }

        public int? SwitchPreCodeID { get; set; }
        public string InstallationDate { get; set; }
        public int? PostContact { get; set; }
        public string MUID { get; set; }
        public int? PCMPortIDInBuchtTable { get; set; }
        public long? PostContactID { get; set; }
        public int? PostID { get; set; }
        public int? CabinetID { get; set; }
        public int? CabinetUsageTypeID { get; set; }
        public int? CabinetSwitchID { get; set; }
        public bool? isOutBoundCabinet { get; set; }
        public int? PostName { get; set; }
        public bool? isOutBoundPost { get; set; }
        public int? CabinetName { get; set; }
        public int? CabinetIDForSearch { get; set; }
        public string CustomerName { get; set; }

        public Customer Customer { get; set; }
        public byte? PostContactStatus { get; set; }
        public byte? PostContactType { get; set; }
        public string PostContactStatusName { get; set; }
        public byte? TelephoneStatus { get; set; }
        public int? MDFID { get; set; }
        public string MDFName { get; set; }
        public string CenterName { get; set; }
        public int CenterID { get; set; }
        public string CityName { get; set; }
        public long? Debt { get; set; }
        public DateTime? LastPaidBillDate { get; set; }
        public bool? IsADSL { get; set; }
        public string BuchtStatusName { get; set; }
        public string BuchtTypeString { get; set; }
        public string PostContactStatusString { get; set; }
        public int? Radif { get; set; }
        public int? Tabaghe { get; set; }

        public long? CabinetInputID { get; set; }

        public string Address { get; set; }
        public string PostallCode { get; set; }
        public Address InstallAddress { get; set; }
        public Address CorrespondenceAddress { get; set; }
        public string ADSLBucht { get; set; }
        public int? VerticalColumnNo { get; set; }
        public string AorBType { get; set; }
        public byte? AorBTypeID { get; set; }
        public int? VerticalRowNo { get; set; }
        public long? BuchtNo { get; set; }

        public int? ADSLVerticalColumnNo { get; set; }
        public int? ADSLVerticalRowNo { get; set; }
        public long? ADSLBuchtNo { get; set; }
        public string CauseOfCut { get; set; }
        public long? RequestID { get; set; }

        public int? OtherVerticalColumnNo { get; set; }
        public int? OtherVerticalRowNo { get; set; }
        public long? OtherBuchtNo { get; set; }



        public int? Rock { get; set; }
        public int? Shelf { get; set; }
        public int? Card { get; set; }

        public int? CauseOfCutID { get; set; }
        public string CutDate { get; set; }
        public string ClassTelephone { get; set; }
        public string SpecialServices { get; set; }
        public long? RequestPaymentAmountSum { get; set; }


        public int? PCMCabinetInputColumnNo { get; set; }
        public int? PCMCabinetInputRowNo { get; set; }
        public long? PCMCabinetInputBuchtNo { get; set; }
        public BuchtDetails PCMCabinetInputBucht { get; set; }
        public string CustomerTypeTitle { get; set; }
        public string CustomerGroupTitle { get; set; }
    }

    public class BuchtDetails
    {
        public int? ColumnNo { get; set; }
        public int? RowNo { get; set; }
        public long? BuchtNo { get; set; }
    }

    //TODO:rad
    public class AssignmentShortInfo
    {
        #region CustomerInfo Fields

        private string _telephoneNo;

        public string TelephoneNo
        {
            get { return _telephoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _telephoneNo = value;
                }
                else
                {
                    _telephoneNo = "-----";
                }
            }
        }
        private string _nationalCodeOrRecordNo;
        public string NationalCodeOrRecordNo
        {
            get
            {
                return _nationalCodeOrRecordNo;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _nationalCodeOrRecordNo = value;
                }
                else
                {
                    _nationalCodeOrRecordNo = "-----";
                }
            }
        }

        private string _personType;
        public string PersonType
        {
            get { return _personType; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    switch (byte.Parse(value))
                    {
                        case 0:
                            _personType = "حقیقی";
                            break;
                        case 1:
                            _personType = "حقوقی";
                            break;
                    }
                }
                else
                {
                    _personType = "-----";
                }
            }
        }

        private string _gender;
        public string Gender
        {
            get { return _gender; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    switch (byte.Parse(value))
                    {
                        case 0:
                            _gender = "مرد";
                            break;
                        case 1:
                            _gender = "زن";
                            break;
                    }
                }
                else
                {
                    _gender = "-----";
                }
            }
        }

        private string _firstNameOrTitle;
        public string FirstNameOrTitle
        {
            get { return _firstNameOrTitle; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _firstNameOrTitle = value;
                }
                else
                {
                    _firstNameOrTitle = "-----";
                }
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _lastName = value;
                }
                else
                {
                    _lastName = "-----";
                }
            }
        }

        private string _fatherName;
        public string FatherName
        {
            get { return _fatherName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fatherName = value;
                }
                else
                {
                    _fatherName = "-----";
                }
            }
        }

        private string _birthCertificateID;
        public string BirthCertificateID
        {
            get { return _birthCertificateID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _birthCertificateID = value;
                }
                else
                {
                    _birthCertificateID = "-----";
                }
            }
        }

        private string _issuePlace;
        public string IssuePlace
        {
            get { return _issuePlace; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _issuePlace = value;
                }
                else
                {
                    _issuePlace = "-----";
                }
            }
        }

        private string _birthDateOrRecordDate;
        public string BirthDateOrRecordDate
        {
            get { return _birthDateOrRecordDate; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _birthDateOrRecordDate = value;
                }
                else
                {
                    _birthDateOrRecordDate = "-----";
                }
            }
        }

        private string _urgentTelNo;
        public string UrgentTelNo
        {
            get { return _urgentTelNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _urgentTelNo = value;
                }
                else
                {
                    _urgentTelNo = "-----";
                }
            }
        }

        private string _mobileNo;
        public string MobileNo
        {
            get { return _mobileNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _mobileNo = value;
                }
                else
                {
                    _mobileNo = "-----";
                }
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _email = value;
                }
                else
                {
                    _email = "-----";
                }
            }
        }

        private string _customerID;
        public string CustomerID
        {
            get { return _customerID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _customerID = value;
                }
                else
                {
                    _customerID = "-----";
                }
            }
        }

        private string _agency;
        public string Agency
        {
            get { return _agency; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _agency = value;
                }
                else
                {
                    _agency = "-----";
                }
            }
        }

        private string _agencyNumber;
        public string AgencyNumber
        {
            get { return _agencyNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _agencyNumber = value;
                }
                else
                {
                    _agencyNumber = "-----";
                }
            }
        }

        #endregion

        #region TechnicalInfo Fields

        private string _cityName;
        public string CityName
        {
            get { return _cityName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _cityName = value;
                }
                else
                {
                    _cityName = "-----";
                }
            }
        }

        private string _centerName;
        public string CenterName
        {
            get { return _centerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _centerName = value;
                }
                else
                {
                    _centerName = "-----";
                }
            }
        }

        private string _cabinetName;
        public string CabinetName
        {
            get { return _cabinetName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _cabinetName = value;
                }
                else
                {
                    _cabinetName = "-----";
                }
            }
        }

        private string _inputNumber;
        public string InputNumber
        {
            get { return _inputNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _inputNumber = value;
                }
                else
                {
                    _inputNumber = "-----";
                }
            }
        }

        private string _postName;
        public string PostName
        {
            get { return _postName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _postName = value;
                }
                else
                {
                    _postName = "-----";
                }
            }
        }

        private string _postContact;
        public string PostContact
        {
            get { return _postContact; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _postContact = value;
                }
                else
                {
                    _postContact = "-----";
                }
            }
        }

        private string _connection;
        public string Connection
        {
            get { return _connection; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _connection = value;
                }
                else
                {
                    _connection = "-----";
                }
            }
        }

        private string _otherBucht;
        public string OtherBucht
        {
            get { return _otherBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _otherBucht = value;
                }
                else
                {
                    _otherBucht = "-----";
                }
            }
        }

        private string _mUID;
        public string MUID
        {
            get { return _mUID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _mUID = value;
                }
                else
                {
                    _mUID = "-----";
                }
            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _postalCode = value;
                }
                else
                {
                    _postalCode = "-----";
                }
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _address = value;
                }
                else
                {
                    _address = "-----";
                }
            }
        }

        private string _pAPName;
        public string PAPName
        {
            get { return _pAPName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _pAPName = value;
                }
                else
                {
                    _pAPName = "-----";
                }
            }
        }

        private string _aDSLBucht;
        public string ADSLBucht
        {
            get { return _aDSLBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _aDSLBucht = value;
                }
                else
                {
                    _aDSLBucht = "-----";
                }
            }
        }

        private string _pcmCabinetInputBucht;
        public string PcmCabinetInputBucht
        {
            get { return _pcmCabinetInputBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _pcmCabinetInputBucht = value;
                }
                else
                {
                    _pcmCabinetInputBucht = "-----";
                }
            }
        }

        private string _aorBType;
        public string AorBType
        {
            get { return _aorBType; }
            set
            {
                _aorBType = value;
            }
        }

        private string _causeofCut;
        public string CauseofCut
        {
            get { return _causeofCut; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _causeofCut = value;
                }
                else
                {
                    _causeofCut = "-----";
                }
            }
        }

        private string _classTelephone;
        public string ClassTelephone
        {
            get { return _classTelephone; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _classTelephone = value;
                }
                else
                {
                    _classTelephone = "-----";
                }
            }
        }

        private string _specialServices;
        public string SpecialServices
        {
            get { return _specialServices; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _specialServices = value;
                }
                else
                {
                    _specialServices = "-----";
                }
            }
        }

        private long? _requestPaymentAmountSum;
        public long? RequestPaymentAmountSum
        {
            get { return _requestPaymentAmountSum; }
            set { _requestPaymentAmountSum = value; }
        }

        #endregion
    }

    /// <summary>
    /// .این کلاس را به منظور چاپ در پایان عملیات تعویض شماره در بخش برگردانها ایجاد کردم
    /// SwapTelephoneForm 
    /// Written by rad.
    /// </summary>
    public class SwapTelephoneRequestLog
    {
        private int? _fromAdslColumnNo;
        public int? FromAdslColumnNo
        {
            get { return _fromAdslColumnNo; }
            set { _fromAdslColumnNo = value; }
        }

        private int? _fromAdslRowNo;
        public int? FromAdslRowNo
        {
            get { return _fromAdslRowNo; }
            set { _fromAdslRowNo = value; }
        }

        private long? _fromAdslBuchtNo;
        public long? FromAdslBuchtNo
        {
            get { return _fromAdslBuchtNo; }
            set { _fromAdslBuchtNo = value; }
        }

        private int? _toAdslColumNo;
        public int? ToAdslColumNo
        {
            get { return _toAdslColumNo; }
            set { _toAdslColumNo = value; }
        }

        private int? _toAdslRowNo;
        public int? ToAdslRowNo
        {
            get { return _toAdslRowNo; }
            set { _toAdslRowNo = value; }
        }

        private long? _toAdslBuchtNo;
        public long? ToAdslBuchtNo
        {
            get { return _toAdslBuchtNo; }
            set { _toAdslBuchtNo = value; }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _userName = value;
                }
                else
                {
                    _userName = "-----";
                }
            }
        }

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _date = value;
                }
                else
                {
                    _date = "-----";
                }
            }
        }

        private string _telephoneNo;
        public string TelephoneNo
        {
            get { return _telephoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _telephoneNo = value;
                }
                else
                {
                    _telephoneNo = "-----";
                }
            }
        }

        private string _toTelephoneNo;
        public string ToTelephoneNo
        {
            get { return _toTelephoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toTelephoneNo = value;
                }
                else
                {
                    _toTelephoneNo = "-----";
                }
            }
        }

        private string _fromCabinetID;
        public string FromCabinetID
        {
            get { return _fromCabinetID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCabinetID = value;
                }
                else
                {
                    _fromCabinetID = "-----";
                }
            }
        }

        private string _fromCabinet;
        public string FromCabinet
        {
            get { return _fromCabinet; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCabinet = value;
                }
                else
                {
                    _fromCabinet = "-----";
                }
            }
        }

        private string _fromCabinetInputID;
        public string FromCabinetInputID
        {
            get { return _fromCabinetInputID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCabinetInputID = value;
                }
                else
                {
                    _fromCabinetInputID = "-----";
                }
            }
        }

        private string _fromCabinetInput;
        public string FromCabinetInput
        {
            get { return _fromCabinetInput; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCabinetInput = value;
                }
                else
                {
                    _fromCabinetInput = "-----";
                }
            }
        }

        private string _fromPostContactID;
        public string FromPostContactID
        {
            get { return _fromPostContactID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromPostContactID = value;
                }
                else
                {
                    _fromPostContactID = "-----";
                }
            }
        }

        private string _fromPostContact;
        public string FromPostContact
        {
            get { return _fromPostContact; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromPostContact = value;
                }
                else
                {
                    _fromPostContact = "-----";
                }
            }
        }

        private string _fromPostID;
        public string FromPostID
        {
            get { return _fromPostID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromPostID = value;
                }
                else
                {
                    _fromPostID = "-----";
                }
            }
        }

        private string _fromPost;
        public string FromPost
        {
            get { return _fromPost; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromPost = value;
                }
                else
                {
                    _fromPost = "-----";
                }
            }
        }

        private string _fromBuchtID;
        public string FromBuchtID
        {
            get { return _fromBuchtID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromBuchtID = value;
                }
                else
                {
                    _fromBuchtID = "-----";
                }
            }
        }

        private string _fromConnectionNo;
        public string FromConnectionNo
        {
            get { return _fromConnectionNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromConnectionNo = value;
                }
                else
                {
                    _fromConnectionNo = "-----";
                }
            }
        }

        private string _fromCustomerID;
        public string FromCustomerID
        {
            get { return _fromCustomerID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCustomerID = value;
                }
                else
                {
                    _fromCustomerID = "-----";
                }
            }
        }

        private string _fromCustomerName;
        public string FromCustomerName
        {
            get { return _fromCustomerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _fromCustomerName = value;
                }
                else
                {
                    _fromCustomerName = "-----";
                }
            }
        }

        private string _fromMUID;

        public string FromMUID
        {
            get { return _fromMUID; }
            set { _fromMUID = value; }
        }

        private string _toCabinetID;
        public string ToCabinetID
        {
            get { return _toCabinetID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCabinetID = value;
                }
                else
                {
                    _toCabinetID = "-----";
                }
            }
        }

        private string _toCabinet;
        public string ToCabinet
        {
            get { return _toCabinet; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCabinet = value;
                }
                else
                {
                    _toCabinet = "-----";
                }
            }
        }

        private string _toCabinetInputID;
        public string ToCabinetInputID
        {
            get { return _toCabinetInputID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCabinetInputID = value;
                }
                else
                {
                    _toCabinetInputID = "-----";
                }
            }
        }

        private string _toCabinetInput;
        public string ToCabinetInput
        {
            get { return _toCabinetInput; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCabinetInput = value;
                }
                else
                {
                    _toCabinetInputID = "-----";
                }
            }
        }

        private string _toPostContactID;
        public string ToPostContactID
        {
            get { return _toPostContactID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toPostContactID = value;
                }
                else
                {
                    _toPostContactID = "-----";
                }
            }
        }

        private string _toPostContact;
        public string ToPostContact
        {
            get { return _toPostContact; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toPostContact = value;
                }
                else
                {
                    _toPostContact = "-----";
                }
            }
        }

        private string _toPostID;
        public string ToPostID
        {
            get { return _toPostID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toPostID = value;
                }
                else
                {
                    _toPostID = "-----";
                }
            }
        }

        private string _toPost;
        public string ToPost
        {
            get { return _toPost; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toPost = value;
                }
                else
                {
                    _toPost = "-----";
                }
            }
        }
        private string _toBuchtID;
        public string ToBuchtID
        {
            get { return _toBuchtID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toBuchtID = value;
                }
                else
                {
                    _toBuchtID = "-----";
                }
            }
        }

        private string _toConnectionNo;
        public string ToConnectionNo
        {
            get { return _toConnectionNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toConnectionNo = value;
                }
                else
                {
                    _toConnectionNo = "-----";
                }
            }
        }

        private string _toCustomerID;
        public string ToCustomerID
        {
            get { return _toCustomerID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCustomerID = value;
                }
                else
                {
                    _toCustomerID = "-----";
                }
            }
        }

        private string _toCustomerName;
        public string ToCustomerName
        {
            get { return _toCustomerName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _toCustomerName = value;
                }
                else
                {
                    _toCustomerName = "-----";
                }
            }
        }

        private string _toMUID;
        public string ToMUID
        {
            get { return _toMUID; }
            set { _toMUID = value; }
        }

        private int? _fromVerticalColumnNo;
        public int? FromVerticalColumnNo
        {
            get { return _fromVerticalColumnNo; }
            set { _fromVerticalColumnNo = value; }
        }

        private int? _fromVerticalRowNo;
        public int? FromVerticalRowNo
        {
            get { return _fromVerticalRowNo; }
            set { _fromVerticalRowNo = value; }
        }

        private long? _fromBuchtNo;
        public long? FromBuchtNo
        {
            get { return _fromBuchtNo; }
            set { _fromBuchtNo = value; }
        }

        private int? _fromPcmCabinetInputColumnNo;
        public int? FromPcmCabinetInputColumnNo
        {
            get { return _fromPcmCabinetInputColumnNo; }
            set { _fromPcmCabinetInputColumnNo = value; }
        }

        private int? _fromPcmCabinetInputRowNo;
        public int? FromPcmCabinetInputRowNo
        {
            get { return _fromPcmCabinetInputRowNo; }
            set { _fromPcmCabinetInputRowNo = value; }
        }

        private long? _fromPcmCabinetInputBuchtNo;
        public long? FromPcmCabinetInputBuchtNo
        {
            get { return _fromPcmCabinetInputBuchtNo; }
            set { _fromPcmCabinetInputBuchtNo = value; }
        }

        private int? _toVerticalColumnNo;
        public int? ToVerticalColumnNo
        {
            get { return _toVerticalColumnNo; }
            set { _toVerticalColumnNo = value; }
        }

        private int? _toVerticalRowNo;
        public int? ToVerticalRowNo
        {
            get { return _toVerticalRowNo; }
            set { _toVerticalRowNo = value; }
        }

        private long? _toBuchtNo;
        public long? ToBuchtNo
        {
            get { return _toBuchtNo; }
            set { _toBuchtNo = value; }
        }

        private int? _toPcmCabinetInputColumnNo;
        public int? ToPcmCabinetInputColumnNo
        {
            get { return _toPcmCabinetInputColumnNo; }
            set { _toPcmCabinetInputColumnNo = value; }
        }

        private int? _toPcmCabinetInputRowNo;
        public int? ToPcmCabinetInputRowNo
        {
            get { return _toPcmCabinetInputRowNo; }
            set { _toPcmCabinetInputRowNo = value; }
        }

        private long? _toPcmCabinetInputBuchtNo;
        public long? ToPcmCabinetInputBuchtNo
        {
            get { return _toPcmCabinetInputBuchtNo; }
            set { _toPcmCabinetInputBuchtNo = value; }
        }

        private int? _aDSLVerticalRowNo;
        public int? ADSLVerticalRowNo
        {
            get { return _aDSLVerticalRowNo; }
            set { _aDSLVerticalRowNo = value; }
        }

        private int? _aDSLVerticalColumnNo;
        public int? ADSLVerticalColumnNo
        {
            get { return _aDSLVerticalColumnNo; }
            set { _aDSLVerticalColumnNo = value; }
        }

        private long? _aDSLBuchtNo;

        public long? ADSLBuchtNo
        {
            get { return _aDSLBuchtNo; }
            set { _aDSLBuchtNo = value; }
        }
    }

    /// <summary>
    /// .این کلاس را به منظور چاپ در پایان عملیات تعویض پی سی ام در بخش برگردانها ایجاد کردم
    /// SwapPCMForm 
    /// Written by rad.
    /// </summary>
    public class SwapPCMRequestLog
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _UserName = value;
                }
                else
                {
                    _UserName = "-----";
                }
            }
        }

        private string _Date;
        public string Date
        {
            get { return _Date; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Date = value;
                }
                else
                {
                    _Date = "-----";
                }
            }
        }

        private string _FromTelephoneNo;
        public string FromTelephoneNo
        {
            get { return _FromTelephoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FromTelephoneNo = value;
                }
                else
                {
                    _FromTelephoneNo = "-----";
                }
            }
        }

        private string _ToTelephoneNo;
        public string ToTelephoneNo
        {
            get { return _ToTelephoneNo; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ToTelephoneNo = value;
                }
                else
                {
                    _ToTelephoneNo = "-----";
                }
            }
        }

        private string _FromPCMBucht;
        public string FromPCMBucht
        {
            get { return _FromPCMBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FromPCMBucht = value;
                }
                else
                {
                    _FromBucht = "-----";
                }
            }
        }

        private string _FromBucht;
        public string FromBucht
        {
            get { return _FromBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FromBucht = value;
                }
                else
                {
                    _FromBucht = "-----";
                }
            }
        }

        private string _FromMUID;
        public string FromMUID
        {
            get { return _FromMUID; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _FromMUID = value;
                }
                else
                {
                    _FromMUID = "-----";
                }
            }
        }

        private string _ToBucht;
        public string ToBucht
        {
            get { return _ToBucht; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _ToBucht = value;
                }
                else
                {
                    _ToBucht = "-----";
                }
            }
        }

        private int? _toVerticalColumnNo;
        public int? ToVerticalColumnNo
        {
            get { return _toVerticalColumnNo; }
            set { _toVerticalColumnNo = value; }
        }

        private int? _toVerticalRowNo;
        public int? ToVerticalRowNo
        {
            get { return _toVerticalRowNo; }
            set { _toVerticalRowNo = value; }
        }

        private long? _toBuchtNo;
        public long? ToBuchtNo
        {
            get { return _toBuchtNo; }
            set { _toBuchtNo = value; }
        }

        private int? _toPcmCabinetInputColumnNo;
        public int? ToPcmCabinetInputColumnNo
        {
            get { return _toPcmCabinetInputColumnNo; }
            set { _toPcmCabinetInputColumnNo = value; }
        }

        private int? _toPcmCabinetInputRowNo;
        public int? ToPcmCabinetInputRowNo
        {
            get { return _toPcmCabinetInputRowNo; }
            set { _toPcmCabinetInputRowNo = value; }
        }

        private long? _toPcmCabinetInputBuchtNo;
        public long? ToPcmCabinetInputBuchtNo
        {
            get { return _toPcmCabinetInputBuchtNo; }
            set { _toPcmCabinetInputBuchtNo = value; }
        }

        private int? _fromVerticalColumnNo;
        public int? FromVerticalColumnNo
        {
            get { return _fromVerticalColumnNo; }
            set { _fromVerticalColumnNo = value; }
        }

        private int? _fromVerticalRowNo;
        public int? FromVerticalRowNo
        {
            get { return _fromVerticalRowNo; }
            set { _fromVerticalRowNo = value; }
        }

        private long? _fromBuchtNo;
        public long? FromBuchtNo
        {
            get { return _fromBuchtNo; }
            set { _fromBuchtNo = value; }
        }

        private int? _fromPcmCabinetInputColumnNo;
        public int? FromPcmCabinetInputColumnNo
        {
            get { return _fromPcmCabinetInputColumnNo; }
            set { _fromPcmCabinetInputColumnNo = value; }
        }

        private int? _fromPcmCabinetInoutRowNo;
        public int? FromPcmCabinetInputRowNo
        {
            get { return _fromPcmCabinetInoutRowNo; }
            set { _fromPcmCabinetInoutRowNo = value; }
        }

        private long? _fromPcmCabinetInputBuchtNo;
        public long? FromPcmCabinetInputBuchtNo
        {
            get { return _fromPcmCabinetInputBuchtNo; }
            set { _fromPcmCabinetInputBuchtNo = value; }
        }

        private string _mdfDescription;
        public string MdfDescription
        {
            get { return _mdfDescription; }
            set { _mdfDescription = value; }
        }
    }

    /// <summary>
    /// .این کلاس را به منظور چاپ در پایان عملیات اصلاح مشخصات در بخش برگردانها ایجاد کردم
    /// ModifyProfileForm
    /// Written by rad.
    /// </summary>
    public class ModifyProfileRequestLog : ReportItemInfoBase
    {
        #region Properties

        public string Date { get; set; }

        public string UserName { get; set; }

        public string OldCabinet
        {
            get;
            set;
        }

        public string OldCabinetID
        {
            get;
            set;
        }

        public string OldCabinetInput
        {
            get;
            set;
        }

        public string OldCabinetInputID
        {
            get;
            set;
        }

        public string OldPost
        {
            get;
            set;
        }

        public string OldPostID
        {
            get;
            set;
        }

        public string OldPostContact
        {
            get;
            set;
        }

        public string OldPostContactID
        {
            get;
            set;
        }

        public string OldConnectionNo
        {
            get;
            set;
        }

        public string OldBuchtID
        {
            get;
            set;
        }

        public string OldInstallContactAddress
        {
            get;
            set;
        }

        public string OldInstallPostalCodeInstall
        {
            get;
            set;
        }

        public string OldCorrespondenceContactAddress
        {
            get;
            set;
        }

        public string OldCorrespondencePostalCodeInstall
        {
            get;
            set;
        }

        public string OldInstallAddressID
        {
            get;
            set;
        }

        public string OldCorrespondenceAddressID
        {
            get;
            set;
        }

        public string OldCustomerName
        {
            get;
            set;
        }

        public string OldCustomerID
        {
            get;
            set;
        }

        public string OldTelephoneNo
        {
            get;
            set;
        }

        public string OldAorBType { get; set; }

        public string NewCabinet { get; set; }

        public string NewCabinetID { get; set; }

        public string NewCabinetInput { get; set; }

        public string NewCabinetInputID { get; set; }

        public string NewPost { get; set; }

        public string NewPostID { get; set; }

        public string NewPostContact { get; set; }

        public string NewPostContactID { get; set; }

        public string NewConnectionNo { get; set; }

        public string NewBuchtID { get; set; }

        public string NewInstallContactAddress { get; set; }

        public string NewCorrespondenceContactAddress { get; set; }

        public string NewCorrespondencePostalCodeInstall { get; set; }

        public string NewInstallAddressID { get; set; }

        public string NewCorrespondenceAddressID { get; set; }

        public string NewCustomerName { get; set; }

        public string NewCustomerID { get; set; }

        public string NewTelephoneNo { get; set; }

        public string NewInstallPostalCodeInstall { get; set; }

        public string NewAorBType { get; set; }

        #endregion

        #region Methods
        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    //TODO:rad
    public abstract class ReportItemInfoBase
    {
        #region Methods

        /// <summary>
        /// .زمانی از این متد استفاده شود که شیء مورد نظر ساخته شده و اعضای آن مقدار دهی شده باشند
        /// کار این متد بدین ترتیب است که مقدار کلیه اعضای یک کلاس از نوع رشته را بررسی کرده و چنانچه نال یا خالی باشند
        /// .آنها را مقدار پیش فرض میدهد تا در گزارش نداشتن دیتا ملموس باشد
        /// </summary>
        public virtual void CheckMembersValue()
        {
            System.Reflection.PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (System.Reflection.PropertyInfo pi in properties.Where(p => p.PropertyType == typeof(System.String)))
            {
                object currentPropertyValue = pi.GetValue(this, null);

                if (
                     (currentPropertyValue == null)
                     ||
                     (string.IsNullOrEmpty(currentPropertyValue.ToString()))
                   )
                {
                    pi.SetValue(this, "-----", null);
                }
            }
        }

        #endregion
    }

    //TODO:rad
    /// <summary>
    /// .این کلاس را به منظور چاپ گزارش بازداشت و توقیف در لیست اخطارها ایجاد کردم
    /// Written by Rad.
    /// </summary>
    public class WarnedTelephoneInfo : ReportItemInfoBase
    {
        #region Properties

        public string TelephoneNo { get; set; }

        public string CustomerName { get; set; }

        public string Trust { get; set; }

        public string Date { get; set; }

        public string WarningType
        {
            private set;
            get;
        }

        public string WarningMessage { get; set; }

        #endregion

        #region Methods

        public void SetWarningType(byte warningType)
        {
            if (warningType >= 1)
            {
                this.WarningType = Helpers.GetEnumDescription(warningType, typeof(DB.WarningHistory));
            }
            else
            {
                this.WarningType = string.Empty;
            }
        }

        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    //TODO:rad
    /// <summary>
    /// .این کلاس را برای استفاده در گزارش سابقه خرابی ها در بخش گزارش اطلاعات فنی ایجاد کردم
    /// </summary>
    public class MalfuctionHistoryInfo : ReportItemInfoBase
    {
        #region Properties

        /// <summary>
        /// تاریخ خرابی
        /// </summary>
        public DateTime DateMalfunction { get; set; }

        /// <summary>
        /// زمان خرابی
        /// </summary>
        public string TimeMalfunction { get; set; }

        /// <summary>
        /// نوع خرابی
        /// </summary>
        public byte TypeMalfunction { get; set; }

        /// <summary>
        /// توضیحات
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// شماره مجوز
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// اعلام خرابی یا اصلاحی
        /// </summary>
        public byte MalfuctionOrhealthy { get; set; }

        /// <summary>
        /// فاصله از کافو
        /// </summary>
        public string DistanceFromCabinet { get; set; }

        /// <summary>
        /// فاصله از ام دی اف
        /// </summary>
        public string DistanceFromMDF { get; set; }

        public string CityName { get; set; }

        public string CenterName { get; set; }

        /// <summary>
        /// مرکزی
        /// </summary>
        public Int32 CabinetInputNumber { get; set; }

        /// <summary>
        /// کافو
        /// </summary>
        public int CabinetNumber { get; set; }

        /// <summary>
        /// پست
        /// </summary>
        public int PostNumber { get; set; }

        /// <summary>
        /// اتصالی
        /// </summary>
        public int PostContactConnectionNo { get; set; }

        /// <summary>
        /// رک
        /// </summary>
        public int Rock { get; set; }

        /// <summary>
        /// شلف
        /// </summary>
        public int Shelf { get; set; }

        /// <summary>
        /// کارت
        /// </summary>
        public int Card { get; set; }

        /// <summary>
        /// پورت
        /// </summary>
        public int PortNumber { get; set; }

        #endregion

        #region Methods
        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    //TODO:rad
    public class OutBoundRequestInfo : ReportItemInfoBase
    {
        #region Properties

        /// <summary>
        /// نوع درخواست
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// تاریخ ثبت درخواست
        /// </summary>
        public string RequestInsertDate { get; set; }

        /// <summary>
        /// تاریخ اتمام یا تکمیل درخواست
        /// </summary>
        public string RequestEndDate { get; set; }

        /// <summary>
        /// نام شهر
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// نام مرکز
        /// </summary>
        public string CenterName { get; set; }

        /// <summary>
        /// تاریخ برقراری متراژ خارج از مرز
        /// </summary>
        public string OutBoundEstablishDate { get; set; }

        /// <summary>
        /// متراژ خارج از مرز
        /// </summary>
        public string OutBoundMeter { get; set; }

        /// <summary>
        /// مجموع هزینه ها
        /// </summary>
        public long? RequestPaymentsSumAmount { get; set; }

        /// <summary>
        /// شماره تلفن
        /// </summary>
        public string TelephoneNo { get; set; }

        #endregion

        #region Methods

        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    /// <summary>
    /// .این کلاس را برای گزارش آمار تلفن های مشغول به کار بر اساس نوع سوئیچ در گزارش اطلاعات فنی ایجاد کردم
    /// </summary>Written by rad
    public class WorkingTelephoneStatisticsInfo : ReportItemInfoBase
    {
        #region Properties

        public string CityName { get; set; }

        public string CenterName { get; set; }

        /// <summary>
        /// نوع سوئیچ
        /// </summary>
        public string SwitchTypeName { get; set; }

        /// <summary>
        /// تعداد منصوبه
        /// </summary>
        public long InstalledCount { get; set; }

        /// <summary>
        /// تعداد مشغول بکار
        /// </summary>
        public long InUseCount { get; set; }

        /// <summary>
        /// تعداد دایری
        /// </summary>
        public long DayriCount { get; set; }

        /// <summary>
        /// تعداد تخلیه
        /// </summary>
        public long DischarginCount { get; set; }

        /// <summary>
        /// تعداد استرداد ودیعه
        /// </summary>
        public long RefundDepositCount { get; set; }

        /// <summary>
        /// تعداد تعویض شماره
        /// </summary>
        public long ChangeNoCount { get; set; }

        /// <summary>
        /// تعداد برگردان نوری
        /// </summary>
        public long TranslationOpticalCabinetToNormalCount { get; set; }

        #endregion

        #region Methods

        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    /// <summary>
    /// .این کلاس را برای گزارش لیست کافو های پر شده در بخش کافو های مرکز ایجاد کردم
    /// </summary>Written by rad
    public class FilledCabinetInfo : ReportItemInfoBase
    {
        #region Properties

        public string CityName { get; set; }

        public string CenterName { get; set; }

        /// <summary>
        /// شماره کافو
        /// </summary>
        public int CabinetNumber { get; set; }

        /// <summary>
        /// تعداد مرکزی ها
        /// </summary>
        public string CabinetInputsCount { get; set; }

        /// <summary>
        /// سهمیه 3 درصد
        /// </summary>
        public int RemainedQuotaReservation { get; set; }

        /// <summary>
        /// تعداد زوج فعال
        /// </summary>
        public int ActiveCount { get; set; }

        /// <summary>
        /// تعداد زوج رزرو شده
        /// </summary>
        public int ReservedCount { get; set; }

        /// <summary>
        /// تعداد زوج خراب
        /// </summary>
        public int BrokenCount { get; set; }

        /// <summary>
        /// تعداد زوج خالی
        /// </summary>
        public int EmptyCount { get; set; }

        #endregion

        #region Methods

        public override void CheckMembersValue()
        {
            base.CheckMembersValue();
        }

        #endregion
    }

    //TODO:rad
    /// <summary>
    /// .از این کلاس برای ذخیره و بازیابی تنظیمات مربوط به گزارش هایی که در گرید ها هستند ، استفاده میشود
    /// </summary>
    public class ReportSetting
    {
        #region Properties And Fields
        public int StiPageOrientation { get; set; }

        public System.Drawing.Font HeaderFont { get; set; }

        public double HeaderFontSize
        {
            get
            {
                double headerFontSize = 13.0;
                if (this.HeaderFont != null)
                {
                    headerFontSize = (double)this.HeaderFont.Size;
                }
                return headerFontSize;
            }
        }
        public string HeaderFontFamilyName
        {
            get
            {
                string headerFontFamilyName = "Tahoma";
                if (this.HeaderFont != null)
                {
                    headerFontFamilyName = this.HeaderFont.Name;
                }
                return headerFontFamilyName;
            }
        }
        public bool HeaderFontIsBold { get; set; }
        public bool HeaderFontIsItalic { get; set; }
        public bool HeaderFontIsUnderlined { get; set; }

        public SolidColorBrush HeaderBackground { get; set; }
        public SolidColorBrush HeaderForeground { get; set; }
        public SolidColorBrush HeaderBorderBrush { get; set; }

        private double _headerBorderThickness;
        public double HeaderBorderThickness
        {
            get
            {
                return _headerBorderThickness;
            }
            set
            {
                if (value > 0)
                {
                    _headerBorderThickness = value;
                }
                else
                {
                }
                _headerBorderThickness = 1.0;
            }
        }

        public System.Drawing.Font TextFont { get; set; }
        public double TextFontSize
        {
            get
            {
                double textFontSize = 11.0;
                if (this.TextFont != null)
                {
                    textFontSize = (double)this.TextFont.Size;
                }
                return textFontSize;
            }
        }
        public string TextFontFamilyName
        {
            get
            {
                string textFontFamilyName = "B Nazanin";
                if (this.TextFont != null)
                {
                    textFontFamilyName = this.TextFont.Name;
                }
                return textFontFamilyName;
            }
        }
        public bool TextFontIsBold { get; set; }
        public bool TextFontIsItalic { get; set; }
        public bool TextFontIsUnderlined { get; set; }

        public SolidColorBrush TextBackground { get; set; }
        public SolidColorBrush TextForeground { get; set; }
        public SolidColorBrush TextBorderBrush { get; set; }

        private double _textBorderThickness;
        public double TextBorderThickness
        {
            get
            {
                return _textBorderThickness;
            }
            set
            {
                if (value > 0)
                {
                    _textBorderThickness = value;
                }
                else
                {
                    _textBorderThickness = 1.0;
                }
            }
        }

        public bool TextHasWordWrap { get; set; }
        public bool HeaderHasWordWrap { get; set; }

        public bool PrintWithPreview { get; set; }
        public bool ReportHasPageFooter { get; set; }
        public bool ReportHasTitle { get; set; }

        public bool ReportHasDate { get; set; }

        public bool ReportHasTime { get; set; }

        public bool ReportHasLogo { get; set; }

        public bool ReportSumRecordsQuantity { get; set; }

        #endregion

        #region Methods

        public void SetMembersDefault()
        {
            this.HeaderBackground = (this.HeaderBackground != null) ? this.HeaderBackground : Brushes.LightSkyBlue;
            this.HeaderBorderBrush = (this.HeaderBorderBrush != null) ? this.HeaderBorderBrush : Brushes.Black;
            this.HeaderForeground = (this.HeaderForeground != null) ? this.HeaderForeground : Brushes.Black;
            this.HeaderFont = (this.HeaderFont != null) ? this.HeaderFont : new System.Drawing.Font("B Nazanin", 13F);
            this.TextBackground = (this.TextBackground != null) ? this.TextBackground : Brushes.Transparent;
            this.TextBorderBrush = (this.TextBorderBrush != null) ? this.TextBorderBrush : Brushes.Black;
            this.TextForeground = (this.TextForeground != null) ? this.TextForeground : Brushes.Black;
            this.TextFont = (this.TextFont != null) ? this.TextFont : new System.Drawing.Font("B Nazanin", 11F);
            this.ReportHasDate = true;
            this.ReportHasLogo = true;
            this.ReportHasPageFooter = true;
            this.ReportHasTime = true;
            this.ReportHasTitle = true;
            this.PrintWithPreview = true;
            this.TextHasWordWrap = true;
            this.HeaderHasWordWrap = true;
            this.ReportSumRecordsQuantity = true;
        }

        #endregion
    }


    public class SwitchCodeInfo
    {
        public long SwitchPreNo { get; set; }
        public string SwitchCode { get; set; }
        public byte PreCodeType { get; set; }
        public string PortNo { get; set; }
        public long TelephoneNo { get; set; }
        public string FeatureONU { get; set; }
        public string CenterName { get; set; }
        public string CommercialName { get; set; }
    }


    public class StepStatusInfo
    {
        public int StepStatusID { get; set; }
        public int? ParentStepStatusID { get; set; }
        public int reqStepID { get; set; }
        public string StatusResult { get; set; }
    }

    public class UserInfo
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public string Role { get; set; }
        public string LastLoginDate { get; set; }
        public List<int> CenterIDs { get; set; }
        public List<int> RequestStepsIDs { get; set; }
        public static List<int> UserCenterIDs { get; set; }
        public List<int> SubscriberTypeIDs { get; set; }
        public List<int> ReportTemplateIDs { get; set; }
        public List<int> CallDetailTypeIDs { get; set; }
        public List<string> ResourceNames { get; set; }
        public ReportSetting ReportSetting { get; set; }

        public List<DataGridColumnConfig> DataGridColumnConfig { get; set; }
        public string LocalIPAddress { get; set; }
        public string ExternalIPAddress { get; set; }
        public CRM.Data.Schema.UserConfig UserConfig { get; set; }

        public UserInfo()
        {
            FullName = FirstName + " " + LastName;
            CenterIDs = new List<int>();
            ResourceNames = new List<string>();
            SubscriberTypeIDs = new List<int>();
            CallDetailTypeIDs = new List<int>();
            RequestStepsIDs = new List<int>();
        }
    }

    public partial class RoleInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UesrCount { get; set; }
        public bool? IsServiceRole { get; set; }
    }

    public partial class ResourceCheckable
    {
        public int ID { get; set; }
        public Guid UniqueID { get; set; }
        public string Name { get; set; }
        public bool? IsChecked { get; set; }
        public string Description { get; set; }
        public bool IsEditable { get; set; }
        public List<ResourceCheckable> ChildResource { get; set; }
    }

    public class TelephoneInfoForRequest
    {
        public long TelephoneNo { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string CustomerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Email { get; set; }
        public int CenterID { get; set; }
        public string Center { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CustomerTelephone { get; set; }
        public string Mobile { get; set; }

        public string CustomerTypeName { get; set; }
        public string CustomerGroupName { get; set; }

    }

    public class CutAndEstablishInfo
    {
        public string CutDate { get; set; }
        public string EstablishDate { get; set; }
        public string Hour { get; set; }
        public long FICode { get; set; }
        public string Name { get; set; }
        public long? TelNumber { get; set; }
        public string LastDateCounter { get; set; }
        public string CounterValue { get; set; }
        public string TypeValue { get; set; }
        public string LetterNumber { get; set; }
        public string ReqName { get; set; }
        public string CutType { get; set; }
        public int Count { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public DateTime? CutDatedate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? EstablishDatedate { get; set; }
        public string RequestLetterNo { get; set; }
        public string RequesterName { get; set; }
        public string CauseOfCut { get; set; }
        public string CounterNo { get; set; }
        public string CutAndEstablishID { get; set; }
        public string CounterDate { get; set; }
        public long? TelephoneNo { get; set; }
        public string InstallAdress { get; set; }
        public string CustomerAgnecyNumber { get; set; }
        public string Region { get; set; }
        public string CenterName { get; set; }
        public string PersonType { get; set; }
        public string RequestDate { get; set; }
        public string City { get; set; }
        public string UrgentTelNo { get; set; }
    }

    public class CuttedTelephoneInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public long? TelephoneNo { get; set; }
        public string ActionCutDueDate { get; set; }
        public int? VerticalColumnNo { get; set; }
        public int? VerticalRowNo { get; set; }
        public long? BuchtNo { get; set; }
        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }
        public int? PcmColumnNo { get; set; }
        public int? PcmRowNo { get; set; }
        public long? PcmBuchtNo { get; set; }
    }
    public class EstablishedTelephoneInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public long? TelephoneNo { get; set; }
        public string ActionEstablishDueDate { get; set; }
        public int? VerticalColumnNo { get; set; }
        public int? VerticalRowNo { get; set; }
        public long? BuchtNo { get; set; }
        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }
        public int? PcmColumnNo { get; set; }
        public int? PcmRowNo { get; set; }
        public long? PcmBuchtNo { get; set; }
    }

    public class ADSLInfo
    {
        public string TelephoneNo { set; get; }
        public string CustomerOwnerName { set; get; }
        public string CustomerOwner_Identification_RecordNO { set; get; }
        public string CustomerOwnerStatus { set; get; }
        public int ServiceID { set; get; }
        public string ServiceTitle { set; get; }
        public string ServiceType { set; get; }
        public string RegistrationProjectType { set; get; }
        public string PapInfoName { set; get; }
        public string ADSLPortID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string MobileNo { get; set; }
        public string Status { set; get; }
        public int Count { set; get; }
        public string InstallDate { get; set; }
        public string Center { get; set; }
        public string CityCenter { get; set; }
        public string PaymentType { get; set; }
        public string CustomerType { get; set; }
        public string PersonType { get; set; }
        public string CityName { get; set; }
        public byte? SaleWayByte { get; set; }
        public string SaleWay { get; set; }
        public string BandWidth { get; set; }
        public string Duration { get; set; }
        public string Traffic { get; set; }
        public string Cost { get; set; }
        public string Tax { get; set; }
        public string AmountSum { get; set; }
        public string EXPDate { get; set; }
        public string RequestType { get; set; }
        public string RanjeCost { get; set; }
        public string InstallmentCost { get; set; }
        public string ADSLPortNo { get; set; }
    }

    public class ADSLCustomerInfo
    {
        public long ID { get; set; }
        public string TelephoneNo { set; get; }
        public string UserID { get; set; }
        public string CustomerName { set; get; }
        public string ADSLCustomerType { set; get; }
        public string PersonType { set; get; }
        public string MelliCode { set; get; }
        public string CustomerStatus { set; get; }
        public int ServiceID { set; get; }
        public string Service { set; get; }
        public long? PortID { set; get; }
        public string Port { set; get; }
        public string IPStatic { set; get; }
        public string GroupIPStatic { set; get; }
        public string IPStartDate { get; set; }
        public string IPEndDate { get; set; }
        public int? ModemID { get; set; }
        public string Modem { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Center { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string InstallDate { get; set; }
        public string ExpDate { get; set; }
        public string Status { get; set; }
    }

    public class ADSLChargedDischargedInfo
    {
        public int NumberOfUsedPorts { get; set; }
        public int NumberOfActiveCustomers { get; set; }
        public int NumberOfDischarge { get; set; }
        public int NumberPrePaidOfCharged { get; set; }
        public int NumberPostPaidOfCharged { get; set; }
        public int NumberFreeOfCharged { get; set; }
        public int NumberOfCharged { get; set; }
        public string Center { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public int NumberOfADSLChangeService { get; set; }
        public long? PrePaidIncome { get; set; }
        public long? PostPaidIncome { get; set; }
        public int NumberOfExpired { get; set; }
        public long? ExpiredAmountSum { get; set; }
        public long? DayeriAmountSum { get; set; }
        public double EXPDatePassedDays { get; set; }
        public string PortNo { get; set; }
    }

    public class ADSLRequestTimeInfo
    {
        public string City { get; set; }
        public string CenterName { get; set; }
        public string CustomerName { get; set; }
        public string TelephoneNo { get; set; }
        public string ADSLServiceTitle { get; set; }
        public string PaymentDate { get; set; }
        public DateTime? PaymentDateValue { get; set; }
        public string MDFDate { get; set; }
        public DateTime? MDFDateValue { get; set; }
        public string WatingMDFDate { get; set; }
        public string InstallDate { get; set; }
        public DateTime? InstallDateValue { get; set; }
        public string WaitingInstallDate { get; set; }
        public string InstalationType { get; set; }
    }

    public class ADSLServiceSellInfo
    {
        public string City { get; set; }
        public string CenterName { get; set; }
        public string CustomerName { get; set; }
        public long? TelephoneNo { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string PersonType { get; set; }
        public string CustomerOwnStatus { get; set; }
        public string AdslServiceTitle { get; set; }
        public int? ServiceCode { get; set; }
        public string PortNo { get; set; }
        public string InsertDate { get; set; }
        public string EndDate { get; set; }
        public string RequesterName { get; set; }
        public string SerialNo { get; set; }
        public string AdslModemTitle { get; set; }
        public string ADSLServiceCostPaymentType { get; set; }
        public string MobileNo { get; set; }
        public long? AmountSum { get; set; }
        public string WorkFlow { get; set; }
        public string CustomerType { get; set; }

        /// <summary>
        /// نظر امور مشترکین
        /// </summary>
        public string CommentCustomers { get; set; }

        /// <summary>
        /// عنوان گروه شغلی
        /// </summary>
        public string JobGroupTitle { get; set; }

        /// <summary>
        /// شماره نامه مجوز پهنای باند
        /// </summary>
        public string LicenseLetterNo { get; set; }
    }

    public class ADSLServiceInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string BandWidth { get; set; }
        public string Traffic { get; set; }
        public int DurationID { get; set; }
        public string Duration { get; set; }
        public int GroupID { get; set; }
        public int CustomerGroupID { get; set; }
        public int PaymentTypeID { get; set; }
        public string PaymentType { get; set; }
        public string Price { get; set; }
        public string Tax { get; set; }
        public string Abonman { get; set; }
        public string PriceSum { get; set; }
        public bool? IsInstalment { get; set; }
        public bool? IsRequiredLicense { get; set; }
        public bool? IsSpecial { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsOnlineRegister { get; set; }
        public string ServiceGroup { get; set; }
        public string ServiceType { get; set; }
        public string NewtworkType { get; set; }
        public string IBSngGroupName { get; set; }
        public string GiftProfileName { get; set; }
        public bool IsModemMandatory { get; set; }
        public bool IsModemInstallment { get; set; }
        public int IPDiscount { get; set; }
        public int ModemDiscount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }
        public string IsSpecialString { get; set; }
        public string IsActiveString { get; set; }
        public string IsOnlineRegisterString { get; set; }
        public int TypeID { get; set; }
        public byte? SaleWayByte { get; set; }
        public string SaleWay { get; set; }
        public string NumberOfSoldTraffic { get; set; }
        public string SoldTraffic { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string CenterName { get; set; }
        public string BillID { get; set; }
        public string PaymentID { get; set; }
        public string SumPrice { get; set; }
        public string InstallmentPrice { get; set; }
        public string RanjePrice { get; set; }
        public string SumAmount { get; set; }
        public string TotalProceeds { get; set; }
        public bool? RequiredInstallation { get; set; }
        public string CenterID { get; set; }
        public bool? IsPaid { get; set; }
        public string IsPaidString { get; set; }
        public string CenterCode { get; set; }
        public string paymentDate { get; set; }
        public string ModemPrice { get; set; }
        public string IPPrice { get; set; }

        public byte PersonType { get; set; }

        public string PersonTypeName { get; set; }

        public string AdslCustomerGroupTitle { get; set; }

        public string AdslServiceType { get; set; }

        public string AdslServiceGroup { get; set; }

        public string ServiceTitle { get; set; }

        public string ServiceCode { get; set; }

        public string SellChannel { get; set; }
        public int CustomersCount { get; set; }

        /// <summary>
        /// حجم کل
        /// </summary>
        public string TotalTraffic { get; set; }

        /// <summary>
        /// حجم مازاد خریداری شده - رشته
        /// </summary>
        public string AdditionalTraffic { get; set; }

        /// <summary>
        /// حجم مازاد خریداری شده - عدد
        /// </summary>
        public decimal? AdditionalTrafficDecimal { get; set; }

        /// <summary>
        /// میانگین  حجم استفاده شده توسط هر مشترک
        /// </summary>
        public string CustomerUsedTrafficAverage { get; set; }

        /// <summary>
        /// تعداد سرویس های گرفته شده
        /// </summary>
        public int NumberOfAllServices { get; set; }

        /// <summary>
        /// مبلغ سرویس های گرفته شده
        /// </summary>
        public long? AmountOfAllServices { get; set; }

        /// <summary>
        ///  حجم ترافیک های گرفته شده  - عدد
        /// </summary>
        public decimal? TotalOfAllTrafficsDeciaml { get; set; }

        /// <summary>
        /// حجم ترافیک های گرفته شده - رشته
        /// </summary>
        public string TotalOfAllTraffics { get; set; }

        /// <summary>
        /// مبلغ ترافیک های گرفته شده
        /// </summary>
        public long? AmountOfAllTraffics { get; set; }

        /// <summary>
        /// مبلغ آی پی های  خریداری شده
        /// </summary>
        public long? AmountOfAllPurchasedIp { get; set; }

        /// <summary>
        /// مبلغ مودم های خریداری شده
        /// </summary>
        public long? AmountOfAllPurchasedModem { get; set; }

        /// <summary>
        /// جمع مبلغ
        /// </summary>
        public long? TotalAmount { get; set; }

        /// <summary>
        /// مدت - به روز
        /// </summary>
        public int? InUseServiceByDay { get; set; }

        /// <summary>
        /// مدت زمان انتظار تا خروج از ام دی اف بر اساس ساعت
        /// </summary>
        public double WaitingHoursUnitlExitFromMDF { get; set; }

        /// <summary>
        /// مدت زمان انتظار تا خروج از ام دی اف  بر اساس روز و ساعت به صورت یک رشته 
        /// </summary>
        public string WaitingDurationUnitlExitFromMDF { get; set; }

        /// <summary>
        /// تاریخ درخواست
        /// </summary>
        public string RequestDate { get; set; }
    }

    public class ADSlServiceBandWidthInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public DateTime? ToDate { get; set; }
        public string PersianToDate { get; set; }
        public string BandWidth { get; set; }

        /// <summary>
        /// تعداد پورتهای دایر شده
        /// </summary>
        public int? InstalledPortCount { get; set; }

        /// <summary>
        /// تعداد پورت های فعال
        /// </summary>
        public int? ActivePortCount { get; set; }
    }

    public class AdslPapPortStatisticsInfo
    {
        public int? CityID { get; set; }
        public int? CenterID { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string PapInfoTitle { get; set; }

        /// <summary>
        /// تعداد منصوبه
        /// </summary>
        public int? TotalCount { get; set; }


        /// <summary>
        /// تعداد دایری
        /// </summary>
        public int? InstallCount { get; set; }

        /// <summary>
        /// تعداد تخلیه
        /// </summary>
        public int? DischargeCount { get; set; }

        /// <summary>
        /// تعداد مشغول به کار
        /// </summary>
        public int? InUseCount { get; set; }

        /// <summary>
        /// از تاریخ
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// تا تاریخ
        /// </summary>
        public string ToDate { get; set; }
    }

    public class ADSLRequestInfo
    {
        public long ID { get; set; }
        public string RequestDate { set; get; }
        public string DayeriDate { set; get; }
        public string RequestType { get; set; }
        public int RequestTypeID { get; set; }
        public string TelephoneNo { get; set; }
        public string Region { get; set; }
        public string Center { get; set; }
        public string CurrentStatusRequest { get; set; }
        public string CustomerOwnerName { get; set; }
        public string CustomerOwner_Identification_RecordNO { get; set; }
        public string CustomerOwnerStatus { get; set; }
        public string Status { get; set; }
        public string Step { get; set; }
        public int? Count { get; set; }
        public string LetterNo { get; set; }
        public long? ServiceID { get; set; }
        public string ServiceTitle { get; set; }
        public string CustomerGroupType { get; set; }
        public string JobGroupType { get; set; }
        public string ModemType { get; set; }
        public int NumberOfSaledADSLService { get; set; }
        public long? SumCostOfSaledADSLService { get; set; }
        public List<ADSLRequestInfo> ListOfSaledADSLService { get; set; }
        public int? CustomerCount { get; set; }
        public long? Cost { get; set; }
        public string MobileNO { get; set; }
        public string MDFPortNo { get; set; }
        public string RegistrationDate { get; set; }
        public string MDFDate { get; set; }
        public string ModemSerial { get; set; }
        public string ModemSetupUser { get; set; }
        public string SellerAgentName { get; set; }
        public int NumberOfADSLSaled { get; set; }
        public string CityName { get; set; }
        public long? SaleCost { get; set; }
        public string DayDate { get; set; }
        public string SaleWay { get; set; }
        public string ProvinceName { get; set; }
        public byte? SaleWayByte { get; set; }
        public string NumberOfSubscribers { get; set; }
        public string NumberOfDayeriServices { get; set; }
        public string CenterId { get; set; }
        public string ProvinceID { get; set; }
        public string AMountSUm { get; set; }
        public string NumberOfInstalledServices { get; set; }
        public string ADSLSellerAgent { get; set; }
        public string EndDate { get; set; }
        public int NumberOfSold { get; set; }
        public string RequestPaymentType { get; set; }
        public bool? NeedModem { get; set; }
        public string NeedModemString { get; set; }
        public int? ADSLSellerAgentID { get; set; }
        public bool? IsIBSNG { get; set; }
        public string FicheNumber { get; set; }
        public string IsIBSNGString { get; set; }
        public string ADSLSellerAgentUSer { get; set; }
        public int? ADSLSellerAgentUserID { get; set; }
        public string CityID { get; set; }
        public long? TrafficSaleAmount { get; set; }
        public long? ServiceSaleAmount { get; set; }
        public int AmountSum { get; set; }
        public string CustomerGroupName { get; set; }
        public long? IPCost { get; set; }
        public long? ServiceCost { get; set; }
        public long? TrafficCost { get; set; }
        public long? ModemCost { get; set; }
        public string PaymentDate { get; set; }
        public long? PrePaidAmountSum { get; set; }
        public long? PostPaidAmountSum { get; set; }
        public int NumberOfSoldADSLPrePaid { get; set; }
        public int NumberOfSoldADSLPostPaid { get; set; }
    }

    public class WirelessRequestInfo
    {
        public long ID { get; set; }
        public string RequestDate { set; get; }
        public string DayeriDate { set; get; }
        public string RequestType { get; set; }
        public int RequestTypeID { get; set; }
        public string TelephoneNo { get; set; }
        public string Region { get; set; }
        public string Center { get; set; }
        public string CurrentStatusRequest { get; set; }
        public string CustomerOwnerName { get; set; }
        public string CustomerOwner_Identification_RecordNO { get; set; }
        public string CustomerOwnerStatus { get; set; }
        public string Status { get; set; }
        public string Step { get; set; }
        public int? Count { get; set; }
        public string LetterNo { get; set; }
        public long? ServiceID { get; set; }
        public string ServiceTitle { get; set; }
        public string CustomerGroupType { get; set; }
        public string JobGroupType { get; set; }
        public string ModemType { get; set; }
        public int NumberOfSaledADSLService { get; set; }
        public long? SumCostOfSaledADSLService { get; set; }
        public List<ADSLRequestInfo> ListOfSaledADSLService { get; set; }
        public int? CustomerCount { get; set; }
        public long? Cost { get; set; }
        public string MobileNO { get; set; }
        public string MDFPortNo { get; set; }
        public string RegistrationDate { get; set; }
        public string MDFDate { get; set; }
        public string ModemSerial { get; set; }
        public string ModemSetupUser { get; set; }
        public string SellerAgentName { get; set; }
        public int NumberOfADSLSaled { get; set; }
        public string CityName { get; set; }
        public long? SaleCost { get; set; }
        public string DayDate { get; set; }
        public string SaleWay { get; set; }
        public string ProvinceName { get; set; }
        public byte? SaleWayByte { get; set; }
        public string NumberOfSubscribers { get; set; }
        public string NumberOfDayeriServices { get; set; }
        public string CenterId { get; set; }
        public string ProvinceID { get; set; }
        public string AMountSUm { get; set; }
        public string NumberOfInstalledServices { get; set; }
        public string ADSLSellerAgent { get; set; }
        public string EndDate { get; set; }
        public int NumberOfSold { get; set; }
        public string RequestPaymentType { get; set; }
        public bool? NeedModem { get; set; }
        public string NeedModemString { get; set; }
        public int? ADSLSellerAgentID { get; set; }
        public bool? IsIBSNG { get; set; }
        public string FicheNumber { get; set; }
        public string IsIBSNGString { get; set; }
        public string ADSLSellerAgentUSer { get; set; }
        public int? ADSLSellerAgentUserID { get; set; }
        public string CityID { get; set; }
        public long? TrafficSaleAmount { get; set; }
        public long? ServiceSaleAmount { get; set; }
        public int AmountSum { get; set; }
        public string CustomerGroupName { get; set; }
        public long? IPCost { get; set; }
        public long? ServiceCost { get; set; }
        public long? TrafficCost { get; set; }
        public long? ModemCost { get; set; }
        public string PaymentDate { get; set; }
        public long? PrePaidAmountSum { get; set; }
        public long? PostPaidAmountSum { get; set; }
        public int NumberOfSoldADSLPrePaid { get; set; }
        public int NumberOfSoldADSLPostPaid { get; set; }
    }
    public class ADSLEquipmentGroup
    {
        public string Region { set; get; }
        public string Center { set; get; }
        public string EquipmentType { set; get; }
        public string ADSLEquipmentID { set; get; }
        public string Name { set; get; }
        public string EquipmentIDCount { set; get; }

    }
    public class ADSLStatisticsGroup
    {
        public string CenterID { set; get; }
        public string RegionID { set; get; }
        public string RequestStepID { set; get; }
        public string RequestStepTitle { set; get; }
        public string Count { set; get; }
        public long RequestNo { get; set; }
    }
    public class ADSLStatisticsInfo
    {
        public string CenterName { set; get; }
        public string RegionName { set; get; }
        public string StepID { set; get; }
        public string ADSLRequestCount { set; get; }
        public string ADSLTaskOfCustomerCount { set; get; }
        public string ADSLAssignmentCount { set; get; }
        public string ADSLMDFCount { set; get; }
        public string ADSLOMCCount { set; get; }
        public string ADSLSetupCount { set; get; }
        public string ADSLTaskOfCustomerManagerCount { set; get; }
        public string ADSLCenterManager { set; get; }
        public int Count { set; get; }
    }
    public class InvestigatePossibilityRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public DateTime? MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public DateTime? WiringDate { set; get; }
        public string WiringHour { set; get; }
        public long ConnectionID { set; get; }
        public long? TelephoneNo { set; get; }
        public DateTime? ConnectionReserveDate { set; get; }
        public int? HasReport { set; get; }
        public int? HasWaitingInfo { set; get; }
        public int InputNumber { set; get; }
        public int? CabinetNumber { set; get; }
        public int ConnectionNo { set; get; }
        public byte? ConnectionType { set; get; }
        public int Number { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public byte AddressTypeID { set; get; }
        public string InstallAddress { set; get; }
        public string InstallPostalcode { set; get; }
        public string CorrespondenceAddress { set; get; }
        public string CorrespondencePostalCode { set; get; }
        public string CustomerName { set; get; }



    }
    public class InvestigatePossibilityInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string WiringDate { set; get; }
        public string WiringHour { set; get; }
        public string ConnectionID { set; get; }
        public string TelephoneNo { set; get; }
        public string ConnectionReserveDate { set; get; }
        public string HasReport { set; get; }
        public string HasWaitingInfo { set; get; }
        public string InputNumber { set; get; }
        public string CabinetNumber { set; get; }
        public string ConnectionNo { set; get; }
        public string ConnectionType { set; get; }
        public string Number { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string InstallAddress { set; get; }
        public string InstallPostalcode { set; get; }
        public string CorrespondenceAddress { set; get; }
        public string CorrespondencePostalCode { set; get; }
        public string CustomerName { set; get; }
    }

    public class ReportTemplateInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string IconName { get; set; }
        public string Category { get; set; }
    }

    public class IssueWiringInfo
    {
        //دایری-دایری مجدد
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string ConnectionID { set; get; }
        public string TelephoneNo { set; get; }
        public string InputNumber { set; get; }
        public string CabinetNumber { set; get; }
        public string ConnectionNo { set; get; }
        public string ConnectionType { set; get; }
        public string Number { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string WiringTypeID { get; set; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string PortNo { get; set; }
        public string SwitchPreNo { get; set; }
        public string SwitchCode { get; set; }
        public string LastPrintDate { get; set; }
    }

    public class PAPInfoUserInfo
    {
        public int ID { get; set; }
        public int PAPInfoID { get; set; }
        public string PAPInfo { get; set; }
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? InstallRequestNo { get; set; }
        public int? DischargeRequestNo { get; set; }
        public int? ExchangeRequestNo { get; set; }
        public int? FeasibilityNo { get; set; }
        public bool IsEnable { get; set; }

    }

    public class ADSLSellerAgentUserInfo
    {
        public int ID { get; set; }
        public int SellerAgentID { get; set; }
        public string SellerAgentTitle { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
        public bool IsAdmin { get; set; }
        public string CreditCash { get; set; }
        public string CreditCashUse { get; set; }
        public string CreditCashRemain { get; set; }
    }

    public class E1Info
    {
        public long RequestID { get; set; }
        public string RequestDate { get; set; }
        public string TelephoneTypeTitle { get; set; }
        public string TelephoneGroupTitle { get; set; }
        public string LinkTypeName { get; set; }
        public string ConnectionNo { get; set; }
        public string LineType { get; set; }

        /// <summary>
        /// چون نیاز بود که مرکز انتخاب شده در بخش اطلاعات درخواست و مرکز درخواست در حال توسعه یکسان باشند
        /// و باید بر روی این موضوع در هنگام ذخیره درخواست فضاپاور اجبار صورت میگرفت
        /// ستون زیر تعریف شد
        /// previousSpaceAndPowerInfo.CenterID = RequestForm.CenterComboBox.SelectedValue
        /// </summary>
        public int CenterID { get; set; }

        public string CenterName { get; set; }
        public string CityName { get; set; }
    }

    /// <summary>
    /// کلاس زیر برای پر کردن کنترل ها در فرم روال فضا و پاور ایجاد شد. از این کلاس زمانی استفاده میشود که کاربر در فرم درخواست
    /// فضا وپاور قصد توسعه درخواستهای قبلی را داشته باشد که باید در آن صورت جزئیات فضاپاور درخواست در حال توسعه برای درخواست جدید لود شود. البته نوع
    /// داده ها باید مشابه ستونهای دیتا بیس باشد
    /// </summary>
    public class SpaceAndPowerEquivalentDatabaseTableInfo
    {
        public byte EquipmentType { get; set; }
        public string EquipmentWeight { get; set; }
        public byte SpaceType { get; set; }
        public string SpaceSize { get; set; }
        public string SpaceUsage { get; set; }
        public string HeatWasteRate { get; set; }
        public string Duration { get; set; }
        public string RigSpace { get; set; }
        public bool HasFibre { get; set; }
        public bool HasAntenna { get; set; }
        public List<PowerType> PowerTypes { get; set; }
        public Antenna Antenna { get; set; }
    }

    public class SpaceAndPowerInfo
    {
        public long ID { get; set; }
        public long RequestID { get; set; }
        public string RequestDate { get; set; }
        public string SpaceAndPowerCustomer { get; set; }
        public string SpaceSize { get; set; }
        public string SpaceType { get; set; }
        //public string EquipmentType { get; set; }
        public byte EquipmentType { get; set; }
        public string EquipmentWeight { get; set; }
        public string SpaceUsage { get; set; }
        public string PowerType { get; set; }
        public string PowerRate { get; set; }
        public string HeatWasteRate { get; set; }
        public string Duration { get; set; }
        public string RequestDescription { get; set; }
        public Guid? EnteghalFile { get; set; }
        public string EnteghalDate { get; set; }
        public string EnteghalUser { get; set; }
        public string EnteghalComment { get; set; }
        public string SakhtemanDate { get; set; }
        public string SakhtemanUser { get; set; }
        public string SakhtemanComment { get; set; }
        public string FazaDate { get; set; }
        public string FazaUser { get; set; }
        public string FazaComment { get; set; }
        public Guid? NirooFile { get; set; }
        public string NirooDate { get; set; }
        public string NirooUser { get; set; }
        public string NirooComment { get; set; }
        public string ModireMohandesiDate { get; set; }
        public string ModireMohandesiUser { get; set; }
        public string ModireMohandesiComment { get; set; }
        public string ModireMantagheDate { get; set; }
        public string ModireMantagheUser { get; set; }
        public string ModireMantagheComment { get; set; }
        public string GhardadDate { get; set; }
        public string GhardadUser { get; set; }
        public string GhardadComment { get; set; }
        public string HerasatDate { get; set; }
        public string HerasatUser { get; set; }
        public string HerasatComment { get; set; }
        public string ManagerDate { get; set; }
        public string ManagerUser { get; set; }
        public string ManagerComment { get; set; }
        public string SooratHesabDate { get; set; }
        public string SooratHesabUser { get; set; }
        public string SooratHesabComment { get; set; }
        public string FinancialScopeComment { get; set; }
        public string FinancialScopeDate { get; set; }
        public string FinancialScopeUser { get; set; }
        public string DesignManagerComment { get; set; }
        public string DesignManagerDate { get; set; }
        public string DesignManagerUser { get; set; }
        public string SwitchDesigningOfficeComment { get; set; }
        public string SwitchDesigningOfficeDate { get; set; }
        public string SwitchDesigningOfficeUser { get; set; }
        public Guid? SwitchDesigningOfficeFile { get; set; }
        public string DesignManagerFinalCheckComment { get; set; }
        public string DesignManagerFinalCheckDate { get; set; }
        public string DesignManagerFinalCheckUser { get; set; }
        public string NetworkAssistantComment { get; set; }
        public string NetworkAssistantDate { get; set; }
        public string NetworkAssistantUser { get; set; }

        /// <summary>
        /// توضیحات اداره نظارت تجهیزات مخابراتی
        /// </summary>
        public string AdministrationOfTheTelecommunicationEquipmentComment { get; set; }

        /// <summary>
        /// تاریخ بررسی توسط اداره نظارت تجهیزات مخابراتی
        /// </summary>
        public string AdministrationOfTheTelecommunicationEquipmentDate { get; set; }

        /// <summary>
        /// کاربر اداره نظارت تجهیزات مخابراتی
        /// </summary>
        public string AdministrationOfTheTelecommunicationEquipmentUser { get; set; }

        public string AdministrationOfTheTelecommunicationEquipmentOperationDate { get; set; }

        public string CableAndNetworkDesignOfficeComment { get; set; }
        public string CableAndNetworkDesignOfficeDate { get; set; }
        public string CableAndNetworkDesignOfficeUser { get; set; }
        public Guid? CableAndNetworkDesignOfficeFile { get; set; }
        public string DeviceHallComment { get; set; }
        public string DeviceHallDate { get; set; }
        public string DeviceHallUser { get; set; }
        public Guid? CircuitCommandFile { get; set; }
        public string RigSpace { get; set; }
        public string AntennaName { get; set; }
        public int? AntennaCount { get; set; }
        public string AntennaHeight { get; set; }

        public string HasAntenna { get; set; }
        public bool HasFibre { get; set; }

        /// <summary>
        /// چون نیاز بود که مرکز انتخاب شده در بخش اطلاعات درخواست و مرکز درخواست در حال توسعه یکسان باشند
        /// و باید بر روی این موضوع در هنگام ذخیره درخواست فضاپاور اجبار صورت میگرفت
        /// ستون زیر تعریف شد
        /// previousSpaceAndPowerInfo.CenterID = RequestForm.CenterComboBox.SelectedValue
        /// </summary>
        public int CenterID { get; set; }

        public string CenterName { get; set; }
        public string CityName { get; set; }
    }

    public class SpaceAndPowerReportInfo
    {

        public long intRequestNo { get; set; }
        public string RequestNo { set; get; }
        public string Region { get; set; }
        public string Center { get; set; }
        public string RequestDate { get; set; }
        public string RequestLetterDate { get; set; }
        public string RequestLetterNo { get; set; }
        public string CurrentStep { get; set; }

        public string SpaceAndPowerCustomer { get; set; }
        public string SpaceSize { get; set; }
        public string SpaceType { get; set; }
        public string EquipmentType { get; set; }
        public string EquipmentWeight { get; set; }
        public string SpaceUsage { get; set; }
        public string PowerType { get; set; }
        public string PowerRate { get; set; }
        public string HeatWasteRate { get; set; }
        public string Duration { get; set; }
        public string RequestDescription { get; set; }
    }

    /// <summary>
    /// .این کلاس برای مدل در چاپ گواهی صدور صورتحساب دوماهه روال فضا و پاور ایجاد شده است
    /// </summary>
    public class SpaceAndPowerInvoiceIssuanceCertificateInfo
    {
        public CustomerReportInfo SpaceAndPowerCustomer { get; set; }

        public long? AddressID { get; set; }

        public long CustomerID { get; set; }

        public string CityName { get; set; }

        public string ProvinceName { get; set; }

        public string RegionName { get; set; }

        public string SpaceSize { get; set; }

        /// <summary>
        /// اجاره ماهیانه فضا
        /// </summary>
        public string SpaceMonthlyRent { get; set; }

        public string PowerRate { get; set; }


        /// <summary>
        /// اجاره ماهیانه برق
        /// </summary>
        public string PowerMonthlyRent { get; set; }

        public int? AntennaCount { get; set; }


        /// <summary>
        /// اجاره ماهیانه آنتن
        /// </summary>
        public string AntennaMonthlyRent { get; set; }

        /// <summary>
        /// تاریخ بهره برداری 
        /// </summary>
        public DateTime? AllocationDate { get; set; }

        /// <summary>
        /// تاریخ بهره برداری - به صورت رشته
        /// </summary>
        public string AllocationDateString { get; set; }

        /// <summary>
        /// مدت استفاده به روز
        /// </summary>
        public string NumberOfUsing { get; set; }

        /// <summary>
        /// مبلغ کل بدون احتساب مالیات و عوارض
        /// </summary>
        public string TotalAmountWithoutTaxVAT { get; set; }

        /// <summary>
        /// ملاحظات عوارض - مالیات
        /// </summary>
        public string RemarkTaxCharges { get; set; }

        public string PostalCode { get; set; }

        public string AddressContent { get; set; }
    }

    public class PowerTypeInfo
    {
        public string Title { get; set; }

        public string Rate { get; set; }
    }

    /// <summary>
    /// .این کلاس برای مدل در چاپ گواهی صدور صورتحساب دوماهه روال ایوان - سیم ایجاد شده است
    /// </summary>
    public class E1InvoiceIssuanceCertificateInfo
    {
        public CustomerReportInfo E1Customer { get; set; }

        public long? InstallAddressID { get; set; }

        public string Origination { get; set; }

        public string Destination { get; set; }

        public long? TargetInstallAddressID { get; set; }

        public long CustomerID { get; set; }

        public string CityName { get; set; }

        public string ProvinceName { get; set; }

        public string RegionName { get; set; }

        public string LinkType { get; set; }

        public string Unit { get; set; }

        public string HasProtection { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public string DeliveryDateString { get; set; }

        public string MonthlyPriceWithoutTaxVAT { get; set; }

        public string RemarkTaxCharges { get; set; }

        public string ProtectionPrice { get; set; }

        public string TotalAmount { get; set; }

        public int? NumberOfLine { get; set; }
    }

    public class E1CertificateReportInfo
    {
        public string CityName { get; set; }

        public string CenterName { get; set; }

        public string CustomerName { get; set; }

        public string NationalCodeOrRecordNo { get; set; }

        public string TelephoneGroupTitle { get; set; }

        public string TelephoneTypeTitle { get; set; }

        public string LinkTypeName { get; set; }

        public string CodeTypeName { get; set; }

        public string ChanalType { get; set; }

        public string ConnectionNo { get; set; }

        public string CompanyCode { get; set; }

        public string LineType { get; set; }

        public string SourcePostalCode { get; set; }

        public string SourceAddress { get; set; }

        public string TargetPostalCode { get; set; }

        public string TargetAddress { get; set; }

        public string CorrespondencePostalCode { get; set; }

        public string CorrespondenceAddress { get; set; }

        public int? NumberOfLine { get; set; }
    }

    public class CustomerReportInfo
    {
        public long ID { get; set; }

        public string CustomerName { get; set; }

        public string ProvinceName { get; set; }

        public string CityName { get; set; }

        public string RegionName { get; set; }

        public string AddressContent { get; set; }

        public string PostalCode { get; set; }

        public string UrgentTelNo { get; set; }

        public long? AddressID { get; set; }

        /// <summary>
        /// کد اقتصادی
        /// </summary>
        public string NationalID { get; set; }

        public string NationalCodeOrRecordNo { get; set; }
    }

    public class IssueWiringRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public long ConnectionID { set; get; }
        public long? TelephoneNo { set; get; }
        public int InputNumber { set; get; }
        public string CabinetNumber { set; get; }
        public int ConnectionNo { set; get; }
        public byte? ConnectionType { set; get; }
        public int Number { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }

        public byte WiringTypeID { get; set; }
        public bool IsPrinted { get; set; }
        public byte PrintCount { get; set; }
        public string PortNo { get; set; }
        public long SwitchPreNo { get; set; }
        public int? SwitchCode { get; set; }

        public DateTime? LastPrintDate { get; set; }

    }
    public class WiringListInfo
    {

        public string ID { set; get; }
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string WiringDate { set; get; }
        public string WiringHour { set; get; }
        public long? TelephoneNo { set; get; }
    }

    public class ExchangeInfoDetails
    {
        public string RequestID { set; get; }
        public string OldConnectionID { set; get; }
        public string NewConnectionID { set; get; }
        public string OldBucht { set; get; }
        public string NewBucht { set; get; }
        public string OldBuchtType { set; get; }
        public string NewBuchtType { set; get; }
        public string PostID { set; get; }
        public string RequestType { set; get; }
        public string WiringNo { set; get; }
        public string TelephoneNo { set; get; }

    }
    public class ExchangeRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public int OldCabinetID { set; get; }
        public int NewCabinetID { set; get; }
        public int OldPostID { set; get; }
        public int NewPostID { set; get; }
        public DateTime? AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }
        public DateTime? ConnectionType { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public byte WiringTypeID { get; set; }
        public bool IsPrinted { get; set; }
        public byte PrintCount { get; set; }
        public long? TelephoneNo { set; get; }
        public DateTime? LastPrintDate { get; set; }

    }
    public class ExchangeInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string ConnectionType { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string WiringTypeID { get; set; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }

        public string OldCabinetID { set; get; }
        public string NewCabinetID { set; get; }
        public string OldPostID { set; get; }
        public string NewPostID { set; get; }
        public string AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }
        public string TelephoneNo { set; get; }
    }
    public class ExchangeMDFRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public byte WiringTypeID { get; set; }
        public bool IsPrinted { get; set; }
        public byte PrintCount { get; set; }
        public DateTime? LastPrintDate { get; set; }

        public string CabinetNumberT { set; get; }
        public string CabinetNumberF { set; get; }
        public long FirstNewBuchtID { set; get; }
        public long LastNewBuchtID { set; get; }
        public DateTime? AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }
        public int InputNumberT { set; get; }
        public int InputNumberF { set; get; }
        public long? TelephoneNo { set; get; }

    }
    public class ExchangeMDFInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string ConnectionType { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string RequestID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string WiringTypeID { get; set; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }

        public string CabinetNumberT { set; get; }
        public string CabinetNumberF { set; get; }
        public string FirstNewBuchtID { set; get; }
        public string LastNewBuchtID { set; get; }
        public string AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }
        public string InputNumberT { set; get; }
        public string InputNumberF { set; get; }
        public string TelephoneNo { set; get; }
    }
    public class ExchangeCabinetRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public byte WiringTypeID { get; set; }
        public bool IsPrinted { get; set; }
        public byte PrintCount { get; set; }
        public DateTime? LastPrintDate { get; set; }

        public int FromOldInputNumber { set; get; }
        public int FromNewInputNumber { set; get; }
        public int ToOldInputNumber { set; get; }
        public int ToNewInputNumber { set; get; }

        public long OldFirstBuchtID { set; get; }
        public long NewFirstBuchtID { set; get; }
        public long OldLastBuchtID { set; get; }
        public long NewLastBuchtID { set; get; }

        public string OldCabinetNumber { set; get; }
        public string NewCabinetNumber { set; get; }

        public DateTime? AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }
        public long? TelephoneNo { set; get; }


    }
    public class ExchangeCabinetInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string WiringTypeID { get; set; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }
        //ورودی کافو
        public string FromOldInputNumber { set; get; }
        public string FromNewInputNumber { set; get; }
        public string ToOldInputNumber { set; get; }
        public string ToNewInputNumber { set; get; }
        //بوخت 
        public string OldFirstBuchtID { set; get; }
        public string NewFirstBuchtID { set; get; }
        public string OldLastBuchtID { set; get; }
        public string NewLastBuchtID { set; get; }
        //کافو
        public string OldCabinetNumber { set; get; }
        public string NewCabinetNumber { set; get; }

        public string AccomplishmentDate { set; get; }
        public string AccomplishmentTime { set; get; }

        public string TelephoneNo { set; get; }
    }
    public class DischargeRaw
    {
        public string WiringNo { set; get; }
        public DateTime? WiringIssueDate { set; get; }
        public string CounterNo { set; get; }
        public DateTime InsertDate { set; get; }
        public long ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public bool IsPrinted { get; set; }
        public byte PrintCount { get; set; }
        public DateTime? LastPrintDate { get; set; }

        public DateTime? MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public DateTime? WiringDate { set; get; }
        public string WiringHour { set; get; }

        public string MDFHorizentalID { set; get; }
        public long BuchtID { set; get; }
        public long TelephoneNo { get; set; }
        public byte? TakePossessionReason { get; set; }

    }
    public class DischargeInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string CounterNo { set; get; }
        public string InsertDate { set; get; }
        public string ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }

        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string WiringDate { set; get; }
        public string WiringHour { set; get; }

        public string MDFHorizentalID { set; get; }
        public string BuchtID { set; get; }
        public string TelephoneNo { get; set; }
        public string TakePossessionReason { get; set; }
    }
    public class ChangeLocationInfo
    {
        public string CounterNo { set; get; }
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string InsertDate { set; get; }
        public string ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }
        public string OldTelephoneNo { set; get; }
        public string NewTelephoneNo { set; get; }
        public string OldBuchtID { set; get; }
        public string NewBuchtID { set; get; }
        public string OldBuchtType { set; get; }
        public string NewBuchtType { set; get; }
        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string WiringDate { set; get; }
        public string WiringHour { set; get; }
        public string OldCustomerAddress { set; get; }
        public string NewCustomerAddress { set; get; }
    }
    public class CenterToCenterTranslationInfo
    {
        public long ID { get; set; }

        public int OldCabinetID { get; set; }
        public int NewCabinetID { get; set; }

        public string OldCabinetName { get; set; }
        public string NewCabinetName { get; set; }

        public string TargetCenterName { get; set; }
        public string SourceCenterName { get; set; }

        public long? FromOldCabinetInputID { get; set; }
        public long? ToOldCabinetInputID { get; set; }

        public string FromOldCabinetInputName { get; set; }
        public string ToOldCabinetInputName { get; set; }

        public long? FromNewCabinetInputID { get; set; }
        public long? ToNewCabinetInputID { get; set; }

        public string FromNewCabinetInputName { get; set; }
        public string ToNewCabinetInputName { get; set; }

        public DateTime InsertDate { get; set; }
        public DateTime? AccomplishmentDate { get; set; }
        public string AccomplishmentTime { get; set; }
        public string StatusTitle { get; set; }
        public string requestLetterNo { get; set; }
    }
    public class ChangeNoInfo
    {
        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string InsertDate { set; get; }
        public string ChangeNoDate { set; get; }
        public string ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }
        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string OldTelephoneNo { set; get; }
        public string NewTelephoneNo { set; get; }
        public string OldBuchtID { set; get; }
        public string NewBuchtID { set; get; }
        public string OldBuchtType { set; get; }
        public string NewBuchtType { set; get; }
        public string OldTelCounterNo { set; get; }
        public string NewTelCounterNo { set; get; }
        public string ChangeReasonID { set; get; }
        public string OldSwitchPortID { set; get; }
        public string NewSwitchPortID { set; get; }
        public string OldPortNo { set; get; }
        public string NewPortNo { set; get; }
        public string OldMDFHorizentalID { set; get; }
        public string NewMDFHorizentalID { set; get; }
        public string Address { get; set; }
        public DateTime? InsertDatedate { get; set; }
        public string CauseOfChangeNo { get; set; }
        public long CustomerID { get; set; }
        public long AddressID { get; set; }
        public string EndDate { get; set; }
        public string PostalCode { get; set; }
        public string MelliCode { get; set; }
        public string Description { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }

        public int CenterID { get; set; }

        public int CityID { get; set; }

        public string CityName { get; set; }

        public string PersianChangeNoDate { get; set; }
    }
    public class RefundDespositInfo
    {

        public string WiringNo { set; get; }
        public string WiringIssueDate { set; get; }
        public string InsertDate { set; get; }
        public string ID { set; get; }
        public string Center { set; get; }
        public string Region { set; get; }
        public string CustomerName { set; get; }
        public string IsPrinted { get; set; }
        public string PrintCount { get; set; }
        public string LastPrintDate { get; set; }

        public string MDFWiringDate { set; get; }
        public string MDFWiringHour { set; get; }
        public string WiringDate { set; get; }
        public string WiringHour { set; get; }

        public string TelephoneNo { set; get; }



        public string ConfirmRecord { set; get; }
        public string SwitchPortID { set; get; }
        public string PortNo { set; get; }

        public string BuchtID { set; get; }
        public string Post { set; get; }
        public string PostContact { set; get; }
        public string Cabinet { set; get; }
        public string CabinetInput { set; get; }

        public string TelCounterNo { set; get; }

    }
    public class SpecialServiceInfo
    {
        public long intRequestNo { get; set; }
        public string RequestNo { set; get; }
        public string Region { get; set; }
        public string Center { get; set; }
        public string CustomerName { set; get; }
        public string RequestDate { get; set; }
        public string RequestLetterDate { get; set; }
        public string RequestLetterNo { get; set; }
        public string CurrentStep { get; set; }
        public List<int> SpecialServiceTypesId { set; get; }
        public string TelephoneNo { set; get; }
        public string InstallDate { set; get; }
        public string UnInstalDate { set; get; }
        public string Status { set; get; }
        public string SpecialServiceTitle { get; set; }
        public string CityName { get; set; }
        public System.Xml.Linq.XElement SpecialServiceTypeIdsXml { get; set; }

        /// <summary>
        /// شماره نامه
        /// </summary>
        public string LetterNo { get; set; }

        /// <summary>
        /// مرجع درخواست کننده
        /// </summary>
        public string SpecialServiceRequestReference { get; set; }

        public string InsertDate { get; set; }

        public SpecialServiceInfo()
        {
            this.SpecialServiceTypesId = new List<int>();
        }
    }

    public class ChangeLocationProcessInfo
    {

        public long intRequestNo { get; set; }
        public string RequestNo { set; get; }
        public string Region { get; set; }
        public string Center { get; set; }
        public string CustomerName { set; get; }
        public string NewCustomer { set; get; }
        public string RequestDate { get; set; }
        public string RequestLetterDate { get; set; }
        public string RequestLetterNo { get; set; }
        public string CurrentStep { get; set; }
        public string Equipment { set; get; }
        public string BuchtIDReserve { set; get; }
        public string BuchtIDOld { set; get; }
        public string OldCustomerAddressID { set; get; }
        public string NewCustomerAddressID { set; get; }
        public string OldTelephone { set; get; }
        public string NewTelephone { set; get; }
        public string NearestTelephon { set; get; }
        public string CountorID { set; get; }
        public string SourceCenter { set; get; }
        public string TargetCenter { set; get; }
        public string OldCounterNo { get; set; }
        public string NewCounterNo { get; set; }
        public string ChangeLocationType { get; set; }
    }
    public class CounterLastInfo
    {
        public long ID { get; set; }
        public string CounterNo { get; set; }
        public DateTime CounterReadDate { get; set; }
        public long TelephonNo { get; set; }
        public string Local { get; set; }
        public string NonLocal { get; set; }
        public string International { get; set; }
        public string IA { get; set; }
        public string BisTalk { get; set; }
        public DateTime? InsertDate { get; set; }
    }

    public class FolderMenu
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public bool? IsChecked { get; set; }
    }

    public class FolderMenuInfo
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public bool? IsChecked { get; set; }
        public string GroupName { get; set; }
        public string Header { get; set; }
        public string Icon { get; set; }
    }

    public class BlackListInfo
    {
        public long ID { get; set; }
        public byte TypeMember { get; set; }
        public string TelephoneNo { get; set; }
        public string CustomerNationalCode { get; set; }
        public string AddressPostalCode { get; set; }
        public byte ReasonID { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }
        public DateTime? ArrestLetterNoDate { get; set; }
    }

    public class BlackListAddressInfo
    {
        public long ID { get; set; }
        public string Reason { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string Center { get; set; }

        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }

        public string CreatorUser { get; set; }
        public string ExitUser { get; set; }
        public DateTime? ArrestLetterNoDate { get; set; }
    }

    public class BlackListCustomerInfo
    {
        public long ID { get; set; }
        public string Reason { get; set; }
        public string PersonType { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string BirthCertificateID { get; set; }
        public string BirthDateOrRecordDate { get; set; }
        public string IssuePlace { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }

        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }

        public string CreatorUser { get; set; }
        public string ExitUser { get; set; }
        public DateTime? ArrestLetterNoDate { get; set; }
    }

    public class BlackListTelephoneInfo
    {
        public long ID { get; set; }
        public string Reason { get; set; }
        public long TelephoneNo { get; set; }
        public string SwitchPrecode { get; set; }
        public string SwitchPort { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsRound { get; set; }
        public string Center { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }

        public string CreatorUser { get; set; }
        public string ExitUser { get; set; }
        public DateTime? ArrestLetterNoDate { get; set; }
    }
    public class CenterCabinetInfo
    {
        public int ID { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public int? CabinetNumber { get; set; }
        public string ABType { get; set; }
        public string CabinetCode { get; set; }
        public string CabinetTypeID { get; set; }
        public string FirstPostID { get; set; }
        public string LastPostID { get; set; }
        public string CabinetUsageType { get; set; }
        public string FromInputNo { get; set; }
        public string ToInputNo { get; set; }

        public string DistanceFromCenter { get; set; }
        public string IsOutBound { get; set; }
        public string OutBoundMeter { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string FromPostalCode { get; set; }
        public string ToPostalCode { get; set; }
        public string SwitchID { get; set; }
        public string Status { get; set; }
        public string Capacity { get; set; }

        public int? CabinetInputCount { get; set; }

        public int? ReservedPostCount { set; get; }

        public string CustomerID { get; set; }
        public string TelephoneNo { get; set; }

        public string Bucht { set; get; }
        public string BuchtIDConnected { get; set; }
        public string BuchtIDConnectedOtherBucht { get; set; }
        public string PCM { set; get; }
        public string BuchtInput { get; set; }
        public string BuchtOutput { get; set; }
        public string MDFHorizintalID { get; set; }
        public string CustomerName { get; set; }
        public int? BuchtType { get; set; }
        public string BuchtTypeName { get; set; }
        public long? CabinetInputID { get; set; }
        public byte? BuchtStatus { get; set; }
        public int? CabinetInputNumber { set; get; }
        public int? PostNumber { get; set; }
        public int? PostContactNumber { get; set; }
        public string Radif { get; set; }
        public string Tabaghe { get; set; }
        public string FromBuchtNo { get; set; }
        public string ToBuchtNo { get; set; }

        public int RemainedQuotaReservation { get; set; }
        public int QuotaReservation { get; set; }
        public double PostCount { get; set; }
        public double ActivePostCount { get; set; }
        public double DeactivePostCount { get; set; }
        public int InputCount { get; set; }
        public int ActiveInputCount { get; set; }
        public int DeactiveInputCount { get; set; }
        public int BrokenInputCount { get; set; }
        public int InputReservation { get; set; }
        public int WaitingListCount { get; set; }
        public int PCMCount { get; set; }
        public int ADSLCount { get; set; }

    }

    public class PostInfo
    {
        public int ID { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string CabinetID { get; set; }
        public int Post { get; set; }
        public int? CabinetInput { get; set; }
        public long? CabinetInputID { get; set; }
        public string ABType { get; set; }
        public string PostTypeName { get; set; }
        public int PostTypeID { get; set; }
        public string PostGroupID { get; set; }
        public int Number { get; set; }
        public string Distance { get; set; }
        public string IsOutBorder { get; set; }
        public string OutBorderMeter { get; set; }
        public string PostalCode { get; set; }
        public string PostGroupNo { get; set; }
        public string Address { get; set; }
        public int? CabinetNumber { get; set; }


        public string FromPostContact { get; set; }
        public string ToPostContact { get; set; }

        public string Status { get; set; }
        public int? PostContact { get; set; }

        public long? PostContactID { get; set; }

        public string Capacity { get; set; }

        public int? PostContactCountFail { get; set; }
        public int? PostContactCountValid { get; set; }

        public int PostContactStatus { get; set; }
        public Guid? DocumentFileID { get; set; }
        public string TelNo { get; set; }
        public string CustomerName { get; set; }
        public int FillCabinetInputCount { get; set; }
        public int FillPortCount { get; set; }

        //TODO:rad
        public string PostContactConnectionType { get; set; }

        public string HasADSL { get; set; }

        public int FreePostContactCount { get; set; }

        public int ActivePostContactCount { get; set; }

        public string AdjacentPost { get; set; }

    }
    public class PostInfoFill
    {
        public int? ID { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string CabinetID { get; set; }
        public int? CabinetInput { get; set; }
        public long? CabinetInputID { get; set; }
        public string PostTypeName { get; set; }
        public int? Number { get; set; }
        public int? CabinetNumber { get; set; }


        public string Status { get; set; }
        public int? PostContact { get; set; }
        public int? PostContactCountFail { get; set; }
        public int? PostContactCountValid { get; set; }

        public int? PostContactStatus { get; set; }
        public int? FillPostContactCount { get; set; }
        public int? FillPortCount { get; set; }
        public int? EmptyPostContactCount { get; set; }
        public int? FailPostContactCount { get; set; }

        public int? PCMCount { get; set; }
        public int? KarkonCount { get; set; }
        public int? PCMKarkonCount { get; set; }
        public int? RemainRequestCount { get; set; }

        public int? ReservedPostContactCount { get; set; }
    }
    public class CabinetInputReport
    {
        public long ID { get; set; }
        public string CabinetID { get; set; }
        public int InputNumber { get; set; }
        public string InsertDate { get; set; }
        public string Status { get; set; }
        public byte boolStatus { get; set; }
        public string Direction { get; set; }
        public string Bucht { get; set; }
        public string TelNo { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string BuchtID { get; set; }
        public byte? BuchtStatus { get; set; }
        public long? CabinetInputID { get; set; }
        public string BuchtIDConnectedOtherBucht { get; set; }

    }

    public class PCMCardInfo
    {
        public bool IsChecked { get; set; }
        public int ID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public int RockNumber { get; set; }
        public int ShelfNumber { get; set; }
        public int Card { get; set; }
        public int PCMBrandID { get; set; }
        public int PCMTypeID { get; set; }
        public string PCMBrandName { get; set; }
        public string InsertDate { get; set; }
        public string PCMTypeName { get; set; }
        public string InstallAddress { get; set; }
        public string InstallPostCode { get; set; }
        public byte Status { get; set; }
        public int PCMRockID { get; set; }
        public int PCMShelfID { get; set; }
        public int NumberConnectionFills { get; set; }
        public int? CabinetNumber { get; set; }
        public int? PostNumber { get; set; }
        public int? ConnectionNo { get; set; }
        public int PortNumber { get; set; }
        public long PostContactID { get; set; }
        public int? PCMCabinetInputColumnNo { get; set; }
        public int? PCMCabinetInputRowNo { get; set; }
        public long? PCMCabinetInputBuchtNo { get; set; }
        public List<SubCustomerOfPCM> CustomerOfPCM { get; set; }
    }

    public class PCMStatisticsInfo
    {
        public string CustomerName { get; set; }
        public long? TelephoneNo { get; set; }
        public int? Port { get; set; }
        public int? ColumnNo { get; set; }
        public int? RowNo { get; set; }
        public long? BuchtNo { get; set; }
        public int ID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public int RockNumber { get; set; }
        public int ShelfNumber { get; set; }
        public int Card { get; set; }
        public int PCMBrandID { get; set; }
        public int PCMTypeID { get; set; }
        public string PCMBrandName { get; set; }
        public string PCMTypeName { get; set; }
        public string InstallAddress { get; set; }
        public string InstallPostalCode { get; set; }
        public byte Status { get; set; }
        public int PCMRockID { get; set; }
        public int PCMShelfID { get; set; }
        public int NumberConnectionFiles { get; set; }
        public int? CabinetNumber { get; set; }
        public int? PostNumber { get; set; }
        public int? ConnectionNo { get; set; }
        public long PostContactID { get; set; }
        public int? PCMCabinetInputColumnNo { get; set; }
        public int? PCMCabinetInputRowNo { get; set; }
        public long? PCMCabinetInputBuchtNo { get; set; }
    }

    public class CablePairInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public long ID { get; set; }
        public long CableID { get; set; }
        public int CableNumber { get; set; }
        public int CablePairNumber { get; set; }
        public byte Status { get; set; }
        public DateTime InsertDate { get; set; }
        public string Connection { get; set; }
    }

    public class PCMDetails
    {
        public long ID { get; set; }
        public int? PostID { get; set; }
        public long? CabinetID { get; set; }
        public string BuchtType { get; set; }
        public string PortNumber { get; set; }
        public byte? boolStatus { get; set; }
        public string Status { get; set; }
        public string Bucht { get; set; }
        public string TelNo { get; set; }
        public string BuchtID { get; set; }
        public string BuchtStatus { get; set; }
        public string MUID { get; set; }
        public long? PostContactID { get; set; }
        public int? ConnectionNo { get; set; }
        public long? CabinetInputID { get; set; }
        public int BuchtTypeID { get; set; }
        public string BuchtTypeName { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string CustomerID { get; set; }
        public string PostCode { get; set; }
    }
    public class PostContactBuchtPortInfo
    {
        public PostContact PostContact { get; set; }
        public Bucht Bucht { get; set; }
        public PCMPort PCMPort { get; set; }
        public PCM PCM { get; set; }

    }

    public class VerticalMDFRowInfo
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string MDFNumberWithDescription { get; set; }
        public int? MDFFrameNo { get; set; }
        public int VerticalMDFColumnNo { get; set; }
        public int ID { get; set; }
        public int VerticalMDFColumnID { get; set; }
        public int VerticalRowNo { get; set; }
        public int RowCapacity { get; set; }
    }

    public class PostContactInfo
    {
        public long ID { get; set; }
        public int CabinetID { get; set; }
        public int? CabinetNumber { get; set; }
        public int? CabinetInputNumber { get; set; }
        public long? CabinetInputID { get; set; }
        public long? TelephoneNo { get; set; }
        public int PostID { get; set; }
        public int PostNumber { get; set; }
        public string AdjacentPost { get; set; }
        public byte ABType { get; set; }
        public string ABTypeName { get; set; }
        public int ConnectionNo { get; set; }
        public byte? ConnectionType { get; set; }
        public string ConnectionTypeName { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
        public string AORBType { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public bool HasADSL { get; set; }

        //TODO:rad
        public string Bucht { get; set; }
        public string DateMalfunction { get; set; }
        public string TimeMalfunction { get; set; }
        public string Description { get; set; }

        public string TypeMalfunction { get; set; }

        public long? RequestID { get; set; }

        public string ToConnectiontype { get; set; }

        public int? BuchtTypeID { get; set; }

    }
    public class PCMStatisticDetails
    {
        public long? CustomerID { get; set; }
        public string MDFHorizentalID { get; set; }
        public string Telno { get; set; }
        public string Portno { get; set; }
        public string Connectionno { get; set; }
        public string Postno { get; set; }
        public string CabinetInputID { get; set; }
        public string Cabinet { get; set; }
        public string ConnectionIDBucht { get; set; }
        public string InputBucht { get; set; }
        public string OutputBucht { get; set; }
        public string PCMSpecification { get; set; }
        public int? BuchtType { get; set; }
        public long? BuchtID { get; set; }
        public int? RockID { get; set; }
        public int? ShelfID { get; set; }
        public int? CardID { get; set; }
        public int? PortID { get; set; }
        public string PCMType { get; set; }
        public string PCMStatus { get; set; }
        public string InstallDate { get; set; }
        public string CenterName { get; set; }
        public string ConnectionSpecification { get; set; }
        public int? SwitchPortID { get; set; }
        public DateTime? InsertDate { get; set; }
        public string InsertDate_Time { get; set; }
        public string InsertDate_Date { get; set; }
        public string BuchtNo { get; set; }
        public string Radif { get; set; }
        public string Tabaghe { get; set; }
        public string Rock { get; set; }
        public string Shelf { get; set; }
        public string Card { get; set; }
        public string Port { get; set; }


    }

    public class CabinetInputInfo
    {
        public long ID { get; set; }
        public long? RequestID { get; set; }
        public int CabinetID { get; set; }
        public int CabinetNumber { get; set; }
        public int InputNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public byte Status { get; set; }
        public byte? Direction { get; set; }
        public string StatusName { get; set; }
        public string DirectionName { get; set; }
        public string Cable { get; set; }
        public string Bucht { get; set; }

        public int? ColumnNo { get; set; }
        public int? RowNo { get; set; }
        public long? BuchtNo { get; set; }
        public long? TelephoneNo { get; set; }
        public string DateMalfunction { get; set; }
        public string TimeMalfunction { get; set; }
        public string Description { get; set; }
        public string TypeMalfunction { get; set; }
        public string BuchtStatus { get; set; }
    }


    public class GroupingCabinetInput : ICloneable
    {
        public long ID { get; set; }
        public int? CabinetID { get; set; }
        public int CabinetNumber { get; set; }
        public string MDF { get; set; }
        public int? FromInputNumber { get; set; }
        public int? ToInputNumber { get; set; }
        public int? VerticalCloumnNo { get; set; }
        public int? VerticalRowNo { get; set; }
        public long? FromBuchtNo { get; set; }
        public long? ToBuchtNo { get; set; }
        public string CableNumber { get; set; }
        public long CableID { get; set; }
        public int? FromCablePairNumber { get; set; }
        public int? ToCablePairNumber { get; set; }
        public decimal Diameter { set; get; }
        public int? Capacity { get; set; }
        public string BuchtTypeName { get; set; }

        public string CityName { get; set; }
        public string CenterName { get; set; }



        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class GroupingCablePair : ICloneable
    {
        public long ID { get; set; }
        public string MDF { get; set; }
        public int? MDFID { get; set; }
        public int? CableNumber { get; set; }
        public int? CableID { get; set; }
        public int? FromCablePairNumber { get; set; }
        public int? ToCablePairNumber { get; set; }
        public string VerticalCloumnNo { get; set; }
        public int? VerticalCloumnID { get; set; }
        public string VerticalRowNo { get; set; }
        public int? VerticalRowID { get; set; }
        public long? FromBuchtNo { get; set; }
        public long? ToBuchtNo { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class SubCustomerOfPCM
    {
        public int? Port { get; set; }
        public string Name { get; set; }
        public long? Telephone { get; set; }

        public int? ColumnNo { get; set; }
        public int? RowNo { get; set; }
        public long? BuchtNo { get; set; }
    }
    public class PostContactConnectionTypeReport
    {
        public long ID { set; get; }
        public int PostID { set; get; }
        public byte? ConnectionType { set; get; }

    }
    public class PostContactsReport
    {

        public long ID { get; set; }
        public long? BuchtID { get; set; }
        public string CabinetID { get; set; }
        public string PostNumber { get; set; }
        public string CabinetNumber { get; set; }
        public string InsertDate { get; set; }
        public string Status { get; set; }
        public bool? boolStatus { get; set; }
        public string TelNo { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string BuchtStatus { get; set; }
        public string PostContactConnectionType { get; set; }
        public string ConnectedToPCM { set; get; }
        public int? PostContactConnectionno { get; set; }
        public string Bucht { get; set; }
        public int PostID { get; set; }
        public string BuchtTypeID { get; set; }
        public long? CabinetInputID { get; set; }

        public DateTime? TelephoneDate { get; set; }
        public string TelephoneDatePersian { get; set; }
        public string TelephoneTime { get; set; }

        public string Description { get; set; }
    }

    public class TechnicalInfoFailure117
    {
        public int SwitchPortID { get; set; }
        public string CabinetNo { get; set; }
        public string CabinetInputNumber { get; set; }
        public string PostNo { get; set; }
        public string ConnectionNo { get; set; }
        public string RADIF { get; set; }
        public string TABAGHE { get; set; }
        public string ETESALII { get; set; }
        public bool IsPCM { get; set; }
        public string PCMPort { get; set; }
        public string PCMModel { get; set; }
        public string PCMType { get; set; }
        public string PCMRock { get; set; }
        public string PCMShelf { get; set; }
        public string PCMCard { get; set; }
        public string PCMInRadif { get; set; }
        public string PCMInTabaghe { get; set; }
        public string PCMInEtesali { get; set; }
        public string PCMOutRadif { get; set; }
        public string PCMOutTabaghe { get; set; }
        public string PCMOutEtesali { get; set; }
        public string BOOKHT_TYPE_NAME { get; set; }
        public string ADSLRadif { get; set; }
        public string ADSLTabaghe { get; set; }
        public string ADSLEtesali { get; set; }
        public bool HasAnotherBucht { get; set; }
    }

    public class FailureTimeReportInfo
    {
        public long ID { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime EndMDFDate { get; set; }
    }

    public class FailureStatusInfo
    {
        public int ID { get; set; }
        public string Parent { get; set; }
        public string Title { get; set; }
        public string Availablity { get; set; }
        public string ArchivedTime { get; set; }
        public bool? IsActive { get; set; }
    }

    public class FailureHistoryInfo
    {
        public long ID { get; set; }
        public string RowNo { get; set; }
        public string LineStatus { get; set; }
        public string FailureStatus { get; set; }
        public string InsertDate { get; set; }
        public string GetNetworkFormDate { get; set; }
        public string EndMDFDate { get; set; }
        public string PhoneNo { get; set; }
    }

    public class FailureFormInfo
    {
        public int ID { get; set; }
        public long RequestID { get; set; }
        public int RowNo { get; set; }
        public int? FailureStatusID { get; set; }
        public string FailureStatus { get; set; }
        public int? CableColorID1 { get; set; }
        public string CableColor1 { get; set; }
        public int? CableColorID2 { get; set; }
        public string CableColor2 { get; set; }
        public int CableTypeID { get; set; }
        public string CableType { get; set; }
        public int? NetworkOfficerID { get; set; }
        public string NetworkOfficer { get; set; }
        public byte? FailureSpeed { get; set; }
        public DateTime? GiveNetworkFormDate { get; set; }
        public string GiveNetworkFormTime { get; set; }
        public DateTime? GetNetworkFormDate { get; set; }
        public string GetNetworkFormTime { get; set; }
        public DateTime? SendToCabelDate { get; set; }
        public string SendToCabelTime { get; set; }
        public DateTime? CabelDate { get; set; }
        public string CabelTime { get; set; }
        public string Description { get; set; }

    }

    public class FailureFormRowInfo
    {
        public int ID { get; set; }
        public long RequestID { get; set; }
        public long TelephoneNo { get; set; }
        public string Center { get; set; }
        public string Customer { get; set; }
        public int? RowNo { get; set; }
        public string NetworkOfficer { get; set; }
        public string FailureStatus { get; set; }
        public string LineStatus { get; set; }
        public string InsertDate { get; set; }
        public string InsertDateString { get; set; }
        public string EliminateFailureDate { get; set; }
        public string EliminateNetworkFailureDate { get; set; }
        public string MDFDate { get; set; }
        public string MDFPerson { get; set; }
        public string NetworkDate { get; set; }
        public string EndMDFDate { get; set; }
        public string EndMDFPerson { get; set; }
        public string EndMDFDateString { get; set; }
        public string Step { get; set; }
        public string CabinetInputID { get; set; }
        public string CabinetNumber { get; set; }
        public string Post { get; set; }
        public string PostContact { get; set; }
        public int Count { get; set; }
        public string NetworkOfficerName { get; set; }
        public double MinDiff { set; get; }
        public double MinDiffNetwork { set; get; }
        public DateTime? GiveNetworkDate { get; set; }
        public DateTime? GetNetworkDate { get; set; }
        public string GiveNetworkDateString { get; set; }
        public string GetNetworkDateString { get; set; }
        public string SaloonDate { get; set; }
        public string SaloonUser { get; set; }
    }

    public class Failure117NetworkContractorOfficerInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Failure117RequestPrintInfo
    {
        public string ID { get; set; }
        public string TelephoneNo { get; set; }
        public string Bucht { get; set; }
        public string BuchtPCM { get; set; }
        public string BuchtADSL { get; set; }
        public string OtherBucht { get; set; }
        public string CabinetNo { get; set; }
        public string CabinetInputNumber { get; set; }
        public string PostNo { get; set; }
        public string ConnectionNo { get; set; }
        public string Date { get; set; }
        public string Count { get; set; }
    }

    public class PAPRequestPrintInfo
    {
        public string ID { get; set; }
        public string RequestType { get; set; }
        public string TelephoneNo { get; set; }
        public string Center { get; set; }
        public string PAPInfo { get; set; }
        public string PortNo { get; set; }
        public string RowNo { get; set; }
        public string ColumnNo { get; set; }
        public string BuchtNo { get; set; }
        public string Bucht { get; set; }
        public string BuchtADSL { get; set; }
        public string OldBuchtADSL { get; set; }
        public string Date { get; set; }
    }

    public class SwitchPortInfo
    {
        public long? Telephone { get; set; }
        public int ID { get; set; }
        public int SwitchID { get; set; }
        public string PortNo { get; set; }
        public string MDFHorizentalID { get; set; }
        public bool? Type { get; set; }
        public byte Status { get; set; }
        public string SwitchCode { get; set; }
    }

    public class TelInfo
    {
        public long TelephoneNo { get; set; }
        public long? switchPreNo { get; set; }
        public string portNo { get; set; }
        public int switchPortID { get; set; }
        public int? SwitchPrecodeID { get; set; }
        public byte? switchPreCodeType { get; set; }
        public int? switchID { get; set; }
        public bool? portType { get; set; }
        public int investigatePossibilityStatus { get; set; }
    }
    public class PCMInfoReport
    {
        public string CenterName { get; set; }
        public string Type { get; set; }
        public long? ConnectionID { get; set; }
        public string Rock { get; set; }
        public string Shelf { get; set; }
        public string Card { get; set; }

        public byte? Status { get; set; }
        public byte PCMActionID { get; set; }
        public string PCMActionName { get; set; }
        public DateTime? PCMActionDate { get; set; }
        public string strPCMActionDate { get; set; }
        public string PCMActionTime { get; set; }
        // public byte? NumberOFChannel { get; set; }
        public int PCMDeviceID { get; set; }
    }

    public class PostGroupInfo
    {

        public int ID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public int GroupNo { get; set; }
        public string Description { get; set; }
    }

    public class PCMPortInfo
    {
        public string PCM { get; set; }

        public int ID { get; set; }

        public int PCMID { get; set; }

        public int PortNumber { get; set; }

        public byte PortType { get; set; }

        public byte Status { get; set; }


        public int Shelf { get; set; }
        public int Rock { get; set; }

        public string PCMBrand { get; set; }

        public string PCMType { get; set; }

        public string Center { get; set; }
        public int CenterID { get; set; }


    }

    public class CableInfo
    {
        public long ID { get; set; }
        public int CenterID { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public int CableNumber { get; set; }
        public int CableTypeID { get; set; }
        public string CableTypeName { get; set; }
        public int CableUsedChannelID { get; set; }
        public string CableUsedChannelName { get; set; }
        public decimal CableDiameter { get; set; }
        public byte? PhysicalType { get; set; }
        public string PhysicalTypeName { get; set; }
        public int FromCablePairNumber { get; set; }
        public int ToCablePairNumber { get; set; }
        public byte Status { get; set; }
        public string StatusName { get; set; }
        public DateTime InsertDate { get; set; }
    }

    public class Failure117NetworkReport
    {
        public long RequestNo { get; set; }
        public string Radif { get; set; }
        public string RequestDate { get; set; }
        public string RequestDate1 { get; set; }
        public string PhoneNo { get; set; }
        public string Center { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public string CabinetNo { get; set; }
        public string CabinetinputNo { get; set; }
        public string PostNo { get; set; }
        public string PostEtesaliNo { get; set; }
        public string RadifBucht { get; set; }
        public string TabagheBucht { get; set; }
        public string EtesaliBucht { get; set; }
        public string MobileNo { get; set; }
        public string CallingNo { get; set; }
        public string IsPCM { get; set; }
        public string PortPCM { get; set; }
        public string ModelPCM { get; set; }
        public string TypePCM { get; set; }
        public string RockPCM { get; set; }
        public string ShelfPCM { get; set; }
        public string CardPCM { get; set; }
        public string RadifInputBucht { get; set; }
        public string TabagheInputBucht { get; set; }
        public string EtesaliInputBucht { get; set; }
        public string RadifOutputBucht { get; set; }
        public string TabagheOutputBucht { get; set; }
        public string EtesaliOutputBucht { get; set; }
        public string ADSLRadif { get; set; }
        public string ADSLTabaghe { get; set; }
        public string ADSLEtesali { get; set; }
        public string SpecialRadif { get; set; }
        public string SpecialTabaghe { get; set; }
        public string SpecialEtesali { get; set; }
        public string E1Radif { get; set; }
        public string E1Tabaghe { get; set; }
        public string E1Etesali { get; set; }
        public string LineStatus { get; set; }
        public string MDFUser { get; set; }
        public string MDFDate { get; set; }
        public string ColorCable { get; set; }
        public string CableType { get; set; }
        public string AdjacentTelephoneNo { get; set; }
        public string Description { get; set; }
        public string CabelDate { get; set; }
        public string SendToCabelDate { get; set; }
        public string EndMDFComment { get; set; }
        public string MDFSpeedMin { get; set; }

        public string NetworkOfficer { get; set; }
        public string ResultAfterReturn { get; set; }
        public string GiveNetworkFormDate { get; set; }
        public string GiveNetworkFormTime { get; set; }
        public string GetNetworkFormDate { get; set; }
        public string GetNetworkFormTime { get; set; }
        public string Rowno { get; set; }



        public string CheckBox1 { get; set; }    //سیم کشی داخلی
        public string CheckBox2 { get; set; }    //اصلاح توسط مشترک
        public string CheckBox3 { get; set; }    //"دستگاه تلفن" 
        public string CheckBox4 { get; set; }    //سالم پس از مراجعه
        public string CheckBox5 { get; set; }     //مشترک نبود، فرم تذکر داده شد

        public string CheckBox6 { get; set; }    //عدم تطبیق خط
        public string CheckBox7 { get; set; }    //کابل و سیم هوایی
        public string CheckBox8 { get; set; }    //قلاب ها و پست ها
        public string CheckBox9 { get; set; }    //جعبه تقسیم
        public string CheckBox10 { get; set; }   //PCM

        public string CheckBox11 { get; set; }   //سرداخله
        public string CheckBox12 { get; set; }  //رانژه کافو 
        public string CheckBox13 { get; set; }   //سیم کشی غیرمجاز
        public string CheckBox14 { get; set; }   //بهینه سازی   
        public string CheckBox15 { get; set; }  //کابل آبونه

        public string CheckBox16 { get; set; }   //کابل مرکزی
        public string CheckBox17 { get; set; }   //کابل ارتباط 
        public string CheckBox18 { get; set; }   //کابل اختصاصی
        public string CheckBox19 { get; set; }   //مفاصل هوایی
        public string CheckBox20 { get; set; }   //پست و سرکابل

        public string CheckBox21 { get; set; }   //خسارت
        public string CheckBox22 { get; set; }   //برگردان
        public string CheckBox23 { get; set; }   //تعویض بوخت 
        public string CheckBox24 { get; set; }   //تعویض پست
        public string CheckBox25 { get; set; }   //تعویض اتصالی
    }
    public class Failure117Requests
    {
        public long RequestID { set; get; }
        public long? TelephoneNo { set; get; }
        public int? RowNo { set; get; }
        public string InsertDate { set; get; }
        public string InsertTime { set; get; }
        public string MDFDate { set; get; }
        public string MDFTime { set; get; }
        public string SendToCableDate { set; get; }
        public string SendToCableTime { set; get; }
        public string CableDate { set; get; }
        public string CableTime { set; get; }
        public string NetworkDate { set; get; }
        public string NetworkTime { set; get; }
        public string EndMDFDate { set; get; }
        public string EndMDFTime { set; get; }
        public string Step { set; get; }
        public string NetworkDescription { set; get; }
        public string FailureStatus { set; get; }
        public string FailureDuration { set; get; }

        public string CabinetNo { set; get; }
        public string CabinetMarkazi { set; get; }
        public string PostNo { set; get; }
        public string PostEtesali { set; get; }


    }

    public class ADSLTelephoneHistoryInfo
    {
        public long ID { get; set; }
        public long TelephoneNo { get; set; }
        public string Center { get; set; }
        public string PAPInfo { get; set; }
        public string InstalDate { get; set; }
        public DateTime? InstallDate_date { get; set; }
        public string DischargeDate { get; set; }
    }

    public class BuchtAndTelephonOfOpticalCabinet
    {
        public int ID { set; get; }

        public string SwitchPreNo { set; get; }
        public int SwitchPrecodeID { set; get; }

        public long FromTelephone { get; set; }
        public long ToTelephone { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class CabinetCapacity
    {
        public int CabinetNumber { get; set; }
    }
    public class InputInfo
    {
        public long? BuchtID { get; set; }
        public int? CabinetID { get; set; }
        public int? CabinetNumber { get; set; }
        public int? InputNumber { get; set; }
        public DateTime InsertDate { get; set; }
        public int? Status { get; set; }
        public byte BuchtStatus { get; set; }
        public string BuchtStatusName { get; set; }
        public byte? Direction { get; set; }
        public string StatusName { get; set; }
        public string DirectionName { get; set; }
        public string ConnectionSpecification { get; set; }
        public string CustomerName { get; set; }
        public string TeleNo { get; set; }
        public string CenterName { get; set; }
        public int? BuchtTypeID { get; set; }
        public string BuchtTypeName { get; set; }
        public int? VerticalCloumnNo { get; set; }
        public int? VerticalRowNo { get; set; }
        public long? BuchtNo { get; set; }
        public int Capacity { get; set; }
        public string FailureTypeName { get; set; }
        public long CabinetInputID { get; set; }

        //TODO:rad
        public string MalfunctionDate { get; set; }

        public string MalfunctionTime { get; set; }

        public string Description { get; set; }

        public byte FailureType { get; set; }

    }
    public class VerticalBuchtReportInfo
    {
        public long? BuchtID { get; set; }
        public long? BuchtNo { get; set; }
        public int? VerticalCloumnNo { get; set; }
        public int? VerticalRowNo { get; set; }
        public int? CabinetNumber { get; set; }
        public int? InputNumber { get; set; }
        public int? Number { get; set; }
        public int? ConnectionNo { get; set; }
        public int? PostNumber { set; get; }
        public byte? Status { get; set; }
        public string StatusName { get; set; }
        public string ConnectionSpecification { get; set; }
        public long? CustomerID { get; set; }
        public long? TelephoneNo { get; set; }
        public string CenterName { get; set; }
        public int? BuchtTypeID { get; set; }
        public string BuchtTypeName { get; set; }

    }
    public class HorizintalBuchtReportInfo
    {
        public long? BuchtID { get; set; }
        public long? BuchtNo { get; set; }
        public string PortNo { get; set; }
        public byte SwitchPortStatus { get; set; }
        public string SwitchPortStatusName { get; set; }
        public int? ConnectionNo { get; set; }
        public string ConnectionSpecification { get; set; }

        public long? TelephoneNo { get; set; }
        public string CenterName { get; set; }
    }
    public class PortMalfunction
    {
        public string CenterName { get; set; }
        public int? PortID { get; set; }
        public int? Rock { get; set; }
        public int? Shelf { get; set; }
        public int? Card { get; set; }
        public string PCMType { get; set; }
        public int? Port { get; set; }
        public int? PortNumber { get; set; }
        public DateTime? Correct_Date { get; set; }
        public string PersianCorrect_Date { get; set; }
        public string Correct_Time { get; set; }

        public string Failure_Time { get; set; }
        public DateTime? Failure_Date { get; set; }
        public string PersianFailure_Date { get; set; }
        public byte? TypeMalfaction { get; set; }
        public string TypeMalfactionName { get; set; }
        public string Description { get; set; }
        public string CorrectionType { get; set; }
    }
    public class PostContactMalfunction
    {
        public string CenterName { get; set; }
        public int? Cabinet { get; set; }
        public int? Post { get; set; }
        public int? PostContact { get; set; }
        public DateTime? Correct_Date { get; set; }
        public string PersianCorrect_Date { get; set; }
        public string Correct_Time { get; set; }

        public string Failure_Time { get; set; }
        public DateTime? Failure_Date { get; set; }
        public string PersianFailure_Date { get; set; }
        public byte? TypeMalfaction { get; set; }
        public string TypeMalfactionName { get; set; }
        public string Description { get; set; }
        public string CorrectionType { get; set; }
    }
    public class CabinetInputMalfunction
    {
        public string CenterName { get; set; }
        public int? Cabinet { get; set; }
        public long? CabinetInput { get; set; }
        public int? VerticalRowNo { get; set; }
        public int? VerticalCloumnNo { get; set; }
        public long? BuchtNo { get; set; }
        public DateTime? Correct_Date { get; set; }
        public string PersianCorrect_Date { get; set; }
        public string Correct_Time { get; set; }

        public string Failure_Time { get; set; }
        public DateTime? Failure_Date { get; set; }
        public string PersianFailure_Date { get; set; }
        public byte? TypeMalfaction { get; set; }
        public string TypeMalfactionName { get; set; }
        public string Description { get; set; }
        public string CorrectionType { get; set; }
    }
    public class PapInfoReport
    {
        public int PAPInfoID { get; set; }
        public string PAPInfo { get; set; }
        public string CenterName { get; set; }
        public int CenterID { get; set; }
        public string CompanyName { get; set; }
        public DateTime? InstallDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PersianInstallDate { get; set; }
        public string PersianDischargeDate { get; set; }
        public string TelNo { get; set; }

        public int DuringDay { get; set; }
        public long Money { get; set; }
    }
    public class PapInfoGroupByCenterID
    {
        public int CenterID { get; set; }
        public long ADSLCustomerCost { get; set; }
        public int ADSLCUstomerCount { get; set; }
    }
    public class PapInfoGroupByPapInfoID
    {
        public int PapInfoID { get; set; }
        public long ADSLCustomerCost { get; set; }
        public int ADSLCUstomerCount { get; set; }
    }

    public class PapInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
    }
    public class CycleDate
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class SpaceAndPowerPapCompany
    {
        public int PAPInfoID { get; set; }
        public string PAPInfo { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public string CompanyName { get; set; }
        public int? ACPower { get; set; }
        public int? DCPower { get; set; }
        public decimal? Space { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PersianStartDate { get; set; }
    }

    public class CablePairReport
    {
        public int? CablePairNumber { get; set; }
        public long CablePairID { get; set; }
        public int? Cabinet { get; set; }
        public long? CabinetInput { get; set; }
        public int? VerticalRowNo { get; set; }
        public int? VerticalCloumnNo { get; set; }
        public long? BuchtNo { get; set; }
        public string BuchtTypeName { get; set; }
        public int? Post { get; set; }
        public int? PostContact { get; set; }
        public long? TelephoneNo { get; set; }
        public string CablePairStatusName { get; set; }
        public byte? CablePairStatusID { get; set; }
        public string BuchtStatus { get; set; }
        public string CablePairDate { get; set; }
        public string CablePairTime { get; set; }
    }


    public class FileInfo
    {
        public Byte[] Content { get; set; }
        public string FileType { get; set; }
    }

    public class EmptyTelephoneReport
    {
        public long? TelephoneNo { get; set; }
        public DateTime? Dayeri { get; set; }
        public DateTime? DisCharge { get; set; }
        public DateTime? ReDayeri { get; set; }
        public DateTime? InsertDate { get; set; }
        public string PersianDayeriDT { get; set; }
        public string PersianUnIstallDT { get; set; }
        public string PersianReDayeri { get; set; }
        public string LastCounter { get; set; }
        public string CenterName { get; set; }

    }
    public class ResignationLinesStatisticReport
    {
        public string PCM { set; get; }
        public string CenterName { set; get; }
        public string CustomerName { set; get; }
        public long? TelephoneNo { set; get; }
        public int? SwitchPortID { set; get; }
        public string PortNo { set; get; }


        public string BuchtID { set; get; }
        public int? Post { set; get; }
        public int? PostContact { set; get; }
        public int? Cabinet { set; get; }
        public int? CabinetInput { set; get; }
        public string HorizintalMDFID { set; get; }
        public string TelCounterNo { set; get; }
        public string BuchtStatus { set; get; }
        public string PCMType { set; get; }
        public string CustomerID { set; get; }


    }
    public class VisitPlacesCabinetAndPostClass
    {

        public int ID { get; set; }
        public string Center { get; set; }
        public long TelephonNo { get; set; }
        public long VisitAddressID { get; set; }
        public int CabinetID { get; set; }
        public string CabinetInput { get; set; }
        public string CabinetNumber { get; set; }
        public int PostID { get; set; }
        public string PostNumber { get; set; }
        public int? ConnectionNo { get; set; }
        public int ProposedFacilityType { get; set; }
        public bool IsADSL { get; set; }
    }
    public class CostsOutsideBound
    {
        public int OutBoundMeter { get; set; }
        public double InitialCost { get; set; }
        public double MonthlyCosts { get; set; }
    }
    public class TelephoneWithOutPCMReport
    {
        public string CenterName { set; get; }
        public string CustomerName { set; get; }
        public long? TelephoneNo { set; get; }
        public string BuchtID { set; get; }
        public int? Post { set; get; }
        public int? PostContact { set; get; }
        public int? Cabinet { set; get; }
        public int? CabinetInput { set; get; }
        public string CustomerID { set; get; }
    }
    public class PCMInPost
    {
        public string PostNumber { set; get; }
        public int PCMCount { set; get; }
        public string CenterName { set; get; }
    }
    public class ReleaseDocuments
    {
        public string FieldID { set; get; }
        public long? TelephoneNo { set; get; }
        public string CenterName { set; get; }
        public string Address { set; get; }
        public string CustomerName { set; get; }
        public string ReasonTypeName { set; get; }
        public DateTime? EnterDateTime { set; get; }
        public DateTime? ExitDateTime { set; get; }
        public string EnterDate { set; get; }
        public string EnterHour { set; get; }
        public string ExitDate { set; get; }
        public string ExitHour { set; get; }
    }

    public class ConnectionInfo
    {
        public int CenterID { get; set; }
        public int MDFID { get; set; }
        public string MDF { get; set; }
        public long BuchtID { get; set; }
        public long BuchtNo { get; set; }
        public byte BuchtStatus { get; set; }
        public int VerticalRowNo { get; set; }
        public int VerticalColumnNo { get; set; }
        public int VerticalColumnID { get; set; }
        public int VerticalRowID { get; set; }
        public string MFDAndVerticalColumnNo { get; set; }
        public int CabinetInput { get; set; }
        public int BuchtTypeID { get; set; }
        public long? TelephoneNo { get; set; }
        public long? RequestID { get; set; }
        public long? NewTelephoneNo { get; set; }
        public string NewMDF { get; set; }
        public long NewBuchtNo { get; set; }
        public int NewVerticalRowNo { get; set; }
        public int NewVerticalColumnNo { get; set; }
        public long? OldTelephoneNo { get; set; }
        public string OldMDF { get; set; }
        public long OldBuchtNo { get; set; }
        public int OldVerticalRowNo { get; set; }
        public int OldVerticalColumnNo { get; set; }
        public long OldBuchtID { get; set; }
        public long NewBuchtID { get; set; }
        public int? InputNumber { get; set; }
        public string Connection { get; set; }
        public long? PostContact { get; set; }
        public string BuchtTypeString { get; set; }
        public string PostContactStatusString { get; set; }
        public string MUID { get; set; }
        public string ADSLBucht { get; set; }
        public string BuchNoInput { get; set; }
        public string BuchtNoInputPCM { get; set; }
        public int Tabaghe { get; set; }
        public int Radif { get; set; }
        public long? CabinetInputID { get; set; }
        public long? NewCabinetInputID { get; set; }
        public long? OldCabinetInputID { get; set; }
    }
    public class Failure117Performancereport
    {
        public int Request_Today { get; set; }
        public int ConfirmRequest_Today { get; set; }
        public int RequestInCartabl { get; set; }
        public int RequestRemaindFromYesterday { get; set; }

        public int Network_Request_Today { get; set; }
        public int Network_ConfirmRequest_Today { get; set; }
        public int Network_RequestInCartabl { get; set; }
        public int Network_RequestRemaindFromYesterday { get; set; }

        public int SaloonRequest_Today { get; set; }
        public int SaloonConfirmRequest_Today { get; set; }
        public int SaloonRequestInCartabl { get; set; }
        public int SaloonRequestRemaindFromYesterday { get; set; }

        public int MDF_Request_Today { get; set; }
        public int MDF_ConfirmRequest_Today { get; set; }
        public int MDF_RequestInCartabl { get; set; }
        public int MDF_RequestRemaindFromYesterday { get; set; }
    }
    public class Failure117PerformanceTotal
    {
        public DateTime? InsertDate_Request { get; set; }
        public DateTime? MDFDate_Request { get; set; }
        public int Status_Request { get; set; }
        public DateTime? InserDate_FailureForm { get; set; }
        public DateTime? NetworkDate_Failure117 { get; set; }
        public DateTime? SaloonDate_Failure117 { get; set; }
        public DateTime? EndMDFDate { get; set; }
    }
    public class RequestPaymentReport
    {
        public string BranchName { set; get; }
        public string AccountNo { set; get; }
        public DateTime? FicheDate { set; get; }
        public string PersianFicheDate { set; get; }
        public string FicheNo { set; get; }
        public string Office { set; get; }
        public decimal? Amount { set; get; }
        public long RequestID { set; get; }
        public string BillId { set; get; }
        public byte? PaymentType { set; get; }
        public byte? PaymentWay { set; get; }
        public long? Cost { set; get; }
        public bool? IsPaid { set; get; }
        public string PaymentID { set; get; }
        public int? Tax { set; get; }
        public long? AmountSum { set; get; }
        public int? BaseCostID { set; get; }
        public string BaseCost { set; get; }
        public int? BankID { set; get; }
        public string PaymentTypeName { get; set; }
        public string PaymentWayName { get; set; }
        public string BankName { get; set; }
        public string CustomerName { get; set; }
        public string TelephoneNo { get; set; }
        public string Center { get; set; }
        public string FichePersianDate { get; set; }
        public long? ServiceID { get; set; }
        public string Duration { get; set; }
        public string IpDuration { get; set; }
        public string Bandwidth { get; set; }
        public int? BandwidthID { get; set; }
        public int? DurationID { get; set; }
        public long? OldServiceID { get; set; }
        public long? NewServiceID { get; set; }
        public int? AdditionalTrafficID { get; set; }
        public string PaymentServiceType { get; set; }

    }


    public class FailureFullViewInfo
    {
        public long ID { get; set; }
        public string TelephoneNo { get; set; }
        public string InsertDate { get; set; }
        public string Center { get; set; }
        public string CustomerNationalCode { get; set; }
        public string CustomerName { get; set; }
        public string CallingNo { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string CabinetNo { get; set; }
        public string CabinetinputNo { get; set; }
        public string PostNo { get; set; }
        public string PostEtesaliNo { get; set; }
        public string PortPCM { get; set; }
        public string ModelPCM { get; set; }
        public string TypePCM { get; set; }
        public string RockPCM { get; set; }
        public string ShelfPCM { get; set; }
        public string CardPCM { get; set; }
        public string MDFAnalysisUser { get; set; }
        public string MDFDate { get; set; }
        public string LineStatus { get; set; }
        public string MDFAnalysisTime { get; set; }
        public string MDFComment { get; set; }
        public string NetworkUser { get; set; }
        public string NetworkDate { get; set; }
        public string FailureStatus { get; set; }
        public string NetworkOfficer { get; set; }
        public string CabelColor { get; set; }
        public string CabelType { get; set; }
        public string GiveNetworkFormDate { get; set; }
        public string GetNetworkFormDate { get; set; }
        public string SendToCabelDate { get; set; }
        public string CabelDate { get; set; }
        public string NetworkTime { get; set; }
        public string NetworkCommnet { get; set; }
        public string SaloonUser { get; set; }
        public string SaloonDate { get; set; }
        public string SaloonCommnet { get; set; }
        public string EndMDFUser { get; set; }
        public string EndMDFDate { get; set; }
        public string ResultAfterReturn { get; set; }
        public string MDFConfirmTime { get; set; }
        public string EndMDFCommnet { get; set; }
    }
    public class DayeriWiringNetwork
    {
        public string CustomerName { get; set; }
        public string FieldID { get; set; }
        public string TelephoneNo { get; set; }
        public string NearestTelephoneNo { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }
        public string InstallAddress { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string InstallType { get; set; }
        public string PostalCode { get; set; }
        public string TelephoneType { get; set; }
        public string PersonType { set; get; }
        public string OfficeCode { get; set; }
        public string CabinetNo { get; set; }
        public string CabinetinputNo { get; set; }
        public string PostNo { get; set; }
        public string PostEtesaliNo { get; set; }
        public string Port { get; set; }
        public string Rock { set; get; }
        public string Shelf { get; set; }
        public string Card { get; set; }
        public string Etesali { set; get; }
        public string Radif { get; set; }
        public string Tabagheh { set; get; }
        public string PCM { get; set; }
        public string UNO { set; get; }
        public string CenterName { get; set; }
        public string RegionName { set; get; }
        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }
        public string MdfDescription { get; set; }
        public int? NewPCMCabinetInputColumnNo { get; set; }
        public int? NewPCMCabinetInputRowNo { get; set; }
        public long? NewPCMCabinetInputBuchtNo { get; set; }
    }
    public class PerformanceWiringNetworkReport
    {
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public string UsualTelNoInUsedCount { get; set; }
        public string PublicTelNoInUsedCount { get; set; }
        public string PrivateTelNoInUsedCount { get; set; }
        public string AllOfTheFailure117InDateTimeDistinct { get; set; }
        public string AllOfTheFailure117RemaindBefore { get; set; }
        public string AllOfTheFailure117IssueInDateTime { get; set; }
        public string AllOfTheFailure117IssueVoidInDateTime { get; set; }
        public string TheNumber_LocationOfTheFaultCorrectionCenter_Network { get; set; }
        public string TheNumber_LocationOfTheFaultCorrectionCenter_Cable { get; set; }
        public string TheNumber_LocationOfTheFaultCorrectionCenter_PCM { get; set; }
        public string TheNumber_LocationOfTheFaultCorrectionCenter_WLL { get; set; }
        public string FailureDeliveredToTheCustomer { set; get; }
        public string FailureDeliveredToTheCustomer_UsualCustomer { get; set; }
        public string FailureDeliveredToTheCustomer_PrivateCustomer { get; set; }
        public string FailureDeliveredToTheCustomer_FXCustomer { get; set; }
        public string FailureDeliveredToTheCustomer_PublicCustomer { get; set; }
        public string FailureRemaindFormInTime { get; set; }
        public string FailureCorrectionTime_1H { set; get; }
        public string FailureCorrectionTime_2H { get; set; }
        public string FailureCorrectionTime_3H { get; set; }
        public string FailureCorrectionTime_4H { get; set; }
        public string FailureCorrectionTime_5H { get; set; }
        public string FailureCorrectionTime_6H { get; set; }
        public string FailureCorrectionTime_12H { get; set; }
        public string FailureCorrectionTime_24H { set; get; }
        public string FailureCorrectionTime_36H { get; set; }
        public string FailureCorrectionTime_48H { get; set; }
        public string FailureCorrectionTime_72H { set; get; }
        public string FailureCorrectionTime_U72H { get; set; }
        public string FailurePercent { set; get; }
        public string ImprovedSpeedNetworkDowntime { get; set; }
        public string AverageSpeedNetworkDowntime { get; set; }
    }
    public class WiringNetworkReport
    {
        public DateTime? InsertDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? FormInsertDate { get; set; }
        public DateTime? SendToCable { get; set; }
        public DateTime? NetworkDate { get; set; }
        public DateTime? MDFDate { get; set; }
        public int? DifMDFNetworkDate { get; set; }
        public int? FailureStatusID { set; get; }
        public int? FailureStatusParentID { set; get; }

    }
    public class EquipmentBilling
    {
        public string TeleComRecordNo { set; get; }
        public string TeleComPostalCode { set; get; }
        public string TeleComCodes { set; get; }
        public string TeleComAddress { set; get; }

        public string PapComName { get; set; }
        public string PapComRecordNo { set; get; }
        public string PapComPostalCode { set; get; }
        public string PapComCodes { set; get; }
        public string PapComAddress { set; get; }
        public string CycleNo { set; get; }
        public string CycleInYear { set; get; }

        public string SpaceAndPower { get; set; }
        public string ADSLCustomerAbonman { set; get; }
        public string TotalCost { set; get; }
        public string Taxvalue { set; get; }
        public string CollectionOfBills { set; get; }
        public string PreviousDebt { set; get; }
        public string PreviousCredit { set; get; }
        public string sumPayable { set; get; }
        public DateTime? PaymentDeadline { set; get; }
        public string PersianPaymentDeadline { set; get; }

    }
    public class TinyBillingOptions
    {
        public decimal? SpaceField { set; get; }
        public decimal? SpaceCost { set; get; }

        public int? ACPowerField { set; get; }
        public decimal? ACPowerCost { set; get; }

        public int? DCPowerFiels { set; get; }
        public decimal? DCPowerCost { set; get; }

        public int ADSLCustomerCount { set; get; }
        public double ADSLCustomerCost { set; get; }

        public int ADSLRequestCount { set; get; }
        public double ADSLRequestCost { set; get; }

        public string CenterName { set; get; }
        public int CenterID { set; get; }
        public string City { set; get; }

        public int? CostID { set; get; }

        public string CustomerIdentityNo { set; get; }
        public string CustomerPostCode { set; get; }
        public string CustomerEconomicCode { set; get; }
        public string CustomerName { set; get; }
        public string CustomerAddress { set; get; }
        public string TelecomIdentityNo { set; get; }
        public string TelecomPostCode { set; get; }
        public string TelecomEconomicCode { set; get; }
        public string TelecomName { set; get; }
        public string TelecomAddress { set; get; }
        public string CycleName { set; get; }
        public double TaxPercent { set; get; }

    }

    public class PAPCostInfo
    {
        public int ID { get; set; }
        public int CostID { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    public class IBSngADSLInfo
    {
        public string PhoneNumber { get; set; }
        public string RealFirstLogin { get; set; }
        public string NormalUsername { get; set; }
        public string LimitMAC { get; set; }
        public string Name { get; set; }
        public string AutoPi { get; set; }
        public string ExpFromRenewUserValue { get; set; }
        public string LastRenew { get; set; }
        public string FirstLogin { get; set; }
        public string NearestExpDate { get; set; }
        public string Cellphone { get; set; }
        public string NearestExpDateEpoch { get; set; }
        public string NormalPassword { get; set; }
        public string UserId { get; set; }
        public string InvoiceCurrentDue { get; set; }
        public string ChargeRuleUsage { get; set; }
        public string TimeToNearestExpDate { get; set; }
        ////////////////////////////////////////////////////////////////////////
        public string RechargeDeposit { get; set; }
        public string Status { get; set; }
        public string BasicUserId { get; set; }
        public string Deposit { get; set; }
        public string ISPId { get; set; }
        public string Credit { get; set; }
        public string ISPName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string CreationDate { get; set; }
        ////////////////////////////////////////////////////////////////////////
        public string RASIP { get; set; }
        public string RASName { get; set; }
        public string RASDate { get; set; }
        public string RASMAC { get; set; }
        public string UserIP { get; set; }
        public string OnlineStatus { get; set; }
        public string HasOnlinePaymentGateways { get; set; }
        public string UserRepr { get; set; }
    }

    public class InvestigatePossibilityWaitinglistInfo
    {
        public long ID { get; set; }
        public long RequestID { get; set; }
        public bool IsChecked { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int? CabinetID { get; set; }
        public int? CabinetNumber { get; set; }
        public int? PostID { get; set; }
        public string PostNumber { get; set; }
        public string InsertDate { get; set; }
        public string ReqeustInsertDate { get; set; }
        public int? StatusID { get; set; }
        public string StatusName { get; set; }
        public int RequestTypeID { get; set; }
        public string RequestTypeName { get; set; }
        public long? InstallAdressID { get; set; }
        public string PostCode { get; set; }
        public string Adress { get; set; }
        public long? TelephoneNo { get; set; }
        public bool DoProvidedFacility { get; set; }
        public bool HasFreePostContact { get; set; }

        public bool HasFreeBucht { get; set; }

        public bool isValidTime { get; set; }

        public double? InWaitingListHours { get; set; }

        public string InWaitingListDuration { get; set; }
    }
    public class ADSLModemInfo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Model { get; set; }
        public string Price { get; set; }
        public bool? IsSalable { get; set; }
        public string Description { get; set; }
        public string CenterName { get; set; }
        public string SerialNo { get; set; }
        public byte Status { get; set; }
        public string SellerAgentName { get; set; }
    }

    public class ADSLSellerAgentInfo
    {
        public int? GroupID { get; set; }
        public int? CityID { get; set; }
        public string Title { get; set; }
        public long? CreditCash { get; set; }
        public long? CreditCashUse { get; set; }
        public long? CreditCashRemain { get; set; }
        public long? CreditInstalment { get; set; }
        public long? CreditInstalmentUse { get; set; }
        public long? CreditInstalmentRemain { get; set; }
        public string CityName { get; set; }
        public string GroupName { get; set; }
        public string ADSLSellerAgentName { get; set; }
        public string ADSLSellerAgnetUserName { get; set; }
        public long? RechargedCost { get; set; }
        public bool? ISSellModem { get; set; }
        public string SellModem { get; set; }
        public string RechargeDate { get; set; }
        public string UserRecharge { get; set; }
    }

    public class PostTotalyInfo
    {
        public int AllOfTheCabinetInput { set; get; }
        public int FreeCabinetInput { set; get; }
        public int FillCabinetInput { set; get; }
        public int EmptyCabinetInput { set; get; }
        public int FailCabinetInput { set; get; }
        public int ReserveCabinetInput { set; get; }
        public int PCMCount { set; get; }
        public int PCMKarkon { set; get; }
        public int EmptyPortPCM { set; get; }
        public int WaitingListCount { set; get; }
    }
    public class SaleFactor
    {
        public string RequestType { set; get; }
        public string RequestID { set; get; }
        public string InsertDate { set; get; }
        public string PrintDate { set; get; }
        public string CustomerName { set; get; }
        public string TelephoneNo { set; get; }
        public string Center { set; get; }
        public string CostSum { set; get; }
        public string BillID { set; get; }
        public string PaymentID { set; get; }
        public string BarCode { set; get; }

    }
    public class RequestPaymentList
    {
        public string Cost { set; get; }
        public string Tax { set; get; }
        public string AmountSum { set; get; }
        public string PaymentType { set; get; }
        public string BaseCostID { set; get; }
    }


    public class CenterCabinet_Subset
    {
        public string CityName { set; get; }
        public string CenterName { set; get; }

        public int? CabinetInputCount { set; get; }
        public int? FreeCabinetInputCount { set; get; }
        public int? BreakCabinetInputCount { set; get; }
        public int? ReservCabinetInputCount { set; get; }

        public int? ActiveCabinetInputCount { set; get; }

        public int? BreakBuchtCount { set; get; }
        public int? PostCount { set; get; }
        public string CabinetNumber { set; get; }

        public int? KarkonCount { set; get; }
        public int? BrokenPostContact { get; set; }
        public int? FreePostContact { get; set; }
        public int? ReservePostContact { get; set; }
        public int? ReservePostCount { get; set; }
        public int? BrokenPostCount { get; set; }

        public int? APostCount { get; set; }
        public int? BPostCount { get; set; }

        public int? ABPostCount { get; set; }

        public double AllPostCount { get; set; }

        public int? PCMPostContact { get; set; }

        public int? ADSLCount { get; set; }

        public int? WaitingCount { get; set; }

        //TODO:rad
        /// <summary>
        /// ظرفیت کافو
        /// </summary>
        public int CabinetCapacity { get; set; }

        /// <summary>
        /// تعداد پست فعال
        /// </summary>
        public int DayerPostCount { get; set; }

        /// <summary>
        /// تعداد باکس پی سی ام
        /// </summary>
        public int PcmBoxCount { get; set; }

        /// <summary>
        /// باقیمانده سهمیه رزرو
        /// </summary>
        public int RemainedQuotaReservation { get; set; }

    }
    public class ID
    {
        public int _ID { get; set; }
    }

    public class ExchangeCenralCableMDFInfo
    {
        public long ID { get; set; }

        public int CabinetID { get; set; }
        public long FromCabinetInputID { get; set; }
        public long ToCabinetInputID { get; set; }

        public int CabinetNumber { get; set; }
        public int FromCabinetInputNumber { get; set; }
        public int ToCabinetInputNumber { get; set; }

        public long CableID { get; set; }
        public long FromCablePairID { get; set; }
        public long ToCablePairID { get; set; }

        public int CableNumber { get; set; }
        public int FromCablePairNumber { get; set; }
        public int ToCablePairNumber { get; set; }

        public DateTime InsertDate { get; set; }
        public string StatusTitle { get; set; }
        public string RequestLetterNo { get; set; }



    }

    public class ExchangeCenralCableMDFTechnicaliInfo
    {
        public List<CheckableItem> Cabinets { get; set; }
        public List<CheckableItem> CabinetInputs { get; set; }

        public List<CheckableItem> Cables { get; set; }
        public List<CheckableItem> CablePairs { get; set; }
    }

    public class CablePairActionLogXml
    {
        public long CablePairID { get; set; }
        public int CenterID { get; set; }
        public DateTime LogDateTime { get; set; }
    }

    public class RequestRejectReasonInfo
    {
        public string Status_S;
        public string Description;
        public string RequestStep;
        public bool? Status;
    }

    public class ADSLInstallCostCenterInfo
    {
        public int ID { get; set; }
        public string CenterName { get; set; }
        public long? Cost { get; set; }
    }

    public class ADSLGeneralCustomerInfo
    {
        public int NumberOfPendingCustomers { get; set; }
        public int NumberOfDischargeCustomers { get; set; }
        public int NumberOfCutCustomers { get; set; }
        public int NumberOfConnectCustomers { get; set; }
        public int NumberOfCustomers { get; set; }
        public int NumberOfSaledADSLService { get; set; }
        public double? SumCostOfSaledADSLService { get; set; }
    }

    public class TechnicalInfoReport
    {
        public string CabinetNumber { get; set; }
        public string CabinetInputNumber { get; set; }
        public string PostNumber { get; set; }
        public string ConnectionNo { get; set; }
    }

    public class OutputBuchtInfoReport
    {
        public string RadifBucht { get; set; }
        public string TabagheBucht { get; set; }
        public string EtesaliBucht { get; set; }
        public string MDF { get; set; }
    }

    public class nextStates
    {
        public int nextState { get; set; }
        public int? SpecialCondition { get; set; }
    }

    public class SpecialConditionsNextStates
    {
        public string Name { get; set; }
        public bool Value { get; set; }
    }

    public class ADSLGeneralContactsInfo
    {
        public int WeekPrePaidInternet { get; set; }
        public int WeekPostpaidInternet { get; set; }
        public int WeekInteranet { get; set; }
        public int WeekTotalDayeri { get; set; }
        public int WeekDischarge { get; set; }
        public int WeekPureDayeri { get; set; }
        public int ADSLInteranetPorts { get; set; }
        public int ADSLInternetPorts { get; set; }
        public int TotalPorts { get; set; }
        public int WeekADSLAssignedPorts91 { get; set; }
        public int WeekADSLAssignedPorts92 { get; set; }
        public decimal DayeriInstalledPercentage { get; set; }
        public int WeekADSLAssignmentFilePorts { get; set; }
        public string CenterName { get; set; }
        public int WeekNo { get; set; }
        public int Installed { get; set; }
        public int NumberOfDayeriStartOfYear { get; set; }
        public int NumberOfDischargedStartOfYear { get; set; }
        public int CurrentWeekNumberOfDayeriPorts { get; set; }
        public int CurrentWeekNumberOfDInstalledPorts { get; set; }
        public int CityInstalled { get; set; }
        public int CityTotalPorts { get; set; }
        public int CityADSLAssignedPorts91 { get; set; }
        public int CityADSLDayeriStartOfYear { get; set; }
        public int CityADSLDischargeStartOfYear { get; set; }
        public int CityADSLAssignedPorts92 { get; set; }
        public string CityName { get; set; }
        public string Month { get; set; }
        public DateTime? EndDate { get; set; }
        public int GroupID { get; set; }
    }
    public class ADSLContactsPAPComparisionInfo
    {
        public int ContactsDayeri { get; set; }
        public int PAPDayeri { get; set; }
        public int ContactsDischarge { get; set; }
        public int PAPDischarge { get; set; }
        public int PureContacsDayeri { get; set; }
        public int PurePAPDayeri { get; set; }
        public string Month { get; set; }
        public int WeekNo { get; set; }

    }

    public class InstalmentRequestPaymentInfo
    {
        public string PrePaymentAmount { get; set; }
        public string InstallmentCount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Daily { get; set; }
        public string IsCheque { get; set; }
        public string TelephoneNo { get; set; }
        public string RequestID { get; set; }
        public string ChequeNumber { get; set; }
        public string Cost { get; set; }
        public bool? IsChequeByte { get; set; }
        public string ID { get; set; }
        public string CustomerName { get; set; }
        public string PaymentDate { get; set; }
        public string PersonType { get; set; }
        public bool IsPaid { get; set; }
    }

    public class InstalmentRequestPaymentList
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string InstalmentCost { get; set; }
    }

    public class ADSLCityWeeklyContacts
    {
        public string CityName { get; set; }
        public int ContactsWeeklyDayeri { get; set; }
        public int ContactsWeeklyDiscahrge { get; set; }
        public int ContactsWeeklyPureDayeri { get; set; }
        public string DayeriTitle { get; set; }
        public string DischargeTitle { get; set; }
        public string PureDayeriTitle { get; set; }
        public int WeekNo { get; set; }
        public int DayeriCount { get; set; }
        public string cityname { get; set; }
        public int Week1 { get; set; }
        public int Week2 { get; set; }
        public int Week3 { get; set; }
        public int Week4 { get; set; }
        public int Week5 { get; set; }
        public int Week6 { get; set; }
        public int Week7 { get; set; }
        public int Week8 { get; set; }
        public int Week9 { get; set; }
        public int Week10 { get; set; }
        public int Week11 { get; set; }
        public int Week12 { get; set; }
        public int Week13 { get; set; }
        public int Week14 { get; set; }
        public int Week15 { get; set; }
        public int Week16 { get; set; }
        public int Week17 { get; set; }
        public int Week18 { get; set; }
        public int Week19 { get; set; }
        public int Week20 { get; set; }
        public int Week21 { get; set; }
        public int Week22 { get; set; }
        public int Week23 { get; set; }
        public int Week24 { get; set; }
        public int Week25 { get; set; }
        public int Week26 { get; set; }
        public int Week27 { get; set; }
        public int Week28 { get; set; }
        public int Week29 { get; set; }
        public int Week30 { get; set; }
        public int Week31 { get; set; }
        public int Week32 { get; set; }
        public int Week33 { get; set; }
        public int Week34 { get; set; }
        public int Week35 { get; set; }
        public int Week36 { get; set; }
        public int Week37 { get; set; }
        public int Week38 { get; set; }
        public int Week39 { get; set; }
        public int Week40 { get; set; }
        public int Week41 { get; set; }
        public int Week42 { get; set; }
        public int Week43 { get; set; }
        public int Week44 { get; set; }
        public int Week45 { get; set; }
        public int Week46 { get; set; }
        public int Week47 { get; set; }
        public int Week48 { get; set; }
        public int Week49 { get; set; }
        public int Week50 { get; set; }
        public int Week51 { get; set; }
        public int Week52 { get; set; }
        public int Week53 { get; set; }
        public int Week54 { get; set; }

    }

    public class SpecialWirePoints : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long ID { get; set; }
        public int CenterID { get; set; }

        public string PostalCode { get; set; }
        public long? NearestTelephoneNo { get; set; }
        public long? InstallAddressID { get; set; } // in changeLocation and vacate is used
        public long? CorrespondenceAddressID { get; set; } // in changeLocation and vacate is used
        public long? BuchtID { get; set; } // in changeLocation and vacate is used
        public long? OtherBuchtID { get; set; } // in changeLocation and vacate is used
        public long? PostContactID { get; set; } // in changeLocation and vacate is used
        public int? SwitchPortID { get; set; } // in changeLocation and vacate is used
        public long? CabinetInputID { get; set; } // in changeLocation and vacate is used
        public long? NewInstallAddressID { get; set; } // in changeLoacation is used

        public string CustomerName { get; set; } // in changeLocation and vacate is used

        public string _newPostCode;// in changeLoacation is used
        public string NewPostCode// in changeLoacation is used
        {
            get
            {
                return _newPostCode;
            }
            set
            {
                _newPostCode = value;
                NotifyPropertyChanged("NewPostCode");
            }
        }

        public int _specialWireTypeID;// in changeLoacation is used
        public int SpecialWireTypeID// in changeLoacation is used
        {
            get
            {
                return _specialWireTypeID;
            }
            set
            {
                _specialWireTypeID = value;
                NotifyPropertyChanged("SpecialWireTypeID");
            }
        }



        public string NewPostContact { get; set; } // in changeLocation and vacate is used
        public string _newAddressContent;// in changeLoacation is used
        public string NewAddressContent// in changeLoacation is used
        {
            get
            {
                return _newAddressContent;
            }
            set
            {
                _newAddressContent = value;
                NotifyPropertyChanged("NewAddressContent");
            }
        }
        public string _addressContent;
        public string AddressContent
        {
            get
            {
                return _addressContent;
            }
            set
            {
                _addressContent = value;
                NotifyPropertyChanged("AddressContent");
            }
        }
        public bool _isSelect { get; set; }
        public bool IsSelect
        {
            get
            {
                return _isSelect;
            }
            set
            {
                _isSelect = value;
                NotifyPropertyChanged("IsSelect");

            }
        }

    }

    public class ADSLConnectionLogsInfo
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string CreditUsed { get; set; }
        public string Duration { get; set; }
        public string IPAddress { get; set; }
        public string ByteIN { get; set; }
        public string ByteOut { get; set; }
        public string[][] Details { get; set; }
    }

    public class ADSLConnectionLogDetailsInfo
    {
        public string TerminateCause { get; set; }
        public string Port { get; set; }
        public string Mac { get; set; }
        public string KillReason { get; set; }
        public string IPpoolAssignedIP { get; set; }
        public string Ippool { get; set; }
        public string AcctSessionID { get; set; }
    }

    public class ADSLDepositChangesInfo
    {
        public string Date { get; set; }
        public string Action { get; set; }
        public string Deposit { get; set; }
        public string ISPName { get; set; }
        public string AdminName { get; set; }
        public string RemoteAddress { get; set; }
        public string Comment { get; set; }
    }

    public class ADSLCreditChangesInfo
    {
        public string Date { get; set; }
        public string Action { get; set; }
        public string ISPName { get; set; }
        public string AdminName { get; set; }
        public string Credit { get; set; }
        public string BeforeCredit { get; set; }
        public string AfterCredit { get; set; }
        public string Comment { get; set; }
    }

    public class ADSLAuditChangesInfo
    {
        public string Date { get; set; }
        public string IsUser { get; set; }
        public string AttrName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string AdminName { get; set; }
        public string RemoteIP { get; set; }
    }

    public class ADSLCityMonthlyInfo
    {
        public string cityname { get; set; }
        public int Month1 { get; set; }
        public int Month2 { get; set; }
        public int Month3 { get; set; }
        public int Month4 { get; set; }
        public int Month5 { get; set; }
        public int Month6 { get; set; }
        public int Month7 { get; set; }
        public int Month8 { get; set; }
        public int Month9 { get; set; }
        public int Month10 { get; set; }
        public int Month11 { get; set; }
        public int Month12 { get; set; }
        public string DayeriTitle { get; set; }
        public string DischargeTitle { get; set; }
        public string PureDayeriTitle { get; set; }

    }

    public class ADSLBandwidth
    {
        public int OneMonth { get; set; }
        public int ThreeMonth { get; set; }
        public int SixMonth { get; set; }
        public int TwelveMonth { get; set; }
        public int UnlimitedOneMonth { get; set; }
        public int UnlimitedThreeMonth { get; set; }
        public int UnlimitedSixMonth { get; set; }
        public int UnlimitedTwelveMonth { get; set; }
        public int OneMonth_64 { get; set; }
        public int ThreeMonth_64 { get; set; }
        public int SixMonth_64 { get; set; }
        public int TwelveMonth_64 { get; set; }
        public int OneMonth_128 { get; set; }
        public int ThreeMonth_128 { get; set; }
        public int SixMonth_128 { get; set; }
        public int TwelveMonth_128 { get; set; }
        public int OneMonth_256 { get; set; }
        public int ThreeMonth_256 { get; set; }
        public int SixMonth_256 { get; set; }
        public int TwelveMonth_256 { get; set; }
        public int OneMonth_512 { get; set; }
        public int ThreeMonth_512 { get; set; }
        public int SixMonth_512 { get; set; }
        public int TwelveMonth_512 { get; set; }
        public int OneMonth_1024 { get; set; }
        public int ThreeMonth_1024 { get; set; }
        public int SixMonth_1024 { get; set; }
        public int TwelveMonth_1024 { get; set; }
        public int OneMonth_2048 { get; set; }
        public int ThreeMonth_2048 { get; set; }
        public int SixMonth_2048 { get; set; }
        public int TwelveMonth_2048 { get; set; }
        public int TotalUnlimited { get; set; }
        public int Total_64 { get; set; }
        public int Total_128 { get; set; }
        public int Total_256 { get; set; }
        public int Total_512 { get; set; }
        public int Total_1024 { get; set; }
        public int Total_2048 { get; set; }
        public int Total { get; set; }
    }

    public class AboneInfo
    {
        public int? CabinetID { get; set; }
        public int? CabinetNumber { get; set; }

        public int? PostID { get; set; }
        public int? PostNumber { get; set; }

        public long? CabinetInputID { get; set; }
        public int? CabinetInputNumber { get; set; }

        public long? PostContactID { get; set; }
        public int? ConnectionNo { get; set; }
    }

    public class ADSLMDFInfo
    {
        public long ID { get; set; }
        public string MDFTitle { get; set; }
        public string Center { get; set; }
        public string Port { get; set; }
        public string Status { get; set; }
    }

    public class ADSLPortInfo
    {
        public long ID { get; set; }
        public string MDFTitle { get; set; }
        public string Center { get; set; }
        public string Port { get; set; }
        public string TelephoneNo { get; set; }
        public string InstalADSLDate { get; set; }
        public string Status { get; set; }
    }

    public class AdslPortStatisticsInfo
    {
        public int? CityID { get; set; }
        public int? CenterID { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }


        /// <summary>
        /// تعداد منصوبه
        /// </summary>
        public int? TotalCount { get; set; }


        /// <summary>
        /// تعداد دایری
        /// </summary>
        public int? InstallCount { get; set; }

        /// <summary>
        /// تعداد تخلیه
        /// </summary>
        public int? DischargeCount { get; set; }

        /// <summary>
        /// تعداد مشغول به کار
        /// </summary>
        public int? InUseCount { get; set; }

        /// <summary>
        /// تعداد پورت های خراب
        /// </summary>
        public int? DestructionCount { get; set; }

        /// <summary>
        /// از تاریخ
        /// </summary>
        public string FromDate { get; set; }

        /// <summary>
        /// تا تاریخ
        /// </summary>
        public string ToDate { get; set; }
    }

    public class ADSLSellItem
    {
        public int CityID { get; set; }
        public int CenterID { get; set; }

        public long TelephoneNo { get; set; }

        public string CustomerName { get; set; }

        public string CityName { get; set; }

        public string CenterName { get; set; }


        /// <summary>
        /// مبلغ فروش اینترنتی حجم
        /// </summary>
        public long? SellTrafficByInternetAmount { get; set; }

        /// <summary>
        /// مبلغ فروش اینترنتی سرویس
        /// </summary>
        public long? SellServiceByInternetAmount { get; set; }

        /// <summary>
        /// مبلغ فروش حضوری حجم - درخواست مجزا
        /// </summary>
        public long? SellTrafficByPresenceAmountPart1 { get; set; }

        /// <summary>
        /// مبلغ فروش حضوری حجم - درخواست اولیه مشترک
        /// </summary>
        public long? SellTrafficByPresenceAmountPart2 { get; set; }

        /// <summary>
        /// جمع کل مبالغ فروش حضوری حجم
        /// </summary>
        public long? TotalSellTrafficByPresenceAmount { get; set; }

        /// <summary>
        /// مبلغ فروش حضوری سرویس - درخواست مجزا
        /// </summary>
        public long? SellServiceByPresenceAmountPart1 { get; set; }

        /// <summary>
        /// مبلغ فروش حضوری سرویس - درخواست اولیه مشترک
        /// </summary>
        public long? SellServiceByPresenceAmountPart2 { get; set; }

        /// <summary>
        /// جمع کل مبالغ فروش حضوری سرویس
        /// </summary>
        public long? TotalSellServiceByPresenceAmount { get; set; }

        /// <summary>
        /// مبلغ فروش مودم
        /// </summary>
        public long? SellModemAmount { get; set; }

        /// <summary>
        /// مبلغ فروش آی پی
        /// </summary>
        public long? SellIPAmount { get; set; }

        /// <summary>
        /// جمع کلیه ی مبالغ
        /// </summary>
        public long? TotalAmount { get; set; }

    }

    public class ADSLSellerAgentStatisticsInfo
    {
        public int CityID { get; set; }

        public string CityName { get; set; }

        /// <summary>
        /// نام نماینده
        /// </summary>
        public string ADSLSellerAgentTitle { get; set; }

        /// <summary>
        /// اعتبار کل نقدی - سقف مبلغ شارژ
        /// </summary>
        public long? ADSLSellerAgentCreditCash { get; set; }

        /// <summary>
        /// اعتبار مصرف شده - نقدی
        /// </summary>
        public long? ADSLSellerAgentCreditCashUse { get; set; }

        /// <summary>
        /// اعتبار باقیمانده - نقدی
        /// </summary>
        public long? ADSLSellerAgentCreditCashRemain { get; set; }

        /// <summary>
        /// مبلغ افزایش
        /// </summary>
        public long? RechargeCost { get; set; }

        /// <summary>
        /// آیا نماینده امکان فروش ای دی اس ال را دارد یا خیر
        /// </summary>
        public string IsSellModem { get; set; }

        /// <summary>
        /// تاریخ افزایش شارژ
        /// </summary>
        public string RechargeInsertDate { get; set; }

        /// <summary>
        /// کاربر افزایش دهنده
        /// </summary>
        public string RechargeUser { get; set; }
    }

    public class ADSLSellerAgentUserStatisticsInfo
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        /// <summary>
        /// نام نماینده
        /// </summary>
        public string ADSLSellerAgentTitle { get; set; }

        /// <summary>
        /// نام کاربر فروش
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// اعتبار کل نقدی - سقف مبلغ شارژ
        /// </summary>
        public long? CreditCash { get; set; }

        /// <summary>
        /// اعتبار مصرف شده - نقدی
        /// </summary>
        public long? CreditCashUse { get; set; }


        /// <summary>
        /// اعتبار باقیمانده - نقدی
        /// </summary>
        public long? CreditCashRemain { get; set; }

        /// <summary>
        /// مبلغ افزایش
        /// </summary>
        public long? RechargeCost { get; set; }

        /// <summary>
        /// آیا امکان فروش ای دی اس ال را دارد یا خیر
        /// </summary>
        public string IsSellModem { get; set; }

        /// <summary>
        /// تاریخ افزایش شارژ
        /// </summary>
        public string RechargeInsertDate { get; set; }

        /// <summary>
        /// کاربر افزایش دهنده
        /// </summary>
        public string RechargeUser { get; set; }

    }

    public class ADSLSellerAgentUserTotalStatisticsInfo
    {
        public int CityID { get; set; }
        public string CityName { get; set; }

        /// <summary>
        /// نام نماینده
        /// </summary>
        public string ADSLSellerAgentTitle { get; set; }

        /// <summary>
        /// نام کاربر فروش
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// مبلغ فروش قسطی
        /// </summary>
        public long? InstallmentCost { get; set; }

        /// <summary>
        /// مبلغ فروش نقدی
        /// </summary>
        public long? CashCost { get; set; }

        /// <summary>
        /// تعداد نصب
        /// </summary>
        public int? InstallCount { get; set; }

        /// <summary>
        /// مبلغ نصب
        /// </summary>
        public long? InstallCost { get; set; }

        /// <summary>
        /// تعداد رانژه
        /// </summary>
        public int? RanjeCount { get; set; }

        /// <summary>
        /// مبلغ رانژه
        /// </summary>
        public long? RanjeCost { get; set; }

        /// <summary>
        /// جمع مبلغ خالص
        /// </summary>
        public long? NetCost { get; set; }

        /// <summary>
        /// مالیات
        /// </summary>
        public long? Tax { get; set; }

        /// <summary>
        /// جمع مبلغ کل
        /// </summary>
        public long? TotalAmount { get; set; }
    }

    public class ADSLCustomerDurationStatisticsInfo
    {
        public long AdslServiceID { get; set; }

        public string AdslServiceTitle { get; set; }

        /// <summary>
        /// مدت زمان
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// تعداد مشترکین
        /// </summary>
        public int? CustomersCount { get; set; }

        /// <summary>
        /// تعداد کل مشترکین
        /// </summary>
        public int? AllCustomersCount { get; set; }
    }

    public class ADSLAAAActionLogInfo
    {
        public int ID { get; set; }
        public string TelephoneNo { get; set; }
        public string Action { get; set; }
        public string InsertDate { get; set; }
        public string User { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }


    public class CenterToCenterTranslationUserControlInfo
    {
        public string SourceCenter { get; set; }
        public string SourceCabinet { get; set; }
        public string FromSourceCabinetInput { get; set; }
        public string ToSourceCabinetInput { get; set; }
        public string TargetCenter { get; set; }
        public string TargetCabinet { get; set; }
        public string FromTargetCabinetInput { get; set; }
        public string ToTargetCabinetInput { get; set; }
    }

    public class CenterToCenterTranslationChooseNumberInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public long TelephonNo { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsRound { get; set; }
        public string CustomerName { get; set; }
        public int _newPreCodeID;
        public int NewPreCodeID
        {
            get
            {
                return _newPreCodeID;
            }
            set
            {
                _newPreCodeID = value;
                NotifyPropertyChanged("NewPreCodeID");
            }
        }
        public long _newPreCodeNumber;
        public long NewPreCodeNumber
        {
            get
            {
                return _newPreCodeNumber;
            }
            set
            {
                _newPreCodeNumber = value;
                NotifyPropertyChanged("NewPreCodeNumber");
            }
        }
        public long? _newTelephonNo;
        public long? NewTelephonNo
        {
            get
            {
                return _newTelephonNo;
            }
            set
            {
                _newTelephonNo = value;
                NotifyPropertyChanged("NewTelephonNo");
            }
        }
        public long BuchtID { get; set; }
        public int VerticalCloumnNo { get; set; }
        public int VerticalRowNo { get; set; }
        public long BuchtNo { get; set; }

        public int NewVerticalCloumnNo { get; set; }
        public int NewVerticalRowNo { get; set; }
        public long NewBuchtNo { get; set; }

        public long PostContactID { get; set; }
        public int PostContactNumber { get; set; }

        public int PostID { get; set; }
        public int PostNumber { get; set; }

        public int CabinetID { get; set; }
        public int CabinetNumber { get; set; }

        public long? CabinetInputID { get; set; }
        public int CabinetInputNumber { get; set; }

        public long? NewCabinetInputID { get; set; }
        public int NewCabinetInputNumber { get; set; }

        public int NewCabinetNumber { get; set; }

        public long OldPreCodeNumber { get; set; }

    }
    public class ADSLModemPropertyInfo
    {
        public int ID { get; set; }
        public string CenterName { get; set; }
        public string ModemModel { get; set; }
        public string Status { get; set; }
        public string SerialNo { get; set; }
        public string MACAddress { get; set; }
        public int? CenterID { get; set; }
        public int? ADSLModemID { get; set; }
        public string TelephoneNo { get; set; }
        public byte? ModemStatus { get; set; }
        public string Title { get; set; }
    }

    public class CenterToCenterTranslationPCMInfo
    {
        public int OldPCMID { get; set; }
        public int OldPCMTypeID { get; set; }
        public string OldPCMName { get; set; }
        public int CabinetInputNumber { get; set; }
        public int PostContactNumber { get; set; }
        public int NewPCMID { get; set; }
        public string NewPCMName { get; set; }

    }

    public class TranslationPostDetails
    {
        public int CabinetNumber { get; set; }
        public int FromPostNumber { get; set; }
        public int ToPostNumber { get; set; }
        public int? OldConnectionNo { get; set; }
        public int? NewConnectionNo { get; set; }
        public bool OverallTransfer { get; set; }
    }

    public class TranslationCabinetDetails
    {
        public int OldCabinet { get; set; }
        public int FromOldCabinetInput { get; set; }
        public int ToOldCabinetInput { get; set; }

        public string OldFromBuchtInfo { get; set; }
        public string OldToBuchtInfo { get; set; }

        public int NewCabinet { get; set; }
        public int FromNewCabinetInput { get; set; }
        public int ToNewCabinetInput { get; set; }

        public string NewFromBuchtInfo { get; set; }
        public string NewToBuchtInfo { get; set; }


    }

    public class TranslationCentralCableMDFDetails
    {
        public int OldCabinet { get; set; }
        public long FromOldCabinetInput { get; set; }
        public string FromOldBucht { get; set; }
        public long ToOldCabinetInput { get; set; }
        public string ToOldBucht { get; set; }

        public int NewCabel { get; set; }
        public long FromNewCabelPair { get; set; }
        public string FromNewBucht { get; set; }
        public long ToNewCablePair { get; set; }
        public string ToNewBucht { get; set; }
    }

    public class TranslationPostInputConectionSelection : ICloneable
    {
        public long ID { get; set; }
        public long Connection { get; set; }

        public long NewConnection { get; set; }
        public long CabinetInput { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class TranslationPostInputDetail
    {

        public long FromCabinet { get; set; }
        public long FromPost { get; set; }
        public long ToCabinet { get; set; }
        public long ToPost { get; set; }
    }
    public class ChangeLocationCenterInfo
    {
        public string RequestNo { get; set; }
        public string TelephoneNo { get; set; }
        public string InsertDate { get; set; }
        public string InstallAddress { get; set; }
        public string DayeriDate { get; set; }
        public string CounterLocal { get; set; }
        public string CounterNonLocal { get; set; }
        public string CounterInternational { get; set; }
        public string CounterBistalk { get; set; }
        public string CounterIA { get; set; }
        public string CustomerName { get; set; }
        public string NewCustomerName { get; set; }
        public long? NewCustomerID { get; set; }
        public string TelephoneType { get; set; }
        public string TelephoneTypeGroup { get; set; }
        public string ChenageLocationDate { get; set; }
        public string OldInstallAddress { get; set; }
        public string NewInstallAddress { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string ChangeLocationDate { get; set; }
        public string OldCustomerName { get; set; }
        public string NewTelephoneNo { get; set; }
        public string OldTelephoneNo { get; set; }
        public string MobileNo { get; set; }
        public string NewPostalCode { get; set; }
        public string OldPostalCode { get; set; }
        public string UrgentTelephoneNo { get; set; }
        public string NewCorrespondedAddress { get; set; }
        public string OldCorrspondedAddress { get; set; }
        public string NewCaffu { get; set; }
        public string OldCaffu { get; set; }
        public string NewPostContactmarkazi { get; set; }
        public string OldPostContactmarkazi { get; set; }
        public string NewPostContactEttesali { get; set; }
        public string OldPostContactEttesali { get; set; }
        public string NewPostContactPost { get; set; }
        public string OldPostContactPost { get; set; }
        public string NewBuchtRadif { get; set; }
        public string OldBuchtRadif { get; set; }
        public string NewBuchtTabaghe { get; set; }
        public string OldBuchtTabaghe { get; set; }
        public string NewBuchtEttesali { get; set; }
        public string OldBuchtEttsali { get; set; }
        public string RegionName { get; set; }
        public string Center { get; set; }

        public string RequestDate { get; set; }
        public string InstallationDate { get; set; }
        public InstallRequestShortInfo LastInstallRequest { get; set; }

        public string OldRack { get; set; }
        public string OldShelf { get; set; }
        public string OldCard { get; set; }
        public string OldPort { get; set; }
        public string NewRack { get; set; }
        public string NewShelf { get; set; }
        public string NewCard { get; set; }
        public string NewPort { get; set; }
        public string MdfDescription { get; set; }


        public int? NewPCMCabinetInputColumnNo { get; set; }
        public int? NewPCMCabinetInputRowNo { get; set; }
        public long? NewPCMCabinetInputBuchtNo { get; set; }

        public int? OldPCMCabinetInputColumnNo { get; set; }
        public int? OldPCMCabinetInputRowNo { get; set; }
        public long? OldPCMCabinetInputBuchtNo { get; set; }

        public int? NewPCMHeadNoColumnNo { get; set; }
        public int? NewPCMHeadNoRowNo { get; set; }
        public long? NewPCMHeadNoBuchtNo { get; set; }

        public int? NewOtherBuchtRadif { get; set; }
        public int? NewOtherBuchtTabaghe { get; set; }
        public long? NewOtherBuchtEttesali { get; set; }


        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }

    }

    public class InstallRequestShortInfo
    {
        public long ID { get; set; }

        private string _installationDate;

        public string InstallationDate
        {
            get { return _installationDate; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _installationDate = DateTime.Parse(value).ToPersian(Date.DateStringType.Short);
                }
                else
                {
                    _installationDate = "-----";
                }
            }
        }

        private string _telephoneType;
        public string TelephoneType
        {
            get
            {
                return _telephoneType;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _telephoneType = value;
                }
                else
                {
                    _telephoneType = "-----";
                }
            }
        }

        private string _telephoneTypeGroup;
        public string TelephoneTypeGroup
        {
            get
            {
                return _telephoneTypeGroup;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _telephoneTypeGroup = value;
                }
                else
                {
                    _telephoneTypeGroup = "-----";
                }
            }
        }

        public byte ChargingType { get; set; }
        public string ChargingTypeName { get; set; }
        public byte PosessionType { get; set; }
        public string PosessionTypeName { get; set; }
        public byte? MethodOfPaymentForTelephoneConnection { get; set; }
        public string MethodOfPaymentForTelephoneConnectionName { get; set; }
    }

    public class RequestShortInfo
    {
        public long? ID { get; set; }
        public long? CustomerID { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public int RequestTypeId { get; set; }
        public string RequestTypeTitle { get; set; }
        public DateTime? RequestInsertDate { get; set; }
    }

    public class CustomerShortInfo
    {
        public long? ID { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public byte PersonType { get; set; }
        public string PersonTypeName { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
    }


    public class ADSLChangeServiceInfo
    {
        public string NumberOfRequest { get; set; }
        public string NewServiceTitle { get; set; }
        public string OldServiceTitle { get; set; }
        public string NewServiceID { get; set; }
        public string OldServiceID { get; set; }
        public string SaleWay { get; set; }
        public string ProvineName { get; set; }
        public string CenterName { get; set; }
        public string CityName { get; set; }
        public byte? SaleWayByte { get; set; }
    }

    public class ADSLDischargeInfo
    {
        public string ID { get; set; }
        public string Comment { get; set; }
        public string Reason { get; set; }
        public string Service { get; set; }
        public string NumberOfADSLDischarge { get; set; }
        public string Center { get; set; }
        public string Province { get; set; }
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }

    }

    public class TakePossessionInfo
    {
        public long ID { get; set; }
        public string TelNo { get; set; }
        public string Name { get; set; }
        public string CustomerType { get; set; }
        public string TelphoneType { get; set; }
        public string TelephoneTypeGroup { get; set; }
        public string DayeriDate { get; set; }
        public string InsertRequestDate { get; set; }
        public string DischargeDate { get; set; }
        public string DischargeReason { get; set; }
        public string Counter { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string MelliCode { get; set; }
        public string ChargingType { get; set; }
        public string Order { get; set; }
        public string PossessionType { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string Region { get; set; }
        public string PersonType { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }
        public long? DepositAmount { get; set; }
        public long? OtherCostsAmount { get; set; }
        public InstallRequestInfo installRequest { get; set; }
        public long? RequestID { get; set; }

    }

    public class _118Info
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string TelNo { get; set; }
        public string Address { get; set; }
        public string RequestInsertDate { get; set; }
        public string RequestEndDate { get; set; }
        public long? TelephoneNo { get; set; }
        public string Status { get; set; }
        public string type { get; set; }
        public string LastNameAt118 { get; set; }
        public string NameTitleAt118 { get; set; }
        public string TitleAt118 { get; set; }


    }

    public class GeneralRequestPaymentInfo
    {
        public string NumberOfCostType { get; set; }
        public string CostType { get; set; }
        public string AmountSum { get; set; }
        public string CostSum { get; set; }
        public string Cost { get; set; }

        public string City { get; set; }

        public string Center { get; set; }

        //******************************************************* ستون های زیر به علت عدم تطبیق نوع داده با داده ذخیره شده در دیتابیس تعریف شدند
        //13941208
        public int CountOfRequestPayment { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public int? BaseCostID { get; set; }
        public int? OtherCostID { get; set; }
        public long? TotalOfAmountSum { get; set; }
        public long? TotalOfCost { get; set; }
        public string BaseCostTitle { get; set; }
        public long? NumericCost { get; set; }

        /// <summary>
        /// چون در اطلاعات پایه هزینه هایی با عناوین تکراری ثبت شده اند ، در گزارش گیری وجه تمایزی بین عناوین تکراری وجود نداشت تنها راه این بود که تاریخ تعریف هزینه به همراه عنوان نوع هزینه مربوطه آورده شود. 
        /// </summary>
        public DateTime? BaseCostFromDateTime { get; set; }
        public string BaseCostFromDatePersianString { get; set; }

        /// <summary>
        /// چون در اطلاعات پایه هزینه هایی با عناوین تکراری ثبت شده اند ، در گزارش گیری وجه تمایزی بین عناوین تکراری وجود نداشت تنها راه این بود که تاریخ تعریف هزینه به همراه عنوان نوع هزینه مربوطه آورده شود. 
        /// </summary>
        public DateTime? BaseCostToDateTime { get; set; }
        public string BaseCostToDatePersianString { get; set; }

    }

    public class FineToFineRequestPaymentInfo
    {
        public long ID { get; set; }
        public long? TelephoneNo { get; set; }
        public string EndDate { get; set; }
        public string CustomerName { get; set; }
        public string CostType { get; set; }
        public long? Cost { get; set; }
        public string PaymentDate { get; set; }
        public string FicheNumber { get; set; }
        public string PersonType { get; set; }
        public string Center { get; set; }
        public string BaseCostTitle { get; set; }
        public string OtherCostTitle { get; set; }
        public string IsPaid { get; set; }
    }

    public class E1NumberInfo
    {
        public int DDFID { get; set; }
        public int DDFNumber { get; set; }

        public int BayID { get; set; }
        public int BayNumber { get; set; }
        public int PositionID { get; set; }
        public int PositionNumber { get; set; }
        public int NumberID { get; set; }
        public int Number { get; set; }
    }

    public class PBXTelephone : ICloneable
    {
        public long HeadTelephoneNo { get; set; }
        public long TelephoneNo { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class RegisterInfo
    {
        public string RequestID { get; set; }
        public string TelephoneNo { get; set; }
        public string InsertDate { get; set; }
        public string DayeriDate { get; set; }
        public string FicheNumber { get; set; }
        public string FicheDate { get; set; }
        public string PrimaryFicheAmount { get; set; }
        public string CustomerName { get; set; }
        public long BuchtID { get; set; }
        public long OtherBuchtID { get; set; }
        public long E1linkNumber { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class VacateE1Info : ICloneable
    {
        public bool IsSelected { get; set; }
        public E1Link E1Link { get; set; }
        public string RequestID { get; set; }
        public string TelephoneNo { get; set; }
        public string InsertDate { get; set; }
        public string DayeriDate { get; set; }
        public string FicheNumber { get; set; }
        public string FicheDate { get; set; }
        public string PrimaryFicheAmount { get; set; }
        public string CustomerName { get; set; }
        public long BuchtID { get; set; }
        public long OtherBuchtID { get; set; }
        public long E1linkNumber { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class ADSLChangeServiceRequestInfo
    {
        public long RequestID { get; set; }
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string TypeID { get; set; }
        public string ServiceTitle { get; set; }
        public string InserDate { get; set; }
        public string BillID { get; set; }
        public string PaymentID { get; set; }
        public string BankName { get; set; }
        public string FicheNumber { get; set; }
        public string PaymentDate { get; set; }
        public string ModifyUser { get; set; }
        public bool? IsPaid { get; set; }
        public int IBSngStatus { get; set; }
    }

    public class ADSLSellTrafficRequestInfo
    {
        public long RequestID { get; set; }
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string TypeID { get; set; }
        public string TrafficTitle { get; set; }
        public string InserDate { get; set; }
        public string BillID { get; set; }
        public string PaymentID { get; set; }
        public string BankName { get; set; }
        public string FicheNumber { get; set; }
        public string PaymentDate { get; set; }
        public string ModifyUser { get; set; }
        public bool? IsPaid { get; set; }
        public int IBSngStatus { get; set; }
    }

    public class ADSLChangePlaceRequestInfo
    {
        public long RequestID { get; set; }
        public string OldTelephoneNo { get; set; }
        public string OldCenter { get; set; }
        public string OldPortNo { get; set; }
        public string NewTelephoneNo { get; set; }
        public string NewCenter { get; set; }
        public string NewPortNo { get; set; }
        public string InsertDate { get; set; }
        public string EndDate { get; set; }
        public string RequestStep { get; set; }
    }

    public class ConnectionForPCM : ICloneable
    {
        public long ID { get; set; }

        public int rowIndex { get; set; }
        public long? TelehoneNo { get; set; }
        public string MDF { get; set; }
        public int? MDFID { get; set; }
        public string VerticalCloumnNo { get; set; }
        public int? VerticalCloumnID { get; set; }
        public string VerticalRowNo { get; set; }
        public int? VerticalRowID { get; set; }
        public string BuchtNo { get; set; }
        public long? BuchtID { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class ADSLCityCenterBandwidthDailyInfo
    {
        public long? SaturdayDischarge { get; set; }
        public long? SundayDischarge { get; set; }
        public long? MondayDischarge { get; set; }
        public long? TuesdayDischarge { get; set; }
        public long? WednesdayDischarge { get; set; }
        public long? ThursdayDischarge { get; set; }
        public long? FridayDischarge { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public long? _64Saturday { get; set; }
        public long? _64Sunday { get; set; }
        public long? _64Monday { get; set; }
        public long? _64Tuesday { get; set; }
        public long? _64Wednesday { get; set; }
        public long? _64Thursday { get; set; }
        public long? _64Friday { get; set; }
        public long? _128Saturday { get; set; }
        public long? _128Sunday { get; set; }
        public long? _128Monday { get; set; }
        public long? _128Tuesday { get; set; }
        public long? _128Wednesday { get; set; }
        public long? _128Thursday { get; set; }
        public long? _128Friday { get; set; }
        public long? _256Saturday { get; set; }
        public long? _256Sunday { get; set; }
        public long? _256Monday { get; set; }
        public long? _256Tuesday { get; set; }
        public long? _256Wednesday { get; set; }
        public long? _256Thursday { get; set; }
        public long? _256Friday { get; set; }
        public long? _512Saturday { get; set; }
        public long? _512Sunday { get; set; }
        public long? _512Monday { get; set; }
        public long? _512Tuesday { get; set; }
        public long? _512Wednesday { get; set; }
        public long? _512Thursday { get; set; }
        public long? _512Friday { get; set; }
        public long? _1024Saturday { get; set; }
        public long? _1024Sunday { get; set; }
        public long? _1024Monday { get; set; }
        public long? _1024Tuesday { get; set; }
        public long? _1024Wednesday { get; set; }
        public long? _1024Thursday { get; set; }
        public long? _1024Friday { get; set; }
        public long? _2048Saturday { get; set; }
        public long? _2048Sunday { get; set; }
        public long? _2048Monday { get; set; }
        public long? _2048Tuesday { get; set; }
        public long? _2048Wednesday { get; set; }
        public long? _2048Thursday { get; set; }
        public long? _2048Friday { get; set; }
        public long? _5120Saturday { get; set; }
        public long? _5120Sunday { get; set; }
        public long? _5120Monday { get; set; }
        public long? _5120Tuesday { get; set; }
        public long? _5120Wednesday { get; set; }
        public long? _5120Thursday { get; set; }
        public long? _5120Friday { get; set; }
        public long? _10240Saturday { get; set; }
        public long? _10240Sunday { get; set; }
        public long? _10240Monday { get; set; }
        public long? _10240Tuesday { get; set; }
        public long? _10240Wednesday { get; set; }
        public long? _10240Thursday { get; set; }
        public long? _10240Friday { get; set; }
        public long? SumOfDayeriWeek { get; set; }
        public long? SumOfDischargeWeek { get; set; }
        public string BandWidth { get; set; }
        public string PaymentType { get; set; }
        public DayOfWeek? DayName { get; set; }
        public long? NumberOfDayeri { get; set; }
        public long? NumberOfDischarge { get; set; }
    }

    public class ADSLIntranetDayeriDailyInfo
    {
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
    }

    public class ADSLIntranetDischargeDailyInfo
    {
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }


    }

    public class ADSLIntarnetDailySaleInfo
    {
        public int Saturday { get; set; }
        public int Sunday { get; set; }
        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public int DischargeSaturday { get; set; }
        public int DischargeSunday { get; set; }
        public int DischargeMonday { get; set; }
        public int DischargeTuesday { get; set; }
        public int DischargeWednesday { get; set; }
        public int DischargeThursday { get; set; }
        public int DischargeFriday { get; set; }
        public int SumOfDischargeWeek { get; set; }
        public int SumOfDayeriWeek { get; set; }

    }

    public class ReqeustStatusLogDetails
    {
        public string Description { get; set; }
        public string Step { get; set; }
        public int ActionID { get; set; }
    }

    public class ItemChartClass
    {
        public string Category { get; set; }
        public int Number { get; set; }
    }

    public class ADSLTelephoneExpirationeDate
    {
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string ExpirationDate { get; set; }
        public string TelephoneNo { get; set; }
        public string TheLastServiceName { get; set; }
        public string ServiceStartDate { get; set; }
        public string ServiceCost { get; set; }
        public string PortNo { get; set; }
        public int? NumberOfPassedDays { get; set; }
        public int? NumberOfRemainDays { get; set; }
    }

    public class ADSLPortStatusInfo
    {
        public string NumberOfFreePort { get; set; }
        public string NumberOfInstalledPort { get; set; }
        public string NumberOfDistryedPort { get; set; }
        public string NumberOfReservedPort { get; set; }
        public string NumberOfClosedPort { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
    }

    public class ADSLSellerAgentCashIncomeInfo
    {
        public string ADSlSellerAgnetName { get; set; }
        public string ADSLSEllerAgnetUserName { get; set; }
        public long? ADSLRequestCost { get; set; }
        public long? ADSLChangeServiceCost { get; set; }
        public long? ADSLSellTrafficCost { get; set; }
        public long? ADSLChangeNoCost { get; set; }
        public long? ADSLChangeIPCost { get; set; }
        public long? ADSLModemCost { get; set; }
        public long? ADSLRequestInstallmentCost { get; set; }
        public long? ADSLRequestRanjeCost { get; set; }
    }

    public class ADSLSellerAgentSaleDetailsInfo
    {
        public string TelephoneNo { get; set; }
        public string Customername { get; set; }
        public string ADSLSellerAgnetName { get; set; }
        public string ADSLSellerAgentUSerName { get; set; }
        public string ADSLSaleType { get; set; }
        public string Title { get; set; }
        public long? Cost { get; set; }
        public long? Tax { get; set; }
        public long? AmountSum { get; set; }
        public string WorkFlow { get; set; }
        public string EndDate { get; set; }
        public string PaymentDate { get; set; }
        public string FicheNumber { get; set; }
        public string PaymentType { get; set; }
        public string CenterCodeCost { get; set; }
    }

    public class ADSLServiceSaleBandWidthSeperation
    {
        public string CenterCostCode { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public long? RanjeCost { get; set; }
        public long? InstallmentCost { get; set; }
        public long? Cost_Unlimited { get; set; }
        public long? Cost_64 { get; set; }
        public long? Cost_128 { get; set; }
        public long? Cost_256 { get; set; }
        public long? Cost_512 { get; set; }
        public long? Cost_1024 { get; set; }
        public long? Cost_2048 { get; set; }
        public long? Cost_5120 { get; set; }
        public long? Cost_10240 { get; set; }
        public long? AmountSum { get; set; }
        public long? Tax { get; set; }
    }

    public class ADSLTrafficSaleTrafficSeperation
    {
        public string CenterCostCode { get; set; }
        public long? RanjeCost { get; set; }
        public long? InstallmentCost { get; set; }
        public long? ModemCost { get; set; }
        public long? IPCost { get; set; }
        public long? Cost_Unlimited { get; set; }
        public long? Cost_0 { get; set; }
        public long? Cost_0_5 { get; set; }
        public long? Cost_1 { get; set; }
        public long? Cost_2 { get; set; }
        public long? Cost_3 { get; set; }
        public long? Cost_4 { get; set; }
        public long? Cost_5 { get; set; }
        public long? Cost_6 { get; set; }
        public long? Cost_7 { get; set; }
        public long? Cost_8 { get; set; }
        public long? Cost_9 { get; set; }
        public long? Cost_10 { get; set; }
        public long? Cost_11 { get; set; }
        public long? Cost_12 { get; set; }
        public long? Cost_13 { get; set; }
        public long? Cost_14 { get; set; }
        public long? Cost_15 { get; set; }
        public long? Cost_16 { get; set; }
        public long? Cost_17 { get; set; }
        public long? Cost_18 { get; set; }
        public long? Cost_19 { get; set; }
        public long? Cost_20 { get; set; }
        public long? Cost_24 { get; set; }
        public long? Cost_30 { get; set; }
        public long? Cost_40 { get; set; }
        public long? Cost_46 { get; set; }
        public long? Cost_48 { get; set; }
        public long? Cost_50 { get; set; }
        public long? Cost_100 { get; set; }
        public long? Cost_200 { get; set; }
        public long? Cost_125 { get; set; }
        public long? Cost_250 { get; set; }
        public long? Cost_25 { get; set; }
        public long? Cost_36 { get; set; }
        public long? Cost_60 { get; set; }
        public long? Cost_84 { get; set; }
        public long? Cost_120 { get; set; }
        public long? Cost_96 { get; set; }
        public long? Cost_80 { get; set; }
        public long? Cost_750 { get; set; }
        public long? Cost_63 { get; set; }
        public long? Cost_35 { get; set; }
        public long? Cost_45 { get; set; }
        public long? Cost_70 { get; set; }
        public long? Cost_90 { get; set; }
        public long? Cost_150 { get; set; }
        public long? Cost_300 { get; set; }
        public long? Cost_400 { get; set; }
        public long? AmountSum { get; set; }
        public long? Tax { get; set; }
    }

    public class ADSlServiceSaleCustomerAndServiceSeperationInfo
    {
        public string CenterCodeCost { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string TelephoneNo { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string PaymentType { get; set; }
        public string Duration { get; set; }
        public string BandWidth { get; set; }
        public string Traffic { get; set; }
        public long? Cost { get; set; }
        public string PaymentDate { get; set; }
        public string FichNo { get; set; }
        public string FicheDate { get; set; }
        public string SellerAgentUser { get; set; }
        public long? RanjeCost { get; set; }
        public long? InstallmentCost { get; set; }
    }

    public class ADSLServiceAggragationSaleCenterSeperation
    {
        public string CenterCostCode { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public string PaymentType { get; set; }
        public string Duration { get; set; }
        public string Traffic { get; set; }
        public long? Cost { get; set; }
        public int? NumberOfSold { get; set; }
        public string BandWidth { get; set; }
    }

    public class ADSLTrafficSaleCustomerSeperationInfo
    {
        public string CenterCostCode { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string TelephoneNo { get; set; }
        public string Date { get; set; }
        public string CustomerName { get; set; }
        public string Traffic { get; set; }
        public long? TrafficCost { get; set; }
        public long? IPCost { get; set; }
        public long? ModemCost { get; set; }
        public long? InstallmentCost { get; set; }
        public long? RanjeCost { get; set; }
        public string FicheNo { get; set; }
        public string PaymentDate { get; set; }
        public string FicheDate { get; set; }
        public string SellerAgentUser { get; set; }
    }

    public class ADSLInstalmentInfo
    {
        public string PaidInstalment { get; set; }
        public string RemainInstalment { get; set; }
        public string Amount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string IsPaid { get; set; }
        public bool? IsPaidBool { get; set; }
    }

    public class RefundDepositInfo
    {
        public string RequestID { get; set; }
        public string TelNo { get; set; }
        public string CustomerName { get; set; }
        public string CauseOfRefundDeposit { get; set; }
        public string RefundAmount { get; set; }
        public string RefundDepositDate { get; set; }
        public string AccountingdocumentNumber { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string InstallAddress { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string UrgentTelNo { get; set; }

        public string MobileNo { get; set; }
    }

    public class PersonTypeInfo
    {
        public string RequestID { get; set; }
        public string CustomerName { get; set; }
        public string TelephoneNo { get; set; }
        public string ChargingType { get; set; }
        public string CustomerType { get; set; }
        public string CustomerGroup { get; set; }
        public string PosessionType { get; set; }
        public string OrderType { get; set; }
        public string EndDate { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string PersonType { get; set; }

    }

    public class EmptyTelephoneNoInfo
    {
        public string City { get; set; }
        public string Center { get; set; }
        public string TelephoneNo { get; set; }
        public string DischargeDate { get; set; }
        public string ExchangeDate { get; set; }
        public string DischargeReason { get; set; }
    }

    public class ADSLSellerAgentUserCreditInfo
    {
        public string RequestID { get; set; }
        public string TelephoneNo { get; set; }
        public string RequestType { get; set; }
        public string Cost { get; set; }
        public string Date { get; set; }
    }

    public class ADSLSellerAgentUserRechargeInfo
    {
        public string User { get; set; }
        public string Cost { get; set; }
        public string Date { get; set; }
    }



    public class TelephoneSpecialServiceTypeInfo
    {
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string RequestDate { get; set; }
        public string ServiceType { get; set; }
        public string MelliCode { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string PersonType { get; set; }
        public string CustomerAgencyNumber { get; set; }
    }

    public class ChangeAddressInfo
    {
        public string TelephoneNo { get; set; }
        public string CustomerName { get; set; }
        public string OldInstallAddress { get; set; }
        public string NewInstallAddress { get; set; }
        public string MelliCode { get; set; }
        public string OldCorrespondenceAddress { get; set; }
        public string NewCorrespondenceAddress { get; set; }
        public string Region { get; set; }
        public string CenterName { get; set; }
        public string AgencyNumber { get; set; }
        public string MobileNo { get; set; }
        public string NewPostalCode { get; set; }
        public string OldPostalCode { get; set; }
        public string CityName { get; set; }
        public string RequestDate { get; set; }
        public string Agency { get; set; }
        public string UrgentTelNo { get; set; }
    }

    public class TelephonePBXInfo
    {
        public string CustomerName { get; set; }
        public string HeadTelephoneNo { get; set; }
        public string OtherTelephoenNo { get; set; }
        public string Address { get; set; }
        public string CorrespondedAddress { get; set; }
        public string MobileNo { get; set; }
        public string AgencyTelephoneNo { get; set; }
        public string Postalcode { get; set; }
        public string MelliCode { get; set; }
        public string priority { get; set; }
        public string Region { get; set; }
        public string Centername { get; set; }
    }

    public class BuchtNoInfo
    {
        public string BuchNoInput { get; set; }
        public string BuchtNoInputPCM { get; set; }
        public int Radif { get; set; }
        public int Tabaghe { get; set; }
        public long? CabinetInputID { get; set; }

    }
    public class TelephoneRequestLog
    {
        public string TelephoneNo { get; set; }
        public string ToTelephoneNo { get; set; }
        public string RequestType { get; set; }
        public string CustomerName { get; set; }
        public string Date { get; set; }

    }

    public class ADSLModemInformation
    {
        public string Model { get; set; }
        public string TelNo { get; set; }
        public string CustomerName { get; set; }
        public string CityName { get; set; }
        public string Center { get; set; }
        public string SerialNo { get; set; }
        public string MacAddress { get; set; }
        public string Satus { get; set; }
        public string FlowControl { get; set; }
        public string ADSLSellerAgent { get; set; }
        public string ADSLSellerAgentUser { get; set; }
        public string DayeriDate { get; set; }
        public string PaymentDate { get; set; }
    }

    public class ADSLSellerAgentcomissionInfo
    {
        public string ADSLSellerAgentName { get; set; }
        public string ADSLSellerAgentUserName { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public long? CashServiceAmount { get; set; }
        public double? CashServiceComission { get; set; }
        public long? InstalmentServiceAmount { get; set; }
        public double? InstalmentServiceComission { get; set; }
        public string PaymentType { get; set; }
        public int? Comission { get; set; }
        public long? AmountSum { get; set; }
        public long? TrafficAmount { get; set; }
        public double? TrafficComissionAmount { get; set; }

    }

    public class CustomerPersonalInfo
    {
        public long ID { get; set; }
        public string DayeriTakePossessionStatus { get; set; }
        public string CutAndEstablishStatus { get; set; }
        public string TakePossessionReason { get; set; }
        public string StringTelephoneNo { get; set; }
        public string InstallDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerFatherName { get; set; }
        public string MelliCode { get; set; }
        public string BirthDate { get; set; }
        public string InsertDate { get; set; }
        public string CustomerType { get; set; }
        public string CustomerGroup { get; set; }
        public string ChargingType { get; set; }
        public string possessionType { get; set; }
        public string OrderType { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string Center { get; set; }
        public string PostalCode { get; set; }
        public string MetrajOuterBand { get; set; }
        public string TelephoneClass { get; set; }
        public long? AmountSum { get; set; }
        public string Amount { get; set; }
        public string PaymentDate { get; set; }
        public string InstallmentCount { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
        public long? TelephoneNo { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class E1LinkReportInfo
    {
        public string TelephoneNo { get; set; }
        public string BuchtMDF { get; set; }
        public string BuchtRadif { get; set; }
        public string BuchtTabaghe { get; set; }
        public string BuchtEttesali { get; set; }
        public string OtherRadif { get; set; }
        public string OtherTabaghe { get; set; }
        public string OtherEttesali { get; set; }
        public string AccessRadif { get; set; }
        public string AccessTabaghe { get; set; }
        public string AccessEttesali { get; set; }
        public long RequestID { get; set; }

    }

    public class TranslatoionPostWiring
    {
        public string fromCabinet { get; set; }
        public string toCabinet { get; set; }
        public string fromPost { get; set; }
        public string toPost { get; set; }
        public int OldConnectionNo { get; set; }
        public int NewConnectionNo { get; set; }
        public long? TelephoneNo { get; set; }
        public int ConnectionNo { get; set; }
        public List<int> OldConnectionNoList { get; set; }
        public List<int> NewConnectionNoList { get; set; }

    }

    public class ADSLInstalmentRequestPaymentTelephoneNoInfo
    {
        public string TelephoneNo { get; set; }
        public long Cost { get; set; }
    }

    public class TranslationPostInputMDFInfo
    {
        public long? BuchtID { get; set; }
        public long? TelephoneNo { get; set; }

        private string _oldRadif;
        public string OldRadif
        {
            get
            {
                return _oldRadif;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldRadif = value;
                }
                else
                {
                    _oldRadif = "-----";
                }
            }
        }

        private string _oldTabaghe;
        public string OldTabaghe
        {
            get
            {
                return _oldTabaghe;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldTabaghe = value;
                }
                else
                {
                    _oldTabaghe = "-----";
                }
            }
        }

        private string _oldEttesali;
        public string OldEttesali
        {
            get
            {
                return _oldEttesali;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldEttesali = value;
                }
                else
                {
                    _oldEttesali = "-----";
                }
            }
        }

        //old otherBucht
        private string _oldOtherRadif;
        public string OldOtherRadif
        {
            get
            {
                return _oldOtherRadif;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldOtherRadif = value;
                }
                else
                {
                    _oldOtherRadif = "-----";
                }
            }
        }

        private string _oldOtherTabaghe;
        public string OldOtherTabaghe
        {
            get
            {
                return _oldOtherTabaghe;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldOtherTabaghe = value;
                }
                else
                {
                    _oldOtherTabaghe = "-----";
                }
            }
        }

        private string _oldOtherEttesali;
        public string OldOtherEttesali
        {
            get
            {
                return _oldOtherEttesali;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldOtherEttesali = value;
                }
                else
                {
                    _oldOtherEttesali = "-----";
                }
            }
        }
        //

        //<summary>
        private string _newOtherRadif;
        public string NewOtherRadif
        {
            get
            {
                return _newOtherRadif;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newOtherRadif = value;
                }
                else
                {
                }
            }
        }

        private string _newOtherTabaghe;
        public string NewOtherTabaghe
        {
            get
            {
                return _newOtherTabaghe;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newOtherTabaghe = value;
                }
                else
                {
                    _newOtherTabaghe = "-----";
                }
            }
        }

        private string _newOtherEttesali;
        public string NewOtherEttesali
        {
            get
            {
                return _newOtherEttesali;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newOtherEttesali = value;
                }
                else
                {
                    _newOtherEttesali = "-----";
                }
            }
        }

        private string _newOtherMDF;
        public string NewOtherMDF
        {
            get
            {
                return _newOtherMDF;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newOtherMDF = value;
                }
                else
                {
                    _newOtherMDF = "-----";
                }
            }
        }
        // </summary>

        private string _oldMDF;
        public string OldMDF
        {
            get
            {
                return _oldMDF;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _oldMDF = value;
                }
                else
                {
                    _oldMDF = "-----";
                }
            }
        }

        private string _newRadif;
        public string NewRadif
        {
            get
            {
                return _newRadif;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newRadif = value;
                }
                else
                {
                }
            }
        }

        private string _newTabaghe;
        public string NewTabaghe
        {
            get
            {
                return _newTabaghe;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newTabaghe = value;
                }
                else
                {
                    _newTabaghe = "-----";
                }
            }
        }

        private string _newEttesali;
        public string NewEttesali
        {
            get
            {
                return _newEttesali;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newEttesali = value;
                }
                else
                {
                    _newEttesali = "-----";
                }
            }
        }

        private string _newMDF;
        public string NewMDF
        {
            get
            {
                return _newMDF;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _newMDF = value;
                }
                else
                {
                    _newMDF = "-----";
                }
            }
        }

        public string InputNumber { get; set; }

        public string ADSLBucht { get; set; }

        private long? _telephone;
        public long? Telephone
        {
            get
            {
                return _telephone;
            }
            set
            {
                if (null != value)
                {
                    _telephone = value;
                }
                else
                {
                    _telephone = 0;
                }
            }
        }
    }

    public class ExchangeCabinetInputInfo
    {
        public string OldCafu { get; set; }
        public string OldMarkazi { get; set; }
        public string OldPost { get; set; }
        public string OldEttesali { get; set; }
        public string NewCafu { get; set; }
        public string NewMarkazi { get; set; }
        public string NewPost { get; set; }
        public string NewEttesali { get; set; }
    }

    public class TranslationPostInfo
    {
        public string OldPost { get; set; }
        public string NewPost { get; set; }
        public string InsertDate { get; set; }
        public string OldCafu { get; set; }
        public string FromEttesali { get; set; }
        public string ToEttesali { get; set; }
        public string CompletationDate { get; set; }
    }

    public class TranslationPostInputInfo
    {
        public string OldCafu { get; set; }
        public string NewCafu { get; set; }
        public string OldPost { get; set; }
        public string NewPost { get; set; }
        public string InsertDate { get; set; }
        public string MDFDate { get; set; }
        public string NetworkDate { get; set; }
        public string MDFTime { get; set; }
        public string NetworkTime { get; set; }
    }

    public class ExchangeCabinetInputReportInfo
    {
        public string OldCabinet { get; set; }
        public string FromOldcabinetInput { get; set; }
        public string ToOldCabinetInput { get; set; }
        public string NewCabinet { get; set; }
        public string FromNewCabinetInput { get; set; }
        public string ToNewCabinetInput { get; set; }
        public string FromPost { get; set; }
        public string MDFDate { get; set; }
        public string MDFTime { get; set; }
        public string NetworkDate { get; set; }
        public string NetworkTime { get; set; }
        public string InsertDate { get; set; }
    }

    public class ExchangeCentralMDFCableReportInfo
    {
        public string Cabinet { get; set; }
        public string FromCabinetInput { get; set; }
        public string ToCabinetInput { get; set; }
        public string FromRadif { get; set; }
        public string FromTabaghe { get; set; }
        public string FromEttesali { get; set; }
        public string ToRadif { get; set; }
        public string ToTabaghe { get; set; }
        public string ToEttesali { get; set; }
        public string InsertDate { get; set; }
    }

    public class CenterToCenterTranslationReportInfo
    {
        public string SourceCenter { get; set; }
        public string OldCabinet { get; set; }
        public string FromOldCabinetInput { get; set; }
        public string ToOldCabinetInput { get; set; }
        public string TargetCenter { get; set; }
        public string NewCabinet { get; set; }
        public string FromNewCabinetInput { get; set; }
        public string ToNewCabinetInput { get; set; }
        public string InsertDate { get; set; }
        public string AccomplishmentDate { get; set; }
        public string AccomplishmentTime { get; set; }
    }

    public class BuchtSwitchingReportInfo
    {
        public string OldRadif { get; set; }
        public string OldTabaghe { get; set; }
        public string OldEttesali { get; set; }
        public string NewRadif { get; set; }
        public string NewTabaghe { get; set; }
        public string NewEttesali { get; set; }
        public string NetworkAccmplishmentDate { get; set; }
        public string NetworkAccomplishmentTime { get; set; }
        public string MDFAccomplishmentDate { get; set; }
        public string MDFAccomplishmentTime { get; set; }
        public string CauseOfChange { get; set; }
    }

    public class InstalmentBillingInfo
    {
        public long TelephoneNo { get; set; }
        public long Cost { get; set; }
    }

    public class IbsngInputInfo
    {
        public string NormalUsername { get; set; }
    }

    public class ChangeIBSngInfo
    {
        public string UserID { get; set; }
        public string Deposit { get; set; }
        public bool IsAbsoluteChange { get; set; }
        public string DepositType { get; set; }
        public string DepositComment { get; set; }
        public string CustomFieldFreeCounter { get; set; }
        public string RenewNextGroup { get; set; }
        public string RenewRemoveUserExpDates { get; set; }
    }

    public class IBSngUserInfo
    {
        public string Name { get; set; }
        public string NormalUsername { get; set; }
        public string NormalPassword { get; set; }
        public System.Object[][] InternetOnlines { get; set; }
        public string UserID { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string RealFirstLogin { get; set; }
        public string FirstLogin { get; set; }
        public string NearestExpDate { get; set; }
        public string RechargeDeposit { get; set; }
        public string Credit { get; set; }
        public bool OnlineStatus { get; set; }
        public string LimitMac { get; set; }
        public string RenewNextGroup { get; set; }
        public string Lock { get; set; }
        public string MultiLogin { get; set; }
        public string CreationDate { get; set; }
        public double CustomFieldFreeCounter { get; set; }
        public string IBSngGroupName { get; set; }
    }

    public class ADSLSupportCommentInfo
    {
        public long ID { get; set; }
        public long TelephoneNo { get; set; }
        public string User { get; set; }
        public string InsertDate { get; set; }
        public string Comment { get; set; }
    }

    public class InvestigateInfo
    {
        public string MDF { get; set; }
        public string Cabinet { get; set; }
        public string Post { get; set; }
        public DateTime ConnectionReserveDate { get; set; }

        public string BuchtInfo { get; set; }
    }

    public class TranslationOpticalCabinetToNormalInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //TODO:rad 13950623

        public int? OldPcmRockNumber { get; set; }
        public int? OldPcmShelfNumber { get; set; }
        public int? OldPcmCard { get; set; }
        public int? OldPcmPortNumber { get; set; }

        //*********

        //TODO:rad 13950624
        public int? OldPcmRockId { get; set; }
        public int? OldPcmShelfId { get; set; }
        public int? OldPcmId { get; set; }
        public int? OldPcmPortId { get; set; }
        //*********
        public int? OldCabinet { get; set; }

        public int? OldCabinetNumber { get; set; }
        public long? OldCabinetInputID { get; set; }
        public long? OldCabinetInputNumber { get; set; }

        public int? OldPostNumber { get; set; }
        public int? OldPostID { get; set; }
        public int? OldPostContactNumber { get; set; }
        public long? OldPostContactID { get; set; }
        public long? OldBuchtID { get; set; }
        public string OldConnectionNo { get; set; }

        public int? OldColumnNo { get; set; }

        public int? OldRowNo { get; set; }

        public long? OldBuchtNo { get; set; }


        public int? NewColumnNo { get; set; }

        public int? NewRowNo { get; set; }

        public long? NewBuchtNo { get; set; }

        public int? OtherColumnNo { get; set; }

        public int? OtherRowNo { get; set; }

        public long? OtherBuchtNo { get; set; }

        public string OtherConnectionNo { get; set; }

        public string CustomerAddress { get; set; }

        public string PostallCode { get; set; }
        public string AfterPostallCode { get; set; }
        public int? OldBuchtStatus { get; set; }
        public long? OldTelephonNo { get; set; }

        public int? OldSwitchPortID { get; set; }

        public long? _oldCounter;
        public long? OldCounter
        {
            get
            {
                return _oldCounter;
            }
            set
            {
                _oldCounter = value;
                NotifyPropertyChanged("OldCounter");
            }
        }
        public bool? OldIsVIP { get; set; }
        public bool? OldIsRound { get; set; }
        public string CustomerName { get; set; }
        public string AfterCustomerName { get; set; }
        public int? NewCabinet { get; set; }
        public int? NewCabinetNumber { get; set; }
        public long? NewCabinetInputID { get; set; }
        public int? NewCabinetInputNumber { get; set; }
        public long? NewBuchtID { get; set; }
        public string NewConnectionNo { get; set; }
        public int? NewBuchtStatus { get; set; }

        public long? _newTelephonNo;
        public long? NewTelephonNo
        {
            get
            {
                return _newTelephonNo;
            }
            set
            {
                _newTelephonNo = value;
                NotifyPropertyChanged("NewTelephonNo");
            }
        }

        public int? NewSwitchPortID { get; set; }

        public long? _newCounter;
        public long? NewCounter
        {
            get
            {
                return _newCounter;
            }
            set
            {
                _newCounter = value;
                NotifyPropertyChanged("NewCounter");
            }
        }

        public int? _newPreCodeID;
        public int? NewPreCodeID
        {
            get
            {
                return _newPreCodeID;
            }
            set
            {
                _newPreCodeID = value;
                NotifyPropertyChanged("NewPreCodeID");
            }
        }

        public long? _newPreCodeNumber;
        public long? NewPreCodeNumber
        {
            get
            {
                return _newPreCodeNumber;
            }
            set
            {
                _newPreCodeNumber = value;
                NotifyPropertyChanged("NewPreCodeNumber");
            }
        }

        public int? NewPostNumber { get; set; }

        public int? NewPostID { get; set; }
        public int? NewPostConntactNumber { get; set; }

        public long? NewPostContactID { get; set; }

        public string CompletionDate { get; set; }

        public string SpecialService { get; set; }

        public string TelephoneClass { get; set; }

        public string OldCabinetUsageType { get; set; }

        public string NewCabinetUsageType { get; set; }

        public bool HasAdsl { get; set; }

        public string CutAndEstablishStatus { get; set; }

        public string CauseOfCut { get; set; }

        public string CityName { get; set; }

        public string CenterName { get; set; }

        public long RequestID { get; set; }

        public int? PcmCabinetInputColumnNo { get; set; }
        public int? PcmCabinetInputRowNo { get; set; }
        public long? PcmCabinetInputBuchtNo { get; set; }

        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }


    public class ExchangeCabinetInputRequestReportInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int? OldCabinet { get; set; }

        public string Type { get; set; }

        public int? OldCabinetNumber { get; set; }
        public long? OldCabinetInputID { get; set; }
        public long? OldCabinetInputNumber { get; set; }

        public int? OldPostNumber { get; set; }
        public int? OldPostID { get; set; }
        public int? OldPostContactNumber { get; set; }
        public long? OldPostContactID { get; set; }
        public long? OldBuchtID { get; set; }
        public string OldConnectionNo { get; set; }

        public int? OldColumnNo { get; set; }

        public int? OldRowNo { get; set; }

        public long? OldBuchtNo { get; set; }


        public int? NewColumnNo { get; set; }

        public int? NewRowNo { get; set; }

        public long? NewBuchtNo { get; set; }

        public int? OtherColumnNo { get; set; }

        public int? OtherRowNo { get; set; }

        public long? OtherBuchtNo { get; set; }

        public string OtherConnectionNo { get; set; }

        public string CustomerAddress { get; set; }

        public string PostallCode { get; set; }
        public int? OldBuchtStatus { get; set; }
        public long? OldTelephonNo { get; set; }

        public int? OldSwitchPortID { get; set; }

        public long? _oldCounter;
        public long? OldCounter
        {
            get
            {
                return _oldCounter;
            }
            set
            {
                _oldCounter = value;
                NotifyPropertyChanged("OldCounter");
            }
        }
        public bool? OldIsVIP { get; set; }
        public bool? OldIsRound { get; set; }
        public string CustomerName { get; set; }
        public int? NewCabinet { get; set; }
        public int? NewCabinetNumber { get; set; }
        public long? NewCabinetInputID { get; set; }
        public int? NewCabinetInputNumber { get; set; }
        public long? NewBuchtID { get; set; }
        public string NewConnectionNo { get; set; }
        public int? NewBuchtStatus { get; set; }

        public int? NewPostNumber { get; set; }

        public int? NewPostID { get; set; }
        public int? NewPostConntactNumber { get; set; }

        public long? NewPostContactID { get; set; }

        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
    //public class ExchangeCabinetConnectionInfo : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    private void NotifyPropertyChanged(String propertyName = "")
    //    {
    //        if (PropertyChanged != null)
    //        {
    //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //        }
    //    }
    //    public int? FromCabinet { get; set; }
    //    public int? FromCabinetNumber { get; set; }
    //    public long? FromCabinetInputID { get; set; }
    //    public long? FromCabinetInputNumber { get; set; }
    //    public int? FromPostNumber { get; set; }
    //    public int? FromPostContactNumber { get; set; }
    //    public long? FromPostContactID { get; set; }
    //    public long? FromBuchtID { get; set; }
    //    public string FromConnectionNo { get; set; }
    //    public int? FromColumnNo { get; set; }
    //    public int? FromRowNo { get; set; }
    //    public long? FromBuchtNo { get; set; }
    //    public int? ToColumnNo { get; set; }
    //    public int? ToRowNo { get; set; }
    //    public long? ToBuchtNo { get; set; }
    //    public int? OtherColumnNo { get; set; }
    //    public int? OtherRowNo { get; set; }
    //    public long? OtherBuchtNo { get; set; }
    //    public string OtherConnectionNo { get; set; }
    //    public string CustomerAddress { get; set; }
    //    public string PostallCode { get; set; }
    //    public int? FromBuchtStatus { get; set; }
    //    public long? FromTelephonNo { get; set; }
    //    public int? FromSwitchPortID { get; set; }
    //    public bool? FromIsVIP { get; set; }
    //    public bool? FromIsRound { get; set; }
    //    public string CustomerName { get; set; }
    //    public int? ToCabinet { get; set; }
    //    public int? ToCabinetNumber { get; set; }
    //    public long? ToCabinetInputID { get; set; }
    //    public int? ToCabinetInputNumber { get; set; }
    //    public long? ToBuchtID { get; set; }
    //    public string ToConnectionNo { get; set; }
    //    public int? ToBuchtStatus { get; set; }
    //    public int? ToPostNumber { get; set; }
    //    public int? ToPostConntactNumber { get; set; }
    //    public long? ToPostContactID { get; set; }
    //    public object Clone()
    //    {
    //        return this.MemberwiseClone();
    //    }

    //}



    public class ExchangeCabinetInputConnectionInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int? FromPostNumber { get; set; }

        public string FromConnectiontype { get; set; }
        public byte? FromAorBType { get; set; }
        public string FromAorBTypeName { get; set; }

        public int? _fromPostConntactNumber { get; set; }
        public int? FromPostConntactNumber
        {
            get
            {
                return _fromPostConntactNumber;
            }
            set
            {
                _fromPostConntactNumber = value;
                NotifyPropertyChanged("FromPostConntactNumber");
            }

        }
        public int? ToPostNumber { get; set; }
        public int? _toPostConntactNumber { get; set; }

        public int? ToPostConntactNumber
        {
            get
            {
                return _toPostConntactNumber;
            }
            set
            {
                _toPostConntactNumber = value;
                NotifyPropertyChanged("ToPostConntactNumber");
            }

        }



        public int? _fromPostID;
        public int? FromPostID
        {
            get
            {
                return _fromPostID;
            }
            set
            {
                _fromPostID = value;
                NotifyPropertyChanged("FromPostID");
            }
        }

        public long? _fromPostContactID;
        public long? FromPostContactID
        {
            get
            {
                return _fromPostContactID;
            }
            set
            {
                _fromPostContactID = value;
                NotifyPropertyChanged("FromPostContactID");
            }
        }

        public int? _toPostID;
        public int? ToPostID
        {
            get
            {
                return _toPostID;
            }
            set
            {
                _toPostID = value;
                NotifyPropertyChanged("ToPostID");
            }
        }

        public long? _toPostConntactID;
        public long? ToPostConntactID
        {
            get
            {
                return _toPostConntactID;
            }
            set
            {
                _toPostConntactID = value;
                NotifyPropertyChanged("ToPostConntactID");
            }
        }

        public long? _toCabinetInputID;
        public long? ToCabinetInputID
        {
            get
            {
                return _toCabinetInputID;
            }
            set
            {
                _toCabinetInputID = value;
                NotifyPropertyChanged("ToCabinetInputID");
            }
        }

        public string _toAorBTypeName;
        public string ToAorBTypeName
        {
            get
            {
                return _toAorBTypeName;
            }
            set
            {
                _toAorBTypeName = value;
                NotifyPropertyChanged("ToAorBTypeName");
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
    public class TranslationOpticalCabinetToNormalConnctionInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long? _fromTelephone;
        public long? FromTelephone
        {
            get
            {
                return _fromTelephone;
            }
            set
            {
                _fromTelephone = value;
            }
        }

        public int? FromPostNumber { get; set; }

        public string FromConnectiontype { get; set; }
        public byte? FromAorBType { get; set; }
        public string FromAorBTypeName { get; set; }

        public int? _fromPostConntactNumber { get; set; }
        public int? FromPostConntactNumber
        {
            get
            {
                return _fromPostConntactNumber;
            }
            set
            {
                _fromPostConntactNumber = value;
                NotifyPropertyChanged("FromPostConntactNumber");
            }

        }
        public int? ToPostNumber { get; set; }
        public int? _toPostConntactNumber { get; set; }

        public int? ToPostConntactNumber
        {
            get
            {
                return _toPostConntactNumber;
            }
            set
            {
                _toPostConntactNumber = value;
                NotifyPropertyChanged("ToPostConntactNumber");
            }

        }



        public int? _fromPostID;
        public int? FromPostID
        {
            get
            {
                return _fromPostID;
            }
            set
            {
                _fromPostID = value;
                NotifyPropertyChanged("FromPostID");
            }
        }

        public long? _fromPostContactID;
        public long? FromPostContactID
        {
            get
            {
                return _fromPostContactID;
            }
            set
            {
                _fromPostContactID = value;
                NotifyPropertyChanged("FromPostContactID");
            }
        }

        public int? _toPostID;
        public int? ToPostID
        {
            get
            {
                return _toPostID;
            }
            set
            {
                _toPostID = value;
                NotifyPropertyChanged("ToPostID");
            }
        }

        public long? _toPostConntactID;
        public long? ToPostConntactID
        {
            get
            {
                return _toPostConntactID;
            }
            set
            {
                _toPostConntactID = value;
                NotifyPropertyChanged("ToPostConntactID");
            }
        }

        public long? _toCabinetInputID;
        public long? ToCabinetInputID
        {
            get
            {
                return _toCabinetInputID;
            }
            set
            {
                _toCabinetInputID = value;
                NotifyPropertyChanged("ToCabinetInputID");
            }
        }

        public string _toAorBTypeName;
        public string ToAorBTypeName
        {
            get
            {
                return _toAorBTypeName;
            }
            set
            {
                _toAorBTypeName = value;
                NotifyPropertyChanged("ToAorBTypeName");
            }
        }


        public string _toConnectiontype;
        public string ToConnectiontype
        {
            get
            {
                return _toConnectiontype;
            }
            set
            {
                _toConnectiontype = value;
                NotifyPropertyChanged("ToConnectiontype");
            }
        }

        public bool isApply { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class TelephonCustomer
    {
        public long TelephonNo { get; set; }
        public Customer Customer { get; set; }
    }

    public class ReservesDayeriInfo
    {
        public long ID { get; set; }
        public string CenterName { get; set; }
        public string CustomerName { get; set; }

        public int? CabinetNumber { get; set; }
        public int? CabinetInputNumber { get; set; }
        public int? PostNumber { get; set; }
        public int? ConnectionNo { get; set; }
        public string StatusName { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string CreatorUser { get; set; }

        public string AORBType { get; set; }
        public string RequestStatusName { get; set; }

        public string RequestTypeName { get; set; }
        public string PersianInsertDate { get; set; }
        public string PersianModifyDate { get; set; }

    }

    public class ADSLIPHistoryInfo
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string TelephoneNo { get; set; }
        public string Type { get; set; }
        public string BlockCount { get; set; }
        public string VirtualRange { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }


    public class TranslationOpticalToNormal
    {
        public long OldCabinetNumber { get; set; }
        public long NewCabinetNumber { get; set; }
    }
    public class MapItemInfo
    {
        public long ID { get; set; }
        public CRM.Data.DB.MapShapeType Type { get; set; }
    }

    public class PCMPostInfo
    {
        public string City { get; set; }
        public string Center { get; set; }
        public int CabinetNumber { get; set; }
        public long PostNumber { get; set; }
        public int? PCMCount { get; set; }
        public string Address { get; set; }

        //TODO:rad add new properties
        public long? TelephoneNo { get; set; }

        public string CabinetInputNumber { get; set; }

        public string PostContactConnectionNo { get; set; }

        public string Bucht { get; set; }

        public string PcmBox { get; set; }

        public string ABType { get; set; }

    }

    public class WorkingTelephoneReport
    {
        public string cityName { get; set; }
        public string CenterName { get; set; }
        public long? Telephone { get; set; }
        public string Bucht { get; set; }
        public int? Cabinet { get; set; }
        public long? CabinetInput { get; set; }
        public int? Post { get; set; }
        public long? PostContact { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string InstallationDatePersian { get; set; }
        public string PAPName { get; set; }
        public string ADSLBucht { get; set; }
    }
    public class WorkingTelephoneBaseDateReport
    {
        public string cityName { get; set; }
        public string CenterName { get; set; }
        public long Telephone { get; set; }

        public string Bucht { get; set; }
        public int Cabinet { get; set; }
        public long CabinetInput { get; set; }
        public int Post { get; set; }
        public long PostContact { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string InstallationDatePersian { get; set; }
    }

    public class WarningHistoryReportInfo
    {
        public string CenterName { get; set; }
        public string cityName { get; set; }
        public long? Telephone { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public DateTime? InsertDate { get; set; }
        public string DatePersian { get; set; }
        public string InsertDatePersian { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }
        public DateTime? ArrestLetterNoDate { get; set; }
        public string ArrestLetterNoDatePersian { get; set; }

        public string CustomerName { get; set; }
    }

    public class RoundTelephoneReportInfo
    {
        public string CenterName { get; set; }
        public string cityName { get; set; }
        public string Precode { get; set; }
        public long Telephone { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public string RoundType { get; set; }
    }

    public class CustomerNationalCodeReportInfo
    {
        public string CenterName { get; set; }
        public string cityName { get; set; }
        public long? TelephoneNo { get; set; }
        public string CustomerID { get; set; }
        public string PersonType { get; set; }
        public string NationalID { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string BirthCertificateID { get; set; }
        public string BirthDateOrRecordDate { get; set; }
        public string IssuePlace { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Agency { get; set; }
        public string AgencyNumber { get; set; }
        public string InsertDate { get; set; }

    }


    public class CustomerOfficeReportInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? TelephoneNo { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string CenterName { get; set; }
        public string FirstNameOrTitle { get; set; }
        public DateTime? InsertDate { get; set; }
        public string InsertDatePersian { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndDatePersian { get; set; }
        public string RequesterName { get; set; }
        public string customerLastName { get; set; }

    }

    public class TranslationPCMToNormalNetworkReportInfo
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PostalCode { get; set; }
        public long? Telephone { get; set; }
        public string Type { get; set; }
        public string BeforCabinet { get; set; }
        public string BeforCabinetInput { get; set; }
        public string BeforPost { get; set; }
        public string BeforPostContact { get; set; }
        public string BeforMUID { get; set; }

        public string AfterCabinet { get; set; }

        public string AfterCabinetInput { get; set; }

        public string AfterPost { get; set; }

        public string AfterPostContact { get; set; }

        public string AfterMUID { get; set; }
    }

    public class TranslationPCMToNormalMDFReportInfo
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }

        public string PostalCode { get; set; }
        public long? Telephone { get; set; }
        public string Type { get; set; }
        public string MUD { get; set; }
        public string PCMBucht { get; set; }
        public string Bucht { get; set; }
        public string ToMUD { get; set; }
        public string ToPCMBucht { get; set; }
        public string ToBucht { get; set; }
    }

    public class SpecialWireReportInfo
    {
        public long? Telephone { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PostalCode { get; set; }

        public string SpecialType { get; set; } // نوع نقطه
        public string BuchtType { get; set; } // نوع بوخت دیگر
        public string Bucht { get; set; }
        public string OtherBucht { get; set; } // بوخت دیگر
        public string SecondOtherBucht { get; set; } // بوخت دوم دیگر
        public string Cabinet { get; set; }
        public string CabinetInput { get; set; }
        public string Post { get; set; }
        public string PostContact { get; set; }

        public int OtherColumnNo { get; set; }
        public int OtherRowNo { get; set; }
        public long OtherBuchtNo { get; set; }


        public int ColumnNo { get; set; }
        public int RowNo { get; set; }
        public long BuchtNo { get; set; }







    }

    public class TranslationOpticalCabinetToNormalReportInfo
    {
        public long RequestID { get; set; }
        public string ToTelephoneNo { get; set; }
        public string FromTelephoneNo { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public string InstallAddress { get; set; }
        public string InstallPostalCode { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string CorrespondencePostalCode { get; set; }
    }

    public class TranslationOpticalCabinetToNormalRequestReportInfo
    {
        public string RowNumber { get; set; }
        public string RequestDate { get; set; }
        public string RequestId { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string NationalCode { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public string OldTelephone { get; set; }
        public string NewTelephone { get; set; }
        public string InstallAddress { get; set; }
        public string InstallPostalCode { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string CorrespondencePostalCode { get; set; }
        public string OldCabinetNumber { get; set; }
        public string OldInputNumber { get; set; }
        public string OldPostNumber { get; set; }
        public string OldConnectionNo { get; set; }
        public string NewCabinetNumber { get; set; }
        public string NewInputNumber { get; set; }
        public string NewPostNumber { get; set; }
        public string NewConnectionNo { get; set; }
        public string OldMdfNumber { get; set; }
        public string OldVerticalColumnNo { get; set; }
        public string OldVerticalRowNo { get; set; }
        public string OldBuchtNo { get; set; }
        public string NewMdfNumber { get; set; }
        public string NewVerticalColumnNo { get; set; }
        public string NewVerticalRowNo { get; set; }
        public string NewBuchtNo { get; set; }
    }

    /// <summary>
    /// .این کلاس برای گزارش گیری از لیستی میباشد که تعیین میکند کدام مشترکین دارای کد ملی میباشند
    /// </summary>
    public class CustomerStatisticsInfo
    {
        public long CustomerID { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }
        public string CustomerName { get; set; }
        public long TelephoneNo { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string FatherName { get; set; }
        public string BirthCertificateID { get; set; }
        public string BirthDateOrRecordDate { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
    }


    public class CustomerFormInfo
    {
        public long ID { get; set; }
        public string CustomerID { get; set; }
        public string PersonType { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
        public string FirstNameOrTitle { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string BirthCertificateID { get; set; }
        public string BirthDateOrRecordDate { get; set; }
        public string IssuePlace { get; set; }
        public string UrgentTelNo { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Agency { get; set; }
        public string AgencyNumber { get; set; }
        public long? TelephoneNo { get; set; }

        public string PostalCode { get; set; }
        public string CityName { get; set; }
        public string CenterName { get; set; }


        public byte PersonTypeByte { get; set; }

        public string NationalID { get; set; }
        public bool? IsAuthenticated { get; set; }
    }

    public class AdjacentPostList
    {
        public int ID { get; set; }
        public int PostID { get; set; }
        public int PostNumber { get; set; }

        public int AdjacentPostID { get; set; }
        public int AdjacentPostNumber { get; set; }
    }

    public class Failure117TotalReport
    {
        public int Total { get; set; }
        public int FailureTotalThisMonth { get; set; }
        public int Repetitive { get; set; }
        public int NightUB { get; set; }
        public int MeditationCut { get; set; }
        public int WellAfterTest { get; set; }
        public int Bargardankhesarat { get; set; }
        public int SendToSalonDastgah { get; set; }
        public int RemaindPastMonth { get; set; }
        public int RemaindPastMonthNetwork { get; set; }
        public int FailureCompleteTotalThisMonth { get; set; }
        public int AdamTatbigheKhat { get; set; }
        public int CableandSimHavaii { get; set; }
        public int GholabandPost { get; set; }
        public int JabeTaghsim { get; set; }
        public int PCM_4 { get; set; }
        public int SarDakhele { get; set; }
        public int RanjeKafo { get; set; }
        public int SimkeshiGheireMojaz { get; set; }
        public int BehineSazi { get; set; }
        public int Unknown { get; set; }
        public int AbooneCabel { get; set; }
        public int MarkaziCabel { get; set; }
        public int ErtebatCabel { get; set; }
        public int EkhtesasiCabel { get; set; }
        public int MafaselHavaii { get; set; }
        public int PostandSarCabel { get; set; }
        public int Khesarat { get; set; }
        public int Bargardan { get; set; }
        public int BuchExchange { get; set; }
        public int PostExchange { get; set; }
        public int EtesaliExchange { get; set; }
        public int KafoNoori { get; set; }
        public int WLL { get; set; }
        public int MDFInside { get; set; }
        public int SimkeshiDakheli { get; set; }
        public int EslahMoshtarek { get; set; }
        public int TelephoneDevice { get; set; }
        public int SalempasazMoraje { get; set; }
        public int Customernabood { get; set; }
        public int PhysicalLine { get; set; }
        public int Network { get; set; }
        public int SalonDastgah { get; set; }
        public int MDFCompelete { get; set; }
        public int RemaindThisMonthMDF { get; set; }
        public int RemaindThisMonthNetwork { get; set; }
        public int Hour_1 { get; set; }
        public int Hour_2 { get; set; }
        public int Hour_3 { get; set; }
        public int Hour_6 { get; set; }
        public int Hour_12 { get; set; }
        public int Hour_24 { get; set; }
        public int Hour_36 { get; set; }
        public int Hour_48 { get; set; }
        public int Hour_72 { get; set; }
        public int Hour_N { get; set; }
    }

    public class Failure117TotalReportSemnan
    {
        public int FailureTotalThisMonth { get; set; }
        public int AnotherCenter { get; set; }
        public int Repetitive { get; set; }
        public int cancelation { get; set; }
        public int WrongBusy { get; set; }
        public int CustomerSection { get; set; }
        public int RightAfterTest { get; set; }
        public int Bargardan_Nosazi { get; set; }
        public int Changes { get; set; }
        public int SendToSalon { get; set; }
        public int RemaindPastMonth { get; set; }
        public int FailuroFormCount { get; set; }
        public int FailuroFormCountandCancelation { get; set; }
        public int Network { get; set; }
        public int Cable { get; set; }
        public int PCM { get; set; }
        public int KafoNoori { get; set; }
        public int MDF { get; set; }
        public int Salon { get; set; }
        public int ClinetNormal { get; set; }
        public int ClientFX { get; set; }
        public int ClientSpecialWire { get; set; }
        public int ClinetPublic { get; set; }
        public int RemaindThisMonthMDF { get; set; }
        public int RemaindThisMonthNetwork { get; set; }
        public int Hour_1 { get; set; }
        public int Hour_2 { get; set; }
        public int Hour_3 { get; set; }
        public int Hour_4 { get; set; }
        public int Hour_5 { get; set; }
        public int Hour_6 { get; set; }
        public int Hour_12 { get; set; }
        public int Hour_24 { get; set; }
        public int Hour_36 { get; set; }
        public int Hour_48 { get; set; }
        public int Hour_72 { get; set; }
        public int Hour_N { get; set; }
        public double NetworkPercent { get; set; }
        public double CablePercent { get; set; }
        public double ClinetNormalPercent { get; set; }
        public string SpeedofFailure { get; set; }
    }

    public class FailureSpeedInfo
    {
        public DateTime InsertDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class TeleInfo
    {
        public long TelephoneNo { get; set; }
        public long? CustomerAddressID { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public long? CustomerID { get; set; }
        public byte TelephoneNoStatus { get; set; }
        public string TelephoneNoStatusName { get; set; }
        public long Dept { get; set; }
        public string CauseOfCut { get; set; }
        public string ClassTelephone { get; set; }
        public bool? IsVIP { get; set; }
        public bool? IsRound { get; set; }
        public string RoundType { get; set; }
        public byte Status { get; set; }
        public string OldCustomer { get; set; }
        public string SpecialService { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? InitialInstallationDate { get; set; }
        public DateTime? InitialDischargeDate { get; set; }
        public DateTime? TelDischargeDate { get; set; }
        public DateTime? CutDate { get; set; }
        public DateTime? ConnectDate { get; set; }
        public DateTime? LastPaidBillDate { get; set; }

        public string UsageType { get; set; }

        public string CauseOfTakePossession { get; set; }
    }

    public class PAPTotalReportInfo
    {
        public int CenterID { get; set; }
        public string Center { get; set; }
        public string City { get; set; }
        public int PAPID { get; set; }
        public string PAP { get; set; }
        public string Ports { get; set; }
        public string InstalCompleted { get; set; }
        public string InstalRejected { get; set; }
        public string InstalCompletedThisYear { get; set; }
        public string DischargeCompleted { get; set; }
        public string DischargeRejected { get; set; }
        public string DischargeCompletedThisYear { get; set; }
        public string ExchangeCompleted { get; set; }
        public string ExchangeRejected { get; set; }
        public string ExchangeCompletedThisYear { get; set; }
        public string Busy { get; set; }
    }

    public class PAPTechnicalReportInfo
    {
        public string Center { get; set; }
        public string TelephoneNo { get; set; }
        public string PAPName { get; set; }
        public int Cabinet { get; set; }
        public int CabinetInput { get; set; }
        public int Post { get; set; }
        public int PostConnection { get; set; }
        public string Bucht { get; set; }
        public int BuchtRow { get; set; }
        public int BuchtColumn { get; set; }
        public int BuchtBucht { get; set; }
        public string Port { get; set; }
        public int PortRow { get; set; }
        public int PortColumn { get; set; }
        public int PortBucht { get; set; }
        public string InstallDate { get; set; }
    }

    public class BlackListAddressReportInfo
    {
        public string Reason { get; set; }
        public string Center { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }
        public string ArrestLetterNoDate { get; set; }

        public string CreateUser { get; set; }
        public string ExistUser { get; set; }
        public string CreateDate { get; set; }

        public string ExistDate { get; set; }


    }


    public class BlackListCustomerReportInfo
    {
        public string Reason { get; set; }

        public string Customer { get; set; }
        public string TelephoneStatus { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }
        public string ArrestLetterNoDate { get; set; }

        public string CreateUser { get; set; }
        public string ExistUser { get; set; }
        public string CreateDate { get; set; }
        public string ExistDate { get; set; }

        public string NationalCode { get; set; }

    }

    public class BlackListTelephoneReportInfo
    {
        public string Reason { get; set; }
        public string NationalCode { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string ArrestReference { get; set; }
        public string ArrestLetterNo { get; set; }
        public string ArrestLetterNoDate { get; set; }
        public long? TelephoneNo { get; set; }
        public string Center { get; set; }
        public string TelephoneStatus { get; set; }
        public string CreateUser { get; set; }
        public string ExistUser { get; set; }
        public string CreateDate { get; set; }
        public string ExistDate { get; set; }
    }

    public class DataGridSelectedIndex
    {
        public int Index { get; set; }
        public string Header { get; set; }

        public string bindingPath { get; set; }

    }

    public class DataGridColumnsInfo
    {
        public string ClipboardContentBindingPath { get; set; }
        public string Header { get; set; }

    }

    public class PAPBillingInfo
    {
        public string PAPCode { get; set; }
        public string PAPName { get; set; }
        public string Count24 { get; set; }
        public string Count48 { get; set; }
        public string Count72 { get; set; }
        public string CountAll { get; set; }
    }


    public class GeneralTranslationCabinetInputInfo
    {
        public List<Telephone> Telephones { get; set; }
        public List<CabinetInput> CabinetInputs { get; set; }
        public List<Bucht> Buchts { get; set; }

    }
    public class TranslationOpticalCabinetToNormalPost
    {
        public int FromPostID { get; set; }
        public int ToPostID { get; set; }
        public bool PCMToNormal { get; set; }

    }


    public class ADSLBucht
    {
        public int? ColumnNo { get; set; }
        public int? RowNo { get; set; }
        public long? BuchtNo { get; set; }

        public string PAPName { get; set; }
    }

    public class FormItems
    {
        public int ItemType { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDefaultValue { get; set; }

        public bool ItemAllowNull { get; set; }
    }


    public class WiringGroupedInfo
    {
        public long RequestID { get; set; }

        public long? TelephonNo { get; set; }
        public string Customer { get; set; }
        public int? Cabinet { get; set; }

        public long? CabinetInput { get; set; }
        public int? Post { get; set; }
        public int? PostContact { get; set; }

        public long? PostContactID { get; set; }
        public string Address { get; set; }
        public int? MDF { get; set; }
        public int? Column { get; set; }
        public int? Row { get; set; }
        public long? Bucht { get; set; }

        public long? BuchtID { get; set; }
        public int? PCMMDF { get; set; }
        public int? PCMColumn { get; set; }
        public int? PCMRow { get; set; }
        public long? PCMBucht { get; set; }
        public long? PCMBuchtID { get; set; }
        public int? ADSLMDF { get; set; }
        public int? ADSLColumn { get; set; }
        public int? ADSLRow { get; set; }
        public long? ADSLBucht { get; set; }
    }


    public class ChangeTelephone
    {

        long _requestID;

        public long RequestID
        {
            get { return _requestID; }
            set { _requestID = value; }
        }

        int _requestType;

        public int RequestType
        {
            get { return _requestType; }
            set { _requestType = value; }
        }


        string _requestTypeName;

        public string RequestTypeName
        {
            get { return _requestTypeName; }
            set { _requestTypeName = value; }
        }

        //TODO:rad 13950121
        private string _cityName;

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }

        string _centerName;

        public string CenterName
        {
            get { return _centerName; }
            set { _centerName = value; }
        }



        string _insertDate;

        public string InsertDate
        {
            get { return _insertDate; }
            set { _insertDate = value; }
        }

        string _endDate;

        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        long _telephoneNo;

        public long TelephoneNo
        {
            get { return _telephoneNo; }
            set { _telephoneNo = value; }
        }


        string _preCodeTypeName;

        public string PreCodeTypeName
        {
            get { return _preCodeTypeName; }
            set { _preCodeTypeName = value; }
        }





        long? _newTelephoneNo;

        public long? NewTelephoneNo
        {
            get { return _newTelephoneNo; }
            set { _newTelephoneNo = value; }
        }


        string _personType;
        public string PersonType
        {
            get { return _personType; }
            set { _personType = value; }
        }


        string _nationalCodeOrRecordNo;
        public string NationalCodeOrRecordNo
        {
            get { return _nationalCodeOrRecordNo; }
            set { _nationalCodeOrRecordNo = value; }
        }

        string _firstNameOrTitle;

        public string FirstNameOrTitle
        {
            get { return _firstNameOrTitle; }
            set { _firstNameOrTitle = value; }
        }

        string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        string _newPersonType;
        public string NewPersonType
        {
            get { return _newPersonType; }
            set { _newPersonType = value; }
        }


        string _newNationalCodeOrRecordNo;
        public string NewNationalCodeOrRecordNo
        {
            get { return _newNationalCodeOrRecordNo; }
            set { _newNationalCodeOrRecordNo = value; }
        }

        string _newFirstNameOrTitle;

        public string NewFirstNameOrTitle
        {
            get { return _newFirstNameOrTitle; }
            set { _newFirstNameOrTitle = value; }
        }

        string _newLastName;

        public string NewLastName
        {
            get { return _newLastName; }
            set { _newLastName = value; }
        }

        string _installAddressPostalCode;

        public string InstallAddressPostalCode
        {
            get { return _installAddressPostalCode; }
            set { _installAddressPostalCode = value; }
        }

        string _installAddress;

        public string InstallAddress
        {
            get { return _installAddress; }
            set { _installAddress = value; }
        }


        string _correspondenceAddressPostalCode;

        public string CorrespondenceAddressPostalCode
        {
            get { return _correspondenceAddressPostalCode; }
            set { _correspondenceAddressPostalCode = value; }
        }

        string _correspondenceAddress;

        public string CorrespondenceAddress
        {
            get { return _correspondenceAddress; }
            set { _correspondenceAddress = value; }
        }


        string _newInstallAddressPostalCode;

        public string NewInstallAddressPostalCode
        {
            get { return _newInstallAddressPostalCode; }
            set { _newInstallAddressPostalCode = value; }
        }

        string _newInstallAddress;

        public string NewInstallAddress
        {
            get { return _newInstallAddress; }
            set { _newInstallAddress = value; }
        }


        string _newCorrespondenceAddressPostalCode;

        public string NewCorrespondenceAddressPostalCode
        {
            get { return _newCorrespondenceAddressPostalCode; }
            set { _newCorrespondenceAddressPostalCode = value; }
        }


        string _newCorrespondenceAddress;

        public string NewCorrespondenceAddress
        {
            get { return _newCorrespondenceAddress; }
            set { _newCorrespondenceAddress = value; }
        }


        int _telephoneType;

        public int TelephoneType
        {
            get { return _telephoneType; }
            set { _telephoneType = value; }
        }

        string _telephoneTypeTitle;
        public string TelephoneTypeTitle
        {
            get { return _telephoneTypeTitle; }
            set { _telephoneTypeTitle = value; }
        }
        int? _telephoneTypeGroup;
        public int? TelephoneTypeGroup
        {
            get { return _telephoneTypeGroup; }
            set { _telephoneTypeGroup = value; }
        }

        string _telephoneTypeGroupTitle;
        public string TelephoneTypeGroupTitle
        {
            get { return _telephoneTypeGroupTitle; }
            set { _telephoneTypeGroupTitle = value; }
        }


        int? _causeOfCut;

        public int? CauseOfCut
        {
            get { return _causeOfCut; }
            set { _causeOfCut = value; }
        }

        string _causeOfCutTitle;

        public string CauseOfCutTitle
        {
            get { return _causeOfCutTitle; }
            set { _causeOfCutTitle = value; }
        }

        int? _causeOfChangeNo;

        public int? CauseOfChangeNo
        {
            get { return _causeOfChangeNo; }
            set { _causeOfChangeNo = value; }
        }

        string _causeOfChangeNoTitle;

        public string CauseOfChangeNoTitle
        {
            get { return _causeOfChangeNoTitle; }
            set { _causeOfChangeNoTitle = value; }
        }
        int? _causeOfRefundDeposit;

        public int? CauseOfRefundDeposit
        {
            get { return _causeOfRefundDeposit; }
            set { _causeOfRefundDeposit = value; }
        }

        string _causeOfRefundDepositTitle;

        public string CauseOfRefundDepositTitle
        {
            get { return _causeOfRefundDepositTitle; }
            set { _causeOfRefundDepositTitle = value; }
        }

        int? _causeOfTakePossession;

        public int? CauseOfTakePossession
        {
            get { return _causeOfTakePossession; }
            set { _causeOfTakePossession = value; }
        }

        string _causeOfTakePossessionTitle;

        public string CauseOfTakePossessionTitle
        {
            get { return _causeOfTakePossessionTitle; }
            set { _causeOfTakePossessionTitle = value; }
        }

        string _classTelephoneTitle;

        public string ClassTelephoneTitle
        {
            get { return _classTelephoneTitle; }
            set { _classTelephoneTitle = value; }
        }

        List<RequestCost> _requestCost;

        public List<RequestCost> RequestCost
        {
            get { return _requestCost; }
            set { _requestCost = value; }
        }

        private bool? isOutBound;
        public bool? IsOutBound
        {
            get { return isOutBound; }
            set { isOutBound = value; }
        }
    }
    public class RequestCost
    {
        long _requestID;

        public long RequestID
        {
            get { return _requestID; }
            set { _requestID = value; }
        }

        string _baseCostTitle;
        public string BaseCostTitle
        {
            get { return _baseCostTitle; }
            set { _baseCostTitle = value; }
        }


        string _otherCostTitle;
        public string OtherCostTitle
        {
            get { return _otherCostTitle; }
            set { _otherCostTitle = value; }
        }

        long? _cost;
        public long? Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        long? _amountSum;
        public long? AmountSum
        {
            get { return _amountSum; }
            set { _amountSum = value; }
        }

        string _ficheNunmber;
        public string FicheNunmber
        {
            get { return _ficheNunmber; }
            set { _ficheNunmber = value; }
        }

        DateTime? _ficheDate;
        public DateTime? FicheDate
        {
            get { return _ficheDate; }
            set { _ficheDate = value; }
        }

        DateTime? _paymentDate;
        public DateTime? PaymentDate
        {
            get { return _paymentDate; }
            set { _paymentDate = value; }
        }


        bool? _isPaid;
        public bool? IsPaid
        {
            get { return _isPaid; }
            set { _isPaid = value; }
        }

        bool? _isKickedBack;
        public bool? IsKickedBack
        {
            get { return _isKickedBack; }
            set { _isKickedBack = value; }
        }

    }

    public class InvestigatePossibilityWaitngListChangeInfo
    {
        public int oldCabinetID { get; set; }
        public int oldPostID { get; set; }
        public int newCabinetID { get; set; }
        public int? newPostID { get; set; }

    }

    public class ObjectChanges
    {
        public string field { get; set; }
        public object oldValue { get; set; }
        public object newValue { get; set; }

    }

    public class RequestThatHasTelecomminucationServiceStatistics
    {
        public string CenterName { get; set; }
        public long RequestID { get; set; }
        public string RequestTypeTitle { get; set; }
        public string InsertDate { get; set; }
        public string EndDate { get; set; }
        public string CustomerName { get; set; }
        public string PersonType { get; set; }
        public string NationalCodeOrRecordNo { get; set; }
    }

    public class RequestStatistics
    {
        //TODO:rad 13950121
        public string CityName { get; set; }

        public int CenterID { get; set; }
        public string Center { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int Dayri { get; set; }
        public int Dischargin { get; set; }
        public int ChangeNo { get; set; }
        public int CutAndEstablish { get; set; }
        public int Connect { get; set; }
        public int OpenAndCloseZero { get; set; }
        public int ChangeAddress { get; set; }
        public int E1 { get; set; }
        public int SpecialWire { get; set; }
        public int ChangeLocationCenterInside { get; set; }
        public int ChangeLocationCenterToCenter { get; set; }
        public int SpecialService { get; set; }
        public int TranslationOpticalCabinetToNormal { get; set; }
        public int TitleIn118 { get; set; }
        public int ChangeTitleIn118 { get; set; }
        public int RemoveTitleIn118 { get; set; }
    }

    public class ADSLSupportRequestInfo
    {
        public long ID { get; set; }
        public string FirstDescription { get; set; }
        public string ResultDescription { get; set; }
        public string RequesterName { get; set; }
        public string RequestDate { get; set; }
        public string ResultName { get; set; }
        public string ResultDate { get; set; }
    }


    public class E1Files
    {
        public int ID { get; set; }

        public long RequestID { get; set; }

        public System.Guid FileID { get; set; }

        public System.DateTime InsertDate { get; set; }

        public string FileType { get; set; }
    }

    public class SpaceAndPowerFile
    {
        public int ID { get; set; }

        public long RequestID { get; set; }

        public Guid FileID { get; set; }

        public DateTime InsertDate { get; set; }

        public string FileType { get; set; }
    }

    public class TechnicalRequestInfo
    {
        public long? ID { get; set; }
        public long? RequestID { get; set; }

        public string CenterName { get; set; }

        public string RequestTypeName { get; set; }

        public string InsertDate { get; set; }

        public string EndDate { get; set; }

        public string FirstNameOrTitle { get; set; }

        public string LastName { get; set; }

        public string PersonType { get; set; }

        public long? OldTelephoneNo { get; set; }
        public long? NewTelephoneNo { get; set; }

        public string PreCodeTypeName { get; set; }

        public int? OldPost { get; set; }
        public long? OldPostContact { get; set; }
        public int? OldCabinet { get; set; }
        public long? OldCabinetInput { get; set; }

        public int? OldColumnNo { get; set; }
        public int? OldRowNo { get; set; }
        public long? OldBuchtNo { get; set; }



        public int? NewPost { get; set; }
        public long? NewPostContact { get; set; }
        public int? NewCabinet { get; set; }
        public long? NewCabinetInput { get; set; }


        public int? NewColumnNo { get; set; }
        public int? NewRowNo { get; set; }
        public long? NewBuchtNo { get; set; }
        public long? NewPostContactID { get; set; }
        public long? OldPostContactID { get; set; }
    }


    public class PCMBuchtTelephonInfo
    {
        public Bucht Bucht { get; set; }

        public PostContact PostContact { get; set; }

        public Telephone Telephone { get; set; }

        public int PCMID { get; set; }

        public int PortNo { get; set; }
    }

    public class PCMBuchtTelephonReportInfo
    {
        public long ID { get; set; }
        public long? OldTelephonNo { get; set; }
        public int OldRock { get; set; }
        public int OldShelf { get; set; }
        public int OldCard { get; set; }
        public int OldColumnNo { get; set; }
        public int OldRowNo { get; set; }
        public long OldBuchtNo { get; set; }
        public int NewRock { get; set; }
        public int NewShelf { get; set; }
        public int NewCard { get; set; }
        public int NewColumnNo { get; set; }
        public int NewRowNo { get; set; }
        public long NewBuchtNo { get; set; }
    }

    public class Failure117Karaj
    {
        public long ID { get; set; }
        public string TelephoneNo { get; set; }
        public string CallingNo { get; set; }
        public string InsertDate { get; set; }
    }


    public class ADSLIRAN
    {
        public long C_P_ID { get; set; }
        public int Os_Code { get; set; }
        public int Ci_Code { get; set; }
        public int Cen_Code { get; set; }
        public int Pap_Code { get; set; }
        public long Tel_Num { get; set; }
        public string S_Date { get; set; }
        public string S_Time { get; set; }
        public string E_Date { get; set; }
        public string E_Time { get; set; }
        public int Kind_Code { get; set; }
        public int Fi_Code { get; set; }
        public int Work_Status { get; set; }
        public int Ci_Pish_Code { get; set; }
        public int Project_Id { get; set; }
        public int Company_Base_Id { get; set; }
        public string Mdf_Comment { get; set; }
        public string Confirm_Date { get; set; }
        public string Confirm_Time { get; set; }
    }

    public class FailureUBInfo
    {
        public long TelephoneNo { get; set; }
        public string Center { get; set; }
        public string Bucht { get; set; }
        public string OtherBucht { get; set; }
        public string Repeatative { get; set; }
        public string UBDate { get; set; }
        public string LastFailureDate { get; set; }
    }

    public class TranslationCentralCabinetInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int? OldCabinet { get; set; }

        public int? OldCabinetNumber { get; set; }
        public long? OldCabinetInputID { get; set; }
        public long? OldCabinetInputNumber { get; set; }

        public int? OldPostNumber { get; set; }
        public int? OldPostID { get; set; }
        public int? OldPostContactNumber { get; set; }
        public long? OldPostContactID { get; set; }
        public long? OldBuchtID { get; set; }
        public string OldConnectionNo { get; set; }

        public int? OldColumnNo { get; set; }

        public int? OldRowNo { get; set; }

        public long? OldBuchtNo { get; set; }


        public int? NewColumnNo { get; set; }

        public int? NewRowNo { get; set; }

        public long? NewBuchtNo { get; set; }

        public int? OtherColumnNo { get; set; }

        public int? OtherRowNo { get; set; }

        public long? OtherBuchtNo { get; set; }

        public string OtherConnectionNo { get; set; }

        public string CustomerAddress { get; set; }

        public string PostallCode { get; set; }
        public string AfterPostallCode { get; set; }
        public int? OldBuchtStatus { get; set; }
        public long? OldTelephonNo { get; set; }

        public int? OldSwitchPortID { get; set; }

        public long? _oldCounter;
        public long? OldCounter
        {
            get
            {
                return _oldCounter;
            }
            set
            {
                _oldCounter = value;
                NotifyPropertyChanged("OldCounter");
            }
        }
        public bool? OldIsVIP { get; set; }
        public bool? OldIsRound { get; set; }
        public string CustomerName { get; set; }
        public string AfterCustomerName { get; set; }
        public int? NewCabinet { get; set; }
        public int? NewCabinetNumber { get; set; }
        public long? NewCabinetInputID { get; set; }
        public int? NewCabinetInputNumber { get; set; }
        public long? NewBuchtID { get; set; }
        public string NewConnectionNo { get; set; }
        public int? NewBuchtStatus { get; set; }

        public long? _newTelephonNo;
        public long? NewTelephonNo
        {
            get
            {
                return _newTelephonNo;
            }
            set
            {
                _newTelephonNo = value;
                NotifyPropertyChanged("NewTelephonNo");
            }
        }

        public int? NewSwitchPortID { get; set; }

        public long? _newCounter;
        public long? NewCounter
        {
            get
            {
                return _newCounter;
            }
            set
            {
                _newCounter = value;
                NotifyPropertyChanged("NewCounter");
            }
        }

        public int? _newPreCodeID;
        public int? NewPreCodeID
        {
            get
            {
                return _newPreCodeID;
            }
            set
            {
                _newPreCodeID = value;
                NotifyPropertyChanged("NewPreCodeID");
            }
        }

        public long? _newPreCodeNumber;
        public long? NewPreCodeNumber
        {
            get
            {
                return _newPreCodeNumber;
            }
            set
            {
                _newPreCodeNumber = value;
                NotifyPropertyChanged("NewPreCodeNumber");
            }
        }

        public int? NewPostNumber { get; set; }

        public int? NewPostID { get; set; }
        public int? NewPostConntactNumber { get; set; }

        public long? NewPostContactID { get; set; }

        public string CompletionDate { get; set; }

        public string SpecialService { get; set; }

        public string TelephoneClass { get; set; }

        public string OldCabinetUsageType { get; set; }

        public string NewCabinetUsageType { get; set; }

        public bool HasAdsl { get; set; }

        public string CutAndEstablishStatus { get; set; }

        public string CauseOfCut { get; set; }

        public string CityName { get; set; }

        public string CenterName { get; set; }

        public long RequestID { get; set; }

        public int? PcmCabinetInputColumnNo { get; set; }
        public int? PcmCabinetInputRowNo { get; set; }
        public long? PcmCabinetInputBuchtNo { get; set; }

        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }

    public class ExchangeGSMInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int? Cabinet { get; set; }

        public int? CabinetNumber { get; set; }
        public long? CabinetInputID { get; set; }
        public long? CabinetInputNumber { get; set; }

        public int? PostNumber { get; set; }
        public int? PostID { get; set; }
        public int? PostContactNumber { get; set; }
        public long? PostContactID { get; set; }
        public long? BuchtID { get; set; }
        public string ConnectionNo { get; set; }

        public int? ColumnNo { get; set; }

        public int? RowNo { get; set; }

        public long? BuchtNo { get; set; }


        public int? OtherColumnNo { get; set; }

        public int? OtherRowNo { get; set; }

        public long? OtherBuchtNo { get; set; }

        public string OtherConnectionNo { get; set; }

        public string CustomerAddress { get; set; }

        public string PostallCode { get; set; }
        public int? BuchtStatus { get; set; }
        public long? FromTelephonNo { get; set; }

        public long? _fromCounter;
        public long? FromCounter
        {
            get
            {
                return _fromCounter;
            }
            set
            {
                _fromCounter = value;
                NotifyPropertyChanged("FromCounter");
            }
        }


        public long? _toTelephonNo;
        public long? ToTelephonNo
        {
            get
            {
                return _toTelephonNo;
            }
            set
            {
                _toTelephonNo = value;
                NotifyPropertyChanged("ToTelephonNo");
            }
        }

        public long? FromSwitchPreCodeNumber;
        public long? ToSwitchPreCodeNumber;

        public byte? FromTelephoneStatus;
        public byte? ToTelephoneStatus;


        public int? _fromSwitchPreCodeID;
        public int? FromSwitchPreCodeID
        {
            get
            {
                return _fromSwitchPreCodeID;
            }
            set
            {
                _fromSwitchPreCodeID = value;
                NotifyPropertyChanged("FromSwitchPreCodeID");
            }
        }


        public int? _toSwitchPreCodeID;
        public int? ToSwitchPreCodeID
        {
            get
            {
                return _toSwitchPreCodeID;
            }
            set
            {
                _toSwitchPreCodeID = value;
                NotifyPropertyChanged("ToSwitchPreCodeID");
            }
        }
        public long? _toCounter;
        public long? ToCounter
        {
            get
            {
                return _toCounter;
            }
            set
            {
                _toCounter = value;
                NotifyPropertyChanged("ToCounter");
            }
        }
        public bool? IsVIP { get; set; }
        public bool? IsRound { get; set; }
        public string CustomerName { get; set; }

        public string CompletionDate { get; set; }

        public string CutAndEstablishStatus { get; set; }

        public string CauseOfCut { get; set; }

        public string CityName { get; set; }

        public string CenterName { get; set; }

        public long RequestID { get; set; }

        public int? PcmCabinetInputColumnNo { get; set; }
        public int? PcmCabinetInputRowNo { get; set; }
        public long? PcmCabinetInputBuchtNo { get; set; }

        public int? AdslColumnNo { get; set; }
        public int? AdslRowNo { get; set; }
        public long? AdslBuchtNo { get; set; }


        public string TelephoneClass { get; set; }
        public string SpecialService { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }


    public class ExchangeGSMConnectionInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long? _fromTelephone;
        public long? FromTelephone
        {
            get
            {
                return _fromTelephone;
            }
            set
            {
                _fromTelephone = value;
            }
        }
        public byte? FromTelephoneStatus { get; set; }

        public int? FromSwitchPreCodeID { get; set; }
        public int? _cabinetID { get; set; }
        public int? CabinetID
        {
            get
            {
                return _cabinetID;
            }
            set
            {
                _cabinetID = value;
                NotifyPropertyChanged("CabinetID");
            }

        }
        public long? _cabinetInputID { get; set; }
        public long? CabinetInputID
        {
            get
            {
                return _cabinetInputID;
            }
            set
            {
                _cabinetInputID = value;
                NotifyPropertyChanged("CabinetInputID");
            }

        }

        public long? _cabinetInputNumber { get; set; }
        public long? CabinetInputNumber
        {
            get
            {
                return _cabinetInputNumber;
            }
            set
            {
                _cabinetInputNumber = value;
                NotifyPropertyChanged("CabinetInputNumber");
            }

        }
        public int? _cabinetNumber { get; set; }
        public int? CabinetNumber
        {
            get
            {
                return _cabinetNumber;
            }
            set
            {
                _cabinetNumber = value;
                NotifyPropertyChanged("CabinetNumber");
            }

        }
        public int? _postID { get; set; }
        public int? PostID
        {
            get
            {
                return _postID;
            }
            set
            {
                _postID = value;
                NotifyPropertyChanged("PostID");
            }

        }
        public int? PostNumber { get; set; }
        public long? _PostConntactID { get; set; }
        public long? PostConntactID
        {
            get
            {
                return _PostConntactID;
            }
            set
            {
                _PostConntactID = value;
                NotifyPropertyChanged("PostConntactID");
            }

        }
        public int? _PostConntactNumber { get; set; }
        public int? PostConntactNumber
        {
            get
            {
                return _PostConntactNumber;
            }
            set
            {
                _PostConntactNumber = value;
                NotifyPropertyChanged("PostConntactNumber");
            }

        }
        public string _connectiontype;

        public string Connectiontype
        {
            get
            {
                return _connectiontype;
            }
            set
            {
                _connectiontype = value;
                NotifyPropertyChanged("Connectiontype");
            }

        }
        public byte? AorBType { get; set; }
        public string _aorBTypeName { get; set; }

        public string AorBTypeName
        {
            get
            {
                return _aorBTypeName;
            }
            set
            {
                _aorBTypeName = value;
                NotifyPropertyChanged("AorBTypeName");
            }

        }
        public bool isApply { get; set; }
        public long? BuchtID { get; set; }
        public byte? BuchtStatus { get; set; }

        public byte? PostConntactStatus { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}

