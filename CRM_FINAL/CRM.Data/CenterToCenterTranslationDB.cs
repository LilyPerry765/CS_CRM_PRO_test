using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class CenterToCenterTranslationDB
    {

        public static int SearchCenterToCenterTranslationCount
            (
                List<int> Cites,
                List<int> centers,
                string oldCabinet,
                string newCabinet,
                DateTime? accomplishmentDateDate,
                string requestLetterNo,
                string accomplishmentTime
            )
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslations.Where(t =>
                                         (Cites.Count == 0 || Cites.Contains(t.Cabinet.Center.Region.CityID)) &&
                                         (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : centers.Contains(t.Cabinet.CenterID)) &&
                                         (string.IsNullOrEmpty(oldCabinet) || t.Cabinet.CabinetNumber.ToString().Contains(oldCabinet)) &&
                                         (string.IsNullOrEmpty(newCabinet) || t.Cabinet1.CabinetNumber.ToString().Contains(newCabinet)) &&
                                         (!accomplishmentDateDate.HasValue || t.AccomplishmentDate == accomplishmentDateDate) &&
                                         (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo) &&
                                         (string.IsNullOrEmpty(accomplishmentTime) || t.AccomplishmentTime == accomplishmentTime)
                                         ).Count();
            }
        }

        public static List<CenterToCenterTranslationInfo> SearchCenterToCenterTranslation
    (
        List<int> Cites,
        List<int> centers,
        string oldCabinet,
        string newCabinet,
        DateTime? accomplishmentDateDate,
        string requestLetterNo,
        string accomplishmentTime,
        int startRowIndex,
        int pageSize
    )
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslations.Where(t =>
                                         (Cites.Count == 0 || Cites.Contains(t.Cabinet.Center.Region.CityID)) &&
                                         (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : centers.Contains(t.Cabinet.CenterID)) &&
                                         (string.IsNullOrEmpty(oldCabinet) || t.Cabinet.CabinetNumber.ToString().Contains(oldCabinet)) &&
                                         (string.IsNullOrEmpty(newCabinet) || t.Cabinet1.CabinetNumber.ToString().Contains(newCabinet)) &&
                                         (!accomplishmentDateDate.HasValue || t.AccomplishmentDate == accomplishmentDateDate) &&
                                         (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo) &&
                                         (string.IsNullOrEmpty(accomplishmentTime) || t.AccomplishmentTime == accomplishmentTime)
                                         ).Select(t => new CenterToCenterTranslationInfo
                                                 {
                                                     ID = t.ID,
                                                     AccomplishmentDate = t.AccomplishmentDate,
                                                     NewCabinetID = t.NewCabinetID,
                                                     OldCabinetID = t.OldCabinetID,
                                                     OldCabinetName = t.Cabinet.CabinetNumber.ToString(),
                                                     NewCabinetName = t.Cabinet1.CabinetNumber.ToString(),
                                                     TargetCenterName = t.Center.CenterName,
                                                     SourceCenterName = t.Center1.CenterName
                                                 }
                ).ToList();
            }
        }

        public static CenterToCenterTranslation GetCenterToCenterTranslation(long ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslations.Where(t => t.ID == ID).SingleOrDefault();
            }
        }

        public static CenterToCenterTranslationUserControlInfo GetCenterToCenterTranslationInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslations.Where(t => t.ID == requestID).Select(t => new CenterToCenterTranslationUserControlInfo
                       {
                           SourceCenter = t.Center1.CenterName,
                           TargetCenter = t.Center.CenterName,

                           SourceCabinet = t.Cabinet.CabinetNumber.ToString(),
                           FromSourceCabinetInput = t.CabinetInput.InputNumber.ToString(),
                           ToSourceCabinetInput = t.CabinetInput1.InputNumber.ToString(),

                           TargetCabinet = t.Cabinet1.CabinetNumber.ToString(),
                           FromTargetCabinetInput = t.CabinetInput2.InputNumber.ToString(),
                           ToTargetCabinetInput = t.CabinetInput3.InputNumber.ToString(),


                       }).SingleOrDefault();
            }
        }

        public static List<CenterToCenterTranslationChooseNumberInfo> GetTelephones(CenterToCenterTranslation centerToCenterTranslation)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
                                     .Where(t => t.Bucht.CabinetInput.CabinetID == context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().CabinetID &&
                                                t.Bucht.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().InputNumber &&
                                                t.Bucht.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.ToOldCabinetInputID).SingleOrDefault().InputNumber)
                                                .Select(t => new CenterToCenterTranslationChooseNumberInfo
                                                            {
                                                                TelephonNo = t.Telephone.TelephoneNo,
                                                                IsRound = t.Telephone.IsRound,
                                                                IsVIP = t.Telephone.IsVIP,
                                                                CustomerName = (t.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.Telephone.Customer.LastName ?? string.Empty),
                                                                BuchtID = t.Bucht.ID,
                                                                VerticalCloumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                                VerticalRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                                                                BuchtNo = t.Bucht.BuchtNo,
                                                                CabinetID = t.Bucht.CabinetInput.CabinetID,
                                                                CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                                CabinetInputID = t.Bucht.CabinetInputID,
                                                                CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                                PostID = t.Bucht.PostContact.PostID,
                                                                PostContactID = t.Bucht.PostContact.ID,
                                                                PostNumber = t.Bucht.PostContact.Post.Number,
                                                                PostContactNumber = t.Bucht.PostContact.ConnectionNo,
                                 
                                                            }
                                                            ).OrderBy(t => t.TelephonNo).ToList();
            }
        }


        public static List<CenterToCenterTranslationChooseNumberInfo> GetTelephonesNew(CenterToCenterTranslation centerToCenterTranslation)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Join(context.Buchts, t => t.SwitchPortID, b => b.SwitchPortID, (t, b) => new { Telephone = t, Bucht = b })
                                     .Where(t => t.Bucht.CabinetInput.CabinetID == context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromNewCabinetInputID).SingleOrDefault().CabinetID &&
                                                t.Bucht.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromNewCabinetInputID).SingleOrDefault().InputNumber &&
                                                t.Bucht.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.ToNewCabinetInputID).SingleOrDefault().InputNumber)
                                                .Select(t => new CenterToCenterTranslationChooseNumberInfo
                                                {
                                                    TelephonNo = t.Telephone.TelephoneNo,
                                                    IsRound = t.Telephone.IsRound,
                                                    IsVIP = t.Telephone.IsVIP,
                                                    CustomerName = (t.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.Telephone.Customer.LastName ?? string.Empty),
                                                    BuchtID = t.Bucht.ID,
                                                    VerticalCloumnNo = t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                    VerticalRowNo = t.Bucht.VerticalMDFRow.VerticalRowNo,
                                                    BuchtNo = t.Bucht.BuchtNo,
                                                    CabinetID = t.Bucht.CabinetInput.CabinetID,
                                                    CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                    CabinetInputID = t.Bucht.CabinetInputID,
                                                    CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                    PostID = t.Bucht.PostContact.PostID,
                                                    PostContactID = t.Bucht.PostContact.ID,
                                                    PostNumber = t.Bucht.PostContact.Post.Number,
                                                    PostContactNumber = t.Bucht.PostContact.ConnectionNo,

                                                }
                                                            ).OrderBy(t => t.TelephonNo).ToList();
            }
        }

        public static List<CenterToCenterTranslationTelephone> GetCenterToCenterTranslationTelephon(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslationTelephones.Where(t => t.RequestID == requestID).ToList();
            }
        }

        public static bool CheckEqualityPostContact(Post oldPost, Post newPost)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int oldPostCount = context.PostContacts.Where(t => t.PostID == oldPost.ID && t.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal).Count();
                int newPostCount = context.PostContacts.Where(t => t.PostID == newPost.ID && t.ConnectionType != (int)DB.PostContactConnectionType.PCMNormal).Count();
                if (oldPostCount == newPostCount)
                    return true;
                else
                    return false;

            }
        }

        public static bool CheckExistPCM(CenterToCenterTranslation centerToCenterTranslation)
        {

            using (MainDataContext context = new MainDataContext())
            {

              return  context.Buchts.Where(t => t.CabinetInput.CabinetID == context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().CabinetID &&
                                          t.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().InputNumber &&
                                          t.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.ToOldCabinetInputID).SingleOrDefault().InputNumber
                                     ).Any(t => t.BuchtTypeID == (int)DB.BuchtType.OutLine);
            }
        }

        public static List<CenterToCenterTranslationPCMInfo> GetCabinetInputsPCM(CenterToCenterTranslation centerToCenterTranslation)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t =>
                                            t.CabinetInput.CabinetID == context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().CabinetID &&
                                            t.CabinetInput.InputNumber >= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.FromOldCabinetInputID).SingleOrDefault().InputNumber &&
                                            t.CabinetInput.InputNumber <= context.CabinetInputs.Where(ci => ci.ID == centerToCenterTranslation.ToOldCabinetInputID).SingleOrDefault().InputNumber &&
                                            t.BuchtTypeID == (int)DB.BuchtType.OutLine
                                           ).Select(t => new CenterToCenterTranslationPCMInfo
                                           {
                                                OldPCMID = t.PCMPort.PCM.ID,
                                                OldPCMTypeID = t.PCMPort.PCM.PCMTypeID,
                                                OldPCMName = "رک : " + t.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " شلف : " + t.PCMPort.PCM.PCMShelf.Number.ToString() + " کارت : " + t.PCMPort.PCM.Card.ToString() + " نوع : " + t.PCMPort.PCM.PCMType.Name,
                                           }).ToList();
            }
        }

        public static bool CheckExistCenterToCenterTranslationPCM(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.CenterToCenterTranslationPCMs.Where(t => t.RequestID == requestID).Count() > 0)
                    return true;
                else
                    return false;
            }
        }

        public static List<CenterToCenterTranslationPCMInfo> GetCenterToCenterPCMs(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
              return  context.CenterToCenterTranslationPCMs.Join(context.PCMs , cp=>cp.OldPCMID , p=>p.ID,(cp , p)=> new{CP = cp ,  PCM = p})
                       .Join(context.PCMs, cp => cp.CP.NewPCMID, p => p.ID, (cp, p) => new { CP = cp.CP, NewPCM = p, OldPCM = cp.PCM })
                       
                       .Where(t=>t.CP.RequestID == requestID)
                       .Select(t => new CenterToCenterTranslationPCMInfo
                                           {
                                                OldPCMID = t.OldPCM.ID,
                                                OldPCMTypeID = t.OldPCM.PCMTypeID,
                                                CabinetInputNumber = context.Buchts.Where(t2=> t.OldPCM.PCMPorts.Where(t3=>t3.PortType == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID == t2.PCMPortID).SingleOrDefault().CabinetInput.InputNumber,
                                                PostContactNumber = context.Buchts.Where(t2 => t.OldPCM.PCMPorts.Where(t3 => t3.PortType == (byte)DB.BuchtType.OutLine).SingleOrDefault().ID == t2.PCMPortID).SingleOrDefault().PostContact.ConnectionNo,
                                                OldPCMName = "رک : " + t.OldPCM.PCMShelf.PCMRock.Number.ToString() + " شلف : " + t.OldPCM.PCMShelf.Number.ToString() + " کارت : " + t.OldPCM.Card.ToString() + " نوع : " + t.OldPCM.PCMType.Name,
                                                NewPCMName = "رک : " + t.NewPCM.PCMShelf.PCMRock.Number.ToString() + " شلف : " + t.NewPCM.PCMShelf.Number.ToString() + " کارت : " + t.NewPCM.Card.ToString() + " نوع : " + t.NewPCM.PCMType.Name,
                                                NewPCMID = t.NewPCM.ID,
                                                
                                           }).ToList();
            }
        }

        public static PCM GetNewPCMByOldPCMID(long requestID , int oldPCMID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.CenterToCenterTranslationPCMs.Join(context.PCMs, cp => cp.OldPCMID, p => p.ID, (cp, p) => new { CP = cp, PCM = p })
                         .Join(context.PCMs, cp => cp.CP.NewPCMID, p => p.ID, (cp, p) => new { CP = cp.CP, NewPCM = p, OldPCM = cp.PCM })
                         .Where(t => t.CP.RequestID == requestID && t.OldPCM.ID == oldPCMID).SingleOrDefault().NewPCM;
            }
        }


    }
}
