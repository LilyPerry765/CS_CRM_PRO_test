using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Enterprise;
using System.Globalization;
using System.Transactions;
using System.Linq.Dynamic;


namespace CRM.Data
{
    public static class ADSLChangePlaceDB
    {
        public static ADSLChangePlace GetADSLChangePlaceById(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePlaces.Where(t => t.ID == requestID).SingleOrDefault();
            }

        }

        public static ADSLChangePlace GetADSLChangePlaceByOldTelephoneNo(long oldTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLChangePlace changePlace = context.ADSLChangePlaces.Where(t => t.OldTelephoneNo == oldTelephoneNo && t.Request.EndDate == null && t.Request.IsCancelation == false && (t.Request.IsVisible == true || t.Request.IsVisible == null)).SingleOrDefault();

                if (changePlace != null)
                    return changePlace;
                else
                    return null;    
            }
        }

        public static ADSLChangePlace GetADSLChangePlaceByOldTelephoneNoAAA(long oldTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLChangePlace changePlace = context.ADSLChangePlaces.Where(t => t.OldTelephoneNo == oldTelephoneNo && t.Request.EndDate != null && t.Request.IsCancelation == false && (t.Request.IsVisible == true || t.Request.IsVisible == null)).SingleOrDefault();

                if (changePlace != null)
                    return changePlace;
                else
                    return null;
            }
        }

        public static ADSLChangePlace GetADSLChangePlaceByNewTelephoneNo(long newTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ADSLChangePlace> changePlaceList = context.ADSLChangePlaces.Where(t => t.NewTelephoneNo == newTelephoneNo).ToList();

                if (changePlaceList != null && changePlaceList.Count!=0)
                    return changePlaceList.FirstOrDefault();
                else
                    return null;    
            }
        }

        public static List<ADSLChangePlaceRequestInfo> SearchADSLChangePlaceInfo(string requestID, string oldTelephoneNo, string newTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLChangePlaces
                    .Where(t => (string.IsNullOrWhiteSpace(requestID) || t.ID.ToString().Contains(requestID)) &&
                                (string.IsNullOrWhiteSpace(oldTelephoneNo) || t.OldTelephoneNo.ToString().Contains(oldTelephoneNo)) &&
                                (string.IsNullOrWhiteSpace(newTelephoneNo) || t.NewTelephoneNo.ToString().Contains(newTelephoneNo)))
                    .Select(t => new ADSLChangePlaceRequestInfo
                    {
                        RequestID = t.ID,
                        OldTelephoneNo = t.OldTelephoneNo.ToString(),
                        OldCenter = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                        OldPortNo = "ردیف : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.ADSLPort.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.ADSLPort.Bucht.BuchtNo.ToString(),
                        NewTelephoneNo = t.NewTelephoneNo.ToString(),
                        NewCenter = t.Center1.Region.City.Name + " : " + t.Center1.CenterName,
                        NewPortNo = "ردیف : " + t.ADSLPort1.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.ADSLPort1.Bucht.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.ADSLPort1.Bucht.BuchtNo.ToString(),
                        EndDate = Date.GetPersianDate(t.Request.EndDate, Date.DateStringType.Short)
                    }).ToList();
            }
        }
    }
}
