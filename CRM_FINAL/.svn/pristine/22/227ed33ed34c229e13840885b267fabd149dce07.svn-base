using CRM.Application.UserControls;
using CRM.Data;
using Enterprise;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for DesignDirectorForm.xaml
    /// </summary>
    /// 

    public partial class DesignDirectorForm : Local.RequestFormBase
    {
        #region Properties And Fileds

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }
        private UserControls.V2SpaceAndPowerInfoSummary _v2SpaceAndOPowerInfoSummary { get; set; }

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        long _requestID = 0;

        Request _request { get; set; }

        CRM.Data.E1 _e1 { get; set; }

        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        CRM.Data.Antenna _antenna { get; set; }


        private long? _subID;


        List<E1Files> _E1Files = new List<E1Files>();

        List<SpaceAndPowerFile> _SpaceAndPowerFiles = new List<SpaceAndPowerFile>();

        #endregion

        #region Constructor

        public DesignDirectorForm()
        {
            InitializeComponent();
            Initialize();
        }
        public DesignDirectorForm(long requstID, long? subID)
            : this()
        {
            _subID = subID;
            base.RequestID = this._requestID = requstID;

        }

        #endregion

        #region Methods
        private void Initialize()
        {

        }

        public void LoadData()
        {
            _request = Data.RequestDB.GetRequestByID(_requestID);
            Status Status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);
            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;


            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.E1:
                    {
                        _e1 = Data.E1DB.GetE1ByRequestID(_requestID);

                        if (Status.StatusType == (byte)DB.RequestStatusType.Confirm)
                        {
                            //DesignDirectorStackPanel.IsEnabled = false;
                            //DesignDirectorLabel.IsEnabled = false;
                            //DescriptionLabel.IsEnabled = false;
                            //DescriptionTextBox.IsEnabled = false;

                            //TransferDepartmentTypeLabel.Visibility = Visibility.Visible;
                            //TransferDepartmentTypeStackPanel.Visibility = Visibility.Visible;
                            //CommandCircuitTypeLabel.Visibility = Visibility.Visible;
                            //CommandCircuitTypeStackPanel.Visibility = Visibility.Visible;
                            //PowerLabel.Visibility = Visibility.Visible;
                            //PowerStackPanel.Visibility = Visibility.Visible;
                            //CableDesignLabel.Visibility = Visibility.Visible;
                            //CableDesignStackPanel.Visibility = Visibility.Visible;
                            //SwitchLabel.Visibility = Visibility.Visible;
                            //SwitchStackPanel.Visibility = Visibility.Visible;
                            DescriptionTextBox.IsReadOnly = true;
                        }
                        DescriptionTextBox.Text = _e1.DesignDirectorDescription;
                        _E1InfoSummary = new E1InfoSummary(_requestID, _subID);
                        _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                        E1InfoSummaryUC.Content = _E1InfoSummary;
                        E1InfoSummaryUC.DataContext = _E1InfoSummary;
                        E1InfoSummaryUC.Visibility = Visibility.Visible;
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Collapsed;

                        if (_subID != null)
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
                        else
                            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        this.DataContext = _e1;
                    }
                    break;
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        //کنترل های مربوط به اطلاعات ای وان نباید نمایش داده شوند
                        E1InfoSummaryUC.Visibility = Visibility.Collapsed;

                        //بازیابی رکورد فضا و پاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_request.ID);

                        //مقداردهی عملیات های مربوطه - با توجه به وضعیت های چرخه کاری
                        ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        //پرکردن کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        _v2SpaceAndOPowerInfoSummary = new V2SpaceAndPowerInfoSummary(_requestID);
                        V2SpaceAndPowerInfoSummaryUC.Content = _v2SpaceAndOPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _v2SpaceAndOPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.Visibility = Visibility.Visible;
                        _v2SpaceAndOPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        this.DataContext = _spaceAndPower;

                        //توضیحات مدیر طراحی
                        DescriptionTextBox.Text = _spaceAndPower.DesignManagerComment;
                    }
                    break;
            }

            LoadFiles();
        }

        private void LoadFiles()
        {
            List<PowerOffice> powerOffice = PowerOfficeDB.GetPowerOfficeByRequestID(_requestID);
            List<CableDesignOffice> cableDesignOffice = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(_requestID);
            List<TransferDepartmentOffice> transferDepartmentOffice = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(_requestID);
            List<SwitchOffice> switchOffice = SwitchOfficeDB.GetSwitchOfficeByRequestID(_requestID);
            ItemsDataGrid.ItemsSource = null;

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.E1:
                    {
                        switchOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.SwitchFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "سوئیچ" }); });
                        cableDesignOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.CableDesignFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "کابل" }); });
                        transferDepartmentOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.TransferDepartmentFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "انتقال" }); });
                        powerOffice.ForEach(t => { _E1Files.Add(new E1Files { ID = t.ID, FileID = t.PowerFileID, InsertDate = t.InsertDate, RequestID = t.RequestID, FileType = "نیرو" }); });
                        ItemsDataGrid.ItemsSource = _E1Files;
                    }
                    break;
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        switchOffice.ForEach(new Action<SwitchOffice>(so =>
                                                                        {
                                                                            _SpaceAndPowerFiles.Add(new SpaceAndPowerFile
                                                                                                        {
                                                                                                            ID = so.ID,
                                                                                                            FileID = so.SwitchFileID,
                                                                                                            InsertDate = so.InsertDate,
                                                                                                            RequestID = so.RequestID,
                                                                                                            FileType = "سوئیچ"
                                                                                                        }
                                                                                                    );
                                                                        }
                                                                      )
                                              );
                        //********************************************************************************************************************************************************************************
                        cableDesignOffice.ForEach(new Action<CableDesignOffice>(co =>
                                                                                    {
                                                                                        _SpaceAndPowerFiles.Add(new SpaceAndPowerFile
                                                                                                                    {
                                                                                                                        ID = co.ID,
                                                                                                                        FileID = co.CableDesignFileID,
                                                                                                                        InsertDate = co.InsertDate,
                                                                                                                        RequestID = co.RequestID,
                                                                                                                        FileType = "کابل"
                                                                                                                    }
                                                                                                                );
                                                                                    }
                                                                                )
                                                  );
                        //********************************************************************************************************************************************************************************
                        transferDepartmentOffice.ForEach(new Action<TransferDepartmentOffice>(to =>
                                                                                                {
                                                                                                    _SpaceAndPowerFiles.Add(new SpaceAndPowerFile
                                                                                                                                {
                                                                                                                                    ID = to.ID,
                                                                                                                                    FileID = to.TransferDepartmentFileID,
                                                                                                                                    InsertDate = to.InsertDate,
                                                                                                                                    RequestID = to.RequestID,
                                                                                                                                    FileType = "انتقال"
                                                                                                                                }
                                                                                                                           );
                                                                                                }
                                                                                              )
                                                        );
                        //********************************************************************************************************************************************************************************
                        powerOffice.ForEach(new Action<PowerOffice>(po =>
                                                                        {
                                                                            _SpaceAndPowerFiles.Add(new SpaceAndPowerFile
                                                                                                        {
                                                                                                            ID = po.ID,
                                                                                                            FileID = po.PowerFileID,
                                                                                                            InsertDate = po.InsertDate,
                                                                                                            RequestID = po.RequestID,
                                                                                                            FileType = "نیرو"
                                                                                                        });
                                                                        }
                                                                    )
                                            );
                        //********************************************************************************************************************************************************************************
                        ItemsDataGrid.ItemsSource = _SpaceAndPowerFiles;
                        break;
                    }
            }
        }

        #endregion

        #region EventHandlers

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Actions

        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    switch (_request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.E1:
                            {
                                _e1 = this.DataContext as CRM.Data.E1;
                                _e1.DesignDirectorDescription = DescriptionTextBox.Text;
                                _e1.Detach();
                                DB.Save(_e1, false);
                                break;
                            }
                        case (byte)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.DesignManagerComment = DescriptionTextBox.Text;
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                                break;
                            }
                    }
                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    ts.Complete();
                    IsSaveSuccess = true;

                }
                ShowSuccessMessage("دخیره اطلاعات انجام شد.");
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
                Save();
                if (IsSaveSuccess == true)
                    IsForwardSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع", ex);
                IsForwardSuccess = false;
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.E1:
                        {
                            _e1 = this.DataContext as CRM.Data.E1;
                            _e1.DesignDirectorDescription = string.Empty;
                            _e1.Detach();
                            DB.Save(_e1, false);
                            break;
                        }
                    case (byte)DB.RequestType.SpaceandPower:
                        {
                            _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                            _spaceAndPower.DesignManagerComment = string.Empty;
                            _spaceAndPower.Detach();
                            DB.Save(_spaceAndPower, false);
                            break;
                        }
                }
                //DocumentsFileDB.DeleteDocumentsFileTable(_FileID);
                //TODO:rad  در مرحله مدیر طراحی - بررسی نهایی باید بتوانیم درخواست ها را بر اساس وضعیتشان از هم تفکیک کنیم
                //چون در فضا و پاور دو ستون توضیخات داریم . یکی برای مرحله سوم و دیگری برای بررسی نهایی
                Subts.Complete();
                IsRejectSuccess = true;
            }

            return IsRejectSuccess;
        }

        #endregion

        #region File

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                object selectedFile = new object();
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.E1:
                        {
                            selectedFile = ItemsDataGrid.SelectedItem;
                            var e1File = selectedFile as E1Files;
                            _FileID = (e1File != null) ? e1File.FileID : default(Guid);
                        }
                        break;
                    case (int)DB.RequestType.SpaceandPower:
                        {
                            selectedFile = ItemsDataGrid.SelectedItem;
                            var spaceAndPowerFile = selectedFile as SpaceAndPowerFile;
                            _FileID = (spaceAndPowerFile != null) ? spaceAndPowerFile.FileID : default(Guid);
                        }
                        break;
                }
                if (selectedFile != null && _FileID != default(Guid))
                {
                    CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
                    FileBytes = fileInfo.Content;
                    Extension = "." + fileInfo.FileType;
                    if (FileBytes != null && Extension != string.Empty)
                    {
                        string path = System.IO.Path.GetTempFileName() + Extension;
                        try
                        {
                            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                            {
                                writer.Write(FileBytes);
                            }

                            if (!(Extension.Contains("jpeg") || Extension.Contains("png") || Extension.Contains("jpg")))
                            {
                                Process p = System.Diagnostics.Process.Start(path);

                                p.WaitForExit();
                            }
                            else
                            {
                                Process imageProcess = new Process();
                                //فایل های عکس را باید با آفیس باز کرد
                                // ois = Microsoft Office Picture Manager
                                imageProcess.StartInfo.FileName = "ois.exe";
                                imageProcess.StartInfo.Arguments = path;
                                imageProcess.Start();

                                imageProcess.WaitForExit();
                            }

                        }
                        catch (Exception ax)
                        {
                            Logger.Write(ax, "خطا در باز شدن فایل فضا و پاور");
                        }
                        finally
                        {

                            int result = DocumentsFileDB.UpdateFileInDocumentsFile(_FileID, File.ReadAllBytes(path));
                            if (result <= 0)
                            {
                                Folder.MessageBox.ShowError("فایل بروز رسانی نشد");
                            }
                            else
                            {
                                File.Delete(path);
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("فایل موجود نمی باشد !");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

        }

        #endregion file

        //private void FileLisBoxItem_Selected(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        //    dlg.Filter = "All files (*.*)|*.*";

        //    if (dlg.ShowDialog() == true)
        //    {
        //        FileBytes = System.IO.File.ReadAllBytes(dlg.FileName);
        //        Extension = System.IO.Path.GetExtension(dlg.FileName);
        //    }

        //    if (FileBytes != null && Extension != string.Empty)
        //    {
        //        _FileID = DocumentsFileDB.SaveFileInDocumentsFile(FileBytes, Extension);
        //    }
        //}

        //private void ScannerLisBox_Selected(object sender, RoutedEventArgs e)
        //{
        //    Scanner oScanner = new Scanner();
        //    string extension;

        //    FileBytes = oScanner.ScannWithExtension(out extension);
        //    Extension = extension;
        //}

        //private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {


        //        if (_e1.DesignDirectorFileID != null)
        //        {
        //            _FileID = (Guid)_e1.DesignDirectorFileID;
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable(_FileID);
        //            FileBytes = fileInfo.Content;
        //            Extension = "." + fileInfo.FileType;
        //        }

        //        if (FileBytes != null && (Extension == ".png" || Extension == ".jpg"  || Extension == ".bmp" ))
        //        {
        //            FileBytes = DocumentsFileDB.GetDocumentsFileTable(_FileID).Content;
        //            CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
        //            window.FileBytes = FileBytes;
        //            window.ShowDialog();
        //        }
        //        else if (FileBytes != null && Extension != string.Empty)
        //        {
        //            string path = System.IO.Path.GetTempFileName() + Extension;
        //            try
        //            {
        //                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                {
        //                    writer.Write(FileBytes);
        //                }

        //                Process p = System.Diagnostics.Process.Start(path);
        //                p.WaitForExit();
        //            }
        //            finally
        //            {
        //                File.Delete(path);
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception("فایل موجود نمی باشد !");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage(ex.Message, ex);
        //    }
        //}

        //private void TransferDepartmentImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    //ShowFile(_e1.TransferDepartmentFileID);
        //}

        //private void CommandCircuitImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    //ShowFile(_e1.CommandCircuitFileID);
        //}

        //private void PowerImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ShowFile(_e1.PowerFileID);
        //}

        //private void CableDesignImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    ShowFile(_e1.CableDesignFileID);
        //}
        //private void SwitchImage_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //}
        //private void ShowFile(Guid? fileID)
        //{
        //    try
        //    {
        //        if (fileID != null)
        //        {
        //            CRM.Data.FileInfo fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)fileID);
        //            fileInfo = DocumentsFileDB.GetDocumentsFileTable((Guid)fileID);
        //            FileBytes = fileInfo.Content;
        //            Extension = "." + fileInfo.FileType;

        //            if (FileBytes != null && (Extension == ".png" || Extension == ".jpg" || Extension == ".bmp"))
        //            {
        //                CRM.Application.Views.DocumentViewForm window = new DocumentViewForm();
        //                window.FileBytes = FileBytes;
        //                window.ShowDialog();
        //            }
        //            else if (FileBytes != null && Extension != string.Empty)
        //            {
        //                string path = System.IO.Path.GetTempFileName() + Extension;
        //                try
        //                {
        //                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        //                    {
        //                        writer.Write(FileBytes);
        //                    }

        //                    Process p = System.Diagnostics.Process.Start(path);
        //                    p.WaitForExit();
        //                }
        //                finally
        //                {
        //                    File.Delete(path);
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("فایل موجود نمی باشد !");
        //            }
        //        }
        //        else
        //        {
        //            Folder.MessageBox.ShowInfo("برای این قسمت فایل اضافه نشده است");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage(ex.Message, ex);
        //    }
        //}

    }
}
