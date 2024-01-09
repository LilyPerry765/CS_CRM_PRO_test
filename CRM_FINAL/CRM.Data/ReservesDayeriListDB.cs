using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ReservesDayeriListDB
    {
        public static int ReservesDayeriCount(List<int> citys, List<int> centers, int cabinet, int cabinetInput, int post, int postContact)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int count = 0;
                count = context.InvestigatePossibilities
                    .Where(t =>
                             (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                             (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                              t.Bucht.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                              t.Request.RequestTypeID == (byte)DB.RequestType.Dayri &&
                              t.Request.EndDate == null &&
                              (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                              (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                              (post == -1 || t.Bucht.PostContact.Post.Number == post) &&
                              (postContact == -1 || t.Bucht.PostContact.ConnectionNo == postContact)
                          ).Count();


                count = count + context.ChangeLocations
                                   .Where(t =>
                (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                 t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                 t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside &&
                 t.Request.EndDate == null &&
                 (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                 (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
                 (post == -1 || t.PostContact.Post.Number == post) &&
                 (postContact == -1 || t.PostContact.ConnectionNo == postContact)).Count();



                count = count + context.ChangeLocations
                                 .Where(t =>
              (citys.Count == 0 || citys.Contains(t.Center1.Region.CityID)) &&
              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Center1.ID) : centers.Contains(t.Center1.ID)) &&
               t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
               t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter &&
               t.Request.EndDate == null &&
               (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
               (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
               (post == -1 || t.PostContact.Post.Number == post) &&
               (postContact == -1 || t.PostContact.ConnectionNo == postContact)).Count();


                count = count + context.SpecialWires
                                 .Where(t =>
              (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
               t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
               (t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWire || t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWireOtherPoint) &&
               t.Request.EndDate == null &&
               (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
               (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
               (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
               (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)).Count();


                count = count + context.ChangeLocationSpecialWires
                                 .Where(t =>
              (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
               t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
               (t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationSpecialWire) &&
               t.Request.EndDate == null &&
               (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
               (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
               (post == -1 || t.PostContact.Post.Number == post) &&
               (postContact == -1 || t.PostContact.ConnectionNo == postContact)).Count();

                count = count + context.E1s
                                 .Where(t =>
              (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
               t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
               (t.Request.RequestTypeID == (byte)DB.RequestType.E1) &&
               t.Request.EndDate == null &&
               (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
               (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
               (post == -1 || t.PostContact.Post.Number == post) &&
               (postContact == -1 || t.PostContact.ConnectionNo == postContact)).Count();

                count = count + context.E1Links
                                 .Where(t =>
              (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
              (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
               t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
               (t.Request.RequestTypeID == (byte)DB.RequestType.E1Link) &&
               t.Request.EndDate == null &&
               (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
               (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
               (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
               (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)).Count();

                return count;

            }
        }

        public static List<ReservesDayeriInfo> SearchReservesDayeri(List<int> citys, List<int> centers, int cabinet, int cabinetInput, int post, int postContact, long requestID, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ReservesDayeriInfo> reservesDayeriInfo = new List<ReservesDayeriInfo>();

                #region Dayeri

                List<ReservesDayeriInfo> investigatePossibilitieInfos = new List<ReservesDayeriInfo>();

                investigatePossibilitieInfos = context.InvestigatePossibilities
                                                      .Where(t =>
                                                               (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                               (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                t.Bucht.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                t.Request.RequestTypeID == (byte)DB.RequestType.Dayri &&
                                                                t.Request.EndDate == null &&
                                                                (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                (post == -1 || t.Bucht.PostContact.Post.Number == post) &&
                                                                (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                (postContact == -1 || t.Bucht.PostContact.ConnectionNo == postContact)
                                                            )
                                                      .Select(t => new ReservesDayeriInfo
                                                                  {
                                                                      ID = t.Request.ID,
                                                                      CustomerName = t.Request.Customer.FirstNameOrTitle + " " + (t.Request.Customer.LastName ?? ""),
                                                                      CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                                      //InsertDate = t.Request.InsertDate,
                                                                      //ModifyDate = t.Request.ModifyDate,
                                                                      PostNumber = t.Bucht.PostContact.Post.Number,
                                                                      AORBType = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                                                                      CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                                      CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                                      ConnectionNo = t.Bucht.PostContact.ConnectionNo,
                                                                      RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                                      StatusName = t.Bucht.PostContact.PostContactStatus.Name,
                                                                      RequestTypeName = t.Request.RequestType.Title,

                                                                      //TODO:rad add persian string
                                                                      PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                      PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                                  }
                                                             )
                                                      .ToList();

                reservesDayeriInfo.AddRange(investigatePossibilitieInfos);

                #endregion

                # region ChangeLocationCenterInside

                List<ReservesDayeriInfo> changeLocationCenterInsideInfos = new List<ReservesDayeriInfo>();

                changeLocationCenterInsideInfos = context.ChangeLocations
                                                         .Where(t =>
                                                                     (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                                     (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                      t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                      t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside &&
                                                                      t.Request.EndDate == null &&
                                                                      (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                      (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                      (post == -1 || t.PostContact.Post.Number == post) &&
                                                                      (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                      (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                )
                                                         .Select(t => new ReservesDayeriInfo
                                                                     {
                                                                         ID = t.Request.ID,
                                                                         CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                                         CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                                         //InsertDate = t.Request.InsertDate,
                                                                         //ModifyDate = t.Request.ModifyDate,
                                                                         PostNumber = t.PostContact.Post.Number,
                                                                         AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                                         CabinetInputNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber,
                                                                         CabinetNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                                         ConnectionNo = t.PostContact.ConnectionNo,
                                                                         RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                                         StatusName = t.PostContact.PostContactStatus.Name,
                                                                         RequestTypeName = t.Request.RequestType.Title,

                                                                         PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                         PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                                     }
                                                                 )
                                                         .ToList();

                reservesDayeriInfo.AddRange(changeLocationCenterInsideInfos);

                # endregion

                # region ChangeLocationCenterToCenter

                List<ReservesDayeriInfo> changeLocationCenterToCenterInfos = new List<ReservesDayeriInfo>();

                changeLocationCenterToCenterInfos = context.ChangeLocations
                                                           .Where(t =>
                                                                      (citys.Count == 0 || citys.Contains(t.Center1.Region.CityID)) &&
                                                                      (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Center1.ID) : centers.Contains(t.Center1.ID)) &&
                                                                      t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                      t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter &&
                                                                      t.Request.EndDate == null &&
                                                                      (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                      (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                      (post == -1 || t.PostContact.Post.Number == post) &&
                                                                      (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                      (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                  )
                                                           .Select(t => new ReservesDayeriInfo
                                                                       {
                                                                           ID = t.Request.ID,
                                                                           CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                                           CenterName = t.Center1.Region.City.Name + " : " + t.Center1.CenterName,
                                                                           //InsertDate = t.Request.InsertDate,
                                                                           //ModifyDate = t.Request.ModifyDate,
                                                                           PostNumber = t.PostContact.Post.Number,
                                                                           AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                                           CabinetInputNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber,
                                                                           CabinetNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                                           ConnectionNo = t.PostContact.ConnectionNo,
                                                                           RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                                           StatusName = t.PostContact.PostContactStatus.Name,
                                                                           RequestTypeName = t.Request.RequestType.Title,

                                                                           PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                           PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                                       }
                                                                  )
                                                           .ToList();

                reservesDayeriInfo.AddRange(changeLocationCenterToCenterInfos);

                # endregion

                # region SpecialWire

                List<ReservesDayeriInfo> specialWiresInfos = new List<ReservesDayeriInfo>();

                specialWiresInfos = context.SpecialWires
                                           .Where(t =>
                                                    (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                    (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                    t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                    (t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWire || t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWireOtherPoint) &&
                                                    t.Request.EndDate == null &&
                                                    (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                    (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                    (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
                                                    (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                    (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)
                                                  )
                                           .Select(t => new ReservesDayeriInfo
                                                        {
                                                            ID = t.Request.ID,
                                                            CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                            CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                            //InsertDate = t.Request.InsertDate,
                                                            //ModifyDate = t.Request.ModifyDate,
                                                            PostNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number,
                                                            AORBType = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.AORBPostAndCabinet.Name,
                                                            CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                            CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                            ConnectionNo = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo,
                                                            RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                            StatusName = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.PostContactStatus.Name,
                                                            RequestTypeName = t.Request.RequestType.Title,

                                                            PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                        }
                                                  )
                                           .ToList();

                reservesDayeriInfo.AddRange(specialWiresInfos);

                # endregion

                # region ChangeLocationSpecialWire

                List<ReservesDayeriInfo> changeLocationSpecialWireInfos = new List<ReservesDayeriInfo>();

                changeLocationSpecialWireInfos = context.ChangeLocationSpecialWires
                                                        .Where(t =>
                                                                    (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                                    (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                    t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                    (t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationSpecialWire) &&
                                                                    t.Request.EndDate == null &&
                                                                    (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                    (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                    (post == -1 || t.PostContact.Post.Number == post) &&
                                                                    (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                    (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                )
                                                        .Select(t => new ReservesDayeriInfo
                                                                       {
                                                                           ID = t.Request.ID,
                                                                           CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                                           CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                                           //InsertDate = t.Request.InsertDate,
                                                                           //ModifyDate = t.Request.ModifyDate,
                                                                           PostNumber = t.PostContact.Post.Number,
                                                                           AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                                           CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                                           CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                                           ConnectionNo = t.PostContact.ConnectionNo,
                                                                           RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                                           StatusName = t.PostContact.PostContactStatus.Name,
                                                                           RequestTypeName = t.Request.RequestType.Title,

                                                                           PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                                           PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                                       }
                                                               )
                                                        .ToList();

                reservesDayeriInfo.AddRange(changeLocationSpecialWireInfos);

                # endregion

                # region E1

                List<ReservesDayeriInfo> e1Infos = new List<ReservesDayeriInfo>();

                e1Infos = context.E1s
                                 .Where(t =>
                                             (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                             (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                             t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                             (t.Request.RequestTypeID == (byte)DB.RequestType.E1) &&
                                             t.Request.EndDate == null &&
                                             (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                             (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                             (post == -1 || t.PostContact.Post.Number == post) &&
                                             (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                             (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                         )
                                 .Select(t => new ReservesDayeriInfo
                                             {
                                                 ID = t.Request.ID,
                                                 CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                 CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                 //InsertDate = t.Request.InsertDate,
                                                 //ModifyDate = t.Request.ModifyDate,
                                                 PostNumber = t.PostContact.Post.Number,
                                                 AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                 CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                 CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                 ConnectionNo = t.PostContact.ConnectionNo,
                                                 RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                 StatusName = t.PostContact.PostContactStatus.Name,
                                                 RequestTypeName = t.Request.RequestType.Title,

                                                 PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                 PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                             }
                                        )
                                 .ToList();

                reservesDayeriInfo.AddRange(e1Infos);

                # endregion

                # region E1Link

                List<ReservesDayeriInfo> e1LinkInfos = new List<ReservesDayeriInfo>();

                e1LinkInfos = context.E1Links
                                     .Where(t =>
                                                (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                (t.Request.RequestTypeID == (byte)DB.RequestType.E1Link) &&
                                                t.Request.EndDate == null &&
                                                (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
                                                (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)
                                           )
                                     .Select(t => new ReservesDayeriInfo
                                                 {
                                                     ID = t.Request.ID,
                                                     CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                     CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                     //InsertDate = t.Request.InsertDate,
                                                     //ModifyDate = t.Request.ModifyDate,
                                                     PostNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number,
                                                     AORBType = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.AORBPostAndCabinet.Name,
                                                     CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                     CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                     ConnectionNo = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo,
                                                     RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                     StatusName = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.PostContactStatus.Name,
                                                     RequestTypeName = t.Request.RequestType.Title,

                                                     PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                     PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                 }
                                           )
                                     .ToList();

                reservesDayeriInfo.AddRange(e1LinkInfos);

                # endregion

                count = reservesDayeriInfo.Count();
                return reservesDayeriInfo.Skip(startRowIndex).Take(pageSize).ToList();

            }
        }

        public static List<ReservesDayeriInfo> SearchReservesDayeri(List<int> citys, List<int> centers, int cabinet, int cabinetInput, int post, int postContact, long requestID, bool forPrint, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<ReservesDayeriInfo> reservesDayeriInfo = new List<ReservesDayeriInfo>();

                #region Dayeri

                List<ReservesDayeriInfo> investigatePossibilitieInfos = new List<ReservesDayeriInfo>();

                investigatePossibilitieInfos = context.InvestigatePossibilities
                                                      .Where(t =>
                                                               (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                               (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                t.Bucht.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                t.Request.RequestTypeID == (byte)DB.RequestType.Dayri &&
                                                                t.Request.EndDate == null &&
                                                                (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                (post == -1 || t.Bucht.PostContact.Post.Number == post) &&
                                                                (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                (postContact == -1 || t.Bucht.PostContact.ConnectionNo == postContact)
                                                            )
                                                      .Select(t => new ReservesDayeriInfo
                                                      {
                                                          ID = t.Request.ID,
                                                          CustomerName = t.Request.Customer.FirstNameOrTitle + " " + (t.Request.Customer.LastName ?? ""),
                                                          CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                          //InsertDate = t.Request.InsertDate,
                                                          //ModifyDate = t.Request.ModifyDate,
                                                          PostNumber = t.Bucht.PostContact.Post.Number,
                                                          AORBType = t.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                                                          CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                          CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                          ConnectionNo = t.Bucht.PostContact.ConnectionNo,
                                                          RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                          StatusName = t.Bucht.PostContact.PostContactStatus.Name,
                                                          RequestTypeName = t.Request.RequestType.Title,

                                                          //TODO:rad add persian string
                                                          PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                          PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                      }
                                                             )
                                                      .ToList();

                reservesDayeriInfo.AddRange(investigatePossibilitieInfos);

                #endregion

                # region ChangeLocationCenterInside

                List<ReservesDayeriInfo> changeLocationCenterInsideInfos = new List<ReservesDayeriInfo>();

                changeLocationCenterInsideInfos = context.ChangeLocations
                                                         .Where(t =>
                                                                     (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                                     (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                      t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                      t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterInside &&
                                                                      t.Request.EndDate == null &&
                                                                      (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                      (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                      (post == -1 || t.PostContact.Post.Number == post) &&
                                                                      (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                      (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                )
                                                         .Select(t => new ReservesDayeriInfo
                                                         {
                                                             ID = t.Request.ID,
                                                             CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                             CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                             //InsertDate = t.Request.InsertDate,
                                                             //ModifyDate = t.Request.ModifyDate,
                                                             PostNumber = t.PostContact.Post.Number,
                                                             AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                             CabinetInputNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber,
                                                             CabinetNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                             ConnectionNo = t.PostContact.ConnectionNo,
                                                             RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                             StatusName = t.PostContact.PostContactStatus.Name,
                                                             RequestTypeName = t.Request.RequestType.Title,

                                                             PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                             PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                         }
                                                                 )
                                                         .ToList();

                reservesDayeriInfo.AddRange(changeLocationCenterInsideInfos);

                # endregion

                # region ChangeLocationCenterToCenter

                List<ReservesDayeriInfo> changeLocationCenterToCenterInfos = new List<ReservesDayeriInfo>();

                changeLocationCenterToCenterInfos = context.ChangeLocations
                                                           .Where(t =>
                                                                      (citys.Count == 0 || citys.Contains(t.Center1.Region.CityID)) &&
                                                                      (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Center1.ID) : centers.Contains(t.Center1.ID)) &&
                                                                      t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                      t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationCenterToCenter &&
                                                                      t.Request.EndDate == null &&
                                                                      (cabinet == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                      (cabinetInput == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                      (post == -1 || t.PostContact.Post.Number == post) &&
                                                                      (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                      (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                  )
                                                           .Select(t => new ReservesDayeriInfo
                                                           {
                                                               ID = t.Request.ID,
                                                               CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                               CenterName = t.Center1.Region.City.Name + " : " + t.Center1.CenterName,
                                                               //InsertDate = t.Request.InsertDate,
                                                               //ModifyDate = t.Request.ModifyDate,
                                                               PostNumber = t.PostContact.Post.Number,
                                                               AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                               CabinetInputNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.InputNumber,
                                                               CabinetNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                               ConnectionNo = t.PostContact.ConnectionNo,
                                                               RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                               StatusName = t.PostContact.PostContactStatus.Name,
                                                               RequestTypeName = t.Request.RequestType.Title,

                                                               PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                               PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                           }
                                                                  )
                                                           .ToList();

                reservesDayeriInfo.AddRange(changeLocationCenterToCenterInfos);

                # endregion

                # region SpecialWire

                List<ReservesDayeriInfo> specialWiresInfos = new List<ReservesDayeriInfo>();

                specialWiresInfos = context.SpecialWires
                                           .Where(t =>
                                                    (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                    (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                    t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                    (t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWire || t.Request.RequestTypeID == (byte)DB.RequestType.SpecialWireOtherPoint) &&
                                                    t.Request.EndDate == null &&
                                                    (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                    (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                    (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
                                                    (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                    (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)
                                                  )
                                           .Select(t => new ReservesDayeriInfo
                                           {
                                               ID = t.Request.ID,
                                               CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                               CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                               //InsertDate = t.Request.InsertDate,
                                               //ModifyDate = t.Request.ModifyDate,
                                               PostNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number,
                                               AORBType = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.AORBPostAndCabinet.Name,
                                               CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                               CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                               ConnectionNo = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo,
                                               RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                               StatusName = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.PostContactStatus.Name,
                                               RequestTypeName = t.Request.RequestType.Title,

                                               PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                               PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                           }
                                                  )
                                           .ToList();

                reservesDayeriInfo.AddRange(specialWiresInfos);

                # endregion

                # region ChangeLocationSpecialWire

                List<ReservesDayeriInfo> changeLocationSpecialWireInfos = new List<ReservesDayeriInfo>();

                changeLocationSpecialWireInfos = context.ChangeLocationSpecialWires
                                                        .Where(t =>
                                                                    (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                                    (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                                    t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                                    (t.Request.RequestTypeID == (byte)DB.RequestType.ChangeLocationSpecialWire) &&
                                                                    t.Request.EndDate == null &&
                                                                    (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                                    (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                                    (post == -1 || t.PostContact.Post.Number == post) &&
                                                                    (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                                    (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                                                )
                                                        .Select(t => new ReservesDayeriInfo
                                                        {
                                                            ID = t.Request.ID,
                                                            CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                                            CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                            //InsertDate = t.Request.InsertDate,
                                                            //ModifyDate = t.Request.ModifyDate,
                                                            PostNumber = t.PostContact.Post.Number,
                                                            AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                                            CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                                            CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                            ConnectionNo = t.PostContact.ConnectionNo,
                                                            RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                                            StatusName = t.PostContact.PostContactStatus.Name,
                                                            RequestTypeName = t.Request.RequestType.Title,

                                                            PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                                            PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                                        }
                                                               )
                                                        .ToList();

                reservesDayeriInfo.AddRange(changeLocationSpecialWireInfos);

                # endregion

                # region E1

                List<ReservesDayeriInfo> e1Infos = new List<ReservesDayeriInfo>();

                e1Infos = context.E1s
                                 .Where(t =>
                                             (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                             (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                             t.PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                             (t.Request.RequestTypeID == (byte)DB.RequestType.E1) &&
                                             t.Request.EndDate == null &&
                                             (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                             (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                             (post == -1 || t.PostContact.Post.Number == post) &&
                                             (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                             (postContact == -1 || t.PostContact.ConnectionNo == postContact)
                                         )
                                 .Select(t => new ReservesDayeriInfo
                                 {
                                     ID = t.Request.ID,
                                     CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                     CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                     //InsertDate = t.Request.InsertDate,
                                     //ModifyDate = t.Request.ModifyDate,
                                     PostNumber = t.PostContact.Post.Number,
                                     AORBType = t.PostContact.Post.AORBPostAndCabinet.Name,
                                     CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                     CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                     ConnectionNo = t.PostContact.ConnectionNo,
                                     RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                     StatusName = t.PostContact.PostContactStatus.Name,
                                     RequestTypeName = t.Request.RequestType.Title,

                                     PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                     PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                 }
                                        )
                                 .ToList();

                reservesDayeriInfo.AddRange(e1Infos);

                # endregion

                # region E1Link

                List<ReservesDayeriInfo> e1LinkInfos = new List<ReservesDayeriInfo>();

                e1LinkInfos = context.E1Links
                                     .Where(t =>
                                                (citys.Count == 0 || citys.Contains(t.Request.Center.Region.CityID)) &&
                                                (centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID) : centers.Contains(t.Request.CenterID)) &&
                                                t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Status == (byte)DB.PostContactStatus.FullBooking &&
                                                (t.Request.RequestTypeID == (byte)DB.RequestType.E1Link) &&
                                                t.Request.EndDate == null &&
                                                (cabinet == -1 || t.Bucht.CabinetInput.Cabinet.CabinetNumber == cabinet) &&
                                                (cabinetInput == -1 || t.Bucht.CabinetInput.InputNumber == cabinetInput) &&
                                                (post == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number == post) &&
                                                (requestID == -1 || t.Request.ID.ToString().Contains(requestID.ToString())) &&
                                                (postContact == -1 || t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo == postContact)
                                           )
                                     .Select(t => new ReservesDayeriInfo
                                     {
                                         ID = t.Request.ID,
                                         CustomerName = t.Request.Customer.FirstNameOrTitle.ToString() + " " + (t.Request.Customer.LastName ?? ""),
                                         CenterName = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                         //InsertDate = t.Request.InsertDate,
                                         //ModifyDate = t.Request.ModifyDate,
                                         PostNumber = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.Number,
                                         AORBType = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.Post.AORBPostAndCabinet.Name,
                                         CabinetInputNumber = t.Bucht.CabinetInput.InputNumber,
                                         CabinetNumber = t.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                         ConnectionNo = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.ConnectionNo,
                                         RequestStatusName = t.Request.Status.RequestStep.StepTitle,
                                         StatusName = t.Request.InvestigatePossibilities.Take(1).SingleOrDefault().PostContact.PostContactStatus.Name,
                                         RequestTypeName = t.Request.RequestType.Title,

                                         PersianInsertDate = t.Request.InsertDate.ToPersian(Date.DateStringType.Short),
                                         PersianModifyDate = t.Request.ModifyDate.ToPersian(Date.DateStringType.Short)
                                     }
                                           )
                                     .ToList();

                reservesDayeriInfo.AddRange(e1LinkInfos);

                # endregion

                count = reservesDayeriInfo.Count;

                if (forPrint)
                {
                    return reservesDayeriInfo;
                }
                else
                {
                    return reservesDayeriInfo.Skip(startRowIndex).Take(pageSize).ToList();
                }
            }
        }

    }
}
