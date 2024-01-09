using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;
using Enterprise;

namespace CRM.Data
{
    public static class PCMTypeDB
    {
//        public static List<PCMType> SearchPCMType(int outportCapacity,int numberOFChannel, string pCMBrandName, string pCMTypeName, string comment)
//        {
//            using (MainDataContext context = new MainDataContext())
//            {
//                return context.PCMTypes
//                  //  .Where(t =>
//                         // (outportCapacity == -1 || t.OutportCapacity == outportCapacity) &&
//                        //  (numberOFChannel == -1 || t.NumberOFChannel == numberOFChannel) &&
//                         // (string.IsNullOrWhiteSpace(pCMBrandName) || t.PCMBrandName.Contains(pCMBrandName)) &&
//                        //  (string.IsNullOrWhiteSpace(pCMTypeName) || t.PCMTypeName.Contains(pCMTypeName)) &&
////(string.IsNullOrWhiteSpace(comment) || t.Comment.Contains(comment))
//                   //       )
//                    .ToList();
//            }
//        }

//        public static PCMType GetPCMTypeByID(int id)
//        {
//            using (MainDataContext context = new MainDataContext())
//            {
//                return context.PCMTypes
//                    .Where(t => t.ID == id)
//                    .SingleOrDefault();
//            }
//        }

//        public static List<PCMType> GetPCMTypeListByID(int id)
//        {
//            using (MainDataContext context = new MainDataContext())
//            {
//                return context.PCMTypes
//                    .Where(t => t.ID == id).ToList();
//            }
//        }

        public static List<CheckableItem> GetPCMTypeCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.PCMTypes
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Name,
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

       

        public static bool CheckBeRepeatedRockShelfCard(PCMCardInfo pCMInfo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                PCMShelf pCMShelf = context.PCMShelfs.Where(t => t.ID == pCMInfo.PCMShelfID).SingleOrDefault();

                PCMRock pCMRock = context.PCMRocks.Where(t => t.ID == pCMShelf.PCMRockID).SingleOrDefault();
                PCMType pCMType = context.PCMTypes.Where(t => t.ID == pCMInfo.PCMTypeID).SingleOrDefault();
                List<PCM> pCM = context.PCMs
                                       .Where(t => 
                                                    (t.PCMShelf.PCMRock.Center.ID == pCMRock.CenterID) && 
                                                    (t.PCMShelf.PCMRock.ID == pCMRock.ID) && 
                                                    (t.PCMShelf.ID == pCMShelf.ID) && 
                                                    (t.Card == pCMInfo.Card)
                                              )
                                       .ToList();
                if (pCM != null && pCM.Count == 1)
                {
                    if (
                            (pCM.SingleOrDefault().PCMType.AorB == (byte)DB.PCMAorB.A && pCMType.AorB == (byte)DB.PCMAorB.B) || 
                            (pCM.SingleOrDefault().PCMType.AorB == (byte)DB.PCMAorB.B && pCMType.AorB == (byte)DB.PCMAorB.A)
                        )
                    {
                        return false;
                    }
                    return true;
                }
                else if(pCM != null && pCM.Count > 1)
                {
                    return true;
                }
                return false;
            }
        }

        public static PCMType GetPCMTypeByID(int id)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.PCMTypes.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}