using CRM.Data;
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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for VacateE1UserControl.xaml
    /// </summary>
    public partial class VacateE1UserControl : Local.UserControlBase
    {
        private long _requestID = 0;
        private long _telephoneNo = 0;

        public List<E1Link> oldVacateE1 { get; set; }

        public VacateE1UserControl()
        {
            InitializeComponent();
            Initialize();
        }

        public VacateE1UserControl(long requestID, long telephoneNo)
            :this()
        {
            this._requestID = requestID;
            this._telephoneNo = telephoneNo;
        }
        private void Initialize()
        {

        }
        private void LoadData()
        {
            List<VacateE1Info> vacateE1Infos = Data.VacateE1DB.E1Info(_telephoneNo);
            if (_requestID != 0)
            {
                oldVacateE1 = Data.E1LinkDB.GetE1LinkByRequestID(_requestID);
                oldVacateE1.ForEach(t =>
                {
                    if (vacateE1Infos.Any(t2 => t2.BuchtID == t.BuchtID))
                    {
                        vacateE1Infos.Where(t2 => t2.BuchtID == t.BuchtID).SingleOrDefault().IsSelected = true;
                    }
                });

            }
          
            PointsInfoDataGrid.ItemsSource = vacateE1Infos;
        }

        private void UserControlBase_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
