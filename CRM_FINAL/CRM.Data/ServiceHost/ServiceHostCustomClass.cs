using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.ServiceHost
{
    public class ServiceHostCustomClass
    {
        public class CashPaymentInfo
        {
            public string CityName { get; set; }
            public string CenterName { get; set; }
            long _requestID;
            public long RequestID
            {
                get { return _requestID; }
                set { _requestID = value; }
            }

            long telephoneNo;
            public long TelephoneNo
            {
                get { return telephoneNo; }
                set { telephoneNo = value; }
            }


            long? _cost;
            public long? Cost
            {
                get { return _cost; }
                set { _cost = value; }
            }

            DateTime? _paymentDate;
            public DateTime? PaymentDate
            {
                get { return _paymentDate; }
                set { _paymentDate = value; }
            }
        }
        public class TelephoneConnectionInstallmentInfo
        {
            public string CityName { get; set; }
            public string CenterName { get; set; }

            long _requestID;
            public long RequestID
            {
                get { return _requestID; }
                set { _requestID = value; }
            }

            long telephoneNo;
            public long TelephoneNo
            {
                get { return telephoneNo; }
                set { telephoneNo = value; }
            }


            long? _cost;
            public long? Cost
            {
                get { return _cost; }
                set { _cost = value; }
            }

        }

        public class RequestType
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }


        }
        public class TechTelephoneType
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }


        }
        public class CityInfo
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }


        }
        public class CenterInfo
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _cityCode;

            public int CityCode
            {
                get { return _cityCode; }
                set { _cityCode = value; }
            }


            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }
        }
        public class TelephoneType
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }


        }
        public class TelephoneGroupType
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _telephoneType;

            public int TelephoneType
            {
                get { return _telephoneType; }
                set { _telephoneType = value; }
            }


            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }
        }
        public class TelephoneInfo
        {
            byte? _isInstallation;
            int _techType = 0;
            bool _hasADSL = false;
            int? _telephoneType = 0;

            public byte? IsInstalition
            {
                get { return _isInstallation; }
                set { _isInstallation = value; }
            }

            public int TechType
            {
                get { return _techType; }
                set { _techType = value; }
            }

            public bool HasADSL
            {
                get { return _hasADSL; }
                set { _hasADSL = value; }
            }

            public int? TelephoneType
            {
                get { return _telephoneType; }
                set { _telephoneType = value; }
            }
        }
        public class CauseOfCutInfo
        {
            string _title;

            public string Title
            {
                get { return _title; }
                set { _title = value; }
            }

            int _code;

            public int Code
            {
                get { return _code; }
                set { _code = value; }
            }
        }
        public class ChangeTelephone
        {

            long? _ID;

            public long? ID
            {
                get { return _ID; }
                set { _ID = value; }
            }

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

            string _centerName;

            public string CenterName
            {
                get { return _centerName; }
                set { _centerName = value; }
            }



            DateTime _insertDate;

            public DateTime InsertDate
            {
                get { return _insertDate; }
                set { _insertDate = value; }
            }

            DateTime _endDate;

            public DateTime EndDate
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

            long? _newTelephoneNo;

            public long? NewTelephoneNo
            {
                get { return _newTelephoneNo; }
                set { _newTelephoneNo = value; }
            }


            byte? _personType;
            public byte? PersonType
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


            string _fatherName;

            public string FatherName
            {
                get { return _fatherName; }
                set { _fatherName = value; }
            }

            DateTime? _birthDateOrRecordDate;

            public DateTime? BirthDateOrRecordDate
            {
                get { return _birthDateOrRecordDate; }
                set { _birthDateOrRecordDate = value; }
            }


            string _mobileNo;
            public string MobileNo
            {
                get { return _mobileNo; }
                set { _mobileNo = value; }
            }

            byte? _newPersonType;
            public byte? NewPersonType
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


            int? _telephoneType;

            public int? TelephoneType
            {
                get { return _telephoneType; }
                set { _telephoneType = value; }
            }

            int? _telephoneTypeGroup;

            public int? TelephoneTypeGroup
            {
                get { return _telephoneTypeGroup; }
                set { _telephoneTypeGroup = value; }
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


            string _classTelephoneTitle;

            public string ClassTelephoneTitle
            {
                get { return _classTelephoneTitle; }
                set { _classTelephoneTitle = value; }
            }

            byte? _preCodeTypeID;

            public byte? PreCodeTypeID
            {
                get { return _preCodeTypeID; }
                set { _preCodeTypeID = value; }
            }

            bool? _isOutBound;

            public bool? IsOutBound
            {
                get { return _isOutBound; }
                set { _isOutBound = value; }
            }
            //List<RequestCost> _requestCost;

            //public List<RequestCost> RequestCost
            //{
            //    get { return _requestCost; }
            //    set { _requestCost = value; }
            //}
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
        public class PAPBillingInfo
        {
            public string PAPCode { get; set; }            
            public string PAPName { get; set; }
            public string Count24 { get; set; }
            public string Count48 { get; set; }
            public string Count72 { get; set; }
            public string CountAll { get; set; }
        }
    }
}
