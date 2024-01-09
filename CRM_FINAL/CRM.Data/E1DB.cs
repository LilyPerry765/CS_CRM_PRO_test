using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1DB
    {
        public static CRM.Data.E1 GetE1ByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1s.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static List<E1Info> GetE1RequestsByCustomerID(long customerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<E1Info> result = new List<E1Info>();
                var firstQuery = context.E1s
                                        .GroupJoin(context.Customers, e1 => e1.Request.CustomerID, cu => cu.ID, (e1, cus) => new { _E1 = e1, _Customers = cus }) //Left join with Customer
                                        .SelectMany(a => a._Customers.DefaultIfEmpty(), (a, cu) => new { _E1 = a._E1, _Customer = cu })

                                        .GroupJoin(context.CustomerTypes, a => a._E1.TelephoneType, ct => ct.ID, (a, cts) => new { _E1 = a._E1, _Customer = a._Customer, TelephoneTypes = cts }) //Left join with CustomerType
                                        .SelectMany(a => a.TelephoneTypes.DefaultIfEmpty(), (a, ct) => new { _E1 = a._E1, _Customer = a._Customer, TelephoneType = ct })

                                        .GroupJoin(context.CustomerGroups, a => a._E1.TelephoneTypeGroup, cg => cg.ID, (a, cgs) => new { _E1 = a._E1, _Customer = a._Customer, TelephoneType = a.TelephoneType, TelephoneGroups = cgs }) //Left join with CustomerGroup
                                        .SelectMany(a => a.TelephoneGroups.DefaultIfEmpty(), (a, cg) => new { _E1 = a._E1, _Customer = a._Customer, TelephoneType = a.TelephoneType, TelephoneGroup = cg })

                                        .Where(a =>
                                                    (a._E1.Request.EndDate.HasValue) &&
                                                    (a._E1.Request.CustomerID == customerID)
                                              )

                                        .OrderByDescending(a => a._E1.Request.EndDate)

                                        .AsQueryable();
                result = firstQuery.Select(t => new E1Info
                                                    {
                                                        CityName = t._E1.Request.Center.Region.City.Name,
                                                        CenterName = t._E1.Request.Center.CenterName,
                                                        CenterID = t._E1.Request.CenterID,
                                                        ConnectionNo = t._E1.ConnectionNo,
                                                        LineType = Helpers.GetEnumDescription(t._E1.LineType, typeof(DB.E1Type)),
                                                        LinkTypeName = (t._E1.LinkTypeID.HasValue) ? t._E1.E1LinkType.Name : string.Empty,
                                                        RequestDate = t._E1.Request.EndDate.ToPersian(Date.DateStringType.Short),
                                                        RequestID = t._E1.RequestID,
                                                        TelephoneGroupTitle = t.TelephoneGroup.Title,
                                                        TelephoneTypeTitle = t.TelephoneType.Title
                                                    }
                                          )
                                   .ToList();
                return result;
            }
        }

        public static E1NumberInfo GetDDFPortByE1Number(int e1Number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1Numbers.Where(t => t.ID == e1Number)
                              .Select(t => new E1NumberInfo
                              {

                                  NumberID = t.ID,
                                  Number = t.Number,
                                  PositionID = t.PositionID,
                                  PositionNumber = t.E1Position.Number,
                                  BayID = t.E1Position.BayID,
                                  BayNumber = t.E1Position.E1Bay.Number,
                                  DDFID = t.E1Position.E1Bay.DDFID,
                                  DDFNumber = t.E1Position.E1Bay.E1DDF.Number,

                              }).SingleOrDefault();
            }
        }

        public static bool CheckTelephoneBeE1(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().UsageType == (byte)DB.TelephoneUsageType.E1;
            }
        }

        public static bool CheckTelephoneBeConnectedE1(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().Status == (byte)DB.TelephoneStatus.Connecting;
            }
        }


        public static int GetNumberOfLink(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<Telephone> telephone = context.Telephones.Where(t => t.TelephoneNo == telephoneNo);
                return context.Buchts.Where(t => t.SwitchPortID == telephone.SingleOrDefault().SwitchPortID).Count();
            }
        }

        public static E1 GetLastInstallE1ByTelephone(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1s.Where(t => t.Request.TelephoneNo == telephone && t.Request.RequestTypeID == (byte)DB.RequestType.E1).OrderByDescending(t => t.Request.EndDate).FirstOrDefault();
            }
        }

        public static int GetE1Count(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.Telephones.Where(t => t.TelephoneNo == telephone)
                                         .Join(context.Buchts, t => t.SwitchPortID, t2 => t2.SwitchPortID, (t, t2) => new { telephone = t, otherBucht = t2 }).Count();
            }

        }

        public static CRM.Data.E1 GetLastE1RequestByTelephone(long telephonNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1s.Where(r => r.Request.TelephoneNo == telephonNo).OrderByDescending(r => r.Request.EndDate).FirstOrDefault();
            }
        }
    }
}
