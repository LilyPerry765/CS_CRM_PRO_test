using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public class AssignmentDB
    {


        public static List<NearestTelephonInfo> GetNearestTelephonInfo(long? telePhone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<NearestTelephonInfo> query = context.Buchts
                     .Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, tel = t })
                    .Where(x => x.tel.TelephoneNo == telePhone)
                     .Select(t => new NearestTelephonInfo
                     {
                         PostID = t.bucht.PostContact.Post.ID,
                         CabinetID = t.bucht.CabinetInput.CabinetID,
                         CabinetInputID = t.bucht.CabinetInputID,
                         ConnectionNo = t.bucht.PostContact.ConnectionNo
                     });
                return query.ToList();
            }
        }

        public static List<VisitPlacesCabinetAndPostClass> GetNearestTelephonVisitPlacesCabinetAndPostClass(long? telePhone, int proposedFacilityType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<VisitPlacesCabinetAndPostClass> query = context.Buchts
                     .Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, tel = t })
                    .Where(x => x.tel.TelephoneNo == telePhone)
                     .Select(t => new VisitPlacesCabinetAndPostClass
                     {
                         PostNumber = t.bucht.PostContact.Post.Number.ToString(),
                         CabinetNumber = t.bucht.CabinetInput.Cabinet.CabinetNumber.ToString(),
                         CabinetInput = t.bucht.CabinetInput.InputNumber.ToString(),
                         ConnectionNo = t.bucht.PostContact.ConnectionNo,
                         TelephonNo = t.tel.TelephoneNo,
                         ProposedFacilityType = proposedFacilityType,
                         Center = t.tel.Center.CenterName,
                         IsADSL = (t.tel.TelephoneNo != null ? context.ADSLPAPPorts.Any(t2 => t2.TelephoneNo == t.tel.TelephoneNo) : false)
                     });
                return query.ToList();
            }
        }

        public static List<NearestTelephonInfo> GetTelephonInfoByBuchtID(long? buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<NearestTelephonInfo> query = context.Buchts
                    .Where(x => x.ID == buchtID)
                     .Select(t => new NearestTelephonInfo
                     {
                         PostID = t.PostContact.Post.ID,
                         PostNumber = t.PostContact.Post.Number.ToString(),
                         ConnectionNo = t.PostContact.ConnectionNo,
                         PostContactID = t.PostContact.ID,

                         CabinetID = t.CabinetInput.CabinetID,
                         CabinetNumber = t.CabinetInput.Cabinet.CabinetNumber.ToString(),
                         CabinetInputID = t.CabinetInputID,
                         CabinetInputNumber = t.CabinetInput.InputNumber.ToString()

                     });
                return query.ToList();
            }
        }





        public class NearestTelephonInfo
        {
            public int? LinemanID { get; set; }
            public long ChangeLocationID { get; set; }
            public int? CabinetID { get; set; }
            public long? PostContactID { get; set; }
            public string CabinetNumber { get; set; }
            public string PostNumber { get; set; }

            public long? CabinetInputID { get; set; }
            public string CabinetInputNumber { get; set; }

            public int? PostID { get; set; }
            public int? ConnectionNo { get; set; }
        }

        public static void Save(AssignmentInfo assignmentInfo, ChangeLocation changeLocation, Request request, Bucht bucht, InvestigatePossibility investigate)
        {
            request.Detach();
            DB.Save(request);

            if (assignmentInfo.BuchtType == (byte)DB.BuchtType.OutLine || assignmentInfo.BuchtType == (byte)DB.BuchtType.InLine)
            {


                //  reserve post contact in pcm
                PostContact Contact = Data.PostContactDB.GetPostContactByID((long)assignmentInfo.PostContactID);

                if (Contact != null && bucht != null)
                {
                    Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                    Contact.Detach();
                    DB.Save(Contact);


                    bucht.Status = (byte)DB.BuchtStatus.Reserve;
                    bucht.Detach();
                    DB.Save(bucht);
                }
            }

            else
            {

                // reserve post contact in not pcm

                PostContact Contact = Data.PostContactDB.GetPostContactByID((long)assignmentInfo.PostContactID);
                if (Contact != null && bucht != null)
                {
                    Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                    Contact.Detach();
                    DB.Save(Contact);

                    // TODO: 
                    bucht.Status = (byte)DB.BuchtStatus.Reserve;
                    bucht.ConnectionID = assignmentInfo.PostContactID;
                    bucht.Detach();
                    DB.Save(bucht);

                }
            }

            changeLocation.Detach();
            DB.Save(changeLocation , false);

            investigate.Detach();
            DB.Save(investigate);

        }

        public static void Updata(AssignmentInfo pastAssingmentInfo, AssignmentInfo connection, ChangeLocation changeLocation, Request request, byte passChangeLocationType, InvestigatePossibility investigate)
        {

            request.Detach();
            DB.Save(request);

            if (pastAssingmentInfo.BuchtType == (byte)DB.BuchtType.OutLine || pastAssingmentInfo.BuchtType == (byte)DB.BuchtType.InLine)
            {

                    Bucht oldbucht = Data.BuchtDB.GetBuchetByID(pastAssingmentInfo.BuchtID);
                    oldbucht.Status = (byte)DB.BuchtStatus.Free;
                    oldbucht.Detach();
                    DB.Save(oldbucht);
                

                PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)oldbucht.ConnectionID);
                oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                oldPostContact.Detach();
                DB.Save(oldPostContact);
            }
            else
            {
                // if change location in itselt Should not change bucht

                if (passChangeLocationType != (byte)DB.ChangeLocationCenterType.itself)
                {
                    Bucht oldbucht = Data.BuchtDB.GetBuchetByID(pastAssingmentInfo.BuchtID);
                    oldbucht.Status = (byte)DB.BuchtStatus.Free;
                    oldbucht.ConnectionID = null;
                    oldbucht.Detach();
                    DB.Save(oldbucht);

                    PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)pastAssingmentInfo.PostContactID);
                    oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                    oldPostContact.Detach();
                    DB.Save(oldPostContact);
                }
                else if (passChangeLocationType == (byte)DB.ChangeLocationCenterType.itself && investigate.PostContactID != changeLocation.OldPostContactID)
                {
                    PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)pastAssingmentInfo.PostContactID);
                    oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                    oldPostContact.Detach();
                    DB.Save(oldPostContact);
                }


            }
            Save(connection, changeLocation, request, Data.BuchtDB.GetBuchetByID(connection.BuchtID) ,investigate );
        }
        public static AssignmentInfo SearchByBuchtID(long? buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IEnumerable<AssignmentInfo> query = context.Buchts
                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, tel = t })
                    .SelectMany(x => x.tel.DefaultIfEmpty(), (t, x) => new { tel = x, bucht = t.bucht })
                    .Where(x => x.bucht.ID == buchtID)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.bucht.BuchtTypeID,
                         MUID = t.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + "-" + t.bucht.PCMPort.PCM.PCMShelf.Number.ToString() + "-" + t.bucht.PCMPort.PCM.Card.ToString(),
                         InputNumber = t.bucht.CabinetInput.InputNumber,
                         InputNumberID = t.bucht.CabinetInput.ID,
                         PostContact = t.bucht.PostContact.ConnectionNo,
                         PostContactID = t.bucht.PostContact.ID,
                         Connection = t.bucht.VerticalMDFRow.VerticalRowNo + " - " + t.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " - " + t.bucht.BuchtNo,
                         BuchtID = t.bucht.ID,
                         PortNo = t.bucht.SwitchPort.PortNo,
                         SwitchPortID = t.bucht.SwitchPort.ID,
                         SwitchCode = t.bucht.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.bucht.SwitchPort.Switch.ID,
                         TelePhoneNo = t.tel.TelephoneNo,
                         PostID = t.bucht.PostContact.Post.ID,
                         CabinetID = t.bucht.CabinetInput.CabinetID
                     });
                return query.SingleOrDefault();
            }
        }
    }
}
