using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
   public static class SugesstionPossibilityDB
    {
       public static List<SugessionSource> GetSugessionSourceByCenterID(int centerID)
       {
           using (MainDataContext context = new MainDataContext())
           {

               //TODO
               throw new Exception("GetSuggessionSource(centerID) باید اصلاح شود");
               return null;
               //return context.GetSuggessionSource(centerID).Select(t => new SugessionSource
               //{
               //      CenterID=t.CenterID,
               //      MDFID=t.MDFID,
               //      SourceType=t.SourceType,
               //      SourceDescriptionType=t.SourceDescriptionType,
               //      MDFNumber=t.MDFNumber,
               //      PostID = t.PostID,
               //      PostNumber = t.PostNumber,
               //      SourceID=t.SourceID,
               //      SourceNumber=t.SourceNumber
               //}

               //    ).ToList();

           }
       }

       public static SugesstionPossibility GetSugesstionPossibilityByID(long suggesstionPossibilityID)
       {
           using(MainDataContext context = new MainDataContext())
           {
               return context.SugesstionPossibilities.Where(t=>t.ID == suggesstionPossibilityID).SingleOrDefault();
           }
       }

       public static int GetCenterByVisitID(int visitID)
       {
           //using (MainDataContext context = new MainDataContext())
           //{
           //    return context.GetCenterVisitID(visitID).Select(t => t.CenterID).Take(1).SingleOrDefault();
           //}
           throw new Exception("GetCenterByVisitID");
           return 0;
       }


       public static List<SugesstionPossibility> GetSugesstionPossibilityByInvestigatePossibilityID(long investigatePossibilityID)
       {
           using (MainDataContext context = new MainDataContext())
           {
               return context.SugesstionPossibilities.Where(t => t.InvestigatePossibilityID == investigatePossibilityID).ToList();
           }
       }
    }
    #region Custom Class 
   public class SugessionSource
   { 
        public int CenterID{get;set;}
        public int MDFID{get;set;}
        public string SourceType{get;set;}
        public string SourceDescriptionType{get;set;}
        public int? MDFNumber{get;set;}
        public int? PostID{get;set;}
        public int? PostNumber{get;set;}
        public int SourceID{get;set;}
        public int? SourceNumber { get; set; }
   }
    #endregion Custom Class

}
