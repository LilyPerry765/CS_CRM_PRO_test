﻿using CRM.Data;
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
    /// Interaction logic for TranslationPostInfo.xaml
    /// </summary>
    public partial class TranslationCentralCableMDFInfo : Local.UserControlBase
    {
        private long reqeustID = 0;

        TranslationCentralCableMDFDetails _translationCentralCableMDFDetails { get; set; }
        Request _reqeust { get; set; }

        public TranslationCentralCableMDFInfo()
        {
            InitializeComponent();
            Initialize();
        }
        public TranslationCentralCableMDFInfo(long requestID)
            : this()
        {
            this.reqeustID = requestID;
        }
        private void Initialize()
        {

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

            if (reqeustID != 0)
            {

                _translationCentralCableMDFDetails = Data.TranslationCentralCableMDFDB.GetTranslationVentralCableMDFDetails(reqeustID);
                _reqeust = Data.RequestDB.GetRequestByID(reqeustID);
                this.DataContext = _translationCentralCableMDFDetails;
            }


        }
    }
}