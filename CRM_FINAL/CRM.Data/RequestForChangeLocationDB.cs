using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public class RequestForChangeLocationDB
    {
        public static void SaveRequest(Request request, ChangeLocation changeLocation, Telephone telephone, long? pastTelephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                {
                    request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request,true);
                }
                else
                {
                    request.Detach();
                    DB.Save(request,false);
                }

                changeLocation.ID = request.ID;
                changeLocation.Detach();
                DB.Save(changeLocation, isNew);

                if (telephone != null)
                {
                    telephone.Detach();
                    DB.Save(telephone);
                }

                ts.Complete();
            }
        }

        public static void SaveDischargeRequest(Request request, TakePossession takePossession, Telephone telephone, long? pastTelephone, bool isNew)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                if (isNew)
                {
                    request.ID = DB.GenerateRequestID();
                    request.Detach();
                    DB.Save(request,true);

                    takePossession.ID = request.ID;
                    takePossession.InsertDate = DB.GetServerDate();
                }
                else
                {
                    request.Detach();
                    DB.Save(request,false);

                    Telephone PastTelephont = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)pastTelephone);
                    if (PastTelephont != null)
                    {
                        PastTelephont.Status = (byte)DB.TelephoneStatus.Connecting;
                        PastTelephont.Detach();
                        DB.Save(PastTelephont);
                    }

                }

                //TODO: چون بین دو جدول ارتباط بود و روالش مشخص نبود یک رکورو خالی اضافه کردم 
                //RefundDeposit refundDeposit = new RefundDeposit();
                //refundDeposit.ID = request.ID;
                //refundDeposit.Detach();
                //DB.Save(refundDeposit, isNew);

                takePossession.Detach();
                DB.Save(takePossession, isNew);

                telephone.Detach();
                DB.Save(telephone);
                ts.Complete();
            }

        }
    }
}
