   M S B A M L   `   `   `  ���� F  BJPL_Gateway, Version=1.0.0.7, Culture=neutral, PublicKeyToken=nullA%clr-namespace:JPL_Gateway.View.MsgBoxJPL_Gateway.View.MsgBox  *    $JPL_Gateway.View.MsgBox.Context_menu   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     7local%clr-namespace:JPL_Gateway.View.MsgBox       N� Title$  Context_menu�� � 	Resources    MainMenuitem �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Padding ��Name	 �� Triggers
 P� Property P� Value �� 
TargetName  SubMenuitem  SubMenuParentitem � ColumnDefinitions 	 Popup �� Triggers ^� 
Conditions �� Binding �� RelativeSource �� Path �� Value��%�  &	        &	 (    &	 P    &	     �� " m�.���  � ��$ 35����  � ��$	 250����  � ��
 �������  � �� �� " m�.R��� $ Border��$ 
35,10,10,5q�$��0q�$��2,0,2,0q�._��� $ 
HeaderHost��$��Header����. 	 P� 
 �@m�IsHighlighted . . ��� $ Border�� � �� �#4C4C4C�� " m�.���  � ��$ 35����  � ��$	 200����  � ��
 �������  � �� �� " m�.R��� $ Border��$ 
35,10,10,5q�$��0q�$��2,0,2,0q�._��� $ 
HeaderHost��$��Header����. 	 P� 
 �@m�IsHighlighted . . ��� $ Border�� � �� �#4C4C4C�� " m�.���  � ��$ 35����  � ��$	 250����  � ��
 �������  � �� �� " m�.R��� $ Border��$ 
35,10,10,5q�$��0q�$��2,0,2,0q�._�� $��Center=� �� $
��Auto ��� $
��Auto �.+��� $ 
HeaderHost��$��Header����. F� $��SelectedArrow��"����     � р�[ ��[ �        	
�������$��	190,0,0,0q�	 P� 
 �@m�IsHighlighted . . ��� $ Border�� � �� �#4C4C4C�� " �.���  � �� .  ^�  ��  ��  �� *Self+$! PlacementTarget.IsMouseOver�	 False��  ��  �� *Self+$ IsMouseOver�	 False.0���  � �� .  .���� ���#333333.+��� $��3q�m� -   $
��Open��$��Open My Cool App��$
��Bold"�#��[  �� $	��240��$	��0.5��m� -   $��Options��$��Options��#��[ m� -   $
��Exit��$
��Exit��#��[  �� $��3q�� -   $��Popup��$
��Fade=�|�. $
��Left=�#��[  � HorizontalOffset$ -5�� � PlacementTarget ��  �� ElementName$ Options��.���� ���#333333
�������$	��0.9q�._��� .+��� $��3q�m� -   $��ChangeColour��$��Change Colour��#��[ m� -   $��
ChangeSize��$��Change Size��#��[ �� $��3q�