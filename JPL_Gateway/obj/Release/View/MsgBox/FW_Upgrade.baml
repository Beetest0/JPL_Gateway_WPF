   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=nullA%clr-namespace:JPL_Gateway.View.MsgBoxJPL_Gateway.View.MsgBox  (    "JPL_Gateway.View.MsgBox.FW_Upgrade   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:JPL_Gateway.View.MsgBox       N� Title$   ��.���� -    ��Name$ 
MainBorder��$��10q� �� CornerRadius$ 8��$��0q�
���   � �� Padding$ 0q� � Triggers :�  :� RoutedEvent$ Window.Loaded��.[��� .���� .��W�  �� 
TargetName$ 
MainBorder�� �� TargetProperty$ Opacity� W� From$ 0X�		 W� To$	 1X�
 g� Duration$
 0:0:0.5D�._��  � RowDefinitions �� $	��400 � � ColumnDefinitions �� $	��550 �.+��� -   $
��Mask��$ 8��
�������� -    �Name$
 Grid��
������� J� OpacityMask !�  !� Visual ��  �� ElementName$
 Mask��.+��� -   $��
StackPanel��.+�]� -   $��ContentHost��$��0,20,0,0q�$��34��.+��� $2��,/JPL_Gateway;component/Resources/Updates.png��$��32��]� $��0,20,0,0q�$��30��.+�(�  (� Stretch$ Uniform=� (� StretchDirection$ DownOnly=�.���� $��25(�$
��Bold"�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)� �� TextAlignment$ Center=�$	��492������  �� Path$ StrPopup_update01�   	Resources �� Source# [  ]� $��0,0,0,0q�$��25��.+�(� $ Uniform=�$ DownOnly=�.���� $��20(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$ Center=�$	��492������ $ StrPopup_update02�# [  ]� $��0,0,0,0q�$��80��.+�(� $ Uniform=�$ DownOnly=�.���� $��16(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$ Center=�$	��550������ $ StrPopup_update03�# [  ]� $��0,0,0,0q�$��80��.+�(� $ Uniform=�$ DownOnly=�.���� -    ��Name$ 
updateinfo��$��16(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��Center=�$ Center=�$	��550��$
��Bold"�1.�� 2.�� 3.�� 4.]� $��0,15,0,0q�$��20��.+�(� $ Uniform=�$ DownOnly=�.���� $��16(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$ Center=�$	��550������ $ StrPopup_update04�# [  ]� -   $��
ButtonHost��$��Center=�$��0,20,0,0q�$	��400��.+��� -    ��Name$ 	cancelBtn��  Apply_Button#��[ $��0,0,0,0q�$	��130��$��36��$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����� $ StrPopup_update05�# [  �� -	   $ 
proceedBtn��#��[ $��
0,0,-140,0q�$	��130��$��36��$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����� $ StrPopup_update06�# [  