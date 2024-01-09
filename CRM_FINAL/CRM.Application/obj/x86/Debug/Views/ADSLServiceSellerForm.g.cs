﻿#pragma checksum "..\..\..\..\Views\ADSLServiceSellerForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1DA13C47C2BB5000193F8664219E2A8CF76897B0"
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
    /// ADSLServiceSellerForm
    /// </summary>
    public partial class ADSLServiceSellerForm : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer MainScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView AccessListBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox SelectAllCheckBox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/adslservicesellerform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
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
            this.CityComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 29 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
            this.CityComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CityComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AccessListBox = ((System.Windows.Controls.ListView)(target));
            
            #line 32 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
            this.AccessListBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.ListBox_PreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SelectAllCheckBox = ((System.Windows.Controls.CheckBox)(target));
            
            #line 42 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
            this.SelectAllCheckBox.Checked += new System.Windows.RoutedEventHandler(this.SelectAllCheckBox_Checked);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
            this.SelectAllCheckBox.Unchecked += new System.Windows.RoutedEventHandler(this.SelectAllCheckBox_Unchecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\Views\ADSLServiceSellerForm.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveForm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

