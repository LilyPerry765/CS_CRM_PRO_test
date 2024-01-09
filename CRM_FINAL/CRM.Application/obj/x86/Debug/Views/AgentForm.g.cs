﻿#pragma checksum "..\..\..\..\Views\AgentForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "732115DC9FED7483E3F6E737F7B38A422ABFB437"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CRM.Application.Codes.CustomControls;
using CRM.Application.Local;
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
    /// AgentForm
    /// </summary>
    public partial class AgentForm : CRM.Application.Local.PopupWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox CustomerGroupBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FirstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FatherNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Enterprise.Controls.DatePicker BirthdateDatePicker;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.Codes.CustomControls.NumericTextBox NationalCodeNumericTextBox;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.Codes.CustomControls.NumericTextBox CertificateNoNumericTextBox;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Views\AgentForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.Codes.CustomControls.NumericTextBox MobileNoNumericTextBox;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Views\AgentForm.xaml"
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/views/agentform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\AgentForm.xaml"
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
            this.CustomerGroupBox = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 2:
            this.FirstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.LastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.FatherNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.BirthdateDatePicker = ((Enterprise.Controls.DatePicker)(target));
            return;
            case 6:
            this.NationalCodeNumericTextBox = ((CRM.Application.Codes.CustomControls.NumericTextBox)(target));
            return;
            case 7:
            this.CertificateNoNumericTextBox = ((CRM.Application.Codes.CustomControls.NumericTextBox)(target));
            return;
            case 8:
            this.MobileNoNumericTextBox = ((CRM.Application.Codes.CustomControls.NumericTextBox)(target));
            return;
            case 9:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\Views\AgentForm.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveForm);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

