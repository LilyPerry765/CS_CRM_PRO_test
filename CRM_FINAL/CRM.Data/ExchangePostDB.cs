using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class ExchangePostDB
    {
        public static List<ExchangePostInfo> SearchExchangePost(
         List<int> city,
         List<int> center,
         List<int> oldCabinetComboBox,
         List<int> newCabinetComboBox,
         List<int> oldPostComboBox,
         List<int> newPostComboBox,
         DateTime? accomplishmentDateDate,
         int fromCabinetInputID,
         int FromOldConnectionNo,
         long toOldConnectionNo,
         long AccomplishmentTime,
            string requestLetterNo,
               int startRowIndex,
               int pageSize)
        {
            using (MainDataContext Context = new MainDataContext())
            {

                return Context.ExchangePosts.Where(t =>
                      (city.Count == 0 || city.Contains(t.Cabinet.Center.Region.CityID)) &&
                      (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : center.Contains(t.Cabinet.CenterID)) &&
                      (oldCabinetComboBox.Count == 0 || oldCabinetComboBox.Contains(t.OldCabinetID)) &&
                      (newCabinetComboBox.Count == 0 || oldCabinetComboBox.Contains(t.NewCabinetID)) &&
                      (oldPostComboBox.Count == 0 || oldCabinetComboBox.Contains(t.OldPostID)) &&
                      (newPostComboBox.Count == 0 || oldCabinetComboBox.Contains(t.NewPostID)) &&
                      (!accomplishmentDateDate.HasValue || accomplishmentDateDate == t.AccomplishmentDate) &&
                      (fromCabinetInputID == -1 || t.FromCabinetInputID == fromCabinetInputID) &&
                      (FromOldConnectionNo == -1 || t.FromOldConnectionID == FromOldConnectionNo) &&
                      (toOldConnectionNo == -1 || t.ToOldConnectionID == toOldConnectionNo) &&
                      (AccomplishmentTime == -1 || t.AccomplishmentTime == AccomplishmentTime.ToString()) &&
                      (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo)
                      ).Select
                            (t => new ExchangePostInfo
                            {
                                ID = t.ID,
                                OldCabinetTypeID = t.Cabinet.CabinetType.ID,
                                OldCabinetTypeName = t.Cabinet.CabinetType.CabinetTypeName,

                                NewCabinetTypeID = t.Cabinet1.CabinetType.ID,
                                NewCabinetTypeName = t.Cabinet1.CabinetType.CabinetTypeName,

                                OldCabinetID = t.OldCabinetID,
                                OldCabinetName = t.Cabinet.CabinetNumber.ToString(),

                                NewCabinetID = t.NewCabinetID,
                                NewCabinetName = t.Cabinet.CabinetNumber.ToString(),

                                OldPostID = t.OldPostID,
                                NewPostName = t.Post1.Number.ToString(),

                                NewPostID = t.NewPostID,
                                OldPostName = t.Post.Number.ToString(),

                                FromOldConnectionID = t.FromOldConnectionID,
                                FromOldConnectionName = t.PostContact.ConnectionNo.ToString(),

                                ToOldConnectionID = t.FromOldConnectionID,
                                ToOldConnectionName = t.PostContact1.ConnectionNo.ToString(),

                                FromCabinetInputID = t.FromCabinetInputID,
                                FromCabinetInputName = t.Cabinet.CabinetNumber.ToString(),

                                AccomplishmentDate = t.AccomplishmentDate,
                                AccomplishmentTime = t.AccomplishmentTime,

                                InsertDate = t.InsertDate,
                                StatusTitle = t.Request.Status.Title,
                                RequestLetterNo = t.Request.RequestLetterNo
                            }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchExchangePostCount(
                  List<int> city,
                  List<int> center,
                  List<int> oldCabinetComboBox,
                  List<int> newCabinetComboBox,
                  List<int> oldPostComboBox,
                  List<int> newPostComboBox,
                  DateTime? accomplishmentDateDate,
                  int fromCabinetInputID,
                  int FromOldConnectionNo,
                  long toOldConnectionNo,
                  long AccomplishmentTime,
                  string requestLetterNo)
        {
            using (MainDataContext Context = new MainDataContext())
            {

                return Context.ExchangePosts.Where(t =>
                      (city.Count == 0 || city.Contains(t.Cabinet.Center.Region.CityID)) &&
                      (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : center.Contains(t.Cabinet.CenterID)) &&
                      (oldCabinetComboBox.Count == 0 || oldCabinetComboBox.Contains(t.OldCabinetID)) &&
                      (newCabinetComboBox.Count == 0 || oldCabinetComboBox.Contains(t.NewCabinetID)) &&
                      (oldPostComboBox.Count == 0 || oldCabinetComboBox.Contains(t.OldPostID)) &&
                      (newPostComboBox.Count == 0 || oldCabinetComboBox.Contains(t.NewPostID)) &&
                      (!accomplishmentDateDate.HasValue || accomplishmentDateDate == t.AccomplishmentDate) &&
                      (fromCabinetInputID == -1 || t.FromCabinetInputID == fromCabinetInputID) &&
                      (FromOldConnectionNo == -1 || t.FromOldConnectionID == FromOldConnectionNo) &&
                      (toOldConnectionNo == -1 || t.ToOldConnectionID == toOldConnectionNo) &&
                      (AccomplishmentTime == -1 || t.AccomplishmentTime == AccomplishmentTime.ToString()) &&
                      (string.IsNullOrEmpty(requestLetterNo) || t.Request.RequestLetterNo == requestLetterNo)
                      ).Count();
            }
        }


        public static ExchangePost GetExchangePostByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangePosts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static long GetExchangePostByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangePosts.Where(t => t.ID == requestID).Select(t => t.ID).SingleOrDefault();
            }
        }
        public static List<ExchangePost> GetExchangePost()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExchangePosts
                    .ToList();
            }
        }

        public class ExchangePostInfo
        {
            public long ID {get ; set ;}
            public long RequestID {get ; set ;}

            public int? OldCabinetTypeID { get; set; }
            public string OldCabinetTypeName { get; set; }

            public int? NewCabinetTypeID { get; set; }
            public string NewCabinetTypeName { get; set; }

            public int    OldCabinetID {get ; set ;}
            public string OldCabinetName { get; set; }

            public int    NewCabinetID {get ; set ;}
            public string NewCabinetName { get; set; }

            public int OldPostID {get ; set ;}
            public string OldPostName { get; set; }

            public int NewPostID {get ; set ;}
            public string NewPostName { get; set; }

            public int OldPostGroupID {get ; set ;}
            public string OldPostGroupName { get; set; }

            public int    NewPostGroupID {get ; set ;}
            public string NewPostGroupName { get; set; }

            public long? FromOldConnectionID {get ; set ;}
            public string FromOldConnectionName { get; set; }

            public long? ToOldConnectionID {get ; set ;}
            public string ToOldConnectionName { get; set; }

            public long? FromCabinetInputID {get ; set ;}
            public string FromCabinetInputName { get; set; }

            public long? ToCabinetInputID {get ; set ;}
            public string ToCabinetInputName { get; set; }

            public DateTime? AccomplishmentDate {get ; set ;}
            public string AccomplishmentTime {get ; set ;}
            public int Status {get ; set ;}
            public DateTime? InsertDate  {get ; set ;}
            public string StatusTitle { get; set; }
            public string RequestLetterNo { get; set; }
        }




      
    }
}