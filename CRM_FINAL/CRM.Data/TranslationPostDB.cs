using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TranslationPostDB
    {
        public static TranslationPost GetTranslationPostByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPosts.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static List<TranslationPost> GetTranslationPostByIDs(List<long> requestIDS)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPosts.Where(t => requestIDS.Contains(t.RequestID)).ToList();
            }
        }

        public static TranslationPostDetails GetTranslationPostDetailsByID(long reqeustID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TranslationPosts.Where(t => t.RequestID == reqeustID).Select(t =>
                    new TranslationPostDetails
                            {
                                CabinetNumber = t.Cabinet.CabinetNumber,
                                OverallTransfer = t.OverallTransfer,
                                OldConnectionNo = t.PostContact.ConnectionNo,
                                NewConnectionNo = t.PostContact1.ConnectionNo,
                                FromPostNumber = t.Post.Number,
                                ToPostNumber = t.Post1.Number,
                            }).SingleOrDefault();
            }
        }

        public static void VerifyData(TranslationPost translationPost , int currentStatus)
        {
            Cabinet OldCabinet = Data.CabinetDB.GetCabinetByID(translationPost.OldCabinetID);
            List<PostContact> oldPostContactList;
            List<PostContact> newPostContactList;

            if (translationPost.OverallTransfer == true)
            {

                oldPostContactList = Data.PostContactDB.GetPostContactByPostID(translationPost.OldPostID);
                newPostContactList = Data.PostContactDB.GetPostContactByPostID(translationPost.NewPostID);

                if (oldPostContactList.Where(t => !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Any(t => !(t.Status == (byte)DB.PostContactStatus.Free || t.Status == (byte)DB.PostContactStatus.CableConnection || t.Status == (byte)DB.PostContactStatus.PermanentBroken)))
                {
                    throw new Exception("پست دارای اتصالی رزرو میباشد");
                }
                if (oldPostContactList.Where(t => (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Any(t => !(t.Status == (byte)DB.PostContactStatus.Free || t.Status == (byte)DB.PostContactStatus.CableConnection || t.Status == (byte)DB.PostContactStatus.NoCableConnection || t.Status == (byte)DB.PostContactStatus.PermanentBroken)))
                {
                    throw new Exception("پست دارای اتصالی رزرو میباشد");
                }
                //Separate free postContact from new postContect
                if (newPostContactList.Any(t => t.Status != (byte)DB.PostContactStatus.Free))
                {
                    throw new Exception("همه اتصالی های پست جدید باید آزاد باشند");
                }

                if (oldPostContactList.Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal).Count() > newPostContactList.Count)
                    throw new Exception("تعداد اتصال های آزاد پست جدید کم تر از تعداد اتصالهای پست قبل برگردان است");
            }
            else
            {
                if (!Data.StatusDB.IsFinalStep(currentStatus))
                {
                         oldPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)translationPost.OldPostContactID) };
                         newPostContactList = new List<PostContact> { Data.PostContactDB.GetPostContactByID((long)translationPost.NewPostContactID) };
                     
                         if (translationPost.TransferWaitingList)
                         {
                             throw new Exception("امکان انتقال عدم امکانات در انتقال جزیی نمی باشد");
                         }
                     
                         if (oldPostContactList.Where(t => !(t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Any(t => !(t.Status == (byte)DB.PostContactStatus.Free || t.Status == (byte)DB.PostContactStatus.CableConnection || t.Status == (byte)DB.PostContactStatus.PermanentBroken)))
                         {
                             throw new Exception("پست دارای اتصالی رزرو میباشد");
                         }
                         if (oldPostContactList.Where(t => (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMRemote)).Any(t => !(t.Status == (byte)DB.PostContactStatus.Free || t.Status == (byte)DB.PostContactStatus.CableConnection || t.Status == (byte)DB.PostContactStatus.NoCableConnection || t.Status == (byte)DB.PostContactStatus.PermanentBroken)))
                         {
                             throw new Exception("پست دارای اتصالی رزرو میباشد");
                         }
                         //Separate free postContact from new postContect
                         if (newPostContactList.Any(t => t.Status != (byte)DB.PostContactStatus.Free))
                         {
                             throw new Exception("همه اتصالی های پست جدید باید آزاد باشند");
                         }
                     
                         if (oldPostContactList.Where(t => t.ConnectionType != (byte)DB.PostContactConnectionType.PCMNormal).Count() > newPostContactList.Count)
                             throw new Exception("تعداد اتصال های آزاد پست جدید کم تر از تعداد اتصالهای پست قبل برگردان است");
                }
            }
            

        }
    }
}
