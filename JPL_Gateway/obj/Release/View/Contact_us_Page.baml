   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  &     JPL_Gateway.View.Contact_us_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources    Textbox �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name �� CornerRadius ��Name  Apply_Button1 (� Stretch (� StretchDirection �� Triggers :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration	 �� To��%3  &	        &	 �     �� " ��.���  � ��$	 10q���  � ��	 �� "
 ��.R��� $ Bd��#��z�����	#FF282259$��2q�$ 5��._��� $ PART_ContentHost���� " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��
	 �������  � ��	 �� "
 ��.R��� $��Border��$ 4��$��1q�|�.  
������ ���	#FF282259._�(� $ Uniform=�$ DownOnly=�.���� $��2q�$��Center=�$��Center=���.  :� $ 
MouseEnter��.[��� .���� .��W� $ Border��$ Opacity�$ 1X�$ 0:0:0.2D��� $ WhiteX�$ 0:0:0.2D�$ Border��$ Background.Color��� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ BorderBrush.Color��� $ 	#FF282259X�$ 0:0:0.2D�$ Foreground.Color�:� $ 
MouseLeave��.[��� .���� .��W� $ Border��$ Opacity�$ 1X�$ 0:0:0.2D��� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ Background.Color��� $ TransparentX�$ 0:0:0.2D�$ Border��$ BorderBrush.Color��� $ WhiteX�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)�.��� .+��� -    ��Name$ backBtn��$��22��
������ $��20,8,0,0q�|�.  $	��Top=�$
��Left=�$	��100������ " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R��  � ColumnDefinitions �� $��15 ��� $	��30* �.+���  ��Name$	 btn��$��20��$��25��
������ $��0�� J� RenderTransformOrigin$ 0.5,0.5&�.+�F�  F�Name$ Line2��
�������$��1����B� ��D� E�  E� 
StartPoint$
 13,0&�.����  �� Point$ 0,10.5&��� $ 13,22&�z��� (� $��1��$ Uniform=�$ DownOnly=�.���� $��85��$��13(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����	#FFA0A0A0����  �� Path$ StrDevice01�  	Resources �� Source# [  P�   P� Property  �@J�IsMouseOver! P� Value! . " N� EnterActions" �� .���� .��W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$	 1.1X�W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$	 1.1X�# N� ExitActions# �� .���� .��W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleX)�$ 1X�W� $ 0:0:0.05D�$	 btn��$/ )(RenderTransform).(ScaleTransform.ScaleY)�$ 1X�P�   � {�! .  . ��� $ �� 
TargetName$	$ btn�� �@J�Opacity$
	 0.25Q�� $
��Left=�$��50��$	��Top=�$	��430��$��	26,34,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��430��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"����� $ StrContact01�# [ � $
��Left=�$��50��$	��Top=�$	��430��$��	26,84,0,0q�.+�(� $ Uniform=�$ DownOnly=�$	��Top=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$	��430������ $ StrContact02�# [ � $
��Left=�$��80��$	��Top=�$	��390��$��
492,84,0,0q�.+�(� $ Uniform=�$ DownOnly=�$	��Top=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$	��390��% �� TextAlignment$% Center=����� $ StrContact09�# [ �� -   $ contact��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�  Apply_Button#��[ $	��390��$��36��$��492,166,0,0q�$	��Top=�$
��Left=�
�������$��15(�|�.  ���� $ StrContact12�# [ �� -   $
 chat��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��390��$��36��$��492,212,0,0q�$	��Top=�$
��Left=�
�������$��15(�|�.  ���� $ StrContact10�# [ �� -   $ website��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��390��$��36��$��492,258,0,0q�$	��Top=�$
��Left=�
�������$��15(�|�.  $ 1.651,2.014&����� $ StrContact11�# [ �� $��JPL UK��$
��Left=�$��50��$	��Top=�$	��117��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(�$��Center=�$��
36,140,0,0q��� $
��Left=�$��50��$��50��$��
97,194,0,0q�$3��-/JPL_Gateway;component/Resources/JPL_Icon.png��$	��Top=��� $
��Left=�$��25��$��25��$��185,190,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Phone_DB.png��$	��Top=��� $��+44 1258 820100��$
��Left=�$��30��$	��Top=�$	��159��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,188,0,0q��� $
��Fill=�$
��Left=�$��25��$��18��$��185,224,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Email_DB.png��$	��Top=��� $��sales@jpl.uk.com��$
��Left=�$��30��$	��Top=�$	��172��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,218,0,0q��� 
�������$��2q�$��1��$��
50,267,0,0q�$	��Top=�$
��Left=�$	��342���� $��JPL USA��$
��Left=�$��50��$	��Top=�$	��117��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(�$��Center=�$��
36,271,0,0q��� $
��Left=�$��50��$��50��$��
97,325,0,0q�$3��-/JPL_Gateway;component/Resources/JPL_Icon.png��$	��Top=��� $
��Left=�$��25��$��25��$��185,320,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Phone_DB.png��$	��Top=��� $��+1 321 300 1141��$
��Left=�$��30��$	��Top=�$	��159��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,318,0,0q��� $
��Left=�$��25��$��18��$��185,355,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Email_DB.png��$	��Top=��� $��usasales@jpltele.com��$
��Left=�$��30��$	��Top=�$	��172��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,348,0,0q��� 
�������$��2q�$��1��$��
50,398,0,0q�$	��Top=�$
��Left=�$	��342���� $��
JPL Europe��$
��Left=�$��50��$	��Top=�$	��117��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(�$��Center=�$��
36,400,0,0q��� $
��Left=�$��50��$��50��$��
97,454,0,0q�$3��-/JPL_Gateway;component/Resources/JPL_Icon.png��$	��Top=��� $
��Left=�$��25��$��25��$��185,450,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Phone_DB.png��$	��Top=��� $��+31 (35) 8080201��$
��Left=�$��30��$	��Top=�$	��159��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,448,0,0q��� $
��Left=�$��25��$��18��$��185,484,0,0q�$?��9/JPL_Gateway;component/Resources/Button_Icon/Email_DB.png��$	��Top=��� $��emeasales@jpltele.com��$
��Left=�$��30��$	��Top=�$	��172��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$��Center=�$��225,478,0,0q�