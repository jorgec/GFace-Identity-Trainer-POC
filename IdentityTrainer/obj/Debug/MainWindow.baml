   M S B A M L   `   `   `  ����J  FIdentityTrainer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:IdentityTrainerIdentityTrainer  5   	        IdentityTrainer.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:IdentityTrainer     5   	     � Title$  
MainWindow��5   	   $	��807��6   $
��1351��6)    � WindowState$ 	Maximized=�66    � WindowStartupLocation$ CenterScreen=�6N   .��5	      � .+�5
   
   +  %System.Windows.Controls.Ribbon.Ribbon  -   5          Name$ Ribbon��5
       �� SelectedIndex$ 0��6!   .@�5      .  (System.Windows.Controls.Ribbon.RibbonTab  $
��Home��6      KeyTip$ H��6&   .@�5      0  *System.Windows.Controls.Ribbon.RibbonGroup  -   5          Name$ CameraGroup��5      $��Camera��63   .@�5      1  +System.Windows.Controls.Ribbon.RibbonButton  -   5          Name$ 
RBtnCamera��5   #      LargeImageSource$ assets\icons\rec.png��67   	   Label$	 Camera��6_   
   KeyTip$
 C��6n   6y     5      -   5        $ RBtnGrab��5   #   $  assets\icons\shutter-1.png��65   $	 
Grab Frame��6c   $
 G��6v   6�     5      -   5        $ RBtnClearStream��5   #   $ assets\icons\trash.png��6<   $	 Trash Stream��6f   $
 T��6{   6�   5        5      -   5        $ APIGroup��5      $	��API��60   .@�5        -   5        $ RBtnLoadToTrainer��5   #   $( "assets\icons\cloud-computing-1.png��6>   $	 Load to Trainer��6t   $
 L��6�   6�     5      -   5        $ 	RBtnTrain��5   #   $  assets\icons\hourglass.png��66   $	 Train Group��6d   $
 T��6x   6�     5      -	   5        $ RBtnQueryService��5   #   $ assets\icons\cloud.png��6=   $	 Query Service��6g   $
 Q��6}   6�   5      5        5      $��Local DB��6   $ D��6*   .@�5        -
   5        $ LocalDbGroup��5      $��Local DB��64   .@�5        -   5        $ RBtnDbSetupGroup��5   #   $ assets\icons\folder.png��6=   $	 Groups��6h   $
 G��6w   6�     5      -   5        $ RBtnDbSetupPersons��5   #   $ assets\icons\profile.png��6?   $	 Persons��6k   $
 P��6{   6�   5      5      5      � 5   
   $
��Left=�6   $	��135��6*   $��
10,159,0,0q�67   $	��Top=�6K   $	��150��6c   �� 5   
   $��RAM Preview��6   $
��Left=�6(   $	��131��6C   $��
10,145,0,0q�6P   $	��Top=�6d   $	��150��6|   .��5       �� -   5         ��Name$ ImgRamPreview��5       $
��Left=�6+   $	��110��6F   $��0,0,-2,0q�6S   $	��Top=�6e   $	��140��6}   5!      �� 5"   
   $��Identity Setup��6   $
��Left=�6+   $	��149��6F   $��168,145,0,0q�6S   $	��Top=�6h   $	��430��6�   .��5#      � $
��Left=�6   $��
10,10,-2,0q�6.   $	��410��6B   $	��112��6N   $	��Top=�6[   .+�5$      �� -   5         ��Name$ BtnNamesReset��5$      $��Reset��60   $
��Left=�6@   $��30��6[   $��
313,77,0,0q�6g   $	��Top=�6{   $��87��6�   6�   �� 5%      -   5        $ BtnNamesSet��5%      $��	Set Names��6.   $
��Left=�6B   $��30��6]   $��
221,77,0,0q�6i   $	��Top=�6}   $��87��6�   6�   �� 5&      $��
Group Name��6   $
��Left=�6-   $��10,6,0,0q�6H   $	��Top=�6Z   �� 5'      $��Person Name��6   $
��Left=�6.   $��	14,41,0,0q�6I   $	��Top=�6\   �� 5(      -   5         ��Name$ 	CboGroups��5(      $
��Left=�6.   $��	100,6,0,0q�6I   $	��Top=�6\   $	��300��6t   $��30��6�   6�   �� 5)      -   5        $ BtnRefreshIdentities��5)      $��Refresh��67   $
��Left=�6I   $	��Top=�6d   $	��120��6|   $��	14,77,0,0q�6�   $��30��6�   6�   �� 5*      -   5        $ 
CboPersons��5*      $
��Left=�60   $��30��6K   $��
100,41,0,0q�6W   $	��Top=�6k   $	��300��6�   6�   5+      5,      �� 5-   
   $��Process Log��6   $
��Left=�6(   $	��174��6C   $��
10,299,0,0q�6P   $	��Top=�6d   $	��588��6|   .��5.      � $��	5,10,-2,1q�6    � ColumnDefinitions 5/      �� 50      $��7* �6'   �� 51      $��0* �6'   �� 52      $
��566* �6'   .+�54      �� $	��131��6   $��0,0,10,0q�6,   $	��Top=�6>   $��2��6V   $��Right=�6f   $	��553��6�   .��55      �� -   5         ��Name$ TxtTrainerLog��55       $
��Wrap=�67   $	��552��6K   56      57      58      �� 59   
   -   5         ��Name$ GrpPreviews��59      $��Current Image Stream: 0��6(   $
��Left=�6I   $	��328��6d   $��603,145,0,0q�6q   $	��Top=�6�   $	��588��6�   .��5:      �� -   5         ��Name$ ScrlPreviewPanel��5:      $
��Left=�65   $	��287��6P   $��0,10,0,0q�6]   $	��Top=�6o   $	��564��6�   .��5;      � -   5         �Name$ 
PnlPreview��5;      $	��436��60   $	��536��6=   5<      5=      5>      5?      