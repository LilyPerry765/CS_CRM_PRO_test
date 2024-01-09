using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class Failure117DB
    {
        public static Request GetFailureRequest(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Request> requestList = context.Requests.Where(t => (t.TelephoneNo == telephoneNo) &&
                                             (t.RequestTypeID == (byte)DB.RequestType.Failure117)).ToList();

                if (requestList.Count != 0)
                    return requestList.OrderByDescending(t => t.InsertDate).First();
                else
                    return null;
            }
        }

        public static List<FailureHistoryInfo> SearchFailureHistory(long id, long telephoneNo)
        {
            if (id != 0)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Failure117s.Where(t => t.ID != id && t.Request.TelephoneNo == telephoneNo)
                                              .Select(t => new FailureHistoryInfo
                                              {
                                                  ID = t.ID,
                                                  RowNo = GetRowNobyRequestID(t.ID),
                                                  LineStatus = t.Failure117LineStatus.Title,
                                                  FailureStatus = t.Failure117FailureStatus.Title,
                                                  InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                                                  EndMDFDate = Date.GetPersianDate(t.EndMDFDate, Date.DateStringType.DateTime),
                                                  PhoneNo = t.Request.TelephoneNo.ToString()
                                              }).OrderBy(t => t.ID).ToList();
                }
            }
            else
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Failure117s.Where(t => t.Request.TelephoneNo == telephoneNo)
                                              .Select(t => new FailureHistoryInfo
                                              {
                                                  ID = t.ID,
                                                  RowNo = GetRowNobyRequestID(t.ID),
                                                  LineStatus = t.Failure117LineStatus.Title,
                                                  FailureStatus = t.Failure117FailureStatus.Title,
                                                  InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                                                  EndMDFDate = Date.GetPersianDate(t.EndMDFDate, Date.DateStringType.DateTime),
                                                  PhoneNo = t.Request.TelephoneNo.ToString()
                                              }).OrderBy(t => t.ID).ToList();
                }
            }
        }

        private static string GetRowNobyRequestID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                FailureForm form = context.FailureForms.Where(t => t.FailureRequestID == id).SingleOrDefault();

                if (form != null)
                    return form.RowNo.ToString();
                else
                    return "-";
            }
        }

        public static Failure117 GetFailureRequestByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static List<Failure117> GetFailureRequestListByID(List<long> iDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => iDs.Contains(t.ID)).ToList();
            }
        }

        public static List<Failure117FailureStatus> GetParentFailureStatus(int step)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus.Where(t => t.ParentID == null && t.Availablity.Contains(step.ToString())).ToList();
            }
        }

        public static List<Failure117FailureStatus> GetFailureStatusByParentID(int parentID, int step)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus.Where(t => t.ParentID == parentID && t.Availablity.Contains(step.ToString()) && t.IsActive == true).ToList();
            }
        }

        public static List<Failure117LineStatus> SearchLineStatus(List<int> typeIDs, string title, bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117LineStatus
                        .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                    (typeIDs.Count == 0 || typeIDs.Contains(t.Type)) &&
                                    (!isActive.HasValue || isActive == t.IsActive))
                        .ToList();
            }
        }

        public static Failure117LineStatus GetLineStatusByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117LineStatus
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static Failure117LineStatus GetLineStatusByTitle(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117LineStatus
                              .Where(t => t.Title == title)
                              .SingleOrDefault();
            }
        }

        public static List<Failure117LineStatus> GetFailure117LineStatusbyTypeID(byte typeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117LineStatus.Where(t => t.Type == typeID && t.IsActive != false && t.IsActive != null).ToList();
            }
        }

        public static List<FailureStatusInfo> SearchFailureStatus(List<int> parentIDs, string title, int archivedTime, bool? isActive)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus
                        .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                    (t.ParentID != null) &&
                                    (parentIDs.Count == 0 || parentIDs.Contains((int)t.ParentID)) &&
                                    (archivedTime == 0 || t.ArchivedTime == archivedTime) &&
                                    (!isActive.HasValue || isActive == t.IsActive))
                        .Select(t => new FailureStatusInfo
                        {
                            ID = t.ID,
                            Parent = t.Failure117FailureStatus1.Title,
                            Title = t.Title,
                            Availablity = GenerateFailureStatusAvailibilitis(t.Availablity),
                            ArchivedTime = t.ArchivedTime.ToString(),
                            IsActive = t.IsActive
                        })
                        .ToList();
            }
        }

        public static Failure117FailureStatus GetFailureStatusByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus
                              .Where(t => t.ID == id)
                              .SingleOrDefault();
            }
        }

        public static Failure117FailureStatus GetFailureStatusByTitle(string title)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus
                              .Where(t => t.Title == title)
                              .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetFailureStatusCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus.Where(t => t.ParentID == null)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetChildFailureStatusCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117FailureStatus.Where(t => t.ParentID != null)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetChildLineStatusCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117LineStatus.Where(t => t.Type != null)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.Title,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static string GenerateFailureStatusAvailibilitis(string avalibilityList)
        {
            string result = "";

            if (avalibilityList != null)
            {
                string[] list = avalibilityList.Split(',');
                foreach (string currentChar in list)
                {
                    if (currentChar != "")
                        result = result + DB.GetEnumDescriptionByValue(typeof(DB.Failure117AvalibilityStatus), Convert.ToInt32(currentChar)) + "   ،   ";
                }
            }

            return result;
        }

        public static List<FailureFormRowInfo> SearchFailureFormInfo(int fromRowNo, int toRowNo, string requestID, string telephoneNo, DateTime? fromDate, DateTime? toDate, bool? isNotFinish, int startRowIndex,
            int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => (fromRowNo == -1 || t.RowNo >= fromRowNo) &&
                                                       (toRowNo == -1 || t.RowNo <= toRowNo) &&
                                                       (DB.CurrentUser.CenterIDs.Contains(t.Failure117.Request.CenterID)) &&
                                                       (string.IsNullOrWhiteSpace(requestID) || t.Failure117.Request.ID.ToString().Contains(requestID)) &&
                                                       (string.IsNullOrWhiteSpace(telephoneNo) || t.Failure117.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                       (!fromDate.HasValue || t.Failure117.Request.InsertDate >= fromDate) &&
                                                       (!toDate.HasValue || t.Failure117.Request.InsertDate <= toDate) &&
                                                       ((isNotFinish == true && t.Failure117.Request.EndDate == null) || (isNotFinish == null)))
                                           .OrderByDescending(t => t.Failure117.MDFDate)
                                           .Select(t => new FailureFormRowInfo
                                           {
                                               ID = t.ID,
                                               RequestID = t.FailureRequestID,
                                               TelephoneNo = (long)t.Failure117.Request.TelephoneNo,
                                               Center = t.Failure117.Request.Center.CenterName,
                                               Customer = t.Failure117.Request.Customer.FirstNameOrTitle + " " + t.Failure117.Request.Customer.LastName,
                                               RowNo = t.RowNo,
                                               NetworkOfficer = t.Failure117NetworkContractorOfficer.Name,
                                               FailureStatus = t.Failure117FailureStatus.Title,
                                               LineStatus = t.Failure117.Failure117LineStatus.Title,
                                               InsertDate = Date.GetPersianDate(t.Failure117.Request.InsertDate, Date.DateStringType.DateTime),
                                               Step = t.Failure117.Request.Status.RequestStep.StepTitle
                                           }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int SearchFailureFormInfoCount(int fromRowNo, int toRowNo, string requestID, string telephoneNo, DateTime? fromDate, DateTime? toDate, bool? isNotFinish)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => (fromRowNo == -1 || t.RowNo >= fromRowNo) &&
                                                       (toRowNo == -1 || t.RowNo <= toRowNo) &&
                                                       (DB.CurrentUser.CenterIDs.Contains(t.Failure117.Request.CenterID)) &&
                                                       (string.IsNullOrWhiteSpace(requestID) || t.Failure117.Request.ID.ToString().Contains(requestID)) &&
                                                       (string.IsNullOrWhiteSpace(telephoneNo) || t.Failure117.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                       (!fromDate.HasValue || t.Failure117.Request.InsertDate >= fromDate) &&
                                                       (!toDate.HasValue || t.Failure117.Request.InsertDate <= toDate) &&
                                                       ((isNotFinish == true && t.Failure117.Request.EndDate == null) || (isNotFinish == null))).Count();
            }
        }

        public static FailureForm GetFailureForm(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                FailureForm form = context.FailureForms.Where(t => t.FailureRequestID == requestID).SingleOrDefault();
                if (form != null)
                    return form;
                else
                    return null;
            }
        }

        public static List<FailureForm> GetFailureFormList(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => requestIDs.Contains(t.FailureRequestID)).ToList();
            }
        }

        public static FailureFormInfo GetFailureFormInfo(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => t.FailureRequestID == requestID)
                                           .Select(t => new FailureFormInfo
                                           {
                                               ID = t.ID,
                                               RequestID = t.FailureRequestID,
                                               RowNo = t.RowNo,
                                               FailureStatusID = t.FailureStatusID,
                                               FailureStatus = t.Failure117FailureStatus.Title,
                                               CableColorID1 = t.CableColor1,
                                               CableColor1 = t.CableColor.Color,
                                               CableColorID2 = t.CableColor2,
                                               CableColor2 = t.CableColor3.Color,
                                               CableTypeID = t.CableTypeID,
                                               CableType = t.Failure117CableType.Type,
                                               NetworkOfficerID = t.Failure117NetworkContractorOfficerID,
                                               NetworkOfficer = t.Failure117NetworkContractorOfficer.Failure117NetworkContractor.Title + " : " + t.Failure117NetworkContractorOfficer.Name,
                                               FailureSpeed = t.FailureSpeed,
                                               GiveNetworkFormDate = t.GiveNetworkFormDate,// Date.GetPersianDate(t.GiveNetworkFormDate, Date.DateStringType.Short),
                                               GiveNetworkFormTime = t.GiveNetworkFormTime,
                                               GetNetworkFormDate = t.GetNetworkFormDate,// Date.GetPersianDate(t.GetNetworkFormDate, Date.DateStringType.Short),
                                               GetNetworkFormTime = t.GetNetworkFormTime,
                                               SendToCabelDate = t.SendToCabelDate,
                                               SendToCabelTime = t.SendToCabelTime,
                                               CabelDate = t.CabelDate,
                                               CabelTime = t.CabelTime,
                                               Description = t.Description
                                           }).SingleOrDefault();
            }
        }

        public static int GetFormRowNo(int centerId)
        {
            DateTime today = DB.GetServerDate();
            string todayShamsi = Date.GetPersianDate(today, Date.DateStringType.Short);
            string[] todayShamsiAry = todayShamsi.Split('/');
            int firstDayofMonth = Convert.ToInt32(todayShamsiAry[2]);
            DateTime firstDayMiladi = today.AddDays(-(firstDayofMonth - 1));
            firstDayMiladi = new DateTime(firstDayMiladi.Year, firstDayMiladi.Month, firstDayMiladi.Day, 1, 0, 0);

            using (MainDataContext context = new MainDataContext())
            {
                List<FailureForm> formList = context.FailureForms.Where(t => t.FormInsertDate >= firstDayMiladi && t.Failure117.Request.CenterID == centerId).ToList();
                if (formList.Count != 0)
                    return formList.OrderByDescending(t => t.RowNo).First().RowNo + 1;
                else
                    return 1;
            }
        }

        public static FailureFullViewInfo GetFailureRequestInfo(long requestId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => t.ID == requestId)
                                          .Select(t => new FailureFullViewInfo
                                          {
                                              ID = t.ID,
                                              TelephoneNo = t.Request.TelephoneNo.ToString(),
                                              InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                                              Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                              CallingNo = t.CallingNo.ToString(),
                                              MDFAnalysisUser = t.MDFPersonnel.FirstName + " " + t.MDFPersonnel.LastName,
                                              MDFDate = Date.GetPersianDate(t.MDFDate, Date.DateStringType.DateTime),
                                              LineStatus = t.Failure117LineStatus.Title,
                                              MDFComment = t.MDFCommnet,
                                              NetworkUser = GetUserFullName(t.NetworkUserID),
                                              NetworkDate = Date.GetPersianDate(t.NetworkDate, Date.DateStringType.DateTime),
                                              FailureStatus = t.Failure117FailureStatus.Title,
                                              NetworkCommnet = t.NetworkComment,
                                              SaloonUser = GetUserFullName(t.SaloonUserID),
                                              SaloonDate = Date.GetPersianDate(t.SaloonDate, Date.DateStringType.DateTime),
                                              SaloonCommnet = t.SaloonComment,
                                              EndMDFUser = t.MDFPersonnel1.FirstName + " " + t.MDFPersonnel1.LastName,
                                              EndMDFDate = Date.GetPersianDate(t.EndMDFDate, Date.DateStringType.DateTime),
                                              ResultAfterReturn = DB.GetEnumDescriptionByValue(typeof(DB.FailureResultAfterReturn), t.ResultAfterReturn),
                                              EndMDFCommnet = t.EndMDFComment
                                          })
                                          .SingleOrDefault();
            }
        }

        public static string GetUserFullName(int? id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Users.Where(t => t.ID == id).SingleOrDefault() != null)
                {
                    User user = context.Users.Where(t => t.ID == id).SingleOrDefault();
                    return user.FirstName + " " + user.LastName;
                }
                else
                    return "";
            }
        }

        public static TechnicalInfoFailure117 GetCabinetInfobyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;
                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault() != null)
                {
                    if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                        switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                    if (switchPortID != 0)
                    {
                        if (context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList().Count() == 1)
                        {
                            return context.Buchts.Where(t => t.SwitchPortID == switchPortID)
                                                 .Select(t => new TechnicalInfoFailure117
                                                 {
                                                     SwitchPortID = (int)t.SwitchPortID,
                                                     CabinetInputNumber = (t.CabinetInputID != null) ? t.CabinetInput.InputNumber.ToString() : "",
                                                     CabinetNo = (t.CabinetInputID != null) ? t.CabinetInput.Cabinet.CabinetNumber.ToString() : "",
                                                     PostNo = (t.ConnectionID != null) ? t.PostContact.Post.Number.ToString() : "",
                                                     ConnectionNo = (t.ConnectionID != null) ? t.PostContact.ConnectionNo.ToString() : "",
                                                     RADIF = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() : GetPCMOutRadifbyCabinetInputID((long)t.CabinetInputID),
                                                     TABAGHE = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalRowNo.ToString() : GetPCMOutTabaghebyCabinetInputID((long)t.CabinetInputID),
                                                     ETESALII = (t.PCMPortID == null) ? t.BuchtNo.ToString() : GetPCMOutEtesalibyCabinetInputID((long)t.CabinetInputID),
                                                     IsPCM = (t.PCMPortID != null) ? true : false,
                                                     PCMPort = (t.PCMPortID != null) ? t.PCMPort.PortNumber.ToString() : "-",
                                                     PCMModel = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMBrand.Name : "-",
                                                     PCMType = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMType.Name : "-",
                                                     PCMRock = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() : "-",
                                                     PCMShelf = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMShelf.Number.ToString() : "-",
                                                     PCMCard = (t.PCMPortID != null) ? t.PCMPort.PCM.Card.ToString() : "-",
                                                     PCMInRadif = (t.PCMPortID != null) ? t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() : "-",
                                                     PCMInTabaghe = (t.PCMPortID != null) ? t.VerticalMDFRow.VerticalRowNo.ToString() : "-",
                                                     PCMInEtesali = (t.PCMPortID != null) ? t.BuchtNo.ToString() : "-",
                                                     PCMOutRadif = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutRadifbyCabinetInputID((long)t.CabinetInputID) : "-",
                                                     PCMOutTabaghe = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutTabaghebyCabinetInputID((long)t.CabinetInputID) : "-",
                                                     PCMOutEtesali = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutEtesalibyCabinetInputID((long)t.CabinetInputID) : "-",
                                                     HasAnotherBucht = false
                                                 }).SingleOrDefault();
                        }
                        else
                        {
                            if (context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList().Count() > 1)
                            {
                                return context.Buchts.Where(t => t.SwitchPortID == switchPortID && t.BuchtTypeID == (byte)DB.BuchtType.CustomerSide)
                                                     .Select(t => new TechnicalInfoFailure117
                                                     {
                                                         SwitchPortID = (int)t.SwitchPortID,
                                                         CabinetInputNumber = (t.CabinetInputID != null) ? t.CabinetInput.InputNumber.ToString() : "",
                                                         CabinetNo = (t.CabinetInputID != null) ? t.CabinetInput.Cabinet.CabinetNumber.ToString() : "",
                                                         PostNo = (t.ConnectionID != null) ? t.PostContact.Post.Number.ToString() : "",
                                                         ConnectionNo = (t.ConnectionID != null) ? t.PostContact.ConnectionNo.ToString() : "",
                                                         RADIF = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() : GetPCMOutRadifbyCabinetInputID((long)t.CabinetInputID),
                                                         TABAGHE = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalRowNo.ToString() : GetPCMOutTabaghebyCabinetInputID((long)t.CabinetInputID),
                                                         ETESALII = (t.PCMPortID == null) ? t.BuchtNo.ToString() : GetPCMOutEtesalibyCabinetInputID((long)t.CabinetInputID),
                                                         IsPCM = (t.PCMPortID != null) ? true : false,
                                                         PCMPort = (t.PCMPortID != null) ? t.PCMPort.PortNumber.ToString() : "-",
                                                         PCMModel = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMBrand.Name : "-",
                                                         PCMType = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMType.Name : "-",
                                                         PCMRock = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() : "-",
                                                         PCMShelf = (t.PCMPortID != null) ? t.PCMPort.PCM.PCMShelf.Number.ToString() : "-",
                                                         PCMCard = (t.PCMPortID != null) ? t.PCMPort.PCM.Card.ToString() : "-",
                                                         PCMInRadif = (t.PCMPortID != null) ? t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() : "-",
                                                         PCMInTabaghe = (t.PCMPortID != null) ? t.VerticalMDFRow.VerticalRowNo.ToString() : "-",
                                                         PCMInEtesali = (t.PCMPortID != null) ? t.BuchtNo.ToString() : "-",
                                                         PCMOutRadif = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutRadifbyCabinetInputID((long)t.CabinetInputID) : "-",
                                                         PCMOutTabaghe = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutTabaghebyCabinetInputID((long)t.CabinetInputID) : "-",
                                                         PCMOutEtesali = (t.PCMPortID != null && t.CabinetInputID != null) ? GetPCMOutEtesalibyCabinetInputID((long)t.CabinetInputID) : "-",
                                                         HasAnotherBucht = true
                                                     }).FirstOrDefault();
                            }
                            else
                                return null;
                        }
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static List<Bucht> GetAnotherBuchtInfobyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;
                return context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList();
            }
        }

        public static TechnicalInfoFailure117 GetSpecilaWireBuchtbySwitchPortID(int switchPortID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.SwitchPortID == switchPortID && t.BuchtTypeID != (byte)DB.BuchtType.CustomerSide)
                                     .Select(t => new TechnicalInfoFailure117
                                     {
                                         RADIF = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() : GetPCMOutRadifbyCabinetInputID((long)t.CabinetInputID),
                                         TABAGHE = (t.PCMPortID == null) ? t.VerticalMDFRow.VerticalRowNo.ToString() : GetPCMOutTabaghebyCabinetInputID((long)t.CabinetInputID),
                                         ETESALII = (t.PCMPortID == null) ? t.BuchtNo.ToString() : GetPCMOutEtesalibyCabinetInputID((long)t.CabinetInputID),
                                     }).FirstOrDefault();
            }
        }

        public static TechnicalInfoFailure117 GetPCMInputbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;
                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault() != null)
                {
                    if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                        switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                    if (switchPortID != 0)
                    {
                        return context.Buchts.Where(t => t.SwitchPortID == switchPortID)
                                          .Select(t => new TechnicalInfoFailure117
                                          {
                                              RADIF = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                              TABAGHE = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                              ETESALII = t.BuchtNo.ToString()
                                          }).SingleOrDefault();
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static string GetPCMOutRadifbyCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault() != null)
                {
                    long buchtID = Convert.ToInt64(context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault().BuchtIDConnectedOtherBucht);
                    return context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString();
                }
                else
                    return "";
            }
        }

        public static string GetPCMOutTabaghebyCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault() != null)
                {
                    long buchtID = Convert.ToInt64(context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault().BuchtIDConnectedOtherBucht);
                    return context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString();
                }
                else
                    return "";
            }
        }

        public static string GetPCMOutEtesalibyCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault() != null)
                {
                    long buchtID = Convert.ToInt64(context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault().BuchtIDConnectedOtherBucht);
                    return context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().BuchtNo.ToString();
                }
                else
                    return "";
            }
        }

        public static string GetPCMOutPropertybyCabinetInputID(long cabinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault() != null)
                {
                    long buchtID = Convert.ToInt64(context.Buchts.Where(t => t.CabinetInputID == cabinetInputID && t.BuchtTypeID == (byte)DB.BuchtType.OutLine).FirstOrDefault().BuchtIDConnectedOtherBucht);
                    return "ردیف : " + context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + context.Buchts.Where(t => t.ID == buchtID).SingleOrDefault().BuchtNo.ToString();
                }
                else
                    return "";
            }
        }

        public static Failure117RequestPrintInfo GetFailure117RequestPrintbyTelephoneNos(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;
                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault() != null)
                {
                    if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                        switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                    if (switchPortID != 0)
                    {
                        Bucht bucht = context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault();
                        if (bucht != null)
                        {
                            if (bucht.PCMPortID == null)
                            {
                                return context.Buchts.Where(t => t.SwitchPortID == switchPortID)
                                                  .Select(t => new Failure117RequestPrintInfo
                                                  {
                                                      Bucht = "ردیف : " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.BuchtNo.ToString(),
                                                      BuchtPCM = "-",
                                                      BuchtADSL = (HasADSLbyTelephonenNo(telephoneNo) != null) ? "ردیف : ،" + HasADSLbyTelephonenNo(telephoneNo).ADSLRadif + " ، طبقه : " + HasADSLbyTelephonenNo(telephoneNo).ADSLTabaghe + " ، اتصالی : " + HasADSLbyTelephonenNo(telephoneNo).ADSLEtesali : "-",
                                                      OtherBucht = GetADSLPAPPortInfo(telephoneNo),//(HasADSLbyTelephonenNo(telephoneNo) != null) ? "ADSL " + "ردیف : ،" + HasADSLbyTelephonenNo(telephoneNo).ADSLRadif + " ، طبقه : " + HasADSLbyTelephonenNo(telephoneNo).ADSLTabaghe + " ، اتصالی : " + HasADSLbyTelephonenNo(telephoneNo).ADSLEtesali : "-",
                                                      //CabinetInputNumber = t.CabinetInput.InputNumber.ToString(),
                                                      CabinetNo = "شماره : " + t.CabinetInput.Cabinet.CabinetNumber.ToString() + " ، ورودی : " + t.CabinetInput.InputNumber.ToString(),
                                                      PostNo = "پست : " + t.PostContact.Post.Number.ToString() + " ، اتصالی : " + t.PostContact.ConnectionNo.ToString(),
                                                      //ConnectionNo = t.PostContact.ConnectionNo.ToString(),
                                                  }).SingleOrDefault();
                            }
                            else
                            {
                                return context.Buchts.Where(t => t.SwitchPortID == switchPortID)
                                                  .Select(t => new Failure117RequestPrintInfo
                                                  {
                                                      Bucht = GetPCMOutPropertybyCabinetInputID((long)t.CabinetInputID),
                                                      BuchtPCM = "ردیف : " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.BuchtNo.ToString(),
                                                      BuchtADSL = (HasADSLbyTelephonenNo(telephoneNo) != null) ? "ردیف : ،" + HasADSLbyTelephonenNo(telephoneNo).ADSLRadif + " ، طبقه : " + HasADSLbyTelephonenNo(telephoneNo).ADSLTabaghe + " ، اتصالی : " + HasADSLbyTelephonenNo(telephoneNo).ADSLEtesali : "-",
                                                      OtherBucht = "PCM " + "ردیف : " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " ، طبقه : " + t.VerticalMDFRow.VerticalRowNo.ToString() + " ، اتصالی : " + t.BuchtNo.ToString(),
                                                      //CabinetInputNumber = t.CabinetInput.InputNumber.ToString(),
                                                      CabinetNo = "شماره : " + t.CabinetInput.Cabinet.CabinetNumber.ToString() + " ، ورودی : " + t.CabinetInput.InputNumber.ToString(),
                                                      PostNo = "پست : " + t.PostContact.Post.Number.ToString() + " ، اتصالی : " + t.PostContact.ConnectionNo.ToString(),
                                                      //ConnectionNo = t.PostContact.ConnectionNo.ToString(),
                                                  }).SingleOrDefault();
                            }
                        }
                        else
                            return new Failure117RequestPrintInfo
                            {
                                Bucht = " - ",
                                BuchtPCM = "",
                                BuchtADSL = "",
                                OtherBucht = "",
                                //CabinetInputNumb
                                CabinetNo = "",
                                PostNo = "",
                                //ConnectionNo
                            };
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }

        public static string GetADSLPAPPortInfo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.Status == (byte)DB.ADSLPAPPortStatus.Instal).SingleOrDefault();

                if (port != null)
                    return "ADSL" + "ردیف : " + port.RowNo + " ، طبقه : " + port.ColumnNo + " ، اتصالی : " + port.BuchtNo;
                else
                    return " - ";
            }
        }

        public static int GetCabinetNobyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;

                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                    switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                if (switchPortID != 0)
                {
                    if (context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList().Count() == 1)
                    {
                        Bucht bucht = context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault();

                        if (bucht.CabinetInputID != null)
                            return context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault().CabinetInput.Cabinet.CabinetNumber;
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        public static int GetPostNobyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = 0;

                if (context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID != null)
                    switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                if (switchPortID != 0)
                {
                    if (context.Buchts.Where(t => t.SwitchPortID == switchPortID).ToList().Count() == 1)
                    {
                        Bucht bucht = context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault();

                        if (bucht.ConnectionID != null)
                            return context.Buchts.Where(t => t.SwitchPortID == switchPortID).SingleOrDefault().PostContact.Post.Number;
                        else
                            return 0;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
        }

        public static TechnicalInfoFailure117 HasADSLbyTelephonenNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int switchPortID = (int)context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault().SwitchPortID;

                long aDSLBuchtID = 0;
                if (context.Buchts.Where(t => t.SwitchPortID == switchPortID && t.BuchtIDConnectedOtherBucht != null).SingleOrDefault() != null)
                    aDSLBuchtID = (long)context.Buchts.Where(t => t.SwitchPortID == switchPortID && t.BuchtIDConnectedOtherBucht != null).SingleOrDefault().BuchtIDConnectedOtherBucht;

                if (aDSLBuchtID != 0)
                    return context.Buchts.Where(t => t.ID == aDSLBuchtID)
                                          .Select(t => new TechnicalInfoFailure117
                                          {
                                              ADSLRadif = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                              ADSLTabaghe = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                              ADSLEtesali = t.BuchtNo.ToString(),
                                              RADIF = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString(),
                                              TABAGHE = t.VerticalMDFRow.VerticalRowNo.ToString(),
                                              ETESALII = t.BuchtNo.ToString(),
                                              BOOKHT_TYPE_NAME = "مشخصه ADSL"
                                          }).SingleOrDefault();
                else
                    return null;
            }
        }

        public static TechnicalInfoFailure117 ADSLPAPbyTelephoneNo(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo && t.Status == (byte)DB.ADSLPAPPortStatus.Instal).SingleOrDefault();

                TechnicalInfoFailure117 aDSLTechnicalInfo = null;
                if (port != null)
                {
                    aDSLTechnicalInfo = new TechnicalInfoFailure117();

                    aDSLTechnicalInfo.ADSLRadif = port.RowNo.ToString();
                    aDSLTechnicalInfo.ADSLTabaghe = port.ColumnNo.ToString();
                    aDSLTechnicalInfo.ADSLEtesali = port.BuchtNo.ToString();

                    aDSLTechnicalInfo.RADIF = port.RowNo.ToString();
                    aDSLTechnicalInfo.TABAGHE = port.ColumnNo.ToString();
                    aDSLTechnicalInfo.ETESALII = port.BuchtNo.ToString();

                    aDSLTechnicalInfo.BOOKHT_TYPE_NAME = "مشخصه ADSL";
                }

                return aDSLTechnicalInfo;
            }
        }

        public static int GetCountTotalTelephone(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.Status == (byte)DB.TelephoneStatus.Connecting && centerIDs.Contains(t.CenterID)).Count();
            }
        }

        public static int GetCountFailureTotal(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                    (t.RequestTypeID == (byte)DB.RequestType.Failure117)).Count();
            }
        }

        public static int GetCountLineStatus(List<int> centerIDs, DateTime? fromDate, DateTime? toDate, int lineStatus)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate) &&
                                                      (t.LineStatusID != null && t.LineStatusID == lineStatus)).Count();
            }
        }

        public static int GetCountFilaureStatus(List<int> centerIDs, DateTime? fromDate, DateTime? toDate, int fialureStatusID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate) &&
                                                      (t.NetworkDate != null) &&
                                                      (t.FailureStatusID != null && t.FailureStatusID == fialureStatusID)).Count();
            }
        }

        public static int GetCountFilaureParentStatus(List<int> centerIDs, DateTime? fromDate, DateTime? toDate, int fialureStatusParentID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate) &&
                                                      (t.NetworkDate != null) &&
                                                      (t.FailureStatusID != null && t.Failure117FailureStatus.ParentID == fialureStatusParentID)).Count();
            }
        }

        public static int GetCountRemaindPastMonthSemnan(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate <= fromDate) &&
                                                      (t.EndMDFDate == null || t.EndMDFDate >= fromDate) &&
                                                      (t.Request.EndDate == null || t.Request.EndDate >= fromDate)).Count();
            }
        }

        public static int GetCountFilaureForm(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Failure117.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Failure117.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Failure117.Request.InsertDate <= toDate) &&
                                                      (!fromDate.HasValue || t.Failure117.MDFDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Failure117.MDFDate <= toDate)).Count();
            }
        }

        public static int GetCountFilaureFormandCancelation(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Failure117.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Failure117.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Failure117.Request.InsertDate <= toDate) &&
                                                      (!fromDate.HasValue || t.Failure117.MDFDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Failure117.MDFDate <= toDate) &&
                                                      (t.Failure117.Request.IsCancelation == true)).Count();
            }
        }

        public static string GetCountFailureRepeatative(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                   (t.RequestTypeID == (byte)DB.RequestType.Failure117) &&
                                                   (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                   (!toDate.HasValue || t.InsertDate <= toDate)).Count().ToString();
            }
        }

        public static int GetCountFailurebyTime(int fromTime, int toTime, List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.FailureForms.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Failure117.Request.CenterID)) &&
                                                       (!fromDate.HasValue || t.Failure117.Request.InsertDate >= fromDate) &&
                                                       (!toDate.HasValue || t.Failure117.Request.InsertDate <= toDate) &&
                                                       (t.Failure117.Request.EndDate != null && t.Failure117.EndMDFDate >= fromDate && t.Failure117.EndMDFDate <= toDate && t.Failure117.NetworkDate != null) &&
                                                       ((t.GetNetworkFormDate.Value - t.GiveNetworkFormDate.Value).TotalHours > fromTime) &&
                                                       ((t.GetNetworkFormDate.Value - t.GiveNetworkFormDate.Value).TotalHours <= toTime)).Count();
                //((DateTime.Compare(new DateTime(t.GetNetworkFormDate.Value.Year, t.GetNetworkFormDate.Value.Month, t.GetNetworkFormDate.Value.Day, 0, 0, 0), new DateTime(t.GiveNetworkFormDate.Value.Year, t.GiveNetworkFormDate.Value.Month, t.GiveNetworkFormDate.Value.Day, 0, 0, 0)) * 24) + ((t.GetNetworkFormDate.Value.Hour >= t.GiveNetworkFormDate.Value.Hour) ? (t.GetNetworkFormDate.Value.Hour - t.GiveNetworkFormDate.Value.Hour) : (24 - t.GiveNetworkFormDate.Value.Hour + t.GetNetworkFormDate.Value.Hour)) + ((t.GetNetworkFormDate.Value.Minute >= t.GiveNetworkFormDate.Value.Minute) ? 1 : 0)) > fromTime &&
                //((DateTime.Compare(new DateTime(t.GetNetworkFormDate.Value.Year, t.GetNetworkFormDate.Value.Month, t.GetNetworkFormDate.Value.Day, 0, 0, 0), new DateTime(t.GiveNetworkFormDate.Value.Year, t.GiveNetworkFormDate.Value.Month, t.GiveNetworkFormDate.Value.Day, 0, 0, 0)) * 24) + ((t.GetNetworkFormDate.Value.Hour >= t.GiveNetworkFormDate.Value.Hour) ? (t.GetNetworkFormDate.Value.Hour - t.GiveNetworkFormDate.Value.Hour) : (24 - t.GiveNetworkFormDate.Value.Hour + t.GetNetworkFormDate.Value.Hour)) + ((t.GetNetworkFormDate.Value.Minute >= t.GiveNetworkFormDate.Value.Minute) ? 1 : 0)) <= toTime).Count();
            }
        }

        //public static int GetCountFailurebyTimeSemnan(int fromTime, int toTime, List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Failure117s.Where(t =>  (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
        //                                               (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
        //                                               (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
        //                                               (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate ) &&
        //                                               ((DateTime.Compare(new DateTime(t.EndMDFDate.Value.Year, t.EndMDFDate.Value.Month, t.EndMDFDate.Value.Day, 0, 0, 0), new DateTime(t.Request.InsertDate.Year, t.Request.InsertDate.Month, t.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((t.EndMDFDate.Value.Hour >= t.Request.InsertDate.Hour) ? (t.EndMDFDate.Value.Hour - t.Request.InsertDate.Hour) : (24 - t.Request.InsertDate.Hour + t.Request.InsertDate.Hour)) + ((t.EndMDFDate.Value.Minute >= t.Request.InsertDate.Minute) ? 1 : 0)) > fromTime &&
        //                                               ((DateTime.Compare(new DateTime(t.EndMDFDate.Value.Year, t.EndMDFDate.Value.Month, t.EndMDFDate.Value.Day, 0, 0, 0), new DateTime(t.Request.InsertDate.Year, t.Request.InsertDate.Month, t.Request.InsertDate.Day, 0, 0, 0)) * 24) + ((t.EndMDFDate.Value.Hour >= t.Request.InsertDate.Hour) ? (t.EndMDFDate.Value.Hour - t.Request.InsertDate.Hour) : (24 - t.Request.InsertDate.Hour + t.EndMDFDate.Value.Hour)) + ((t.EndMDFDate.Value.Minute >= t.Request.InsertDate.Minute) ? 1 : 0)) <= toTime).Count();
        //    }
        //}

        public static List<Failure117> GetFailurebyTimeSemnanforTime(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                       (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                       (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                       (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate)).ToList();
            }
        }

        public static int GetCountFailurebyTimeSemnan(int fromTime, int toTime, List<FailureTimeReportInfo> failureList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return failureList.Where(t => ((DateTime.Compare(new DateTime(t.EndMDFDate.Year, t.EndMDFDate.Month, t.EndMDFDate.Day, 0, 0, 0), new DateTime(t.InsertDate.Year, t.InsertDate.Month, t.InsertDate.Day, 0, 0, 0)) * 24) + ((t.EndMDFDate.Hour >= t.InsertDate.Hour) ? (t.EndMDFDate.Hour - t.InsertDate.Hour) : (24 - t.InsertDate.Hour + t.InsertDate.Hour)) + ((t.EndMDFDate.Minute >= t.InsertDate.Minute) ? 1 : 0)) > fromTime &&
                                              ((DateTime.Compare(new DateTime(t.EndMDFDate.Year, t.EndMDFDate.Month, t.EndMDFDate.Day, 0, 0, 0), new DateTime(t.InsertDate.Year, t.InsertDate.Month, t.InsertDate.Day, 0, 0, 0)) * 24) + ((t.EndMDFDate.Hour >= t.InsertDate.Hour) ? (t.EndMDFDate.Hour - t.InsertDate.Hour) : (24 - t.InsertDate.Hour + t.EndMDFDate.Hour)) + ((t.EndMDFDate.Minute >= t.InsertDate.Minute) ? 1 : 0)) <= toTime).Count();
            }
        }

        public static string GetCountSpeedThisMonth(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            string result = "";
            using (MainDataContext context = new MainDataContext())
            {
                List<FailureSpeedInfo> speedTimeList = context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                   (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                   (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                   (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate))
                                                   .Select(t => new FailureSpeedInfo
                                                   {
                                                       InsertDate = t.Request.InsertDate,
                                                       EndDate = (DateTime)t.EndMDFDate
                                                   }).ToList();
                int minute = 0;
                foreach (FailureSpeedInfo item in speedTimeList)
                {
                    minute = minute + Convert.ToInt32((item.EndDate - item.InsertDate).TotalMinutes);
                }

                int avarage = minute / speedTimeList.Count();

                int hour = avarage / 60;
                int min = avarage - hour * 60;

                return string.Format("{0} : {1}", hour.ToString(), min);
            }

        }

        public static int GetCountTotalThisMonth(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                   (t.RequestTypeID == (byte)DB.RequestType.Failure117) &&
                                                   (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                   (!toDate.HasValue || t.InsertDate <= toDate)).Count();
            }
        }

        public static int GetCountCancelationThisMonth(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                   (t.RequestTypeID == (byte)DB.RequestType.Failure117) &&
                                                   (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                   (!toDate.HasValue || t.InsertDate <= toDate) &&
                                                   (t.IsCancelation == true)).Count();
            }
        }

        public static int GetCountRepetitive(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int count = 0;

                List<long> IDs = context.Requests.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                             (t.RequestTypeID == (byte)DB.RequestType.Failure117) &&
                                                             (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                             (!toDate.HasValue || t.InsertDate <= toDate) &&
                                                             (t.TelephoneNo != 2333388205)).Select(t => t.ID).ToList();

                foreach (long id in IDs)
                {
                    long telephoneNo = (long)context.Requests.Where(t => t.ID == id).SingleOrDefault().TelephoneNo;
                    count = count + context.Requests.Where(t => (!fromDate.HasValue || t.InsertDate >= fromDate) &&
                                                                (!toDate.HasValue || t.InsertDate <= toDate) &&
                                                                (t.TelephoneNo != 2333388205) &&
                                                                (t.TelephoneNo == telephoneNo && t.ID != id)).ToList().Count();
                }

                return count;
            }
        }

        public static string GetCountCompleteThisMonth(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                   (t.Request.RequestTypeID == (byte)DB.RequestType.Failure117) &&
                                                   (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                   (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                   (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate)).Count().ToString();
            }
        }

        public static int GetCountRemaindThisMonthMDF(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate == null || t.EndMDFDate >= toDate) &&
                                                      (t.MDFDate == null || t.MDFDate >= toDate)).Count();
            }
        }

        public static int GetCountRemaindThisMonthNetwork(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate == null || t.EndMDFDate >= toDate) &&
                                                      (t.MDFDate != null && t.MDFDate <= toDate) &&
                                                      (t.NetworkDate == null || t.NetworkDate >= toDate)).Count();
            }
        }

        public static int GetCountRemaindPastMonth(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate <= fromDate) &&
                                                      (t.MDFDate != null && t.MDFDate <= fromDate) &&
                                                      (t.NetworkDate == null || t.NetworkDate >= fromDate) &&
                                                      (t.Request.EndDate == null || t.EndMDFDate >= fromDate)).Count();
            }
        }

        public static int GetCountMDFCompelete(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate) &&
                                                      (t.MDFDate != null && t.EndMDFDate != null && t.MDFDate == t.EndMDFDate)).Count();
            }
        }

        public static int GetCountSalonDastgah(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.Request.EndDate != null && t.EndMDFDate >= fromDate && t.EndMDFDate <= toDate) &&
                                                      (t.SaloonDate != null)).Count();
            }
        }

        public static string GetCountNetwork(List<int> centerIDs, DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (!fromDate.HasValue || t.Request.InsertDate >= fromDate) &&
                                                      (!toDate.HasValue || t.Request.InsertDate <= toDate) &&
                                                      (t.NetworkDate != null)).Count().ToString();
            }
        }

        public static List<long> GetFailure117UBList(DateTime? fromDate, DateTime? toDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID)) &&
                                                      (fromDate == null || t.Request.InsertDate >= fromDate) &&
                                                      (toDate == null || t.Request.InsertDate < toDate) &&
                                                      (t.LineStatusID == 36))
                                          .Select(t => (long)t.Request.TelephoneNo).Distinct().ToList();


                //.GroupBy(t => new
                //{
                //    TelephoneNo = t.TelephoneNo,
                //    Bucht = t.Bucht,
                //    BuchtPCM = t.BuchtPCM,
                //    Date = t.Date,
                // //   Count = t.Count
                //}).Select(t => new Failure117RequestPrintInfo
                //{
                //    TelephoneNo = t.Key.TelephoneNo,
                //    Bucht = t.Key.Bucht,
                //    BuchtPCM = t.Key.BuchtPCM,
                //    Date = t.Key.Date,
                //    Count = t.Count().ToString()
                //}).ToList();
            }
        }

        public static string GetFailure117LastRequestDate(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime date = context.Failure117s.Where(t => t.Request.TelephoneNo == telephoneNo && (t.LineStatusID == 36)).OrderByDescending(t => t.Request.InsertDate).FirstOrDefault().Request.InsertDate;
                return Date.GetPersianDate(date, Date.DateStringType.Short);
            }
        }

        public static string GetFailure117UBCount(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Failure117> failureList = context.Failure117s.Where(t => t.LineStatusID == 36 && t.Request.TelephoneNo == telephoneNo).ToList();

                if (failureList != null)
                    return failureList.Count().ToString();
                else
                    return "0";
            }
        }

        public static List<Failure117Karaj> GetKarajRequest(string telephoneNo, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)).Select(t => new Failure117Karaj
                    {
                        ID = t.ID,
                        TelephoneNo = t.Request.TelephoneNo.ToString(),
                        CallingNo = t.CallingNo.ToString(),
                        InsertDate = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime)
                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int GetKarajRequestCount(string telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)).Count();
            }
        }

        public static List<FailureUBInfo> GetFailure117UBInfoList(long telephoneNo, List<int> centerIDs, DateTime? insertDate, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117UBs.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                                        (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                                        (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                        (insertDate == null || t.UBDate == insertDate))
                                            .Select(t => new FailureUBInfo
                                            {
                                                TelephoneNo = t.TelephoneNo,
                                                Center = t.Center.Region.City.Name + " : " + t.Center.CenterName,
                                                Bucht = GetFailure117RequestPrintbyTelephoneNos(t.TelephoneNo).Bucht,
                                                OtherBucht = GetFailure117RequestPrintbyTelephoneNos(t.TelephoneNo).OtherBucht,
                                                UBDate = Date.GetPersianDate(t.UBDate, Date.DateStringType.Short),
                                                LastFailureDate = Date.GetPersianDate(context.Failure117s.Where(a => a.Request.TelephoneNo == t.TelephoneNo && a.Request.RequestTypeID == 65).OrderByDescending(a => a.EndMDFDate).FirstOrDefault().EndMDFDate, Date.DateStringType.Short),
                                                Repeatative = context.Failure117UBs.Where(r => r.TelephoneNo == t.TelephoneNo).ToList().Count.ToString()
                                            }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int GetFailure117UBInfoListCount(long telephoneNo, List<int> centerIDs, DateTime? insertDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117UBs.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID) &&
                                                        (telephoneNo == -1 || t.TelephoneNo == telephoneNo) &&
                                                        (centerIDs.Count == 0 || centerIDs.Contains(t.CenterID)) &&
                                                        (insertDate == null || t.UBDate == insertDate)).Count();
            }
        }

        public static List<Failure117UB> GetFailureUBbyDateandCenter(int centerID, DateTime date)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117UBs.Where(t => t.CenterID == centerID && t.UBDate == date).ToList();
            }
        }

        public static List<FailureFormRowInfo> GetFailure117TotelList(List<int> centerIDs, string id, string telephoneNo, DateTime? fromInsertDate, DateTime? toInsertDate, DateTime? fromEndDate, DateTime? toEndDate, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID)) &&
                                                      (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                                      (string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                      (fromInsertDate == null || t.Request.InsertDate >= fromInsertDate) &&
                                                      (toInsertDate == null || t.Request.InsertDate < toInsertDate) &&
                                                      (fromEndDate == null || t.EndMDFDate >= fromEndDate) &&
                                                      (toEndDate == null || t.EndMDFDate < toEndDate))
                                          .Select(t => new FailureFormRowInfo
                                                    {
                                                        RequestID = t.ID,
                                                        TelephoneNo = (long)t.Request.TelephoneNo,
                                                        Center = t.Request.Center.Region.City.Name + " : " + t.Request.Center.CenterName,
                                                        LineStatus = t.Failure117LineStatus.Title,
                                                        FailureStatus = t.Failure117FailureStatus.Title,
                                                        InsertDateString = Date.GetPersianDate(t.Request.InsertDate, Date.DateStringType.DateTime),
                                                        MDFDate = Date.GetPersianDate(t.MDFDate, Date.DateStringType.DateTime),
                                                        MDFPerson = t.MDFPersonnel.FirstName + " " + t.MDFPersonnel.LastName,
                                                        NetworkDate = Date.GetPersianDate(t.NetworkDate, Date.DateStringType.DateTime),
                                                        NetworkOfficer = GetUserFullName(t.NetworkUserID),
                                                        SaloonDate = Date.GetPersianDate(t.SaloonDate, Date.DateStringType.DateTime),
                                                        SaloonUser = GetUserFullName(t.SaloonUserID),
                                                        EndMDFDateString = Date.GetPersianDate(t.EndMDFDate, Date.DateStringType.DateTime),
                                                        EndMDFPerson = t.MDFPersonnel1.FirstName + " " + t.MDFPersonnel1.LastName,
                                                        Step = t.Request.Status.RequestStep.StepTitle + "، " + t.Request.Status.Title
                                                    }).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static int GetFailure117TotelListCount(List<int> centerIDs, string id, string telephoneNo, DateTime? fromInsertDate, DateTime? toInsertDate, DateTime? fromEndDate, DateTime? toEndDate)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Failure117s.Where(t => (DB.CurrentUser.CenterIDs.Contains(t.Request.CenterID)) &&
                                                      (centerIDs.Count == 0 || centerIDs.Contains(t.Request.CenterID)) &&
                                                      (string.IsNullOrWhiteSpace(id) || t.ID.ToString().Contains(id)) &&
                                                      (string.IsNullOrWhiteSpace(telephoneNo) || t.Request.TelephoneNo.ToString().Contains(telephoneNo)) &&
                                                      (fromInsertDate == null || t.Request.InsertDate >= fromInsertDate) &&
                                                      (toInsertDate == null || t.Request.InsertDate < toInsertDate) &&
                                                      (fromEndDate == null || t.Request.EndDate >= fromEndDate) &&
                                                      (toEndDate == null || t.Request.EndDate < toEndDate))
                                          .ToList().Count;
            }
        }

        public static List<Request> GetRequestListKaraj()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.RequestTypeID == 65 && t.IsViewed == false).ToList();
            }
        }
    }
}