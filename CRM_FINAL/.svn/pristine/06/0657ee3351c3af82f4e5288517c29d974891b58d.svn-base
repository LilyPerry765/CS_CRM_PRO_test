using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class InvestigatePossibilityWaitinglistDB
    {
        //public static List<InvestigatePossibilityWaitinglistInfo> Serach(
        //    List<int> cites,
        //    List<int> centers,
        //    long requestID,
        //    int cabinet,
        //    int post,
        //    long telephone,
        //    bool? oneYear,
        //    List<int> causeIDs,
        //    int startRowIndex,
        //    int pageSize,
        //    out int count
        //    )
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        DateTime currentTime = DB.GetServerDate();
        //        var query = context.InvestigatePossibilityWaitinglists.Where(t =>
        //                                                                    (cites.Count == 0 || cites.Contains(t.WaitingList.Request.Center.Region.CityID)) &&
        //                                                                    (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.WaitingList.Request.CenterID) : centers.Contains(t.WaitingList.Request.CenterID)) &&
        //                                                                    (requestID == -1 || t.WaitingList.Request.ID == requestID) &&
        //                                                                    (cabinet == -1 || t.Cabinet.CabinetNumber == cabinet) &&
        //                                                                    (post == -1 || t.Post.Number == post) &&
        //                                                                    (telephone == -1 || t.WaitingList.Request.TelephoneNo == telephone) &&
        //                                                                    (t.WaitingList.Status == false) &&
        //                                                                    (t.WaitingList.WaitingListType == (int)DB.WatingListType.investigatePossibility) &&
        //                                                                    (oneYear != null ? (oneYear == true ? t.WaitingList.InsertDate.AddYears(1) <= currentTime : t.WaitingList.InsertDate.AddYears(1) > currentTime) : true) &&
        //                                                                    (causeIDs.Count == 0 || causeIDs.Contains((int)t.WaitingList.StatusID)))
        //                                                          .Select(t => new InvestigatePossibilityWaitinglistInfo
        //                                                                       {
        //                                                                           IsChecked = false,
        //                                                                           ID = t.ID,
        //                                                                           RequestID = (long)t.WaitingList.RequestID,
        //                                                                           Adress = t.Address.AddressContent,
        //                                                                           CabinetID = (int)t.CabinetID,
        //                                                                           CabinetNumber = t.Cabinet.CabinetNumber,
        //                                                                           CenterName = t.WaitingList.Request.Center.CenterName,
        //                                                                           CustomerID = t.Customer.CustomerID,
        //                                                                           CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
        //                                                                           InsertDate = t.WaitingList.InsertDate.ToPersian(Date.DateStringType.Short),
        //                                                                           InstallAdressID = t.InstallAdressID,
        //                                                                           PostCode = t.Address.PostalCode,
        //                                                                           PostID = t.PostID,
        //                                                                           PostNumber = t.Post.Number.ToString(),
        //                                                                           ReqeustInsertDate = t.WaitingList.Request.InsertDate.ToPersian(Date.DateStringType.Short),
        //                                                                           RequestTypeID = t.WaitingList.Request.RequestTypeID,
        //                                                                           RequestTypeName = t.WaitingList.Request.RequestType.Title,
        //                                                                           StatusID = t.WaitingList.StatusID,
        //                                                                           StatusName = t.WaitingList.Status1.Title,
        //                                                                           TelephoneNo = t.WaitingList.Request.TelephoneNo,
        //                                                                           DoProvidedFacility = t.Cabinet.LastTimeAddFacility.HasValue ? (t.Cabinet.LastTimeAddFacility.Value >= t.WaitingList.InsertDate ? true : false) : false,
        //                                                                           HasFreePostContact = t.Post.PostContacts.Any(t2 => t2.Status == (byte)DB.PostContactStatus.Free),
        //                                                                           HasFreeBucht = context.Buchts.Any(t2 => t2.CabinetInput.CabinetID == t.CabinetID && t2.Status == (int)DB.BuchtStatus.Free),
        //                                                                           isValidTime = t.WaitingList.InsertDate.AddYears(1) <= currentTime ? true : false,
        //                                                                       }).OrderBy(t => t.RequestID);


        //        count = query.Count();
        //        if (count > 0 && count <= pageSize)
        //        {
        //            return query.Take(pageSize).ToList();
        //        }

        //        return query.Skip(startRowIndex).Take(pageSize).ToList();
        //    }

        //}

        public static List<InvestigatePossibilityWaitinglistInfo> Serach(
            List<int> cites,
            List<int> centers,
            long requestID,
            int cabinet,
            int post,
            long telephone,
            bool? oneYear,
            List<int> causeIDs,
            bool isOutOfWaitingList,
            int startRowIndex,
            int pageSize,
            out int count
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime currentTime = DB.GetServerDate();
                List<InvestigatePossibilityWaitinglistInfo> finalResult = new List<InvestigatePossibilityWaitinglistInfo>();
                IQueryable<InvestigatePossibilityWaitinglistInfo> secondQuery = Enumerable.Empty<InvestigatePossibilityWaitinglistInfo>().AsQueryable();

                var query = context.InvestigatePossibilityWaitinglists.Where(t =>
                                                                            (cites.Count == 0 || cites.Contains(t.WaitingList.Request.Center.Region.CityID)) &&
                                                                            (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.WaitingList.Request.CenterID) : centers.Contains(t.WaitingList.Request.CenterID)) &&
                                                                            (requestID == -1 || t.WaitingList.Request.ID == requestID) &&
                                                                            (cabinet == -1 || t.Cabinet.CabinetNumber == cabinet) &&
                                                                            (post == -1 || t.Post.Number == post) &&
                                                                            (telephone == -1 || t.WaitingList.Request.TelephoneNo == telephone) &&
                                                                            (t.WaitingList.WaitingListType == (int)DB.WatingListType.investigatePossibility) &&
                                                                            (oneYear != null ? (oneYear == true ? t.WaitingList.InsertDate.AddYears(1) <= currentTime : t.WaitingList.InsertDate.AddYears(1) > currentTime) : true) &&
                                                                            (causeIDs.Count == 0 || causeIDs.Contains((int)t.WaitingList.StatusID)))
                                                                      .AsQueryable();

                if (isOutOfWaitingList)
                {
                    secondQuery = query.Where(st =>
                                                    (st.WaitingList.Status == true) &&
                                                    (st.WaitingList.Request.EndDate.HasValue)
                                             )
                                       .Select(t => new InvestigatePossibilityWaitinglistInfo
                                                    {
                                                        IsChecked = false,
                                                        ID = t.ID,
                                                        RequestID = (long)t.WaitingList.RequestID,
                                                        Adress = t.Address.AddressContent,
                                                        CabinetID = (int)t.CabinetID,
                                                        CabinetNumber = t.Cabinet.CabinetNumber,
                                                        CenterName = t.WaitingList.Request.Center.CenterName,
                                                        CityName = t.WaitingList.Request.Center.Region.City.Name,
                                                        CustomerID = t.Customer.CustomerID,
                                                        CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                                                        InsertDate = t.WaitingList.InsertDate.ToPersian(Date.DateStringType.Short),
                                                        InstallAdressID = t.InstallAdressID,
                                                        PostCode = t.Address.PostalCode,
                                                        PostID = t.PostID,
                                                        PostNumber = t.Post.Number.ToString(),
                                                        ReqeustInsertDate = t.WaitingList.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                        RequestTypeID = t.WaitingList.Request.RequestTypeID,
                                                        RequestTypeName = t.WaitingList.Request.RequestType.Title,
                                                        StatusID = t.WaitingList.StatusID,
                                                        StatusName = t.WaitingList.Status1.Title,
                                                        TelephoneNo = t.WaitingList.Request.TelephoneNo,
                                                        DoProvidedFacility = t.Cabinet.LastTimeAddFacility.HasValue ? (t.Cabinet.LastTimeAddFacility.Value >= t.WaitingList.InsertDate ? true : false) : false,
                                                        HasFreePostContact = t.Post.PostContacts.Any(t2 => t2.Status == (byte)DB.PostContactStatus.Free),
                                                        HasFreeBucht = context.Buchts.Any(t2 => t2.CabinetInput.CabinetID == t.CabinetID && t2.Status == (int)DB.BuchtStatus.Free),
                                                        isValidTime = t.WaitingList.InsertDate.AddYears(1) <= currentTime ? true : false,
                                                        InWaitingListHours = (t.WaitingList.ExitDate.Value - t.WaitingList.InsertDate).TotalHours
                                                    }
                                            )
                                       .OrderBy(t => t.RequestID);
                }
                else
                {
                    secondQuery = query.Where(st => st.WaitingList.Status == false)
                                       .Select(t => new InvestigatePossibilityWaitinglistInfo
                                                   {
                                                       IsChecked = false,
                                                       ID = t.ID,
                                                       RequestID = (long)t.WaitingList.RequestID,
                                                       Adress = t.Address.AddressContent,
                                                       CabinetID = (int)t.CabinetID,
                                                       CabinetNumber = t.Cabinet.CabinetNumber,
                                                       CenterName = t.WaitingList.Request.Center.CenterName,
                                                       CityName = t.WaitingList.Request.Center.Region.City.Name,
                                                       CustomerID = t.Customer.CustomerID,
                                                       CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                                                       InsertDate = t.WaitingList.InsertDate.ToPersian(Date.DateStringType.Short),
                                                       InstallAdressID = t.InstallAdressID,
                                                       PostCode = t.Address.PostalCode,
                                                       PostID = t.PostID,
                                                       PostNumber = t.Post.Number.ToString(),
                                                       ReqeustInsertDate = t.WaitingList.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                       RequestTypeID = t.WaitingList.Request.RequestTypeID,
                                                       RequestTypeName = t.WaitingList.Request.RequestType.Title,
                                                       StatusID = t.WaitingList.StatusID,
                                                       StatusName = t.WaitingList.Status1.Title,
                                                       TelephoneNo = t.WaitingList.Request.TelephoneNo,
                                                       DoProvidedFacility = t.Cabinet.LastTimeAddFacility.HasValue ? (t.Cabinet.LastTimeAddFacility.Value >= t.WaitingList.InsertDate ? true : false) : false,
                                                       HasFreePostContact = t.Post.PostContacts.Any(t2 => t2.Status == (byte)DB.PostContactStatus.Free),
                                                       HasFreeBucht = context.Buchts.Any(t2 => t2.CabinetInput.CabinetID == t.CabinetID && t2.Status == (int)DB.BuchtStatus.Free),
                                                       isValidTime = t.WaitingList.InsertDate.AddYears(1) <= currentTime ? true : false,
                                                       InWaitingListHours = (currentTime - t.WaitingList.InsertDate).TotalHours
                                                   }
                                            )
                                      .OrderBy(t => t.RequestID);
                }

                count = secondQuery.Count();
                if (count > 0 && count <= pageSize)
                {
                    finalResult = secondQuery.Take(pageSize).ToList();
                }
                else
                {
                    finalResult = secondQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }

                //محاسبه مدت زمان انتظار
                finalResult.ForEach(ip =>
                                        {
                                            int days = TimeSpan.FromHours(ip.InWaitingListHours.Value).Days;
                                            int hours = (int)ip.InWaitingListHours.Value % 24;
                                            ip.InWaitingListDuration = string.Format("تعداد روز : {0} - تعداد ساعت : {1} ", days, hours);
                                        }
                                    );


                return finalResult;
            }

        }


        //TODO:rad 13950617
        public static List<InvestigatePossibilityWaitinglistInfo> Serach(
           List<int> cites,
           List<int> centers,
           long requestID,
           int cabinet,
           int post,
           long telephone,
           bool? oneYear,
           List<int> causeIDs,
           DateTime? fromInsertDate,
           DateTime? toInsertDate,
           bool isOutOfWaitingList,
           bool forPrint,
           int startRowIndex,
           int pageSize,
           out int count
           )
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime currentTime = DB.GetServerDate();
                if (toInsertDate.HasValue)
                {
                    toInsertDate = toInsertDate.Value.AddDays(1);
                }

                List<InvestigatePossibilityWaitinglistInfo> finalResult = new List<InvestigatePossibilityWaitinglistInfo>();
                IQueryable<InvestigatePossibilityWaitinglistInfo> secondQuery = Enumerable.Empty<InvestigatePossibilityWaitinglistInfo>().AsQueryable();

                var query = context.InvestigatePossibilityWaitinglists.Where(t =>
                                                                                (cites.Count == 0 || cites.Contains(t.WaitingList.Request.Center.Region.CityID)) &&
                                                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.WaitingList.Request.CenterID) : centers.Contains(t.WaitingList.Request.CenterID)) &&
                                                                                (!fromInsertDate.HasValue || fromInsertDate <= t.WaitingList.InsertDate) &&
                                                                                (!toInsertDate.HasValue || toInsertDate >= t.WaitingList.InsertDate) &&
                                                                                (requestID == -1 || t.WaitingList.Request.ID == requestID) &&
                                                                                (cabinet == -1 || t.Cabinet.CabinetNumber == cabinet) &&
                                                                                (post == -1 || t.Post.Number == post) &&
                                                                                (telephone == -1 || t.WaitingList.Request.TelephoneNo == telephone) &&
                                                                                (t.WaitingList.WaitingListType == (int)DB.WatingListType.investigatePossibility) &&
                                                                                (oneYear != null ? (oneYear == true ? t.WaitingList.InsertDate.AddYears(1) <= currentTime : t.WaitingList.InsertDate.AddYears(1) > currentTime) : true) &&
                                                                                (causeIDs.Count == 0 || causeIDs.Contains((int)t.WaitingList.StatusID))
                                                                             )
                                                                      .AsQueryable();

                if (isOutOfWaitingList)
                {
                    secondQuery = query.Where(st =>
                                                    (st.WaitingList.Status == true) &&
                                                    (st.WaitingList.Request.EndDate.HasValue)
                                             )
                                       .Select(t => new InvestigatePossibilityWaitinglistInfo
                                       {
                                           IsChecked = false,
                                           ID = t.ID,
                                           RequestID = (long)t.WaitingList.RequestID,
                                           Adress = t.Address.AddressContent,
                                           CabinetID = (int)t.CabinetID,
                                           CabinetNumber = t.Cabinet.CabinetNumber,
                                           CenterName = t.WaitingList.Request.Center.CenterName,
                                           CityName = t.WaitingList.Request.Center.Region.City.Name,
                                           CustomerID = t.Customer.CustomerID,
                                           CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                                           InsertDate = t.WaitingList.InsertDate.ToPersian(Date.DateStringType.Short),
                                           InstallAdressID = t.InstallAdressID,
                                           PostCode = t.Address.PostalCode,
                                           PostID = t.PostID,
                                           PostNumber = t.Post.Number.ToString(),
                                           ReqeustInsertDate = t.WaitingList.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                           RequestTypeID = t.WaitingList.Request.RequestTypeID,
                                           RequestTypeName = t.WaitingList.Request.RequestType.Title,
                                           StatusID = t.WaitingList.StatusID,
                                           StatusName = t.WaitingList.Status1.Title,
                                           TelephoneNo = t.WaitingList.Request.TelephoneNo,
                                           DoProvidedFacility = t.Cabinet.LastTimeAddFacility.HasValue ? (t.Cabinet.LastTimeAddFacility.Value >= t.WaitingList.InsertDate ? true : false) : false,
                                           HasFreePostContact = t.Post.PostContacts.Any(t2 => t2.Status == (byte)DB.PostContactStatus.Free),
                                           HasFreeBucht = context.Buchts.Any(t2 => t2.CabinetInput.CabinetID == t.CabinetID && t2.Status == (int)DB.BuchtStatus.Free),
                                           isValidTime = t.WaitingList.InsertDate.AddYears(1) <= currentTime ? true : false,
                                           InWaitingListHours = (t.WaitingList.ExitDate.Value - t.WaitingList.InsertDate).TotalHours
                                       }
                                            )
                                       .OrderBy(t => t.RequestID);
                }
                else
                {
                    secondQuery = query.Where(st => st.WaitingList.Status == false)
                                       .Select(t => new InvestigatePossibilityWaitinglistInfo
                                       {
                                           IsChecked = false,
                                           ID = t.ID,
                                           RequestID = (long)t.WaitingList.RequestID,
                                           Adress = t.Address.AddressContent,
                                           CabinetID = (int)t.CabinetID,
                                           CabinetNumber = t.Cabinet.CabinetNumber,
                                           CenterName = t.WaitingList.Request.Center.CenterName,
                                           CityName = t.WaitingList.Request.Center.Region.City.Name,
                                           CustomerID = t.Customer.CustomerID,
                                           CustomerName = string.Format("{0} {1}", t.Customer.FirstNameOrTitle, t.Customer.LastName),
                                           InsertDate = t.WaitingList.InsertDate.ToPersian(Date.DateStringType.Short),
                                           InstallAdressID = t.InstallAdressID,
                                           PostCode = t.Address.PostalCode,
                                           PostID = t.PostID,
                                           PostNumber = t.Post.Number.ToString(),
                                           ReqeustInsertDate = t.WaitingList.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                           RequestTypeID = t.WaitingList.Request.RequestTypeID,
                                           RequestTypeName = t.WaitingList.Request.RequestType.Title,
                                           StatusID = t.WaitingList.StatusID,
                                           StatusName = t.WaitingList.Status1.Title,
                                           TelephoneNo = t.WaitingList.Request.TelephoneNo,
                                           DoProvidedFacility = t.Cabinet.LastTimeAddFacility.HasValue ? (t.Cabinet.LastTimeAddFacility.Value >= t.WaitingList.InsertDate ? true : false) : false,
                                           HasFreePostContact = t.Post.PostContacts.Any(t2 => t2.Status == (byte)DB.PostContactStatus.Free),
                                           HasFreeBucht = context.Buchts.Any(t2 => t2.CabinetInput.CabinetID == t.CabinetID && t2.Status == (int)DB.BuchtStatus.Free),
                                           isValidTime = t.WaitingList.InsertDate.AddYears(1) <= currentTime ? true : false,
                                           InWaitingListHours = (currentTime - t.WaitingList.InsertDate).TotalHours
                                       }
                                            )
                                      .OrderBy(t => t.RequestID);
                }


                if (forPrint)
                {
                    finalResult = secondQuery.ToList();
                    count = finalResult.Count;
                }
                else
                {
                    count = secondQuery.Count();
                    finalResult = secondQuery.Skip(startRowIndex).Take(pageSize).ToList();
                }

                //محاسبه مدت زمان انتظار
                finalResult.ForEach(ip =>
                                        {
                                            int days = TimeSpan.FromHours(ip.InWaitingListHours.Value).Days;
                                            int hours = (int)ip.InWaitingListHours.Value % 24;
                                            ip.InWaitingListDuration = string.Format("میزان روز : {0} - میزان ساعت : {1} ", days, hours);
                                        }
                                    );

                return finalResult;
            }

        }

        public static int SerachCount(
        List<int> cites,
        List<int> centers,
        long reqeustID,
        int cabinet,
        int post,
        long telephone)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilityWaitinglists.Where(t =>
                                                                            (cites.Count == 0 || cites.Contains(t.Cabinet.Center.Region.CityID)) &&
                                                                            (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Cabinet.CenterID) : centers.Contains(t.Cabinet.CenterID)) &&
                                                                            (reqeustID == -1 || t.WaitingList.Request.ID == reqeustID) &&
                                                                            (cabinet == -1 || t.Cabinet.CabinetNumber == cabinet) &&
                                                                            (post == -1 || t.Post.Number == post) &&
                                                                            (telephone == -1 || t.WaitingList.Request.TelephoneNo == telephone) &&
                                                                            (t.WaitingList.Status == false) &&
                                                                            (t.WaitingList.WaitingListType == (int)DB.WatingListType.investigatePossibility)).Count();
            }

        }

        public static List<InvestigatePossibilityWaitinglist> GetPostInvestigatePossibilityWaitingList(List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilityWaitinglists.Where(t => investigatePossibilityWaitngListChangeInfo.Select(t2 => t2.oldPostID).Contains((int)t.PostID)).ToList();
            }
        }

        public static List<InvestigatePossibilityWaitinglist> GetCabinetInvestigatePossibilityWaitingList(List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilityWaitinglists.Where(t => investigatePossibilityWaitngListChangeInfo.Select(t2 => t2.oldCabinetID).Contains((int)t.CabinetID)).ToList();
            }
        }
    }
}
