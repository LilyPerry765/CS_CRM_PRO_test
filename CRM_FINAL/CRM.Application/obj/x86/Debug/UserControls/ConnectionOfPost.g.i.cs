﻿#pragma checksum "..\..\..\..\UserControls\ConnectionOfPost.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B69ED8C933430B844C2D781B53C48C1187B10079"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace CRM.Application.UserControls {
    
    
    /// <summary>
    /// ConnectionOfPost
    /// </summary>
    public partial class ConnectionOfPost : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander CabinetAndPostExpander;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox InputComboBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ConnectionDataGrid;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn ConnectionColumn;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn PostContactColumn;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn BuchtTypeColumn;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn MUIDColumn;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn SwitchCodeColumn;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn TelePhoneNoColumn;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridComboBoxColumn StatusColumn;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/usercontrols/connectionofpost.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
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
            
            #line 7 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
            ((CRM.Application.UserControls.ConnectionOfPost)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CabinetAndPostExpander = ((System.Windows.Controls.Expander)(target));
            return;
            case 3:
            this.InputComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\..\UserControls\ConnectionOfPost.xaml"
            this.InputComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.InputComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ConnectionDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.ConnectionColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.PostContactColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 7:
            this.BuchtTypeColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            case 8:
            this.MUIDColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 9:
            this.SwitchCodeColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 10:
            this.TelePhoneNoColumn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 11:
            this.StatusColumn = ((System.Windows.Controls.DataGridComboBoxColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

