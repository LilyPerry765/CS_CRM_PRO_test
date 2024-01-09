using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;

namespace CRM.Data
{
    public static class CustomerDB
    {
        public static List<CustomerFormInfo> SearchCustomer(
            List<int> citiesId,
            List<int> centersId,
            List<int> personType,
            string nationalCodeOrRecordNo,
            string firstNameOrTitle,
            string lastName,
            string fatherName,
            List<int> gender,
            string birthCertificateID,
            //string customerId,
            DateTime? birthDateOrRecord,
            string issuePlace,
            string urgentTelNo,
            string mobileNo,
            string email,
            long telephoneNo,
            string postalCode,
            DB.ShahkarAuthenticationStatus authenticationStatus,
            int startRowIndex,
            int pageSize,
            out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<CustomerFormInfo> query = context.Customers
                    .GroupJoin(context.Telephones, C => C.ID, T => T.CustomerID, (C, T) => new { Customer = C, Telephone = T })
                    .SelectMany(t2 => t2.Telephone.DefaultIfEmpty(), (CT, t2) => new { Customer = CT, Telephone = t2 })
                    .Where(t =>
                                (!t.Telephone.CustomerID.HasValue || (citiesId.Count == 0 || citiesId.Contains(t.Telephone.Center.Region.CityID))) &&
                                (!t.Telephone.CustomerID.HasValue || (centersId.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Telephone.CenterID) : centersId.Contains(t.Telephone.CenterID))) &&
                                (personType.Count == 0 || personType.Contains((int)t.Customer.Customer.PersonType)) &&
                                (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.Customer.Customer.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                (string.IsNullOrEmpty(postalCode) || t.Telephone.Address.PostalCode.Contains(postalCode)) &&

                                (string.IsNullOrEmpty(firstNameOrTitle) || t.Customer.Customer.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                (string.IsNullOrEmpty(lastName) || t.Customer.Customer.LastName.Contains(lastName)) &&
                                (string.IsNullOrEmpty(fatherName) || t.Customer.Customer.FatherName.Contains(fatherName)) &&
                                (gender.Count == 0 || gender.Contains((int)t.Customer.Customer.Gender)) &&
                                (string.IsNullOrEmpty(birthCertificateID) || t.Customer.Customer.BirthCertificateID.Contains(birthCertificateID)) &&
                                (!birthDateOrRecord.HasValue || t.Customer.Customer.BirthDateOrRecordDate == birthDateOrRecord) &&
                                (string.IsNullOrEmpty(issuePlace) || t.Customer.Customer.IssuePlace.Contains(issuePlace)) &&
                                (string.IsNullOrEmpty(urgentTelNo) || t.Customer.Customer.UrgentTelNo.Contains(urgentTelNo)) &&
                                (string.IsNullOrEmpty(mobileNo) || t.Customer.Customer.MobileNo.Contains(mobileNo)) &&
                                (string.IsNullOrEmpty(email) || t.Customer.Customer.Email.Contains(email)) &&
                                    //(string.IsNullOrEmpty(customerId) || t.Customer.Customer.CustomerID.Contains(customerId)) &&
                                (telephoneNo == -1 || telephoneNo == t.Telephone.TelephoneNo) &&
                                (
                                 (authenticationStatus == DB.ShahkarAuthenticationStatus.IsAunthenticated) ?
                                 (t.Customer.Customer.IsAuthenticated.HasValue && t.Customer.Customer.IsAuthenticated.Value) :
                                 (authenticationStatus == DB.ShahkarAuthenticationStatus.NotAunthenticated) ?
                                 (t.Customer.Customer.IsAuthenticated.HasValue && !t.Customer.Customer.IsAuthenticated.Value) :
                                 (authenticationStatus == DB.ShahkarAuthenticationStatus.HasNoAunthenticationRequest) ?
                                 (!t.Customer.Customer.IsAuthenticated.HasValue) :
                                 (1 == 1)
                                )

                          ).Select(t =>
                              new CustomerFormInfo
                              {
                                  CityName = t.Telephone.Center.Region.City.Name,
                                  CenterName = t.Telephone.Center.CenterName,
                                  ID = t.Customer.Customer.ID,
                                  Agency = t.Customer.Customer.Agency,
                                  AgencyNumber = t.Customer.Customer.AgencyNumber,
                                  BirthCertificateID = t.Customer.Customer.BirthCertificateID,
                                  BirthDateOrRecordDate = context.mi2sh(t.Customer.Customer.BirthDateOrRecordDate, true),
                                  //CustomerID = t.Customer.Customer.CustomerID,
                                  Email = t.Customer.Customer.Email,
                                  FatherName = t.Customer.Customer.FatherName,
                                  FirstNameOrTitle = t.Customer.Customer.FirstNameOrTitle,
                                  Gender = Helpers.GetEnumDescription(t.Customer.Customer.Gender, typeof(DB.Gender)),
                                  IssuePlace = t.Customer.Customer.IssuePlace,
                                  LastName = t.Customer.Customer.LastName,
                                  MobileNo = t.Customer.Customer.MobileNo,
                                  NationalCodeOrRecordNo = t.Customer.Customer.NationalCodeOrRecordNo,
                                  PersonType = Helpers.GetEnumDescription(t.Customer.Customer.PersonType, typeof(DB.PersonType)),
                                  UrgentTelNo = t.Customer.Customer.UrgentTelNo,
                                  TelephoneNo = t.Telephone.TelephoneNo,
                                  PostalCode = t.Telephone.Address.PostalCode,
                                  PersonTypeByte = t.Customer.Customer.PersonType,
                                  NationalID = t.Customer.Customer.NationalID,
                                  IsAuthenticated = t.Customer.Customer.IsAuthenticated
                              });



                count = query.Count();

                return query.Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchCustomersCount(
            List<int> personType,
            string nationalCodeOrRecordNo,
            string firstNameOrTitle,
            string lastName,
            string fatherName,
            List<int> gender,
            string birthCertificateID,
            DateTime? birthDateOrRecord,
            string issuePlace,
            string urgentTelNo,
            string mobileNo,
            string email
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers
                    .Where(t => (personType.Count == 0 || personType.Contains((int)t.PersonType)) &&
                                (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                (string.IsNullOrEmpty(firstNameOrTitle) || t.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                (string.IsNullOrEmpty(lastName) || t.LastName.Contains(lastName)) &&
                                (string.IsNullOrEmpty(fatherName) || t.FatherName.Contains(fatherName)) &&
                                (gender.Count == 0 || gender.Contains((int)t.Gender)) &&
                                (string.IsNullOrEmpty(birthCertificateID) || t.BirthCertificateID.Contains(birthCertificateID)) &&
                                (!birthDateOrRecord.HasValue || t.BirthDateOrRecordDate == birthDateOrRecord) &&
                                (string.IsNullOrEmpty(issuePlace) || t.IssuePlace.Contains(issuePlace)) &&
                                (string.IsNullOrEmpty(urgentTelNo) || t.UrgentTelNo.Contains(urgentTelNo)) &&
                                (string.IsNullOrEmpty(mobileNo) || t.MobileNo.Contains(mobileNo)) &&
                                (string.IsNullOrEmpty(email) || t.Email.Contains(email)))
                    .Count();
            }
        }

        public static Customer GetCustomerByNationalCode(string nationalCode)
        {
            Customer customer;
            using (MainDataContext context = new MainDataContext())
            {
                customer = context.Customers.Where(t => t.NationalCodeOrRecordNo.Trim() == nationalCode.Trim()).OrderBy(t => t.ID).Take(1).SingleOrDefault();
                if (customer != null)
                    return customer;
                else
                    return null;
            }
        }

        public static List<Customer> GetCustomerListByNationalCode(string nationalCode)
        {
            List<Customer> customerList;
            using (MainDataContext context = new MainDataContext())
            {
                customerList = context.Customers.Where(t => t.NationalCodeOrRecordNo.Trim() == nationalCode.Trim()).ToList();

                if (customerList != null && customerList.Count != 0)
                    return customerList;
                else
                    return null;
            }
        }

        public static int GetCustomerByNationalCodeCount(string nationalCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.NationalCodeOrRecordNo.Trim() == nationalCode.Trim()).Count();
            }
        }

        public static Customer GetCustomerbyElkaID(long elkaID)
        {
            Customer customer;
            using (MainDataContext context = new MainDataContext())
            {
                customer = context.Customers.Where(t => t.ElkaID == elkaID).SingleOrDefault();

                if (customer != null)
                    return customer;
                else
                    return null;
            }
        }

        public static List<Customer> GetCustomer()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.ToList();
            }
        }

        public static Customer GetCustomerByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static Customer GetCustomerByCustomerID(string customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.CustomerID == customerID).SingleOrDefault();
            }
        }

        public static List<Customer> SearchCustomerInfo(
        int personType,
        string nationalCodeOrRecordNo,
        string firstNameOrTitle,
        string lastName,
        string fatherName,
        string BirthCertificateID,
        string IssuePlace,
        int gender,
        string sortParameter = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.Customers.Where(t => (t.PersonType == personType) &&
                                                    (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                                    (string.IsNullOrEmpty(firstNameOrTitle) || t.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                                    (string.IsNullOrEmpty(lastName) || t.LastName.Contains(lastName)) &&
                                                    (string.IsNullOrEmpty(fatherName) || t.FatherName.Contains(fatherName)) &&
                                                    (string.IsNullOrEmpty(BirthCertificateID) || t.FatherName.Contains(BirthCertificateID)) &&
                                                    (string.IsNullOrEmpty(IssuePlace) || t.FatherName.Contains(IssuePlace)) &&
                                                    (gender == -1 || t.Gender == gender));

                if (string.IsNullOrEmpty(sortParameter))
                    return query.OrderByDescending(t => t.ID)
                        .ToList();

                return query.OrderBy(sortParameter)
                        .ToList();
            }
        }

        public static List<Customer> SearchCustomerInfoListbyNationalCode(string natioanlCodes)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => string.Equals(natioanlCodes, t.NationalCodeOrRecordNo)).ToList();
            }
        }

        public static List<Customer> SearchCustomerInfoForWeb(
          int personType,
          string nationalCodeOrRecordNo,
          string firstNameOrTitle,
          string lastName,
          string fatherName,
          string BirthCertificateID,
          string IssuePlace,
          int gender,
          string sortParameter = null, int pageSize = 10, int startRowIndex = 0)
        {
            pageSize = 10;
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.Customers.Where(t => (t.PersonType == personType) &&
                                                    (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                                    (string.IsNullOrEmpty(firstNameOrTitle) || t.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                                    (string.IsNullOrEmpty(lastName) || t.LastName.Contains(lastName)) &&
                                                    (string.IsNullOrEmpty(fatherName) || t.FatherName.Contains(fatherName)) &&
                                                    (string.IsNullOrEmpty(BirthCertificateID) || t.FatherName.Contains(BirthCertificateID)) &&
                                                    (string.IsNullOrEmpty(IssuePlace) || t.FatherName.Contains(IssuePlace)) &&
                                                    (gender == -1 || t.Gender == gender));

                if (string.IsNullOrEmpty(sortParameter))
                    return query.OrderByDescending(t => t.ID)
                                .Skip(startRowIndex)
                                .Take(pageSize)
                                .ToList();

                return query.OrderBy(sortParameter)
                            .Skip(startRowIndex)
                            .Take(pageSize)
                            .ToList();
            }
        }

        public static int SearchCustomerInfoCount(
           int personType,
           string nationalCodeOrRecordNo,
           string firstNameOrTitle,
           string lastName,
           string fatherName,
           string BirthCertificateID,
           string IssuePlace,
           int gender)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => (t.PersonType == personType) &&
                                                    (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                                    (string.IsNullOrEmpty(firstNameOrTitle) || t.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                                    (string.IsNullOrEmpty(lastName) || t.LastName.Contains(lastName)) &&
                                                    (string.IsNullOrEmpty(fatherName) || t.FatherName.Contains(fatherName)) &&
                                                    (string.IsNullOrEmpty(BirthCertificateID) || t.FatherName.Contains(BirthCertificateID)) &&
                                                    (string.IsNullOrEmpty(IssuePlace) || t.FatherName.Contains(IssuePlace)) &&
                                                    (gender == -1 || t.Gender == gender)).Count();
            }
        }


        public static string GetFullNameByCustomerID(long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == customerID).SingleOrDefault().FirstNameOrTitle + " " + context.Customers.Where(t => t.ID == customerID).SingleOrDefault().LastName;
            }
        }

        public static string GetCustomerMobileByID(long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == customerID).SingleOrDefault().MobileNo;
            }
        }

        public static string GetCustomerMobileByElkaID(long elkaID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ElkaID == elkaID).SingleOrDefault().MobileNo;
            }
        }

        public static Customer GetCustomerByTelephone(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == context.Telephones.Where(t1 => t1.TelephoneNo == telephone).SingleOrDefault().CustomerID).SingleOrDefault();
            }
        }

        public static Customer GetCustomerByCustomerName(string Name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.FirstNameOrTitle.Contains(Name) || t.LastName.Contains(Name)).SingleOrDefault();
            }
        }

        public static List<TelephonCustomer> GetCustomerByTelephones(List<Telephone> oldTelephones)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => oldTelephones.Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo)).Select(t => new TelephonCustomer { Customer = t.Customer, TelephonNo = t.TelephoneNo }).ToList();
            }
        }

        public static List<CustomerStatisticsInfo> SearchCustomerStatisticInfos(List<int> cities, List<int> centers, bool hasNationalCode, long telephoneNo, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CustomerStatisticsInfo> finalResult = new List<CustomerStatisticsInfo>();

                var query = context.Telephones
                                   .GroupJoin(context.Customers, te => te.CustomerID, cu => cu.ID, (te, cus) => new { _Telephone = te, Customers = cus })
                                   .SelectMany(a => a.Customers.DefaultIfEmpty(), (a, cu) => new { _Telephone = a._Telephone, _Customer = cu })

                                   .Where(re =>
                                              (re._Customer.PersonType == (byte)DB.PersonType.Person) &&
                                              (cities.Count == 0 || cities.Contains(re._Telephone.Center.Region.CityID)) &&
                                              (centers.Count == 0 || centers.Contains(re._Telephone.CenterID)) &&
                                              (telephoneNo == -1 || re._Telephone.TelephoneNo == telephoneNo) &&
                                              ((hasNationalCode == true) ? !(re._Customer.NationalCodeOrRecordNo == null || re._Customer.NationalCodeOrRecordNo.Equals("")) : (re._Customer.NationalCodeOrRecordNo == null || re._Customer.NationalCodeOrRecordNo.Equals("")))
                                         )
                                   .Select(re => new CustomerStatisticsInfo
                                                 {
                                                     CityName = re._Telephone.Center.Region.City.Name,
                                                     CenterName = re._Telephone.Center.CenterName,
                                                     TelephoneNo = re._Telephone.TelephoneNo,
                                                     CustomerName = string.Format("{0} {1}", re._Customer.FirstNameOrTitle, re._Customer.LastName),
                                                     NationalCodeOrRecordNo = re._Customer.NationalCodeOrRecordNo,
                                                     FatherName = re._Customer.FatherName,
                                                     BirthCertificateID = re._Customer.BirthCertificateID,
                                                     BirthDateOrRecordDate = re._Customer.BirthDateOrRecordDate.ToPersian(Date.DateStringType.Short),
                                                 }
                                          )
                                   .AsQueryable();
                count = query.Count();

                if (pageSize != 0)
                {
                    finalResult = query.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = query.ToList();
                }

                return finalResult;
            }
        }

        public static List<CustomerStatisticsInfo> SearchCustomerStatisticInfos(
                                                                                List<int> cities, List<int> centers, bool hasNationalCode,
                                                                                long telephoneNo, DateTime? fromBirthDate, DateTime? toBirthDate,
                                                                                string firstNameOrTitle, string lastName, string birthCertificateID, string fatherName,
                                                                                bool forPrint, int startRowIndex, int pageSize, out int count
                                                                                )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CustomerStatisticsInfo> finalResult = new List<CustomerStatisticsInfo>();

                var query = context.Telephones
                                   .GroupJoin(context.Customers, te => te.CustomerID, cu => cu.ID, (te, cus) => new { _Telephone = te, Customers = cus })
                                   .SelectMany(a => a.Customers.DefaultIfEmpty(), (a, cu) => new { _Telephone = a._Telephone, _Customer = cu })

                                   .Where(re =>
                                              (re._Customer.PersonType == (byte)DB.PersonType.Person) &&

                                              (cities.Count == 0 || cities.Contains(re._Telephone.Center.Region.CityID)) &&

                                              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(re._Telephone.CenterID) : centers.Contains(re._Telephone.CenterID)) &&

                                              (telephoneNo == -1 || (DB.CurrentUser.CenterIDs.Contains(re._Telephone.CenterID) && re._Telephone.TelephoneNo == telephoneNo)) &&

                                              ((hasNationalCode == true) ? !(re._Customer.NationalCodeOrRecordNo == null || re._Customer.NationalCodeOrRecordNo.Equals("")) : (re._Customer.NationalCodeOrRecordNo == null || re._Customer.NationalCodeOrRecordNo.Equals(""))) &&
                                              (!fromBirthDate.HasValue || fromBirthDate <= re._Customer.BirthDateOrRecordDate) &&
                                              (!toBirthDate.HasValue || toBirthDate >= re._Customer.BirthDateOrRecordDate) &&
                                              (string.IsNullOrEmpty(birthCertificateID) || re._Customer.BirthCertificateID.Contains(birthCertificateID)) &&
                                              (string.IsNullOrEmpty(fatherName) || re._Customer.FatherName.Contains(fatherName)) &&
                                              (string.IsNullOrEmpty(firstNameOrTitle) || re._Customer.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                              (string.IsNullOrEmpty(lastName) || re._Customer.LastName.Contains(lastName))
                                         )
                                   .Select(re => new CustomerStatisticsInfo
                                                {
                                                    CityName = re._Telephone.Center.Region.City.Name,
                                                    CenterName = re._Telephone.Center.CenterName,
                                                    TelephoneNo = re._Telephone.TelephoneNo,
                                                    FirstNameOrTitle = re._Customer.FirstNameOrTitle,
                                                    LastName = re._Customer.LastName,
                                                    NationalCodeOrRecordNo = re._Customer.NationalCodeOrRecordNo,
                                                    FatherName = re._Customer.FatherName,
                                                    BirthCertificateID = re._Customer.BirthCertificateID,
                                                    BirthDateOrRecordDate = re._Customer.BirthDateOrRecordDate.ToPersian(Date.DateStringType.Short),
                                                }
                                          )
                                   .AsQueryable();



                if (forPrint)
                {
                    finalResult = query.ToList();
                    count = finalResult.Count;
                }
                else
                {
                    finalResult = query.Skip(startRowIndex).Take(pageSize).ToList();
                    count = query.Count();
                }

                return finalResult;
            }
        }


        public static CustomerShortInfo GetCustomerShortInfoById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                CustomerShortInfo result;
                var query = context.Customers.Where(cust => cust.ID == id)
                                             .Select(filteredData => new CustomerShortInfo
                                                                     {
                                                                         ID = filteredData.ID,
                                                                         FirstNameOrTitle = filteredData.FirstNameOrTitle,
                                                                         LastName = filteredData.LastName,
                                                                         NationalCodeOrRecordNo = filteredData.NationalCodeOrRecordNo,
                                                                         PersonType = filteredData.PersonType
                                                                     }
                                                    );
                result = query.SingleOrDefault();
                if (result != null)
                {
                    result.PersonTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PersonType), result.PersonType);
                }
                return result;
            }
        }
    }
}