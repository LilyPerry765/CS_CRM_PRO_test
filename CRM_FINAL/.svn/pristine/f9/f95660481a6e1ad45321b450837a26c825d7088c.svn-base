using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class BlackListDB
    {
        public static BlackListInfo GetBlackListById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists
                              .Where(t => t.ID == id)
                              .Select(t => new BlackListInfo
                              {
                                  ID = t.ID,
                                  TypeMember = t.TypeMember,
                                  TelephoneNo = t.Telephone.TelephoneNo.ToString(),
                                  CustomerNationalCode = t.Customer.NationalCodeOrRecordNo,
                                  AddressPostalCode = t.Address.PostalCode,
                                  ReasonID = t.ReasonID,
                                  ArrestReference	 = t.ArrestReference,
                                  ArrestLetterNo	 = t.ArrestLetterNo,
                                  ArrestLetterNoDate	= t.ArrestLetterNoDate
                              })
                              .SingleOrDefault();
            }
        }

        public static BlackList GetBlackListEntityById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists
                              .Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<BlackListAddressInfo> SearchBlackListAddresses(
            List<int> centerIDs,
            string postalCode,
            string address,
            byte type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists
                    .Where(t => (type == t.TypeMember) &&
                                ((centerIDs.Count == 0) || (centerIDs.Contains(t.Address.CenterID))) &&
                                (string.IsNullOrWhiteSpace(postalCode) || t.Address.PostalCode.Contains(postalCode)) &&
                                (string.IsNullOrWhiteSpace(address) || t.Address.AddressContent.Contains(address)))
                    .OrderBy(t => t.Address.PostalCode)
                    .Select(t => new BlackListAddressInfo
                    {
                        ID = t.ID,
                        Reason = t.BlackListReason.ReasonContent,
                        Center = t.Address.Center.Region.City.Name + " : " + t.Address.Center.CenterName,
                        PostalCode = t.Address.PostalCode,
                        Address = t.Address.AddressContent,
                        ArrestLetterNo = t.ArrestLetterNo,
                        ArrestLetterNoDate = t.ArrestLetterNoDate,
                        ArrestReference = t.ArrestReference,
                        CreatorUser = t.User.FirstName + t.User.LastName,
                        ExitUser = t.User1.FirstName + t.User1.LastName,
                    }).ToList();
            }
        }

        public static List<BlackListCustomerInfo> SearchBlackListCustomer(
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
            string email,
            byte type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists
                    .Where(t => (type == t.TypeMember) &&
                                (personType.Count == 0 || personType.Contains((int)t.Customer.PersonType)) &&
                                (string.IsNullOrEmpty(nationalCodeOrRecordNo) || t.Customer.NationalCodeOrRecordNo.Contains(nationalCodeOrRecordNo)) &&
                                (string.IsNullOrEmpty(firstNameOrTitle) || t.Customer.FirstNameOrTitle.Contains(firstNameOrTitle)) &&
                                (string.IsNullOrEmpty(lastName) || t.Customer.LastName.Contains(lastName)) &&
                                (string.IsNullOrEmpty(fatherName) || t.Customer.FatherName.Contains(fatherName)) &&
                                (gender.Count == 0 || gender.Contains((int)t.Customer.Gender)) &&
                                (string.IsNullOrEmpty(birthCertificateID) || t.Customer.BirthCertificateID.Contains(birthCertificateID)) &&
                                (!birthDateOrRecord.HasValue || t.Customer.BirthDateOrRecordDate == birthDateOrRecord) &&
                                (string.IsNullOrEmpty(issuePlace) || t.Customer.IssuePlace.Contains(issuePlace)) &&
                                (string.IsNullOrEmpty(urgentTelNo) || t.Customer.UrgentTelNo.Contains(urgentTelNo)) &&
                                (string.IsNullOrEmpty(mobileNo) || t.Customer.MobileNo.Contains(mobileNo)) &&
                                (string.IsNullOrEmpty(email) || t.Customer.Email.Contains(email)))
                    .OrderBy(t => t.Customer.ID)
                    .Select(t => new BlackListCustomerInfo
                    {
                        ID = t.ID,
                        Reason = t.BlackListReason.ReasonContent,
                        PersonType = DB.GetEnumDescriptionByValue(typeof(DB.PersonType), t.Customer.PersonType),
                        NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                        FirstNameOrTitle = t.Customer.FirstNameOrTitle,
                        LastName = t.Customer.LastName,
                        FatherName = t.Customer.FatherName,
                        Gender = DB.GetEnumDescriptionByValue(typeof(DB.Gender), t.Customer.Gender),
                        BirthCertificateID = t.Customer.BirthCertificateID,
                        BirthDateOrRecordDate = Date.GetPersianDate(t.Customer.BirthDateOrRecordDate, Date.DateStringType.Short),
                        IssuePlace = t.Customer.IssuePlace,
                        UrgentTelNo = t.Customer.UrgentTelNo,
                        MobileNo = t.Customer.MobileNo,
                        Email = t.Customer.Email,
                        ArrestLetterNo = t.ArrestLetterNo,
                        ArrestLetterNoDate = t.ArrestLetterNoDate,
                        ArrestReference = t.ArrestReference,
                        CreatorUser = t.User.FirstName + t.User.LastName,
                        ExitUser = t.User1.FirstName + t.User1.LastName,
                    })
                    .ToList();
            }
        }

        public static List<BlackListTelephoneInfo> SearchBlackListTelephone(
            long telephoneNo,
            List<int> status,
            List<int> switchPrecode,
            List<int> switchPort,
            List<int> center,
            bool? isVIP,
            bool? isRound,
            byte type)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists
                    .Where(t => (type == t.TypeMember) &&
                                (status.Count == 0 || status.Contains(t.Telephone.Status)) &&
                                (switchPrecode.Count == 0 || switchPrecode.Contains((int)t.Telephone.SwitchPrecodeID)) &&
                                (switchPort.Count == 0 || switchPort.Contains((int)t.Telephone.SwitchPortID)) &&
                                (center.Count == 0 || center.Contains(t.Telephone.CenterID)) &&
                                (!isVIP.HasValue || t.Telephone.IsVIP == isVIP) &&
                                (!isRound.HasValue || t.Telephone.IsRound == isRound) &&
                                (telephoneNo == -1 || t.TelephoneNo == telephoneNo))
                    //.OrderBy(t => t.Name)
                    .Select(t => new BlackListTelephoneInfo
                    {
                        ID = t.ID,
                        Reason = t.BlackListReason.ReasonContent,
                        TelephoneNo = t.Telephone.TelephoneNo,
                        Center = t.Telephone.Center.Region.City.Name + " : " + t.Telephone.Center.CenterName,
                        Customer = (t.Telephone.Customer.FirstNameOrTitle ?? "" ) + " " + ( t.Telephone.Customer.LastName ?? "" ),
                        Status = DB.GetEnumDescriptionByValue(typeof(DB.TelephoneStatus), t.Telephone.Status),
                        SwitchPort = t.Telephone.SwitchPort.PortNo,
                        SwitchPrecode = t.Telephone.SwitchPrecode.SwitchPreNo.ToString(),
                        IsVIP = t.Telephone.IsVIP,
                        IsRound = t.Telephone.IsRound,
                        Address = t.Address.AddressContent,
                        ArrestLetterNo = t.ArrestLetterNo,
                        ArrestLetterNoDate = t.ArrestLetterNoDate,
                        ArrestReference = t.ArrestReference,
                        CreatorUser = t.User.FirstName + t.User.LastName,
                        ExitUser = t.User1.FirstName + t.User1.LastName,
                    })
                    .ToList();
            }
        }

        public static void SaveBlackList(BlackList blackList, byte type)
        {
            switch (type)
            {
                case (byte)DB.BlackListType.Address:
                    BlackList itemAddress = DB.SearchByPropertyName<BlackList>("AddressID", blackList.AddressID).SingleOrDefault();
                    if (itemAddress != null)
                        if (itemAddress.Status)
                            return;
                        else
                        {
                            itemAddress.Status = true;
                            itemAddress.Detach();
                            DB.Save(itemAddress);
                            return;
                        }
                    break;

                case (byte)DB.BlackListType.Customer:
                    BlackList itemCustomer = DB.SearchByPropertyName<BlackList>("CustomerID", blackList.CustomerID).SingleOrDefault();
                    if (itemCustomer != null)
                        if (itemCustomer.Status)
                            return;
                        else
                        {
                            itemCustomer.Status = true;
                            itemCustomer.Detach();
                            DB.Save(itemCustomer);
                            return;
                        }
                    break;

                case (byte)DB.BlackListType.TelephoneNo:
                    BlackList itemTelephone = DB.SearchByPropertyName<BlackList>("TelephoneNo", blackList.TelephoneNo).SingleOrDefault();
                    if (itemTelephone != null)
                        if (itemTelephone.Status)
                            return;
                        else
                        {
                            itemTelephone.Status = true;
                            itemTelephone.Detach();
                            DB.Save(itemTelephone);
                            return;
                        }
                    break;

                default:
                    break;
            }

            blackList.Status = true;
            blackList.Detach();
            DB.Save(blackList);
        }

        public static bool ExistNationalCodeInBlackList(string nationalCode)
        {
            using(MainDataContext context = new MainDataContext())
            {
                List<Customer> customers = context.Customers.Where(t => t.NationalCodeOrRecordNo == nationalCode).ToList();
                return context.BlackLists.Any(t => t.TypeMember == (int)DB.BlackListType.Customer && t.Status == true && customers.Select(t2 => t2.ID).Contains((long)t.CustomerID));
            }
        }

        public static bool ExistPostallCodeInBlackList(string postallCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Address> address = context.Addresses.Where(t => t.PostalCode == postallCode).ToList();
                return context.BlackLists.Any(t => t.TypeMember == (int)DB.BlackListType.Address && t.Status == true && address.Select(t2 => t2.ID).Contains((long)t.AddressID));
            }
        }

        public static bool ExistTelephoneNoInBlackList(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BlackLists.Any(t => t.TypeMember == (int)DB.BlackListType.TelephoneNo && t.Status == true && telephoneNo == (long)t.TelephoneNo);
            }
        }
    }
}
