using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for BuchtSwitchingMDFForm.xaml
    /// </summary>
    public partial class BuchtSwitchingNetworkFrom : Local.RequestFormBase
    {
        private long requestID;
        private Request request { get; set; }
        private CRM.Data.BuchtSwitching buchtSwitching { get; set; }

        private UserControls.BuchtSwitchingUserControl _buchtSwitchingUserControl { get; set; }

        public BuchtSwitchingNetworkFrom()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
        }

        public BuchtSwitchingNetworkFrom(long requestID)
            : this()
        {
            this.requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            buchtSwitching = Data.BuchtSwitchingDB.GetBuchtSwitchingByID(requestID);

            if (buchtSwitching.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                buchtSwitching.NetworkAccomplishmentDate = dateTime.Date;
                buchtSwitching.NetworkAccomplishmentTime = dateTime.ToShortTimeString();
            }

            request = Data.RequestDB.GetRequestByID(requestID);

            _buchtSwitchingUserControl = new UserControls.BuchtSwitchingUserControl(requestID);
            _buchtSwitchingUserControl.BuchtInformation.Visibility = Visibility.Collapsed;
            TranslationInfo.Content = _buchtSwitchingUserControl;
            TranslationInfo.DataContext = _buchtSwitchingUserControl;


            AccomplishmentGroupBox.DataContext = buchtSwitching;
        }
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {



                    request.Detach();
                    DB.Save(request, false);

                    buchtSwitching.Detach();
                    DB.Save(buchtSwitching, false);

                    IsSaveSuccess = true;
                    ts2.Complete();
                }

            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;
        }

        public override bool Forward()
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {

                    Save();
                    this.RequestID = request.ID;
                    if (IsSaveSuccess)
                    {
                        DoWork();
                        IsForwardSuccess = true;
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsForwardSuccess;

        }

        public override bool Deny()
        {

            try
            {
                base.RequestID = request.ID;
                if (buchtSwitching.DateOfFinal == null)
                {
                    IsRejectSuccess = true;
                }
                else
                {
                    IsRejectSuccess = false;
                    Folder.MessageBox.ShowWarning("بعد از تایید نهایی امکان رد درخواست نمی باشد.");
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }

        private void DoWork()
        {
            try
            {
                if (buchtSwitching.DateOfFinal == null)
                {
                    DateTime currentDataTime = DB.GetServerDate();
                    using (TransactionScope ts3 = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(0)))
                    {

                        RequestLog requestLog = new RequestLog();
                        requestLog.IsReject = false;
                        requestLog.RequestTypeID = (int)DB.RequestType.BuchtSwiching;
                        requestLog.TelephoneNo = (long)request.TelephoneNo;
                        requestLog.RequestID = request.ID;
                        Data.Schema.BuchtSwitching buchtSwitchingLog = new Data.Schema.BuchtSwitching();

                        Bucht oldBucht = Data.BuchtDB.GetBuchetByID(buchtSwitching.OldBuchtID);
                        Bucht newBucht = Data.BuchtDB.GetBuchetByID(buchtSwitching.NewBuchtID);
                        Bucht otherBucht = new Bucht();

                        //TODO:rad 13950118
                        Telephone _telephone = TelephoneDB.GetTelephoneByTelePhoneNo((long)request.TelephoneNo);
                        //end TODO

                        if (oldBucht.ConnectionID != null)
                        {
                            PostContact oldPostContact = Data.PostContactDB.GetPostContactByID((long)oldBucht.ConnectionID);
                            Post oldPost = Data.PostDB.GetPostByID(oldPostContact.PostID);
                            buchtSwitchingLog.OldPostContact = oldPostContact.ConnectionNo;
                            buchtSwitchingLog.OldPost = oldPost.Number;

                            //milad doran
                            //oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                            //oldPostContact.Detach();
                            //DB.Save(oldPostContact);

                            //TODO:rad 13950118
                            if (this.buchtSwitching.PostContactID != null) //فقط برای تعویض بوخت هایی که اتصالی پست جدید مشخص کرده اند ، بایستی اتصالی پست قدیم آزاد شود
                            {
                                oldPostContact.Status = (byte)DB.PostContactStatus.Free;
                                oldPostContact.Detach();
                                DB.Save(oldPostContact);
                            }
                            //end TODO
                        }

                        if (oldBucht.CabinetInputID != null)
                        {
                            CabinetInput oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(oldBucht.CabinetInputID ?? 0);
                            Cabinet oldcabinet = Data.CabinetDB.GetCabinetByID(oldCabinetInput.CabinetID);
                            buchtSwitchingLog.OldCabinetInput = oldCabinetInput.InputNumber;
                            buchtSwitchingLog.OldCabinet = oldcabinet.CabinetNumber;
                        }


                        buchtSwitchingLog.OldBuchtNo = BuchtDB.GetConnectionByBuchtID(oldBucht.ID);
                        if (newBucht != null)
                        {
                            buchtSwitchingLog.NewBuchtNo = BuchtDB.GetConnectionByBuchtID(newBucht.ID);
                        }

                        if (buchtSwitching.OtherBuchtID != null)
                        {
                            otherBucht = Data.BuchtDB.GetBuchetByID(buchtSwitching.OtherBuchtID);
                            buchtSwitchingLog.OtherBuchtNo = BuchtDB.GetConnectionByBuchtID(otherBucht.ID);
                        }

                        //milad doran
                        //Telephone _telephone = TelephoneDB.GetTelephoneByTelePhoneNo((long)request.TelephoneNo);

                        if (_telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire && newBucht != null)
                        {
                            SpecialWireAddress specialWireAddresses = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID(oldBucht.ID);
                            SpecialWireAddress newSpecialWireAddress = new SpecialWireAddress();
                            newSpecialWireAddress.BuchtID = newBucht.ID;
                            newSpecialWireAddress.TelephoneNo = specialWireAddresses.TelephoneNo;
                            newSpecialWireAddress.InstallAddressID = specialWireAddresses.InstallAddressID;
                            newSpecialWireAddress.CorrespondenceAddressID = specialWireAddresses.CorrespondenceAddressID;
                            newSpecialWireAddress.SecondBuchtID = specialWireAddresses.SecondBuchtID;
                            newSpecialWireAddress.SpecialTypeID = specialWireAddresses.SpecialTypeID;
                            newSpecialWireAddress.Detach();
                            DB.Save(newSpecialWireAddress, true);
                            DB.Delete<SpecialWireAddress>(specialWireAddresses.BuchtID);
                        }
                        else if (_telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire && newBucht == null && buchtSwitching.OtherBuchtID != null)
                        {
                            SpecialWireAddress specialWireAddresses = SpecialWireAddressDB.GetSpecialWireAddressByBuchtID(oldBucht.ID);
                            specialWireAddresses.SecondBuchtID = buchtSwitching.OtherBuchtID;
                            specialWireAddresses.Detach();
                            DB.Save(specialWireAddresses);

                        }
                        else if (_telephone.UsageType == (int)DB.TelephoneUsageType.PrivateWire && newBucht == null && buchtSwitching.OtherBuchtID == null)
                        {
                            throw new Exception("خطا در ثبت اطلاعات");
                        }


                        if (oldBucht.BuchtIDConnectedOtherBucht != null && buchtSwitching.OtherBuchtID == null)
                        {

                            newBucht.BuchtIDConnectedOtherBucht = oldBucht.BuchtIDConnectedOtherBucht;
                            Bucht otherOldBucht = Data.BuchtDB.GetBuchetByID(oldBucht.BuchtIDConnectedOtherBucht);
                            if (otherOldBucht.BuchtIDConnectedOtherBucht != null && oldBucht.BuchtIDConnectedOtherBucht == otherOldBucht.BuchtIDConnectedOtherBucht)
                            {
                                otherOldBucht.BuchtIDConnectedOtherBucht = newBucht.ID;
                                otherOldBucht.Detach();
                                DB.Save(otherOldBucht);
                            }

                        }
                        else if (oldBucht.BuchtIDConnectedOtherBucht != null && buchtSwitching.OtherBuchtID != null)
                        {

                            //newBucht.BuchtIDConnectedOtherBucht = oldBucht.BuchtIDConnectedOtherBucht;
                            Bucht otherOldBucht = Data.BuchtDB.GetBuchetByID(oldBucht.BuchtIDConnectedOtherBucht);
                            if (buchtSwitching.CauseBuchtSwitchingID == (int)DB.CauseBuchtSwitching.BuchtBroking)
                            {
                                otherOldBucht.Status = (byte)DB.BuchtStatus.Destroy;
                            }
                            else
                            {
                                otherOldBucht.Status = (int)DB.BuchtStatus.Free;
                            }




                            if (otherOldBucht.BuchtIDConnectedOtherBucht != null && oldBucht.BuchtIDConnectedOtherBucht == otherOldBucht.BuchtIDConnectedOtherBucht)
                            {
                                if (newBucht != null)
                                {
                                    otherOldBucht.BuchtIDConnectedOtherBucht = newBucht.ID;
                                }
                                else
                                {
                                    otherOldBucht.BuchtIDConnectedOtherBucht = oldBucht.ID;
                                }
                            }

                            otherBucht.SwitchPortID = otherOldBucht.SwitchPortID;
                            otherOldBucht.SwitchPortID = null;
                            otherOldBucht.Detach();
                            DB.Save(otherOldBucht);

                            if (newBucht != null)
                            {
                                newBucht.BuchtIDConnectedOtherBucht = otherBucht.ID;
                            }
                            else
                            {
                                oldBucht.BuchtIDConnectedOtherBucht = otherBucht.ID;
                            }


                            otherBucht.Status = (int)DB.BuchtStatus.Connection;
                            otherBucht.Detach();
                            DB.Save(otherBucht);

                        }
                        else if (oldBucht.BuchtIDConnectedOtherBucht == null && buchtSwitching.OtherBuchtID != null)
                        {
                            if (newBucht != null)
                            {
                                newBucht.BuchtIDConnectedOtherBucht = otherBucht.ID;
                            }
                            else
                            {
                                oldBucht.BuchtIDConnectedOtherBucht = otherBucht.ID;
                            }


                            otherBucht.Status = (int)DB.BuchtStatus.Connection;
                            otherBucht.Detach();
                            DB.Save(otherBucht);
                        }

                        if (newBucht != null)
                        {
                            #region Broking

                            if (buchtSwitching.CauseBuchtSwitchingID == (int)DB.CauseBuchtSwitching.BuchtBroking)
                            {
                                oldBucht.Status = (byte)DB.BuchtStatus.Destroy;
                            }
                            else if (buchtSwitching.CauseBuchtSwitchingID == (int)DB.CauseBuchtSwitching.CabinetInputBroking)
                            {
                                oldBucht.Status = (byte)DB.BuchtStatus.Free;
                                CabinetInput cabinetInput = CabinetInputDB.GetCabinetInputByID((long)oldBucht.CabinetInputID);
                                cabinetInput.Status = (int)DB.CabinetInputStatus.Malfuction;

                                cabinetInput.Detach();
                                DB.Save(cabinetInput);
                            }
                            else if (buchtSwitching.CauseBuchtSwitchingID == (int)DB.CauseBuchtSwitching.BuchtAndCabinetInputBroking)
                            {
                                oldBucht.Status = (byte)DB.BuchtStatus.Destroy;
                                CabinetInput cabinetInput = CabinetInputDB.GetCabinetInputByID((long)oldBucht.CabinetInputID);
                                cabinetInput.Status = (int)DB.CabinetInputStatus.Malfuction;

                                cabinetInput.Detach();
                                DB.Save(cabinetInput);
                            }
                            else
                            {
                                oldBucht.Status = (byte)DB.BuchtStatus.Free;
                            }

                            #endregion

                            if (buchtSwitching.PostContactID == null)
                            {
                                newBucht.ConnectionID = oldBucht.ConnectionID;
                                oldBucht.ConnectionID = null;
                            }
                            else
                            {
                                PostContact newPostContact = Data.PostContactDB.GetPostContactByID((long)buchtSwitching.PostContactID);
                                oldBucht.ConnectionID = null;
                                newBucht.ConnectionID = newPostContact.ID;

                                CabinetInput newCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(newBucht.CabinetInputID ?? 0);
                                Cabinet newCabinet = Data.CabinetDB.GetCabinetByID(newCabinetInput.CabinetID);
                                Post newPost = Data.PostDB.GetPostByID(newPostContact.PostID);

                                buchtSwitchingLog.NewPostContact = newPostContact.ConnectionNo;
                                buchtSwitchingLog.NewPost = newPost.Number;
                                buchtSwitchingLog.NewCabinetInput = newCabinetInput.InputNumber;
                                buchtSwitchingLog.NewCabinet = newCabinet.CabinetNumber;



                                newPostContact.Status = (byte)DB.PostContactStatus.CableConnection;

                                newPostContact.Detach();
                                DB.Save(newPostContact);
                            }

                            newBucht.SwitchPortID = oldBucht.SwitchPortID;

                            oldBucht.SwitchPortID = null;

                            newBucht.Status = (byte)DB.BuchtStatus.Connection;



                        }

                        oldBucht.Detach();
                        DB.Save(oldBucht);

                        if (newBucht != null)
                        {
                            newBucht.Detach();
                            DB.Save(newBucht);
                        }

                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.BuchtSwitching>(buchtSwitchingLog, true));
                        requestLog.Date = currentDataTime;
                        requestLog.Detach();
                        DB.Save(requestLog, true);


                        buchtSwitching.DateOfFinal = currentDataTime;
                        buchtSwitching.Detach();
                        DB.Save(buchtSwitching, false);

                        ts3.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ShowSuccessMessage(e.ProgressPercentage.ToString() + "%");
        }

        public override bool Print()
        {
            this.Cursor = Cursors.Wait;
            try
            {

                List<ExchangeCabinetInputInfo> Result = ReportDB.GetBuchtSwitchingNetworkInfo(requestID);
                SendToPrint(Result);

                IsPrintSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در نمایش گزارش", ex);
                IsPrintSuccess = false;
            }
            this.Cursor = Cursors.Arrow;
            return IsPrintSuccess;

        }

        private void SendToPrint(List<ExchangeCabinetInputInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.BuchtSwitchingNewtworking);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);
            stiReport.Dictionary.Variables["ReportDate"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["ReportTime"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
    }
}

