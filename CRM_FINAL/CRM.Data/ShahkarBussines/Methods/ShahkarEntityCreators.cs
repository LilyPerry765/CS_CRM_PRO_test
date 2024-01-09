using CRM.WebAPI.Models.Shahkar.CustomClasses;
using CRM.WebAPI.Models.Shahkar.Methods;
using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.ShahkarBussines.Methods
{
    public static class ShahkarEntityCreators
    {
        /// <summary>
        /// .این متد دیتای موردنیاز برای احراز هویت مشترک حقیقی ایرانی در سامانه شاهکار را ایجاد می نماید
        /// </summary>
        /// <param name="customer">مشترکی که آبجکت شاهکاری آن باید ساخته شود</param>
        /// <returns></returns>
        public static IranianAuthentication CreateIranianAuthenticationFromPersonCustomer(Customer customer)
        {
            IranianAuthentication iranianAuthentication = new IranianAuthentication();

            try
            {
                iranianAuthentication.name = (!string.IsNullOrEmpty(customer.FirstNameOrTitle)) ? customer.FirstNameOrTitle.Trim() : "";
                iranianAuthentication.family = (!string.IsNullOrEmpty(customer.LastName)) ? customer.LastName.Trim() : "";
                iranianAuthentication.fatherName = (!string.IsNullOrEmpty(customer.FatherName)) ? customer.FatherName.Trim() : "";
                iranianAuthentication.birthDate = (customer.BirthDateOrRecordDate.HasValue) ? customer.BirthDateOrRecordDate.Value.ToPersian(Date.DateStringType.Short).Replace("/", "") : "";
                iranianAuthentication.certificateNo = !(string.IsNullOrEmpty(customer.BirthCertificateID)) ? customer.BirthCertificateID : "0";
                iranianAuthentication.identificationNo = (!string.IsNullOrEmpty(customer.NationalCodeOrRecordNo)) ? customer.NationalCodeOrRecordNo.Trim() : "";
                iranianAuthentication.identificationType = 0;

                return iranianAuthentication;
            }
            catch (Exception ex)
            {
                iranianAuthentication = null;
                Logger.WriteError("Following exception caught in {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.WriteException(ex.Message);
            }

            return iranianAuthentication;
        }

        /// <summary>
        /// .این متد دیتای مورد نیاز برای ایجاد سرویس تلفن ثابت افراد حقیقی ایرانی در سامانه شاهکار را ایجاد میکند  
        /// </summary>
        /// <param name="telephoneNo">تلفنی که در سیستم ما در حال دایر شدن است و باید به شاهکار اطلاع داده شود</param>
        /// <returns></returns>
        public static IranianPersonCustomerInstallTelephone CreateInstallTelephoneFromTelephone(long telephoneNo)
        {
            IranianPersonCustomerInstallTelephone result = new IranianPersonCustomerInstallTelephone();
            try
            {
                using (MainDataContext context = new MainDataContext())
                {
                    DateTime? birthDate = null;
                    string certificateNo = string.Empty;

                    var query = context.Telephones
                                       .Join(context.Customers, tel => tel.CustomerID, cus => cus.ID, (tel, cus) => new { _Telephone = tel, _Customer = cus })
                                       .Join(context.Centers, fromTop => fromTop._Telephone.CenterID, cen => cen.ID, (fromTop, cen) => new
                                                                                                                                       {
                                                                                                                                           _Telephone = fromTop._Telephone,
                                                                                                                                           _Customer = fromTop._Customer,
                                                                                                                                           _Center = cen
                                                                                                                                       }
                                             )
                                       .Join(context.Buchts, fromTop => fromTop._Telephone.SwitchPortID, bu => bu.SwitchPortID, (fromTop, bu) => new
                                                                                                                                                 {
                                                                                                                                                     _Telephone = fromTop._Telephone,
                                                                                                                                                     _Customer = fromTop._Customer,
                                                                                                                                                     _Center = fromTop._Center,
                                                                                                                                                     _Bucht = bu
                                                                                                                                                 }
                                            )
                                       .Where(joinedData => joinedData._Telephone.TelephoneNo == telephoneNo)
                                       .AsQueryable();

                    result = query.Select(filterData => new IranianPersonCustomerInstallTelephone
                                                        {
                                                            name = filterData._Customer.FirstNameOrTitle.Trim(),
                                                            family = filterData._Customer.LastName.Trim(),
                                                            fatherName = filterData._Customer.FatherName.Trim(),
                                                            identificationNo = filterData._Customer.NationalCodeOrRecordNo.Trim(),
                                                            gender = (filterData._Customer.Gender.HasValue && filterData._Customer.Gender.Value == 0) ? 1 //مرد
                                                                   : (filterData._Customer.Gender.HasValue && filterData._Customer.Gender.Value == 1) ? 2 //زن
                                                                   : 3, //ناشناخته,
                                                            birthPlace = "",
                                                            mobile = filterData._Customer.MobileNo.Trim(),
                                                            email = filterData._Customer.Email.Trim()
                                                        }
                                          )
                                  .FirstOrDefault();

                    birthDate = query.Select(a => a._Customer.BirthDateOrRecordDate).FirstOrDefault();
                    result.birthDate = (birthDate.HasValue) ? birthDate.ToPersian(Date.DateStringType.Short).Replace("/", "") : "";

                    certificateNo = query.Select(a => a._Customer.BirthCertificateID).FirstOrDefault();
                    result.certificateNo = !(string.IsNullOrEmpty(certificateNo)) ? certificateNo : "0";

                    result.service = query.Select(a => new CRM.WebAPI.Models.Shahkar.CustomClasses.ShahkarService
                                                       {
                                                           province = a._Center.Region.City.Province.Name.Trim(),
                                                           centerName = a._Center.CenterName.Trim(),
                                                           county = a._Center.Region.City.Name.Trim(),
                                                           kafu = a._Bucht.CabinetInput.Cabinet.CabinetNumber.ToString() + "-" + a._Bucht.CabinetInput.InputNumber.ToString(),
                                                           post = a._Bucht.PostContact.Post.Number.ToString(),
                                                           credit = (a._Telephone.PosessionType.HasValue && a._Telephone.PosessionType.Value == 2) ? 1 : 0,
                                                           general = a._Telephone.CustomerGroup.Title.Contains("همگان") ? 1 : 0,
                                                           phoneNumber = "0" + a._Telephone.TelephoneNo.ToString(),
                                                       }
                                                 )
                                          .FirstOrDefault();

                    result.address = query.GroupJoin(context.Addresses, a => a._Telephone.InstallAddressID, ia => ia.ID, (a, ias) => new { _FromTop = a, _Addresses = ias })
                                          .SelectMany(a => a._Addresses.DefaultIfEmpty(), (a, ia) => new { _Address = ia })
                                          .Select(a => new CRM.WebAPI.Models.Shahkar.CustomClasses.ShahkarAddress
                                                       {
                                                           address = a._Address.AddressContent.Trim(),
                                                           postalCode = a._Address.PostalCode.Trim()
                                                       }
                                                 )
                                          .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                result = null;
                Logger.WriteError("Following exception caught in {0}", System.Reflection.MethodBase.GetCurrentMethod().Name);
                Logger.Write(ex);
            }

            return result;
        }
    }
}
