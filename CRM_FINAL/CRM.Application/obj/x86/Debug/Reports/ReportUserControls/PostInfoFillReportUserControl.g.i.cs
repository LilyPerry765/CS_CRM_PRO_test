﻿#pragma checksum "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "58CB11C8CA411DB64D07950E2A46BD5E2CFC26CD"
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


namespace CRM.Application.Reports.ReportUserControls {
    
    
    /// <summary>
    /// PostInfoFillReportUserControl
    /// </summary>
    public partial class PostInfoFillReportUserControl : CRM.Application.Local.ReportBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CityComboBox;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CenterComboBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox CabinetComboBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PostContactLessThanTextBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PostContactMoreThanTextBox;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortLessThanTextBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PortMoreThanTextBox;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CRM.Application.UserControls.CheckableComboBox PCMTypeComboBox;
        
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
            System.Uri resourceLocater = new System.Uri("/CRM.Application;component/reports/reportusercontrols/postinfofillreportusercontr" +
                    "ol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
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
            
            #line 40 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.CityComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CityComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CenterComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 43 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.CenterComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CenterComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CabinetComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            case 4:
            this.PostContactLessThanTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 50 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.PostContactLessThanTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PostContactMoreThanTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.PostContactMoreThanTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PortLessThanTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 56 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.PortLessThanTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 7:
            this.PortMoreThanTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 59 "..\..\..\..\..\Reports\ReportUserControls\PostInfoFillReportUserControl.xaml"
            this.PortMoreThanTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.NumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PCMTypeComboBox = ((CRM.Application.UserControls.CheckableComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
