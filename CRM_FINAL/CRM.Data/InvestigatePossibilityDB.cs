using CRM.Data.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CRM.Data
{
    public static class InvestigatePossibilityDB
    {
        //public static List<ConnectionSource> GetConnectionSourceByCenterID(int centerID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.PostContacts.Where(t => t.Post.Cabinet.CenterID == centerID).
        //        Select(t => new ConnectionSource
        //        {
        //            CenterID = t.Post.Cabinet.CenterID,
        //            //MDFID = t.Post.Cabinet.MDFID,
        //           // SourceType = t.Post.Cabinet.MDF.Type,
        //           // MDFNumber = t.Post.Cabinet.MDF.Number,
        //            CabinetID = t.Post.CabinetID,
        //            CabinetNumber = t.Post.Cabinet.CabinetNumber,
        //            PostID = t.PostID,
        //            PostNumber = t.Post.Number,
        //            ConnectionType = t.ConnectionType,
        //            ContStatus = t.Status,
        //            SourceID = t.ID,
        //            SourceNumber = t.ConnectionNo.ToString() + "," + t.Buchts.Where(b=>b.ConnectionID==t.Post.PostContacts).SingleOrDefault().PCMChannelNo,
        //            PCMDeviceID = t.PCMDeviceID,
        //            PCMChannelNo = t.PCMChannelNo,
        //            MUID = t.PCMDevice.MUID,
        //            PCMStatus = t.PCMDevice.Status,
        //            PCMBrandName = t.PCMDevice.PCMType.PCMBrandName,
        //            PCMTypeName = t.PCMDevice.PCMType.PCMTypeName,
        //            NormalEtesali = t.Post.Cabinet.CabinetNumber.ToString() + "-" 
        //            + DB.SearchByPropertyName<Bucht>("ConnectionID",t.ID).Select(s=>s.CentralCableNo).Take(1).SingleOrDefault().ToString()+"-"
        //            + t.Post.Number.ToString() + "-" + t.ConnectionNo.ToString(),
        //            PCMEtesali = t.Post.Cabinet.CabinetNumber.ToString() + "-" 
        //            + DB.SearchByPropertyName<Bucht>("ConnectionID",t.ID).Select(s=>s.CentralCableNo).Take(1).SingleOrDefault().ToString()+"-"
        //            + t.Post.Number.ToString() + "-" + t.ConnectionNo.ToString() + "," + t.PCMChannelNo.ToString(),
        //        }
        //          ).ToList();
        //    }
        //}

        public static void SaveInvestigate(Request request, InvestigatePossibility investigate, Bucht bucht, AssignmentInfo assignmentInfo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    request.Detach();
                    DB.Save(request);
                    if (assignmentInfo.BuchtType == (byte)DB.BuchtType.OutLine || assignmentInfo.BuchtType == (byte)DB.BuchtType.InLine)
                    {
                        // رزرو اتصالی پست
                        PostContact Contact = Data.PostContactDB.GetPostContactByID(assignmentInfo.PostContactID ?? 0);
                        Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                        Contact.Detach();
                        DB.Save(Contact);

                        // رزرو بوخت. پی سی ام نیاز به تنظیم اتصالی پست ندارد  
                        Bucht buchtPCM = Data.BuchtDB.GetBuchetByID(assignmentInfo.BuchtID);
                        buchtPCM.Status = (byte)DB.BuchtStatus.Reserve;
                        buchtPCM.Detach();
                        DB.Save(buchtPCM);

                        PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(assignmentInfo.PCMPortIDInBuchtTable ?? 0);
                        PCMPort.Status = (byte)DB.PCMPortStatus.Reserve;
                        PCMPort.Detach();
                        DB.Save(PCMPort);
                    }
                    else
                    {
                        // رزرو اتصالی پست
                        PostContact Contact = Data.PostContactDB.GetPostContactByID(assignmentInfo.PostContactID ?? 0);
                        Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                        Contact.Detach();
                        DB.Save(Contact);

                        // رزرو بوخت  
                        bucht.Status = (byte)DB.BuchtStatus.Reserve;
                        bucht.ConnectionID = assignmentInfo.PostContactID;
                        bucht.Detach();
                        DB.Save(bucht);
                    }
                    investigate.Detach();
                    DB.Save(investigate);
                    ts.Complete();
                }
            }
        }

        public static void UpdateInvestigate(Request request, InvestigatePossibility investigate, Bucht bucht, PostContact oldPostContect, Bucht oldBucht, AssignmentInfo assignmentInfo, AssignmentInfo oldAssignmentInfo, bool modeOfDelete)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (oldPostContect != null)
                    {

                        if (oldAssignmentInfo.BuchtType == (byte)DB.BuchtType.OutLine || oldAssignmentInfo.BuchtType == (byte)DB.BuchtType.InLine)
                        {
                            //تغییر وضعیت اتصالی پست به حالت آزاد
                            oldPostContect.Status = (byte)DB.PostContactStatus.Free;
                            oldPostContect.Detach();
                            DB.Save(oldPostContect);


                            // تغییر وضعیت بوخت سابق به حالت آزاد
                            oldBucht.Status = (byte)DB.BuchtStatus.Free;
                            oldBucht.Detach();
                            DB.Save(oldBucht);


                            PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID(oldAssignmentInfo.PCMPortIDInBuchtTable ?? 0);
                            PCMPort.Status = (byte)DB.PCMPortStatus.Empty;
                            PCMPort.Detach();
                            DB.Save(PCMPort);


                        }
                        else
                        {
                            //تغییر وضعیت اتصالی پست به حالت آزاد
                            oldPostContect.Status = (byte)DB.PostContactStatus.Free;
                            oldPostContect.Detach();
                            DB.Save(oldPostContect);

                            // تغییر وضعیت بوخت سابق به حالت آزاد
                            oldBucht.Status = (byte)DB.BuchtStatus.Free;
                            oldBucht.ConnectionID = null;
                            oldBucht.Detach();
                            DB.Save(oldBucht);

                        }
                    }
                    if (!modeOfDelete)
                        SaveInvestigate(request, investigate, bucht, assignmentInfo);
                    ts.Complete();
                }
            }
        }

        public static void SaveTelAndPort(Request request, InvestigatePossibility investigate, Telephone tel, Bucht bucht)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    request.Detach();
                    DB.Save(request, false);

                    tel.Detach();
                    DB.Save(tel, false);

                    if (investigate != null && bucht.ID != 0)
                    {
                        investigate.Detach();
                        DB.Save(investigate, false);
                    }

                    if (bucht != null && bucht.ID != 0)
                    {
                        bucht.Detach();
                        DB.Save(bucht, false);
                    }

                    ts.Complete();
                }
            }
        }


        public static void SaveTelephoneOfReinstall(Telephone telephone, Bucht bucht, InvestigatePossibility investigate)
        {

            if (telephone != null)
            {
                telephone.Detach();
                DB.Save(telephone);
            }


            if (bucht != null)
            {
                bucht.Detach();
                DB.Save(bucht);
            }
            if (investigate != null)
            {
                investigate.Detach();
                DB.Save(investigate);
            }
        }
        public static List<InvestigatePossibility> GetInvestigatePossibilityByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilities.Where(t => t.RequestID == requestID).ToList();

                // List<InvestigatePossibility> InvestigatePossibilityList = context.InvestigatePossibilities.Where(t => t.RequestID == requestID).ToList();

                //if (InvestigatePossibilityList != null && InvestigatePossibilityList.Count != 0)
                //    return InvestigatePossibilityList;
                //else
                //    return null;
            }
        }
        public static List<InvestigatePossibility> GetInvestigatePossibility()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilities.ToList();
            }
        }

        public static bool CheckFreePassTelephonForAutoForward(Request request, Bucht bucht, InvestigatePossibility investigate, string fullName, string title, string userName, Cabinet cabinet)
        {

            RequestLog requestLog = new RequestLog();
            CRM.Data.Schema.DischargeTelephone dischargeTelephone = new CRM.Data.Schema.DischargeTelephone();
            CRM.Data.Schema.RefundDesposit refundDesposit = new CRM.Data.Schema.RefundDesposit();

            InstallRequest installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(request.ID);
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(installRequest.PassTelephone ?? 0);



            //// اگر تلفن سابق آزاد باشد لاگ ثبت شده آن را استخراج میکند
            if (telephone != null && ((telephone.Status == (byte)DB.TelephoneStatus.Free || telephone.Status == (byte)DB.TelephoneStatus.Discharge) || (request.TelephoneNo == telephone.TelephoneNo)))
            {

                // اگر درخواست 
                int? opticalcabinet = null;
                if (Data.TelephoneDB.CheckTelephoneOnOpticalCabinet(installRequest.PassTelephone ?? 0, out opticalcabinet))
                {
                    if (opticalcabinet != null && opticalcabinet != cabinet.ID)
                        return true;
                }
                else
                {
                    if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
                        return true;
                }

                //
                telephone.InstallAddressID = installRequest.InstallAddressID;
                telephone.CorrespondenceAddressID = installRequest.CorrespondenceAddressID;
                telephone.CustomerID = request.CustomerID;
                telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                //


                //اگر تلفن آزاد است و به پورت متصل است نیاز به تعویض شماره نیست
                if (telephone.SwitchPortID != null)
                {
                    bucht = Data.BuchtDB.GetBuchtByID((long)investigate.BuchtID);
                    bucht.SwitchPortID = telephone.SwitchPortID;
                    //

                    InvestigatePossibilityDB.SaveTelephoneOfReinstall(telephone, bucht, investigate);
                    request.TelephoneNo = telephone.TelephoneNo;
                    request.Detach();
                    DB.Save(request);


                    ////

                    return false;
                }
                else
                {
                    InvestigatePossibilityDB.SaveTelephoneOfReinstall(telephone, null, investigate);
                    request.TelephoneNo = telephone.TelephoneNo;
                    request.Detach();
                    DB.Save(request);
                    return true;
                }

                ////// اگر تلفن قبلی ثبت شده باشد اخرین لاگ مربوط به ان تلفن را استخراج میکند
                //if (installRequest.PassTelephone != null)
                //    requestLog = Data.RequestLogDB.GetLastRequestLogByTelephoneNo(installRequest.PassTelephone ?? 0);

                //int PortID = 0;
                //// اگر اخرین لاگ تخلیه یا استرداد باشد لاگ را استخراج میکند
                //if (requestLog.RequestTypeID == (int)DB.RequestType.Dischargin)
                //{
                //    dischargeTelephone = LogSchemaUtility.Deserialize<CRM.Data.Schema.DischargeTelephone>(requestLog.Description.ToString());
                //    PortID = dischargeTelephone.PortID;
                //}
                //else if (requestLog.RequestTypeID == (int)DB.RequestType.RefundDeposit)
                //{
                //    refundDesposit = LogSchemaUtility.Deserialize<CRM.Data.Schema.RefundDesposit>(requestLog.Description.ToString());
                //    PortID = refundDesposit.SwitchPort ?? 0;
                //}


                //// اگر آخرین لاگ موجود باشد 
                //if (requestLog != null)
                //{

                //    if (PortID != 0)
                //    {

                //        SwitchPort switchPort = Data.SwitchPortDB.GetSwitchPortByID((int)PortID);
                //        //// اگر پورت آزاد باشد
                //        if (switchPort != null && telephone.SwitchPortID == switchPort.ID)
                //        {

                //            //
                //            bucht = Data.BuchtDB.GetBuchtByID((long)investigate.BuchtID);
                //            bucht.SwitchPortID = switchPort.ID;
                //            //

                //            InvestigatePossibilityDB.SaveTelephoneOfReinstall(telephone, switchPort, bucht, investigate);
                //            request.TelephoneNo = telephone.TelephoneNo;
                //            request.Detach();
                //            DB.Save(request);


                //            ////

                //            return false;
                //            ////

                //        }
                //        ////
                //        else
                //        {
                //            //// اگر تلفن آزاد باشد ولی پورت نه فقط تلفن نصب میشود
                //            InvestigatePossibilityDB.SaveTelephoneOfReinstall(telephone, null, null, investigate);
                //            request.TelephoneNo = telephone.TelephoneNo;
                //            request.Detach();
                //            DB.Save(request);
                //            return true;
                //            ////
                //        }

                //    }
                //}

                //else
                //{
                //    ////  اگر اطلاعات از لاگ موجود نباشد ولی تلفن آزاد باشد تلفن ثبت میشود
                //    InvestigatePossibilityDB.SaveTelephoneOfReinstall(telephone, null, null, investigate);
                //    return true;
                //}
            }

            return true;
        }

        public static bool CheckChangeCabinetForAutoForward(Request _request, ChangeLocation changeLocation, Bucht newBucht, SpecialCondition specialCondition, Cabinet cabinet)
        {
            if (changeLocation.OldTelephone != null)
            {
                AssignmentInfo oldAssignmetInfo = DB.GetAllInformationByTelephoneNo(changeLocation.OldTelephone ?? 0);

                int CabinetUsageID = cabinet.CabinetUsageType;

                int oldCabinetUsageID = (int)oldAssignmetInfo.CabinetUsageTypeID;

                // اگر تغییر مکان از بوخت معمولی به بوخت نوری یا بلعکس است
                // در این حالت نیاز به تعویض شماره است
                if ((CabinetUsageID != (int)DB.CabinetUsageType.OpticalCabinet && oldCabinetUsageID != (int)DB.CabinetUsageType.OpticalCabinet) && (CabinetUsageID != (int)DB.CabinetUsageType.WLL && oldCabinetUsageID != (int)DB.CabinetUsageType.WLL))
                {
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                }

                // اگر تغییر مکان از بوخت نوری به بوخت نوری دیگری است
                // در این حالت نیاز به تعویض شماره است
                else if (
                             (CabinetUsageID == (int)DB.CabinetUsageType.OpticalCabinet && oldCabinetUsageID == (int)DB.CabinetUsageType.OpticalCabinet) && (cabinet.ID == oldAssignmetInfo.CabinetID)
                          || (CabinetUsageID == (int)DB.CabinetUsageType.WLL && oldCabinetUsageID == (int)DB.CabinetUsageType.WLL) && (cabinet.ID == oldAssignmetInfo.CabinetID)
                       )
                {
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = true;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                }
                else
                {
                    if (specialCondition == null)
                    {
                        specialCondition = new SpecialCondition();
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, true);
                    }
                    else
                    {
                        specialCondition.RequestID = _request.ID;
                        specialCondition.EqualityOfBuchtTypeCusromerSide = false;
                        specialCondition.Detach();
                        DB.Save(specialCondition, false);
                    }
                }

                return true;
            }
            else
            {
                throw new Exception("تلفن یافت نشد");
            }
        }

        public static InvestigatePossibility GetInvestigatePossibilityByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilities.Where(t => t.ID == id).SingleOrDefault();
            }
        }

        public static void E1Save(AssignmentInfo assingmentInfo, E1 e1, Request request, Bucht bucht, E1Link e1Link, Bucht otherBucht, InvestigatePossibility investigate)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                // رزرو اتصالی پست
                PostContact Contact = Data.PostContactDB.GetPostContactByID(assingmentInfo.PostContactID ?? 0);
                Contact.Status = (byte)DB.PostContactStatus.FullBooking;
                Contact.Detach();
                DB.Save(Contact);

                // رزرو بوخت  
                bucht.Status = (byte)DB.BuchtStatus.Reserve;
                bucht.ConnectionID = assingmentInfo.PostContactID;
                bucht.Detach();
                DB.Save(bucht);

                if (otherBucht != null && otherBucht.ID != 0)
                {
                    // رزرو بوخت دیگر  
                    otherBucht.Status = (byte)DB.BuchtStatus.Reserve;
                    otherBucht.Detach();
                    DB.Save(otherBucht);
                    e1Link.OtherBuchtID = otherBucht.ID;
                }

                investigate.Detach();
                DB.Save(investigate);


                e1Link.InvestigatePossibilityID = investigate.ID;
                e1Link.Detach();
                DB.Save(e1Link, false);

                request.Detach();
                DB.Save(request, false);

                ts.Complete();
            }
        }

        public static void E1Update(AssignmentInfo oldAssignmentInfo, AssignmentInfo assingmentInfo, E1 e1, Request request, Bucht bucht, Bucht oldBucht, E1Link e1Link, Bucht oldOtherBucht, Bucht otherBucht, InvestigatePossibility investigate)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                // رزرو اتصالی پست
                PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
                Contact.Status = (byte)DB.PostContactStatus.Free;
                Contact.Detach();
                DB.Save(Contact);

                // آزاد سازی بوخت
                oldBucht.Status = (byte)DB.BuchtStatus.Free;
                oldBucht.ConnectionID = null;
                oldBucht.Detach();
                DB.Save(oldBucht);

                // آزاد سازی بوخت
                if (oldOtherBucht != null)
                {
                    oldOtherBucht.Status = (byte)DB.BuchtStatus.Free;
                    oldOtherBucht.Detach();
                    DB.Save(oldOtherBucht);
                }
                Data.InvestigatePossibilityDB.E1Save(assingmentInfo, e1, request, bucht, e1Link, otherBucht, investigate);

                ts.Complete();
            }
        }

        //public static void privateWireSave(AssignmentInfo assingmentInfo, PrivateWire _privateWire, Request _request, Bucht bucht)
        //{

        //    using(TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        _request.Detach();
        //        DB.Save(_request);

        //    // رزرو اتصالی پست
        //    PostContact Contact = Data.PostContactDB.GetPostContactByID(assingmentInfo.PostContactID ?? 0);
        //    Contact.Status = (byte)DB.PostContactStatus.FullBooking;
        //    Contact.Detach();
        //    DB.Save(Contact);

        //    // رزرو بوخت  
        //    bucht.Status = (byte)DB.BuchtStatus.Reserve;
        //    bucht.ConnectionID = assingmentInfo.PostContactID;
        //    bucht.Detach();
        //    DB.Save(bucht);


        //    _privateWire.Detach();
        //    DB.Save(_privateWire);
        //    Subts.Complete();
        //    }
        //}

        //public static void privateWireUpdate(AssignmentInfo oldAssignmentInfo, AssignmentInfo assingmentInfo, PrivateWire privateWire, Request _request, Bucht bucht, Bucht oldBucht)
        //{
        //    using (TransactionScope SubUpdatets = new TransactionScope(TransactionScopeOption.Required))
        //    {
        //        // آزاد سازی اتصالی پست
        //        PostContact Contact = Data.PostContactDB.GetPostContactByID(oldAssignmentInfo.PostContactID ?? 0);
        //        Contact.Status = (byte)DB.PostContactStatus.Free;
        //        Contact.Detach();
        //        DB.Save(Contact);

        //        // آزاد سازی بوخت  
        //        oldBucht.Status = (byte)DB.BuchtStatus.Free;
        //        oldBucht.ConnectionID = null;
        //        oldBucht.Detach();
        //        DB.Save(oldBucht);

        //     //   Data.InvestigatePossibilityDB.privateWireSave(assingmentInfo, privateWire, _request, bucht);

        //        SubUpdatets.Complete();
        //    }
        //}

        public static bool CheckCabinetShare(Cabinet cabinet)
        {
            using (MainDataContext context = new MainDataContext())
            {
                int cabinetShare = 0;
                int.TryParse(DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.ApplyCabinetShare)), out cabinetShare);

                if (cabinetShare == 0) return false;

                int CabinetInputCount = context.Buchts.Where(t => t.CabinetInput.CabinetID == cabinet.ID && t.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && t.PCMPortID == null).Count();

                int CabinetInputIsFreeCount = context.Buchts.Where(t => t.CabinetInput.CabinetID == cabinet.ID && t.Status == (byte)DB.BuchtStatus.Free && t.CabinetInput.Status == (byte)DB.CabinetInputStatus.healthy && t.PCMPortID == null).Count();

                if ((CabinetInputCount * cabinetShare / 100) > (CabinetInputIsFreeCount - 1))
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
        }

        public static InvestigateInfo GetInvestigateInfoByRequestIDDayri(long reuqestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilities.Where(t => t.RequestID == reuqestID)
                        .Select(t => new InvestigateInfo
                        {
                            BuchtInfo = "ردیف : " + t.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.Bucht.BuchtNo,
                            Cabinet = t.Bucht.CabinetInput.Cabinet.CabinetNumber + "(" + t.Bucht.CabinetInput.Cabinet.CabinetUsageType1.Name + ")" + "-" + t.Bucht.CabinetInput.InputNumber,
                            MDF = t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "(" + t.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description + ")",
                            Post = t.Bucht.PostContact.Post.Number + "-" + t.Bucht.PostContact.ConnectionNo
                        }).SingleOrDefault();
            }
        }

        public static List<WiringGroupedInfo> GetInvestigatePossibilityInfoByRequestIDs(List<long> requestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.InvestigatePossibilities
                    //join with ADSLPAPPort
                               .GroupJoin(context.ADSLPAPPorts, ip => ip.Request.TelephoneNo, p => p.TelephoneNo, (ip, p) => new { InvestigatePossibilitie = ip, ADSLPAPPort = p })
                               .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (ip, p) => new { InvestigatePossibilitie = ip.InvestigatePossibilitie, ADSLPAPPort = p })
                               .Where(t => requestIDs.Contains((long)t.InvestigatePossibilitie.RequestID)).Select(t =>
                    new WiringGroupedInfo
                    {
                        RequestID = (long)t.InvestigatePossibilitie.RequestID,
                        TelephonNo = t.InvestigatePossibilitie.Request.TelephoneNo,
                        Cabinet = t.InvestigatePossibilitie.Bucht.CabinetInput.Cabinet.CabinetNumber,
                        CabinetInput = t.InvestigatePossibilitie.Bucht.CabinetInput.InputNumber,
                        Post = t.InvestigatePossibilitie.PostContact.Post.Number,
                        PostContact = t.InvestigatePossibilitie.PostContact.ConnectionNo,
                        PostContactID = t.InvestigatePossibilitie.PostContact.ID,
                        Address = t.InvestigatePossibilitie.Request.InstallRequests.Take(1).SingleOrDefault().Address.AddressContent,

                        Customer = string.Format("{0} {1}", t.InvestigatePossibilitie.Request.Customer.FirstNameOrTitle, t.InvestigatePossibilitie.Request.Customer.LastName),
                        MDF = t.InvestigatePossibilitie.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number,
                        Column = t.InvestigatePossibilitie.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        Row = t.InvestigatePossibilitie.Bucht.VerticalMDFRow.VerticalRowNo,
                        Bucht = t.InvestigatePossibilitie.Bucht.BuchtNo,
                        BuchtID = (long)t.InvestigatePossibilitie.BuchtID,

                        PCMMDF = context.Buchts.Where(t2 => t2.CabinetInputID == t.InvestigatePossibilitie.Bucht.CabinetInputID && t2.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t2.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t2 => t2.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number).SingleOrDefault(),
                        PCMColumn = context.Buchts.Where(t2 => t2.CabinetInputID == t.InvestigatePossibilitie.Bucht.CabinetInputID && t2.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t2.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t2 => t2.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo).SingleOrDefault(),
                        PCMRow = context.Buchts.Where(t2 => t2.CabinetInputID == t.InvestigatePossibilitie.Bucht.CabinetInputID && t2.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t2.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t2 => t2.VerticalMDFRow.VerticalRowNo).SingleOrDefault(),
                        PCMBucht = context.Buchts.Where(t2 => t2.CabinetInputID == t.InvestigatePossibilitie.Bucht.CabinetInputID && t2.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t2.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t2 => t2.BuchtNo).SingleOrDefault(),
                        PCMBuchtID = context.Buchts.Where(t2 => t2.CabinetInputID == t.InvestigatePossibilitie.Bucht.CabinetInputID && t2.BuchtTypeID == (int)DB.BuchtType.CustomerSide && t2.Status == (int)DB.BuchtStatus.AllocatedToInlinePCM).Select(t2 => t2.ID).SingleOrDefault(),


                        ADSLColumn = t.ADSLPAPPort.RowNo,
                        ADSLRow = t.ADSLPAPPort.ColumnNo,
                        ADSLBucht = t.ADSLPAPPort.BuchtNo,

                    }
                );


                return x.ToList();
            }
        }

        public static List<InvestigatePossibility> GetInvestigatePossibilityByRequestIDs(List<long> RequestIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.InvestigatePossibilities.Where(t => RequestIDs.Contains((long)t.RequestID)).ToList();
            }
        }

        public static void ChangePostInvestigatePossibilityWaitingList(List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo)
        {

            List<InvestigatePossibilityWaitinglist> _investigatePossibilityWaitinglist = InvestigatePossibilityWaitinglistDB.GetPostInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);
            _investigatePossibilityWaitinglist.ForEach(i =>
            {
                InvestigatePossibilityWaitngListChangeInfo item = investigatePossibilityWaitngListChangeInfo.Where(t => t.oldPostID == i.PostID).SingleOrDefault();

                if (item != null && item.newPostID.HasValue)
                {
                    i.PostID = item.newPostID;
                    i.CabinetID = item.newCabinetID;
                }
            });

            _investigatePossibilityWaitinglist.ForEach(t => t.Detach());
            DB.UpdateAll(_investigatePossibilityWaitinglist);

        }

        public static void ChangeCabinetInvestigatePossibilityWaitingList(List<InvestigatePossibilityWaitngListChangeInfo> investigatePossibilityWaitngListChangeInfo)
        {

            List<InvestigatePossibilityWaitinglist> _investigatePossibilityWaitinglist = InvestigatePossibilityWaitinglistDB.GetCabinetInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo);


            _investigatePossibilityWaitinglist.ForEach(i =>
            {
                InvestigatePossibilityWaitngListChangeInfo item = investigatePossibilityWaitngListChangeInfo.Where(t => t.oldPostID == i.PostID).SingleOrDefault();
                if (item != null && item.newPostID.HasValue)
                {
                    i.PostID = item.newPostID;
                    i.CabinetID = item.newCabinetID;
                }
            });

            _investigatePossibilityWaitinglist.ForEach(t => t.Detach());
            DB.UpdateAll(_investigatePossibilityWaitinglist);


            _investigatePossibilityWaitinglist.Clear();
            _investigatePossibilityWaitinglist = InvestigatePossibilityWaitinglistDB.GetCabinetInvestigatePossibilityWaitingList(investigatePossibilityWaitngListChangeInfo).Where(t => t.PostID == null).ToList();

            _investigatePossibilityWaitinglist.ForEach(i =>
            {
                i.CabinetID = investigatePossibilityWaitngListChangeInfo.Where(t2 => t2.oldCabinetID == i.CabinetID).Take(1).SingleOrDefault().newCabinetID;
            });

            _investigatePossibilityWaitinglist.ForEach(t => t.Detach());
            _investigatePossibilityWaitinglist.ForEach(i =>
            {
                i.CabinetID = investigatePossibilityWaitngListChangeInfo.Where(t2 => t2.oldCabinetID == i.CabinetID).Take(1).SingleOrDefault().newCabinetID;
                i.Detach();
            });

            DB.UpdateAll(_investigatePossibilityWaitinglist);

        }
    }
    #region Custom Class

    public class ConnectionSource
    {
        public int CenterID { get; set; }
        public int MDFID { get; set; }
        public byte SourceType { get; set; }
        public string SourceDescriptionType { get; set; }
        public int? MDFNumber { get; set; }
        public int CabinetID { get; set; }
        public string CabinetNumber { get; set; }
        public int PostID { get; set; }
        public int PostNumber { get; set; }
        public byte? ConnectionType { get; set; }
        public byte ContStatus { get; set; }
        public long SourceID { get; set; }
        public string SourceNumber { get; set; }
        public int? PCMDeviceID { get; set; }
        public byte? PCMChannelNo { get; set; }
        public string MUID { get; set; }
        public byte? PCMStatus { get; set; }
        public string PCMBrandName { get; set; }
        public string PCMTypeName { get; set; }
        public string NormalEtesali { get; set; }
        public string PCMEtesali { get; set; }



    }
    #endregion Custom Class

}
