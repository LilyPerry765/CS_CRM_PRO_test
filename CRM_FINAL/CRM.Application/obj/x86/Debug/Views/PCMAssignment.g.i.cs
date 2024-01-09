﻿#pragma checksum "..\..\..\..\Views\PCMAssignment.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3E9907444022CB451DF720AD69A6008FC71A3BD3"
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
    /// PCMAssignment
    /// </summary>
    public partial class PCMAssignment : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MUIDComboBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox pcmTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox InputComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BuchtNoTextBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AddressTextBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\Views\PCMAssignment.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PostCodeTextBox;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Views\PCMAssignment.xaml"
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/pcmassignment.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\PCMAssignment.xaml"
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
            this.MUIDComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 28 "..\..\..\..\Views\PCMAssignment.xaml"
            this.MUIDComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.MUIDComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.pcmTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.InputComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\..\Views\PCMAssignment.xaml"
            this.InputComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InputComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.BuchtNoTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AddressTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.PostCodeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\Views\PCMAssignment.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveForm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

