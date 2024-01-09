﻿#pragma checksum "..\..\..\..\Views\QuotaDiscountList.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "78F7C71C6A442E324C883D04395479FE4C8F089F"
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
    /// QuotaDiscountList
    /// </summary>
    public partial class QuotaDiscountList : CRM.Application.Local.TabWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander SearchExpander;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox JobTitleComboBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox RequestTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox AnnounceComboBox;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromStartDate;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToStartDate;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DiscountAmountTextBox;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker FromEndDate;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker ToEndDate;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetButton;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ItemsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn QuotaJobTitleColumn;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn RequestTypeColumn;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\Views\QuotaDiscountList.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn AnnounceColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/quotadiscountlist.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\QuotaDiscountList.xaml"
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
            this.JobTitleComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 3:
            this.RequestTypeComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 4:
            this.AnnounceComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 5:
            this.FromStartDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 6:
            this.ToStartDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 7:
            this.DiscountAmountTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.FromEndDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 9:
            this.ToEndDate = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 10:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\..\Views\QuotaDiscountList.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.Search);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ResetButton = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\Views\QuotaDiscountList.xaml"
            this.ResetButton.Click += new System.Windows.RoutedEventHandler(this.ResetSearchForm);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ItemsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 89 "..\..\..\..\Views\QuotaDiscountList.xaml"
            this.ItemsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 93 "..\..\..\..\Views\QuotaDiscountList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddItem);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 98 "..\..\..\..\Views\QuotaDiscountList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditItem);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 103 "..\..\..\..\Views\QuotaDiscountList.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteItem);
            
            #line default
            #line hidden
            return;
            case 16:
            this.QuotaJobTitleColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 17:
            this.RequestTypeColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 18:
            this.AnnounceColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

