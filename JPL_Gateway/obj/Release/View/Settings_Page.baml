   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  $    JPL_Gateway.View.Settings_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View     -     � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources    ComboBox �� 
TargetType �� Property	 �� Value
 �� 
TargetType d� 	IsChecked �� Path �� Mode �� RelativeSource � ColumnDefinitions ��Name �� CornerRadius F�Name �� Stretch �� Triggers P� Property P� Value �� 
TargetName J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency  J� SnapsToDevicePixels! � ActualWidth" �� MaxDropDownHeight# K� IsItemsHost$ �� Triggers  MyButton  
Update_Now% (� Stretch& (� StretchDirection' �� TextAlignment  	Resources( �� Source) ��Name* ��Name	+ �� X1	, �� Y1	- �� X2	. �� Y2/ E� 
StartPoint0 �� Point1 � Point12 � Point23 :� RoutedEvent	4 �� To5 g� Duration6 �� 
TargetName7 �� TargetProperty  
Up_to_date��%�  &	        &	 �    &	 �    7  &	 �    7  �� " ��.���  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� .+�d� $��2��|�.   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+���� .R��  �� $	��57* ��� $��7* �.+��� $ Border��$��0��$��2��$ 4�����#0C2340$��2q�F� $ Arrow��$��1�����	#FFBBBBBB$��Center=�$��Center=�"����        � 	=  	= � z     	$��7.667��$��10.833,15.333,10.833,13q�$��13.334��$
 Fill=� P�  �@J�IsMouseOver . . ��� $ Border�� � ��
	 �����P�  �@d�	IsChecked$
 trueY�. ��� $ Border�� � ��
	 ������� $��ContentSite�� .  #��z #��z #��z���� $ PART_EditableTextBox��$y�Hidden=�# z � $��Popup��$��Bottom=�#��z  . |�.  $��Slide=�.��� $��DropDown��  . #��z! #��z" .+��� $ DropDownBorder��
�������$��2q����#0C2340��   . .���� # . $ �� " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R��� $��border��$��0q�
���   �#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  �@J�Opacity$		 0.8Q��� " ��.���  �@�OverridesDefaultStyle	 . ��  � ��
	 �������  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� .+��� $ Border��$ 4��$��1q�|�.  
������ ���#0C2340._��  �� $��* ��� $��30 �.+�(� $��0��$% Uniform=�$& DownOnly=�.���� $	��105��$' Center=�$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����� $ StrDevice_update10�8(   �� $��1��$��24��$��24��$��0,0,0,0q�._��� $) Canvas1��$��24��$��24��.+��� $* Line1��$+ 12��$	, 2.5��$- 12��$. 17��
�������$��2��F� $ Line2��
�������$��2����B� ��D� E� $
/ 4,11&�.���� $0 10,16&�� $1 12,17.5&�$2 14,16&��� $0 20,11&�F� $ Line3��
�������$��2����B� ��D� E� $
/ 2,17&�.���� $
0 2,18&�� $1 2.5,21&�$
2 5,21&��� $0 19,21&�� $1 21.5,21&�$2 22,18&��� $0 22,17&� :� $3 
MouseEnter��.[��� .���� .���� $4 WhiteX�$5 0:0:0.2D�$6 Border��$7 Background.Color��� $4 #0C2340X�$5 0:0:0.2D�$6 Border��$7 BorderBrush.Color��� $4 #0C2340X�$5 0:0:0.2D�$*7 $(Foreground).(SolidColorBrush.Color)��� $4 #0C2340X�$5 0:0:0.2D�$6 Line1��$7 Stroke.Color��� $4 #0C2340X�$5 0:0:0.2D�$6 Line2��$7 Stroke.Color��� $4 #0C2340X�$5 0:0:0.2D�$6 Line3��$7 Stroke.Color�:� $3 
MouseLeave��.[��� .���� .���� $4 #0C2340X�$5 0:0:0.2D�$6 Border��$7 Background.Color��� $4 TransparentX�$5 0:0:0.2D�$6 Border��$7 BorderBrush.Color��� $4 WhiteX�$5 0:0:0.2D�$*7 $(Foreground).(SolidColorBrush.Color)��� $4 WhiteX�$5 0:0:0.2D�$6 Line1��$7 Stroke.Color��� $4 WhiteX�$5 0:0:0.2D�$6 Line2��$7 Stroke.Color��� $4 WhiteX�$5 0:0:0.2D�$6 Line3��$7 Stroke.Color��� " ��.���  �@�OverridesDefaultStyle	 . ��  � {�	 .  ��  � ��	 �� "
 ��.R��  �� $��* ��� $��30 �.+��� $ Border��$��0��|�.  ._�(� $% Uniform=�$& DownOnly=�.���� $	��115��$' Center=�$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$
��Bold"����� $ StrDevice_update07�8(   �� $��1��$��25��$��25��$��0,0,0,0q�._��� $<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��.��� -   .+�� $
��Left=�$��50��$	��Top=�$	��430��$��	26,34,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��430��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"����� $ StrSetting01�#( [ � $
��Left=�$��50��$	��Top=�$	��500��$��	26,71,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrSetting02�#( [ � $
��Left=�$��30��$	��Top=�$	��300��$��
26,135,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��300��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrSetting03�#( [ � $
��Left=�$��30��$	��Top=�$	��300��$��
26,196,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��300��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrSetting04�#( [ � $
��Left=�$��30��$	��Top=�$	��500��$��
26,255,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrSetting07�#( [ � $
��Left=�$��30��$	��Top=�$	��500��$��
53,278,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(����	#FF6C6C6C���� $ StrSetting08�#( [ � -   $��lbNoti01��$
��Left=�$��30��$	��Top=�$	��500��$��
26,315,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrSetting05�#( [ � -   $��lbNoti02��$
��Left=�$��30��$	��Top=�$	��500��$��
53,338,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(����	#FF6C6C6C���� $ StrSetting06�#( [ �� 
�������$��2q�$��1��$��	0,384,0,0q�$	��Top=�$��Center=�$	��868��� $
��Left=�$��50��$	��Top=�$	��500��$��
26,410,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrSetting09�#( [ � -   $��gateway_grid��$	��920��$��80��$��	0,469,0,0q�$	��Top=�$y�Visible=�.+�� $
��Left=�$��30��$	��Top=�$	��200��$��26,5,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $��JPL Gateway��$	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(�� $
��Left=�$��30��$	��Top=�$	��200��$��	48,27,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$
��Bold"����� $ StrSetting10�#( [ �� $��:��$
��Left=�$��32��$	��Top=�$��20��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
230,27,0,0q�$
��Bold"��� -   8 ��Name$8 NextVersion��$
��####��$
��Left=�$��32��$	��Top=�$��89��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$
��Bold"�$��
255,27,0,0q�� $
��Left=�$��30��$	��Top=�$	��200��$��	48,51,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(����	#FF6C6C6C���� $ StrSetting11�#( [ �� $��:��$
��Left=�$��32��$	��Top=�$��20��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
230,51,0,0q����	#FF6C6C6C�� -   $8 CurrentVersion��$
��####��$
��Left=�$��32��$	��Top=�$��71��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
255,51,0,0q����	#FF6C6C6C�� -   9 ��Name$9 gateway_update��#��[ $	��140��$��35��$��	754,2,0,0q�$
��Left=�$	��Top=�|�.  � -	   $��chrome_grid��$	��920��$��80��$��	0,549,0,0q�$	��Top=�$y�Visible=�.+�� $
��Left=�$��30��$	��Top=�$	��200��$��26,5,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $��ChromeHost Native App��$	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(�� $
��Left=�$��30��$	��Top=�$	��200��$��	48,27,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$
��Bold"����� $ StrSetting10�#( [ �� $��:��$
��Left=�$��32��$	��Top=�$��20��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
230,27,0,0q�$
��Bold"��� -
   $8 NextVersion_Chrome��$
��####��$
��Left=�$��32��$	��Top=�$��89��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$
��Bold"�$��
255,27,0,0q�� $
��Left=�$��30��$	��Top=�$	��200��$��	48,51,0,0q�.+�(� $% Uniform=�$& DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(����	#FF6C6C6C���� $ StrSetting11�#( [ �� $��:��$
��Left=�$��32��$	��Top=�$��20��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
230,51,0,0q����	#FF6C6C6C�� -   $8 CurrentVersion_Chrome��$
��####��$
��Left=�$��32��$	��Top=�$��71��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��16(�$��Center=�$��
255,51,0,0q����	#FF6C6C6C�� -   $9 chrome_update��#��[ $	��140��$��35��$��	754,2,0,0q�$
��Left=�$	��Top=�|�.  �� -   : ��Name$: combobox_language��$��574,132,26,0q�$	��320��#��[  ; �� SelectedIndex$; 0��$	��Top=�|�.  .@��� .���� < �� Orientation$< 
Horizontal=�$	��310��$��32��.+��� $��English��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/UK.png��$��50��$��32��$��15,0,0,0q��� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��Español��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/ES.png��$��80��$��32���� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��	Français��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/FR.png��$��80��$��32���� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��Deutsch��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/DE.png��$��80��$��32���� -   $: combobox_country��$��574,192,26,0q�$	��320��#��[  $; 0��$	��Top=�|�.  .@��� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��United Kingdom��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/UK.png��$��80��$��32���� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��Spain��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/ES.png��$��80��$��32���� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��French��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/FR.png��$��80��$��32���� .���� $< 
Horizontal=�$	��310��$��32��.+��� $��Germany��
���   �$��18(�$	��200��$' Center=�$��Center=��� $-��'/JPL_Gateway;component/Resources/DE.png��$��80��$��32��d� -   = d�Name$= startup��$��46��$��28��  ToggleButton#��[ $��714,263,160,359q�|�.  > � Cursor$
> Hand��d� -   $
= noti��$��46��$��28��#��[ $��714,323,160,299q�|�.  {�.  $
> Hand��