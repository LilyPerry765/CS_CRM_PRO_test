﻿#pragma checksum "..\..\..\..\Views\TechnicalRequestList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B3DC202492544C1F137436718C9F60CF2656E5D0"
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


namespace CRM.Application.Views {
    
    
    /// <summary>
    /// TechnicalRequestList
    /// </summary>
    public partial class TechnicalRequestList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 9 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpander;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox CenterComboBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RequestIDTextBox;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox RequestTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromDate;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToDate;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OldTelehoneNoTextBox;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewTelehoneNoTextBox;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrintButton;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.Pager Pager;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Views\TechnicalRequestList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/technicalrequestlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\TechnicalRequestList.xaml"
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
            this.SearchExpander = ((System.Windows.Controls.Expander)(target));
            return;
            case 2:
            this.CityComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 3:
            this.CenterComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 4:
            this.RequestIDTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.RequestTypeComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 6:
            this.FromDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 7:
            this.ToDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 8:
            this.OldTelehoneNoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.NewTelehoneNoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\..\..\Views\TechnicalRequestList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\..\Views\TechnicalRequestList.xaml"
            this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 12:
            this.PrintButton = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Views\TechnicalRequestList.xaml"
            this.PrintButton.Click += new System.Windows.RoutedEventHandler(this.PrintButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Pager = ((CRM.Application.UserControls.Pager)(target));
            return;
            case 14:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 16:
            
            #line 112 "..\..\..\..\Views\TechnicalRequestList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveColumnItem_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 117 "..\..\..\..\Views\TechnicalRequestList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ReportSettingItem_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 15:
            
            #line 104 "..\..\..\..\Views\TechnicalRequestList.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxChanged);
            
            #line default
            #line hidden
            
            #line 104 "..\..\..\..\Views\TechnicalRequestList.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.UnCheckBoxChanged);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

