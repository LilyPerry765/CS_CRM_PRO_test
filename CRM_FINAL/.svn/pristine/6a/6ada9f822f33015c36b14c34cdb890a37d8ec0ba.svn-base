using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
     public class PCMTransferFormDB
    {
        public static List<PCMCardInfo> Search(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var temp =  context.PCMs
                                .Join(context.PCMPorts, p => p.ID, pp => pp.PCMID, (p, pp) => new { PCM = p, PCMPort = pp })
                                .Join(context.Buchts, pp => pp.PCMPort.ID, b => b.PCMPortID, (pp, b) => new { PCMPort = pp, Bucht = b })
                                .GroupJoin(context.CabinetInputs, ppb => ppb.Bucht.CabinetInputID, c => c.ID, (ppb, c) => new { BuchtAndPortAndPCM = ppb, CabinetInput = c  })
                                .SelectMany(t => t.CabinetInput.DefaultIfEmpty(), (b, t) => new { BuchtAndPortAndPCM = b.BuchtAndPortAndPCM, CabinetInput = t})
                                .GroupJoin(context.PostContacts, ppb => ppb.BuchtAndPortAndPCM.Bucht.ConnectionID, pc => pc.ID, (ppb, c) => new { BuchtAndPortAndPCM = ppb, PostContact = c})
                                .SelectMany(t => t.PostContact.DefaultIfEmpty(), (b, t) => new { BuchtAndPortAndPCM = b.BuchtAndPortAndPCM, PostContact = t })
                                .Where(t => t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.Bucht.PostContact.PostID == postID && t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCMPort.PortType == (byte)DB.BuchtType.OutLine)
                    .Distinct()
                  .Select(t =>
                 new PCMCardInfo
                 {
                     IsChecked = false,
                     ID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ID,
                     CenterID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.CenterID,
                     CenterName = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.Center.CenterName,
                     RockNumber = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRock.Number,
                     ShelfNumber = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.Number,
                     Card = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.Card,
                     PCMBrandID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMBrandID,
                     PCMBrandName = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMBrand.Name,
                     PCMTypeID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMTypeID,
                     PCMTypeName = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMType.Name,
                     InstallAddress = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.InstallAddress,
                     InstallPostCode = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.InstallPostCode,
                     Status = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.Status,
                     PCMRockID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.PCMShelf.PCMRockID,
                     PCMShelfID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ShelfID,
                     CabinetNumber = t.BuchtAndPortAndPCM.CabinetInput.Cabinet.CabinetNumber,
                     PostNumber = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.Bucht.PostContact.Post.Number,
                     ConnectionNo = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.Bucht.PostContact.ConnectionNo,
                     PostContactID = t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.Bucht.PostContact.ID,
                     CustomerOfPCM = context.Buchts.Where(b => b.PCMPort.PCMID == t.BuchtAndPortAndPCM.BuchtAndPortAndPCM.PCMPort.PCM.ID && b.PCMPort.PortType == (byte)DB.BuchtType.InLine).Select(b => new SubCustomerOfPCM { Port = b.PCMPort.PortNumber, Name = context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().Customer.FirstNameOrTitle + " " + context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().Customer.LastName, Telephone = context.Telephones.Where(te => te.SwitchPortID == b.SwitchPortID).SingleOrDefault().TelephoneNo }).ToList()
                 })
                 .ToList();
                return temp;
            }
        }
    }
}
