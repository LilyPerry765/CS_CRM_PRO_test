using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLMDFRangeDB
    {
        public static List<ADSLMDFRange> GetADSLMDFRangebyMDFID(int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLMDFRanges.Where(t => t.MDFID == mDFID).ToList();
            }
        }

        public static ADSLMDFInfo GetADSLMDFInfobyMDFID(int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.MDFs.Where(t => t.ID == mDFID)
                                   .Select(t => new ADSLMDFInfo
                                   {
                                       ID = t.ID,
                                       MDFTitle = t.Description,
                                       Center = t.Center.Region.City.Name + " : " + t.Center.CenterName
                                   }).SingleOrDefault();
            }
        }

        public static int GetMDFinRangebyTelephoneNo(long telephoneNo, int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int mDFResultID = 0;

                List<ADSLMDFInfo> mDFsList = context.Buchts.Where(t => (t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID == centerID) && (t.BuchtTypeID == (byte)DB.BuchtType.ADSL)).Select(t => new ADSLMDFInfo { ID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID }).Distinct().ToList();
                
                foreach (ADSLMDFInfo currentMDF in mDFsList)
                {
                    List<ADSLMDFRange> mDFRangeList = context.ADSLMDFRanges.Where(t=>t.MDFID==(int)currentMDF.ID).ToList();

                    foreach (ADSLMDFRange range in mDFRangeList)
                    {
                        if (telephoneNo >= range.StartTelephoneNo && telephoneNo <= range.EndTelephoneNo)
                        {
                            mDFResultID = (int)currentMDF.ID;
                            break;
                        }
                        else
                            mDFResultID = 0;
                    }

                    if (mDFResultID != 0)
                        break;
                }

                return mDFResultID;
            }
        }
    }
}
