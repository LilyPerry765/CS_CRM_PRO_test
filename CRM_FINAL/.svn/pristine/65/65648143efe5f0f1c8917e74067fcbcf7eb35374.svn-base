using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class RoundListDB
    {
        public static List<TelRoundSale> FindRoundNumber(long telNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales
                    .Where(t => (telNo == 0 || telNo == t.TelephoneNo))
                    .ToList();
            }
        }

        public static List<RoundListNo> GetRoundListByCenterID(long centerID, byte? roundType)//,DateTime saleDate
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales
                    .Where(t => t.Telephone.CenterID == centerID)
                    .Select(t => new RoundListNo
                    {
                        TelephoneNo = t.TelephoneNo,
                        RoundID = t.ID,
                        IsRound = t.Telephone.IsRound,
                        CenterID = t.Telephone.CenterID,
                        telStatus = t.Telephone.Status,
                        RoundType = t.Telephone.RoundType,
                        EntryDate = t.RoundSaleInfo.EntryDate,
                        StartDate = t.RoundSaleInfo.StartDate,
                        EndDate = t.RoundSaleInfo.EndDate,
                        IsSelectable = t.RoundSaleInfo.IsActive,
                        saleStatus = t.SaleStatus,
                        RoundSaleInfoID = t.RoundSaleInfoID,
                    }

                   ).Distinct().ToList();
            }

        }

        public static TelRoundInfo GetRoundTelInfoByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contracts.Where(t => t.RequestID == requestID && t.TelRoundSaleID != null).Select(t => new TelRoundInfo
                {
                    switchID = t.TelRoundSale.Telephone.SwitchPrecode.SwitchID,
                    switchPreCodeID = t.TelRoundSale.Telephone.SwitchPrecodeID,
                    TelephoneNo = t.TelRoundSale.TelephoneNo,
                    portNo = t.TelRoundSale.Telephone.SwitchPort.PortNo,
                    PorID = t.TelRoundSale.Telephone.SwitchPort.ID,
                    portType = t.TelRoundSale.Telephone.SwitchPort.Type,
                    switchPreNo = t.TelRoundSale.Telephone.SwitchPrecode.SwitchPreNo,
                    switchPreCodeType = t.TelRoundSale.Telephone.SwitchPrecode.PreCodeType,
                }
                                                                                     ).SingleOrDefault();
            }
        }

        public static RoundSaleInfo GetRoundById(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoundSaleInfos
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<RoundSaleInfo> SearchRound(sbyte status, DateTime? entryDate, DateTime? startDate, DateTime? endDate, bool? isSelectable, long telephone, long insertUserId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.RoundSaleInfos
                    .Where(t =>
                        //(status == -1 || t.Status == status) &&
                        (!entryDate.HasValue || t.EntryDate == entryDate) &&
                        (!startDate.HasValue || t.StartDate == startDate) &&
                        (!endDate.HasValue || t.EndDate == endDate) &&
                        (!isSelectable.HasValue || t.IsActive == isSelectable) &&
                        (telephone == -1 || t.TelRoundSales.Select(s => s.TelephoneNo).SingleOrDefault() == telephone) &&
                        (insertUserId == -1 || t.InsertUserID == insertUserId)).OrderBy(t => t.ID)
                        .ToList();
            }

        }



        public static List<RoundListNo> GetRoundTelInfo(long centerID, byte roundType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelRoundSales
                    .Where(t => t.Telephone.CenterID == centerID &&
                                 t.Telephone.IsRound== true &&
                                t.RoundSaleInfo.IsActive == true && 
                                t.SaleStatus ==  0 &&
                                t.Telephone.Status == (byte)DB.TelephoneStatus.Free &&
                                t.Telephone.RoundType == roundType )
                    .Select(t => new RoundListNo
                    {
                        TelephoneNo = t.TelephoneNo,
                        RoundID = t.ID,
                        IsRound = t.Telephone.IsRound,
                        CenterID = t.Telephone.CenterID,
                        telStatus = t.Telephone.Status,
                        RoundType = t.Telephone.RoundType,
                        EntryDate = t.RoundSaleInfo.EntryDate,
                        StartDate = t.RoundSaleInfo.StartDate,
                        EndDate = t.RoundSaleInfo.EndDate,
                        IsSelectable = t.RoundSaleInfo.IsActive,
                        saleStatus = t.SaleStatus,
                        RoundSaleInfoID = t.RoundSaleInfoID,
                    }

                   ).Distinct().ToList();
            }
        }
    }

    #region Custom Class
    public class RoundListNo
    {

        public long RoundID { get; set; }
        public long TelephoneNo { get; set; }
        public bool? IsRound { get; set; }
        public int CenterID { get; set; }
        public byte telStatus { get; set; }
        public byte? RoundType { get; set; }
        public DateTime? EntryDate { get; set; }
        public bool? IsSelectable { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte? saleStatus { get; set; }
        public long RoundSaleInfoID { get; set; }


    }
    public class TelRoundInfo
    {
        public long TelephoneNo { get; set; }
        public long switchPreNo { get; set; }
        public long? PorID { get; set; }
        public long? switchPreCodeID { get; set; }
        public string portNo { get; set; }
        public byte switchPreCodeType { get; set; }
        public int? switchID { get; set; }
        public bool? portType { get; set; }
    }



    #endregion Custom Class
}


//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;

//////namespace CRM.Data
//////{
//////    public static class  RoundListDB
//////    {
//////        public static List<TelRoundSale> FindRoundNumber(long telNo)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.TelRoundSales
//////                    .Where(t => (telNo == 0 || telNo == t.TelephoneNo))
//////                    .ToList();
//////            }
//////        }
        
//////        public static List<RoundListNo> GetRoundListByCenterID(long centerID, byte? roundType)//,DateTime saleDate
//////          {
//////              using (MainDataContext context = new MainDataContext())
//////              {
//////                  return context.TelRoundSales
//////                      .Where(t => t.Telephone.CenterID == centerID                     
//////                              // && t.SaleStatus==0 && t.IsActive==true
//////                            )
//////                      .Select(t => new RoundListNo
//////                      {
//////                          TelephoneNo = t.TelephoneNo,
//////                          RoundID=t.ID,                          
//////                          IsRound = t.Telephone.IsRound,
//////                          CenterID = t.Telephone.CenterID,
//////                          telStatus = t.Telephone.Status,
//////                          RoundType = t.Telephone.RoundType,
//////                          EntryDate = t.RoundSaleInfo.EntryDate,
//////                          StartDate = t.RoundSaleInfo.StartDate,
//////                          EndDate = t.RoundSaleInfo.EndDate,
//////                          IsSelectable = t.RoundSaleInfo.IsActive,
//////                          saleStatus = t.SaleStatus,
//////                          RoundSaleInfoID=t.RoundSaleInfoID,
//////                      }

//////                     ).Distinct().ToList();
//////              }

//////          }

//////        public static TelRoundInfo GetRoundTelInfoByRequestID(long requestID)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Contracts.Where(t => t.RequestID == requestID).Select(t => new TelRoundInfo {
//////                                                                                                            switchID=t.TelRoundSale.Telephone.SwitchPort.SwitchID,
//////                                                                                                            TelephoneNo = t.TelRoundSale.TelephoneNo,
//////                                                                                                            portNo = t.TelRoundSale.Telephone.SwitchPort.PortNo,
//////                                                                                                            portType = t.TelRoundSale.Telephone.SwitchPort.Type,
//////                                                                                                            switchPreNo = t.TelRoundSale.Telephone.SwitchPort.Switch.SwitchPreNo,
//////                                                                                                            switchPreCodeType = t.TelRoundSale.Telephone.SwitchPort.Switch.PreCodeType,
//////                                                                                                           }
//////                                                                                     ).SingleOrDefault();
//////            }
//////        }

//////        public static RoundSaleInfo GetRoundById(long id)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.RoundSaleInfos
//////                    .Where(t => t.ID == id)
//////                    .SingleOrDefault();
//////            }
//////        }

//////        public static List<RoundSaleInfo> SearchRound(byte status, DateTime? entryDate, DateTime? startDate, DateTime? endDate, bool? isSelectable, long telephone, long insertUserId)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.RoundSaleInfos
//////                    .Where(t =>
//////                        //(status == -1 || t.Status == status) &&
//////                        (!entryDate.HasValue || t.EntryDate == entryDate) &&
//////                        (!startDate.HasValue || t.StartDate == startDate) &&
//////                        (!endDate.HasValue || t.EndDate == endDate) &&
//////                        (!isSelectable.HasValue || t.IsActive == isSelectable) &&
//////                        (telephone == -1 || t.TelRoundSales.Select(s=>s.TelephoneNo).SingleOrDefault()==telephone ) &&
//////                        (insertUserId == -1 || t.InsertUserID == insertUserId)).OrderBy(t => t.ID)
//////                        .ToList();
//////            }

//////        }

      
//////    }

//////    #region Custom Class
//////    public class RoundListNo
//////    {

//////        public long RoundID { get; set; }
//////        public long TelephoneNo {get;set;}
//////        public bool? IsRound {get;set;}
//////        public int CenterID {get;set;}
//////        public byte telStatus {get;set;}
//////        public byte? RoundType {get;set;}
//////        public DateTime? EntryDate {get;set;}
//////        public bool? IsSelectable {get;set;}
//////        public DateTime? StartDate {get;set;}
//////        public DateTime? EndDate {get;set;}
//////        public byte? saleStatus {get;set;}
//////        public long RoundSaleInfoID { get; set; }

        
//////    }
//////    public class TelRoundInfo
//////    {
//////        public long TelephoneNo { get; set; }
//////        public long? switchPreNo { get; set; }
//////        public string portNo { get; set; }        
//////        public byte? switchPreCodeType { get; set; }
//////        public int? switchID { get; set; }
//////        public bool? portType { get; set; }
//////    }


   
//////    #endregion Custom Class
//////}
