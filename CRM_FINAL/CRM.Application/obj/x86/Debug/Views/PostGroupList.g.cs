﻿#pragma checksum "..\..\..\..\Views\PostGroupList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "37D05512E0D9E254D77BDCA91C5FBDC77AA17CD2"
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
    /// PostGroupList
    /// </summary>
    public partial class PostGroupList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpander;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox GroupNoTextBox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.Pager Pager;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\Views\PostGroupList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn CenterColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/postgrouplist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\PostGroupList.xaml"
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
            this.GroupNoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Views\PostGroupList.xaml"
            this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\Views\PostGroupList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Pager = ((CRM.Application.UserControls.Pager)(target));
            return;
            case 6:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 78 "..\..\..\..\Views\PostGroupList.xaml"
            this.ItemsDataGrid.RowEditEnding += new System.EventHandler<System.Windows.Controls.DataGridRowEditEndingEventArgs>(this.ItemsDataGrid_RowEditEnding);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 81 "..\..\..\..\Views\PostGroupList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItem);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 86 "..\..\..\..\Views\PostGroupList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 91 "..\..\..\..\Views\PostGroupList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteItem);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CenterColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

