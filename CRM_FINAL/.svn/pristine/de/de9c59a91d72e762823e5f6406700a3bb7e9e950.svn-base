using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class PAPInfoCostDB
    {
        public static List<PAPCostInfo> SearchPAPInfoCost(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoCostHistories
                        .Where(t => (string.IsNullOrWhiteSpace(title) || t.PAPInfoCost.Title.Contains(title)) && t.EndDate == null)
                        .Select(t => new PAPCostInfo
                        {
                            ID = t.ID,
                            CostID = t.CostID,
                            Title = t.PAPInfoCost.Title,
                            Value = t.Value,
                            StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                            EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        })
                        .ToList();
            }
        }

        public static List<PAPCostInfo> SearchPAPCostHistories(int costID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoCostHistories
                        .Where(t => t.CostID == costID)
                        .Select(t => new PAPCostInfo
                        {
                            ID = t.ID,
                            CostID = t.CostID,
                            Title = t.PAPInfoCost.Title,
                            Value = t.Value,
                            StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                            EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short),
                        })
                        .ToList();
            }
        }

        public static PAPCostInfo GetPAPInfoCostById(int costID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoCostHistories
                              .Where(t => t.CostID == costID && t.EndDate == null)
                              .Select(t => new PAPCostInfo
                              {
                                  ID = t.ID,
                                  CostID = t.CostID,
                                  Title = t.PAPInfoCost.Title,
                                  //Value = t.Value,
                                  //StartDate = Date.GetPersianDate(t.StartDate, Date.DateStringType.Short),
                                  EndDate = Date.GetPersianDate(t.EndDate, Date.DateStringType.Short)
                              })
                              .SingleOrDefault();
            }
        }

        public static PAPInfoCostHistory GetPAPInfoCostByIdDateTime(int id,DateTime? FromDate , DateTime? ToDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoCostHistories
                              .Where(t => (t.CostID == id) && ((FromDate >= t.StartDate ) && (t.EndDate == null || ToDate <= t.EndDate)))
                              .SingleOrDefault();
            }
        }
        


        public static PAPInfoCostHistory LastPAPCostHistory(int costID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PAPInfoCostHistories.Where(t => t.EndDate == null && t.CostID == costID).SingleOrDefault();
            }
        }

    }
}
