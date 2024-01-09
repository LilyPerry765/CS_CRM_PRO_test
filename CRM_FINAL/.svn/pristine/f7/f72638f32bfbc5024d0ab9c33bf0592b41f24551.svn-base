using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public class ExchangeCenralCableCabinetDB
    {
        public static long GetExchangeCentralCableMDFByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == requestID).Select(t => t.ID).SingleOrDefault();
            }
        }

        public static ExchangeCabinetInput GetExchangeCabinetInput(long exchangeCabinetInput)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs.Where(t => t.ID == exchangeCabinetInput).SingleOrDefault();
            }
        }

        public class ExchangeCenralCableCabinetInfo
        {
             public long  ID { get; set; }
             public long  RequestID{get ; set ;} 
             public int   OldCabinetID{get ; set ;} 
             public int   NewCabinetID{get ; set ;}

             public string OldCabinetName { get; set; }
             public string NewCabinetName { get; set; } 

             public long?  FromOldCabinetInputID{get ; set ;} 
             public long?  ToOldCabinetInputID{get ; set ;}

             public string FromOldCabinetInputName { get; set; }
             public string ToOldCabinetInputName { get; set; } 

             public long?  FromNewCabinetInputID{get ; set ;} 
             public long?  ToNewCabinetInputID{get ; set ;}

             public string FromNewCabinetInputName { get; set; }
             public string ToNewCabinetInputName { get; set; } 

             public DateTime  InsertDate{get ; set ;} 
             public DateTime?  AccomplishmentDate{get ; set ;} 
             public string  AccomplishmentTime{get ; set ;}
             public string  StatusTitle { get; set; }
             public string requestLetterNo { get; set; }
        }

      

        public static int SearchExchangeCenralCableCabinetCount(
            List<int> city,
            List<int> center,
            List<int> oldCabinet,
            List<int> newCabinet,
            List<int> fromOldCabinetInput,
            List<int> toOldCabinetInput,
            List<int> fromNewCabinetInput,
            List<int> toNewCabinetInput,
             DateTime? accomplishmentDateDate,
            string requestLetterNo,
            string accomplishmentTime)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs
                    .Where(t =>
                    (city.Count == 0 || city.Contains(t.Cabinet.Center.Region.CityID)) &&
                    (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : center.Contains(t.Cabinet.CenterID)) &&
                    (oldCabinet.Count == 0 || oldCabinet.Contains(t.OldCabinetID)) &&
                    (newCabinet.Count == 0 || newCabinet.Contains(t.NewCabinetID)) &&
                    (fromOldCabinetInput.Count == 0 || fromOldCabinetInput.Contains((int)t.FromOldCabinetInputID)) &&
                    (toOldCabinetInput.Count == 0 || toOldCabinetInput.Contains((int)t.ToOldCabinetInputID)) &&
                    (fromNewCabinetInput.Count == 0 || fromNewCabinetInput.Contains((int)t.FromNewCabinetInputID)) &&
                    (!accomplishmentDateDate.HasValue || t.MDFAccomplishmentDate == accomplishmentDateDate) &&
                    (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo) &&
                    (string.IsNullOrEmpty(accomplishmentTime) || t.MDFAccomplishmentTime == accomplishmentTime)
                    ).Count();
            }
        }

        public static List<ExchangeCenralCableCabinetInfo> SearchExchangeCenralCableCabinet(
            List<int> city,
            List<int> center,
            List<int> oldCabinet,
            List<int> newCabinet,
            List<int> fromOldCabinetInput,
            List<int> toOldCabinetInput,
            List<int> fromNewCabinetInput,
            List<int> toNewCabinetInput,
             DateTime? accomplishmentDateDate,
            string requestLetterNo,
            string accomplishmentTime,
             int      startRowIndex,
             int pageSize
             )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangeCabinetInputs
                    .Where(t =>
                     (city.Count == 0 || city.Contains(t.Cabinet.Center.Region.CityID)) &&
                    (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : center.Contains(t.Cabinet.CenterID)) &&
                    (oldCabinet.Count == 0 || oldCabinet.Contains(t.OldCabinetID)) &&
                    (newCabinet.Count == 0 || newCabinet.Contains(t.NewCabinetID)) &&
                    (fromOldCabinetInput.Count == 0 || fromOldCabinetInput.Contains((int)t.FromOldCabinetInputID)) &&
                    (toOldCabinetInput.Count == 0 || toOldCabinetInput.Contains((int)t.ToOldCabinetInputID)) &&
                    (fromNewCabinetInput.Count == 0 || fromNewCabinetInput.Contains((int)t.FromNewCabinetInputID)) &&
                    (!accomplishmentDateDate.HasValue || t.MDFAccomplishmentDate == accomplishmentDateDate) &&
                    (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo) &&
                    (string.IsNullOrEmpty(accomplishmentTime) || t.MDFAccomplishmentTime == accomplishmentTime)
                    )
                    .Select(t => new ExchangeCenralCableCabinetInfo
                    {
                        ID = t.ID,
                        RequestID = t.ID,

                        OldCabinetID = t.OldCabinetID,
                        NewCabinetID = t.NewCabinetID,

                        NewCabinetName = t.Cabinet1.CabinetNumber.ToString(),
                        OldCabinetName = t.Cabinet.CabinetNumber.ToString(),

                        FromOldCabinetInputID = t.ToOldCabinetInputID,
                        ToOldCabinetInputID = t.ToOldCabinetInputID,

                        FromOldCabinetInputName = t.CabinetInput.InputNumber.ToString(),
                        ToOldCabinetInputName = t.CabinetInput1.InputNumber.ToString(),

                        FromNewCabinetInputID = t.FromNewCabinetInputID,
                        ToNewCabinetInputID = t.ToNewCabinetInputID,

                        FromNewCabinetInputName = t.CabinetInput2.InputNumber.ToString(),
                        ToNewCabinetInputName = t.CabinetInput3.InputNumber.ToString(),

                        InsertDate = t.InsertDate,
                        AccomplishmentDate = t.MDFAccomplishmentDate,
                        AccomplishmentTime = t.MDFAccomplishmentTime,
                        StatusTitle = t.Request.Status.Title,
                        requestLetterNo = t.Request.RequestLetterNo
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }
    }
}
