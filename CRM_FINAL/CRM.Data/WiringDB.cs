using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class WiringDB
    {
        //public static WiringDetailInfo GetInfoWiringRequest(long investigateID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.InvestigatePossibilities.Where(i => i.ID == investigateID)
        //               .Select(i => new WiringDetailInfo
        //               {
        //                   Investigate = i.InvestigatePossibilities.Select(j => j).SingleOrDefault(),
        //                   Visit = i.VisitAddresses.Select(t => t).SingleOrDefault(),
        //                   Address = i.CustomerAddress1,
        //                   request = context.Requests.Where(r => r.ID == i.CustomerAddress1.RequestID).SingleOrDefault(),
        //                   Installrequest = context.InstallRequests.Where(inst => inst.ID == i.CustomerAddress1.RequestID).SingleOrDefault(),
        //                   customer = context.Requests.Where(r => r.ID == i.CustomerAddress1.RequestID).Select(r => r.Customer).SingleOrDefault(),

        //               }
        //    ).SingleOrDefault();
        //    }
        //}

        public static void SaveWiringIssue(Request request, IssueWiring issueWiring, Wiring wiring, bool mode)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                request.Detach();
                DB.Save(request);

                if (mode == true)
                    issueWiring.Detach();

                DB.Save(issueWiring);
                wiring.IssueWiringID = issueWiring.ID;
                wiring.Detach();
                DB.Save(wiring);
                ts.Complete();
            }
        }

        public static void SaveNetWiring(Wiring wiring, long connectionID, Request request, AssignmentInfo assignmentInfo)
        {
            using (TransactionScope ts = new TransactionScope())
            {

                request.Detach();
                DB.Save(request);

              
                    PostContact contact = Data.PostContactDB.GetPostContactByID( connectionID);
                    contact.Status = (byte)DB.PostContactStatus.CableConnection;
                    contact.Detach();
                    DB.Save(contact);
                    wiring.ConnectionID = contact.ID;

                    if (contact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || contact.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
                    {
                        PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID((long)assignmentInfo.PCMPortIDInBuchtTable);
                        PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                        PCMPort.Detach();
                        DB.Save(PCMPort);

                     }


                if (wiring.ResolveObstacleDate.HasValue == false)
                    wiring.ResolveObstacleDate = DB.GetServerDate();
                if (string.IsNullOrEmpty(wiring.ResolveObstacleHour))
                    wiring.ResolveObstacleHour = string.Empty;
                wiring.Detach();
                DB.Save(wiring);
                // SaveWiringNet(wiring);
                ts.Complete();
            }
        }

        public static void SaveMDFWiring(Wiring wiring, Bucht bucht, byte sourceType , Request request)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {

                
                //  SaveWiringMDF(wiring, bucht);
                ts.Complete();
            }
        }

        //public static void SaveWiringMDF(Wiring wiring, Bucht bucht)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        context.ExecuteCommand("UPDATE Wiring SET MDFWiringDate = {0},MDFWiringHour={1},BuchtID={2},BuchtType={3},MDFComment={4},MDFInsertDate={5},MDFStatus={6} WHERE ID = {7}",
        //                                                  wiring.MDFWiringDate, wiring.MDFWiringHour, bucht.ID, bucht.BuchtType, wiring.MDFComment, wiring.MDFInsertDate, wiring.MDFStatus, wiring.ID);
        //        context.SubmitChanges();
        //    }
        //}

        //public static void SaveWiringNet(Wiring wiring)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {

        //        context.ExecuteCommand("UPDATE Wiring SET WiringDate = {0}  ,WiringHour={1}   ,ConnectionID={2}    ,ConnectionType={3}     ,WiringComment={4}   ,WiringInsertDate={5}   ,WiringStatus={6}     ,ResolveObstacleDate={7}   ,ResolveObstacleHour={8}  ,IssueWiringID={9}   WHERE ID = {10}",
        //                                                  wiring.WiringDate, wiring.WiringHour, wiring.ConnectionID, wiring.ConnectionType, wiring.WiringComment, wiring.WiringInsertDate, wiring.WiringStatus, wiring.ResolveObstacleDate, wiring.ResolveObstacleHour, wiring.IssueWiringID, wiring.ID);
        //        context.SubmitChanges();
        //    }
        //}

        public static Wiring GetInfoWiringByInvestigatePossibility(long investigatePossibilityID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Wirings.Where(t => t.InvestigatePossibilityID == investigatePossibilityID).OrderByDescending(t=>t.ID).FirstOrDefault();
                // return context.IssueWirings.Join(context.Wirings, iw => iw.ID, w => w.IssueWiringID, (iw, w) => new { IssueWirings = iw, Wirings = w }).Where(t => t.Wirings.InvestigatePossibilityID == investigatePossibilityID).Select(x => x.IssueWirings).SingleOrDefault();
            }
        }


        public static void SaveWiringIssue(Request request, IssueWiring issueWiring)
        {



        }

        public static void SaveWiringIssue(Request request, IssueWiring issueWiring, Wiring wiring)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                request.Detach();
                DB.Save(request);

                issueWiring.Detach();
                DB.Save(issueWiring);

                wiring.IssueWiringID = issueWiring.ID;
                wiring.Detach();

                DB.Save(wiring);
                ts.Complete();
            }

        }

        public static Wiring GetWiringByIssueWiringID(long issueWiringID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Wirings.Where(t => t.IssueWiringID == issueWiringID).OrderByDescending(t=>t.ID).FirstOrDefault();
            }
        }

        public static void DeleteWiringByissueWiringID(long issueWiringID)
        {
           using(MainDataContext context = new MainDataContext())
           {
               var wirings = context.Wirings.Where(w => w.IssueWiringID == issueWiringID);
               context.Wirings.DeleteAllOnSubmit(wirings);
               context.SubmitChanges();
           }
        }
    }




    #region CustomClass WiringDetailInfo

    public class WiringDetailInfo
    {
        public InvestigatePossibility Investigate { get; set; }
        public VisitAddress Visit { get; set; }
        public Customer customer { get; set; }
        public Address Address { get; set; }
        public Request request { get; set; }
        public InstallRequest Installrequest { get; set; }

    }

    #endregion
}


//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Transactions;

//////namespace CRM.Data
//////{
//////    public static class WiringDB
//////    {
//////        public static WiringDetailInfo GetInfoWiringRequest(long investigateID)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.InvestigatePossibilities.Where(i => i.ID == investigateID)

//////                       .Select(i => new WiringDetailInfo
//////                       {
//////                           Investigate = i.InvestigatePossibilities.Select(j => j).SingleOrDefault(),
//////                           Visit = i.VisitAddresses.Select(t => t).SingleOrDefault(),
//////                           Address = i.CustomerAddress,
//////                           request = context.Requests.Where(r => r.ID == i.CustomerAddress.RequestID).SingleOrDefault(),
//////                           Installrequest = context.InstallRequests.Where(inst => inst.ID == i.CustomerAddress.RequestID).SingleOrDefault(),
//////                           customer = context.Requests.Where(r => r.ID == i.CustomerAddress.RequestID).Select(r => r.Customer).SingleOrDefault(),

//////                       }
//////            ).SingleOrDefault();
//////            }
//////        }

//////        public static void SaveWiringIssue(Request request, Wiring wiring,bool mode)
//////        {
//////            using (TransactionScope ts = new TransactionScope())
//////            {
//////                request.Detach();
//////                DB.Save(request);

//////                if (mode == true)
//////                    wiring.Detach();

//////                DB.Save(wiring);
//////                ts.Complete();
//////            }
//////        }

//////        public static void SaveNetWiring(Wiring wiring, long connectionID, byte sourceType)
//////        {
//////            using (TransactionScope ts = new TransactionScope())
//////            {
//////                if (sourceType == (byte)DB.SourceType.Post || sourceType == (byte)DB.SourceType.PCM)
//////                {
//////                    PostContact contact = Data.PostContactDB.GetPostContactByID( connectionID).Take(1).SingleOrDefault();
//////                    contact.Status = 2;
//////                    contact.Detach();
//////                    DB.Save(contact);
//////                    wiring.ConnectionID = contact.ID;
//////                    wiring.ConnectionType = sourceType;
//////                }
//////                if (wiring.ResolveObstacleDate.HasValue == false)
//////                    wiring.ResolveObstacleDate = DB.GetServerDate();
//////                if (string.IsNullOrEmpty(wiring.ResolveObstacleHour))
//////                    wiring.ResolveObstacleHour =string.Empty;
//////                SaveWiringNet(wiring);
//////                ts.Complete();
//////            }
//////        }

//////        public static void SaveMDFWiring(Wiring wiring,Bucht bucht, byte sourceType)
//////        {
//////            using (TransactionScope ts = new TransactionScope())
//////            {
//////                    bucht.Status = 1;
//////                    bucht.Detach();
//////                    DB.Save(bucht);
//////                    SaveWiringMDF(wiring, bucht);      
//////                    ts.Complete();
//////            }
//////        }

//////        public static void SaveWiringMDF(Wiring wiring, Bucht bucht)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                context.ExecuteCommand("UPDATE Wiring SET MDFWiringDate = {0},MDFWiringHour={1},BuchtID={2},BuchtType={3},MDFComment={4},MDFInsertDate={5},MDFStatus={6} WHERE ID = {7}",
//////                                                          wiring.MDFWiringDate, wiring.MDFWiringHour, bucht.ID, bucht.BuchtType, wiring.MDFComment, wiring.MDFInsertDate, wiring.MDFStatus, wiring.ID);
//////                context.SubmitChanges();
//////            }
//////        }

//////        public static void SaveWiringNet(Wiring wiring)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                context.ExecuteCommand("UPDATE Wiring SET WiringDate = {0}  ,WiringHour={1}   ,ConnectionID={2}    ,ConnectionType={3}     ,WiringComment={4}   ,WiringInsertDate={5}   ,WiringStatus={6}     ,ResolveObstacleDate={7}   ,ResolveObstacleHour={8}    WHERE ID = {9}",
//////                                                          wiring.WiringDate, wiring.WiringHour, wiring.ConnectionID, wiring.ConnectionType, wiring.WiringComment, wiring.WiringInsertDate, wiring.WiringStatus,wiring.ResolveObstacleDate,wiring.ResolveObstacleHour, wiring.ID);
//////                context.SubmitChanges();
//////            }
//////        }

//////        public static Wiring GetInfoWiringByInvestigatePossibility(long investigatePossibilityID)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Wirings.Where(t => t.InvestigatePossibilityID == investigatePossibilityID).SingleOrDefault();
//////            }
//////        }

//////    }




//////    #region CustomClass WiringDetailInfo

//////    public class WiringDetailInfo
//////    {
//////        public InvestigatePossibility Investigate { get; set; }
//////        public VisitAddress Visit { get; set; }
//////        public Customer customer { get; set; }
//////        public CustomerAddress Address { get; set; }
//////        public Request request { get; set; }
//////        public InstallRequest Installrequest { get; set; }

//////    }

//////    #endregion
//////}
