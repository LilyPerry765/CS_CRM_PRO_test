﻿#pragma checksum "..\..\..\..\UserControls\PCMUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "94294F3DCA24E6E92064FA962A6B43E3A31EB8F7"
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
    /// PCMUserControl
    /// </summary>
    public partial class PCMUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PCMBrandComboBox;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PCMTypelbl;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PCMTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox RockComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ShelfComboBox;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\UserControls\PCMUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CardComboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/usercontrols/pcmusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\PCMUserControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.PCMBrandComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.PCMTypelbl = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.PCMTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.RockComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 34 "..\..\..\..\UserControls\PCMUserControl.xaml"
            this.RockComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.RockComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ShelfComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\..\..\UserControls\PCMUserControl.xaml"
            this.ShelfComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ShelfComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CardComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
