using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using CRM.Application.Codes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for WiringNetworkGroupedForm.xaml
    /// </summary>
    public partial class WiringNetworkGroupedForm : Local.RequestFormBase
    {
        private long? _subID;
        List<long> _RequestIDs = new List<long>();
        List<Request> _Requests = new List<Request>();
        NetworkWiring _NetworkWiring = new NetworkWiring();
        ObservableCollection<WiringGroupedInfo> _InvestigatePossibilityInfos = new ObservableCollection<WiringGroupedInfo>();
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();

        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        public WiringNetworkGroupedForm()
        {
            InitializeComponent();
            Initialize();
        }
        public WiringNetworkGroupedForm(List<long> ids, long? subID)
            : this()
        {
            this._subID = subID;
            _RequestIDs = ids;
        }
        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print };
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            if (_RequestIDs.Count() <= 0) return;

            DateTime currentDateTime = DB.GetServerDate();

            _Requests = RequestDB.GetRequestListByID(_RequestIDs);
            _InvestigatePossibilityInfos = new ObservableCollection<WiringGroupedInfo>(InvestigatePossibilityDB.GetInvestigatePossibilityInfoByRequestIDs(_RequestIDs));
            StatusComboBox.SelectedValue = _Requests.Take(1).SingleOrDefault().StatusID;
            if (_RequestIDs.Count() == 1)
            {
                _NetworkWiring = NetworkWiringDB.GetNetworkWiringByRequestID((long)_RequestIDs.Take(1).SingleOrDefault());

            }
            if (_NetworkWiring == null || _NetworkWiring.ID == 0)
            {
                _NetworkWiring = new NetworkWiring();
                _NetworkWiring.NetworkWiringDate = currentDateTime;
                _NetworkWiring.NetworkWiringHour = currentDateTime.ToString("hh:mm:ss");
            }
            StatusComboBox.ItemsSource = DB.GetStepStatus(_Requests.Take(1).SingleOrDefault().RequestTypeID, this.currentStep);

            switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    break;
            }

            TelItemsDataGrid.ItemsSource = _InvestigatePossibilityInfos;

            NetworkWiringInfoGroupBox.DataContext = _NetworkWiring;
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
                    switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                    {
                        case (byte)DB.RequestType.Dayri:
                        case (byte)DB.RequestType.Reinstall:
                            {
                                SaveDayeri();
                            }
                            break;


                    }

                    ts2.Complete();
                }
                ShowSuccessMessage("ذخیره انجام شد");

                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }

            base.Confirm();

            return IsSaveSuccess;
        }


        public override bool Forward()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
                {
                    RequestIDs = _RequestIDs;
                    Save();
                    if (IsSaveSuccess)
                    {
                        Status Status = Data.StatusDB.GetStatueByStatusID((int)StatusComboBox.SelectedValue);
                        if (Status.StatusType == (byte)DB.RequestStatusType.Recursive)
                        {
                            _Requests.ForEach(t =>
                            {
                                SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(t.ID);
                                if (specialCondition == null)
                                {
                                    specialCondition = new SpecialCondition();
                                    specialCondition.RequestID = t.ID;
                                    specialCondition.ReturnedFromWiring = true;
                                    specialCondition.Detach();
                                    DB.Save(specialCondition, true);
                                }
                                else
                                {
                                    specialCondition.RequestID = t.ID;
                                    specialCondition.ReturnedFromWiring = true;
                                    specialCondition.Detach();
                                    DB.Save(specialCondition, false);
                                }

                            });

                            IsForwardSuccess = true;
                        }
                        else if (Status.StatusType == (byte)DB.RequestStatusType.Completed)
                        {


                            switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                            {
                                case (byte)DB.RequestType.Dayri:
                                case (byte)DB.RequestType.Reinstall:
                                    {
                                        ForwardDayeri();
                                    }
                                    break;


                            }

                            _Requests.ForEach(t =>
                           {

                               SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(t.ID);
                               if (specialCondition == null)
                               {
                                   specialCondition = new SpecialCondition();
                                   specialCondition.RequestID = t.ID;
                                   specialCondition.ReturnedFromWiring = false;
                                   specialCondition.Detach();
                                   DB.Save(specialCondition, true);
                               }
                               else
                               {
                                   specialCondition.RequestID = t.ID;
                                   specialCondition.ReturnedFromWiring = false;
                                   specialCondition.Detach();
                                   DB.Save(specialCondition, false);
                               }
                           });

                            IsForwardSuccess = true;
                        }
                        else
                        {
                            _Requests.ForEach(t =>
        {
            SpecialCondition specialCondition = Data.SpecialConditionDB.GetSpecialConditionByRequestID(t.ID);
            if (specialCondition == null)
            {
                specialCondition = new SpecialCondition();
                specialCondition.RequestID = t.ID;
                specialCondition.ReturnedFromWiring = false;
                specialCondition.Detach();
                DB.Save(specialCondition, true);
            }
            else
            {
                specialCondition.RequestID = t.ID;
                specialCondition.ReturnedFromWiring = false;
                specialCondition.Detach();
                DB.Save(specialCondition, false);
            }
        });

                            IsForwardSuccess = true;
                        }

                    }
                    else
                    {
                        IsForwardSuccess = false;
                    }

                    ts.Complete();
                }


            }
            catch(Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
            }
            return IsForwardSuccess;
        }


        public override bool Deny()
        {

            try
            {
                base.RequestIDs = _RequestIDs;
                switch (_Requests.Take(1).SingleOrDefault().RequestTypeID)
                {
                    case (byte)DB.RequestType.Dayri:
                    case (byte)DB.RequestType.Reinstall:
                        DayeriDeny();
                        break;
                    case (byte)DB.RequestType.Dischargin:

                        break;
                    case (byte)DB.RequestType.ChangeNo:

                        break;
                    case (byte)DB.RequestType.RefundDeposit:

                        break;
                    case (byte)DB.RequestType.ChangeLocationCenterInside:
                    case (byte)DB.RequestType.ChangeLocationCenterToCenter:

                        break;
                    case (byte)DB.RequestType.SpecialWire:

                        break;
                    case (byte)DB.RequestType.E1:
                        break;
                    case (byte)DB.RequestType.VacateSpecialWire:

                        break;
                    case (byte)DB.RequestType.ChangeLocationSpecialWire:
                        break;

                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد درخواست", ex);
            }

            return IsRejectSuccess;
        }

        private void DayeriDeny()
        {
        }

        private void ForwardDayeri()
        {
            DateTime currentDateTime = DB.GetServerDate();

           if( _InvestigatePossibilityInfos.Any(t=>!t.PostContact.HasValue))
           {
               throw new Exception("اتصالی پست پیدا نشد");
           }

            _Requests.ForEach(t =>
            {
                long? telephone = null;
                if (Data.RequestDocumnetDB.CheckTelephoneBeRound(t, out telephone))
                {
                    TelRoundInfo roundTel = RoundListDB.GetRoundTelInfoByRequestID(t.ID);
                    Telephone tele = Data.TelephoneDB.GetTelephoneByTelephoneNo(roundTel.TelephoneNo);
                    tele.InRoundSale = false;
                    tele.Detach();
                    DB.Save(tele);
                }
            });

            List<InstallRequest> installReqeusts = Data.InstallRequestDB.GetInstallRequestByRequestIDs(_InvestigatePossibilityInfos.Select(t => t.RequestID).ToList());

            installReqeusts.ForEach(t =>
            {
                t.InstallationDate = currentDateTime;
                t.Detach();
            });
            DB.UpdateAll(installReqeusts);

            List<Telephone> telephones = Data.TelephoneDB.GetTelephones(_InvestigatePossibilityInfos.Select(t => (long)t.TelephonNo).ToList());


            _InvestigatePossibilityInfos.ToList().ForEach(t =>
            {
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).InstallationDate = currentDateTime;
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).DischargeDate = null;
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).CauseOfTakePossessionID = null;
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).CustomerTypeID = installReqeusts.Find(t3 => t3.RequestID == t.RequestID).TelephoneType;
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).CustomerGroupID = installReqeusts.Find(t3 => t3.RequestID == t.RequestID).TelephoneTypeGroup;
                telephones.Find(t2 => t2.TelephoneNo == t.TelephonNo).Detach();
            });
            DB.UpdateAll(telephones);



            List<PostContact> contacts = Data.PostContactDB.GetPostContactByIDs(_InvestigatePossibilityInfos.Select(t => (long)t.PostContactID).ToList());

            contacts.ForEach(t=>
            {
                t.Status = (byte)DB.PostContactStatus.CableConnection;
                t.Detach();
                DB.Save(t);

            if (t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal || t.ConnectionType == (byte)DB.PostContactConnectionType.PCMNormal)
            {
                Bucht bucht = BuchtDB.GetBuchtByConnectionID(t.ID);
                PCMPort PCMPort = Data.PCMPortDB.GetPCMPortByID((long)bucht.PCMPortID);
                PCMPort.Status = (byte)DB.PCMPortStatus.Connection;
                PCMPort.Detach();
                DB.Save(PCMPort);
            }

            });


            List<NetworkWiring> InserNetworkWirings = new List<NetworkWiring>();
            List<NetworkWiring> NetworkWiringItems = NetworkWiringDB.GetNetworkWiringByRequestIDs(_RequestIDs);
            NetworkWiringItems.ForEach(t =>
            {
                t.NetworkWiringDate = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkWiringDate;
                t.NetworkWiringHour = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkWiringHour;
                t.NetworkComment = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkComment;
                t.Detach();
            });
            DB.UpdateAll(NetworkWiringItems);


            _InvestigatePossibilityInfos.Where(t => !NetworkWiringItems.Select(t2 => t2.ID).Contains((long)t.RequestID)).ToList().ForEach(t =>
            {
                NetworkWiring NetworkWiring = new Data.NetworkWiring();
                NetworkWiring.ID = (long)t.RequestID;
                NetworkWiring.NetworkWiringDate = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkWiringDate;
                NetworkWiring.NetworkWiringHour = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkWiringHour;
                NetworkWiring.NetworkComment = (NetworkWiringInfoGroupBox.DataContext as NetworkWiring).NetworkComment;
                NetworkWiring.InsertDate = currentDateTime;
                NetworkWiring.Detach();
                InserNetworkWirings.Add(NetworkWiring);
            });

            DB.SaveAll(InserNetworkWirings);

        }





        private void SaveDayeri()
        {
            if (StatusComboBox.SelectedValue != null)
            {


                int status = (int)StatusComboBox.SelectedValue;
                _Requests.ForEach(t =>
                {
                    t.StatusID = status;
                    t.Detach();

                });


                DB.UpdateAll(_Requests);

            }
            else
            {
                throw new Exception("لطفا وضعیت را انتخاب کنید");
            }
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = TelItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = CRM.Application.Codes.Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

                DataSet data = _InvestigatePossibilityInfos.ToList().ToDataSet("Result", TelItemsDataGrid);
                CRM.Application.Codes.Print.DynamicPrintV2(data,_title, dataGridSelectedIndexs, _groupingColumn);

            this.Cursor = Cursors.Arrow;
        }
        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(TelItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }

        public override bool Print()
        {
            this.Cursor = Cursors.Wait;
            try
            {

                    DataSet data = _InvestigatePossibilityInfos.ToDataSet("Result", TelItemsDataGrid);
                    CRM.Application.Codes.Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn);
                    IsPrintSuccess = true;

            }
            catch (Exception ex)
            {
                IsPrintSuccess = false;
            }
            this.Cursor = Cursors.Arrow;
            return IsPrintSuccess;
        }


        #region Filters
        private bool PredicateFilters(object obj)
        {
            WiringGroupedInfo checkableObject = obj as WiringGroupedInfo;
            return checkableObject.TelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

        private void FilterTelephonNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(TelItemsDataGrid.ItemsSource);
            if (view != null)
            {
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        #endregion Filters

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            if (!CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, TelItemsDataGrid.Name, TelItemsDataGrid.Columns))
            {
                Folder.MessageBox.ShowError("خطا در ذخیره اطلاعات");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<DayeriWiringNetwork> result = new List<DayeriWiringNetwork>();
            result = ReportDB.GetDayeriWiringNetwork(_RequestIDs);
            CRM.Application.Local.ReportBase.SendToPrint(result, (int)DB.UserControlNames.DayeriWiringNetwork);
        }
    }
}
