   M S B A M L   `   `   `  ����E  APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin  5   	        PaintAdmin.WareHouseWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PaintAdmin     5   	     � Title$
  Neon��5   	   $	��450��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5        $��DataAPI��5      $��0��6"   $��0��6/      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6?   $��6��6O   $��6��6c      CanUserAddRows . $��Center=�6�   $��Center=�6�      AutoGenerateColumns .     Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   
Visibility$ Hidden=�6j   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6%   �� *IdWareHouse+
 �� UpdateSourceTrigger$
 PropertyChanged=�6j     5      $ Hidden=�6e   	 6%   �� *CityId+$
 PropertyChanged=�6e     5         Header$ 
Город��6l      
IsReadOnly . 	 6%   �� *City.NameCity+$
 PropertyChanged=�6{   4  .System.Windows.Controls.DataGridTemplateColumn  5          Width$ 10 6)   $	 ↓��64      CellEditingTemplate 5!      �� 5"      .R�5#      ��  ��Name$ Cites��6'   -   5         �� DisplayMemberPath$ NameCity��5#   '   �� SelectedValuePath$ IdCity��6F   �� Text 6L   �� *City.NameCity+ �� NotifyOnSourceUpdated .  �� Mode$ TwoWay=�$
 PropertyChanged=� �� SelectedValue 6�   �� *CityId+ . $ TwoWay=�$
 PropertyChanged=�6F  5$      5&        5'      $ 
Адрес��6e   	 6%   �� *Adress+$
 PropertyChanged=�6e     5(      $ Удалить��6)      CellTemplate 5)      �� 5*      .R�5+      �� $��Delete��6%   -   5        .��5+   H   Удалить6Q   5,      5.      50      51      52      