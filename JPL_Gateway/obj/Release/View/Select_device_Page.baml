   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  )    #JPL_Gateway.View.Select_device_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=�.��� .+��� -    ��Name$ backBtn��$	��100��$��22��
������ $��20,8,0,0q�$	��Top=�$
��Left=�|�.  ����  �� 
TargetType" ��.���  �� Property �@�OverridesDefaultStyle	 �� Value	 . ��  �@�Cursor$
	 Hand����  � ��	 �� 
 �� 
TargetType"
 ��.R��  � ColumnDefinitions �� $��15 ��� $	��30* �.+���  ��Name$	 btn��$��20��$��25��
������ $��0�� J� RenderTransformOrigin$ 0.5,0.5&�.+�F�  F�Name$ Line2��
�������$��1����B� ��D� E�  E� 
StartPoint$
 13,0&�.����  �� Point$ 0,10.5&��� $ 13,22&�z��� (� $��1�� (� Stretch$ Uniform=� (� StretchDirection$ DownOnly=�.���� $��85��$��13(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����	#FFA0A0A0����  �� Path$ StrDevice01�   	Resources �� Source# [   �� Triggers P�  P� Property �@J�IsMouseOver P� Value .  N� EnterActions �� .���� .��W�  g� Duration$ 0:0:0.05D� �� 
TargetName$	 btn�� �� TargetProperty$/ )(RenderTransform).(ScaleTransform.ScaleX)�	 W� To$	 1.1X�W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$	 1.1X� N� ExitActions �� .���� .��W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$ 1X�W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$ 1X�P�  � {� .  . ���  �� 
TargetName$	 btn�� �@J�Opacity$
	 0.25Q�� $
��Left=�$��50��$	��Top=�$	��700��$��	26,34,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -    ��Name$ 
DeviceName��$��	No Device��$	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"�� $
��Left=�$��50��$	��Top=�$	��700��$��	26,71,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $ 
DeviceData��$��################��$	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(�� $
��Left=�$��50��$	��Top=�$	��700��$��
26,102,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $	 SKU��$��###-###-###��$	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(��� -   $ Device_update��$��190,216,580,284q�  ScaleButton#��[ $	��150��$��Stretch=�$��Stretch=�|�.  $	��150��  � Triggers  :� ! :� RoutedEvent$! Button.Click��[����� $H B/JPL_Gateway;component/Resources/Button_Icon/Device_Updates_DB.png��� $
��Left=�$��60��$	��Top=�$	��200��$��164,379,556,211q�.+�(� $ Uniform=�$ DownOnly=�$	��Top=�.���� -   $ Updates_text��$	��200��" �� TextAlignment$" Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����#0C2340$��25(����� $ StrDevice02�# [  �� -   $ Device_settings��$��580,216,190,284q�#��[ $��Stretch=�$��Stretch=�|�.  $	��150��$	��150������ $I C/JPL_Gateway;component/Resources/Button_Icon/Device_Settings_DB.png��� $
��Left=�$��60��$	��Top=�$	��200��$��555,379,165,211q�.+�(� $ Uniform=�$ DownOnly=�$	��Top=�.���� -   $ Settings_text��$	��200��$" Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����#0C2340$��25(����� $ StrDevice03�# [  