   M S B A M L   `   `   `  ����J  FIdentityTrainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:IdentityTrainerIdentityTrainer  5   	   *    $IdentityTrainer.LocalDbPersonsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:IdentityTrainer     5   	     � Title$  Persons��5   	   $	��580��6   $	��660��6&    � 
ResizeMode$ NoResize=�62   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ GridPersonModel��5
      $	��210��6,   $	��Top=�69   $
��10,0q�6Q   � 5   
   $
��Left=�6   $	��326��6*   $��
10,215,0,0q�67   $	��Top=�6K   $	��634��6c   .+�5      �� -   5         ��Name$ 	BtnDelete��5      $��Delete��6(   $
��Left=�69   $	��Top=�6T   $	��120��6l   $��30��6x   6�   �� 5      $
��Left=�6   $	��273��66   $��
323,52,0,0q�6C   $	��Top=�6W   $	��310��6o   .��5      �� -   5         ��Name$ TxtLog��5      $	��272��6*    �� TextWrapping$
 Wrap=�67   $	��291��6K   5      5      �� 5   
   $��Person Management��6   $
��Left=�6.   $��10,257,0,10q�6I   $	��309��6^   .��5      � $
��Left=�6   $	��265��6.   $	��Top=�6;   $	��299��6S   $��	0,0,-2,-3q�6_   .+�5      �� $��Group��6   $
��Left=�6(   $	��Top=�6C   $��	10,10,0,0q�6[   $��64��6n   �� 5      -   5         ��Name$ 	CboGroups��5      $
��Left=�6.   $	��Top=�6I   $	��215��6a   $��	74,14,0,0q�6m   6�   �� 5      $
��Name��6   $
��Left=�6'   $	��Top=�6B   $��	10,41,0,0q�6Z   $��64��6m   �� 5      -   5        $ TxtPersonName��5      $
��Left=�61   $��23��6L   $
 Wrap=�6X   $	��Top=�6l   $	��215��6�   $��	74,45,0,0q�6�   �� 5      -   5        $ BtnCreatePerson��5      $��Create��62   $
��Left=�6C   $	��Top=�6^   $��75��6v   $��
214,73,0,0q�6�   6�   5      5      5      5      