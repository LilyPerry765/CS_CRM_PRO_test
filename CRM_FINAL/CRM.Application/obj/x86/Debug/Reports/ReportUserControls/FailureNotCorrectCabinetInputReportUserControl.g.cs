﻿#pragma checksum "..\..\..\..\..\Reports\ReportUserControls\FailureNotCorrectCabinetInputReportUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7AACF9F5B58ABD134FF28E8FE6C0539FE3CB0345"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CRM.Application.Local;
using CRM.Application.UserControls;
using Enterprise.Controls;
using Enterprise.Controls.Primitives;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CRM.Application.Reports.ReportUserControls {
    
    
    /// <summary>
    /// FailureNotCorrectCabinetInputReportUserControl
    /// </summary>
    public partial class FailureNotCorrectCabinetInputReportUserControl : CRM.Application.Local.ReportBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Reports\ReportUserControls\FailureNotCorrectCabinetInputReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CenterCityUserControl CityCenterUC;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Reports\ReportUserControls\FailureNotCorrectCabinetInputReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromDate;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Reports\ReportUserControls\FailureNotCorrectCabinetInputReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToDate;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/reports/reportusercontrols/failurenotcorrectcabinetinp" +
                    "utreportusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Reports\ReportUserControls\FailureNotCorrectCabinetInputReportUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.CityCenterUC = ((CRM.Application.UserControls.CenterCityUserControl)(target));
            return;
            case 2:
            this.FromDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 3:
            this.ToDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

