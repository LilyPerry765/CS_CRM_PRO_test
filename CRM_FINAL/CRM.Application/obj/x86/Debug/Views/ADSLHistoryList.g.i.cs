﻿#pragma checksum "..\..\..\..\Views\ADSLHistoryList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51EF646F9DF741D6F2AF7F22603DCD50EAC4503A"
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
    /// ADSLHistoryList
    /// </summary>
    public partial class ADSLHistoryList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpender;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TelNoTextBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox ServiceComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker StartDate;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ADSLHistoryListDataGrid;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn TelNoColumn;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn ServiceColumn;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Views\ADSLHistoryList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn StartDateColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/adslhistorylist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ADSLHistoryList.xaml"
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
            this.SearchExpender = ((System.Windows.Controls.Expander)(target));
            return;
            case 2:
            this.TelNoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ServiceComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 4:
            this.StartDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 5:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\..\..\Views\ADSLHistoryList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\..\Views\ADSLHistoryList.xaml"
            this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ADSLHistoryListDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 8:
            this.TelNoColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.ServiceColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 10:
            this.StartDateColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
