﻿#pragma checksum "..\..\..\View\Device_update_Page.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6F5026806C2D7980668A6E6CEEBEBDD3FA2F7E612C99780D9156B7365E8B7D65"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using JPL_Gateway.View;
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


namespace JPL_Gateway.View {
    
    
    /// <summary>
    /// Device_update_Page
    /// </summary>
    public partial class Device_update_Page : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 306 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame frame1;
        
        #line default
        #line hidden
        
        
        #line 307 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backBtn;
        
        #line default
        #line hidden
        
        
        #line 395 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DeviceName;
        
        #line default
        #line hidden
        
        
        #line 407 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock currentversion;
        
        #line default
        #line hidden
        
        
        #line 419 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock latestversion;
        
        #line default
        #line hidden
        
        
        #line 445 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Update_device_name;
        
        #line default
        #line hidden
        
        
        #line 449 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpdateInfo_grid;
        
        #line default
        #line hidden
        
        
        #line 455 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbUpdateInfo;
        
        #line default
        #line hidden
        
        
        #line 457 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button FirmwareBtn;
        
        #line default
        #line hidden
        
        
        #line 459 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Device_image;
        
        #line default
        #line hidden
        
        
        #line 468 "..\..\..\View\Device_update_Page.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox softphone;
        
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
            System.Uri resourceLocater = new System.Uri("/JPL_Gateway;component/view/device_update_page.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\Device_update_Page.xaml"
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
            this.frame1 = ((System.Windows.Controls.Frame)(target));
            return;
            case 2:
            this.backBtn = ((System.Windows.Controls.Button)(target));
            
            #line 307 "..\..\..\View\Device_update_Page.xaml"
            this.backBtn.Click += new System.Windows.RoutedEventHandler(this.backBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.DeviceName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.currentversion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.latestversion = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.Update_device_name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.UpdateInfo_grid = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.lbUpdateInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.FirmwareBtn = ((System.Windows.Controls.Button)(target));
            
            #line 457 "..\..\..\View\Device_update_Page.xaml"
            this.FirmwareBtn.Click += new System.Windows.RoutedEventHandler(this.FirmwareBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.Device_image = ((System.Windows.Controls.Image)(target));
            return;
            case 11:
            this.softphone = ((System.Windows.Controls.ComboBox)(target));
            
            #line 468 "..\..\..\View\Device_update_Page.xaml"
            this.softphone.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.softphone_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
