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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.UserControls
{
    public partial class ReadCountor : Local.UserControlBase
    {
        Counter _Counter;
        HotBillingService.HotBillingService hotBillingService = new HotBillingService.HotBillingService();

        static long _CounterID = 0;

        public long TelephoneNo
        {
            get { return _Counter.TelephoneNo; }
            set { _Counter.TelephoneNo = value; }
        }

        public long CountorID
        {
            get { return _CounterID; }
            set { _CounterID = value; }
        }

        public Counter Counter
        {
            get { return this.DataContext as Counter; }
            set { _Counter = value; }
        }

        public ReadCountor()
        {
            InitializeComponent();
            Initialize();
        }

        public ReadCountor(long? countorID)
            : this()
        {
            _Counter.ID = countorID ?? 0;
        }

        private void Initialize()
        {
            CycleComnboBox.ItemsSource = Data.CycleDB.GetCycleByCheckable();
            _Counter = new Counter();
            
           
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {

            if (_IsLoaded) return;
            else _IsLoaded = true;

            if (_CounterID != 0)
            {
                _Counter = Data.CounterDB.GetCounterByID(_CounterID);
                _CounterID = _Counter.ID;
            }
            else
            {
              DateTime dateTime =DB.GetServerDate();
                _Counter.CounterReadDate = dateTime;
                _Counter .CounterReadHour = dateTime.ToShortTimeString();
            }
        
            this.DataContext = _Counter;

              Cycle cycle = new Cycle();
            // for not Hamper error in get current cycle in load form, The Try was considered to be
            try
            {
                cycle = Data.CycleDB.GetDateCurrentCycle();
                (this.DataContext as Counter).CycleID = cycle.ID;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Sequence contains more than one element"))
                    Folder.MessageBox.ShowInfo("با تاریخ فعلی چند دوره یافت شد. لطفا اطلاعات دوره ها را اصلاح کنید.");
            }

            if (TelephoneNo != 0)
            {
                var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
                switch (city)
                {
                    case "semnan":
                        {
                            // for not Hamper error in get current counter in load form, The Try was considered to be
                            try
                            {
                                int localCounter = 0;
                                int nonLocalCounter = 0;

                                string ErrorMessge = string.Empty;
                                hotBillingService.GetCounter(TelephoneNo.ToString(), DB.GetServerDate(), out localCounter, out nonLocalCounter, out ErrorMessge);
                                if (ErrorMessge != "" || ErrorMessge != string.Empty)
                                {
                                    Folder.MessageBox.ShowInfo("خطای مربوط به دریافت اطلاعات کنتور : " + ErrorMessge);
                                }
                                (this.DataContext as Counter).CounterNo = (localCounter + nonLocalCounter).ToString();



                            }
                            catch
                            {
                                Folder.MessageBox.ShowInfo("در یافت اطلاعات تلفن از HotBilling با خطا مواجه شد.");
                            }

                            if (cycle != null && cycle.ID != 0)
                            {
                                try
                                {
                                    int local = 0;
                                    int nonLocal = 0;
                                    int international = 0;
                                    int BistTalk = 0;
                                    int IA = 0;

                                    DateTime endDate = (DateTime)cycle.ToDate;
                                    string ErrorMessge = string.Empty;
                                    hotBillingService.GetKarkardInTimeRange(TelephoneNo.ToString(), (DateTime)cycle.FromDate, ref endDate, out local, out nonLocal, out international, out BistTalk, out IA, out ErrorMessge);
                                    if (ErrorMessge != "" || ErrorMessge != string.Empty)
                                    {
                                        Folder.MessageBox.ShowInfo("خطای مربوط به دریافت اطلاعات کارکرد : " + ErrorMessge);
                                    }
                                    (this.DataContext as Counter).Local = local;
                                    (this.DataContext as Counter).NonLocal = nonLocal;
                                    (this.DataContext as Counter).International = international;
                                    (this.DataContext as Counter).BistTalk = BistTalk;
                                    (this.DataContext as Counter).IA = IA;
                                    if (endDate != DateTime.MinValue)
                                        (this.DataContext as Counter).OperatingDate = endDate;



                                }
                                catch
                                {
                                    Folder.MessageBox.ShowInfo("در یافت اطلاعات تلفن از HotBilling با خطا مواجه شد.");
                                }
                            }
                            else
                            {
                                Folder.MessageBox.ShowInfo("بعلت عدم دریافت اطلاعات دوره امکان دریافت اطلاعات کارکرد تلفن نمی باشد،لطفا اطلاعات دوره ها را اصلاح کنید.");
                            }
                        }
                        break;
                }
            }



          

        }
    }
}
