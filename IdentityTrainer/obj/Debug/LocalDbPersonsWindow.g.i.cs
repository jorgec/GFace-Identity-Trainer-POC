﻿#pragma checksum "..\..\LocalDbPersonsWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DC90D421905F8652496F0C802E7F79B91FB2801B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IdentityTrainer;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace IdentityTrainer {
    
    
    /// <summary>
    /// LocalDbPersonsWindow
    /// </summary>
    public partial class LocalDbPersonsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid GridPersonModel;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDelete;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtLog;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboGroups;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtPersonName;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\LocalDbPersonsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnCreatePerson;
        
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
            System.Uri resourceLocater = new System.Uri("/IdentityTrainer;component/localdbpersonswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LocalDbPersonsWindow.xaml"
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
            this.GridPersonModel = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.BtnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\LocalDbPersonsWindow.xaml"
            this.BtnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TxtLog = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.CboGroups = ((System.Windows.Controls.ComboBox)(target));
            
            #line 20 "..\..\LocalDbPersonsWindow.xaml"
            this.CboGroups.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboGroups_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TxtPersonName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.BtnCreatePerson = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\LocalDbPersonsWindow.xaml"
            this.BtnCreatePerson.Click += new System.Windows.RoutedEventHandler(this.BtnCreatePerson_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

