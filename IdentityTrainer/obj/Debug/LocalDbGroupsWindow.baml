   M S B A M L   `   `   `  ����J  FIdentityTrainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:IdentityTrainerIdentityTrainer  5   	   )    #IdentityTrainer.LocalDbGroupsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:IdentityTrainer     5   	     � Title$  Person Groups��5   	   $	��593��6   $	��660��6,    � 
ResizeMode$ NoResize=�68   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ GridGroupModel��5
      $	��210��6+   $��
10,10,10,0q�68   $	��Top=�6L    � BindingGroup 5      &   System.Windows.Data.BindingGroup  5       � DataContext 5         IdentityTrainer.GroupModel  5      5      � 5   
   $
��Left=�6   $��10,225,0,10q�6*   $	��631��6?   .+�5      �� $��Group Management��6   $
��Left=�61   $��	0,56,0,10q�6L   $	��319��6_   .��5      � $	��194��6   $��
-6,1,-6,-6q�6$   $	��Top=�68    � ColumnDefinitions 5      �� 5      $	��17* �6+   �� 5      $��8* �6+   �� 5      $
��294* �6+   .+�5      �� $��
Group Name��6   $
��Left=�61   $��	10,10,0,0q�6L   $	��Top=�6_   $��3��6w   �� 5      -   5         ��Name$ TxtGroupName��5      $
��Left=�64   $��23��6O    �� TextWrapping$
 Wrap=�6[   $	��Top=�6o   $	��196��6�   $��	88,13,0,0q�6�   $��2��6�   �� 5      -   5         ��Name$ BtnCreateGroup��5      $��Create��65   $
��Left=�6F   $	��Top=�6a   $��75��6y   $��
209,41,0,0q�6�   $��2��6�   5      5      �� 5      $��324,56,10,10q�6   .��5       �� -   5        $ TxtLog��5       $	��249��6*   $
 Wrap=�67   $	��276��6K   5!      �� 5"      -   5        $ 	BtnDelete��5"      $��Delete Selected��6(   $
��Left=�6B   $	��Top=�6]   $	��124��6u   $��30��6�   6�   5#      5$      5%      