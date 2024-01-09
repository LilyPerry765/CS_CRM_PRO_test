using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for V2SpaceAndPowerInfoSummary.xaml
    /// </summary>
    public partial class V2SpaceAndPowerInfoSummary : UserControlBase
    {
        #region Properties And Fields

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        long _requestID = 0;

        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        CRM.Data.Address _Address { get; set; }

        CRM.Data.Antenna _antenna { get; set; }

        List<SpaceAndPowerFile> _SpaceAndPowerFiles = new List<SpaceAndPowerFile>();

        List<PowerTypeInfo> _PowerTypeInfos = new List<PowerTypeInfo>();

        #endregion

        #region Constructor

        public V2SpaceAndPowerInfoSummary()
        {
            InitializeComponent();
            Initialize();
        }

        public V2SpaceAndPowerInfoSummary(long requestID)
            : this()
        {
            this._requestID = requestID;
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            EquipmentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.EquipmentType));
            SpaceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SpaceType));
            PowerTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PowerType));
        }

        public void LoadData()
        {
            if (this._requestID != 0)
            {
                _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(this._requestID);
                _antenna = SpaceAndPowerDB.GetAntennaBySpaceAndPowerId(_spaceAndPower.ID);

                //چون نوع پاور مصرفی در یک جدول دیگر نگهداری میشود ، برای نمایش آن باید پاورهای مصرفی را گرفته  و در یک لیست نمایش دهیم
                _PowerTypeInfos = PowerTypeDB.GetPowerTypeInfosBySpaceAndPowerID(_spaceAndPower.ID);
                if (_PowerTypeInfos.Count != 0) 
                {
                    //string powerTypeString = string.Join(" , ", _PowerTypes.Select(pt => pt.Title).ToList().ToArray());
                    //PowerTypesTextBox.Text = powerTypeString;
                    PowerTypesListView.ItemsSource = _PowerTypeInfos;
                }

                if (_spaceAndPower.AddressID.HasValue)
                {
                    _Address = AddressDB.GetAddressByID(_spaceAndPower.AddressID.Value);
                }
            }
            AntennaInfoGrid.DataContext = _antenna;
            AddressGrid.DataContext = _Address;
            this.DataContext = _spaceAndPower;
            LoadFiles(_requestID);

            //جهت جلوگیری از لود شدن دوباره که اضافی میباشد
            _IsLoaded = true;
        }

        private void LoadFiles(long requestId)
        {
            //چون در دبلیو پی اف برخی از یوزر کنترل ها دو بار لود میشوند بلاک  ایف اضافه شد
            if (_IsLoaded == false)
            {
                List<PowerOffice> powerOffice = PowerOfficeDB.GetPowerOfficeByRequestID(requestId);
                List<CableDesignOffice> cableDesignOffice = CableDesignOfficeDB.GetCableDesignOfficeByRequestID(requestId);
                List<TransferDepartmentOffice> transferDepartmentOffice = TransferDepartmentDB.GetTransferDepartmentOfficeByRequestID(requestId);
                List<SwitchOffice> switchOffice = SwitchOfficeDB.GetSwitchOfficeByRequestID(requestId);

                FileItemsDataGrid.ItemsSource = null;

                switchOffice.ForEach(so => { _SpaceAndPowerFiles.Add(new SpaceAndPowerFile { ID = so.ID, FileID = so.SwitchFileID, InsertDate = so.InsertDate, RequestID = so.RequestID, FileType = "سوئیچ" }); });
                cableDesignOffice.ForEach(co => { _SpaceAndPowerFiles.Add(new SpaceAndPowerFile { ID = co.ID, FileID = co.CableDesignFileID, InsertDate = co.InsertDate, RequestID = co.RequestID, FileType = "کابل" }); });
                transferDepartmentOffice.ForEach(to => { _SpaceAndPowerFiles.Add(new SpaceAndPowerFile { ID = to.ID, FileID = to.TransferDepartmentFileID, InsertDate = to.InsertDate, RequestID = to.RequestID, FileType = "انتقال" }); });
                powerOffice.ForEach(po => { _SpaceAndPowerFiles.Add(new SpaceAndPowerFile { ID = po.ID, FileID = po.PowerFileID, InsertDate = po.InsertDate, RequestID = po.RequestID, FileType = "نیرو" }); });
                FileItemsDataGrid.ItemsSource = _SpaceAndPowerFiles;
            }
        }

        public void RefreshFiles(long requestId, bool isLoaded = true)
        {
            //به این خاطر خط زیر اضافه شد که ممکن است درخواست فضا و پاور جاری از قبل هم دارای فایل هایی بوده باشد
            //در این صورت علاوه بر آنها فایل های جدید هم به دیتا گرید اضافه میشد
            //_oldSpaceAndPowerFiles + _newSpaceAndPowerFiles is incorrect.
            //only _newSpcaeAndPowerFiles
            _SpaceAndPowerFiles = new List<SpaceAndPowerFile>();
            _IsLoaded = isLoaded;
            this.LoadFiles(requestId);
        }

        #endregion

        #region EventHandlers

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SpaceAndPowerFile spaceAndPowerFileItem = FileItemsDataGrid.SelectedItem as SpaceAndPowerFile;
                if (spaceAndPowerFileItem != null)
                {
                    _FileID = (Guid)spaceAndPowerFileItem.FileID;
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
                                //بازکردن عکس با استفاده از 
                                //microsoft picture manager
                                imageProcess.StartInfo.FileName = "ois.exe";
                                imageProcess.StartInfo.Arguments = path;
                                imageProcess.Start();

                                imageProcess.WaitForExit();
                            }

                        }
                        catch (Exception ax)
                        {
                            Logger.Write(ax, "خطا در باز شدن فایل فضا و پاور");
                            //Folder.MessageBox.ShowInfo("برای باز کردن این نوع فایل باید نرم افزار Microsoft Picture Manager را بروی سیستم نصب کنید.");
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
                Logger.Write(ex, "خطا در باز شدن فایل فضا و پاور");
            }
        }

        #endregion
    }
}
