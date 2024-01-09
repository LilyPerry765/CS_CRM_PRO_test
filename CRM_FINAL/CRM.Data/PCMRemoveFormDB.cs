using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PCMRemoveFormDB
    {
        public static List<PCMCardInfo> Search
            (
             List<int> centers,
             List<int> cites,
             List<int> rocks,
             List<int> shelfs,
             List<int> cards,
             int startRowIndex,
             int pageSize
            )
        {

            using (MainDataContext context = new MainDataContext())
            {
                //return context.PCMs.Join(context.PCMPorts, p => p.ID, pp => pp.PCMID, (p, pp) => new { PCM = p, PCMPort = pp })
                //                 .Join(context.Buchts, pp => pp.PCMPort.ID, b => b.PCMPortID, (pp, b) => new { PCMPort = pp, Bucht = b })
                //                 .GroupJoin(context.CabinetInputs, ppb => ppb.Bucht.CabinetInputID, c => c.ID, (ppb, c) => new { BuchtAndPortAndPCM = ppb, CabinetInput = c })
                //                 .SelectMany(t => t.CabinetInput.DefaultIfEmpty(), (b, t) => new { BuchtAndPortAndPCM = b.BuchtAndPortAndPCM, CabinetInput = t })
                //                 .GroupJoin(context.PostContacts, ppb => ppb.BuchtAndPortAndPCM.Bucht.ConnectionID, pc => pc.ID, (ppb, c) => new { BuchtAndPortAndPCM = ppb, PostContact = c })
                //                 .SelectMany(t => t.PostContact.DefaultIfEmpty(), (b, t) => new { BuchtAndPortAndPCM = b.BuchtAndPortAndPCM, PostContact = t })
                //     .Where(t => context.PCMPorts.GroupBy(x => new { PCMID = x.PCMID, PCMPortType = x.PortType, PCMPortStatus = x.Status })
                //                                 .Where(x2 => x2.Key.PCMPortType == (byte)DB.BuchtType.InLine
                //                                        && x2.Key.PCMPortStatus ==  (byte)DB.PCMPortStatus.Connection
                //                                        && x2.Count() == 1)
                //                                .Select(x3 => x3.Key.PCMID)
                //                                  .Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ID)
                //                                && t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCMPort.PortType == (byte)DB.BuchtType.OutLine
                //                                   && DB.CurrentUser.CenterIDs.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.CenterID))
                //                 .Where(t => (cites.Count == 0 || cites.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.Center.Region.CityID)) &&
                //                        (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.CenterID) : centers.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.CenterID)) &&
                //                        (rocks.Count == 0 || rocks.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRockID)) &&
                //                        (shelfs.Count == 0 || shelfs.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ShelfID)) &&
                //                        (cards.Count == 0 || cards.Contains(t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ID)))

                return context.PCMs.Where(t => (cites.Count == 0 || cites.Contains(t.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                        (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMShelf.PCMRock.CenterID) : centers.Contains(t.PCMShelf.PCMRock.CenterID)) &&
                                        (rocks.Count == 0 || rocks.Contains(t.PCMShelf.PCMRockID)) &&
                                        (shelfs.Count == 0 || shelfs.Contains(t.ShelfID)) &&
                                        (cards.Count == 0 || cards.Contains(t.ID)))
                    .Select(t =>
                 new PCMCardInfo
                 {
                     IsChecked = false,
                     ID = t.ID,
                     CenterID = t.PCMShelf.PCMRock.CenterID,
                     CenterName = t.PCMShelf.PCMRock.Center.CenterName,
                     RockNumber = t.PCMShelf.PCMRock.Number,
                     ShelfNumber = t.PCMShelf.Number,
                     Card = t.Card,
                     PCMBrandID = t.PCMBrandID,
                     PCMTypeID = t.PCMTypeID,
                     PCMBrandName = t.PCMBrand.Name,
                     PCMTypeName = t.PCMType.Name,
                     InstallAddress = t.InstallAddress,
                     InstallPostCode = t.InstallPostCode,
                     Status = t.Status,
                     PCMRockID = t.PCMShelf.PCMRockID,
                     PCMShelfID = t.ShelfID,
                     PCMCabinetInputColumnNo = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(x => x.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo).SingleOrDefault(),
                     PCMCabinetInputRowNo = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(x => x.Bucht1.VerticalMDFRow.VerticalRowNo).SingleOrDefault(),
                     PCMCabinetInputBuchtNo = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(x => x.Bucht1.BuchtNo).SingleOrDefault(),
                     // CabinetNumber = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Take(1).SingleOrDefault().CabinetInput.Cabinet.CabinetNumber,
                     // PostNumber = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Take(1).SingleOrDefault().PostContact.Post.Number,
                     // ConnectionNo = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Take(1).SingleOrDefault().PostContact.ConnectionNo,
                     CustomerOfPCM = context.Buchts.Where(b => b.PCMPort.PCMID == t.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine)
                                    .Select(b => new SubCustomerOfPCM
                                    {
                                        Port = b.PCMPort.PortNumber,
                                        Name = context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().Customer.FirstNameOrTitle.ToString() + " " + (context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().Customer.LastName ?? ""),
                                        ColumnNo = b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                        RowNo = b.VerticalMDFRow.VerticalRowNo,
                                        BuchtNo = b.BuchtNo,
                                        Telephone = context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().TelephoneNo
                                    }).ToList(),
                 }).Skip(startRowIndex).Take(pageSize).ToList();

            }
        }

        public static int SearchCount(
            List<int> centers,
             List<int> cites,
             List<int> rocks,
             List<int> shelfs,
            List<int> cards)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMs.Where(t => (cites.Count == 0 || cites.Contains(t.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                        (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.PCMShelf.PCMRock.CenterID) : centers.Contains(t.PCMShelf.PCMRock.CenterID)) &&
                                        (rocks.Count == 0 || rocks.Contains(t.PCMShelf.PCMRockID)) &&
                                        (shelfs.Count == 0 || shelfs.Contains(t.ShelfID)) &&
                                        (cards.Count == 0 || cards.Contains(t.ID))).Count();

            }
        }


        public static List<PCMStatisticsInfo> SearchPCMStatistics(List<int> cities, List<int> centers, List<int> rocks, List<int> shelfs, List<int> cards, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<PCMStatisticsInfo> finalResult = new List<PCMStatisticsInfo>();
                var query = context.PCMs
                                   .GroupJoin(context.PCMPorts, pc => pc.ID, pp => pp.PCMID, (pc, pps) => new { Pcm = pc, PcmPorts = pps })
                                   .SelectMany(a => a.PcmPorts.DefaultIfEmpty(), (a, pp) => new { Pcm = a.Pcm, PcmPort = pp })
                                   .Where(pc =>
                                              (cities.Count == 0 || cities.Contains(pc.Pcm.PCMShelf.PCMRock.Center.Region.CityID)) &&
                                              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(pc.Pcm.PCMShelf.PCMRock.CenterID) : centers.Contains(pc.Pcm.PCMShelf.PCMRock.CenterID)) &&
                                              (rocks.Count == 0 || rocks.Contains(pc.Pcm.PCMShelf.PCMRockID)) &&
                                              (shelfs.Count == 0 || shelfs.Contains(pc.Pcm.ShelfID)) &&
                                              (cards.Count == 0 || cards.Contains(pc.Pcm.ID))
                                         )
                                   .AsQueryable();
                var secondQuery = query.Select(pc => new PCMStatisticsInfo
                {
                    ID = pc.PcmPort.PCMID,
                    CenterName = pc.PcmPort.PCM.PCMShelf.PCMRock.Center.CenterName,
                    RockNumber = pc.PcmPort.PCM.PCMShelf.PCMRock.Number,
                    ShelfNumber = pc.PcmPort.PCM.PCMShelf.Number,
                    Card = pc.PcmPort.PCM.Card,
                    PCMBrandID = pc.PcmPort.PCM.PCMBrandID,
                    PCMTypeID = pc.PcmPort.PCM.PCMTypeID,
                    PCMBrandName = pc.PcmPort.PCM.PCMBrand.Name,
                    PCMTypeName = pc.PcmPort.PCM.PCMType.Name,
                    InstallAddress = context.Telephones
                                            .Where(t =>
                                                     t.SwitchPortID == (context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.SwitchPortID).FirstOrDefault())
                                                  )
                                            .SingleOrDefault().Address.AddressContent,
                    InstallPostalCode = context.Telephones
                                            .Where(t =>
                                                     t.SwitchPortID == (context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.SwitchPortID).FirstOrDefault())
                                                  )
                                            .SingleOrDefault().Address.PostalCode,
                    Status = pc.PcmPort.PCM.Status,
                    PCMRockID = pc.PcmPort.PCM.PCMShelf.PCMRockID,
                    PCMShelfID = pc.PcmPort.PCM.ShelfID,
                    PCMCabinetInputColumnNo = context.Buchts.Where(bu => bu.PCMPort.PCMID == pc.Pcm.ID && bu.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(bu => bu.Bucht1.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo).SingleOrDefault(),
                    PCMCabinetInputRowNo = context.Buchts.Where(bu => bu.PCMPort.PCMID == pc.Pcm.ID && bu.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(bu => bu.Bucht1.VerticalMDFRow.VerticalRowNo).SingleOrDefault(),
                    PCMCabinetInputBuchtNo = context.Buchts.Where(bu => bu.PCMPort.PCMID == pc.Pcm.ID && bu.PCMPort.PortType == (byte)DB.BuchtType.OutLine).Take(1).Select(bu => bu.Bucht1.BuchtNo).SingleOrDefault(),
                    Port = context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.PCMPort.PortNumber).FirstOrDefault(),
                    ColumnNo = context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo).FirstOrDefault(),
                    RowNo = context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.VerticalMDFRow.VerticalRowNo).FirstOrDefault(),
                    BuchtNo = context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.BuchtNo).FirstOrDefault(),
                    TelephoneNo = context.Telephones
                                         .Where(t =>
                                                  t.SwitchPortID == (context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.SwitchPortID).FirstOrDefault())
                                               )
                                         .SingleOrDefault()
                                         .TelephoneNo,
                    CustomerName = context.Telephones
                                         .Where(t =>
                                                  t.SwitchPortID == (context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.SwitchPortID).FirstOrDefault())
                                               )
                                         .SingleOrDefault()
                                         .Customer.FirstNameOrTitle
                                         + " " +
                                         context.Telephones
                                         .Where(t =>
                                                  t.SwitchPortID == (context.Buchts.Where(b => b.PCMPortID == pc.PcmPort.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => b.SwitchPortID).FirstOrDefault())
                                               )
                                         .SingleOrDefault()
                                         .Customer.LastName
                }
                                          )
                                          .AsQueryable();
                count = query.Count();
                if (pageSize != 0)
                {
                    finalResult = secondQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }
                else
                {
                    finalResult = secondQuery.ToList();
                }

                return finalResult;
            }
        }
    }
}
