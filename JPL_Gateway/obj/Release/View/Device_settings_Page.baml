   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=null3clr-namespace:JPL_Gateway.ViewJPL_Gateway.View  +    %JPL_Gateway.View.Device_settings_Page   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:JPL_Gateway.View       � UseLayoutRounding  .  N� Title$  ��-  'System.Windows.Input.KeyboardNavigation   TabNavigation$
 None=� N� 
Background ��  �� ImageSource$4 ./JPL_Gateway;component/Resources/Watermark.png�� j� Stretch$ UniformToFill=� � 	Resources  )  %HorizontalSliderTrackNormalBackground �� Color % !HorizontalSliderTrackNormalBorder �� EndPoint	 �� 
StartPoint
 � Color � Offset  SliderRepeatButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType  CustomThumbForSlider  MyCustomStyleForSlider � RowDefinitions m�Name m� Fill m� 	Placement ��Name �� CornerRadius ��Name ]�Name ]� DecreaseRepeatButton �� DecreaseLarge ]� IncreaseRepeatButton �� IncreaseLarge ]� Thumb n�Name  MyButton �� Triggers P� Property  P� Value  ComboBox! d� 	IsChecked" �� Path# �� Mode$ �� RelativeSource% � ColumnDefinitions& F�Name' �� Stretch( �� 
TargetName) J� IsHitTestVisible* �� SelectionBoxItem+ �� SelectionBoxItemTemplate, ��Name- �� 
IsReadOnly. �� 
IsReadOnly/ �� IsDropDownOpen0 � AllowsTransparency1 J� SnapsToDevicePixels2 � ActualWidth3 �� MaxDropDownHeight4 K� IsItemsHost5 �� Triggers��%	  &	        &	      &	 g     &	 �     &	 �    7   7  7  7  7  &	 "    &	 *    �� $ Black���� $	 0,1&�$		 0,0&�.��� $
 	#FFE7EAEA��$	 0.1Q�� $
 White��$	 0.5Q��� " ��.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ �� " n�.���  �@�OverridesDefaultStyle . ��  � �� �� " n�.R�A� 
����������	#FF282259$��20��$��20��$��0,-1,0,0q��� " ��.���  � �� �� " ��.R��� #��z��#��z��#��z��._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��$y�Visible=�
 �   �$	 Top=�$��8��$��0��m� $ 
BottomTick��$y�Visible=�
 �   �$ Bottom=�$��8��$��0��m� $ BothTick��$y�Visible=�
 �   �$ Bottom=�$��8��$��2���� $ TrackBackground��8��  8�� $��1q�$ 1��$	��5,0q�$��Center=�$	��4.0��$��1��._��� $��-6,-1q�.+��� $ PART_SelectionRange��$y�Hidden=�$	��4.0��#��� ��#��� ��$	��1.0��]� $ 
PART_Track��$��1�� �� 8�� #��Z  �� 8�� #��Z  n� $ Thumb��8�� 
���   ��� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $��border��$��0q�
���   �#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver  . . ���  �@J�Opacity$	 0.8Q��� " ��.���  �@�Cursor$
 Hand����  � �� �� " ��.R�� .+�d� $��2��|�.  ! �� $" IsDropDownOpen�$# TwoWay=�$ �� *TemplatedParent+���� .R�� % �� $��* ��� $��40 �.+��� $ Border��$��2��$ 4�����#0C2340$��2q�F� $& Arrow��$��1�����	#FFBBBBBB$��Center=�$��Center=�"����        � 	=  	= � z     	$��7.667��$��10.833,15.333,10.833,13q�$��13.334��$
' Fill=� P�  �@J�IsMouseOver  . . ��� $( Border�� � ��
 �����P�  �@d�	IsChecked$
  trueY�. ��� $( Border�� � ��
 ������� $��ContentSite��) .  #��z* #��z+ #��z��$��3q��� $, PART_EditableTextBox��$y�Hidden=�#. z- � $��Popup��$��Bottom=�#��z/ 0 . |�.  $��Slide=�.��� $��DropDown��1 . #��z2 #��z3 .+��� $ DropDownBorder��
�������$��2q����#0C2340�� 1 . .���� 4 . 5 .��� .+��� -   6 ��Name$6 backBtn��$��22��
������ $��20,8,0,0q�|�.  $	��Top=�$
��Left=�$	��100������ " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R�� % �� $��15 ��� $	��30* �.+��� 7 ��Name$	7 btn��$��20��$��25��
������ $��0��8 J� RenderTransformOrigin$8 0.5,0.5&�.+�F� $& Line2��
�������$��1����B� ��D� E� 9 E� 
StartPoint$
9 13,0&�.���� : �� Point$: 0,10.5&��� $: 13,22&�z��� (� $��1��; (� Stretch$; Uniform=�< (� StretchDirection$< DownOnly=�.���� $��85��$��13(�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)����	#FFA0A0A0���� $" StrDevice01�  	Resources= �� Source#= [  P�  �@J�IsMouseOver  . > N� EnterActions> �� .���� .��W� ? g� Duration$? 0:0:0.05D�@ �� 
TargetName$	@ btn��A �� TargetProperty$/A )(RenderTransform).(ScaleTransform.ScaleX)�	B W� To$	B 1.1X�W� $? 0:0:0.05D�$	@ btn��$/A )(RenderTransform).(ScaleTransform.ScaleY)�$	B 1.1X�C N� ExitActionsC �� .���� .��W� $? 0:0:0.05D�$	@ btn��$/A )(RenderTransform).(ScaleTransform.ScaleX)�$B 1X�W� $? 0:0:0.05D�$	@ btn��$/A )(RenderTransform).(ScaleTransform.ScaleY)�$B 1X�P�  � {�  .  . ��� $	( btn�� �@J�Opacity$
 0.25Q�� $
��Left=�$��50��$	��Top=�$	��430��$��	26,34,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��430��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��35(�$
��Bold"����� $" StrDevice_setting01�#= [ � $
��Left=�$��50��$	��Top=�$	��700��$��	26,71,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� -   D ��Name$D 
DeviceName��$��	No Device��$	��700��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(��� -   $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�  Apply_Button#��[ $	��200��$��36��$��
469,20,0,0q�$	��Top=�$
��Left=�
�������$��15(�|�.  ���� $" StrDevice_setting02�#= [ �� -   $2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��200��$��36��$��
686,20,0,0q�$	��Top=�$
��Left=�
�������$��15(�|�.  ���� $" StrDevice_setting03�#= [ � -   E �Name$E EqGrid��$��
20,120,0,0q�$	��435��$	��350��$	��Top=�$
��Left=�.+�� $
��Left=�$��50��$	��Top=�$	��410��$��5,0,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��410��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $" StrDevice_setting04�#= [ � $
��Left=�$��25��$	��Top=�$	��130��$��5,55,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��130��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $" StrDevice_setting05�#= [ �� -   F ��Name$F 	eq_preset��$��
143,48,0,0q�#��[ G �� SelectedIndex$G 0��$	��Top=�|�.  $
��Left=�$	��276��.@��� .���� H �� Orientation$H 
Horizontal=�$��32��.+��� $	��OFF��
���   �$��18(�$	��266��I �� TextAlignment$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��266��$��32��.+��� $
��User��
���   �$��18(�$	��266��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��266��$��32��.+��� $��Communication��
���   �$��18(�$	��266��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��266��$��32��.+��� $��Movie��
���   �$��18(�$	��266��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��266��$��32��.+��� $��Music��
���   �$��18(�$	��266��$I Center=�$��Center=��� -   J ��Name$J fr100��K � Value$K 24Q�#��[ $��
58,162,0,0q�$
��Left=�$	��158��$��0��$��40��$	��Top=�L �� TickFrequency$L 12Q�M �� TickPlacement$
M Both=�N � Maximum$N 48Q�O � Minimum$O 0Q�$8 0.5,0.5&�|�.  z�V� .~��� �� �� P �� Angle$	P 270Q�T� �� -   $J fr350��$K 24Q�#��[ $��123,162,0,0q�$
��Left=�$	��158��$��0��$��40��$	��Top=�$L 12Q�$
M Both=�$N 48Q�$O 0Q�$8 0.5,0.5&�|�.  z�V� .~��� �� �� $	P 270Q�T� �� -	   $J fr1000��$K 24Q�#��[ $��184,162,0,0q�$
��Left=�$	��158��$��0��$��40��$	��Top=�$L 12Q�$
M Both=�$N 48Q�$O 0Q�$8 0.5,0.5&�|�.  z�V� .~��� �� �� $	P 270Q�T� �� -
   $J fr3500��$K 24Q�#��[ $��247,162,0,0q�$
��Left=�$	��158��$��0��$��40��$	��Top=�$L 12Q�$
M Both=�$N 48Q�$O 0Q�$8 0.5,0.5&�|�.  z�V� .~��� �� �� $	P 270Q�T� �� -   $J fr13000��$K 24Q�#��[ $��312,162,-35,0q�$
��Left=�$	��158��$��0��$��40��$	��Top=�$L 12Q�$
M Both=�$N 48Q�$O 0Q�$8 0.5,0.5&�|�.  z�V� .~��� �� �� $	P 270Q�T� �� $��+12 dB��$��
46,104,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��60��$I Right=����	#FF6C6C6C$��18��$	��Top=��� $
��0 dB��$��
46,173,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��60��$I Right=����	#FF6C6C6C$��18��$	��Top=��� $��-12dB��$��
46,242,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��60��$I Right=����	#FF6C6C6C$��18��$	��Top=�� $
��Left=�$��25��$	��Top=�$	��130��$��	5,267,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��130��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $" StrDevice_setting06�#= [ �� $��100 Hz��$��106,272,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��61��$I Center=����	#FF6C6C6C$��18��$	��Top=��� $��350 Hz��$��171,272,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��61��$I Center=����	#FF6C6C6C$��18��$	��Top=��� $��1 kHz��$��232,272,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��61��$I Center=����	#FF6C6C6C$��18��$	��Top=��� $��3.5 kHz��$��295,272,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��61��$I Center=����	#FF6C6C6C$��18��$	��Top=��� $��13 kHz��$��360,272,0,0q�$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��15(�$
��Left=�$��61��$I Center=����	#FF6C6C6C$��18��$	��Top=��� -   $6 EQ_apply��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��130��$��290,304,0,0q�$
��Left=�$��15(�|�.  $��36��$	��Top=����� $" StrDevice_setting07�#= [ � -   $E BeepGrid��$��465,120,0,0q�$	��435��$	��240��$	��Top=�$
��Left=�.+�� $
��Left=�$��50��$	��Top=�$	��410��$��5,0,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��410��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $" StrDevice_setting09�#= [ � $
��Left=�$��25��$	��Top=�$	��250��$��5,55,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $" StrDevice_setting10�#= [ � $
��Left=�$��25��$	��Top=�$	��250��$��	5,105,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $" StrDevice_setting11�#= [ � $
��Left=�$��25��$	��Top=�$	��250��$��	5,155,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��250��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��18(����� $" StrDevice_setting12�#= [ d� -   Q d�Name$Q Keytone��$��46��$��28�� 	 ToggleButton#��[	 $��332,53,57,159q�|�.  d� -   $Q Ringtone��$��46��$��28��#��[	 $��332,105,57,107q�|�.  �� -   $F 	Muteinter��$��291,148,0,0q�#��[ $G 0��$	��Top=�$
��Left=�$	��131��|�.  .@��� .���� $H 
Horizontal=�$	��121��$��32��.+��� $	��OFF��
���   �$��18(�$	��121��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��121��$��32��.+��� $��2.5 sec��
���   �$��18(�$	��121��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��121��$��32��.+��� $��5 sec��
���   �$��18(�$	��121��$I Center=�$��Center=��� .���� $H 
Horizontal=�$	��121��$��32��.+��� $��15 sec��
���   �$��18(�$	��121��$I Center=�$��Center=��� -   $6 
Beep_apply��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��130��$��36��$��291,195,0,0q�$	��Top=�$
��Left=�$��15(�|�.  ���� $" StrDevice_setting07�#= [ � -   $E SoftphoneGrid��$��20,475,0,-25q�$	��435��$	��150��$	��Top=�$
��Left=�.+�� $
��Left=�$��50��$	��Top=�$	��410��$��5,0,0,0q�.+�(� $; Uniform=�$< DownOnly=�.���� $	��410��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�$��25(����� $" StrDevice_setting08�#= [ �� -   $F Softphonelist��$��5,48,0,0q�#��[ $	��Top=�$
��Left=�$	��414��$��38��|�.  .@��� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��Avaya One-X Agent��
���   �$��15(�$	��250��$��Center=��� -   $D 	ReadyOneX��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   R ��Name$R 	CheckOneX��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��Avaya One-X Communicator��
���   �$��15(�$	��250��$��Center=��� -   $D ReadyOneXComm��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   $R CheckOneXComm��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��Avaya IX Workplace��
���   �$��15(�$	��250��$��Center=��� -   $D ReadyIXWorkplace��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   $R CheckIXWorkplace��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��Cisco Jabber��
���   �$��15(�$	��250��$��Center=��� -   $D ReadyJabber��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   $R CheckJabber��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��CounterPath Bria��
���   �$��15(�$	��250��$��Center=��� -   $D 	ReadyBria��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   $R 	CheckBria��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$
��Bria��
���   �$��15(�$	��250��$��Center=��� -   $D 
ReadyBria6��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -   $R 
CheckBria6��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� .���� $H 
Horizontal=�$	��404��$��30��.+��� $��10,0,0,0q�$��3CXPhone for Windows��
���   �$��15(�$	��250��$��Center=��� -    $D Ready3CX��$��(Not ready)��
�������$��15(�$��80��$I Right=�$��Center=��� -!   $R Check3CX��$y�Hidden=�$��10,0,0,0q�$<��6/JPL_Gateway;component/Resources/Button_Icon/Check.png��$��20��$��20���� -"   $6 Softphone_apply��$2��,/JPL_Gateway;component/Font/#Source Sans Pro)�#��[ $	��130��$��290,104,0,0q�$
��Left=�$��15(�|�.  $��36��$	��Top=����� $" StrDevice_setting07�#= [ 