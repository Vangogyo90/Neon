   M S B A M L   `   `   `  ����E  APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin  5   	       PaintAdmin.NewsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PaintAdmin     5   	     � Title$
  Neon��5   	   $	��450��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/    � 
ResizeMode$ NoResize=�6T   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��50 �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DataAPI��5      $��0��6"   $��0��6/   $��6��6?   $��5��6S      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6d      CanUserAddRows .  $��Center=�6�   $��Center=�6�      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   
Visibility$	 Hidden=�6e   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6%   �� *IdNews+ �� UpdateSourceTrigger$ PropertyChanged=�6e     5         Header$% Название новости��6j      
IsReadOnly . 
 6%   �� *HeadingNews+$ PropertyChanged=�6�     5      $ Дата новости��6c    . 
 6%   �� *Date+$ PropertyChanged=�6y   4  .System.Windows.Controls.DataGridTemplateColumn  5       $ Подробнее��6)      CellTemplate 5!      �� 5"      .R�5#      �� $
��More��6%   -   5        .��5#   D   Подробнее6O   5$      5&        5'      $ Превью��6)    5(      �� 5)      .R�5*      �� $��Prewiew��6%   -   5        .��5*   J   Превью6R   5+      5-        5.      $ Удалить��6)    5/      �� 50      .R�51      �� $��Delete��6%   -   5        .��51   H   Удалить6Q   52      54      56      �� 58   
   -   5        $	��Add��58      $��Добавить��6   $��30(�6/   $��Center=�6=   $��0��6j   $��6��6z   $��5��6�   59      5:      