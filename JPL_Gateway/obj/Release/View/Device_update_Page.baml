   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  )    #JPL_Gateway.View.Device_update_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources    
Update_Now �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name �� CornerRadius � ColumnDefinitions (� Stretch (� StretchDirection �� TextAlignment �� Path  	Resources �� Source ��Name ��Name	 �� X1	 �� Y1	 �� X2	 �� Y2 F�Name E� 
StartPoint �� Point � Point1 � Point2 �� Triggers :� RoutedEvent	  �� To! g� Duration" �� 
TargetName# �� TargetProperty  
Up_to_date  FilledColor$ �� A% �� B& �� R' �� G  UnfilledColor  BusyAnimationStyle( � 	Resources  Key) g� 	BeginTime* g� RepeatBehavior+ �� KeyTime, �� Value- P� Property. P� Value/ N� EnterActions0 ��Name  
Animation0  
Animation1 	 
Animation2 
 
Animation3  
Animation4  
Animation5  
Animation6  
Animation71 N� ExitActions2 �� BeginStoryboardName3 A�Name4 �� Left
5 �� Top  ComboBox6 d� 	IsChecked7 �� Mode8 �� RelativeSource9 �� Stretch: �� 
TargetName; ��Name< J� IsHitTestVisible= �� SelectionBoxItem> �� SelectionBoxItemTemplate? ��Name@ �� 
IsReadOnlyA �� 
IsReadOnlyB �NameC �� IsDropDownOpenD � AllowsTransparencyE �NameF J� SnapsToDevicePixelsG � ActualWidthH �� MaxDropDownHeightI K� IsItemsHost��%�  &	        7  &	     7  &	 �    &	 	    &	 :	    7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7 	 7 
 7  7  7  7  &	     '�� �    �� " ��.���  �@�OverridesDefaultStyle	 . ��  � ��
	 �������  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� .+��� $ Border��$ 4��$��1q�|�.  
������ ���#0C2340._��  �� $��* ��� $��30 �.+�(� $��0��$ Uniform=�$ DownOnly=�.���� $	��105��$ Center=�$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����� $ StrDevice_update10�8   �� $��1��$��24��$��24��$��0,0,0,0q�._��� $ Canvas1��$��24��$��24��.+��� $ Line1��$ 12��$	 2.5��$ 12��$ 17��
�������$��2��F� $ Line2��
�������$��2����B� ��D� E� $
 4,11&�.���� $ 10,16&�� $ 12,17.5&�$ 14,16&��� $ 20,11&�F� $ Line3��
�������$��2����B� ��D� E� $
 2,17&�.���� $
 2,18&�� $ 2.5,21&�$
 5,21&��� $ 19,21&�� $ 21.5,21&�$ 22,18&��� $ 22,17&� :� $ 
MouseEnter��.[��� .���� .���� $  WhiteX�$! 0:0:0.2D�$" Border��$# Background.Color��� $  #0C2340X�$! 0:0:0.2D�$" Border��$# BorderBrush.Color��� $  #0C2340X�$! 0:0:0.2D�$*# $(Foreground).(SolidColorBrush.Color)��� $  #0C2340X�$! 0:0:0.2D�$" Line1��$# Stroke.Color��� $  #0C2340X�$! 0:0:0.2D�$" Line2��$# Stroke.Color��� $  #0C2340X�$! 0:0:0.2D�$" Line3��$# Stroke.Color�:� $ 
MouseLeave��.[��� .���� .���� $  #0C2340X�$! 0:0:0.2D�$" Border��$# Background.Color��� $  TransparentX�$! 0:0:0.2D�$" Border��$# BorderBrush.Color��� $  WhiteX�$! 0:0:0.2D�$*# $(Foreground).(SolidColorBrush.Color)��� $  WhiteX�$! 0:0:0.2D�$" Line1��$# Stroke.Color��� $  WhiteX�$! 0:0:0.2D�$" Line2��$# Stroke.Color��� $  WhiteX�$! 0:0:0.2D�$" Line3��$# Stroke.Color��� " ��.���  �@�OverridesDefaultStyle	 . ��  � {�	 .  ��  � ��	 �� "
 ��.R��  �� $��* ��� $��30 �.+��� $ Border��$��0��|�.  ._�(� $ Uniform=�$ DownOnly=�.���� $	��115��$ Center=�$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$
��Bold"����� $ StrDevice_update07�8   �� $��1��$��25��$��25��$��0,0,0,0q�._��� $<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png���� $	$ 255��$	% 155��$	& 155��$	' 155���� $$ 0��$	% 155��$	& 155��$	' 155���� " ��.���  � ��
	 ���� ��  � ��	 �� "
 ��( ���� 
Animation0 $) 
00:00:00.0X�$* Forever��.���� $" ellipse0��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,   �� $+ 
00:00:01.6��8,  �� 
Animation1 $) 
00:00:00.2X�$* Forever��.���� $" ellipse1��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8,  �� 
Animation2 $) 
00:00:00.4X�$* Forever��.���� $" ellipse2��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8,  �� 
Animation3 $) 
00:00:00.6X�$* Forever��.���� $" ellipse3��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8,  �� 
Animation4 $) 
00:00:00.8X�$* Forever��.���� $" ellipse4��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8, 	 �� 
Animation5 $) 
00:00:01.0X�$* Forever��.���� $" ellipse5��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8, 
 �� $+ 
00:00:01.6��8,  �� 
Animation6 $) 
00:00:01.2X�$* Forever��.���� $" ellipse6��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8,  �� 
Animation7 $) 
00:00:01.4X�$* Forever��.���� $" ellipse7��$*# $(Shape.Fill).(SolidColorBrush.Color)�.g��� $+ 
00:00:00.0��8,  �� $+ 
00:00:01.6��8,   P� - �@J�	IsVisible. . / �� $0 Storyboard0��8�� �� $0 Storyboard1��8�� �� $0 Storyboard2��8�� �� $0 Storyboard3��8�� �� $0 Storyboard4��8�� �� $0 Storyboard5��8�� �� $0 Storyboard6��8�� �� $0 Storyboard7��8�� 1 �� $2 Storyboard0���� $2 Storyboard1���� $2 Storyboard2���� $2 Storyboard3���� $2 Storyboard4���� $2 Storyboard5���� $2 Storyboard6���� $2 Storyboard7��.R��� #��z��#��z��#��z��._�� .+��� $��60��$��60�� ���� 'A�       " A�.���  � ��$	 15����  � ��$	 15����  � ��	 �	#009B9B9B.+�A� $3 ellipse0��$
4 1.75��$5 21��A� $3 ellipse1��$5 7��$	4 6.5��A� $3 ellipse2��$
4 20.5��$
5 0.75��A� $3 ellipse3��$4 34.75��$
5 6.75��A� $3 ellipse4��$
4 40.5��$5 20.75��A� $3 ellipse5��$4 34.75��$
5 34.5��A� $3 ellipse6��$4 20.75��$5 39.75��A� $3 ellipse7��$5 34.25��$4 7��A� $
��39.5��$
��39.5��$
4 8.75��$5 8��$y�Hidden=��� $��Center=�$��Center=����� $
 Text��� " ��.���  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� .+�d� $��2��|�.  6 �� *IsDropDownOpen+$7 TwoWay=�8 �� *TemplatedParent+���� .R��  �� $��* ��� $��35 �.+��� $ Border��$��2��$ 4�����#0C2340$��2q�F� $ Arrow��$��1�����	#FFBBBBBB$��Center=�$��Center=�"����        � 	=  	= � z     	$��7.667��$��10.833,15.333,10.833,13q�$��13.334��$
9 Fill=� P� - �@J�IsMouseOver. . . ��� $: Border�� � ��
	 �����P� - �@d�	IsChecked$
. trueY�. ��� $: Border�� � ��
	 ������� $; ContentSite��$��0,0,20,0q�$��Center=�$��Center=�< .  #��z= #��z> #��z���� $? PART_EditableTextBox��$y�Hidden=�#A z@ � $B Popup��$��Bottom=�#��zC D . |�.  $��Slide=�.��� $E DropDown��F . #��zG #��zH .+��� $ DropDownBorder��
�������$��2q����#0C2340�� F . .���� I . �� " ��.���  � ��$	 33����  � ��$	 Center=���  � ��$	 Center=�.��� .+� � -   J  �Name$J frame1���� -   K ��Name$K backBtn��$	��100��$��22��
������ $��20,8,0,0q�$	��Top=�$
��Left=�|�.  ���� " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R��  �� $��15 ��� $	��30* �.+��� $	 btn��$��20��$��25��
������ $��0��L J� RenderTransformOrigin$L 0.5,0.5&�.+�F� $ Line2��
�������$��1����B� ��D� E� $
 13,0&�.���� $ 0,10.5&��� $ 13,22&�z��� (� $��1��$ Uniform=�$ DownOnly=�.���� $��85��$��13(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����	#FFA0A0A0���� $ StrDevice01�# [  P� - �@J�IsMouseOver. . / �� .���� .��W� $! 0:0:0.05D�$	" btn��$/# )(RenderTransform).(ScaleTransform.ScaleX)�	M W� To$	M 1.1X�W� $! 0:0:0.05D�$	" btn��$/# )(RenderTransform).(ScaleTransform.ScaleY)�$	M 1.1X�1 �� .���� .��W� $! 0:0:0.05D�$	" btn��$/# )(RenderTransform).(ScaleTransform.ScaleX)�$M 1X�W� $! 0:0:0.05D�$	" btn��$/# )(RenderTransform).(ScaleTransform.ScaleY)�$M 1X�P� - � {�. .  . ��� $	: btn�� �@J�Opacity$
	 0.25Q�� $
��Left=�$��50��$	��Top=�$	��430��$��	26,34,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��430��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"����� $ StrDevice_update01�# [ � $
��Left=�$��50��$	��Top=�$	��700��$��	26,71,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   N ��Name$N 
DeviceName��$��	No Device��$	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(�� $
��Left=�$��50��$	��Top=�$	��250��$��
26,136,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrDevice_update02�# [ � $
��Left=�$��50��$	��Top=�$	��280��$��
26,196,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $N currentversion��$
��####��$	��280��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(�� $
��Left=�$��50��$	��Top=�$	��250��$��332,136,0,0q�.+�(� $ Uniform=�$ DownOnly=�$��1,0,-1,0q�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrDevice_update03�# [ � $
��Left=�$��50��$	��Top=�$	��280��$��319,196,0,0q�.+�(� $ Uniform=�$ DownOnly=�$��
13,0,-13,0q�.���� -   $N latestversion��$
��####��$	��280��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(�$
��Bold"�� $
��Left=�$��50��$	��Top=�$	��250��$��624,136,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrDevice_update04�# [ � $
��Left=�$��50��$	��Top=�$	��220��$��624,196,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��220��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $ StrDevice_update05�# [ �� $
��Left=�$��29��$��855,205,0,0q�$	��Top=�$��23��$A��;/JPL_Gateway;component/Resources/Button_Icon/Padlock_DB.png��� $
��Left=�$��50��$	��Top=�$	��500��$��
26,261,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��500��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrDevice_update06�# [ � $
��Left=�$��25��$	��Top=�$	��220��$��
26,328,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� -   $N Update_device_name��$��#####��$	��220��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(�$
��Bold"�� -   $��UpdateInfo_grid��$��
47,369,0,0q�$
��Left=�$	��481��$	��192��$	��Top=�.+�� $
��Left=�$��25��$	��Top=�$	��200��$��5,0,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(����	#FF858585���� $ StrDevice_update09�# [ �� -   O ��Name$O lbUpdateInfo��$��0,18,0,2q�$y�Visible=�$�� ���� -	   $K FirmwareBtn��#��[  $	��140��$��35��$
��Left=�$��410,324,0,0q�|�.  $	��Top=��� 
���   �$��0q�$
��Left=�$	��310��$	��310��$��561,311,0,0q�$	��Top=�._��� -
   P ��Name$P Device_image��$7��1/JPL_Gateway;component/Resources/Device/NOUSB.png��� $
��Left=�$��25��$	��Top=�$	��200��$��
31,347,0,0q�.+�(� $ Uniform=�$ DownOnly=�.���� $	��200��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��13(�$
��Bold"����	#FFFD0000���� $ StrDevice_update08�# [ �� -   Q ��Name$Q 	softphone��$��262,324,0,0q�R �� SelectedIndex$R 0��#��[ $	��Top=�$
��Left=�$	��131��$��35��|�.  