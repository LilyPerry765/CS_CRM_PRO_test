using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using System.Xml.Linq;
using CRM.Data;
using CRM.Data.Schema;

namespace CRM.Data
{
    public static class PCMDB
    {
        public static List<PCMCardInfo> SearchPCM(
                                                    List<int> cities, List<int> centers, int rock,
                                                    int shelf, int card, List<int> pCMBrand,List<int> statues,
                                                    List<int> pCMType, string installAddress, string installPostCode,
                                                    int startRowIndex, int pageSize, DateTime? installationDateFromDate,
                                                    DateTime? installationDateToDate, out int totalRecords
                                                  )
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<PCMCardInfo> query = context.PCMs
                                                       .Where(t =>
                                                               (cities.Count == 0 || cities.Contains(t.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                                               (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMShelf.PCMRock.CenterID) : centers.Contains(t.PCMShelf.PCMRock.CenterID)) &&
                                                               (rock == -1 || rock == t.PCMShelf.PCMRock.Number) &&
                                                               (shelf == -1 || shelf == t.PCMShelf.Number) &&
                                                               (card == -1 || card == t.Card) &&
                                                               (pCMBrand.Count == 0 || pCMBrand.Contains(t.PCMBrandID)) &&
                                                               (pCMType.Count == 0 || pCMType.Contains(t.PCMTypeID)) &&
                                                               (string.IsNullOrWhiteSpace(installAddress) || t.InstallAddress.Contains(installAddress)) &&
                                                               (string.IsNullOrWhiteSpace(installPostCode) || t.InstallPostCode.Contains(installPostCode)) &&
                                                               (!installationDateFromDate.HasValue || t.InsertDate >= installationDateFromDate) &&
                                                               (!installationDateToDate.HasValue || t.InsertDate <= installationDateToDate) &&
                                                               (statues.Count==0 || statues.Contains(t.Status))
                                                             )
                                                       .OrderBy(t => t.PCMShelf.PCMRock.CenterID)
                                                       .OrderBy(t => t.PCMShelf.PCMRockID)
                                                       .OrderBy(t => t.PCMShelf.ID)
                                                       .OrderBy(t => t.Card)
                                                       .Select(t => new PCMCardInfo
                                                                        {
                                                                            ID = t.ID,
                                                                            CenterID = t.PCMShelf.PCMRock.CenterID,
                                                                            CenterName = t.PCMShelf.PCMRock.Center.CenterName,
                                                                            RockNumber = t.PCMShelf.PCMRock.Number,
                                                                            ShelfNumber = t.PCMShelf.Number,
                                                                            Card = t.Card,
                                                                            PCMBrandID = t.PCMBrandID,
                                                                            PCMTypeID = t.PCMTypeID,
                                                                            InstallAddress = t.InstallAddress,
                                                                            InstallPostCode = t.InstallPostCode,
                                                                            Status = t.Status,
                                                                            PCMRockID = t.PCMShelf.PCMRockID,
                                                                            PCMShelfID = t.ShelfID,
                                                                            InsertDate = t.InsertDate.ToPersian(Date.DateStringType.Short)
                                                                        }
                                                                );

                totalRecords = query.Count();

                return query.Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchPCMCount(
            List<int> cities,
            List<int> centers,
            int rock,
            int shelf,
            int card,
            List<int> pCMBrand,
            List<int> pCMType,
            string installAddress,
            string installPostCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs
                    .Where(t =>
                            (cities.Count == 0 || cities.Contains(t.PCMShelf.PCMRock.Center.Region.CityID)) &&
                            (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMShelf.PCMRock.CenterID) : centers.Contains(t.PCMShelf.PCMRock.CenterID)) &&
                            (rock == -1 || rock == t.PCMShelf.PCMRock.Number) &&
                            (shelf == -1 || shelf == t.PCMShelf.Number) &&
                            (card == -1 || card == t.Card) &&
                            (pCMBrand.Count == 0 || pCMBrand.Contains(t.PCMBrandID)) &&
                            (pCMType.Count == 0 || pCMType.Contains(t.PCMTypeID)) &&
                            (string.IsNullOrWhiteSpace(installAddress) || t.InstallAddress.Contains(installAddress)) &&
                            (string.IsNullOrWhiteSpace(installPostCode) || t.InstallPostCode.Contains(installPostCode))
                          ).Count();
            }
        }

        public static PCMCardInfo GetPCMCardInfoByPCMID(int PCMID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => t.ID == PCMID).Select(t =>
                 new PCMCardInfo
                 {
                     ID = t.ID,
                     CenterID = t.PCMShelf.PCMRock.CenterID,
                     RockNumber = t.PCMShelf.PCMRock.Number,
                     ShelfNumber = t.PCMShelf.Number,
                     Card = t.Card,
                     PCMBrandID = t.PCMBrandID,
                     PCMTypeID = t.PCMTypeID,
                     InstallAddress = t.InstallAddress,
                     InstallPostCode = t.InstallPostCode,
                     Status = t.Status,
                     PCMRockID = t.PCMShelf.PCMRockID,
                     PCMShelfID = t.ShelfID

                 }).SingleOrDefault();
            }
        }

        public static PCM GetPCMByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => t.ID == id).SingleOrDefault().PCMShelf.PCMRock.Center.Region.CityID;
            }
        }

        /// <summary>
        ///  وضعیت 255 برای زمانی که میخواهیم همه پی سی ام ها را داشته باشیم
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static List<CheckableItem> GetPCMCheckable(long centerID, byte status = 255)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => status == 255 || t.Status == status && t.PCMShelf.PCMRock.CenterID == centerID)
                                    .Select(t => new CheckableItem
                                    {
                                        ID = t.ID,
                                        Name = "رک :" + t.PCMShelf.PCMRock.Number.ToString() + " " + "شلف :" + t.PCMShelf.Number.ToString() + " " + "کارت :" + t.Card.ToString() + " نوع : " + t.PCMType.Name,
                                        Description = t.PCMTypeID.ToString(),
                                        IsChecked = false
                                    })
                                    .ToList();
            }
        }

        public static List<CheckableItem> GetCheckableItemPCMCardInfoByShelfID(List<int> shelfIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => shelfIDs.Contains(t.ShelfID)).OrderBy(t => t.Card).AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.Card.ToString() + DB.GetDescription(t.PCMType.Name), IsChecked = false }).ToList();

            }
        }

        public static List<PostContactBuchtPortInfo> GetAllPostContactByPCMID(int PCMID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var temp = context.PCMs
                     .Join(context.PCMPorts, p => p.ID, pp => pp.PCMID, (p, pp) => new { PCM = p, PCMPort = pp })
                                .Join(context.Buchts, pp => pp.PCMPort.ID, b => b.PCMPortID, (pp, b) => new { PCMPort = pp, Bucht = b })
                                .Join(context.PostContacts, ppb => ppb.Bucht.ConnectionID, pc => pc.ID, (ppb, pc) => new { PCMPortBucht = ppb, PostContact = pc })
                                .Where(t => t.PCMPortBucht.PCMPort.PCM.ID == PCMID)
                                .Select(t => new PostContactBuchtPortInfo { PostContact = t.PostContact, Bucht = t.PCMPortBucht.Bucht, PCMPort = t.PCMPortBucht.PCMPort.PCMPort, PCM = t.PCMPortBucht.PCMPort.PCM }).ToList();



                return temp;
            }
        }


        public static List<PCMStatisticDetails> GetEmptyPCMs(int? CenterId, int? PCMBrandID, int? RockID, int? ShelfID, int? CardID, bool InstallPCM)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                                .Where(t =>
                                    (!CenterId.HasValue ? DB.CurrentUser.CenterIDs.Contains(t.PCMPort.PCM.PCMShelf.PCMRock.CenterID) : t.PCMPort.PCM.PCMShelf.PCMRock.CenterID == (int)CenterId) &&
                                    (!RockID.HasValue || RockID == t.PCMPort.PCM.PCMShelf.PCMRockID) &&
                                    (!ShelfID.HasValue || ShelfID == t.PCMPort.PCM.ShelfID) &&
                                    (!CardID.HasValue || CardID == t.PCMPort.PCM.ID) &&
                                    ((InstallPCM && t.PCMPort.PCM.Status == (byte)DB.PCMStatus.Install) || (!InstallPCM && t.PCMPort.PCM.Status != (byte)DB.PCMStatus.Install))
                                      )
                    .Select(t =>
                 new PCMStatisticDetails
                 {
                     ConnectionIDBucht = t.Bucht1.ID.ToString(),
                     Connectionno = t.Bucht1.PostContact.ConnectionNo.ToString(),
                     ConnectionSpecification = DB.GetConnectionByBuchtID(t.ID),
                     PCMSpecification = "رک:" + t.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : "
                     + t.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : "
                     + t.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.PCMPort.PortNumber.ToString(),
                     Portno = t.PCMPort.PortNumber.ToString(),
                     Postno = t.PostContact.Post.Number.ToString(),
                     BuchtType = t.BuchtTypeID,
                     BuchtID = t.ID,
                     RockID = t.PCMPort.PCM.PCMShelf.PCMRock.ID,
                     ShelfID = t.PCMPort.PCM.PCMShelf.ID,
                     CardID = t.PCMPort.PCM.ID,
                     PortID = t.PCMPort.ID,
                     PCMType = t.PCMPort.PCM.PCMType.Name + "_" + t.PCMPort.PCM.PCMBrand.Name,
                     PCMStatus = t.PCMPort.PCM.Status.ToString()
                 })
                 .ToList();

            }

        }
        public static List<PCMStatisticDetails> GetInstallPCMs(List<int> CenterIDs, DateTime? FromDate, DateTime? ToDate)//, List<int> CenterIds, List<int> PCMBrandIDs, List<int> RockIDs, List<int> ShelfIDs, List<int> CardIDs, bool InstallPCM)
        {
            List<PCMStatisticDetails> result = new List<PCMStatisticDetails>();
            using (MainDataContext context = new MainDataContext())
            {
                List<ActionLog> ActionLogTemp = context.ActionLogs.Where(t => (t.ActionID == (int)DB.ActionLog.PCMCreate)
                                                                            && (!FromDate.HasValue || t.Date >= FromDate)
                                                                            && (!ToDate.HasValue || t.Date <= ToDate)).ToList();
                foreach (ActionLog item in ActionLogTemp)
                {
                    Schema.PCMCreate PCMCreate = LogSchemaUtility.Deserialize<CRM.Data.Schema.PCMCreate>(item.Description.ToString());
                    PCMStatisticDetails NewRecord = new PCMStatisticDetails();
                    NewRecord.PCMSpecification = "رک:" + PCMCreate.Rock + " ،  " + "شلف : "
                 + PCMCreate.Shelf + " ،  " + "کارت : "
                 + PCMCreate.Card;
                    NewRecord.InstallDate = item.Date.ToString();
                    NewRecord.RockID = int.Parse(PCMCreate.Rock);
                    NewRecord.ShelfID = int.Parse(PCMCreate.Shelf);
                    NewRecord.CardID = int.Parse(PCMCreate.Card);
                    NewRecord.PCMType = PCMCreate.Type;
                    NewRecord.CenterName = PCMCreate.Center;



                    result.Add(NewRecord);
                }
            }
            return result;
        }

        public static List<PCMStatisticDetails> GetTotalStatisticPCMPorts(DateTime? FromDate, DateTime? ToDate, int? CenterId, int? PCMBrandID, int? RockID, int? ShelfID, int? CardID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                                .Where(t => (!FromDate.HasValue || t.PCMPort.PCM.InsertDate >= FromDate) &&
                                (!ToDate.HasValue || t.PCMPort.PCM.InsertDate <= ToDate) &&
                                (!CenterId.HasValue ? DB.CurrentUser.CenterIDs.Contains(t.PCMPort.PCM.PCMShelf.PCMRock.CenterID) : t.PCMPort.PCM.PCMShelf.PCMRock.CenterID == (int)CenterId) &&
                                (!RockID.HasValue || RockID == t.PCMPort.PCM.PCMShelf.PCMRockID) &&
                                (!ShelfID.HasValue || ShelfID == t.PCMPort.PCM.ShelfID) &&
                                (!CardID.HasValue || CardID == t.PCMPort.PCM.ID) &&
                                (t.BuchtTypeID != (int)DB.BuchtType.OutLine))
                    .Select(t =>
                 new PCMStatisticDetails
                 {
                     ConnectionIDBucht = t.Bucht1.ID.ToString(),
                     Connectionno = t.Bucht1.PostContact.ConnectionNo.ToString(),
                     SwitchPortID = (int)t.SwitchPortID,
                     PCMSpecification = "رک:" + t.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : "
                     + t.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : "
                     + t.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.PCMPort.PortNumber.ToString(),
                     Portno = t.PCMPort.PortNumber.ToString(),
                     Postno = t.PostContact.Post.Number.ToString(),
                     BuchtType = t.BuchtTypeID,
                     BuchtID = t.ID,
                     RockID = t.PCMPort.PCM.PCMShelf.PCMRock.ID,
                     ShelfID = t.PCMPort.PCM.PCMShelf.ID,
                     CardID = t.PCMPort.PCM.ID,
                     PortID = t.PCMPort.ID,
                     PCMType = t.PCMPort.PCM.PCMType.Name + "_" + t.PCMPort.PCM.PCMBrand.Name,
                     PCMStatus = t.PCMPort.PCM.Status.ToString(),
                     ConnectionSpecification = DB.GetConnectionWithPortByBuchtID(t.ID, t.PCMPort.PortNumber),
                     InsertDate = t.PCMPort.PCM.InsertDate
                 })
                 .ToList();

            }
        }



        public static List<PCM> GetPCMByIDs(List<int> IDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => IDs.Contains(t.ID)).ToList();
            }
        }

        public static PCM GetPCMByPortID(int PortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => context.PCMPorts.Where(t2 => t2.ID == PortID).SingleOrDefault().PCMID == t.ID).SingleOrDefault();
            }

        }

        public static byte GetLastSatausOfMalfaction(int pcmID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Any(t => t.PCMPort.PCMID == pcmID))
                {
                    return (byte)DB.PCMStatus.Connection;
                }
                else
                {
                    return (byte)DB.PCMStatus.Install;
                }
            }
        }

        public static PCM GetPCMByNumber(int shelfID, int card)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => t.ShelfID == shelfID && t.Card == card).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetPCMByShelfID(int shelfID, int pcmStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs
                              .Where(t => t.ShelfID == shelfID && t.Status == pcmStatus)
                              .OrderBy(t => t.Card)
                              .Select(t => new CheckableItem { ID = t.ID, Name = t.Card.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<PCMBuchtTelephonInfo> GetAllTelephoneByPCMID(int PCMID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var temp = context.PCMs
                                  .Join(context.PCMPorts, p => p.ID, pp => pp.PCMID, (p, pp) => new { PCM = p, PCMPort = pp })
                                  .Join(context.Buchts, pp => pp.PCMPort.ID, b => b.PCMPortID, (pp, b) => new { PCMPort = pp, Bucht = b })
                                  .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
                                  .SelectMany(t => t.Telephone.DefaultIfEmpty(), (x, t) => new { Bucht = x.Bucht, Telephone = t })
                                  .GroupJoin(context.PostContacts, b => b.Bucht.Bucht.ConnectionID, t => t.ID, (b, t) => new { Bucht = b.Bucht, Telephone = b.Telephone, PostContact = t })
                                  .SelectMany(t => t.PostContact.DefaultIfEmpty(), (b, t) => new { Bucht = b.Bucht, Telephone = b.Telephone, PostContact = t })
                                  .Where(t => t.Bucht.PCMPort.PCM.ID == PCMID).OrderBy(t => t.Bucht.Bucht.BuchtNo)
                                  .Select(t => new PCMBuchtTelephonInfo { Telephone = t.Telephone, Bucht = t.Bucht.Bucht, PCMID = t.Bucht.PCMPort.PCM.ID, PostContact = t.PostContact, PortNo = t.Bucht.Bucht.PCMPort.PortNumber });


                return temp.ToList();
            }
        }

        public static string GetMUIDByPCMID(int pcmID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => t.ID == pcmID).Select(t => "رک:" + t.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.Card.ToString()).SingleOrDefault();
            }
        }
    }
}