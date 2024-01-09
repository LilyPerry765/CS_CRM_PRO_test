using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PostInfoReportUserControl.xaml
    /// </summary>
    public partial class PostInfoReportUserControl :Local.ReportBase
    {
       // List<int> centerInfoList = new List<int>();
        PostTotalyInfo _postTotalyInfo;
        public PostInfoReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CityComboBox.SelectedIndex = 0;
            _postTotalyInfo = new PostTotalyInfo();
            PostTypeComboBox.ItemsSource = Data.PostTypeDB.GetPostTypeCheckable();
            StatusPostComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PostStatus));
            AorBComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.AORBPostAndCabinet));

        }
        public override void Search()
        {

            List<PostInfo> result = LoadData();
         //   result = result.OrderBy(t => (int)t.PostContact).ToList();
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();
            string title = string.Empty;
            string path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            title = "گزارش اطلاعات فنی پست";


            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();



            stiReport.Dictionary.Variables["Header"].Value = title;
            switch (UserControlID)
            {
                case (int)DB.UserControlNames.PostInfoCable:
                case (int)DB.UserControlNames.PostInfoDetails:
                    stiReport.RegData("result", "result", result);
                    //stiReport.RegData("PostTotaly", "PostTotaly", _postTotalyInfo);
                    break;
                case (int)DB.UserControlNames.PostInfoTotal:
                    stiReport.RegData("result", "result", result);
                    break;
            }

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        public List<PostInfo> LoadData()
        {
            List<PostInfo> result = new List<PostInfo>();
            switch (UserControlID)
            {
                case (int)DB.UserControlNames.PostInfoCable:
                    result = ReportDB.GetPostInfoCable(new List<int>{(int)CenterComboBox.SelectedValue},
                                                             CabinetComboBox.SelectedIDs,
                                                             PostNumberCheckableCombobox.SelectedIDs,
                                                             PostTypeComboBox.SelectedIDs,
                                                             AorBComboBox.SelectedIDs,
                                                             string.IsNullOrEmpty(DistanceTextBox.Text.Trim()) ? 0 : int.Parse(DistanceTextBox.Text.Trim()),
                                                             (IsBorderMeterCheckBox.IsChecked == true) ? 1 : 0,
                                                             string.IsNullOrEmpty(PostalCodeTextBox.Text.Trim()) ? (string)null : PostalCodeTextBox.Text.Trim(),
                                                             string.IsNullOrEmpty(OutBorderMeterTextBox.Text.Trim()) ? 0 : int.Parse(OutBorderMeterTextBox.Text.Trim()),
                                                             string.IsNullOrEmpty(AddressTextBox.Text.Trim()) ? (string)null : AddressTextBox.Text.Trim(),
                                                             StatusPostComboBox.SelectedIDs,
                                                             PostGroupCheckableCombobox.SelectedIDs);
                    break;

                case (int)DB.UserControlNames.PostInfoDetails:
                    result = ReportDB.GetPostInfo(new List<int> { (int)CenterComboBox.SelectedValue },
                                                             CabinetComboBox.SelectedIDs,
                                                             PostNumberCheckableCombobox.SelectedIDs,
                                                             PostTypeComboBox.SelectedIDs,
                                                             AorBComboBox.SelectedIDs,
                                                             string.IsNullOrEmpty(DistanceTextBox.Text.Trim()) ? 0 : int.Parse(DistanceTextBox.Text.Trim()),
                                                             (IsBorderMeterCheckBox.IsChecked == true) ? 1 : 0,
                                                             string.IsNullOrEmpty(PostalCodeTextBox.Text.Trim()) ? (string)null : PostalCodeTextBox.Text.Trim(),
                                                             string.IsNullOrEmpty(OutBorderMeterTextBox.Text.Trim()) ? 0 : int.Parse(OutBorderMeterTextBox.Text.Trim()),
                                                             string.IsNullOrEmpty(AddressTextBox.Text.Trim()) ? (string)null : AddressTextBox.Text.Trim(),
                                                             StatusPostComboBox.SelectedIDs,
                                                             PostGroupCheckableCombobox.SelectedIDs);
                    using (MainDataContext context = new MainDataContext())
                    {

                        List<string> CabinetIDs = result.Select(t => t.CabinetID).ToList();
                        List<long?> CabinetInputIDs = result.Select(t => t.CabinetInputID).ToList();
                        List<long?> postContacts = result.Select(t => t.PostContactID).ToList();
                       // _postTotalyInfo.AllOfTheCabinetInput = context.CabinetInputs.Where(t => (CabinetIDs.Count == 0 || CabinetIDs.Contains(t.CabinetID.ToString()))).Count();
                        _postTotalyInfo.AllOfTheCabinetInput = CabinetInputIDs.Count();
                        _postTotalyInfo.FreeCabinetInput = context.Buchts.Where(t => ((CabinetInputIDs.Count == 0 || CabinetInputIDs.Contains(t.CabinetInputID)) &&
                                                                                      t.Status == (int)DB.BuchtStatus.Free)).Count();

                        _postTotalyInfo.ReserveCabinetInput = context.Buchts.Where(t => ((CabinetInputIDs.Count == 0 || CabinetInputIDs.Contains(t.CabinetInputID)) &&
                                                                                      t.Status == (int)DB.BuchtStatus.Reserve)).Count();

                        _postTotalyInfo.PCMCount = context.Buchts.Where(t => ((CabinetInputIDs.Count == 0 || CabinetInputIDs.Contains(t.CabinetInputID)) &&
                                                                                      t.BuchtTypeID == (int)DB.BuchtType.OutLine)).Count();

                        _postTotalyInfo.PCMKarkon = context.Buchts.Where(t => ((CabinetInputIDs.Count == 0 || CabinetInputIDs.Contains(t.CabinetInputID)) &&
                                                                                     t.BuchtTypeID == (int)DB.BuchtType.InLine && t.Status == (int)DB.BuchtStatus.Connection)).Count();

                        _postTotalyInfo.EmptyPortPCM = context.Buchts.Where(t => ((CabinetInputIDs.Count == 0 || CabinetInputIDs.Contains(t.CabinetInputID)) &&
                                                                                     t.BuchtTypeID == (int)DB.BuchtType.InLine && t.Status == (int)DB.BuchtStatus.Free)).Count();

                        _postTotalyInfo.WaitingListCount = context.InvestigatePossibilityWaitinglists.Where(t => (CabinetIDs.Count == 0 || CabinetIDs.Contains(t.CabinetID.ToString())) &&
                                                                                                                t.WaitingList.Status == false).Count();

                        _postTotalyInfo.FillCabinetInput = context.PostContacts.Where(t => (postContacts.Count == 0 || postContacts.Contains(t.ID)) &&
                                                                                            t.Status == (int)DB.PostContactStatus.CableConnection).Count();


                        _postTotalyInfo.EmptyCabinetInput = context.PostContacts.Where(t => (postContacts.Count == 0 || postContacts.Contains(t.ID)) &&
                                                                        t.Status == (int)DB.PostContactStatus.Free).Count();


                        _postTotalyInfo.FailCabinetInput = context.PostContacts.Where(t => (postContacts.Count == 0 || postContacts.Contains(t.ID)) &&
                                                                        t.Status == (int)DB.PostContactStatus.PermanentBroken).Count();

                    }
                    break;
                case (int)DB.UserControlNames.PostInfoTotal:
                    {
                        result = ReportDB.GetPostInfoTotal(
                                         new List<int> { (int)CityComboBox.SelectedValue },
                                         new List<int> { (int)CenterComboBox.SelectedValue },
                                         CabinetComboBox.SelectedIDs,
                                         PostNumberCheckableCombobox.SelectedIDs ,
                                         PostTypeComboBox.SelectedIDs,
                                                             AorBComboBox.SelectedIDs,
                                                             string.IsNullOrEmpty(DistanceTextBox.Text.Trim()) ? 0 : int.Parse(DistanceTextBox.Text.Trim()),
                                                             (IsBorderMeterCheckBox.IsChecked == true) ? 1 : 0,
                                                             string.IsNullOrEmpty(PostalCodeTextBox.Text.Trim()) ? (string)null : PostalCodeTextBox.Text.Trim(),
                                                             string.IsNullOrEmpty(OutBorderMeterTextBox.Text.Trim()) ? 0 : int.Parse(OutBorderMeterTextBox.Text.Trim()),
                                                             string.IsNullOrEmpty(AddressTextBox.Text.Trim()) ? (string)null : AddressTextBox.Text.Trim(),
                                                             StatusPostComboBox.SelectedIDs,
                                                             PostGroupCheckableCombobox.SelectedIDs);
                        break;
                    }
            }


                
                List < EnumItem > ABType = Helper.GetEnumItem(typeof(DB.AORBPostAndCabinet));
                //List<EnumItem> Status = Helper.GetEnumItem(typeof(DB.Status));
                //foreach (PostInfo item in result)
                //{
                //    item.Status = Status.Find(i => i.ID == int.Parse(item.Status)).Name;
                //}

               
                return result;
            
        }
        
       
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
        private void CabinetComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            PostNumberCheckableCombobox.ItemsSource = PostDB.GetPostCheckableByCabinetID(CabinetComboBox.SelectedIDs);

            
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);
            CenterComboBox.SelectedIndex = 0;
        }

        private void CenterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                CabinetComboBox.ItemsSource = Data.CabinetDB.GetCabinetCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
                PostGroupCheckableCombobox.ItemsSource = PostGroupDB.GetPostGroupCheckableByCenterIDs(new List<int> { (int)CenterComboBox.SelectedValue });
            
        }

       

    }
}
