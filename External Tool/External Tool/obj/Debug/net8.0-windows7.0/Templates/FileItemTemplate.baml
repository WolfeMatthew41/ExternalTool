   M S B A M L   `   `   `  ����H  DExternal Tool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:External_Tool.TemplatesExternal_Tool.Templates  5      .    (External_Tool.Templates.FileItemTemplate   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      7local%clr-namespace:External_Tool.Templates     5      .��5      � $��Stretch=�6     � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ FileExtensionText��5      $��60(�6/   $��.Temp��6=   �� 5   
   -   5        $ NewText��5      $	��New��6%   $��1��60   $��40(�6=   
���  ��$
��Bold"�6\   �� 5   
   $��1��6   $��Center=�6%   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Current Name: ��6   $��30(�62   �� 5      -   5        $ CurrentFileName��5      $
��Temp��65   $��20(�6A   $��Center=�6O   5      �� 5      $ 
Horizontal=�6   .+�5      �� $��Base Name: ��6   $��30(�6/   �� 5      -   5         ��Name$ BaseFileNameBox��5      $
��Temp��63   $	��200��6?   $��15(�6K   $��Center=�6Y   5       5!      �� 5#   
   $��1��6   $��1��6%   $��Center=�62   .+�5$      �� $��Naming Rule:��6   $��20(�6,   �� 5%      -   5         ��Name$ NamingRuleList��5%      5&      5)      5*      