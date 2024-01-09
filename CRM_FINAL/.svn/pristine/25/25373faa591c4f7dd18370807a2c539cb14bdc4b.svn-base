using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data.Linq.Mapping;

namespace CRM.Data
{
    public static class RequestForADSL
    {
        public static void SaveADSLRequest(Request request, ADSLRequest ADSLRequest, ADSLIP iPStatic, ADSLGroupIP groupIPStatic, ADSL ADSL, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (ADSLRequest != null)
                {
                    if (request != null)
                        ADSLRequest.ID = request.ID;

                    ADSLRequest.Detach();
                    DB.Save(ADSLRequest, isNew);
                }

                if (iPStatic != null)
                {
                    iPStatic.Detach();
                    DB.Save(iPStatic);
                }

                if (groupIPStatic != null)
                {
                    groupIPStatic.Detach();
                    DB.Save(groupIPStatic);
                }

                if (ADSL != null)
                {
                    ADSL.Detach();

                    if (DB.SearchByPropertyName<ADSL>("TelephoneNo", ADSL.TelephoneNo).SingleOrDefault() == null)
                        DB.Save(ADSL, true);
                    else
                        DB.Save(ADSL, false);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLChangeServiceRequest(Request request, ADSLChangeService ADSLChangeService, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                Save(request, isNew);

                if (ADSLChangeService != null)
                {
                    ADSLChangeService.ID = request.ID;
                    ADSLChangeService.Detach();
                    Save(ADSLChangeService, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    Save(requestLog, true);
                }

                scope.Complete();
            }
        }
        
        public static void SaveADSLChangeServiceRequestForWeb(Request request, ADSLChangeService ADSLChangeService, RequestLog requestLog, bool isNew)
        {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                Save(request, isNew);

                if (ADSLChangeService != null)
                {
                    ADSLChangeService.ID = request.ID;
                    ADSLChangeService.Detach();
                    Save(ADSLChangeService, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    Save(requestLog, true);
                }
        }

        public static void SaveADSLSellTrafficRequest(Request request, ADSLSellTraffic ADSLSellTraffic, WirelessSellTraffic wirelessSellTraffic, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                Save(request, isNew);

                if (ADSLSellTraffic != null)
                {
                    ADSLSellTraffic.ID = request.ID;
                    ADSLSellTraffic.Detach();
                    Save(ADSLSellTraffic, isNew);
                }

                if (wirelessSellTraffic != null)
                {
                    wirelessSellTraffic.ID = request.ID;
                    wirelessSellTraffic.Detach();
                    Save(wirelessSellTraffic, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    Save(requestLog, true);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLChangeIPRequest(Request request, ADSLChangeIPRequest ADSLChangeIP, ADSLIP iPStatic, ADSLGroupIP groupIPStatic, ADSL ADSL, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (ADSLChangeIP != null)
                {
                    if (request != null)
                        ADSLChangeIP.ID = request.ID;

                    ADSLChangeIP.Detach();
                    DB.Save(ADSLChangeIP, isNew);
                }

                if (iPStatic != null)
                {
                    iPStatic.Detach();
                    DB.Save(iPStatic);
                }

                if (groupIPStatic != null)
                {
                    groupIPStatic.Detach();
                    DB.Save(groupIPStatic);
                }

                if (ADSL != null)
                {
                    ADSL.Detach();

                    if (DB.SearchByPropertyName<ADSL>("TelephoneNo", ADSL.TelephoneNo).SingleOrDefault() == null)
                        DB.Save(ADSL, true);
                    else
                        DB.Save(ADSL, false);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLInstallRequest(Request request, ADSLInstallRequest aDSLInstallRequest, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (aDSLInstallRequest != null)
                {
                    if (request != null)
                    {
                        aDSLInstallRequest.ID = request.ID;

                    }

                    aDSLInstallRequest.Detach();
                    DB.Save(aDSLInstallRequest, isNew);
                }

                scope.Complete();
            }
        }        

        public static void SaveADSLChangePlaceRequest(Request Request, ADSLChangePlace aDSLChangePlace, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (Request != null)
                {
                    if (isNew)
                        Request.ID = DB.GenerateRequestID();
                    Request.Detach();
                    DB.Save(Request, isNew);
                }

                if (aDSLChangePlace != null)
                {
                    if (Request != null)
                    {
                        aDSLChangePlace.ID = Request.ID;
                    }

                    aDSLChangePlace.Detach();
                    DB.Save(aDSLChangePlace, isNew);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLDischargeRequest(Request request, ADSLDischarge aDSLDischargeRequest,bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();

                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (aDSLDischargeRequest != null)
                {
                    if (request != null)
                        aDSLDischargeRequest.ID = request.ID;

                    aDSLDischargeRequest.Detach();
                    DB.Save(aDSLDischargeRequest, isNew);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLDischargeOnly(ADSLDischarge aDSLDischargeRequest, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (aDSLDischargeRequest != null)
                {
                    aDSLDischargeRequest.Detach();
                    DB.Save(aDSLDischargeRequest, isNew);
                }

                scope.Complete();
            }

        }

        public static void SaveADSLChangePortRequest(Request request, ADSLChangePort1 aDSLChangePortRequest, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();

                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (aDSLChangePortRequest != null)
                {
                    if (request != null)
                        aDSLChangePortRequest.ID = request.ID;

                    aDSLChangePortRequest.Detach();
                    DB.Save(aDSLChangePortRequest, isNew);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLCutTemporaryRequest(Request request, ADSLCutTemporary ADSLCutTemporary, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);

                if (ADSLCutTemporary != null)
                {
                    ADSLCutTemporary.ID = request.ID;
                    ADSLCutTemporary.Detach();
                    DB.Save(ADSLCutTemporary, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLTechnicalEquipment(ADSLRequest ADSLRequest, Bucht mainBucht, Bucht inputBucht, Bucht outputBucht, ADSLPort port, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ADSLRequest != null)
                {
                    ADSLRequest.Detach();
                    DB.Save(ADSLRequest, isNew);
                }

                if (mainBucht != null)
                {
                    mainBucht.Detach();
                    DB.Save(mainBucht, isNew);
                }

                if (inputBucht != null)
                {
                    inputBucht.Detach();
                    DB.Save(inputBucht, isNew);
                }

                if (outputBucht != null)
                {
                    outputBucht.Detach();
                    DB.Save(outputBucht, isNew);
                }

                if (port != null)
                {
                    port.Detach();
                    DB.Save(port, isNew);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLPAPRequest(Request request, ADSLPAPRequest ADSLPAPRequest, ADSL ADSL, ADSLPAPPort port, ADSLPAPPort newPort, Bucht bucht, RequestLog requestLog, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ADSLPAPRequest != null)
                {
                    ADSLPAPRequest.Detach();
                    DB.Save(ADSLPAPRequest, isNew);
                }

                if (request != null)
                {
                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (ADSL != null)
                {
                    ADSL.Detach();

                    if (DB.SearchByPropertyName<ADSL>("TelephoneNo", ADSL.TelephoneNo).SingleOrDefault() == null)
                        DB.Save(ADSL, true);
                    else
                        DB.Save(ADSL, false);
                }

                if (port != null)
                {
                    port.Detach();
                    DB.Save(port, isNew);
                }

                if (newPort != null)
                {
                    newPort.Detach();
                    DB.Save(newPort, isNew);
                }

                if (bucht != null)
                {
                    bucht.Detach();
                    DB.Save(bucht, isNew);
                }

                if (requestLog != null)
                {
                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                }

                scope.Complete();
            }
        }

        public static void SaveADSLPAPTechnicalEquipment(ADSLPAPRequest ADSLPAPRequest, ADSLPAPPort port, Bucht bucht, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (ADSLPAPRequest != null)
                {
                    ADSLPAPRequest.Detach();
                    DB.Save(ADSLPAPRequest, isNew);
                }

                if (port != null)
                {
                    port.Detach();
                    DB.Save(port, true);
                }

                if (bucht != null)
                {
                    bucht.Detach();
                    DB.Save(bucht, isNew);
                }

                scope.Complete();
            }
        }

        public static void SaveWaitingList(long requestID, Request request, WaitingList waitingList)//(Request request, ADSLRequest ADSLRequest, WaitingList waitingList, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    request.IsWaitingList = true;
                    request.Detach();
                    DB.Save(request, false);
                }

                //if (ADSLRequest != null)
                //{
                //    if (request != null)
                //        ADSLRequest.ID = request.ID;

                //    ADSLRequest.Detach();
                //    DB.Save(ADSLRequest, isNew);
                //}

                if (waitingList != null)
                {
                    waitingList.RequestID = requestID;
                    waitingList.Detach();
                    DB.Save(waitingList, true);
                }

                scope.Complete();
            }
        }

        public static void ExitWaitingList(WaitingList waitingList, Request request)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    request.IsWaitingList = false;
                    request.IsViewed = false;
                    request.PreviousAction = (byte)DB.Action.WaitingList;

                    request.Detach();
                    DB.Save(request, false);
                }

                if (waitingList != null)
                {
                    waitingList.ExitDate = DB.GetServerDate();
                    waitingList.ExitUserID = DB.CurrentUser.ID;
                    waitingList.Status = true;

                    waitingList.Detach();
                    DB.Save(waitingList, false);
                }

                scope.Complete();
            }
        }
        
        private static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save(object instance, bool isNew = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];
                MetaDataMember insertDateField = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "InsertDate").SingleOrDefault();
                MetaDataMember modifyDate = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyDate").SingleOrDefault();

                object obj = instance;

                if (modifyDate != null)
                    modifyDate.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    if (insertDateField != null)
                        insertDateField.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();
            }
        }

        public static void SaveADSLChangeCustomerCharacteristics(Request request, ADSLChangeCustomerOwnerCharacteristic aDSLChangeCustomer, bool isNew)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (request != null)
                {
                    if (isNew)
                        request.ID = DB.GenerateRequestID();

                    request.Detach();
                    DB.Save(request, isNew);
                }

                if (aDSLChangeCustomer != null)
                {
                    if (request != null)
                        aDSLChangeCustomer.ID = request.ID;

                    aDSLChangeCustomer.Detach();
                    DB.Save(aDSLChangeCustomer, isNew);
                }

                scope.Complete();
            }
        }

        #region Web

        public static void SaveADSLRequestForWeb(Request request, ADSLRequest ADSLRequest, ADSLIP iPStatic, ADSLGroupIP groupIPStatic, ADSL ADSL, RequestLog requestLog, bool isNew)
        {
            if (request != null)
            {
                if (isNew)
                    request.ID = DB.GenerateRequestID();
                request.Detach();
                DB.Save(request, isNew);
            }

            if (ADSLRequest != null)
            {
                if (request != null)
                    ADSLRequest.ID = request.ID;

                ADSLRequest.Detach();
                DB.Save(ADSLRequest, isNew);
            }

            if (iPStatic != null)
            {
                iPStatic.Detach();
                DB.Save(iPStatic);
            }

            if (groupIPStatic != null)
            {
                groupIPStatic.Detach();
                DB.Save(groupIPStatic);
            }

            if (ADSL != null)
            {
                ADSL.Detach();

                if (DB.SearchByPropertyName<ADSL>("TelephoneNo", ADSL.TelephoneNo).SingleOrDefault() == null)
                    DB.Save(ADSL, true);
                else
                    DB.Save(ADSL, false);
            }

            if (requestLog != null)
            {
                requestLog.Date = DB.GetServerDate();
                requestLog.Detach();
                DB.Save(requestLog, true);
            }
        }

        #endregion
    }
}
