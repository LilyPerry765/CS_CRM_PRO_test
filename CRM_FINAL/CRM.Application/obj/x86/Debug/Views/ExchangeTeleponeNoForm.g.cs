﻿#pragma checksum "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "54E84D0DB5799A8E0D3B344695D832D916977B8C"
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
    /// ExchangeSwitchForm
    /// </summary>
    public partial class ExchangeSwitchForm : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.ExchangeRequestInfo ExchangeRequestInfoUserControl;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox ExchangSwitch;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ExchangePostDetailDataGrid;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn FromSwitchColumn;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn TOSwitchColumn;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn FromSwitchPrecodeColumn;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn TOSwitchPrecodeColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/exchangeteleponenoform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
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
            this.ExchangeRequestInfoUserControl = ((CRM.Application.UserControls.ExchangeRequestInfo)(target));
            return;
            case 2:
            this.ExchangSwitch = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 3:
            this.ExchangePostDetailDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            
            #line 32 "..\..\..\..\Views\ExchangeTeleponeNoForm.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.NewItem);
            
            #line default
            #line hidden
            return;
            case 5:
            this.FromSwitchColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 6:
            this.TOSwitchColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 7:
            this.FromSwitchPrecodeColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 8:
            this.TOSwitchPrecodeColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
