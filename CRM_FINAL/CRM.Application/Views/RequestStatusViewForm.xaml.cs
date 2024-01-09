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
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for RequestStatusViewForm.xaml
    /// </summary>
    public partial class RequestStatusViewForm : Local.PopupWindow
    {
        private Data.RequestInfo requestInfo;

        public RequestStatusViewForm()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
        }
        public RequestStatusViewForm(Data.RequestInfo requestInfo):this()
        {
            this.requestInfo = requestInfo;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            List<ReqeustStatusLogDetails> ReqeustStatusLogDetails = Data.RequestDB.GetReqeustStatusLogDetails(this.requestInfo.ID);

            for (int i = 0; i < ReqeustStatusLogDetails.Count; i++ )
                {
                    if (ReqeustStatusLogDetails[i] != null)
                    {
                        Application.UserControls.StatusView statusView = new UserControls.StatusView();
                        if (i == ReqeustStatusLogDetails.Count - 1)
                            statusView.Arrow.Visibility = Visibility.Collapsed;
                        statusView.DataContext = ReqeustStatusLogDetails[i];
                        if (ReqeustStatusLogDetails[i].ActionID == (int)DB.Action.Reject)
                            statusView.Arrow.Stroke = new SolidColorBrush(Colors.Red);
                        StackPanelItem.Children.Add(statusView);
                    }
                };
            
        }
    }


}
