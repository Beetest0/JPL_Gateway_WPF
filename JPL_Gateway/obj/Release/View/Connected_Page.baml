   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  %    JPL_Gateway.View.Connected_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View     -     � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources    DeviceButton �� 
TargetType �� Property	 �� Value
 �� 
TargetType J� RenderTransformOrigin �� Triggers P� Property P� Value N� EnterActions g� Duration �� 
TargetName �� TargetProperty	 W� To g� DesiredFrameRate N� ExitActions �� 
TargetName��%K  &	        �� " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� $��
ButtonGrid��
������ $ 0.5,0.5&�.+��� $��Center=�$��Center=��� $��border��$��0q�
���   �#��z��._��� .+��� #��z��z���  P�  �@J�IsMouseOver .  �� .���� .��W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$	 1.1X�$ 30X�W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$	 1.1X�$ 30X� �� .���� .��W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$ 1X�$ 30X�W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$ 1X�$ 30X�P�  � {� .  . ��� $ 
ButtonGrid�� �@J�Opacity$
	 0.25Q�.��� .+�� -    �Name$ Device1��$��110,130,0,0q�$	��300��$	��374��$	��Top=�$
��Left=�.+��� -    ��Name$ Conn_DeviceModel1��$	��300��$	��300��$	��Top=�
������ #��[   �� IsCancel . ����  �� 	UriSource$! /Resources/Device/NOUSB.png?�� $
��Left=�$��18��$	��Top=�$	��300��$��	0,305,0,0q�.+�(�  (� Stretch$ Uniform=� (� StretchDirection$ DownOnly=�.���� -    ��Name$ DeviceName1��$��	No Device��$	��300�� �� TextAlignment$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340$
��Bold"�� $
��Left=�$��18��$	��Top=�$	��300��$��	0,324,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $ DeviceData1��$��################��$	��300��$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340� $
��Left=�$��18��$	��Top=�$	��300��$��	0,342,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $
 SKU1��$��###-###-###��$	��300��$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340�� -   #��[  
������ $
��Left=�$��30��$��30��$��253,340,0,0q�$	��Top=�$ 0.5,0.5&����� -    ��Name$ Conn_update0��$1��+/Resources/Button_Icon/Updates_DB_Small.png��� -	   $ Device2��$��510,130,0,0q�$	��300��$	��374��$	��Top=�$
��Left=�.+��� -
   $ Conn_DeviceModel2��$	��300��$	��300��$	��Top=�
������ #��[   . ���� $! /Resources/Device/NOUSB.png?�� $
��Left=�$��18��$	��Top=�$	��300��$��	0,305,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $ DeviceName2��$��	No Device��$	��300��$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340$
��Bold"�� $
��Left=�$��18��$	��Top=�$	��300��$��	0,324,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $ DeviceData2��$��################��$	��300��$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340� $
��Left=�$��18��$	��Top=�$	��300��$��	0,342,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $
 SKU2��$��###-###-###��$	��300��$ Center=�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����#0C2340�� -     ScaleButton#��[ $
��Left=�$��30��$��30��$��253,340,0,0q�$	��Top=����� -   $ Conn_update1��$3��-/Resources/Button_Icon/Updates_DB_Small_N.png��