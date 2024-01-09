using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class ContractDB
    {
        public static void SaveRequestContract(RequestDocument reqDoc, Contract contract, TelRoundSale roundInfo, TelRoundSale previousNo)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                if (roundInfo != null)
                {
                    Telephone tel = Data.TelephoneDB.GetTelephoneByTelephoneNo(roundInfo.TelephoneNo);
                    //update status of previousNo
                        if (previousNo != null && previousNo.TelephoneNo != roundInfo.TelephoneNo)
                        {
                            Telephone oldtel = Data.TelephoneDB.GetTelephoneByTelephoneNo(previousNo.TelephoneNo);

                            oldtel.Status = DB.GetTelephoneLastStatus(oldtel.TelephoneNo);
                            oldtel.Detach();
                            DB.Save(oldtel);

                            previousNo.SaleStatus = (byte)DB.TelRoundSaleStatus.InSale;
                            previousNo.IsActive = true;
                            previousNo.Detach();
                            DB.Save(previousNo);
                        }

                        roundInfo.SaleStatus = (byte)DB.TelRoundSaleStatus.regContract;
                        roundInfo.IsActive = false;
                        roundInfo.Detach();
                        DB.Save(roundInfo);

                        tel.Status = (byte)DB.TelephoneStatus.Reserv;
                        tel.Detach();
                        DB.Save(tel);

                        SelectTelephone _SelectTelephone = new SelectTelephone();
                        _SelectTelephone = SelectTelephoneDB.GetSelectTelephone((long)contract.RequestID);

                        if (_SelectTelephone == null)
                        {
                            _SelectTelephone = new SelectTelephone();
                            _SelectTelephone.ID = (long)contract.RequestID;
                            _SelectTelephone.ReserveDate = DB.GetServerDate();
                            _SelectTelephone.TelephoneNo = tel.TelephoneNo;
                            _SelectTelephone.SwitchPortID = tel.SwitchPortID;
                            _SelectTelephone.Detach();
                            DB.Save(_SelectTelephone, true);
                        }
                        else
                        {
                            _SelectTelephone.ReserveDate = DB.GetServerDate();
                            _SelectTelephone.TelephoneNo = tel.TelephoneNo;
                            _SelectTelephone.SwitchPortID = tel.SwitchPortID;
                            _SelectTelephone.Detach();
                            DB.Save(_SelectTelephone);
                        }

                }

                //save requestdocument
                reqDoc.Detach();
                DB.Save(reqDoc);



                //save referencedocument    
                ReferenceDocument refDoc = Data.ReferenceDocumentDB.GetReferenceDocumentByRequestDocumentIDByRequestID(reqDoc.ID, (long)contract.RequestID);
                if (refDoc == null)
                 {
                      refDoc = new ReferenceDocument();
                      refDoc.RequestDocumentID = reqDoc.ID;
                      refDoc.RequestID = contract.RequestID;
                      refDoc.Detach();
                      DB.Save(refDoc);
                 }


                //save contract
                contract.RequestDocumentID = reqDoc.ID;
                contract.Detach();
                DB.Save(contract);

                ts.Complete();

            }
        }


        public static void DeleteRequestContract(Contract contract)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //Change Status of telephone and roundlist
                if (contract.TelRoundSaleID != null)
                {

                    TelRoundSale rnd = Data.TelRoundSaleDB.GetTelRoundSaleByID((long)contract.TelRoundSaleID);
                    Telephone tel = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)rnd.TelephoneNo);
                    if (tel.RoundType == (int)DB.RoundType.Express)
                    {
                        tel.IsRound = false;
                        tel.RoundType = null;
                    }
                    else
                    {
                        rnd.SaleStatus = 0;
                        rnd.IsActive = true;
                    }


                    rnd.Detach();
                    DB.Save(rnd);

                    tel.Status = 0;
                    tel.Detach();
                    DB.Save(tel);
                }

                //delete referencedocument  
                List<UsedDocs> usedDocs = DocumentRequestTypeDB.GetUsedDocs().Where(t => t.RequestDocumentID == contract.RequestDocumentID).ToList();

                List<ReferenceDocument> r = DB.SearchByPropertyName<ReferenceDocument>("RequestDocumentID", contract.RequestDocumentID);
                ReferenceDocument refDoc = r.Where(t => t.RequestID == contract.RequestID).SingleOrDefault();

                DB.Delete<ReferenceDocument>(refDoc.ID);

                //delete contract
                DB.Delete<Contract>(contract.ID);

                //delete requestdocument                
                if (usedDocs.Count == 1)
                {
                    RequestDocument reqDoc = DB.GetEntitiesbyID<RequestDocument>(contract.RequestDocumentID).ToList()[0];
                    DB.Delete<RequestDocument>(reqDoc.ID);
                }
                ts.Complete();
            }
        }

        public static void SaveNumberStatus(RoundSaleInfo rndNo, Telephone telNo)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                DB.Save(rndNo);
                DB.Save(telNo);
                ts.Complete();
            }
        }

        public static List<RelatedContracts> GetRelatedContracts()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contracts.Join(context.RequestDocuments, c => c.RequestDocumentID, r => r.ID, (c, r) => new { doc = r, contracts = c }).Select(t => new RelatedContracts { requestDocument = t.doc, contract = t.contracts }).ToList();
            }
        }

        public static List<Contract> GetContractsByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contracts.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static Contract GetContractsByID(long contractID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Contracts.Where(t => t.ID == contractID).SingleOrDefault();
            }
        }

        public static Contract GetContractsByRequestIDAndRequestDocumentID(long requstID, long reqeustDocumentID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.Contracts.Where(t => t.RequestID == requstID && t.RequestDocumentID == reqeustDocumentID).SingleOrDefault();
            }
        }
    }

    #region Custom class
    public class RelatedContracts
    
    {
        public Contract contract{get;set;}
        //public Request request{get;set;}
        public RequestDocument requestDocument{get;set;}
    
    }
     
    #endregion Custom class

}
