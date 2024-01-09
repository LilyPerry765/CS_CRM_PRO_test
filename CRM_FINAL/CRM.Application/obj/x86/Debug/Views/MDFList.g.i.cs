﻿#pragma checksum "..\..\..\..\Views\MDFList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "68BFCDB430E8C5FCECC23522BD9DBC35C17CB132"
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
    /// MDFList
    /// </summary>
    public partial class MDFList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpander;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox CenterComboBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox MDFComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastNoVerticalFramesTextBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastNoHorizontalFramesTextBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox TypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox UsesComboBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.Pager Pager;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn TypeColmn;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn CenterColumn;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\Views\MDFList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn UsesColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/mdflist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\MDFList.xaml"
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
            this.MDFComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 5:
            this.LastNoVerticalFramesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.LastNoHorizontalFramesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.TypeComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 8:
            this.UsesComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 9:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\Views\MDFList.xaml"
            this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 10:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\..\..\Views\MDFList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Pager = ((CRM.Application.UserControls.Pager)(target));
            return;
            case 12:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 95 "..\..\..\..\Views\MDFList.xaml"
            this.ItemsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 99 "..\..\..\..\Views\MDFList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItem);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 104 "..\..\..\..\Views\MDFList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 109 "..\..\..\..\Views\MDFList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteItem);
            
            #line default
            #line hidden
            return;
            case 16:
            this.TypeColmn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 17:
            this.CenterColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 18:
            this.UsesColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

