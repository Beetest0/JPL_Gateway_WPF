   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=nullA%clr-namespace:JPL_Gateway.View.MsgBoxJPL_Gateway.View.MsgBox  %    JPL_Gateway.View.MsgBox.Upgrade   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:JPL_Gateway.View.MsgBox       N� Title$   �� � 	Resources    FilledColor �� A �� B �� R �� G  UnfilledColor  BusyAnimationStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 � 	Resources  Key g� 	BeginTime g� RepeatBehavior �� 
TargetName �� TargetProperty �� KeyTime �� Value �� Triggers P� Property P� Value N� EnterActions ��Name  
Animation0  
Animation1  
Animation2  
Animation3  
Animation4 	 
Animation5 
 
Animation6  
Animation7 N� ExitActions �� BeginStoryboardName A�Name �� Left
 �� Top �� Path��%�
  &	        &	 -     &	 X     7   7  7   7  7   7  7   7  7   7  7   7  7   7  7   7  7  7  7  7  7  7 	 7 
 7  �� $	 255��$	 155��$	 155��$	 155���� $ 0��$	 155��$	 155��$	 155���� " ��.���  � ��
 ���� ��  � �� �� "	 ��
 ���� 
Animation0 $ 
00:00:00.0X�$ Forever��.���� $ ellipse0��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8   �� $ 
00:00:01.6��8  �� 
Animation1 $ 
00:00:00.2X�$ Forever��.���� $ ellipse1��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8  �� 
Animation2 $ 
00:00:00.4X�$ Forever��.���� $ ellipse2��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8  �� 
Animation3 $ 
00:00:00.6X�$ Forever��.���� $ ellipse3��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8  �� 
Animation4 $ 
00:00:00.8X�$ Forever��.���� $ ellipse4��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8 	 �� 
Animation5 $ 
00:00:01.0X�$ Forever��.���� $ ellipse5��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8 
 �� $ 
00:00:01.6��8  �� 
Animation6 $ 
00:00:01.2X�$ Forever��.���� $ ellipse6��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8  �� 
Animation7 $ 
00:00:01.4X�$ Forever��.���� $ ellipse7��$* $(Shape.Fill).(SolidColorBrush.Color)�.g��� $ 
00:00:00.0��8  �� $ 
00:00:01.6��8   P�  �@J�	IsVisible .  �� $ Storyboard0��8�� �� $ Storyboard1��8�� �� $ Storyboard2��8�� �� $ Storyboard3��8�� �� $ Storyboard4��8�� �� $ Storyboard5��8�� �� $ Storyboard6��8�� �� $ Storyboard7��8��  �� $ Storyboard0���� $ Storyboard1���� $ Storyboard2���� $ Storyboard3���� $ Storyboard4���� $ Storyboard5���� $ Storyboard6���� $ Storyboard7��.R��� #��z��#��z��#��z��._�� .+��� $��60��$��60�� ���� 'A�       " A�.���  � ��$ 15����  � ��$ 15����  � �� �	#009B9B9B.+�A� $ ellipse0��$
 1.75��$ 21��A� $ ellipse1��$ 7��$	 6.5��A� $ ellipse2��$
 20.5��$
 0.75��A� $ ellipse3��$ 34.75��$
 6.75��A� $ ellipse4��$
 40.5��$ 20.75��A� $ ellipse5��$ 34.75��$
 34.5��A� $ ellipse6��$ 20.75��$ 39.75��A� $ ellipse7��$ 34.25��$ 7��A� $
��39.5��$
��39.5��$
 8.75��$ 8��$y�Hidden=��� $��Center=�$��Center=����� $
 Text�.���� -    ��Name$ 
MainBorder��$��10q� �� CornerRadius$ 8��$��0q�
���   � �� Padding$ 0q� � Triggers :�   :� RoutedEvent$  Window.Loaded��.[��� .���� .��W� $ 
MainBorder��$ Opacity�! W� From$! 0X�	" W� To$" 1X�# g� Duration$# 0:0:0.5D�._�� $ � RowDefinitions$ �� $	��180 �% � ColumnDefinitions% �� $	��400 �.+��� -   $
��Mask��$ 8��
�������� -   & �Name$
& Grid��
�������' J� OpacityMask' !� ( !� Visual( �� ) �� ElementName$
) Mask��.+��� -   $��
StackPanel��.+�]� -   $��ContentHost��$��0,10,0,0q�$��34��.+��� $2��,/JPL_Gateway;component/Resources/Updates.png��$��32��]� $��	0,15,0,10q�$��55��.+�(� * (� Stretch$* Uniform=�+ (� StretchDirection$+ DownOnly=�.���� -   , ��Name$, 	updatemsg��$��23(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�- �� TextAlignment$- Center=�$	��400������ $ StrPopup_update07�  	Resources. �� Source#. [ � $��40��.+��� -   / ��Name$/ loading��#��[ �� -   0 ��Name$0 finish��$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$y�Hidden=��� -	   1 ��Name$1 okBtn��$	��130��$��36��$��OK��$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$y�Hidden=�  Apply_Button#��[ 