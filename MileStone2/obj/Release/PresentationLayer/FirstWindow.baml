   M S B A M L   `   `   `  ���� E  AMileStone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MileStone2.PresentationLayerMileStone2.PresentationLayer  .    (MileStone2.PresentationLayer.FirstWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:MileStone2.PresentationLayer       � Title$  
MainWindow��$��385.5��$	��539��.���  J� RenderTransformOrigin$ 1.063,0.528&�����  �� ImageSource$ Pics/startBackground.jpg��.+��� -    ��Name$ btnRegister��$��Register��$
��Left=�$��377,243,0,0q�$	��Top=�$��99��$��27��$
��Bold"�$��14(��� -   $ btnLogin��$��Login��$
��Left=�$��377,158,0,0q�$	��Top=�$��99��$��65��$ -0.576,0.318&�$��20(�$
��Bold"���  J� IsHitTestVisible .  $��Enter a nickname��$��Center=�$
��Left=�$��318,79,0,324q�
�����������  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers �� 	 �� Binding	 �� *Text+
 �� ElementName$
 txtNickname�� �� Value  .a���  � y�$ Visible=��� -   $ btnExit��$
��Exit��$
��Left=�$��226,300,0,0q�$	��Top=�$��99��$ 
1.987,-0.6&�$
��Bold"��� -    ��Name$ lblEnterNickname��$��Nickname��$
��Left=�$��
58,149,0,0q�$	��Top=�$	��106��$��35��
�������$��20(��� -   $ lblEnterGroupID��$��Group ID��$
��Left=�$��
57,194,0,0q�$	��Top=�$��93��
�������$��20(��� -    ��Name$ 
tbNickname��$
��Left=�$��25��$��168,158,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��188������ *StartNickname+ �� Mode$ TwoWay=��� -   $ tbGroup��$
��Left=�$��25��$��168,198,0,0q�$
 Wrap=�$	��Top=�$	��188������ *StartGroupId+$ TwoWay=��� $ ��Not registered? Click here��$
��Left=�$��168,243,0,0q�$	��Top=�
�������$��16(��� $��MileStone        ChatRoom��$
��Left=�$��69��$��	79,55,0,0q�$	��Top=�$	��397�����	#FFE1E8F5$��48(�$��Gabriola)� J� OpacityMask
 �   �$
��Bold"�����  �� Color$ Black���� $��2��$
��Left=�$	��128��$��
235,-9,0,0q�$	��Top=�$��90�����	#FFE1E8F5$	��110(�$��Gabriola)�
 �   �
���   �