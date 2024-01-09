using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class VisitAddressDB
    {
        public static List<CheckableItem> GetVisitAddressCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VisitAddresses.AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.VisitHour + " " + Date.GetPersianDate(t.VisitDate, Date.DateStringType.Short).ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetVisitAddressCheckable(long addressID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VisitAddresses.Where(t => t.AddressID == addressID).AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.VisitHour + " " + Date.GetPersianDate(t.VisitDate, Date.DateStringType.Short).ToString(), IsChecked = false }).ToList();
            }
        }

        //public static List<VisitAddress> GetVisitAddressesByInvestigatePossibility(long InvestigatePossibilityID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VisitAddresses.Where(t => t.InvestigatePossibilityID == InvestigatePossibilityID).ToList();
        //    }
        //}

        //milad doran
        //public static List<VisitPlacesCabinetAndPostClass> GetVisitAddressCabinetAndPost(long addressID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VisitPlacesCabinetAndPosts
        //               .Where(t => t.VisitAddress.AddressID == addressID)
        //               .Select(t => new VisitPlacesCabinetAndPostClass
        //                               {
        //                                   ID = t.ID,
        //                                   CabinetID = t.CabinetID,
        //                                   CabinetNumber = t.Cabinet.CabinetNumber.ToString(),
        //                                   PostID = t.PostID,
        //                                   PostNumber = t.Post.Number.ToString(),
        //                                   ProposedFacilityType = (int)DB.ProposedFacilityType.VisitPlaces,
        //                                   Center = t.Cabinet.Center.CenterName,
        //                                   VisitAddressID = t.VisitAddressID
        //                               }).ToList();
        //    }
        //}

        //TODO:rad 13950226
        public static List<VisitPlacesCabinetAndPostClass> GetVisitAddressCabinetAndPost(long addressID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<VisitPlacesCabinetAndPostClass> result = new List<VisitPlacesCabinetAndPostClass>();
                var query = context.VisitPlacesCabinetAndPosts
                                   .Where(t => t.VisitAddress.AddressID == addressID)
                                   .Select(t => new VisitPlacesCabinetAndPostClass
                                                {
                                                    ID = t.ID,
                                                    CabinetID = t.CabinetID,
                                                    CabinetNumber = t.Cabinet.CabinetNumber.ToString(),
                                                    PostID = t.PostID,
                                                    PostNumber = t.Post.Number.ToString(),
                                                    ProposedFacilityType = (int)DB.ProposedFacilityType.VisitPlaces,
                                                    Center = t.Cabinet.Center.CenterName,
                                                    VisitAddressID = t.VisitAddressID
                                                }
                                          )
                                   .AsQueryable();

                result = query.ToList();
                return result;
            }
        }

        public static List<VisitAddress> GetVisitAddressByRequestID(long _requestID, long addressID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VisitAddresses.Where(t => t.RequestID == _requestID && t.AddressID == addressID).ToList();
            }
        }


        public static int? GetOutBoundMeterByRequestID(long requestID, long addressID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.VisitAddresses.Any(t => t.RequestID == requestID && t.AddressID == addressID && t.ConfirmInvestigatePossibility == true))
                {
                    return context.VisitAddresses.Where(t => t.RequestID == requestID && t.AddressID == addressID && t.ConfirmInvestigatePossibility == true).OrderByDescending(t => t.VisitDate).FirstOrDefault().OutBoundMeter;
                }
                else
                {
                    return null;
                }

            }
        }

        public static long? GetCableMeterByRequestID(long requestID, long addressID)
        {
            using (MainDataContext context = new MainDataContext())
            {

                if (context.Requests.Where(t => t.ID == requestID).SingleOrDefault().RequestTypeID == (int)DB.RequestType.SpecialWire)
                {
                    IQueryable<Request> reqeust = context.Requests.Where(t => t.ID == requestID || t.MainRequestID == requestID);
                    return context.VisitAddresses.Where(t => reqeust.Select(t2 => t2.ID).ToList().Contains(t.RequestID)).Sum(t => t.CableMeter);
                }
                else
                {
                    if (context.VisitAddresses.Any(t => t.RequestID == requestID && t.AddressID == addressID))
                    {
                        return context.VisitAddresses.Where(t => t.RequestID == requestID && t.AddressID == addressID).OrderByDescending(t => t.VisitDate).FirstOrDefault().CableMeter;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
        }

        public static VisitAddress GetLastVisitAddressByTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                VisitAddress result = context.Telephones
                                             .GroupJoin(context.VisitAddresses, te => te.InstallAddressID, va => va.AddressID, (te, vas) => new { _Telephone = te, VisitAddresses = vas })
                                             .SelectMany(a => a.VisitAddresses.DefaultIfEmpty(), (a, va) => new { _Telephone = a._Telephone, _VisitAddress = va })
                                             .Where(a=>a._Telephone.TelephoneNo==telephoneNo)
                                             .OrderByDescending(a => a._VisitAddress.ID)
                                             .Select(a => a._VisitAddress)
                                             .Take(1)
                                             .SingleOrDefault();
                return result;
            }
        }
    }
}
