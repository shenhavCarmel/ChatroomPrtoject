   M S B A M L   `   `   `  ����E  AMileStone3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MileStone3.PresentationLayerMileStone3.PresentationLayer  5   	   1    +MileStone3.PresentationLayer.ChatRoomWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:MileStone3.PresentationLayer     5   	     � Title$  ChatRoomWindow��5   	   $	��450��6    $	��800��6-   .��5	      � ��5
   
   �� 5       �� ImageSource$ Pics/background.jpg��6   .+�5   
   �� -   5         ��Name$ 
txtMsgBody��5      $
��Left=�6M   $��40��6h   $��310,338,0,0q�6t    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��369��6�   4  .System.Windows.Automation.AutomationProperties   HelpText$ write a message��6�   ��6'   �� *	MsgBody+ �� Mode$ TwoWay=�6�   �� 5   
   -   5         ��Name$ btnSend��5      $
��SEND��6"   $
��Left=�61   $��694,338,0,0q�6L   $	��Top=�6a   $��78��6y   $��40��6�    J� RenderTransformOrigin$ 0.514,2.188&�6�   �� 5   
   -   5         ��Name$ 
cbSortType��5      $
��Left=�6'   $��	37,83,0,0q�6B   $	��Top=�6U   $	��251��6m   .@�5      �� $��	Timestamp��6E   	 �� 
IsSelected	 6   �� 
 �� Path$
 SorterType[0]�6E   �� 5      $��Nickname��6E   	 6   �� $
 SorterType[1]�6E   �� 5      $&�� Group ID, Nickname and Timestamp��6E   	 6   �� $
 SorterType[2]�6E   5      	� 5   
   -   5         	�Name$ rdnAccending��5   L   $��	Accending��6b   $
��Left=�6v   $��
36,124,0,0q�6�   $	��Top=�6�   
������� d� 	IsChecked 6   �� $
 SorterMode[0]�$ TwoWay=�6�   	� 5   
   -   5        $ rdnDeccending��5   L   $��
Deccending��6c   $
��Left=�6x   $��
37,144,0,0q�6�   $	��Top=�6�   
������� 6   �� $
 SorterMode[1]�$ TwoWay=�6�   �� 5   
   -   5        $ cbFilterType��5      $
��Left=�6)   $��
36,203,0,0q�6D   $	��Top=�6X   $	��160��6p   .@�5      �� $��Group��6R   	 6   �� $
 FilterType[0]�$ TwoWay=�6R   �� 5      $
��User��6R   	 6   �� $
 FilterType[1]�$ TwoWay=�6R   �� 5      $
��None��6R   	 6   �� $
 FilterType[2]�$ TwoWay=�6R   5      �� 5   
   -   5        $ 	btnFilter��5      $��Filter��6$   $
��Left=�65   $��208,237,0,0q�6P   $	��Top=�6e   $��80��6}   $��57��6�   $��18(�6�   �� 5   
   -   5        $ txtEnterUser��5      $
��Left=�6�   $��23��6�   $��115,237,0,0q�6�   $
 Wrap=�6  $	��Top=�6  $��80��6.  {�6(   �� *NicknameFilterEnabled+$ OneWay=���6a   �� *FilterNickname+$ TwoWay=� �� NotifyOnSourceUpdated .  �� NotifyOnTargetUpdated . 69  �� 5   
   $
��Left=�6   $��	36,58,0,0q�6/   $
��Wrap=�6B   $��	Sort Type��6V   $	��Top=�6g   $��98��6   $��20��6�   $��16(�6�   
�������6�   �� 5   
   $
��Left=�6   $��
36,178,0,0q�6/   $
��Wrap=�6C   $��Filter Type��6W   $	��Top=�6j   $��20��6�   $	��109��6�   $��16(�6�   
�������6�   �� 5    
   -	   5        $ txtEnterGroupID��5       $
��Left=�6�   $��23��6�   $��115,268,0,0q�6�   $
 Wrap=�6  $	��Top=�6  $��80��6/  {�6+   �� *GroupIDFilterEnabled+$ OneWay=���6c   �� *FilterGroupId+$ TwoWay=� .  . 6:  �� 5!   
   -
   5         ��Name$ lbDisplayMsgs��5!      $
��Left=�6�   $	��262��6�   $��
310,58,0,0q�6�   $	��Top=�6�   $	��462��6�   ���	#FFCDD6E8
���   �$	��0,5q�62   � UseLayoutRounding .   J� Opacity$	 0.9Q�6Z   �� SelectedValue 6[   �� *SelectedMsg+��6�   �� *DisplayedMsgs+6Z  �� 5"   
   $��	Nickname:��6   $
��Left=�6$   $��23��6?   $��
37,237,0,0q�6K   $	��Top=�6_   $��76��6w   
�������6�   �� 5#   
   $��	Group ID:��6   $
��Left=�6$   $��23��6?   $��
37,265,0,0q�6K   $	��Top=�6_   $��76��6w   
�������6�   �� 5$   
   -   5        $ 	btnLogOut��5$      $��LOGOUT��6$   $
��Left=�65   $��
36,338,0,0q�6P   $	��Top=�6d   $��75��6|   $ -3.707,10.575&�6�   $��40��6�   �� 5%   
   $�� ��6   $
��Left=�6   $	��105��66   $��196,127,0,0q�6C   $	��Top=�6X   $	��102��6p   $	 0.6Q�6|   ��5&      �� 5'      $ Pics/pandaHi2.png��6   5)      �� 5*   
   $'��!MileStone3 ChatRoom - By Group 23��6   $��Center=�6<   $��Center=�6Y   $��0,10,0,0q�6}   $	��Top=�6�   $	��792��6�   $
 0.75Q�6�   $��Italic$�6�   ��5+      �� 5,       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5-      �  � Color$ 	#FFAFB2DA��6#   � 5.      $ 	#FF5F6391��6#    � Offset$ 0.674Q�65   5/      51      �� 52   
   $��what's on your mind?��6   $
��Left=�6/   $��37��6J   $��146,343,0,0q�6V   $	��Top=�6k   $	��173��6�   ���	#FFA3A5A8$��16(�6�   $	 0.9Q�6�   53      54      