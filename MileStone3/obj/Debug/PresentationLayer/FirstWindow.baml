   M S B A M L   `   `   `  ����E  AMileStone3, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MileStone3.PresentationLayerMileStone3.PresentationLayer  5   	   .    (MileStone3.PresentationLayer.FirstWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:MileStone3.PresentationLayer     5   	     � Title$  
MainWindow��5   	   $��385.5��6   $	��539��6+   .��5	      �  J� RenderTransformOrigin$ 1.063,0.528&�6   ��5
   
   �� 5       �� ImageSource$ Pics/startBackground.jpg��6   .+�5   
   �� -   5         ��Name$ btnRegister��5      $��Register��6&   $
��Left=�69   $��377,264,0,0q�6T   $	��Top=�6i   $��99��6�   $��27��6�   $
��Bold"�6�   $��14(�6�   �� 5   
   -   5        $ btnLogin��5      $��Login��6#   $
��Left=�63   $��377,158,0,0q�6N   $	��Top=�6c   $��99��6{   $��65��6�   $ -0.576,0.318&�6�   $��20(�6�   $
��Bold"�6�   �� 5   
    J� IsHitTestVisible .  $��Enter a nickname��6-   $��Center=�6E   $
��Left=�6`   $��318,79,0,324q�6{   
���������5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y� �� Value$ 	Collapsed=�63    �� Triggers 5      �� 5      	 �� Binding	 6&   �� *Text+
 �� ElementName$
 txtNickname�� �� Value  6X   .a�5      ��  � y�$ Visible=�6;   5      5      5      �� 5   
   -   5        $ btnExit��5      $
��Exit��6"   $
��Left=�61   $��226,300,0,0q�6L   $	��Top=�6a   $��99��6y   $ 
1.987,-0.6&�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ lblEnterNickname��5      $��Nickname��6*   $
��Left=�6=   $��
58,149,0,0q�6X   $	��Top=�6l   $	��106��6�   $��35��6�   
�������$��20(�6�   �� 5   
   -   5        $ lblEnterGroupID��5      $��Group ID��6)   $
��Left=�6<   $��
57,194,0,0q�6W   $	��Top=�6k   $��93��6�   
�������$��20(�6�   �� 5    
   -   5         ��Name$ 
tbNickname��5       $
��Left=�6R   $��25��6m   $��168,158,0,0q�6y    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��188��6�   ��6&   �� *StartNickname+ �� Mode$ TwoWay=�6�   �� 5!   
   -   5        $ tbGroup��5!      $
��Left=�6N   $��25��6i   $��168,198,0,0q�6u   $
 Wrap=�6�   $	��Top=�6�   $	��188��6�   ��6#   �� *StartGroupId+$ TwoWay=�6�   �� 5"   
   $ ��Not registered? Click here��6   $
��Left=�65   $��168,264,0,0q�6P   $	��Top=�6e   
�������$��16(�6�   �� 5#   
   $��MileStone        ChatRoom��6   $
��Left=�64   $��69��6O   $��	79,55,0,0q�6[   $	��Top=�6n   $	��397��6�   ���	#FFE1E8F5$��48(�6�   $��Gabriola)�6�    J� OpacityMask
 �   �$
��Bold"�6�   ��5$      �� 5%       �� Color$ Black��6"   5'      �� 5(   
   $��3��6   $
��Left=�6   $	��162��67   $��
235,-9,0,0q�6D   $	��Top=�6X   $��90��6p   ���	#FFE1E8F5$	��110(�6�   $��Gabriola)�6�   
 �   �
���   �6�   �� 5)   
   -   5        $ lblEnterPassword��5)      $��Password��6*   $
��Left=�6=   $��
58,235,0,0q�6X   $	��Top=�6l   $��92��6�   ���	#FFF0EFEF$��20(�6�   G� 5*   
   -	   5        $
��Left=�5*   D   $��168,239,0,0q�6_   $	��Top=�6t   $	��188��6�   $��25��6�   5+      5,      