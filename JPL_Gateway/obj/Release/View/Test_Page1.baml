   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  !    JPL_Gateway.View.Test_Page1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       N� Title$  
Test_Page1�� N� 
Background
 �����-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� � 	Resources    DeviceButton �� 
TargetType �� Property �� Value �� 
TargetType J� RenderTransformOrigin	 �� Triggers
 P� Property P� Value N� EnterActions g� Duration �� 
TargetName �� TargetProperty	 W� To N� ExitActions �� 
TargetName��%'  &	        �� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R�� $��
ButtonGrid��
������ $ 0.5,0.5&�.+��� $��Center=�$��Center=��� $��border��$��0q�
���   �#��z��._��� .+��� #��z��z��� 	 P� 
 �@J�IsMouseOver .  �� .���� .��W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$	 1.1X�W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$	 1.1X� �� .���� .��W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$ 1X�W� $ 0:0:0.05D�$ 
ButtonGrid��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$ 1X�P� 
 � {� .  . ��� $ 
ButtonGrid�� �@J�Opacity$
 0.25Q�.��� .+�� -    �Name$ Device0��$	��300��$��312,126,308,170q�.+��� -    ��Name$ Conn_DeviceModel��$	��Top=�#��[   �� IsCancel . ��Z� ��Z� ����  �� 	UriSource$! /Resources/Device/NOUSB.png?��� -    ��Name$ 
DeviceName��$��	No Device��$��10,305,10,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����	#FF282259 �� TextAlignment$ Center=�$
��Bold"�$��18��$	��Top=��� -   $ 
DeviceData��$��10,324,10,0q�$��################��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����	#FF282259$ Center=�$��18��$	��Top=��� -   $	 SKU��$��###-###-###��$��10,342,10,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����	#FF282259$ Center=�$��18��$	��Top=��� -   $ Conn_update��  ScaleButton#��[ $
��Left=�$��20��$��20��$��253,343,0,0q�$	��Top=�����  �� ImageSource$G A/JPL_Gateway;component/Resources/Button_Icon/Updates_DB_Small.png��