using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
     public class TelephoneSpecialServiceTypeDB
    {
        public static List<TelephoneSpecialServiceType> GetTelephoneSpecialServiceTypeDB(long telephoneNo, List<int> spesialServiceTypes)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.TelephoneSpecialServiceTypes.Where(t => t.TelephoneNo == telephoneNo && spesialServiceTypes.Contains(t.SpecialServiceTypeID)).ToList();
            }
        }

        public static void DeleteTelephoneSpecialServiceType(long telephoneNo, List<int> spesialServiceTypes)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                using (MainDataContext context = new MainDataContext())
                {

                        var itemlist =   context.TelephoneSpecialServiceTypes.Where(t => t.TelephoneNo == telephoneNo && spesialServiceTypes.Contains(t.SpecialServiceTypeID)).ToList();
                        context.TelephoneSpecialServiceTypes.DeleteAllOnSubmit(itemlist);
                        context.SubmitChanges();
                }

                ts.Complete();
            }
        }

        public static List<CheckableItem> GetSpecialServicesOfTelephone(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TelephoneSpecialServiceTypes.Where(t => t.TelephoneNo == telephoneNo).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.SpecialServiceType.Title, IsChecked = true }).ToList();
            }
        }

         /// <summary>
         /// transfor telephone feature
         /// </summary>
         /// <param name="oldTelephone"> from telephone </param>
         /// <param name="newTelephone"> to telephone </param>
        public static void ExchangeTelephoneNoFeature(Request request, DB.RequestType requestType, DB.LogType logType,long oldTelephone, long newTelephone , bool isReject)
        {
            try
            {

                DateTime serverDateTime = DB.GetServerDate();
                using (MainDataContext context = new MainDataContext())
                {
                    using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (!isReject)
                        {
                            RequestLog requestLog = new RequestLog();
                            requestLog.RequestID = request.ID;
                            requestLog.RequestTypeID = (int)requestType;
                            requestLog.LogType = (byte)logType;
                            requestLog.IsReject = false;
                            requestLog.TelephoneNo = oldTelephone;
                            requestLog.ToTelephoneNo = newTelephone;

                            if (request.CustomerID != null)
                            requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(request.CustomerID);

                            CRM.Data.Schema.TransferTelephoneNoFeature transferTelephoneNoFeature = new Data.Schema.TransferTelephoneNoFeature();
                            transferTelephoneNoFeature.SpecialServiceIds = context.TelephoneSpecialServiceTypes.Where(t => t.TelephoneNo == oldTelephone).Select(t => t.SpecialServiceTypeID).ToList();
                            transferTelephoneNoFeature.ClassTelephone = context.Telephones.Where(t => t.TelephoneNo == oldTelephone).Single().ClassTelephone;


                            requestLog.Description = System.Xml.Linq.XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.TransferTelephoneNoFeature>(transferTelephoneNoFeature, true));
                            requestLog.Date = serverDateTime;
                            context.RequestLogs.InsertOnSubmit(requestLog);

                            //context.Telephones.Where(t => t.TelephoneNo == newTelephone).Single().ClassTelephone = context.Telephones.Where(t => t.TelephoneNo == oldTelephone).Single().ClassTelephone;

                            //context.Telephones.Where(t => t.TelephoneNo == oldTelephone).Single().ClassTelephone = (byte)DB.ClassTelephone.LimitLess;

                            context.ExecuteCommand(@"UPDATE [dbo].[TelephoneSpecialServiceType]
                                               SET [TelephoneNo] = {0}
                                               WHERE TelephoneNo = {1}", newTelephone, oldTelephone);


                        }
                        else
                        {
                            if(context.RequestLogs.Any(t => t.RequestID == request.ID && t.LogType == (int)logType && t.IsReject == false))
                            {
                                context.RequestLogs.Where(t => t.RequestID == request.ID && t.LogType == (int)logType).OrderByDescending(t => t.Date).FirstOrDefault().IsReject = isReject;

                                //context.Telephones.Where(t => t.TelephoneNo == newTelephone).Single().ClassTelephone = context.Telephones.Where(t => t.TelephoneNo == oldTelephone).Single().ClassTelephone;

                                //context.Telephones.Where(t => t.TelephoneNo == oldTelephone).Single().ClassTelephone = (byte)DB.ClassTelephone.LimitLess;

                                context.ExecuteCommand(@"UPDATE [dbo].[TelephoneSpecialServiceType]
                                               SET [TelephoneNo] = {0}
                                               WHERE TelephoneNo = {1}", newTelephone, oldTelephone);
                            }
                        }



                        context.SubmitChanges();




                        Subts.Complete();
                    }
                }

            }
            catch(Exception ex)
            {
                throw new Exception("خطا در انتقال سرویس ویژه ها");
            }

        }

        public static List<TelephoneSpecialServiceType> GetSpecialServicesOfTelephone(List<long> telephoneNos)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.TelephoneSpecialServiceTypes.Where(t=>telephoneNos.Contains(t.TelephoneNo)).ToList();
            }
        }
    }
}
