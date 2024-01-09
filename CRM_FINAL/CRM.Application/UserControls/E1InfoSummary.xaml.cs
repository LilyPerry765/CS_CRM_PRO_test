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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for E1InfoSummary.xaml
    /// </summary>
    public partial class E1InfoSummary : Local.UserControlBase
    {
        #region Properties and Fields

        private Guid _FileID { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extension { get; set; }

        long _requestID = 0;
        CRM.Data.E1 _e1 { get; set; }

        private long? _subID;

        List<E1Files> _E1Files = new List<E1Files>();

        #endregion

        #region Constructors

        public E1InfoSummary()
        {
            InitializeComponent();
            Initialize();
        }

        public E1InfoSummary(long requestID, long? subID)
            : this()
        {
            this._requestID = requestID;
            this._subID = subID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            LinkTypeComboBox.ItemsSource = Data.E1LinkTypeDB.GetE1LinkTypeCheckable();
            CodeTypeComboBox.ItemsSource = Data.E1CodeTypeDB.GetE1CodeTypeCheckable();
            ChanalTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.E1ChanalType));
            TelephoneTypeComboBox.ItemsSource = Data.CustomerTypeDB.GetIsShowCustomerTypesCheckable();
        }

        private void LoadData()
        {
            _e1 = Data.E1DB.GetE1ByRequestID(_requestID);
            if (_e1 != null)
            {
                TelephoneTypeComboBox.SelectedValue = _e1.TelephoneType;
                TelephoneTypecomboBox_SelectionChanged(null, null);
                TelephoneTypeGroupComboBox.SelectedValue = _e1.TelephoneTypeGroup;
                Address InstallAddress = Data.AddressDB.GetAddressByID((long)_e1.InstallAddressID);
                InstallPostalCodeTextBox.Text = InstallAddress.PostalCode;
                InstallAddressTextBox.Text = InstallAddress.AddressContent;

                if (_e1.TargetInstallAddressID != null)
                {
                    Address TargetInstallAddress = Data.AddressDB.GetAddressByID((long)_e1.TargetInstallAddressID);
                    TargetInstallPostalCodeTextBox.Text = TargetInstallAddress.PostalCode;
                    TargetInstallAddressTextBox.Text = TargetInstallAddress.AddressContent;
                }

                Address CorrespondenceAddress = Data.AddressDB.GetAddressByID((long)_e1.CorrespondenceAddressID);
                CorrespondencePostalCodeTextBox.Text = CorrespondenceAddress.PostalCode;
                CorrespondenceAddressTextBox.Text = CorrespondenceAddress.AddressContent;

                if (_subID != null)
                {
                    E1Link _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);
                    if (_e1Link.SwitchE1NumberID != null)
                    {
                        E1NumberInfo e1NumberInfo = Data.E1DB.GetDDFPortByE1Number((int)_e1Link.SwitchE1NumberID);
                        AcsessDDFTextBox.Text = "دی دی اف :" + e1NumberInfo.DDFNumber + " ردیف :" + e1NumberInfo.BayNumber + " تیغه :" + e1NumberInfo.PositionNumber + " پی سی ام :" + e1NumberInfo.Number;
                    }
                }

            }
            else
            {
                _e1 = new Data.E1();
            }
            this.DataContext = _e1;
            LoadFiles(_requestID);

            //جهت جلوگیری از لود شدن دوباره که اضافی میباشد
            _IsLoaded = true;
        }

        #endregion

        #region EventHandlers

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void TelephoneTypecomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TelephoneTypeComboBox.SelectedValue != null)
            {
                TelephoneTypeGroupComboBox.ItemsSource = Data.CustomerGroupDB.GetCustomerGroupsCheckableByCustomerTypeID((int)TelephoneTypeComboBox.SelectedValue);
            }
        }

        #endregion

        #region File

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

                switchOffice.ForEach(so => { _E1Files.Add(new E1Files { ID = so.ID, FileID = so.SwitchFileID, InsertDate = so.InsertDate, RequestID = so.RequestID, FileType = "سوئیچ" }); });
                cableDesignOffice.ForEach(co => { _E1Files.Add(new E1Files { ID = co.ID, FileID = co.CableDesignFileID, InsertDate = co.InsertDate, RequestID = co.RequestID, FileType = "کابل" }); });
                transferDepartmentOffice.ForEach(to => { _E1Files.Add(new E1Files { ID = to.ID, FileID = to.TransferDepartmentFileID, InsertDate = to.InsertDate, RequestID = to.RequestID, FileType = "انتقال" }); });
                powerOffice.ForEach(po => { _E1Files.Add(new E1Files { ID = po.ID, FileID = po.PowerFileID, InsertDate = po.InsertDate, RequestID = po.RequestID, FileType = "نیرو" }); });
                FileItemsDataGrid.ItemsSource = _E1Files;
            }
        }

        public void RefreshFiles(long requestId, bool isLoaded = true)
        {
            //به این خاطر خط زیر اضافه شد که ممکن است درخواست ایوان جاری از قبل هم دارای فایل هایی بوده باشد
            //در این صورت علاوه بر آنها فایل های جدید هم به دیتا گرید اضافه میشد
            //_oldE1Files + _newE1Files is incorrect.
            //only _newE1Files
            _E1Files = new List<E1Files>();
            _IsLoaded = isLoaded;
            this.LoadFiles(requestId);
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                E1Files e1FileItem = FileItemsDataGrid.SelectedItem as E1Files;
                if (e1FileItem != null)
                {
                    _FileID = (Guid)e1FileItem.FileID;
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
                                imageProcess.StartInfo.FileName = "ois.exe";
                                imageProcess.StartInfo.Arguments = path;
                                imageProcess.Start();

                                imageProcess.WaitForExit();
                            }

                        }
                        catch (Exception ax)
                        {
                            Logger.Write(ax, "خطا در باز شدن فایل ای وان");
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
                Logger.Write(ex, "خطا در باز شدن فایل ای وان");
            }
        }

        #endregion
    }
}
