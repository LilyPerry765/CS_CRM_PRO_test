﻿using System.Windows;
using System.Windows.Controls;
//using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using System.Collections;
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ReportChangeName.xaml
    /// </summary>
    public partial class ReportChangeName : Local.PopupWindow
    {
        long _id;
        public ReportChangeName(long ID)
        {
            _id = ID;

            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            //IEnumerable<ChangeNameInfo> changeNameInfo = ChangeNameDB.GetChangeNameInfoByID((long)_id);

            //Viewer.LocalReport.DataSources.Clear();
            //Viewer.LocalReport.ReportEmbeddedResource = @"CRM.Application.Reports.ChangeName.rdlc";

            //ReportDataSource dataSource = new ReportDataSource("ChangeNameInfo", changeNameInfo);
            //Viewer.LocalReport.DataSources.Add(dataSource);

            //Viewer.RefreshReport();

            //Viewer.SetDisplayMode(DisplayMode.PrintLayout);
            //Viewer.ZoomMode = ZoomMode.Percent;
        }
    }
}