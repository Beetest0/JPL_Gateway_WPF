   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null)clr-namespace:JPL_GatewayJPL_Gateway  ?$clr-namespace:JPL_Gateway.PropertiesJPL_Gateway.Properties      JPL_Gateway.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:JPL_Gateway     2p$clr-namespace:JPL_Gateway.Properties     -     � UseLayoutRounding  .  � Title$ JPL Gateway��$	��750��$
��1000��$	��750��$
��1000�� � WindowStyle$
 None=� � AllowsTransparency . ���	#FFE8E8E8 � WindowStartupLocation$ CenterScreen=�-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� � Icon$ Resources/JPL_icon.ico�� � 
ResizeMode$ NoResize=� � Triggers :� 	 :� RoutedEvent$	 Window.Loaded��.[��� .���� '  !System.Windows.Shell.WindowChrome
   WindowChrome
      CaptionHeight$ 0Q�   ResizeBorderThickness$ 5q�.��� .+� � -     �Name$ frame4�� K� ZIndex$
 1000��$y�Hidden=��� -   $
��main��$��0q�$��0q�._�]� .+��� -   $��border��$	��Top=�$	��100��._�� .+��� $��0,77,0,0q�$
��Left=�$	��300������  �� ImageSource$' !Resources/Title/Logo_Bar_Crop.png�� j� Stretch$
 Fill=��� $��
300,77,0,0q����#0C2340�� $��Center=�$
��Left=�$	��120������ $% Resources/Title/Logo_circle.png��.���� -   $��JPL_logo��$��50��$��50��$��
10,-25,0,0q�   	ScaleIcon#��[  |�.  �� -   $��Center=�$��120,0,75,23q�.���� $��JPL Gateway��$��	0,20,0,20q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��22(��� $��	0,25,3,23q�$��Right=�$��35��$��50������ $&  Resources/Title/Queens_award.png����  �� Orientation$ 
Horizontal=�$��Right=�$��0,0,0,75q� � 	Resources  �� 
TargetType  systemButton �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value��%3  &	       �� " ��.���  � ��$ 0q���  � ��$ 35����  � ��$ 25����  � ��$ Right=���  � ��$	 Top=���  � ��
 ���� ��  � �� �� " ��.R��� #��z��$��0q�._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 �����.+��� -   $��25��#��[ .���� $��11��
���   �$
��0.75���� -   $��25������ " �� �� BasedOn# [  P�  �@J�IsMouseOver . . ���  � ��
 �  ��.���� $��25��$��35��.+��� 	 �� X1$ 12��	 �� Y1$ 8��	 �� X2$ 22��	 �� Y2$ 18��
���   �$
��0.75���� $ 12��$ 18��$ 22��$ 8��
���   �$
��0.75��� $��Bottom=�.+��  � RowDefinitions �� $	��15* �  � ColumnDefinitions  �� $��80 ��� .+� � -	   $��frame0��$��0��$��2��� -
   ! �Name$! 
sidebutton��$	��650��$	��Top=�$
��Left=�$��80�� �� $��65 ��� $��50 ��� $��5 ��� $��50 ��� $��25 ��� $��50 ��� $��5 ��� $��50 ��� $��25 ��� $��50 ��� $��5 ��� $��50 ��� $��25 ��� $��50 ��� $��5 ��� $��* �.+�(� $��1��" (� Stretch$
" None=�.���� -   $��50��$��50��  ScaleButton#��[ |�.  ���� $) #Resources/Button_Icon/Device_DB.png��(� $��3��$��70��$	��Top=�$" Uniform=�# (� StretchDirection$# DownOnly=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(����#0C2340$ �� TextAlignment$$ Center=����� % �� Path$% 	StrMain01�  	Resources& �� Source#& [ (� $��5��$
" None=�.���� -   $��50��$��50��#��[ |�.  ���� $* $Resources/Button_Icon/Support_DB.png��(� $��7��$	��Top=�$" Uniform=�$# DownOnly=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(����#0C2340$$ Center=����� $% 	StrMain02�#& [ (� $��9��$
" None=�.���� -   $��50��$��50��#��[ |�.  ���� $+ %Resources/Button_Icon/Settings_DB.png��(� $��11��$	��Top=�$" Uniform=�$# DownOnly=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(����#0C2340$$ Center=����� $% 	StrMain03�#& [ (� $��13��$
" None=�.���� -   $��50��$��50��#��[ |�.  ���� $+ %Resources/Button_Icon/Products_DB.png��(� $��15��$	��Top=�$" Uniform=�$# DownOnly=�.���� $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(����#0C2340$$ Center=����� $% 	StrMain04�#& [  � -   $��frame1��$��1��'  � NavigationUIVisibility$' Hidden=�(  � Source$( View/Nodevice_Page.xaml?� � -   $��frame2��$' Hidden=�$��2���� -   ) ��Name$) Navigation_Border��
������ $��0q�$��1��$��175,115,0,0q�$
��Left=�$	��570��$	��420��$	��Top=�._��� $��Center=�$��Center=�.+� � -   $ frame5��$' Hidden=�$y�Hidden=�