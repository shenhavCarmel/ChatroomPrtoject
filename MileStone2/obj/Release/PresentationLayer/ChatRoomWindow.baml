   M S B A M L   `   `   `  ���� E  AMileStone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MileStone2.PresentationLayerMileStone2.PresentationLayer  1    +MileStone2.PresentationLayer.ChatRoomWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:MileStone2.PresentationLayer       � Title$  ChatRoomWindow��$	��450��$	��800��.��� ����  �� ImageSource$ Pics/background.jpg��.+��� -    ��Name$ 
txtMsgBody��$
��Left=�$��40��$��310,338,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��369��4  .System.Windows.Automation.AutomationProperties   HelpText$ write a message������ *	MsgBody+ �� Mode$ TwoWay=��� -    ��Name$ btnSend��$
��SEND��$
��Left=�$��694,338,0,0q�$	��Top=�$��78��$��40�� J� RenderTransformOrigin$ 0.514,2.188&��� -    ��Name$ 
cbSortType��$
��Left=�$��	37,83,0,0q�$	��Top=�$	��251��.@��� $��	Timestamp��	 �� 
IsSelected	 �� 
 �� Path$
 SorterType[0]��� $��Nickname��	 �� $
 SorterType[1]��� $&�� Group ID, Nickname and Timestamp��	 �� $
 SorterType[2]�	� -    	�Name$ rdnAccending��$��	Accending��$
��Left=�$��
36,124,0,0q�$	��Top=�
������� d� 	IsChecked �� $
 SorterMode[0]�$ TwoWay=�	� -   $ rdnDeccending��$��
Deccending��$
��Left=�$��
37,144,0,0q�$	��Top=�
������� �� $
 SorterMode[1]�$ TwoWay=��� -   $ cbFilterType��$
��Left=�$��
36,203,0,0q�$	��Top=�$	��160��.@��� $��Group��	 �� $
 FilterType[0]�$ TwoWay=��� $
��User��	 �� $
 FilterType[1]�$ TwoWay=��� $
��None��	 �� $
 FilterType[2]�$ TwoWay=��� -   $ 	btnFilter��$��Filter��$
��Left=�$��208,237,0,0q�$	��Top=�$��80��$��57��$��18(��� -   $ txtEnterUser��$
��Left=�$��23��$��115,237,0,0q�$
 Wrap=�$	��Top=�$��80��{��� *NicknameFilterEnabled+$ OneWay=����� *FilterNickname+$ TwoWay=��� $
��Left=�$��	36,58,0,0q�$
��Wrap=�$��	Sort Type��$	��Top=�$��98��$��20��$��16(�
��������� $
��Left=�$��
36,178,0,0q�$
��Wrap=�$��Filter Type��$	��Top=�$��20��$	��109��$��16(�
��������� -	   $ txtEnterGroupID��$
��Left=�$��23��$��115,268,0,0q�$
 Wrap=�$	��Top=�$��80��{��� *GroupIDFilterEnabled+$ OneWay=����� *FilterGroupId+$ TwoWay=��� -
    ��Name$ lbDisplayMsgs��$
��Left=�$	��262��$��
310,58,0,0q�$	��Top=�$	��462�����	#FFCDD6E8
���   �$	��0,5q� � UseLayoutRounding .   J� Opacity$	 0.9Q����� *DisplayedMsgs+�� $��	Nickname:��$
��Left=�$��23��$��
37,237,0,0q�$	��Top=�$��76��
��������� $��	Group ID:��$
��Left=�$��23��$��
37,265,0,0q�$	��Top=�$��76��
��������� -   $ 	btnLogOut��$��LOGOUT��$
��Left=�$��
36,338,0,0q�$	��Top=�$��75��$ -3.707,10.575&�$��40���� $�� ��$
��Left=�$	��105��$��196,127,0,0q�$	��Top=�$	��102��$	 0.6Q����� $ Pics/pandaHi2.png���� $'��!MileStone2 ChatRoom - By Group 23��$��Center=�$��Center=�$��0,10,0,0q�$	��Top=�$	��792��$
 0.75Q�$��Italic$�����  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFAFB2DA��� $ 	#FF5F6391�� � Offset$ 0.674Q��� $��what's on your mind?��$
��Left=�$��37��$��146,343,0,0q�$	��Top=�$	��173�����	#FFA3A5A8$��16(�$	 0.9Q�