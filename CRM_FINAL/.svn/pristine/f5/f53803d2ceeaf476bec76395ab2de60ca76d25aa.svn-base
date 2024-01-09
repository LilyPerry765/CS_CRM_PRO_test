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
using CRM.Data;
using Microsoft.Win32;
using System.IO;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class SpaceAndPowerForm : Local.RequestFormBase
    {
        #region Properties

        private Data.SpaceAndPower _SpaceAndPower { get; set; }

        private Data.Request _Request { get; set; }

        private Data.RequestInfo _RequestInfo { get; set; }

        public byte[] FileBytes { get; set; }

        public string Extension { get; set; }

        public Guid? FileID { get; set; }

        private Guid? CircuitCommandFileID { get; set; }

        public byte[] CircuitCommandFileBytes { get; set; }

        public string CircuitCommandFileExtension { get; set; }

        #endregion

        #region Constructors

        public SpaceAndPowerForm(long requestID)
        {
            InitializeComponent();

            RequestID = requestID;

            Initialize();
            LoadData();
            this.Closed += SpaceAndPowerForm_Closed;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Exit };
        }

        private void LoadData()
        {
            try
            {
                _Request = Data.RequestDB.GetRequestByID(RequestID);
                _SpaceAndPower = DB.SearchByPropertyName<Data.SpaceAndPower>("ID", RequestID).SingleOrDefault();

                SpaceAndPowerInfo spaceAndPowerInfo = Data.SpaceAndPowerDB.GetSpaceAndPowerInfoByID(RequestID);
                SpaceAnfPowerInfo.DataContext = spaceAndPowerInfo;

                _RequestInfo = Data.RequestDB.GetRequestInfoByID(RequestID);
                RequestInfo.DataContext = _RequestInfo;

                switch (_RequestInfo.StepID)
                {
                    case (int)DB.RequestStepSpaceAndPower.FinancialScope:
                        {
                            FinancialScopeGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManager:
                        {
                            DesignManagerGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.CableAndNetworkDesignOffice:
                        {
                            CableAndNetworkDesignOfficeGroupBox.Visibility = Visibility.Visible;
                            FileID = spaceAndPowerInfo.CableAndNetworkDesignOfficeFile;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Enteghal:
                        {
                            EnteghalGroupBox.Visibility = Visibility.Visible;
                            FileID = spaceAndPowerInfo.EnteghalFile;
                            CircuitCommandFileID = spaceAndPowerInfo.CircuitCommandFile;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SwitchDesigningOffice:
                        {
                            SwitchDesigningOfficeGroupBox.Visibility = Visibility.Visible;
                            FileID = spaceAndPowerInfo.SwitchDesigningOfficeFile;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Niroo:
                        {
                            NirooGroupBox.Visibility = Visibility.Visible;
                            FileID = spaceAndPowerInfo.NirooFile;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Sakhteman:
                        {
                            SakhtemanGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManagerFinalCheck:
                        {
                            DesignManagerFinalCheckGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.NetworkAssistant:
                        {
                            NetworkAssistantGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Ghardad:
                        {
                            GhardadGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DeviceHall:
                        {
                            DeviceHallGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.AdministrationOfTheTelecommunicationEquipment:
                        {
                            AdministrationOfTheTelecommunicationEquipmentGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SooratHesab:
                        {
                            SooratHesabGroupBox.Visibility = Visibility.Visible;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لود رکورد فضا و پاور");
                MessageBox.Show("خطا در فراخوانی!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override bool Confirm()
        {
            try
            {
                switch (_RequestInfo.StepID)
                {
                    case (int)DB.RequestStepSpaceAndPower.FinancialScope:
                        {
                            _SpaceAndPower.FinancialScopeComment = FinancialScopeCommentTextBox.Text.Trim();
                            _SpaceAndPower.FinancialScopeDate = DB.GetServerDate();
                            _SpaceAndPower.FinancialScopeUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManager:
                        {
                            _SpaceAndPower.DesignManagerComment = DesignManagerCommentTextBox.Text.Trim();
                            _SpaceAndPower.DesignManagerDate = DB.GetServerDate();
                            _SpaceAndPower.DesignManagerUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.CableAndNetworkDesignOffice:
                        {
                            _SpaceAndPower.CableAndNetworkDesignOfficeComment = CableAndNetworkDesignOfficeCommentTextBox.Text.Trim();
                            _SpaceAndPower.CableAndNetworkDesignOfficeDate = DB.GetServerDate();
                            _SpaceAndPower.CableAndNetworkDesignOfficeUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.CableAndNetworkDesignOfficeFile = FileID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Enteghal:
                        {
                            _SpaceAndPower.EnteghalDate = DB.GetServerDate();
                            _SpaceAndPower.EnteghalUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.EnteghalComment = EnteghalCommentTextBox.Text.Trim();
                            _SpaceAndPower.EnteghalFile = FileID;
                            _SpaceAndPower.CircuitCommandFile = CircuitCommandFileID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SwitchDesigningOffice:
                        {
                            _SpaceAndPower.SwitchDesigningOfficeComment = SwitchDesigningOfficeCommentTextBox.Text.Trim();
                            _SpaceAndPower.SwitchDesigningOfficeDate = DB.GetServerDate();
                            _SpaceAndPower.SwitchDesigningOfficeUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.SwitchDesigningOfficeFile = FileID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Niroo:
                        {
                            _SpaceAndPower.NirooComment = NirooCommentTextBox.Text.Trim();
                            _SpaceAndPower.NirooDate = DB.GetServerDate();
                            _SpaceAndPower.NirooUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.NirooFile = FileID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Sakhteman:
                        {
                            _SpaceAndPower.SakhtemanDate = DB.GetServerDate();
                            _SpaceAndPower.SakhtemanUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.SakhtemanComment = SakhtemanCommentTextBox.Text.Trim();
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManagerFinalCheck:
                        {
                            _SpaceAndPower.DesignManagerFinalCheckComment = DesignManagerFinalCheckCommentTextBox.Text.Trim();
                            _SpaceAndPower.DesignManagerFinalCheckDate = DB.GetServerDate();
                            _SpaceAndPower.DesignManagerFinalCheckUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.NetworkAssistant:
                        {
                            _SpaceAndPower.NetworkAssistantComment = NetworkAssistantCommentTextBox.Text.Trim();
                            _SpaceAndPower.NetworkAssistantDate = DB.GetServerDate();
                            _SpaceAndPower.NetworkAssistantUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Ghardad:
                        {
                            _SpaceAndPower.GhardadDate = DB.GetServerDate();
                            _SpaceAndPower.GhardadUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.GhardadComment = GhardadCommentTextBox.Text.Trim();
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DeviceHall:
                        {
                            _SpaceAndPower.DeviceHallComment = DeviceHallCommentTextBox.Text.Trim();
                            _SpaceAndPower.DeviceHallDate = DB.GetServerDate();
                            _SpaceAndPower.DeviceHallUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.AdministrationOfTheTelecommunicationEquipment:
                        {
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentComment = AdministrationOfTheTelecommunicationEquipmentCommentTextBox.Text.Trim();
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentOperationDate = AdministrationOfTheTelecommunicationEquipmentOperationDatePicker.SelectedDate;
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentDate = DB.GetServerDate();
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentUserID = DB.CurrentUser.ID;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SooratHesab:
                        {
                            _SpaceAndPower.SooratHesabDate = DB.GetServerDate();
                            _SpaceAndPower.SooratHesabUserID = DB.CurrentUser.ID;
                            _SpaceAndPower.SooratHesabComment = SooratHesabCommentTextBox.Text.Trim();
                            break;
                        }
                    default:
                        break;
                }

                Data.RequestForSpaceAndPower.SaveSpaceAndPowerRequest(_Request, _SpaceAndPower, null, false);

                IsConfirmSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("عملیات تایید ناموفق بود !", ex);
            }

            return IsConfirmSuccess;
        }

        public override bool Deny()
        {
            try
            {
                switch (_RequestInfo.StepID)
                {
                    case (int)DB.RequestStepSpaceAndPower.FinancialScope:
                        {
                            _SpaceAndPower.DesignManagerFinalCheckComment = string.Empty;
                            _SpaceAndPower.FinancialScopeDate = null;
                            _SpaceAndPower.FinancialScopeUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManager:
                        {
                            _SpaceAndPower.DesignManagerComment = string.Empty;
                            _SpaceAndPower.DesignManagerDate = null;
                            _SpaceAndPower.DesignManagerUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.CableAndNetworkDesignOffice:
                        {
                            _SpaceAndPower.CableAndNetworkDesignOfficeComment = string.Empty;
                            _SpaceAndPower.CableAndNetworkDesignOfficeDate = null;
                            _SpaceAndPower.CableAndNetworkDesignOfficeUserID = null;
                            Guid? cableAndNetworkFileId = _SpaceAndPower.CableAndNetworkDesignOfficeFile;
                            _SpaceAndPower.CableAndNetworkDesignOfficeFile = null;

                            //به خاطر جلوگیری از خطا در هنگام حذف فایل باید نال شدن به سمت دیتابیس برگردد
                            //because : Enforce Foreign Key Constraint = YES
                            _SpaceAndPower.Detach();
                            DB.Save(_SpaceAndPower);

                            if (cableAndNetworkFileId.HasValue)
                            {
                                DocumentsFileDB.DeleteDocumentsFileTable(cableAndNetworkFileId.Value);
                            }
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Enteghal:
                        {
                            _SpaceAndPower.EnteghalComment = string.Empty;
                            _SpaceAndPower.EnteghalDate = null;
                            _SpaceAndPower.EnteghalUserID = null;
                            Guid? enteghalFileId = _SpaceAndPower.EnteghalFile;
                            Guid? circuitCommandFileId = _SpaceAndPower.CircuitCommandFile;
                            _SpaceAndPower.CircuitCommandFile = null;
                            _SpaceAndPower.EnteghalFile = null;

                            //به خاطر جلوگیری از خطا در هنگام حذف فایل باید نال شدن به سمت دیتابیس برگردد
                            //because : Enforce Foreign Key Constraint = YES
                            _SpaceAndPower.Detach();
                            DB.Save(_SpaceAndPower);

                            if (enteghalFileId.HasValue)
                            {
                                DocumentsFileDB.DeleteDocumentsFileTable(enteghalFileId.Value);
                            }
                            if (circuitCommandFileId.HasValue)
                            {
                                DocumentsFileDB.DeleteDocumentsFileTable(circuitCommandFileId.Value);
                            }
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SwitchDesigningOffice:
                        {
                            _SpaceAndPower.SwitchDesigningOfficeComment = string.Empty;
                            _SpaceAndPower.SwitchDesigningOfficeDate = null;
                            _SpaceAndPower.SwitchDesigningOfficeUserID = null;
                            Guid? switchDesignFileId = _SpaceAndPower.SwitchDesigningOfficeFile;
                            _SpaceAndPower.SwitchDesigningOfficeFile = null;

                            //به خاطر جلوگیری از خطا در هنگام حذف فایل باید نال شدن به سمت دیتابیس برگردد
                            //because : Enforce Foreign Key Constraint = YES
                            _SpaceAndPower.Detach();
                            DB.Save(_SpaceAndPower);

                            if (switchDesignFileId.HasValue)
                            {
                                DocumentsFileDB.DeleteDocumentsFileTable(switchDesignFileId.Value);
                            }
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Niroo:
                        {
                            _SpaceAndPower.NirooComment = string.Empty;
                            _SpaceAndPower.NirooDate = null;
                            _SpaceAndPower.NirooUserID = null;
                            Guid? nirooFileId = _SpaceAndPower.NirooFile;
                            _SpaceAndPower.NirooFile = null;

                            //به خاطر جلوگیری از خطا در هنگام حذف فایل باید نال شدن به سمت دیتابیس برگردد
                            //because : Enforce Foreign Key Constraint = YES
                            _SpaceAndPower.Detach();
                            DB.Save(_SpaceAndPower);

                            if (nirooFileId.HasValue)
                            {
                                DocumentsFileDB.DeleteDocumentsFileTable(nirooFileId.Value);
                            }
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Sakhteman:
                        {
                            _SpaceAndPower.SakhtemanComment = string.Empty;
                            _SpaceAndPower.SakhtemanDate = null;
                            _SpaceAndPower.SakhtemanUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DesignManagerFinalCheck:
                        {
                            _SpaceAndPower.DesignManagerFinalCheckComment = string.Empty;
                            _SpaceAndPower.DesignManagerFinalCheckDate = null;
                            _SpaceAndPower.DesignManagerFinalCheckUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.NetworkAssistant:
                        {
                            _SpaceAndPower.NetworkAssistantComment = string.Empty;
                            _SpaceAndPower.NetworkAssistantDate = null;
                            _SpaceAndPower.NetworkAssistantUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.Ghardad:
                        {
                            _SpaceAndPower.GhardadComment = string.Empty;
                            _SpaceAndPower.GhardadDate = null;
                            _SpaceAndPower.GhardadUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.DeviceHall:
                        {
                            _SpaceAndPower.DeviceHallComment = string.Empty;
                            _SpaceAndPower.DeviceHallDate = null;
                            _SpaceAndPower.DeviceHallUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.AdministrationOfTheTelecommunicationEquipment:
                        {
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentComment = string.Empty;
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentDate = null;
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentOperationDate = null;
                            _SpaceAndPower.AdministrationOfTheTelecommunicationEquipmentUserID = null;
                            break;
                        }
                    case (int)DB.RequestStepSpaceAndPower.SooratHesab:
                        {
                            _SpaceAndPower.SooratHesabComment = string.Empty;
                            _SpaceAndPower.SooratHesabDate = null;
                            _SpaceAndPower.SooratHesabUserID = null;
                            break;
                        }
                    default:
                        {
                            //do nothing
                            break;
                        }
                }

                Data.RequestForSpaceAndPower.SaveSpaceAndPowerRequest(_Request, _SpaceAndPower, null, false);

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage(".عملیات رد ناموفق بود", ex);
            }
            return IsRejectSuccess;
        }

        #endregion

        #region Event Handlers

        private void ShowFullView(object sender, MouseButtonEventArgs e)
        {
            SpaceAndPowerFullView window = new SpaceAndPowerFullView(RequestID);
            window.ShowDialog();
        }

        //TODO:rad
        private void FileSelectionMethodsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //برای تعیین فایل طرح دو روش وجود دارد
            // 1- جستجو در فایل های موجود در هارد
            // 2- اسکن فایل از طریق اسکنر

            ListBox originalSource = e.OriginalSource as ListBox;
            if (originalSource != null && originalSource.SelectedIndex != -1)
            {
                ListBoxItem selectedItem = originalSource.SelectedItem as ListBoxItem;
                if (selectedItem != null)
                {
                    string tag = (selectedItem.Tag != null) ? selectedItem.Tag.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(tag))
                    {
                        try
                        {
                            switch (tag)
                            {
                                case "BrowseFile": // 1- جستجو در فایل های موجود در هارد
                                    {
                                        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                                        openFileDialog.Filter = "All files (*.*)|*.*";
                                        if (openFileDialog.ShowDialog().Value)
                                        {
                                            FileBytes = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                                            Extension = System.IO.Path.GetExtension(openFileDialog.FileName);
                                        }

                                        if (FileBytes != null && !string.IsNullOrEmpty(Extension))
                                        {
                                            FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                                            (this.Resources["SuccessAttachmentStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                                        }

                                        break;
                                    }
                                case "ScannerFile":// 2- اسکن فایل از طریق اسکنر
                                    {
                                        Scanner scanner = new Scanner();
                                        string extension;
                                        FileBytes = scanner.ScannWithExtension(out extension);
                                        Extension = extension;

                                        if (FileBytes != null && !string.IsNullOrEmpty(Extension))
                                        {
                                            FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
                                            (this.Resources["SuccessAttachmentStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                                        }
                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex, "خطا در تعیین فایل برای روال فضا و پاور");
                            originalSource.SelectedIndex = -1;
                            MessageBox.Show("خطا در وارد کردن فایل!", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        //TODO:rad
        private void PreviewImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image originalSource = e.OriginalSource as Image;
                if (originalSource != null)
                {
                    CRM.Data.FileInfo fileInfo = new Data.FileInfo();

                    //زمانی  کاربر میتواند فایلی را ویرایش کند که ابتدا آن را اضافه کرده باشد
                    if (FileID.HasValue)
                    {
                        fileInfo = DocumentsFileDB.GetDocumentsFileTable(FileID.Value);
                    }
                    else
                    {
                        MessageBox.Show(".فایلی وجود ندارد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (
                        (fileInfo.Content != null && fileInfo.Content.Length > 0)
                        &&
                        !string.IsNullOrEmpty(fileInfo.FileType)
                       )
                    {
                        string tempFilePath = string.Format("{0}{1}.{2}", System.IO.Path.GetTempPath(), "sample", fileInfo.FileType);
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(tempFilePath, FileMode.Create)))
                            {
                                writer.Write(fileInfo.Content);
                            }
                            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.Start(tempFilePath);
                            currentProcess.WaitForExit();
                        }
                        finally
                        {
                            //فایل ویرایش شده را از مسیر تمپ خوانده و بر روی رکورد قبلی بروزرسانی میکند
                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(FileID.Value, File.ReadAllBytes(tempFilePath));

                            if (result <= 0) //بروزرسانی ناموفق
                            {
                                (this.Resources["FialureUpdateStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                            }
                            else //بروزرسانی موفق
                            {
                                //در صورت بروزرسانی موفق باید فایل ویرایش شده جدید که در مسیر موقت ساخته شده است ، حذف گردد
                                File.Delete(tempFilePath);
                                (this.Resources["SuccessUpdateStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ویرایش/بازکردن فایل طرح", ex);
                Logger.Write(ex, "خطا در ویرایش فایل طرح - روال فضا و پاور");
            }
        }

        //TODO:rad
        private void SpaceAndPowerForm_Closed(object sender, EventArgs e)
        {
            //چنانچه کاربر یک فایل را اضافه کند ولی تایید را نزند ، مسلما باید آن فایل پاک شود
            if (!this.IsConfirmSuccess && FileID.HasValue)
            {
                DocumentsFileDB.DeleteDocumentsFileTable(FileID.Value);
            }
            else if (!this.IsConfirmSuccess && CircuitCommandFileID.HasValue)
            {
                //چون در مرحله انتقال علاوه بر فایل طرح ، فایل دستور مداری نیز ممکن است اضافه شده باشد
                DocumentsFileDB.DeleteDocumentsFileTable(CircuitCommandFileID.Value);
            }
        }

        //TODO:rad
        private void CircuitCommandFileSelectionMethodsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //چون برای مرحله انتقال کاربر باید بتواند 2 فایل را مشخص کند این قسمت به طور جداگانه نوشته شد
            //برای تعیین فایل دستور مداری دو روش وجود دارد
            //1- جستجو در فایل های موجود در هارد 
            //2- اسکن فایل از طریق اسکنر
            ListBox originalSource = e.OriginalSource as ListBox;
            if (originalSource != null && originalSource.SelectedIndex != -1)
            {
                ListBoxItem selectedItem = originalSource.SelectedItem as ListBoxItem;
                if (selectedItem != null)
                {
                    string tag = (selectedItem.Tag != null) ? selectedItem.Tag.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(tag))
                    {
                        try
                        {
                            switch (tag)
                            {
                                case "BrowseFile":
                                    {
                                        OpenFileDialog openFileDialog = new OpenFileDialog();
                                        openFileDialog.Filter = "All files (*.*)|*.*";
                                        if (openFileDialog.ShowDialog().Value)
                                        {
                                            CircuitCommandFileBytes = File.ReadAllBytes(openFileDialog.FileName);
                                            CircuitCommandFileExtension = System.IO.Path.GetExtension(openFileDialog.FileName);
                                        }
                                        if (CircuitCommandFileBytes != null && !string.IsNullOrEmpty(CircuitCommandFileExtension))
                                        {
                                            CircuitCommandFileID = DocumentsFileDB.SaveFileInDocumentsFile(CircuitCommandFileBytes, CircuitCommandFileExtension);
                                            (this.Resources["SuccessAttachmentStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();

                                        }
                                        break;
                                    }
                                case "ScannerFile":
                                    {
                                        Scanner scanner = new Scanner();
                                        string extension;
                                        CircuitCommandFileBytes = scanner.ScannWithExtension(out extension);
                                        CircuitCommandFileExtension = extension;
                                        if (CircuitCommandFileBytes != null && !string.IsNullOrEmpty(CircuitCommandFileExtension))
                                        {
                                            CircuitCommandFileID = DocumentsFileDB.SaveFileInDocumentsFile(CircuitCommandFileBytes, CircuitCommandFileExtension);
                                            (this.Resources["SuccessAttachmentStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                                        }
                                        break;
                                    }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Write(ex, "خطا در تعیین فایل دستور مداری - روال فضا و پاور");
                            originalSource.SelectedIndex = -1;
                            MessageBox.Show("خطا در وارد کردن فایل!", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        //TODO:rad
        private void CircuitCommandFilePreviewImage_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image originalSource = e.OriginalSource as Image;
                if (originalSource != null)
                {
                    CRM.Data.FileInfo fileInfo = new Data.FileInfo();

                    //زمانی کاربر میتواند فایل دستور مداری را ویرایش کند که ابتدا آن را اضافه کرده باشد
                    if (CircuitCommandFileID.HasValue)
                    {
                        fileInfo = DocumentsFileDB.GetDocumentsFileTable(CircuitCommandFileID.Value);
                    }
                    else
                    {
                        MessageBox.Show(".فایلی وجود ندارد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    if (
                        (fileInfo.Content != null && fileInfo.Content.Length > 0)
                        &&
                        !string.IsNullOrEmpty(fileInfo.FileType)
                       )
                    {
                        string tempFilePath = string.Format("{0}{1}.{2}", System.IO.Path.GetTempPath(), "sample", fileInfo.FileType);
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(tempFilePath, FileMode.Create)))
                            {
                                writer.Write(fileInfo.Content);
                            }
                            System.Diagnostics.Process currentProcess = System.Diagnostics.Process.Start(tempFilePath);
                            currentProcess.WaitForExit();
                        }
                        finally
                        {
                            //فایل ویرایش شده را از مسیر تمپ خوانده و بر روی رکورد قبلی بروزرسانی میکند
                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(CircuitCommandFileID.Value, File.ReadAllBytes(tempFilePath));

                            if (result <= 0) //بروزرسانی ناموفق
                            {
                                (this.Resources["FialureUpdateStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                            }
                            else //بروزرسانی موفق
                            {
                                //در صورت بروزرسانی موفق باید فایل ویرایش شده جدید که در مسیر موقت ساخته شده است ، حذف گردد
                                File.Delete(tempFilePath);
                                (this.Resources["SuccessUpdateStoryBoard"] as System.Windows.Media.Animation.Storyboard).Begin();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ویرایش/بازکردن فایل دستور مداری", ex);
                Logger.Write(ex, "خطا در ویرایش فایل دستور مداری - روال فضا و پاور");
            }
        }

        #endregion

    }
}
