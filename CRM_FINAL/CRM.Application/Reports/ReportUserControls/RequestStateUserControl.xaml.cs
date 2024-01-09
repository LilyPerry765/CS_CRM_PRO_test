using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
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

namespace CRM.Application.Reports.ReportUserControls
{
    
    public partial class RequestStateUserControl : ReportBase
    {
        #region Constructor

        public RequestStateUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public override void Search()
        {
           
            try
            {
                DateTime? fromDate = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate : null;
                DateTime? toDate = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate : null;

                //آیا یک مقدار عددی برای شناسه درخواست مشخص شده است یا خیر


                IEnumerable result
                    = ReportDB.GetRequestStateInfo(fromDate, toDate, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
                               
                if (result != null)
                {
                    StiVariable vari = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    ReportBase.SendToPrint(result, (int)DB.UserControlNames.RequestStateReport, vari);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex,"خطا در لیست گزارش ها - گزارش درخواست کافو نوری به معمولی");
            }
        }

        #endregion
    }
}
