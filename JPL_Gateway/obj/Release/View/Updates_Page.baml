   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  #    JPL_Gateway.View.Updates_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       N� Title$  Updates_Page�� N� 
Background
 ����� � 	Resources    Disable_Button �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  
Update_Now ��Name	 ��Name
 ��Name	 �� X1	 �� Y1	 �� X2	 �� Y2 F�Name E� 
StartPoint �� Point � Point1 � Point2 �� Triggers :� RoutedEvent	 �� To g� Duration �� 
TargetName �� TargetProperty  ComboBox d� 	IsChecked �� Path �� Mode �� RelativeSource P� Property P� Value  �� 
TargetName! J� IsHitTestVisible" �� SelectionBoxItem# �� SelectionBoxItemTemplate$ ��Name% �� 
IsReadOnly& �� 
IsReadOnly' �� IsDropDownOpen( � AllowsTransparency) J� SnapsToDevicePixels* � ActualWidth+ �� MaxDropDownHeight, K� IsItemsHost- �� Triggers & "roundcornerTextboxwithbordereffect. ��Name  Apply_Button1��%3  &	        &	 �     &	 U    &	 
    &	 �
    �� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $��Border��$ 4��$��1q�|�.  
�������._��� $��2q�$��Center=�$��Center=���. �� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 �������  �@�Cursor$
 Hand����  � �� �� " ��.R�� .+��� $ Border��$ 4��$��1q�|�.  
������ ���	#FF282259._��� $
��Left=�$��Center=�$��30,5,0,5q�$��
UPDATE NOW��$��15(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)��� $��24��$��24��$
��Left=�$��	145,0,0,0q�._��� $	 Canvas1��$��24��$��24��.+��� $
 Line1��$ 12��$	 2.5��$ 12��$ 17��
�������$��2��F� $ Line2��
�������$��2����B� ��D� E� $
 4,11&�.���� $ 10,16&�� $ 12,17.5&�$ 14,16&��� $ 20,11&�F� $ Line3��
�������$��2����B� ��D� E� $
 2,17&�.���� $
 2,18&�� $ 2.5,21&�$
 5,21&��� $ 19,21&�� $ 21.5,21&�$ 22,18&��� $ 22,17&� :� $ 
MouseEnter��.[��� .���� .���� $ WhiteX�$ 0:0:0.2D�$ Border��$ Background.Color��� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ BorderBrush.Color��� $ 	#FF282259X�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)��� $ 	#FF282259X�$ 0:0:0.2D�$ Line1��$ Stroke.Color��� $ 	#FF282259X�$ 0:0:0.2D�$ Line2��$ Stroke.Color��� $ 	#FF282259X�$ 0:0:0.2D�$ Line3��$ Stroke.Color�:� $ 
MouseLeave��.[��� .���� .���� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ Background.Color��� $ WhiteX�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)��� $ WhiteX�$ 0:0:0.2D�$ Line1��$ Stroke.Color��� $ WhiteX�$ 0:0:0.2D�$ Line2��$ Stroke.Color��� $ WhiteX�$ 0:0:0.2D�$ Line3��$ Stroke.Color��� " ��.���  � �� �� " ��.R�� .+�d� $��2��|�.   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+���� .R�� .+��� $ Border��$��2��$ 5�����	#FF282259$��2q� P�  �@J�IsMouseOver . . ��� $  Border�� � ��
 �����P�  �@d�	IsChecked$
 trueY�. ��� $  Border�� � ��
 ������� $��ContentSite��! .  #��z" #��z# #��z��$��3q��� $$ PART_EditableTextBox��$y�Hidden=�#& z% � $��Popup��$��Bottom=�#��z' ( . |�.  $��Slide=�.��� $��DropDown��) . #��z* #��z+ .+��� $ DropDownBorder��
�������$��2q����	#FF282259�� ) . .���� , . - �� " ��.���  � ��$ 10q���  � �� �� " ��.R��� $ Bd��#��z�����	#FF282259$��2q�$ 5��._��� $. PART_ContentHost���� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $��Border��$ 2��$��1q�|�.  ���	#FF282259���	#FF282259._��� $��2q�$��Center=�$��Center=���.  :� $ 
MouseEnter��.[��� .���� .���� $ WhiteX�$ 0:0:0.2D�$ Border��$ Background.Color��� $ 	#FF282259X�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)�:� $ 
MouseLeave��.[��� .���� .���� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ Background.Color��� $ WhiteX�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)�.��� .+��� -   $��
txtMessage��#��[ / �� TextWrapping$
/ Wrap=�$	��300��$��40��.���� $��574,118,26,0q�$	��320��$��44��#��[ 0 �� SelectedIndex$0 0��$	��Top=�.@��� $��44��.���� 1 �� Orientation$1 
Horizontal=�$	��310��$��40��.+��� 
���   �$��18(�$	��310��$��30��2 �� TextAlignment$2 Center=��� $�English���� $1 
Horizontal=�$	��310��$��40��.+��� 
���   �$��18(�$	��310��$��30��$2 Center=��� $�English���� 	� $
��Left=�$	��Top=�$��230,150,0,0q�	� $
��Left=�$	��Top=�$��230,198,0,0q�	� $
��Left=�$	��Top=�$��230,244,0,0q��� $��CHAT NOW��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[  $	��182��$��36��$��164,474,0,0q�$	��Top=�$
��Left=�$��15(�|�.  
��������� -   3 ��Name$3 button1��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$	��182��$��30��$
��Left=�$��164,548,0,92q�|�.  $��15(�
����������� " ��.R�� .+��� $��Border��$ 2��$��1q�|�.  ���	#FF282259���	#FF282259._��� $
��Left=�$��Center=�$��40,5,0,5q�
UPDATE NOW�� $��25��$��25��$
��Left=�$��	145,0,0,0q�._��� $?��9/JPL_Gateway;component/Resources/Button_Icon/Update_W.png�� :� $ 
MouseEnter��.[��� .���� .���� $ WhiteX�$ 0:0:0.2D�$ Border��$ Background.Color��� $ 	#FF282259X�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)�:� $ 
MouseLeave��.[��� .���� .���� $ 	#FF282259X�$ 0:0:0.2D�$ Border��$ Background.Color��� $ WhiteX�$ 0:0:0.2D�$* $(Foreground).(SolidColorBrush.Color)��� -   $3 Device_update��#��[ $	��182��$��30��$
��Left=�$	��Top=�$��552,503,0,0q�|�.  