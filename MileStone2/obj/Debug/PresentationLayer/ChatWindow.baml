   M S B A M L   `   `   `  ����E  AMileStone2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:MileStone2.PresentationLayerMileStone2.PresentationLayer  5   	   1    +MileStone2.PresentationLayer.ChatRoomWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:MileStone2.PresentationLayer     5   	     � Title$  ChatRoomWindow��5   	   $	��450��6    $	��800��6-   .��5	      � .+�5
   
   �� -   5         ��Name$ 
txtMsgBody��5
      $
��Left=�6&   $��40��6A   $��300,338,0,0q�6M    �� TextWrapping$
 Wrap=�6b   $�� ��6v   $	��Top=�6~   $	��379��6�   4  .System.Windows.Automation.AutomationProperties   HelpText$ write a message��6�   �� 5   
   -   5         ��Name$ btnSend��5      $��Button��6"   $
��Left=�63   $��694,338,0,0q�6N   $	��Top=�6c   $��78��6{   $��40��6�    J� RenderTransformOrigin$ 0.514,2.188&�6�   �� 5   
   $
��Left=�6   $	��268��6-   $��
300,55,0,0q�6:   $	��Top=�6N   $	��472��6f   �� 5   
   -   5         ��Name$ 
cbSortType��5      $
��Left=�6'   $��
72,100,0,0q�6B   $	��Top=�6V   $	��160��6n    �� ItemBindingGroup 5      &   System.Windows.Data.BindingGroup  5      5      	� 5   
   -   5         	�Name$ rdnAccending��5      $��	Accending��6,   $
��Left=�6@   $��
85,143,0,0q�6[   $	��Top=�6o   6�   	� 5   
   -   5        $ rdnDeccending��5      $��
Deccending��6-   $
��Left=�6B   $��
85,164,0,0q�6]   $	��Top=�6q   $ 0.396,0.908&�6�   �� 5   
   -   5        $ cbFilterType��5      $
��Left=�6)   $��
72,238,0,0q�6D   $	��Top=�6X   $	��160��6p   �� 5   
   -   5        $ btnFilterAndSort��5      $��Filter And Sort��6+   $
��Left=�6E   $��111,379,0,0q�6`   $	��Top=�6u   $��86��6�   �� 5   
   $
��Left=�6   $��
49,288,0,0q�6/   $
��Wrap=�6C   $��
Enter User��6W   $	��Top=�6i   $ 
-2.518,1.2&�6�   �� 5   
   -   5        $ txtEnterUser��5      $
��Left=�6(   $��23��6C   $��139,281,0,0q�6O   $
 Wrap=�6d   $�� ��6x   $	��Top=�6�   $��80��6�   6�   �� 5   
   $
��Left=�6   $��	73,75,0,0q�6/   $
��Wrap=�6B   $��	Sort Type��6V   $	��Top=�6g   $��98��6   $��20��6�   $��16(�6�   �� 5   
   $
��Left=�6   $��
505,35,0,0q�6/   $
��Wrap=�6C   $��	Chat Room��6W   $	��Top=�6h   $��20��6�   $	��267��6�   ���	#FF1A14146�   �� 5   
   $
��Left=�6   $��
72,213,0,0q�6/   $
��Wrap=�6C   $��Filter Type��6W   $	��Top=�6j   $��20��6�   $	��109��6�   $��14(�6�   �� 5   
   $
��Left=�6   $��
47,325,0,0q�6/   $
��Wrap=�6C   $��Enter Group ID��6W   $	��Top=�6m   $��87��6�   �� 5   
   -	   5        $ txtEnterGroupID��5      $
��Left=�6+   $��23��6F   $��139,319,0,0q�6R   $
 Wrap=�6g   $�� ��6{   $	��Top=�6�   $��80��6�   6�   5      5      