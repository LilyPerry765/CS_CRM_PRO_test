﻿#pragma checksum "..\..\..\..\UserControls\TranslationCabinetInfo.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1EB7D94EE417104A4CF99409F5932DCFBD908023"
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


namespace CRM.Application.UserControls {
    
    
    /// <summary>
    /// TranslationCabinetInfo
    /// </summary>
    public partial class TranslationCabinetInfo : CRM.Application.Local.UserControlBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OldCabinetTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FromOldCabinetInputTextBox;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ToOldCabinetInputTextBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NewCabinetTextBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FromNewCabinetInputTextBox;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ToNewCabinetInputTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/usercontrols/translationcabinetinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\TranslationCabinetInfo.xaml"
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
            this.OldCabinetTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.FromOldCabinetInputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ToOldCabinetInputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.NewCabinetTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.FromNewCabinetInputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.ToNewCabinetInputTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
