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
    /// Interaction logic for PCMInfoForm.xaml
    /// </summary>
    public partial class PCMInfoForm : Local.PopupWindow
    {
        AssignmentInfo AssignmentInfo = null;
        public PCMInfoForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PCMInfoForm(AssignmentInfo _assignmentInfo)
            : this()
        {
            AssignmentInfo = _assignmentInfo;
        }

        private void Initialize()
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AssignmentInfo != null)
            {
                int count = 0;
                ItemsDataGrid.ItemsSource = DB.GetAllInformationBucht
               (new List<int> { },
                new List<int> { (int)AssignmentInfo.CenterID },
                new List<int> { },
                -1,
                -1,
                new List<int> { AssignmentInfo.CabinetID ?? 0 },
                AssignmentInfo.InputNumber ?? -1,
                new List<int> { },
                -1,
                string.Empty,
                -1
                , -1
                , 0
                , 10
                , -1
                , new List<int> { }
                , new List<int> { }
                , new List<int> { }
                , new List<int> { }
                , new List<int> { }
                , null
                , new List<int> { }
                , new List<int> { }
                , null
                , null
                , out count
                , null
                , null
                , null
                , new List<int> { }
                , new List<int> { }
                , new List<int> { }
                , new List<int> { }
                ,true
                );
            }
        }
    }
}
