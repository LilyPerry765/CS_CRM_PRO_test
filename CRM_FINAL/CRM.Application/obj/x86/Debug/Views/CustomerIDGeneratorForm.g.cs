﻿#pragma checksum "..\..\..\..\Views\CustomerIDGeneratorForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A34FE4EC991C04A31DF7A7E7293F69FF2DD42A8B"
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
    /// CustomerIDGeneratorForm
    /// </summary>
    public partial class CustomerIDGeneratorForm : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CenterComboBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CustomerType;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/customeridgeneratorform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
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
            this.CityComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 19 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
            this.CityComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CityComboBox_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CenterComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 22 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
            this.CenterComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CenterComboBox_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CustomerType = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\..\Views\CustomerIDGeneratorForm.xaml"
            this.CustomerType.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CustomerType_SelectionChanged_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

