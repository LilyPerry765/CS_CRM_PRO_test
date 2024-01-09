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
using System.ComponentModel;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for GeneralInformationList.xaml
    /// </summary>
    public partial class GeneralInformationList : Local.ExtendedTabWindowBase
    {

        #region Properties and Fields

        BackgroundWorker worker;
        List<AssignmentInfo> assingnmentInfo;
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;
        System.Data.DataTable DT;

        #endregion

        #region Constructor
        public GeneralInformationList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods
        private void Initialize()
        {
            assingnmentInfo = new List<AssignmentInfo>();

            //BackgroundWorker initialization
            //milad doran
            //worker = new BackgroundWorker();
            //worker.DoWork += new DoWorkEventHandler(DoWorker);
            //worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(completedWorker);
            //BackgroundWorker initialization

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            OtherBuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable();
            BuchtTypeComboBox.ItemsSource = Data.BuchtTypeDB.GetBuchtTypeCheckable();
            CauseOfCutComboBox.ItemsSource = CauseOfCutDB.GetCauseOfCutCheckableItem();

            // BuchtStatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtStatus));
            // TelephoneStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));


            List<CheckableItem> statues = Helper.GetEnumCheckable(typeof(DB.TelephoneStatus));
            statues.Add(new CheckableItem { ID = -1, Name = "فعال", IsChecked = false });
            TelephoneStatusComboBox.ItemsSource = statues;

            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PreCodeType));
            UsageTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneUsageType));

            AORBTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.AORBPostAndCabinet));

            TelephoneClassComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ClassTelephone));
            BuchtStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BuchtStatus)).Where(ci => ci.ID <= 3).ToList();

            PCMTypeComboBox.ItemsSource = Data.PCMTypeDB.GetPCMTypeCheckable();
        }

        #endregion

        #region BackgroundWorker EventHandlers

        //milad doran
        //private void completedWorker(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    ItemsDataGrid.ItemsSource = assingnmentInfo;
        //    //DateTime st = DateTime.Now;
        //
        //    //ItemsDataGrid.ItemsSource = DT.DefaultView;
        //    //DateTime et = DateTime.Now;
        //    //var pt = (et - st).Seconds;
        //    //System.Diagnostics.Debug.WriteLine("Load data : " + pt.ToString());
        //}

        //milad doran
        //private void DoWorker(object sender, DoWorkEventArgs e)
        //{
        //    Dispatcher.BeginInvoke(
        //        (Action)delegate()
        //        {
        //            this.Cursor = Cursors.Wait;
        //            int startRowIndex = Pager.StartRowIndex;
        //            int pageSize = Pager.PageSize;

        //            int column = -1;
        //            if (!int.TryParse(ColumnTextBox.Text.Trim(), out column)) { column = -1; };

        //            int row = -1;
        //            if (!int.TryParse(RowTextBox.Text.Trim(), out row)) { row = -1; };


        //            int cabinetInput;
        //            if (!int.TryParse(CabinetInputTextBox.Text.Trim(), out cabinetInput)) { cabinetInput = -1; };


        //            int postContact;
        //            if (!int.TryParse(PostContactTextBox.Text.Trim(), out postContact)) { postContact = -1; };

        //            int buchtNo;
        //            if (!int.TryParse(BuchtNoTextBox.Text.Trim(), out buchtNo)) { buchtNo = -1; };
        //            long fromTelephone = -1;
        //            if (!long.TryParse(FromTelephoneTextBox.Text.Trim(), out fromTelephone)) { fromTelephone = -1; };

        //            long toTelephone = -1;
        //            if (!long.TryParse(ToTelephoneTextBox.Text.Trim(), out toTelephone)) { toTelephone = -1; };


        //            int totalRecords = default(int);

        //            assingnmentInfo = DB.GetTotalInformationBucht(
        //                                                            CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs,
        //                                                            MDFComboBox.SelectedIDs, column,
        //                                                            row, CabinetComboBox.SelectedIDs,
        //                                                            cabinetInput, PostComboBox.SelectedIDs,
        //                                                            postContact, CustomerNameTextBox.Text.Trim(),
        //                                                            fromTelephone, toTelephone,
        //                                                            startRowIndex, pageSize,
        //                                                            buchtNo, BuchtTypeComboBox.SelectedIDs,
        //                                                            OtherBuchtTypeComboBox.SelectedIDs, TelephoneStatusComboBox.SelectedIDs,
        //                                                            PreCodeTypeComboBox.SelectedIDs, UsageTypeComboBox.SelectedIDs,
        //                                                            ISADSLCheckBox.IsChecked, AORBTypeComboBox.SelectedIDs,
        //                                                            SwitchPrecodeComboBox.SelectedIDs, ISReqeustCheckBox.IsChecked,
        //                                                            OnlyHeadNumberCheckBox.IsChecked, out totalRecords,
        //                                                            InstallationDateFromDate.SelectedDate, InstallationDateToDate.SelectedDate,
        //                                                            CutDateDatePicker.SelectedDate, CauseOfCutComboBox.SelectedIDs,
        //                                                            TelephoneClassComboBox.SelectedIDs, BuchtStatusComboBox.SelectedIDs,
        //                                                            PCMTypeComboBox.SelectedIDs, false
        //                                                        );

        //            Pager.TotalRecords = totalRecords;
        //            this.Cursor = Cursors.Arrow;
        //        });
        //}

        #endregion

        #region EventHandlers

        private void Pager_PageChanged(int pageNumber)
        {
            Search(null, null);
        }

        //milad doran
        //private void Search(object sender, RoutedEventArgs e)
        //{

        //    if (!worker.IsBusy)
        //        worker.RunWorkerAsync();
        //}

        //TODO:rad 13950711
        private void Search(object sender, RoutedEventArgs e)
        {
            int startRowIndex = Pager.StartRowIndex;
            int pageSize = Pager.PageSize;
            int count = default(int);

            int column = -1;
            if (!int.TryParse(ColumnTextBox.Text.Trim(), out column)) { column = -1; };

            int row = -1;
            if (!int.TryParse(RowTextBox.Text.Trim(), out row)) { row = -1; };

            int cabinetInput;
            if (!int.TryParse(CabinetInputTextBox.Text.Trim(), out cabinetInput)) { cabinetInput = -1; };

            int postContact;
            if (!int.TryParse(PostContactTextBox.Text.Trim(), out postContact)) { postContact = -1; };

            int buchtNo;
            if (!int.TryParse(BuchtNoTextBox.Text.Trim(), out buchtNo)) { buchtNo = -1; };

            long fromTelephone = -1;
            if (!long.TryParse(FromTelephoneTextBox.Text.Trim(), out fromTelephone)) { fromTelephone = -1; };

            long toTelephone = -1;
            if (!long.TryParse(ToTelephoneTextBox.Text.Trim(), out toTelephone)) { toTelephone = -1; };

            List<int> citiesId = CityComboBox.SelectedIDs;
            List<int> centersId = CenterComboBox.SelectedIDs;
            List<int> mdfsId = MDFComboBox.SelectedIDs;
            List<int> cabinetsId = CabinetComboBox.SelectedIDs;
            List<int> postsId = PostComboBox.SelectedIDs;
            List<int> buchtTypesId = BuchtTypeComboBox.SelectedIDs;
            List<int> otherBuchtTypesId = OtherBuchtTypeComboBox.SelectedIDs;
            List<int> telephoneStatusesId = TelephoneStatusComboBox.SelectedIDs;
            List<int> precodeTypesId = PreCodeTypeComboBox.SelectedIDs;
            List<int> usageTypesId = UsageTypeComboBox.SelectedIDs;
            List<int> aOrbTypesId = AORBTypeComboBox.SelectedIDs;
            List<int> switchPrecodesId = SwitchPrecodeComboBox.SelectedIDs;
            List<int> causeOfCutsId = CauseOfCutComboBox.SelectedIDs;
            List<int> telephoneClassesId = TelephoneClassComboBox.SelectedIDs;
            List<int> buchtStatusesId = BuchtStatusComboBox.SelectedIDs;
            List<int> pcmTypesId = PCMTypeComboBox.SelectedIDs;

            string customrtName = CustomerNameTextBox.Text.Trim();
            bool? isAdsl = ISADSLCheckBox.IsChecked;
            bool? isRequest = ISReqeustCheckBox.IsChecked;
            bool? onlyHeadNumber = OnlyHeadNumberCheckBox.IsChecked;

            DateTime? installationFromDate = InstallationDateFromDate.SelectedDate;
            DateTime? installationToDate = InstallationDateToDate.SelectedDate;
            DateTime? cutDate = CutDateDatePicker.SelectedDate;

            //مقداردهی عملیات اصلی- زمانبر
            Action mainAction = new Action(() =>
                                                    {
                                                        assingnmentInfo = DB.GetTotalInformationBucht(
                                                                                                       citiesId, centersId,
                                                                                                       mdfsId, column,
                                                                                                       row, cabinetsId,
                                                                                                       cabinetInput, postsId,
                                                                                                       postContact, customrtName,
                                                                                                       fromTelephone, toTelephone,
                                                                                                       startRowIndex, pageSize,
                                                                                                       buchtNo, buchtTypesId,
                                                                                                       otherBuchtTypesId, telephoneStatusesId,
                                                                                                       precodeTypesId, usageTypesId,
                                                                                                       isAdsl, aOrbTypesId,
                                                                                                       switchPrecodesId, isRequest,
                                                                                                       onlyHeadNumber, out count,
                                                                                                       installationFromDate, installationToDate,
                                                                                                       cutDate, causeOfCutsId,
                                                                                                       telephoneClassesId, buchtStatusesId,
                                                                                                       pcmTypesId,false
                                                                                                      );
                                                        Dispatcher.BeginInvoke(new System.Windows.Forms.MethodInvoker(() =>
                                                                                                                        {
                                                                                                                            ItemsDataGrid.ItemsSource = assingnmentInfo;
                                                                                                                            Pager.TotalRecords = count;
                                                                                                                        }
                                                                                                                      )
                                                                               );
                                                    }
                                            );

            //مقداردهی عملیات اطلاع رسانی از وضعیت اجرای عملیات اصلی
            Action duringOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = true;
                MainExtendedStatusBar.MessageLabel.FontSize = 13;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Bold;
                MainExtendedStatusBar.MessageLabel.Text = "درحال بارگذاری...";
                Pager.IsEnabled = false;
                SearchExpander.IsEnabled = false;
                ItemsDataGrid.IsEnabled = false;
                this.Cursor = Cursors.Wait;
            });

            //مقداردهی عملیاتی که باید بعد از اتمام عملیات اصلی اجرا شود 
            Action afterOperationAction = new Action(() =>
            {
                MainExtendedStatusBar.ShowProgressBar = false;
                MainExtendedStatusBar.MessageLabel.FontSize = 8;
                MainExtendedStatusBar.MessageLabel.FontWeight = FontWeights.Normal;
                MainExtendedStatusBar.MessageLabel.Text = string.Empty;
                Pager.IsEnabled = true;
                SearchExpander.IsEnabled = true;
                ItemsDataGrid.IsEnabled = true;
                this.Cursor = Cursors.Arrow;
            });

            CRM.Application.Local.TimeConsumingOperation timeConsumingOperation = new Local.TimeConsumingOperation
            {
                MainOperationAction = mainAction,
                DuringOperationAction = duringOperationAction,
                AfterOperationAction = afterOperationAction
            };

            //اجرای عملیات
            this.RunTimeConsumingOperation(timeConsumingOperation);
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            System.Windows.UIElement container = this.SearchExpander as System.Windows.UIElement;
            Helper.ResetSearch(container);

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    AssignmentInfo item = ItemsDataGrid.SelectedItem as AssignmentInfo;

                    Bucht Bucht = Data.BuchtDB.GetBuchetByID(item.BuchtID);

                    if (Bucht.Status != (int)DB.BuchtStatus.Free) { MessageBox.Show("فقط بوخت آزاد را می توانید حذف کنید"); return; }

                    DB.Delete<Data.Bucht>(item.BuchtID);
                    ShowSuccessMessage("بوخت مورد نظر حذف شد");
                    if (!worker.IsBusy)
                        worker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در بوخت", ex);
            }
        }

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            MDFComboBox.ItemsSource = Data.MDFDB.GetMDFCheckableByCenterIDs(CenterComboBox.SelectedIDs);
            SwitchPrecodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDs(CenterComboBox.SelectedIDs);
            CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(CenterComboBox.SelectedIDs);
        }

        private void TabWindow_Loaded_1(object sender, RoutedEventArgs e)
        {
            //  if (!worker.IsBusy)
            //  worker.RunWorkerAsync();
        }

        private void TeleInfo(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignmentInfo teleAssignmentInfo = ItemsDataGrid.SelectedItem as AssignmentInfo;
                if (teleAssignmentInfo.TelePhoneNo != null && teleAssignmentInfo.TelePhoneNo != 0)
                {
                    GeneralInformationForm window = new GeneralInformationForm(teleAssignmentInfo.TelePhoneNo);
                    window.ShowDialog();
                }
                else
                {
                    Folder.MessageBox.ShowInfo("تلفن یافت نشد");
                }
            }
        }

        private void PreCodeTypeComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SwitchPrecodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableByCenterIDsAndPreCodeType(CenterComboBox.SelectedIDs, PreCodeTypeComboBox.SelectedIDs);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                AssignmentInfo teleAssignmentInfo = ItemsDataGrid.SelectedItem as AssignmentInfo;
                if (teleAssignmentInfo.BuchtID != null)
                {
                    BuchtInfoForm window = new BuchtInfoForm((long)teleAssignmentInfo.BuchtID);
                    window.ShowDialog();
                    if (window.DialogResult == true)
                    {
                        Search(null, null);
                    }
                }
            }
        }

        private void CabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);
        }

        #endregion

        #region Print EventHandlers

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره اطلاعات");
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            int startRowIndex = 0;
            int pageSize = Pager.TotalRecords;

            int column = -1;
            if (!int.TryParse(ColumnTextBox.Text.Trim(), out column)) { column = -1; };

            int row = -1;
            if (!int.TryParse(RowTextBox.Text.Trim(), out row)) { row = -1; };


            int cabinetInput;
            if (!int.TryParse(CabinetInputTextBox.Text.Trim(), out cabinetInput)) { cabinetInput = -1; };


            int postContact;
            if (!int.TryParse(PostContactTextBox.Text.Trim(), out postContact)) { postContact = -1; };

            int buchtNo;
            if (!int.TryParse(BuchtNoTextBox.Text.Trim(), out buchtNo)) { buchtNo = -1; };

            long fromTelephone = -1;
            if (!long.TryParse(FromTelephoneTextBox.Text.Trim(), out fromTelephone)) { fromTelephone = -1; };

            long toTelephone = -1;
            if (!long.TryParse(ToTelephoneTextBox.Text.Trim(), out toTelephone)) { toTelephone = -1; };

            int totalRecords = default(int);

            System.Data.DataSet data = DB.GetTotalInformationBucht(
                                                                    CityComboBox.SelectedIDs, CenterComboBox.SelectedIDs,
                                                                    MDFComboBox.SelectedIDs, column,
                                                                    row, CabinetComboBox.SelectedIDs,
                                                                    cabinetInput, PostComboBox.SelectedIDs,
                                                                    postContact, CustomerNameTextBox.Text.Trim(),
                                                                    fromTelephone, toTelephone,
                                                                    startRowIndex, pageSize,
                                                                    buchtNo, BuchtTypeComboBox.SelectedIDs,
                                                                    OtherBuchtTypeComboBox.SelectedIDs, TelephoneStatusComboBox.SelectedIDs,
                                                                    PreCodeTypeComboBox.SelectedIDs, UsageTypeComboBox.SelectedIDs,
                                                                    ISADSLCheckBox.IsChecked, AORBTypeComboBox.SelectedIDs,
                                                                    SwitchPrecodeComboBox.SelectedIDs, ISReqeustCheckBox.IsChecked,
                                                                    OnlyHeadNumberCheckBox.IsChecked, out totalRecords,
                                                                    InstallationDateFromDate.SelectedDate, InstallationDateToDate.SelectedDate,
                                                                    CutDateDatePicker.SelectedDate, CauseOfCutComboBox.SelectedIDs,
                                                                    TelephoneClassComboBox.SelectedIDs, BuchtStatusComboBox.SelectedIDs,
                                                                    PCMTypeComboBox.SelectedIDs, true
                                                                  )
                                         .ToDataSet("Result", ItemsDataGrid);

            this.Cursor = Cursors.Arrow;

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;
        }

        #endregion

    }
}
