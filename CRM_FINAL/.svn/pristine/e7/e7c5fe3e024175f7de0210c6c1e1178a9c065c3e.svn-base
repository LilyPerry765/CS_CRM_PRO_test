using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class InstallRequestDB
    {
        public static InstallRequest GetInstallRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallRequests.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static InstallRequest GetInstallRequestByRequestID(long RequestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallRequests.Where(t => t.RequestID == RequestID).Take(1).SingleOrDefault();
            }
        }

        public static List<InstallRequest> GetInstallRequestByRequestIDs(List<long> RequestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InstallRequests.Where(t => RequestIDs.Contains(t.RequestID)).ToList();
            }
        }

        /// <summary>
        /// .آخرین رکورد مربوط به درخواست دایری تلفن مورد نظر را برمیگرداند
        /// </summary>
        /// <param name="telephone">تلفن مورد نظر</param>
        /// <returns></returns>
        public static InstallRequest GetLastInstallRequestByTelephone(long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteQuery<InstallRequest>(@"select top 1 InstallRequest.* 
                                                              from(
                                                              select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where TelephoneNo = {0} )
                                                              union
                                                              select * from Request where TelephoneNo in ( select  TelephoneNo from RequestLog where ToTelephoneNo = {0} )
                                                              union
                                                              select * from Request where TelephoneNo  = {0} ) as T 
                                                              join InstallRequest on T.ID = InstallRequest.RequestID
                                                              Order by  InstallRequest.InstallationDate DESC ", telephone).SingleOrDefault();

            }
        }

        public static InstallRequestShortInfo GetInstallShortInfoByRequestId(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                InstallRequestShortInfo result;
                var query = context.InstallRequests.Where(ir => ir.RequestID == requestId)
                                                   .Select(ir => new InstallRequestShortInfo
                                                                    {
                                                                        ChargingType = ir.ChargingType,
                                                                        MethodOfPaymentForTelephoneConnection = ir.MethodOfPaymentForTelephoneConnection,
                                                                        PosessionType = ir.PosessionType
                                                                    }
                                                           );
                result = query.SingleOrDefault();
                if (result != null)
                {
                    result.ChargingTypeName = DB.GetEnumDescriptionByValue(typeof(DB.ChargingGroup), result.ChargingType);
                    result.MethodOfPaymentForTelephoneConnectionName = DB.GetEnumDescriptionByValue(typeof(DB.MethodOfPaymentForTelephoneConnection), result.MethodOfPaymentForTelephoneConnection);
                    result.PosessionTypeName = DB.GetEnumDescriptionByValue(typeof(DB.PossessionType), result.PosessionType);
                }
                return result;
            }
        }
    }
}
