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

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for ADSLModemPropertyForm.xaml
    /// </summary>
    public partial class ADSLModemPropertyForm : Window
    {
        public ADSLModemPropertyForm()
        {
            InitializeComponent();

            Service1 aDSLService = new Service1();
            Center center = new Center();
            //ADSLModemProperty modemProperty = new ADSLModemProperty();
            List<ADSLModemProperty>aDSLModemPropertyList = GetTelephoneNoList();

            int count = 0;

            foreach (ADSLModemProperty item in aDSLModemPropertyList)
            {
                if (item.ID >= 2557)
                {
                    string telephoneNo = item.TelephoneNo.ToString();

                    System.Data.DataTable telephoneInfo = aDSLService.GetInformationForPhone("Admin", "alibaba123", telephoneNo);
                    if (telephoneInfo.Rows.Count != 0)
                    {
                        string centerCode = telephoneInfo.Rows[0]["CENTERCODE"].ToString();
                        center = CenterDB.GetCenterByCenterCode(Convert.ToInt32(centerCode));

                        item.CenterID = center.ID;

                        item.Detach();
                        DB.Save(item);

                        count = count + 1;
                        CounterLabel.Content = count.ToString();
                    }
                }
            }
        }

        private List<ADSLModemProperty> GetTelephoneNoList()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModemProperties.ToList();
            }
        }
    }
}
