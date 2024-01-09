using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialWirePointsDB
    {
        public static List<SpecialWirePoints> GetSpecialWirePointsByRequestID(long requestID)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.SpecialWirePoints.Where(t => t.RequestID == requestID)
                             .Select(t => new SpecialWirePoints 
                                           { 
                                               ID = t.ID,
                                               CenterID = t.CenterID,
                                               InstallAddressID = t.InstallAddressID,
                                               AddressContent = t.AddressContent,
                                               NearestTelephoneNo = t.NearestTelephoneNo,
                                               PostalCode = t.PostalCode ,
                                                SpecialWireTypeID  = t.SpecialWireType
                                           }).ToList();
           }
       }

        public static List<SpecialWirePoints> GetSpecialWirePointsByTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                //IQueryable<SpecialWirePoints>  temp = context.Telephones
                //              .Where(t=>t.TelephoneNo == telephoneNo)
                //              .Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
                //              .GroupJoin(context.Buchts , b=>b.Bucht.BuchtIDConnectedOtherBucht , ob => ob.ID , (b ,ob) => new { Telephone = b.Telephone , OtherBucht = ob , Bucht = b})
                //              .SelectMany(t => t.OtherBucht.DefaultIfEmpty(), (b, ob) => new { Telephone = b.Telephone, OtherBucht = ob, Bucht = b })
                //              .GroupJoin(context.SpecialWireAddresses, si => si.Bucht.Bucht.Bucht.ID, swa => swa.BuchtID, (si, swa) => new { Telephone = si.Telephone, OtherBucht = si.OtherBucht, Bucht = si.Bucht, SpecialWireAddress = swa })
                //              .SelectMany(t2 => t2.SpecialWireAddress.DefaultIfEmpty(), (t2, od) => new { Telephone = t2.Telephone, OtherBucht = t2.OtherBucht, Bucht = t2.Bucht, SpecialWireAddress = od })
                //       .Select(t =>
                //              new SpecialWirePoints
                //              {
                //                  CustomerName = t.Telephone.Customer.FirstNameOrTitle ?? "" + " " + t.Telephone.Customer.LastName ?? "",
                //                  CenterID = t.Bucht.Bucht.Bucht.CabinetInput.Cabinet.CenterID,
                //                  PostalCode =   context.Addresses.Where(t2=>t2.ID == t.SpecialWireAddress.InstallAddressID).SingleOrDefault().PostalCode,
                //                  AddressContent = context.Addresses.Where(t2 => t2.ID == t.SpecialWireAddress.InstallAddressID).SingleOrDefault().AddressContent,
                //                  InstallAddressID = context.Addresses.Where(t2 => t2.ID == t.SpecialWireAddress.InstallAddressID).SingleOrDefault().ID,
                //                  CorrespondenceAddressID = context.Addresses.Where(t2 => t2.ID == t.SpecialWireAddress.CorrespondenceAddressID).SingleOrDefault().ID,
                //                  BuchtID = t.Bucht.Bucht.Bucht.ID,
                //                  OtherBuchtID = t.OtherBucht.ID,
                //                  CabinetInputID = t.Bucht.Bucht.Bucht.CabinetInputID,
                //                  PostContactID =t.Bucht.Bucht.Bucht.ConnectionID,
                //                  SwitchPortID = t.Telephone.SwitchPortID,
                //                  SpecialWireTypeID = (int)DB.SpecialWireType.General,
                //                  IsSelect = false,
                //              });

                       IQueryable<SpecialWirePoints> temp = context.SpecialWireAddresses.Where(t => t.TelephoneNo == telephoneNo)
                                                     
                              .Select(t =>
                              new SpecialWirePoints
                              {
                                  CustomerName = t.Telephone.Customer.FirstNameOrTitle ?? "" + " " + t.Telephone.Customer.LastName ?? "",
                                  CenterID = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                                  PostalCode = context.Addresses.Where(t2 => t2.ID == t.InstallAddressID).SingleOrDefault().PostalCode,
                                  AddressContent = context.Addresses.Where(t2 => t2.ID == t.InstallAddressID).SingleOrDefault().AddressContent,
                                  InstallAddressID = context.Addresses.Where(t2 => t2.ID == t.InstallAddressID).SingleOrDefault().ID,
                                  CorrespondenceAddressID = context.Addresses.Where(t2 => t2.ID == t.CorrespondenceAddressID).SingleOrDefault().ID,
                                  BuchtID = t.Bucht.ID,
                                  OtherBuchtID = t.Bucht1.ID,
                                  CabinetInputID = t.Bucht.CabinetInputID,
                                  PostContactID = t.Bucht.ConnectionID,
                                  SwitchPortID = t.Telephone.SwitchPortID,
                                  SpecialWireTypeID = (int)t.SpecialTypeID,
                                  IsSelect = false,
                              });

                return temp.ToList();
            }
        }

        public static List<SpecialWirePoint> GetSpecialWirePointByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialWirePoints.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
