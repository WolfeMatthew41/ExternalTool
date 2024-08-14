﻿#pragma checksum "..\..\..\..\Popups\FolderSelectionWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6266FF0C480764D1D73002DAC9F29426FEAD65A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using External_Tool.Popups;
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


namespace External_Tool.Popups {
    
    
    /// <summary>
    /// FolderSelectionWindow
    /// </summary>
    public partial class FolderSelectionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox PathList;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FolderPathBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPathButton;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemovePathButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FolderListNameBox;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox FolderListSelector;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConfirmButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/External Tool;component/popups/folderselectionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PathList = ((System.Windows.Controls.ListBox)(target));
            
            #line 16 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.PathList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.PathList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.FolderPathBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.FolderPathBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FolderPathBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AddPathButton = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.AddPathButton.Click += new System.Windows.RoutedEventHandler(this.OnAddButton);
            
            #line default
            #line hidden
            return;
            case 4:
            this.RemovePathButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.RemovePathButton.Click += new System.Windows.RoutedEventHandler(this.OnRemoveButton);
            
            #line default
            #line hidden
            return;
            case 5:
            this.FolderListNameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 42 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.FolderListNameBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.FolderPathBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 44 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnAddFolderListButton);
            
            #line default
            #line hidden
            return;
            case 7:
            this.FolderListSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 51 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.FolderListSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FolderListSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ConfirmButton = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            this.ConfirmButton.Click += new System.Windows.RoutedEventHandler(this.OnOkButton);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 56 "..\..\..\..\Popups\FolderSelectionWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCancelButton);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

