﻿#pragma checksum "..\..\..\..\Views\CycleList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78C2E57AEE1180A392CD87023EA26C4E3F8D2439"
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
    /// CycleList
    /// </summary>
    public partial class CycleList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpander;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CycleNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromDateDataPicker;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToDateDataPicker;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CycleYearTextBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker DueDateDataPicker;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Views\CycleList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetListButton;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\CycleList.xaml"
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/cyclelist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CycleList.xaml"
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
            this.CycleNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.FromDateDataPicker = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 4:
            this.ToDateDataPicker = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 5:
            this.CycleYearTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.DueDateDataPicker = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 7:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\Views\CycleList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ResetListButton = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\Views\CycleList.xaml"
            this.ResetListButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 78 "..\..\..\..\Views\CycleList.xaml"
            this.ItemsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 82 "..\..\..\..\Views\CycleList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItem);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 87 "..\..\..\..\Views\CycleList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 92 "..\..\..\..\Views\CycleList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteItem);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
