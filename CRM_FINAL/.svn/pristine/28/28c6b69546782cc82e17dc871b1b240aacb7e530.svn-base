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
    /// Interaction logic for TranslationOpticalToNormalInfo.xaml
    /// </summary>
    public partial class TranslationOpticalToNormalInfo : Local.UserControlBase
    {
        long _requestID = 0;
        TranslationOpticalCabinetToNormal _translationOpticalCabinetToNormal { get; set; }

        TranslationOpticalCabinetToNormalInfo translationOpticalCabinetToNormalInfo;
        public TranslationOpticalToNormalInfo()
        {
            InitializeComponent();
            Initialize();
        }
        public TranslationOpticalToNormalInfo(long requestID):this()
        {
            _requestID = requestID;
        }

        private void Initialize()
        {
        }

        private void UserControlBase_Loaded(object sender, RoutedEventArgs e)
        {
            translationOpticalCabinetToNormalInfo = Data.TranslationOpticalCabinetToNormalDB.GetTranslationOpticalCabinetToNormalInfo(_requestID);

            this.DataContext = translationOpticalCabinetToNormalInfo;
        }
    }
}
