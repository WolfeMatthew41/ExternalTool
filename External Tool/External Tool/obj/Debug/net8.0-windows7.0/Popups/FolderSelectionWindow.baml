   M S B A M L   `   `   `  ����H  DExternal Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:External_Tool.PopupsExternal_Tool.Popups  5   	   0    *External_Tool.Popups.FolderSelectionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:External_Tool.Popups     5   	      DarkestBackgroundColor#��[  5   	     � Title$  FolderSelectionWindow��5	   	   $	��500��6'   $	��800��64   .��5
      �� $	��Top=�6   .+�5   
   �� $��Folder Path List:��6   �� 5   
   $
��Auto=�6   $
��Auto=�5      $	��300��5      .��5      �� -   5         ��Name$ PathList��5      $	��275��6(   
������ 5      5      � 5   
   $��Center=�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��5* �6#   �� 5      $��* �6#   .+�5      �� $��Folder Path: ��6   $��Center=�6-   �� 5      -   5         ��Name$ FolderPathBox��5      $��1��6-   $	��250��6=   $��Center=�6I   5      5       �� 5"   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5#      �� -   5         ��Name$ AddPathButton��5#      $	��Add��6,   $��35��6:   $��50��6F    J� Opacity$	 0.5Q�6e   �� 5$      -   5        $ RemovePathButton��5$      $��Remove��6/   $��35��6@   $��70��6L   $	 0.5Q�6n   5%      �� 5'   
   $ 
Horizontal=�6   $��Center=�6.   .+�5(      �� $��Folder List Name: ��6   $��Center=�62   �� 5*      -   5        $ FolderListNameBox��5*      $��1��61   $	��250��6A   $��Center=�6M   5+      �� 5-      -   5        $��+��5-      $��40��6!   $��Center=�6,   6G   5.      �� 50   
   $ 
Horizontal=�6   $��Center=�6.   .+�51      �� $��Folder List Selection: ��6   $��Center=�67   �� 53      -   5         ��Name$ FolderListSelector��53      $��Center=�63   $	��250��6N   54      55      �� 57   
   $ 
Horizontal=�6   $��Right=�6.   .+�58      �� -   5        $ ConfirmButton��58      $
��Okay��6,   $��35��6;   $��60��6G   $	 0.5Q�6e   �� 59      -	   5        $��Cancel��59      $��35��6&   $��70��62   6=   5:      5;      5<      