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
    /// Interaction logic for TechnicalSpecificationsOfADSL.xaml
    /// </summary>
    public partial class TechnicalSpecificationsOfADSL : Local.UserControlBase
    {
        long _RequestID { get; set; }

        public TechnicalSpecificationsOfADSL()
        {
            InitializeComponent();
        }

        public TechnicalSpecificationsOfADSL(long requestID):this()
        {
            this._RequestID = requestID;
            Initialize();
        }

        private void Initialize()
        {
            
        }
  

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            TechnicalSpecificationsOfADSLGrid.DataContext = Data.TechnicalSpecificationsOfADSLDB.Search(_RequestID);
        }
    }
}
