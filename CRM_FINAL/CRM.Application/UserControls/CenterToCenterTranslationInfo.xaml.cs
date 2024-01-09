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

namespace CRM.Application.UserControls
{
    /// <summary>
    /// Interaction logic for CenterToCenterTranslationInfo.xaml
    /// </summary>
    public partial class CenterToCenterTranslationInfo : Local.UserControlBase
    {
        private long requestID = 0;

        public CenterToCenterTranslationInfo()
        {
            InitializeComponent();
        }
        public CenterToCenterTranslationInfo(long ID):this()
        {
            this.requestID = ID;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (_IsLoaded)
                return;
            else
                _IsLoaded = true;

            CenterToCenterTranslationUserControlInfo centerToCenterTranslationInfo = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationInfo(requestID);

            this.DataContext = centerToCenterTranslationInfo;

        }
    }
}
