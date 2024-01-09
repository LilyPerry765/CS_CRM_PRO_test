using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Web.Services;
using CRM.Data;
//using CRM.WebService.SMSService;
using System.Data.Linq.Mapping;
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;
using Enterprise;

namespace CRM.WebService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CRMWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string SaveFailure117(long telephoneNo, long callingNo, int centercode, byte[] recordeSound, out bool result, out bool isConfirmed)
        {
            //result = false; 

            Request request = new Request();
            Data.Failure117 failureRequest = new Data.Failure117();

            Service1 service = new Service1();
            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

            if (telephoneNo == 2333388205)
            {
                request = new Request();

                request.IsViewed = false;
                request.TelephoneNo = telephoneNo;
                request.RequestTypeID = (byte)DB.RequestType.Failure117;
                request.CenterID = CenterDB.GetCenterByCenterCode(centercode).ID;
                //request.CustomerID=
                request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                request.RequestDate = DB.GetServerDate();
                request.InsertDate = DB.GetServerDate();
                request.CreatorUserID = 1;
                request.ModifyUserID = 1;
                request.IsWaitingList = false;
                request.IsCancelation = false;
                request.IsVisible = true;
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                failureRequest.CallingNo = callingNo;
                failureRequest.RecordeSound = recordeSound;

                RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                result = true;
                isConfirmed = false;
            }
            else
            {
                request = Failure117DB.GetFailureRequest(telephoneNo);

                if (request != null)
                {
                    if (request.EndDate != null && request.EndDate > DB.GetServerDate())
                    {
                        result = false;
                        isConfirmed = true;
                    }
                    else
                    {
                        if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                        {
                            result = false;
                            isConfirmed = true;
                        }
                        else
                        {
                            request = new Request();

                            request.IsViewed = false;
                            request.TelephoneNo = telephoneNo;
                            request.RequestTypeID = (byte)DB.RequestType.Failure117;
                            request.CenterID = CenterDB.GetCenterByCenterCode(centercode).ID;
                            //request.CustomerID=
                            request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                            request.RequestDate = DB.GetServerDate();
                            request.InsertDate = DB.GetServerDate();
                            request.CreatorUserID = 1;
                            request.ModifyUserID = 1;
                            request.IsWaitingList = false;
                            request.IsCancelation = false;
                            request.IsVisible = true;
                            Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                            request.StatusID = status.ID;

                            failureRequest.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                            failureRequest.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                            failureRequest.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                            failureRequest.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                            failureRequest.CallingNo = callingNo;
                            failureRequest.RecordeSound = recordeSound;

                            RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                            result = true;
                            isConfirmed = false;
                        }
                    }
                }
                else
                {
                    request = new Request();

                    request.IsViewed = false;
                    request.TelephoneNo = telephoneNo;
                    request.RequestTypeID = (byte)DB.RequestType.Failure117;
                    request.CenterID = CenterDB.GetCenterByCenterCode(centercode).ID;
                    //request.CustomerID=
                    request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                    request.RequestDate = DB.GetServerDate();
                    request.InsertDate = DB.GetServerDate();
                    request.CreatorUserID = 1;
                    request.ModifyUserID = 1;
                    request.IsWaitingList = false;
                    request.IsCancelation = false;
                    request.IsVisible = true;
                    Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                    request.StatusID = status.ID;

                    failureRequest.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                    failureRequest.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                    failureRequest.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                    failureRequest.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                    failureRequest.CallingNo = callingNo;
                    failureRequest.RecordeSound = recordeSound;

                    RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                    result = true;
                    isConfirmed = false;
                }
            }

            return request.ID.ToString();
        }

        [WebMethod]
        public string SaveFailure117Kermanshah(long telephoneNo, long callingNo, byte[] recordeSound, out bool result, out bool isConfirmed, out bool isDischarge, out bool isTechnicalFailed, out bool isUnkown)
        {
            Logger.WriteInfo("Start Filaure117 Methos");

            isTechnicalFailed = false;
            isDischarge = false;
            isUnkown = false;

            try
            {
                telephoneNo = 8300000000 + telephoneNo;

                if (TelephoneDB.HasTelephoneNo(telephoneNo))
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);

                    if (telephone != null)
                    {
                        if (telephone.Status == (byte)DB.TelephoneStatus.Discharge || telephone.Status == (byte)DB.TelephoneStatus.Cut || telephone.Status == (byte)DB.TelephoneStatus.Free)
                        {
                            isDischarge = true;
                            result = false;
                            isConfirmed = false;
                            return "";
                        }
                    }
                }
                else
                {
                    if (!TelephoneDB.HasTelephoneTemp(telephoneNo))
                    {
                        isUnkown = true;
                        isDischarge = false;
                        result = false;
                        isConfirmed = false;
                        return "";
                    }
                }

                Request request = new Request();
                Data.Failure117 failureRequest = new Data.Failure117();
                request = Failure117DB.GetFailureRequest(telephoneNo);
                TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(telephoneNo);

                if (request != null)
                {
                    if (request.EndDate != null && request.EndDate > DB.GetServerDate())
                    {
                        result = false;
                        isConfirmed = true;
                    }
                    else
                    {
                        if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                        {
                            result = false;
                            isConfirmed = true;
                        }
                        else
                        {
                            request = new Request();

                            int centerID = 0;
                            if (TelephoneDB.HasTelephoneNo(telephoneNo))
                            {
                                centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);

                                int cabinetNo = Failure117DB.GetCabinetNobyTelephoneNo(telephoneNo);
                                if (cabinetNo != 0)
                                {
                                    if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                                    {
                                        isTechnicalFailed = true;
                                        isDischarge = false;
                                        result = false;
                                        isConfirmed = false;
                                        return "";
                                    }
                                    else
                                    {
                                        int postNo = Failure117DB.GetPostNobyTelephoneNo(telephoneNo);
                                        if (postNo != 0)
                                        {
                                            if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                                            {
                                                isTechnicalFailed = true;
                                                isDischarge = false;
                                                result = false;
                                                isConfirmed = false;
                                                return "";
                                            }
                                            else
                                            {
                                                if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                                                {
                                                    isTechnicalFailed = true;
                                                    isDischarge = false;
                                                    result = false;
                                                    isConfirmed = false;
                                                    return "";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                            request.IsViewed = false;
                            request.TelephoneNo = telephoneNo;
                            request.RequestTypeID = (byte)DB.RequestType.Failure117;
                            request.CenterID = centerID;
                            request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                            request.RequestDate = DB.GetServerDate();
                            request.InsertDate = DB.GetServerDate();
                            request.CreatorUserID = 6;
                            request.ModifyUserID = 6;
                            request.IsWaitingList = false;
                            request.IsCancelation = false;
                            request.IsVisible = true;
                            Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                            request.StatusID = status.ID;

                            if (technicalInfo != null)
                            {
                                failureRequest.CabinetNo = technicalInfo.CabinetNo;
                                failureRequest.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                                failureRequest.PostNo = technicalInfo.PostNo;
                                failureRequest.PostEtesali = technicalInfo.ConnectionNo;
                            }
                            else
                            {
                                failureRequest.CabinetNo = "";
                                failureRequest.CabinetMarkazi = "";
                                failureRequest.PostNo = "";
                                failureRequest.PostEtesali = "";
                            }

                            failureRequest.CallingNo = callingNo;
                            failureRequest.RecordeSound = recordeSound;

                            RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                            result = true;
                            isConfirmed = false;
                        }
                    }
                }
                else
                {
                    request = new Request();

                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(telephoneNo))
                    {
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);

                        int cabinetNo = Failure117DB.GetCabinetNobyTelephoneNo(telephoneNo);
                        if (cabinetNo != 0)
                        {
                            if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                            {
                                isTechnicalFailed = true;
                                isDischarge = false;
                                result = false;
                                isConfirmed = false;
                                return "";
                            }
                            else
                            {
                                int postNo = Failure117DB.GetPostNobyTelephoneNo(telephoneNo);
                                if (postNo != 0)
                                {
                                    if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                                    {
                                        isTechnicalFailed = true;
                                        isDischarge = false;
                                        result = false;
                                        isConfirmed = false;
                                        return "";
                                    }
                                    else
                                    {
                                        if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                                        {
                                            isTechnicalFailed = true;
                                            isDischarge = false;
                                            result = false;
                                            isConfirmed = false;
                                            return "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                    request.IsViewed = false;
                    request.TelephoneNo = telephoneNo;
                    request.RequestTypeID = (byte)DB.RequestType.Failure117;
                    request.CenterID = centerID;
                    Logger.WriteInfo(telephoneNo.ToString() + " - " + centerID.ToString());
                    request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                    request.RequestDate = DB.GetServerDate();
                    request.InsertDate = DB.GetServerDate();
                    request.CreatorUserID = 6;
                    request.ModifyUserID = 6;
                    request.IsWaitingList = false;
                    request.IsCancelation = false;
                    request.IsVisible = true;
                    Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                    request.StatusID = status.ID;

                    if (technicalInfo != null)
                    {
                        failureRequest.CabinetNo = technicalInfo.CabinetNo;
                        failureRequest.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                        failureRequest.PostNo = technicalInfo.PostNo;
                        failureRequest.PostEtesali = technicalInfo.ConnectionNo;
                    }
                    else
                    {
                        failureRequest.CabinetNo = "";
                        failureRequest.CabinetMarkazi = "";
                        failureRequest.PostNo = "";
                        failureRequest.PostEtesali = "";
                    }

                    failureRequest.CallingNo = callingNo;
                    failureRequest.RecordeSound = recordeSound;

                    RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                    result = true;
                    isConfirmed = false;
                }
                Logger.WriteInfo("End Filaure117 Methos");
                return request.ID.ToString();

            }
            catch (Exception ex)
            {
                Logger.WriteInfo(ex.Message);
                result = false;
                isConfirmed = false;
                return "";
            }
        }

        [WebMethod]
        public string SaveFailure117Karaj(long telephoneNo, long callingNo, byte[] recordeSound, out bool result, out bool isConfirmed)
        {
            try
            {
                Request request = new Request();
                Data.Failure117 failureRequest = new Data.Failure117();

                request.IsViewed = false;
                request.TelephoneNo = telephoneNo;
                request.RequestTypeID = (byte)DB.RequestType.Failure117;
                request.CenterID = 1;
                request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                request.RequestDate = DB.GetServerDate();
                request.InsertDate = DB.GetServerDate();
                request.CreatorUserID = 1;
                request.ModifyUserID = 1;
                request.IsWaitingList = false;
                request.IsCancelation = false;
                request.IsVisible = true;
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                failureRequest.CabinetNo = "";
                failureRequest.CabinetMarkazi = "";
                failureRequest.PostNo = "";
                failureRequest.PostEtesali = "";
                failureRequest.CallingNo = callingNo;
                failureRequest.RecordeSound = recordeSound;

                RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                result = true;
                isConfirmed = false;

                return request.ID.ToString();
            }
            catch (Exception ex)
            {
                Logger.WriteInfo("CRM Web Service Error : " + ex.Message);
                result = false;
                isConfirmed = false;
                return "";
            }
        }

        [WebMethod]
        public bool GetFailureRequestState(long telephoneNo, out bool isFinished, out int resultFailure, out int color1, out int color2, out int cableType)
        {
            bool isFailed = false;
            isFinished = true;
            resultFailure = 0;
            color1 = 0;
            color2 = 0;
            cableType = 0;

            List<Request> requestList = RequestDB.GetFailureRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.Failure117);
            Request request = requestList.OrderByDescending(t => t.InsertDate).FirstOrDefault();
            Failure117 failure = new Failure117();
            FailureForm failureForm = new FailureForm();

            if (request != null)
            {
                if (request.EndDate == null)
                    isFinished = false;
                else
                {
                    failure = Failure117DB.GetFailureRequestByID(request.ID);
                    if (failure != null)
                    {
                        if (failure.ResultAfterReturn != null)
                            resultFailure = (int)failure.ResultAfterReturn;

                        failureForm = Failure117DB.GetFailureForm(failure.ID);
                        if (failureForm != null)
                        {
                            if (failureForm.CableColor1 != null)
                                color1 = (int)failureForm.CableColor1;
                            else
                                color1 = 0;

                            if (failureForm.CableColor2 != null)
                                color2 = (int)failureForm.CableColor2;
                            else
                                color2 = 0;

                            cableType = failureForm.CableTypeID;
                        }
                    }
                    else
                        isFailed = true;
                }
            }
            else
                isFailed = true;

            return isFailed;
        }

        [WebMethod]
        public bool CheckCabinetAccuracy(int cabinetNo, int postNo, long teleophoneNo, int centercode)
        {
            bool result = false;
            int centerID = CenterDB.GetCenterByCenterCode(centercode).ID;

            if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                result = true;
            else
            {
                if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                    result = true;
                else
                {
                    if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(teleophoneNo, centerID))
                        result = true;
                    else
                        result = false;
                }
            }

            return result;
        }

        [WebMethod]
        public bool SaveFailure117fromHelpDesk(long telephoneNo, long ticketID, string description, out long requestID)
        {
            bool result = true;

            Request request = new Request();
            Data.Failure117 failureRequest = new Data.Failure117();

            Service1 service = new Service1();
            System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

            request = Failure117DB.GetFailureRequest(telephoneNo);

            if (request != null)
            {
                failureRequest = Failure117DB.GetFailureRequestByID(request.ID);

                if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                {
                    result = false;
                    requestID = 0;
                }
                else
                {
                    request = new Request();

                    request.IsViewed = false;
                    request.TelephoneNo = telephoneNo;
                    request.RequestTypeID = (byte)DB.RequestType.Failure117;
                    request.CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
                    //request.CustomerID=
                    request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                    request.RequestDate = DB.GetServerDate();
                    request.InsertDate = DB.GetServerDate();
                    request.CreatorUserID = 1;
                    request.ModifyUserID = 1;
                    request.IsWaitingList = false;
                    request.IsCancelation = false;
                    request.IsVisible = true;
                    Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                    request.StatusID = status.ID;

                    failureRequest.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                    failureRequest.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                    failureRequest.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                    failureRequest.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                    //failureRequest.CallingNo = callingNo;
                    failureRequest.RecordeSound = null;
                    failureRequest.HelpDeskTicketID = ticketID;
                    failureRequest.HelpDeskDescription = description;

                    RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                    requestID = request.ID;
                }
            }
            else
            {
                request = new Request();

                request.IsViewed = false;
                request.TelephoneNo = telephoneNo;
                request.RequestTypeID = (byte)DB.RequestType.Failure117;
                request.CenterID = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString())).ID;
                //request.CustomerID=
                request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                request.RequestDate = DB.GetServerDate();
                request.InsertDate = DB.GetServerDate();
                request.CreatorUserID = 1;
                request.ModifyUserID = 1;
                request.IsWaitingList = false;
                request.IsCancelation = false;
                request.IsVisible = true;
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                failureRequest.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                failureRequest.CabinetMarkazi = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                failureRequest.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                failureRequest.PostEtesali = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                //failureRequest.CallingNo = callingNo;
                failureRequest.RecordeSound = null;
                failureRequest.HelpDeskTicketID = ticketID;
                failureRequest.HelpDeskDescription = description;

                RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                requestID = request.ID;
            }

            return result;
        }

        [WebMethod]
        public void SendMessage(string telephoneNos, string message)
        {
            if (!string.IsNullOrWhiteSpace(telephoneNos))
            {
                SOAPSendSMS.MessageRelayService SOAPSMS = new SOAPSendSMS.MessageRelayService();
                string[] list = new string[1];

                string mobileNo = telephoneNos;
                if (mobileNo.StartsWith("0"))
                    mobileNo = mobileNo.Remove(0, 1);
                mobileNo = "98" + mobileNo;
                list[0] = mobileNo;

                try
                {
                    SOAPSMS.sendMessageOneToMany("tctsemnan", "6311256", "500042020", list, message);
                }
                catch (Exception ex)
                {
                }
            }
        }

        [WebMethod]
        public long SaveADSLChangeService(long telephoneNo, int oldServiceID, int newServiceID, out bool result)
        {
            long id = 0;
            result = false;

            try
            {
                Request request = new Request();
                Data.ADSLChangeService changeServiceRequest = new Data.ADSLChangeService();

                Service1 service = new Service1();
                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

                Center center = null;
                if (telephoneInfo.Rows.Count > 0)
                    center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                else
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                    center = CenterDB.GetCenterByCenterID(telephone.CenterID);
                }

                request.IsViewed = false;
                request.TelephoneNo = telephoneNo;
                request.RequestTypeID = (byte)DB.RequestType.ADSLChangeService;
                request.CenterID = center.ID;
                //request.CustomerID=
                request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
                request.RequestDate = DB.GetServerDate();
                request.InsertDate = DB.GetServerDate();
                request.CreatorUserID = 1;
                request.ModifyUserID = 1;
                request.IsWaitingList = false;
                request.IsCancelation = false;
                request.IsVisible = false;
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                changeServiceRequest.OldServiceID = oldServiceID;
                changeServiceRequest.NewServiceID = newServiceID;
                changeServiceRequest.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Internet;
                changeServiceRequest.ChangeServiceActionType = (byte)DB.ADSLChangeServiceActionType.ExtensionService;
                //changeServiceRequest.FinalUserID = 1;
                //changeServiceRequest.FinalDate = DB.GetServerDate();
                //changeServiceRequest.FinalComment = "";
                changeServiceRequest.Status = false;

                RequestForADSL.SaveADSLChangeServiceRequest(request, changeServiceRequest, null, true);

                result = true;

                id = request.ID;
            }
            catch (Exception ex)
            {
            }

            return id;
        }

        [WebMethod]
        public bool ConfirmADSLChangeService(long requestID, int newServiceID, bool isIBSngUpdated)
        {
            bool result = false;

            try
            {
                Request request = RequestDB.GetRequestByID(requestID);
                Data.ADSLChangeService changeServiceRequest = ADSLChangeTariffDB.GetADSLChangeServicebyID(requestID);
                ADSL aDSl = ADSLDB.GetADSLByTelephoneNo((long)request.TelephoneNo);
                int? duration = ADSLServiceDB.GetADSLServiceDurationByServiceID(newServiceID);

                aDSl.TariffID = newServiceID;
                aDSl.InstallDate = DB.GetServerDate();
                if (duration != null && duration != 0)
                    aDSl.ExpDate = aDSl.ExpDate.Value.AddMonths((int)duration);

                aDSl.Detach();
                CRMWebServiceDB.Save(aDSl, false);

                Logger.WriteInfo(requestID.ToString()+ "  ......   "+ "Save ADSl");

                if (isIBSngUpdated == true)
                    changeServiceRequest.IsIBSngUpdated = true;
                else
                    changeServiceRequest.IsIBSngUpdated = false;

                changeServiceRequest.FinalUserID = 1;
                changeServiceRequest.FinalDate = DB.GetServerDate();
                changeServiceRequest.FinalComment = "";

                changeServiceRequest.Detach();
                CRMWebServiceDB.Save(changeServiceRequest);

                Logger.WriteInfo(requestID.ToString() + "  ......   " + "Save ADSlChangeService");

                request.EndDate = DB.GetServerDate();
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.End);
                request.StatusID = status.ID;

                request.Detach();
                CRMWebServiceDB.Save(request);

                Logger.WriteInfo(requestID.ToString() + "  ......   " + "Save Request");

                ADSLHistory history = new ADSLHistory();

                history.TelephoneNo = request.TelephoneNo.ToString();
                history.ServiceID = newServiceID;
                history.UserID = 1;
                history.InsertDate = DB.GetServerDate();

                history.Detach();
                CRMWebServiceDB.Save(history);

                Logger.WriteInfo(requestID.ToString() + "  ......   " + "Save ADSlHistory");

                result = true;
            }
            catch (Exception ex)
            {
                Logger.WriteError(ex.Message);
            }

            return result;
        }

        [WebMethod]
        public long SaveADSLSaleTraffice(long telephoneNo, int trafficeID, out bool result)
        {
            long id = 0;
            result = false;

            try
            {
                Request request = new Request();
                Data.ADSLSellTraffic SellTrafficRequest = new Data.ADSLSellTraffic();

                Service1 service = new Service1();
                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

                Center center = null;
                if (telephoneInfo.Rows.Count > 0)
                    center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(telephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                else
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
                    center = CenterDB.GetCenterByCenterID(telephone.CenterID);
                }

                request.IsViewed = false;
                request.TelephoneNo = telephoneNo;
                request.RequestTypeID = (byte)DB.RequestType.ADSLSellTraffic;
                request.CenterID = center.ID;
                //request.CustomerID=
                request.RequestPaymentTypeID = (byte)DB.PaymentType.Cash;
                request.RequestDate = DB.GetServerDate();
                request.InsertDate = DB.GetServerDate();
                request.CreatorUserID = 1;
                request.ModifyUserID = 1;
                request.IsWaitingList = false;
                request.IsCancelation = false;
                request.IsVisible = false;
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                request.StatusID = status.ID;

                SellTrafficRequest.AdditionalServiceID = trafficeID;
                SellTrafficRequest.ChangeServiceType = (byte)DB.ADSLChangeServiceType.Internet;

                RequestForADSL.SaveADSLSellTrafficRequest(request, SellTrafficRequest, null, null, true);

                result = true;

                id = request.ID;
            }
            catch (Exception ex)
            {
            }

            return id;
        }

        [WebMethod]
        public bool ConfirmADSLSaleTraffic(long requestID, bool isIBSngUpdated)
        {
            bool result = false;

            try
            {
                Request request = RequestDB.GetRequestByID(requestID);
                ADSLSellTraffic selltraficcRequest = ADSLSellTrafficDB.GetADSLSellTrafficById(requestID);

                if (isIBSngUpdated == true)
                    selltraficcRequest.IsIBSngUpdated = true;
                else
                    selltraficcRequest.IsIBSngUpdated = false;

                selltraficcRequest.IsIBSngUpdated = true;
                selltraficcRequest.FinalUserID = 1;
                selltraficcRequest.FinalDate = DB.GetServerDate();
                selltraficcRequest.FinalComment = "";

                selltraficcRequest.Detach();
                CRMWebServiceDB.Save(selltraficcRequest);

                request.EndDate = DB.GetServerDate();
                Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.End);
                request.StatusID = status.ID;

                request.Detach();
                CRMWebServiceDB.Save(request);

                ADSLHistory history = new ADSLHistory();

                history.TelephoneNo = request.TelephoneNo.ToString();
                history.ServiceID = selltraficcRequest.AdditionalServiceID;
                history.UserID = 1;
                history.InsertDate = DB.GetServerDate();

                history.Detach();
                CRMWebServiceDB.Save(history);

                result = true;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        [WebMethod]
        public long SaveRequestPayment(long requestID, string orderID /* string billID, string paymentID */)
        {
            Request request = RequestDB.GetRequestByID(requestID);

            ADSLService service = null;
            BaseCost baseCost = null;
            if (request.RequestTypeID == (byte)DB.RequestType.ADSLChangeService)
            {
                ADSLChangeService aDSLChangeServiceRequest = ADSLChangeTariffDB.GetADSLChangeServicebyID(requestID);
                service = ADSLServiceDB.GetADSLServiceById((int)aDSLChangeServiceRequest.NewServiceID);
                baseCost = BaseCostDB.GetServiceCostForADSLChangeService();
            }
            if (request.RequestTypeID == (byte)DB.RequestType.ADSLSellTraffic)
            {
                ADSLSellTraffic aDSLSellTrafficRequest = ADSLSellTrafficDB.GetADSLSellTrafficById(requestID);
                service = ADSLServiceDB.GetADSLServiceById((int)aDSLSellTrafficRequest.AdditionalServiceID);
                baseCost = BaseCostDB.GetSellTrafficCostForADSL();
            }

            int? duration = ADSLServiceDB.GetADSLServiceDurationByServiceID((int)service.ID);

            RequestPayment requestPayment = new RequestPayment();

            requestPayment.BaseCostID = baseCost.ID;
            requestPayment.RequestID = requestID;
            requestPayment.PaymentType = (byte)DB.PaymentType.Cash;
            requestPayment.Cost = service.Price;
            requestPayment.Abonman = (service.Abonman != null) ? service.Abonman * (int)duration : 0;
            requestPayment.Tax = service.Tax;
            requestPayment.AmountSum = service.PriceSum;
            requestPayment.IsKickedBack = false;
            //requestPayment.BillID = billID;
            //requestPayment.PaymentID = paymentID;
            requestPayment.OrderID = orderID;
            requestPayment.IsAccepted = false;

            requestPayment.Detach();
            RequestPaymentDB.Save(requestPayment);

            return requestPayment.ID;
        }

        [WebMethod]
        public void PaidRequestPayment(long requestPaymentID, int bankCode, string traceNo)
        {
            RequestPayment requestPayment = RequestPaymentDB.GetRequestPaymentByID(requestPaymentID);

            requestPayment.PaymentWay = (byte)DB.PaymentWay.Internet;
            requestPayment.BankID = BankDB.GetBankIDbyCode(bankCode);
            requestPayment.FicheNunmber = traceNo;
            requestPayment.FicheDate = DB.GetServerDate();
            requestPayment.PaymentDate = DB.GetServerDate();
            requestPayment.UserID = 1;
            requestPayment.IsPaid = true;

            requestPayment.Detach();
            RequestPaymentDB.Save(requestPayment);
        }

        [WebMethod]
        public string GenerateBillID(long telephoneNo, int centerID, byte subsidiaryCodeType)
        {
            string subsidiaryCode = CenterDB.GetSubsidiaryCode(telephoneNo, centerID, subsidiaryCodeType);
            string billID = (telephoneNo.ToString().Substring(3) + subsidiaryCode + "4").AddCheckDigit();

            return billID;
        }

        [WebMethod]
        public string GeneratePaymentID(long amount, long telephoneNo, string billID, byte subsidiaryCodeType, bool isAddCycle)
        {
            TelephoneCycleFiche cycle = TelephoneCycleFicheDB.GetTelephoneCycle(telephoneNo, subsidiaryCodeType);

            if (cycle == null)
            {
                cycle = new TelephoneCycleFiche();
                cycle.TelephoneNo = telephoneNo;
                cycle.YearCode = 1392;
                cycle.CycleCode = 10;
                cycle.SubsidiaryCodeType = subsidiaryCodeType;
            }
            else
                if (isAddCycle)
                    cycle.CycleCode = cycle.CycleCode + 1;

            cycle.Detach();
            CRMWebServiceDB.Save(cycle);

            string paymentID = (amount.ToString().Substring(0, amount.ToString().Length - 3) + cycle.YearCode.ToString().Substring(3) + cycle.CycleCode.ToString().AddZeroPrefix(2)).AddCheckDigit();
            paymentID += (billID + paymentID).GetCheckDigit();

            return paymentID;
        }

        [WebMethod]
        public long GetADSLPayment(long telephoneNo)
        {
            List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentRemainByTelephoneNo(telephoneNo);

            long sumInstalment = 0;

            foreach (InstallmentRequestPayment currentInstalment in instalmentList)
            {
                if ((Convert.ToInt32((DateTime.Now - (DateTime)currentInstalment.StartDate).TotalDays)) > 0)
                {
                    sumInstalment = sumInstalment + currentInstalment.Cost;
                }
            }

            return sumInstalment;
        }

        [WebMethod]
        public List<InstalmentBillingInfo> GetADSLInstalmentforBilling(DateTime startDate, DateTime endDate)
        {
            List<InstalmentBillingInfo> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentInfoforCycle(startDate, endDate);

            return instalmentList;
        }

        [WebMethod]
        public void ConfirmADSLInstalmentforBilling(DateTime startDate, DateTime endDate)
        {
            List<InstallmentRequestPayment> instalmentList = InstallmentRequestPaymentDB.GetInstallmentRequestPaymentforCycle(startDate, endDate);

            foreach (InstallmentRequestPayment currentItem in instalmentList)
            {
                currentItem.IsPaid = true;

                CRMWebServiceDB.Save(currentItem, false);
            }
        }

        [WebMethod]
        public IBSngUserInfo GetUserInfo(IbsngInputInfo ibsngInputInfo)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

            XmlRpcStruct userAuthentication = new XmlRpcStruct();
            XmlRpcStruct userInfos = new XmlRpcStruct();
            XmlRpcStruct userInfo = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            try
            {
                userAuthentication.Add("normal_username", ibsngInputInfo.NormalUsername);
                userInfos = ibsngService.GetUserInfo(userAuthentication);
            }
            catch (Exception ex)
            {
                return null;
            }

            foreach (DictionaryEntry User in userInfos)
            {
                userInfo = (XmlRpcStruct)User.Value;
            }

            IBSngUserInfo ibsngUserInfo = new IBSngUserInfo();

            try
            {
                ibsngUserInfo.Name = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["name"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.Name = string.Empty;
            }
            try
            {
                ibsngUserInfo.NormalUsername = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_username"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.NormalUsername = string.Empty;
            }
            try
            {
                ibsngUserInfo.NormalPassword = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["normal_password"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.NormalPassword = string.Empty;
            }
            try
            {
                ibsngUserInfo.InternetOnlines = ((System.Object[][])(userInfo["internet_onlines"]));
            }
            catch (Exception ex)
            {
                ibsngUserInfo.InternetOnlines = null;
            }
            try
            {
                ibsngUserInfo.UserID = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["user_id"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.UserID = string.Empty;
            }
            try
            {
                ibsngUserInfo.CellPhone = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["cell_phone"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.CellPhone = string.Empty;
            }
            try
            {
                ibsngUserInfo.Email = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["email"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.Email = string.Empty;
            }
            try
            {
                ibsngUserInfo.PostalCode = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["postal_code"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.PostalCode = string.Empty;
            }
            try
            {
                ibsngUserInfo.Address = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["address"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.Address = string.Empty;
            }
            try
            {
                ibsngUserInfo.RealFirstLogin = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["real_first_login"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.RealFirstLogin = string.Empty;
            }
            try
            {
                ibsngUserInfo.FirstLogin = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["first_login"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.FirstLogin = string.Empty;
            }
            try
            {
                ibsngUserInfo.NearestExpDate = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["nearest_exp_date"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.NearestExpDate = string.Empty;
            }
            try
            {
                ibsngUserInfo.RechargeDeposit = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["recharge_deposit"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.RechargeDeposit = string.Empty;
            }
            try
            {
                ibsngUserInfo.Credit = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["credit"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.Credit = string.Empty;
            }
            try
            {
                ibsngUserInfo.OnlineStatus = (System.Boolean)(userInfo["online_status"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.OnlineStatus = false;
            }
            try
            {
                ibsngUserInfo.LimitMac = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["limit_mac"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.LimitMac = string.Empty;
            }
            try
            {
                ibsngUserInfo.RenewNextGroup = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["renew_next_group"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.RenewNextGroup = string.Empty;
            }
            try
            {
                ibsngUserInfo.Lock = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["lock"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.Lock = string.Empty;
            }
            try
            {
                ibsngUserInfo.MultiLogin = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["multi_login"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.MultiLogin = string.Empty;
            }
            try
            {
                ibsngUserInfo.CreationDate = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["creation_date"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.CreationDate = string.Empty;
            }
            try
            {
                ibsngUserInfo.CustomFieldFreeCounter = Convert.ToDouble(DB.ToStringSpecial(((XmlRpcStruct)(userInfo["attrs"]))["custom_field_free_counter"]));
            }
            catch (Exception ex)
            {
                ibsngUserInfo.CustomFieldFreeCounter = 0;
            }
            try
            {
                ibsngUserInfo.IBSngGroupName = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["group_name"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.IBSngGroupName = string.Empty;
            }
            try
            {
                ibsngUserInfo.CreationDate = DB.ToStringSpecial(((XmlRpcStruct)(userInfo["basic_info"]))["creation_date"]);
            }
            catch (Exception ex)
            {
                ibsngUserInfo.CreationDate = string.Empty;
            }

            return ibsngUserInfo;
        }

        [WebMethod]
        public void changeDeposit(ChangeIBSngInfo ibsngInfo)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("user_id", ibsngInfo.UserID);
            userAuthentication.Add("deposit", ibsngInfo.Deposit);
            userAuthentication.Add("is_absolute_change", ibsngInfo.IsAbsoluteChange);
            userAuthentication.Add("deposit_type", ibsngInfo.DepositType);
            userAuthentication.Add("deposit_comment", ibsngInfo.DepositComment);

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            ibsngService.changeDeposit(userAuthentication);
        }

        [WebMethod]
        public void UpdateUserAttrs(ChangeIBSngInfo ibsngInfo)
        {
            IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));
            XmlRpcStruct userAuthentication = new XmlRpcStruct();

            userAuthentication.Clear();

            userAuthentication.Add("auth_name", "pendar");
            userAuthentication.Add("auth_pass", "Pendar#!$^");
            userAuthentication.Add("auth_type", "ADMIN");

            XmlRpcStruct to_del_attrs_list = new XmlRpcStruct();
            XmlRpcStruct userAttrs = new XmlRpcStruct();

            if (ibsngInfo.RenewNextGroup != null && !string.IsNullOrWhiteSpace(ibsngInfo.RenewNextGroup))
                userAttrs.Add("renew_next_group", ibsngInfo.RenewNextGroup);
            if (ibsngInfo.RenewRemoveUserExpDates != null && !string.IsNullOrWhiteSpace(ibsngInfo.RenewRemoveUserExpDates))
                userAttrs.Add("renew_remove_user_exp_dates", ibsngInfo.RenewRemoveUserExpDates);

            if (ibsngInfo.CustomFieldFreeCounter != null && !string.IsNullOrWhiteSpace(ibsngInfo.CustomFieldFreeCounter))
            {
                XmlRpcStruct[] list = new XmlRpcStruct[2];

                list[0] = new XmlRpcStruct();
                list[1] = new XmlRpcStruct();
                list[0].Add("custom_field_free_counter", ibsngInfo.CustomFieldFreeCounter);
                userAttrs.Add("custom_fields", list);
            }

            userAuthentication.Add("user_id", ibsngInfo.UserID);
            userAuthentication.Add("attrs", userAttrs);
            userAuthentication.Add("to_del_attrs", to_del_attrs_list);

            ibsngService.UpdateUserAttrs(userAuthentication);
        }

        [WebMethod]
        public bool SaveADSLRequestKermanshah(string userName, string password, long telephoneNo, string customerName, int rowNo, int columnNo, int buchtNo, out string error)
        {
            try
            {
                bool result = false;
                error = string.Empty;

                if (userName == "data" && password == "kermanshah@data")
                {
                    error = "نام کاربری و رمز عبور صحیح نمی باشد";
                    return result;
                }

                BillingCRMService billingService = new BillingCRMService();
                V_LastDebtInfo debtInfo = null;

                try
                {
                    debtInfo = billingService.GetV_LastDebt(telephoneNo.ToString(), "pendar", "pendar@92");
                }
                catch (Exception)
                {

                }

                if (debtInfo != null)
                {
                    double debt = debtInfo.Lastdebt;

                    if (debt > Convert.ToInt64(Data.SettingDB.GetSettingValueByKey("ADSLPAPRequestDebt")))
                    {
                        error = "به دلیل بدهی ثبت درخواست امکان پذیر نمی باشد !";
                        return result;
                    }
                }

                int centerID = 0;
                if (TelephoneDB.HasTelephoneNo(telephoneNo))
                {
                    centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);
                    Bucht buchtCRM = BuchtDB.GetBuchtbyTelephoneNo(telephoneNo);

                    if (buchtCRM != null)
                        if (buchtCRM.PCMPortID != null)
                        {
                            error = "امکان تخصیص ADSL به این شماره وجود ندارد !";
                            return result;
                        }
                }
                else
                    centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                if (centerID == 0)
                {
                    error = "این شماره در سامانه امور مشترکین موجود نمی باشد !";
                    return result;
                }

                List<Request> aDSLPAPInstallRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(telephoneNo, (int)DB.RequestType.ADSLInstalPAPCompany);
                if (aDSLPAPInstallRequests.Count != 0)
                {
                    error = "برای این شماره در حال حاضر درخواست دایری موجود می باشد !";
                    return result;
                }

                if (ADSLTelephoneAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                {
                    error = "* امکان تخصیص ADSL به این شماره وجود ندارد !";
                    return result;
                }

                if (ADSLPAPPortDB.HasADSLbyTelephoneNo(telephoneNo))
                {
                    error = "* این شماره تلفن در حال حاضر دارای ADSL می باشد !";
                    return result;
                }

                ADSLPAPRequest papRequestK = new ADSLPAPRequest();
                Request requestK = new Request();
                ADSLPAPPort bucht = null;

                if (rowNo != 0 && columnNo != 0 && buchtNo != 0)
                {
                    if (!Data.ADSLPAPPortDB.HasPAPBucht(1, rowNo, columnNo, buchtNo, centerID))
                    {
                        error = "* بوخت مورد نظر موجود نمی باشد !";
                        return result;
                    }
                    if (!Data.ADSLPAPPortDB.GetBuchtStatus(1, rowNo, columnNo, buchtNo, centerID))
                    {
                        error = "* بوخت مورد نظر خالی نمی باشد !";
                        return result;
                    }

                    bucht = ADSLPAPPortDB.GetADSLPAPPortByBuchtNoAndCenter(1, rowNo, columnNo, buchtNo, centerID);
                }

                requestK.TelephoneNo = telephoneNo;
                requestK.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                requestK.CenterID = centerID;
                requestK.RequestDate = DB.GetServerDate();
                requestK.RequesterName = "مخابرات";
                requestK.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                requestK.StatusID = Data.WorkFlowDB.GetNextStatesID(DB.Action.Confirm, DB.GetStatus((byte)DB.RequestType.ADSLInstalPAPCompany, (byte)DB.RequestStatusType.Start).ID);
                requestK.InsertDate = DB.GetServerDate();
                requestK.ModifyDate = DB.GetServerDate();
                requestK.CreatorUserID = 6;
                requestK.ModifyUserID = 6;
                requestK.PreviousAction = 1;
                requestK.IsViewed = false;

                papRequestK.RequestTypeID = (byte)DB.RequestType.ADSLInstalPAPCompany;
                papRequestK.PAPInfoID = 1;
                papRequestK.TelephoneNo = telephoneNo;
                papRequestK.Customer = customerName;
                papRequestK.CustomerStatus = 1;
                if (bucht != null)
                {
                    papRequestK.ADSLPAPPortID = bucht.ID;
                    papRequestK.SplitorBucht = rowNo.ToString() + "," + columnNo.ToString() + "," + buchtNo.ToString();
                }
                else
                    papRequestK.SplitorBucht = "";

                papRequestK.LineBucht = "";
                papRequestK.NewPort = "";
                papRequestK.InstalTimeOut = 1;// (byte)Convert.ToInt16(InstalTimeOutList.SelectedValue);
                papRequestK.Status = (byte)DB.ADSLPAPRequestStatus.Pending;

                if (requestK != null)
                {
                    requestK.ID = DB.GenerateRequestID();
                    requestK.Detach();
                    RequestForADSL.Save(requestK, true);
                }

                if (papRequestK != null)
                {
                    if (requestK != null)
                        papRequestK.ID = requestK.ID;

                    papRequestK.Detach();
                    RequestForADSL.Save(papRequestK, true);
                }

                if (bucht != null)
                {
                    bucht.TelephoneNo = telephoneNo;
                    bucht.Status = (byte)DB.ADSLPAPPortStatus.Reserve;

                    bucht.Detach();
                    RequestForADSL.Save(bucht);
                }

                result = true;
                return result;

            }
            catch (Exception ex)
            {
                error = "ثبت درخواست با خطا مواجه شد";
                return false;
            }
        }
    }
}
