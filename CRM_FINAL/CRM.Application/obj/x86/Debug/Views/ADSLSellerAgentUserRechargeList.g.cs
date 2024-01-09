﻿#pragma checksum "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7D21EA4558412E7CCACCD30A2B712B438A818694"
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
using System.Configuration.Assemblies;
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


namespace CRM.Application.Views {
    
    
    /// <summary>
    /// ADSLSellerAgentUserRechargeList
    /// </summary>
    public partial class ADSLSellerAgentUserRechargeList : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer MainScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromPaymentDate;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToPaymentDate;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrintButton;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/adslselleragentuserrechargelist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
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
            this.MainScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.FromPaymentDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 3:
            this.ToPaymentDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 4:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.PrintButton = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\..\Views\ADSLSellerAgentUserRechargeList.xaml"
            this.PrintButton.Click += new System.Windows.RoutedEventHandler(this.PrintButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

