using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using CRM.Data;
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class TelephoneForm : Local.PopupWindow
    {
        private long _TelephoneNo = 0;

        private byte passStatus
        {
            get;
            set;
        }

        private bool? passIsRound
        {
            get;
            set;
        }

        private byte? passRoundType
        {
            get;
            set;
        }

        public TelephoneForm()
        {
            InitializeComponent();
            Initialize();
        }

        public TelephoneForm(long telephoneNo)
            : this()
        {
            _TelephoneNo = telephoneNo;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            RoundTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RoundType));
            SwitchPrecodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckable();

            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TelephoneStatus));

        }

        private void LoadData()
        {
            Telephone item = new Telephone();

            if (_TelephoneNo == 0)
            {
                SwitchPortComboBox.ItemsSource = Data.SwitchPortDB.GetSwitchPortCheckable();
                SaveButton.Content = "ذخیره";
            }
            else
            {

                item = Data.TelephoneDB.GetTelephoneByTelephoneNo(_TelephoneNo);
                passStatus = item.Status;
                passIsRound = item.IsRound;
                passRoundType = item.RoundType;
                SwitchPortComboBox.ItemsSource = Data.SwitchPortDB.GetSwitchPortCheckable(item.CenterID);
                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    Telephone item = this.DataContext as Telephone;
                    if ((item.IsRound == false) && (passIsRound == true))//وقتی تلفن رند بوده است و کاربر در حال تغییر وضیعت آن به عدم رند میباشد
                    {
                        CRM.Data.Schema.ChangeTelephoneRoundLog changeTelephoneRoundLog = new Data.Schema.ChangeTelephoneRoundLog();
                        //برای ذخیره کردن لاگ تغییر رند بودن تلفن باید نوع رند قبلی را نگهداریم 
                        item.RoundType = null;
                        changeTelephoneRoundLog.PreviousRoundType = passRoundType;
                        changeTelephoneRoundLog.CurrentRoundType = item.RoundType;
                        changeTelephoneRoundLog.IsRound = item.IsRound.Value;
                        changeTelephoneRoundLog.Description = "تغییر وضعیت به غیر رند";
                        RequestLog requestLog = new RequestLog();
                        requestLog.RequestTypeID = (int)DB.RequestType.ChangeTelephoneRound;
                        requestLog.TelephoneNo = _TelephoneNo;
                        requestLog.UserID = DB.CurrentUser.ID;
                        requestLog.Date = DB.GetServerDate();
                        requestLog.Description = System.Xml.Linq.XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeTelephoneRoundLog>(changeTelephoneRoundLog, true));
                        requestLog.Detach();
                        DB.Save(requestLog);
                    }
                    else if ((item.IsRound == true) && (passIsRound == false))//وقتی تلفن رند نبوده است امّا کاربر درحال رند کردن آن میباشد
                    {
                        CRM.Data.Schema.ChangeTelephoneRoundLog changeTelephoneRoundLog = new Data.Schema.ChangeTelephoneRoundLog();
                        changeTelephoneRoundLog.PreviousRoundType = passRoundType;
                        changeTelephoneRoundLog.CurrentRoundType = item.RoundType;
                        changeTelephoneRoundLog.IsRound = item.IsRound.Value;
                        changeTelephoneRoundLog.Description = "تغییر وضعیت به رند";
                        RequestLog requestLog = new RequestLog();
                        requestLog.RequestTypeID = (int)DB.RequestType.ChangeTelephoneRound;
                        requestLog.TelephoneNo = _TelephoneNo;
                        requestLog.UserID = DB.CurrentUser.ID;
                        requestLog.Date = DB.GetServerDate();
                        requestLog.Description = System.Xml.Linq.XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeTelephoneRoundLog>(changeTelephoneRoundLog, true));
                        requestLog.Detach();
                        DB.Save(requestLog);
                    }

                    if (passStatus == (byte)DB.TelephoneStatus.Connecting)
                    {
                        if (item.Status == (byte)DB.TelephoneStatus.Discharge)
                        {
                            Bucht bucht = Data.BuchtDB.GetBuchtBySwitchPortID((int)item.SwitchPortID);
                            if (bucht != null)
                                throw new Exception("تلفن دارای اطلاعات فنی می باشد لطفا برای تخلیه از روال تخلیه استفاده کنید");

                            if (item.InstallAddressID != null || item.CorrespondenceAddressID != null || item.CustomerID != null)
                            {
                                MessageBoxResult result = Folder.MessageBox.Show("تلفن به مشترک یا آدرس متصل می باشد.آیا آدرس و مشترک پاک شود؟", "پرسش", MessageBoxImage.Question, MessageBoxButton.YesNo);
                                switch (result)
                                {
                                    case MessageBoxResult.No:
                                        throw new Exception("ذخیره انجام نشد");
                                        break;
                                    case MessageBoxResult.Yes:
                                        item.CustomerID = null;
                                        item.InstallAddressID = null;
                                        item.CorrespondenceAddressID = null;
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }
                        else if (item.Status == (byte)DB.TelephoneStatus.Cut)
                        {

                        }
                        else if (item.Status == (byte)DB.TelephoneStatus.Destruction)
                        {

                        }
                        else
                        {
                            throw new Exception("امکان تغییر وضعیت نمی باشد");
                        }
                    }
                    else if (passStatus == (byte)DB.TelephoneStatus.Cut)
                    {
                        if (item.Status == (byte)DB.TelephoneStatus.Connecting)
                        {

                        }
                        else
                        {
                            throw new Exception("امکان تغییر وضعیت نمی باشد");
                        }
                    }
                    else if (passStatus == (byte)DB.TelephoneStatus.Free)
                    {
                        if (item.IsRound.Value)
                            if (item.Status == (byte)DB.TelephoneStatus.Discharge || item.Status == (byte)DB.TelephoneStatus.Reserv || item.Status == (byte)DB.TelephoneStatus.Discharge)
                            {

                            }
                            else
                            {
                                throw new Exception("امکان تغییر وضعیت نمی باشد");
                            }

                    }
                    else if (passStatus == (byte)DB.TelephoneStatus.Discharge)
                    {
                        if (item.Status == (byte)DB.TelephoneStatus.Free || item.Status == (byte)DB.TelephoneStatus.Destruction)
                        {

                        }
                        else
                        {
                            throw new Exception("امکان تغییر وضعیت نمی باشد");
                        }

                    }
                    else if (passStatus == (byte)DB.TelephoneStatus.Destruction && passStatus != item.Status)
                    {

                        TelephoneStatusLog telephoneStatusLog = Data.TelephonStatusLogDB.GetLastTelephoneLogByTelephoneNo(item.TelephoneNo);
                        if (telephoneStatusLog != null)
                        {
                            item.Status = (byte)telephoneStatusLog.FromStatus;
                        }
                        else
                        {
                            throw new Exception("اخرین وضعیت تلفن پیدا نشد");
                        }

                    }
                    else if (passStatus != item.Status)
                    {
                        throw new Exception("امکان تغییر وضعیت نمی باشد");
                    }

                    if (passStatus != item.Status)
                    {
                        SaveTelephonStatusLog(item.TelephoneNo, passStatus, item.Status);
                    }

                    item.Detach();
                    Save(item);

                    ts.Complete();
                }
                ShowSuccessMessage("تلفن ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تلفن", ex);
            }
        }

        private void SaveTelephonStatusLog(long tel, byte fromStatus, byte toStatus)
        {
            TelephoneStatusLog telephoneStatusLog = new TelephoneStatusLog();
            telephoneStatusLog.TelephoneNo = tel;
            telephoneStatusLog.FromStatus = fromStatus;
            telephoneStatusLog.ToStatus = toStatus;
            telephoneStatusLog.InsertData = DB.GetServerDate();
            telephoneStatusLog.Detach();
            DB.Save(telephoneStatusLog);

        }

        private void SaveTelephonStatusLog(byte passStatus)
        {
            throw new NotImplementedException();
        }
    }
}


