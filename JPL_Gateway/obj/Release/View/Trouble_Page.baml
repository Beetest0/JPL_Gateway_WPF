   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  #    JPL_Gateway.View.Trouble_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View     -     � UseLayoutRounding  .  N� Title$ Trouble_Page��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources    ComboBox �� 
TargetType �� Property	 �� Value
 �� 
TargetType d� 	IsChecked �� Path �� Mode �� RelativeSource ��Name �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate ��Name �� 
IsReadOnly �� 
IsReadOnly �� IsDropDownOpen � AllowsTransparency J� SnapsToDevicePixels � ActualWidth �� MaxDropDownHeight  K� IsItemsHost��%B  &	        '�� �    �� " ��.���  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� .+�d� $��2��|�.   �� $ IsDropDownOpen�$ TwoWay=� �� *TemplatedParent+���� .R�� .+��� $ Border��$��2��$ 4�����#0C2340$��2q� P�  �@J�IsMouseOver . . ��� $ Border�� � ��
	 �����P�  �@d�	IsChecked$
 trueY�. ��� $ Border�� � ��
	 ������� $��ContentSite��$��Center=�$��Center=� .  #��z #��z #��z���� $ PART_EditableTextBox��$y�Hidden=�# z � $��Popup��$��Bottom=�#��z  . |�.  $��Slide=�.��� $��DropDown�� . #��z #��z .+��� $ DropDownBorder��
�������$��2q����#0C2340��  . .����   . �� " ��.���  � ��$	 33����  � ��$	 Center=���  � ��$	 Center=�.��� .+��� -   ! ��Name$! backBtn��$��22��
������ $��20,8,0,0q�|�.  $	��Top=�$
��Left=�$	��100������ " ��.���  �@�OverridesDefaultStyle	 . ��  �@�Cursor$
	 Hand����  � ��	 �� "
 ��.R�� " � ColumnDefinitions" �� $��15 ��� $	��30* �.+��� # ��Name$	# btn��$��20��$��25��
������ $��0��$ J� RenderTransformOrigin$$ 0.5,0.5&�.+�F� % F�Name$% Line2��
�������$��1����B� ��D� E� & E� 
StartPoint$
& 13,0&�.���� ' �� Point$' 0,10.5&��� $' 13,22&�z��� (� $��1��( (� Stretch$( Uniform=�) (� StretchDirection$) DownOnly=�.���� $��85��$��13(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����	#FFA0A0A0���� $ StrDevice01�  	Resources* �� Source#* [  P�  �@J�IsMouseOver . + N� EnterActions+ �� .���� .��W� , g� Duration$, 0:0:0.05D�- �� 
TargetName$	- btn��. �� TargetProperty$/. )(RenderTransform).(ScaleTransform.ScaleX)�	/ W� To$	/ 1.1X�W� $, 0:0:0.05D�$	- btn��$/. )(RenderTransform).(ScaleTransform.ScaleY)�$	/ 1.1X�0 N� ExitActions0 �� .���� .��W� $, 0:0:0.05D�$	- btn��$/. )(RenderTransform).(ScaleTransform.ScaleX)�$/ 1X�W� $, 0:0:0.05D�$	- btn��$/. )(RenderTransform).(ScaleTransform.ScaleY)�$/ 1X�P�  � {� .  . ��� $	 btn�� �@J�Opacity$
	 0.25Q�� $
��Left=�$��50��$	��Top=�$	��430��$��	26,34,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��430��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"����� $ StrTrouble01�#* [ � $
��Left=�$��50��$	��Top=�$	��700��$��	26,71,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $ StrTrouble02�#* [ � $
��Left=�$��30��$	��Top=�$	��300��$��
26,137,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��300��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrTrouble04�#* [ �� -   $! Speaker_test��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�  Apply_Button#��[ $	��190��$��36��$��
25,177,0,0q�$	��Top=�$
��Left=�$��15(�|�.  ���� $ StrTrouble04�#* [ �� -   1 ��Name$1 	comboBox1��{�. $��312,177,179,0q�#��[  2 �� SelectedIndex$2 0��$	��Top=�|�.  $��36��3 � Cursor$
3 Hand��� -   4 �Name$4 progressBar1��$
��Left=�$��15��$��312,227,0,0q�$	��Top=�$	��429��
����������	#FF282259��Z� � $
��Left=�$��30��$	��Top=�$	��230��$��
26,220,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��230��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����� $ StrTrouble05�#* [ � $
��Left=�$��30��$	��Top=�$	��300��$��
26,316,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��300��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��20(����� $ StrTrouble06�#* [ �� -   $! mic_test��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��190��$��36��$��
25,355,0,0q�$	��Top=�$
��Left=�$��15(�|�.  ���� $ StrTrouble07�#* [ �� -   $1 	comboBox2��{�. $��312,355,179,0q�#��[  $2 0��$	��Top=�|�.  $��36��� -   $4 progressBar2��$
��Left=�$��15��$��312,405,0,0q�$	��Top=�$	��429��
����������	#FF282259��Z� � $
��Left=�$��30��$	��Top=�$	��230��$��
26,398,0,0q�.+�(� $( Uniform=�$) DownOnly=�.���� $	��230��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(����� $ StrTrouble08�#* [ �� 
�������$��2q�$��1��$��
26,468,0,0q�$	��Top=�$
��Left=�$	��868��