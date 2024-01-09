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
using System.Collections.ObjectModel;

namespace CRM.Application.UserControls
{
    public partial class InvestigateInfoSummary: UserControl
    {
        public InvestigatePossibility investigate { get; set; }
        public InvestigateInfo investigateInfo { get; set; }
  
        public static Request _request  { get; set; }

        public InvestigateInfoSummary()
        {
            InitializeComponent();
        }

        public InvestigateInfoSummary(long reqeustID): this()
        {
             _request = Data.RequestDB.GetRequestByID(reqeustID);
             investigate = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_request.ID).Take(1).SingleOrDefault();
        }

        private void Initialize()
        {
            
        }     
        
        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (_request != null)
            {
                switch (_request.RequestTypeID)
                {
                    case (int)DB.RequestType.Dayri:
                      
                        investigateInfo = Data.InvestigatePossibilityDB.GetInvestigateInfoByRequestIDDayri(_request.ID);
                        break;
                }
            }

            this.DataContext = investigateInfo;
        }      
    
    }
}
