using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services.Description;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CRM.Data;
using Enterprise;
using CRM.Data.Services;
using CookComputing.XmlRpc;
using System.Collections;

namespace CRM.Application.Views
{
    public partial class TelephoneNoInputForm : Local.PopupWindow
    {
        #region Properties

        public byte RequestType { get; set; }
        public byte Mode { get; set; }
        private Telephone _Telephone { get; set; }
        private long _TelephoneNo { get; set; }
        private Service1 aDSLService { get; set; }
        private System.Data.DataTable _TelephoneInfo { get; set; }
        private string city = string.Empty;

        #endregion

        #region Costructors

        public TelephoneNoInputForm(byte requestType, byte mode = 0)
        {
            InitializeComponent();
            RequestType = requestType;
            Mode = mode;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            TelephoneNoTextBox.Focus();

            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }

        private void CheckConditions()
        {
            switch (RequestType)
            {
                case (byte)DB.RequestType.ADSL:
                    CheckADSLConditions();
                    break;

                case (byte)DB.RequestType.ADSLInstalPAPCompany:
                    CheckPAPInstalConditions();
                    break;

                case (byte)DB.RequestType.ADSLChangeService:
                    CheckADSLChangeServiceConditions();
                    break;

                case (byte)DB.RequestType.ADSLChangeIP:
                    CheckADSLChangeIPConditions();
                    break;

                case (byte)DB.RequestType.ADSLInstall:
                    CheckADSLInstallConditions();
                    break;

                case (byte)DB.RequestType.ADSLSellTraffic:
                    CheckADSLSellTrafficConditions();
                    break;

                case (byte)DB.RequestType.ADSLCutTemporary:
                    CheckADSLCutConditions();
                    break;

                case (byte)DB.RequestType.ADSLChangePlace:
                    CheckADSLChangePlaceConditions();
                    break;

                case (byte)DB.RequestType.ADSLDischarge:
                    CheckADSLDischargeADSLConditions();
                    break;

                case (byte)DB.RequestType.ADSLChangePort:
                    CheckADSLChangePortADSLConditions();
                    break;

                case (byte)DB.RequestType.CutAndEstablish:
                    CheckCutAndEstablishConditions();
                    break;

                case (byte)DB.RequestType.Connect:
                    CheckConnectConditions();
                    break;

                case (byte)DB.RequestType.ChangeNo:
                    CheckChangeNoConditions();
                    break;

                case (byte)DB.RequestType.ChangeLocationCenterInside:
                    CheckChangeLocationCenterInsideConditions();
                    break;

                case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                    CheckChangeLocationCenterToCenterConditions();
                    break;

                case (byte)DB.RequestType.RefundDeposit:
                    CheckRefundableDepositOfFixedPhoneConditions();
                    break;

                case (byte)DB.RequestType.ChangeAddress:
                    CheckChangeAddressPhoneConditions();
                    break;

                case (byte)DB.RequestType.Dischargin:
                    CheckDischarginPhoneConditions();
                    break;

                case (byte)DB.RequestType.TitleIn118:
                    CheckTitleIn118Conditions();
                    break;

                case (byte)DB.RequestType.RemoveTitleIn118:
                    CheckRemoveTitleIn118Conditions();
                    break;

                case (byte)DB.RequestType.ChangeTitleIn118:
                    CheckChangeTitleIn118Conditions();
                    break;

                case (byte)DB.RequestType.Failure117:
                    CheckFailue117Conditions();
                    break;

                case (byte)DB.RequestType.ChangeName:
                    CheckNameConditions();
                    break;

                case (byte)DB.RequestType.SpecialService:
                    SpecialServiceConditions();
                    break;

                case (byte)DB.RequestType.PBX:
                    PBXCheckCondition();
                    break;

                case (byte)DB.RequestType.E1Link:
                    E1linkCheckCondition();
                    break;

                case (byte)DB.RequestType.VacateE1:
                    VacateE1CheckCondition();
                    break;
                case (byte)DB.RequestType.OpenAndCloseZero:
                    OpenAndCloseZeroCheckCondition();
                    break;
                case (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:
                    ChcekConditionADSLChangeCustomerOwnerCharacteristics();
                    break;

                default:
                    break;
            }
        }

        private void OpenAndCloseZeroCheckCondition()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }


                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
        }

        private void CheckChangeTitleIn118Conditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            TitleIn118 title118 = Data.TitleIn118DB.GetLastTitlein118ByTelephone(_Telephone.TelephoneNo);
            if (title118 == null)
            {
                throw new Exception("برای این شماره در سامانه 118 عنوانی ثبت نشده است.");
            }
            else if (title118.Status == (int)DB.RequestType.RemoveTitleIn118)
            {
                throw new Exception("عنوان این شماره در سامانه 118 پیش از این حذف شده است.");
            }

        }

        private void CheckRemoveTitleIn118Conditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            TitleIn118 title118 = Data.TitleIn118DB.GetLastTitlein118ByTelephone(_Telephone.TelephoneNo);
            if (title118 == null)
            {
                throw new Exception("برای این شماره در سامانه 118 عنوانی ثبت نشده است.");
            }
            else
            {
                if (title118.Status == (int)DB.RequestType.RemoveTitleIn118)
                    throw new Exception("عنوان این شماره در سامانه 118 پیش از این حذف شده است.");
            }
        }

        private void VacateE1CheckCondition()
        {

            if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
            {
                throw new Exception("تلفن قطع می باشد.");
            }

            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {
                throw new Exception("این تلفن دایر نیست");
            }
        }

        private void E1linkCheckCondition()
        {
            using (MainDataContext context = new MainDataContext())
            {
                if (!Data.E1DB.CheckTelephoneBeE1(_Telephone.TelephoneNo))
                {
                    throw new Exception("تلفن وارد شده E1 نمی باشد.");
                }

                if (!Data.E1DB.CheckTelephoneBeConnectedE1(_Telephone.TelephoneNo))
                {
                    throw new Exception("تلفن وارده دایر نمی باشد.");
                }
            }
        }

        private void PBXCheckCondition()
        {
            if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
            {
                string causeOfCutName;
                bool RequestAbility = false;
                int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                if (causeOfCut == -2)
                {
                    MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                    switch (dialogResult)
                    {
                        case MessageBoxResult.No:
                            throw new Exception("در خواست لغو گردید");
                    }
                }
                else if (causeOfCut != -1 && RequestAbility == false)
                {
                    throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                }
                else if (causeOfCut != -1 && RequestAbility == true)
                {
                    Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                }

            }

            if (Data.TelephonePBXDB.CheckTelephoneBePBX(_Telephone.TelephoneNo))
            {
                throw new Exception("تلفن پی بی ایکس می باشد");
            }
        }

        private void SpecialServiceConditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
        }

        private void CheckDischarginPhoneConditions()
        {

            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            if (_Telephone.CustomerID == null)
            {
                throw new Exception("اطلاعات مشترک یافت نشد");
            }
        }

        private void CheckNameConditions()
        {

            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);
                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            if (_Telephone.CustomerID == null)
            {
                throw new Exception("اطلاعات مشترک یافت نشد");
            }
        }

        private void CheckChangeLocationCenterToCenterConditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {
                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }
            }
            if (_Telephone.CustomerID == null)
            {
                throw new Exception("اطلاعات مشترک یافت نشد");
            }
        }

        private void CheckChangeLocationCenterInsideConditions()
        {

            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {
                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }
            }

            if (_Telephone.CustomerID == null)
            {
                throw new Exception("اطلاعات مشترک یافت نشد");
            }
        }

        private void CheckCutAndEstablishConditions()
        {
            switch (_Telephone.Status)
            {
                case (byte)DB.TelephoneStatus.Free:
                    throw new Exception("این شماره آزاد می باشد!");

                case (byte)DB.TelephoneStatus.ChangingLocation:
                    throw new Exception("این شماره هم اکنون در حال تغییر مکان می باشد!");

                //case (byte)DB.TelephoneStatus.Cut:
                //    throw new Exception("این شماره هم اکنون قطع می باشد!");

                case (byte)DB.TelephoneStatus.Discharge:
                    throw new Exception("این شماره تخلیه شده است!");

                case (byte)DB.TelephoneStatus.Reserv:
                    throw new Exception("این شماره هم اکنون رزرو می باشد !");

                default:
                    break;
            }

        }

        private void CheckConnectConditions()
        {
            if (_Telephone.Status == (byte)DB.TelephoneStatus.Connecting)
                throw new Exception("این شماره هم اکنون وصل می باشد!");
        }

        private void CheckADSLConditions()
        {
            #region ADSL Without Web Service

            //switch (_Telephone.Status)
            //{
            //    case (byte)DB.TelephoneStatus.Free:
            //        throw new Exception("شماره تلفن مورد نظر آزاد است و دایر نشده است !");

            //    case (byte)DB.TelephoneStatus.Reserv:
            //        throw new Exception("شماره تلفن مورد نظر رزرو است و دایر نشده است !");

            //    case (byte)DB.TelephoneStatus.Cut:
            //        throw new Exception("شماره تلفن مورد نظر قطع شده است !");

            //    case (byte)DB.TelephoneStatus.ChangingLocation:
            //        throw new Exception("شماره تلفن مورد نظر در حال تغییر مکان می باشد !");

            //    case (byte)DB.TelephoneStatus.Discharge:
            //        throw new Exception("شماره تلفن مورد نظر تخلیه شده است و دایر نمی باشد !");

            //    default:
            //        break;
            //}

            //ADSL ADSL = DB.SearchByPropertyName<ADSL>("TelephoneNo", _Telephone.TelephoneNo).SingleOrDefault();

            //if (ADSL != null)
            //{
            //    switch (ADSL.Status)
            //    {
            //        case (byte)DB.ADSLStatus.Connect:
            //            throw new Exception("این شماره دارای سرویس ADSL می باشد ! ");

            //        case (byte)DB.ADSLStatus.Cut:
            //            throw new Exception("سرویس ADSL برای این شماره هم اکنون قطع موقت می باشد ! ");

            //        case (byte)DB.ADSLStatus.Discharge:
            //            break;

            //        default:
            //            break;
            //    }
            //}

            //List<Request> aDSLRequests = RequestDB.GetRequestbytelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSL);
            //if (aDSLRequests.Count != 0)
            //    throw new Exception("برای این شماره در حال حاضر درخواست ADSL موجود می باشد !");

            #endregion

            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSL);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (ADSLDB.GetActiveADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL می باشد !");

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            //if (aDSLService.Phone_Is_PCM(TelephoneNoTextBox.Text))
            //    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");

            //if (aDSLService.Phone_Exist_In_Post_PCM(TelephoneNoTextBox.Text))
            //    throw new Exception("* امکان تخصیص ADSL به این شماره وجود ندارد !");


            //System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

            //string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
            //int centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(centerCode));
            //int cabinetNo = Convert.ToInt32(telephoneInfo.Rows[0]["KAFU_NUM"].ToString());

            //if (ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
            //    throw new Exception("این شماره در کافویی است که امکان تخصیص ADSL ندارد !");
        }

        private void CheckPAPInstalConditions()
        {
            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(TelephoneNoTextBox.Text))
                throw new Exception("شماره وارد شده موجود نمی باشد !");
            else
            {
                System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text);

                string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
                int centerID = DB.SearchByPropertyName<Center>("CenterCode", centerCode).SingleOrDefault().ID;
                int cabinetNo = Convert.ToInt32(telephoneInfo.Rows[0]["KAFU_NUM"].ToString());

                if (ADSLPAPCabinetAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                    throw new Exception("این شماره در کافویی است که امکان تخصیص ADSL ندارد !");

                if (aDSLService.Tel_Have_ADSl_Port(TelephoneNoTextBox.Text))
                    throw new Exception("شماره وارد شده دارای ADSL می باشد !");
                else
                {
                    if (aDSLService.TelDissectionStatus(TelephoneNoTextBox.Text))
                        throw new Exception("شماره وارد شده قطع می باشد !");
                    else
                    {
                        WebReference.PhoneStatusService deptorService = new WebReference.PhoneStatusService();

                        bool result1 = true;
                        long debtAmount = 0;

                        deptorService.GetDebtStatus(TelephoneNoTextBox.Text, out debtAmount, out result1);
                        if (debtAmount > 50000)
                            throw new Exception("بدهی مالی دارد !");
                    }
                }
            }
        }

        private void CheckADSLChangeServiceConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLChangeService);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست شارژ مجدد ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                if (ADSL.TariffID == null)
                    throw new Exception("برای این شماره سرویسی تعریف نشده است");
                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }

            CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
            CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
            CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();

            ibsngInputInfo.NormalUsername = _TelephoneNo.ToString();
            ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);

            if (ibsngUserInfo == null)
            {
                ibsngInputInfo.NormalUsername = ADSL.UserName;
                ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);
            }

            if (!string.IsNullOrEmpty(ibsngUserInfo.RenewNextGroup))
                throw new Exception("شماره تلفن دارای سرویس ذخیره شده می باشد !");
        }

        private void ChcekConditionADSLChangeCustomerOwnerCharacteristics()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست تغییر مشخصات مالک ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {


                if (ADSL.TariffID == null)
                    throw new Exception("برای این شماره سرویسی تعریف نشده است");
                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }

        }

        private void CheckADSLChangeIPConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLChangeIP);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست شارژ مجدد ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                if (ADSL.TariffID == null)
                    throw new Exception("برای این شماره سرویسی تعریف نشده است");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckADSLInstallConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLInstall);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست نصب ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckADSLCutConditions()
        {
            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckADSLChangePortADSLConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLChangePort);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست تعویض پورت ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckADSLDischargeADSLConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLDischarge);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست تخلیه ADSL موجود می باشد !");

            List<Request> aDSLRequestsDayeri = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSL);
            if (aDSLRequestsDayeri.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست دایری نا تمام ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();

            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                if (ADSL.TariffID == null)
                    throw new Exception("برای این شماره سرویسی تعریف نشده است");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckADSLSellTrafficConditions()
        {
            Service1 aDSLService = new Service1();

            if (_TelephoneNo != 0)
            {
                List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLSellTraffic);
                if (aDSLRequests.Count != 0)
                    throw new Exception("برای این شماره در حال حاضر درخواست خرید ترافیک ADSL موجود می باشد !");

                if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                    if (telephone == null)
                        throw new Exception("* شماره وارد شده موجود نمی باشد !");
                }

                string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
                if (!string.IsNullOrWhiteSpace(papName))
                    throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

                if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                    throw new Exception("* شماره وارد شده قطع می باشد !");

                if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                    throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

                ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

                if (ADSL == null)
                {
                    throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
                }
                else
                {
                    if (ADSL.PAPInfoID != null)
                        throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                    if (ADSL.TariffID == null)
                        throw new Exception("برای این شماره سرویسی تعریف نشده است");

                    switch (ADSL.Status)
                    {
                        case (byte)DB.ADSLStatus.Cut:
                            throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                        case (byte)DB.ADSLStatus.Discharge:
                            throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                        default:
                            break;
                    }
                }

                CRMWebService.IbsngInputInfo ibsngInputInfo = new CRMWebService.IbsngInputInfo();
                CRMWebService.IBSngUserInfo ibsngUserInfo = new CRMWebService.IBSngUserInfo();
                CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();

                ibsngInputInfo.NormalUsername = _TelephoneNo.ToString();
                ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);

                if (ibsngUserInfo == null)
                {
                    ibsngInputInfo.NormalUsername = ADSL.UserName;
                    ibsngUserInfo = service.GetUserInfo(ibsngInputInfo);
                }

                if (!string.IsNullOrEmpty(ibsngUserInfo.NearestExpDate))
                {
                    DateTime nearestExpDate = Convert.ToDateTime(ibsngUserInfo.NearestExpDate);

                    if ((Convert.ToInt32((DB.GetServerDate() - nearestExpDate).TotalDays)) >= 0)
                        throw new Exception("سرویس ADSL شماره مورد نظر پایان یافته است !");
                }
                else
                    throw new Exception("مشترک مورد نظر اتصالی برقرار نکرده است !");
            }
        }

        private void CheckADSLChangePlaceConditions()
        {
            List<Request> aDSLRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSLChangePlace);
            if (aDSLRequests.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست تغییر مکان ADSL موجود می باشد !");

            List<Request> aDSLRequestsDayeri = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.ADSL);
            if (aDSLRequestsDayeri.Count != 0)
                throw new Exception("برای این شماره در حال حاضر درخواست دایری نا تمام ADSL موجود می باشد !");

            ADSLChangePlace adslChangePlace = ADSLChangePlaceDB.GetADSLChangePlaceByOldTelephoneNo(_TelephoneNo);
            if (adslChangePlace != null)
                throw new Exception("برای این شماره در حال حاضر درخواست تغییر مکان ADSL موجود می باشد !");

            Service1 aDSLService = new Service1();
            if (!aDSLService.Is_Phone_Exist(_TelephoneNo.ToString()))
            {
                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                if (telephone == null)
                    throw new Exception("* شماره وارد شده موجود نمی باشد !");
            }

            string papName = ADSLPAPPortDB.GetActiveADSLPAPbyTelephoneNo(_TelephoneNo);
            if (!string.IsNullOrWhiteSpace(papName))
                throw new Exception("* شماره وارد شده دارای ADSL از شرکت " + papName + " می باشد !");

            if (aDSLService.TelDissectionStatus(_TelephoneNo.ToString()))
                throw new Exception("* شماره وارد شده قطع می باشد !");

            if (!ADSLDB.HasADSLbyTelephoneNo(_TelephoneNo))
                throw new Exception("* شماره وارد شده دارای ADSL نمی باشد !");

            ADSL ADSL = ADSLDB.GetADSLByTelephoneNo(_TelephoneNo);

            if (ADSL == null)
            {
                throw new Exception("این شماره دارای سرویس ADSL نمی باشد !");
            }
            else
            {
                if (ADSL.PAPInfoID != null)
                    throw new Exception("سرویس ADSL این شماره از شرکت PAP گرفته شده است !");

                if (ADSL.TariffID == null)
                    throw new Exception("برای این شماره سرویسی تعریف نشده است");

                switch (ADSL.Status)
                {
                    case (byte)DB.ADSLStatus.Cut:
                        throw new Exception("سرویس ADSL برای این شماره قطع موقت می باشد !");

                    case (byte)DB.ADSLStatus.Discharge:
                        throw new Exception("سرویس ADSL برای این شماره تخلیه شده است !");

                    default:
                        break;
                }
            }
        }

        private void CheckRefundableDepositOfFixedPhoneConditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }

            if (DB.GetBuchtIDByTelephonNo(_Telephone.TelephoneNo).ADSLStatus == true)
            {
                throw new Exception("این تلفن دارای ADSL میباشد لطفا ابتداد روال تخلیه ADSL را اجرا کنید");
            }

        }

        private void CheckChangeNoConditions()
        {

            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            if (_Telephone.CustomerID == null)
            {
                throw new Exception("اطلاعات مشترک یافت نشد");
            }

        }

        private bool CheckCutTelephone(Telephone _Telephone)
        {
            throw new NotImplementedException();
        }

        private void CheckChangeAddressPhoneConditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }
            if (_Telephone.InstallAddressID == null)
            {
                throw new Exception("اطلاعات آدرس مشترک یافت نشد");
            }
        }

        private void CheckTitleIn118Conditions()
        {
            if (_Telephone.Status != (byte)DB.TelephoneStatus.Connecting)
            {

                if (_Telephone.Status == (byte)DB.TelephoneStatus.Cut)
                {
                    string causeOfCutName;
                    bool RequestAbility = false;
                    int causeOfCut = Data.TelephoneDB.GetLastCauseOfCut(_Telephone.TelephoneNo, out causeOfCutName, out RequestAbility);

                    if (causeOfCut == -2)
                    {
                        MessageBoxResult dialogResult = Folder.MessageBox.ShowQuestion("تلفن قطع می باشد واطلاعات علت قطع یافت نشد آیا مایل با ثبت درخواست می باشید");

                        switch (dialogResult)
                        {
                            case MessageBoxResult.No:
                                throw new Exception("در خواست لغو گردید");
                        }
                    }
                    else if (causeOfCut != -1 && RequestAbility == false)
                    {
                        throw new Exception("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }
                    else if (causeOfCut != -1 && RequestAbility == true)
                    {
                        Folder.MessageBox.ShowInfo("این تلفن به علت " + causeOfCutName + "قطع می باشد ");
                    }

                }
                else
                {
                    throw new Exception("این تلفن دایر نیست");
                }


            }

            TitleIn118 title118 = Data.TitleIn118DB.GetLastTitlein118ByTelephone(_Telephone.TelephoneNo);
            if (title118 != null && (title118.Status == (int)DB.RequestType.TitleIn118 || title118.Status == (int)DB.RequestType.ChangeTitleIn118))
                throw new Exception("این شماره در سامانه 118 دارای عنوان می باشد، در صورت نیاز به تغییر عنوان به روال تغییر عنوان در 118 رجوع نمایید.");
        }

        private void CheckFailue117Conditions()
        {
            if (city == "semnan")
            {
                Service1 service = new Service1();
                bool isPhoneExist = service.Is_Phone_Exist(TelephoneNoTextBox.Text.Replace(" ", ""));
                if (!isPhoneExist)
                    throw new Exception("شماره وارد شده موجود نمی باشد !");
                else
                {
                    List<Request> failureRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.Failure117);
                    if (failureRequests.Count != 0)
                        throw new Exception("برای این شماره تلفن هم اکنون درخواستی در حال بررسی موجود می باشد");

                    System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", TelephoneNoTextBox.Text);

                    if (telephoneInfo.Rows.Count != 0)
                    {
                        string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
                        int centerID = CenterDB.GetCenterIDByCenterCode(Convert.ToInt32(centerCode));
                        int cabinetNo = 0;
                        try
                        {
                            cabinetNo = Convert.ToInt32(telephoneInfo.Rows[0]["KAFU_NUM"].ToString());
                        }
                        catch (Exception ex)
                        {
                            cabinetNo = 0;
                        }

                        int postNo = 0;
                        try
                        {
                            postNo = Convert.ToInt32(telephoneInfo.Rows[0]["POST_NUM"].ToString());
                        }
                        catch (Exception ex)
                        {
                            postNo = 0;
                        }

                        if (!DB.CurrentUser.CenterIDs.Contains(centerID))
                            throw new Exception("این تلفن در مراکز دسترسی شما وجود ندارد !");

                        if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                            throw new Exception("کافو مربوط به این تلفن در لیست کافو های خراب می باشد !");

                        if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                            throw new Exception("پست مربوط به این تلفن در لیست پست های خراب می باشد !");

                        if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(_TelephoneNo, centerID))
                            throw new Exception("این شماره تلفن در لیست تلفن های خراب می باشد !");
                    }
                }
            }

            if (city == "kermanshah")
            {
                int centerID = 0;

                if (TelephoneDB.HasTelephoneNo(_TelephoneNo) == false)
                {
                    if (TelephoneDB.HasTelephoneTemp(_TelephoneNo) == false)
                        throw new Exception("شماره وارد شده موجود نمی باشد !");
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(_TelephoneNo);
                }
                else
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                    if (telephone.Status == (byte)DB.TelephoneStatus.Cut)
                    {
                        string cutReason = CutAndEstablishDB.GetCutReason(_TelephoneNo);
                        if (!string.IsNullOrWhiteSpace(cutReason))
                            throw new Exception("تلفن مورد نظر قطع می باشد، دلیل قطع : " + cutReason);
                        else
                            throw new Exception("تلفن مورد نظر قطع می باشد، دلیل قطع در سیستم موجود نمی باشد.");
                    }

                    if (telephone.Status == (byte)DB.TelephoneStatus.Discharge)
                        throw new Exception("تلفن مورد نظر تخلیه می باشد.");

                    centerID = telephone.CenterID;
                }

                List<Request> failureRequests = RequestDB.GetRequestbyTelephoneNoandRequestTypeID(_TelephoneNo, (int)DB.RequestType.Failure117);
                if (failureRequests.Count != 0)
                {
                    FailureForm form = Failure117DB.GetFailureForm(failureRequests[0].ID);
                    if (form == null)
                        throw new Exception("برای این شماره تلفن هم اکنون درخواستی در حال بررسی موجود می باشد");
                    else
                        throw new Exception("برای این شماره تلفن هم اکنون درخواستی در حال بررسی موجود می باشد، شماره ردیف خرابی : " + form.RowNo.ToString());
                }
                if (!DB.CurrentUser.CenterIDs.Contains(centerID))
                    throw new Exception("این تلفن در مراکز دسترسی شما وجود ندارد !");

                int cabinetNo = Failure117DB.GetCabinetNobyTelephoneNo(_TelephoneNo);
                {
                    if (cabinetNo != 0)
                        if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                            throw new Exception("کافو مربوط به این تلفن در لیست کافو های خراب می باشد !");

                    int postNo = Failure117DB.GetPostNobyTelephoneNo(_TelephoneNo);
                    if (postNo != 0)
                        if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                            throw new Exception("پست مربوط به این تلفن در لیست پست های خراب می باشد !");
                }

                if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(_TelephoneNo, centerID))
                    throw new Exception("این شماره تلفن در لیست تلفن های خراب می باشد !");
            }
        }

        private string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        #endregion

        #region Event Handlers

        private void ShowRequestFormButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!TelephoneNoTextBox.Text.Contains("WL") && !TelephoneNoTextBox.Text.Contains("wl"))
                    _TelephoneNo = Convert.ToInt64(TelephoneNoTextBox.Text.Trim().Replace(" ", "").TrimStart('0'));
                else
                    _TelephoneNo = 0;

                if (RequestType != (byte)DB.RequestType.ADSLChangeService
                    && RequestType != (byte)DB.RequestType.ADSLSellTraffic
                    && RequestType != (byte)DB.RequestType.ADSL
                    && RequestType != (byte)DB.RequestType.ADSLChangePort
                    && RequestType != (byte)DB.RequestType.Failure117
                    && RequestType != (byte)DB.RequestType.ADSLChangeIP
                    && RequestType != (byte)DB.RequestType.ADSLInstall
                    && RequestType != (byte)DB.RequestType.ADSLDischarge
                    && RequestType != (byte)DB.RequestType.ADSLCutTemporary
                    && RequestType != (byte)DB.RequestType.ADSLChangePlace
                    && RequestType != (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics
                    && RequestType != (byte)DB.RequestType.WirelessSellTraffic
                    && RequestType != (byte)DB.RequestType.WirelessChangeService)
                {
                    string message = string.Empty;
                    bool inWaitingList = false;
                    
                    this._Telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                    if (this._Telephone != null)
                    {
                        if (this._Telephone.UsageType == (byte)DB.TelephoneUsageType.GSM)
                        {
                            if (DB.HasRestrictionsTelphone(_TelephoneNo, out message, out inWaitingList))
                            {
                                if ((RequestType == (byte)DB.RequestType.CutAndEstablish || RequestType == (byte)DB.RequestType.Connect) && inWaitingList)
                                {
                                    MessageBoxResult result = Folder.MessageBox.Show("این تلفن در لیست عدم امکانات می باشد. آیا مطمئن هستید درخواست ثبت شود؟", "پرسش", MessageBoxImage.Question, MessageBoxButton.YesNo);

                                    if (result != MessageBoxResult.Yes)
                                        throw new Exception(message);
                                }
                                else
                                    throw new Exception(message);
                            }
                        }
                        else
                        {
                            AssignmentInfo technicalInformation = DB.GetGeneralInformationByTelephoneNo(this._Telephone.TelephoneNo);
                            if (technicalInformation != null) //نداشتن امکانات فنی را به صورت کلی بررسی میکند
                            {
                                if (_Telephone.UsageType == (byte)CRM.Data.DB.TelephoneUsageType.Usuall) //اگر نوع استفاده تلفن وارد شده معمولی باشد میتواند وارد بلاک زیر شود
                                {
                                    if (technicalInformation.BuchtID.HasValue)//متصل بودن به بوخت را بررسی میکند
                                    {
                                        if (technicalInformation.CabinetInputID.HasValue)//متصل بودن ورودی کافو را بررسی میکند
                                        {
                                            if (technicalInformation.PostContactID.HasValue)//متصل بودن به اتصالی پست را بررسی میکند
                                            {
                                                if (DB.HasRestrictionsTelphone(_TelephoneNo, out message, out inWaitingList))
                                                {
                                                    if ((RequestType == (byte)DB.RequestType.CutAndEstablish || RequestType == (byte)DB.RequestType.Connect) && inWaitingList)
                                                    {
                                                        MessageBoxResult result = Folder.MessageBox.Show("این تلفن در لیست عدم امکانات می باشد. آیا مطمئن هستید درخواست ثبت شود؟", "پرسش", MessageBoxImage.Question, MessageBoxButton.YesNo);

                                                        if (result != MessageBoxResult.Yes)
                                                            throw new Exception(message);
                                                    }
                                                    else
                                                        throw new Exception(message);
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception("تلفن وارد شده به اتصالی پست متصل نمیباشد!");
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception("تلفن وارد شده به ورودی کافو متصل نمیباشد!");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("تلفن وارد شده به بوخت متصل نمیباشد!");
                                    }
                                }
                                else if (_Telephone.UsageType == (byte)DB.TelephoneUsageType.E1 || _Telephone.UsageType == (byte)DB.TelephoneUsageType.PrivateWire)  //اگر نوع استفاده ار تلفن سیم خصوصی و یا ایوان باشد
                                {
                                    if (technicalInformation.BuchtID.HasValue)
                                    {
                                        if (DB.HasRestrictionsTelphone(_TelephoneNo, out message, out inWaitingList))
                                        {
                                            if ((RequestType == (byte)DB.RequestType.CutAndEstablish || RequestType == (byte)DB.RequestType.Connect) && inWaitingList)
                                            {
                                                MessageBoxResult result = Folder.MessageBox.Show("این تلفن در لیست عدم امکانات می باشد. آیا مطمئن هستید درخواست ثبت شود؟", "پرسش", MessageBoxImage.Question, MessageBoxButton.YesNo);

                                                if (result != MessageBoxResult.Yes)
                                                    throw new Exception(message);
                                            }
                                            else
                                                throw new Exception(message);
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("تلفن وارد شده به بوخت متصل نمیباشد!");
                                    }
                                    //TODO : rad - General - بررسی عدم امکانات فنی ناقص ماند
                                    //if (technicalInformation.BuchtType == (int)DB.BuchtType.CustomerSide) //نوع بوخت را در صورت طرف مشترک بودن بررسی میکند
                                    //{
                                    //    if (technicalInformation.CabinetInputID.HasValue)
                                    //    {
                                    //        if (technicalInformation.PostContactID.HasValue)
                                    //        {

                                    //        }
                                    //        else
                                    //        {
                                    //            throw new Exception("تلفن وارد شده به اتصالی پست متصل نمیباشد!");
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        throw new Exception("تلفن وارد شده به ورودی کافو متصل نمیباشد!");
                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                throw new Exception("تلفن وارد شده دارای امکانات فنی نمیباشد!");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("شماره تلفن وارد شده در سیستم موجود نمیباشد!");
                    }
                }
                else
                {
                    aDSLService = new Service1();
                    Center center = null;

                    if (city == "semnan")
                    {
                        if (!TelephoneNoTextBox.Text.Contains("WL") && !TelephoneNoTextBox.Text.Contains("wl"))
                        {
                            _TelephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                            if (_TelephoneInfo.Rows.Count == 0)
                            {
                                Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                                if (telephone == null)
                                    throw new Exception("شماره تلفن وارد شده صحیح نمی باشد !");
                                else
                                    center = CenterDB.GetCenterByCenterID(telephone.CenterID);
                            }
                            else
                                center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(_TelephoneInfo.Rows[0]["CENTERCODE"].ToString()));
                        }
                    }

                    if (city == "kermanshah")
                    {
                        Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                        if (telephone == null)
                        {
                            int centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(_TelephoneNo);
                            center = CenterDB.GetCenterByCenterID(centerID);
                        }
                        else
                            center = CenterDB.GetCenterByCenterID(telephone.CenterID);
                    }

                    //if (!DB.CurrentUser.CenterIDs.Contains(center.ID))
                    //    throw new Exception("این تلفن در مراکز دسترسی شما وجود ندارد !");
                }

                Views.RequestForm requestForm;

                switch (RequestType)
                {
                    case (byte)DB.RequestType.ADSL:
                    case (byte)DB.RequestType.ADSLChangeService:
                    case (byte)DB.RequestType.ADSLChangeIP:
                    case (byte)DB.RequestType.ADSLInstall:
                    case (byte)DB.RequestType.ADSLDischarge:
                    case (byte)DB.RequestType.ADSLChangePort:
                    case (byte)DB.RequestType.ADSLSellTraffic:
                    case (byte)DB.RequestType.ADSLChangePlace:
                    case (byte)DB.RequestType.ADSLCutTemporary:
                    case (byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics:

                        CheckConditions();
                        requestForm = new RequestForm(RequestType, _TelephoneNo, Mode);
                        requestForm.ShowDialog();
                        this.Close();
                        break;

                    case (byte)DB.RequestType.WirelessSellTraffic:
                    case (byte)DB.RequestType.WirelessChangeService:

                        CheckConditions();
                        requestForm = new RequestForm(RequestType, TelephoneNoTextBox.Text.Trim(), Mode);
                        requestForm.ShowDialog();
                        this.Close();
                        break;

                    case (byte)DB.RequestType.Failure117:

                        CheckConditions();

                        requestForm = new RequestForm(RequestType, _TelephoneNo, Mode);
                        requestForm.ShowDialog();
                        this.Close();
                        break;

                    case (byte)DB.RequestType.ADSLInstalPAPCompany:

                        CheckConditions();

                        Views.ADSLPAPRequest papInstalRequest = new ADSLPAPRequest(_TelephoneNo);

                        this.Close();
                        papInstalRequest.ShowDialog();
                        break;

                    default:
                        _Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);

                        if (_Telephone == null)
                            throw new Exception("شماره وارد شده موجود نمی باشد !");
                        CheckConditions();
                        requestForm = new RequestForm(RequestType, _Telephone.TelephoneNo, Mode);
                        this.Close();
                        requestForm.ShowDialog();
                        requestForm.Focus();
                        break;
                }
            }

            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c) || e.Text == "w" || e.Text == "W" || e.Text == "l" || e.Text == "L")
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
