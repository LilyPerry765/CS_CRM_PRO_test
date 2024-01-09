using CRM.Data;
using CRM.Data.Schema;
using Enterprise;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CabinetInputMalfunctionRegistrationForm.xaml
    /// </summary>
    public partial class CabinetInputMalfunctionRegistrationForm : Local.PopupWindow
    {
        #region Properties and Fields

        private CabinetInput _cabinetInput;

        private long _cabinetInputId;
        public long CabinetInputId
        {
            get { return _cabinetInputId; }
            set { _cabinetInputId = value; }
        }

        #endregion

        #region Constructors

        public CabinetInputMalfunctionRegistrationForm()
        {
            InitializeComponent();
            Initialize();
        }
        public CabinetInputMalfunctionRegistrationForm(long cabinetInputId)
            : this()
        {
            _cabinetInputId = cabinetInputId;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            TypeMalfunctionComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CabinetInputMalfuctionType));
            MalfuctionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.MalfuctionType));

            //چون ثبت خرابی جاری متعلق به مرکزی میباشد بنابراین باید به طور پیش فرض در حالت انتخاب شده باشد
            MalfuctionTypeComboBox.SelectedItem = MalfuctionTypeComboBox.Items
                                                                        .Cast<EnumItem>()
                                                                        .Where(ei => ei.ID == (byte)DB.MalfuctionType.CabinetInput)
                                                                        .Single();
        }

        #endregion

        #region EventHandlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime currentDate = DB.GetServerDate();
            _cabinetInput = new CabinetInput();
            Malfuction malfaction = new Malfuction();

            //خواندن دیتای مرکزی جاری
            _cabinetInput = CabinetInputDB.GetCabinetInputByID(this.CabinetInputId);

            //مقداردهی برخی از پراپرتی های رکورد خرابی
            malfaction.DateMalfunction = currentDate;
            malfaction.TimeMalfunction = currentDate.ToString("HH:mm:ss tt"); //13:25:10 ب-ظ
            malfaction.MalfuctionType = (byte)DB.MalfuctionType.CabinetInput;

            this.DataContext = malfaction;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                return;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (TypeMalfunctionComboBox.SelectedValue == null)
                    {
                        MessageBox.Show(".علت خرابی را مشخص نمائید", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (_cabinetInput.Status == (byte)DB.CabinetInputStatus.Malfuction)
                    {
                        MessageBox.Show(".مرکزی در وضعیت خراب قرار دارد", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                        return;
                    }

                    //تغییر وضعیت مرکزی به خراب
                    _cabinetInput.Status = (byte)DB.CabinetInputStatus.Malfuction;
                    _cabinetInput.Detach();
                    DB.Save(_cabinetInput, false);

                    //بدست آوردن اطلاعات وارد شده در کنترل های فرم
                    Malfuction item = this.DataContext as Malfuction;

                    //شناسه مرکزی انتخاب شده را در رکورد خرابی میریزیم
                    item.CabinetInputID = this.CabinetInputId;
                    item.MalfuctionOrhealthy = (byte)DB.MalfuctionStatus.Malfuction;
                    item.LicenseNumber = LicenseUserControl.LisenseNumber;
                    item.LicenseFile = (System.Data.Linq.Binary)LicenseUserControl.lisenseFile;
                    item.Detach();
                    DB.Save(item, true);

                    //بدست آوردن مرکز مخابراتی مرکزی 
                    Center center = CenterDB.GetCenterByCabinetInputID(this.CabinetInputId);

                    //در انتهای ثبت خرابی باید عملیات های در دیتابیس لاگ زده شوند
                    ActionLog actionLog = new ActionLog();
                    actionLog.ActionID = (byte)DB.ActionLog.CablePaired;
                    actionLog.Date = DB.GetServerDate();
                    actionLog.UserName = Folder.User.Current.Username;

                    CablePairedMalFuction cablePairedMalFuction = new CablePairedMalFuction();
                    cablePairedMalFuction.CenterID = center.ID;
                    cablePairedMalFuction.CenterName = center.CenterName;
                    cablePairedMalFuction.Date = DB.GetServerDate();
                    cablePairedMalFuction.Description = item.Description;
                    cablePairedMalFuction.Status = (byte)DB.CabinetInputStatus.Malfuction;
                    cablePairedMalFuction.CabinetInputID = this.CabinetInputId;
                    //ثبت جزئیات خرابی
                    actionLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CablePairedMalFuction>(cablePairedMalFuction, true));
                    actionLog.Detach();

                    DB.Save(actionLog, true);

                    scope.Complete();

                    ShowSuccessMessage("ذخیره وضعیت انجام شد");
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در ذخیره - ثبت خرابی مرکزی");
                ShowErrorMessage("ذخیره خرابی با خطا مواجه شد", ex);
            }
        }

        #endregion

    }
}
