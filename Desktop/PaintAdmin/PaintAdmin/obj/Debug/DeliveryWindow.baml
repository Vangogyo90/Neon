   M S B A M L   `   `   `  ����E  APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin  5   	       PaintAdmin.DeliveryWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PaintAdmin     5   	     � Title$
  Neon��5   	   $	��800��6   $
��1200��6#    � WindowStartupLocation$ CenterScreen=�60   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� -   5        $��cbStatus��5      $��0��6#   $��6��60   $��20(�6D   $��Center=�6R   $��Center=�6t    �� SelectedValuePath$ IdStatusOrder��6�    �� DisplayMemberPath$ NameStatusOrder��6   &   System.Windows.Controls.DataGrid  5   
   -   5        $��DataAPI��5      $��1��6"   $��0��6/      ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6?   $��6��6O   $��3��6c      CanUserAddRows .  $��Center=�6�   $��Center=�6�      AutoGenerateColumns .  ��5      �� 5      	 �� 
TargetType)  #System.Windows.Controls.DataGridRow"	  6   .�5       �� 
 �� Property
 � �� �� Value
 ���� 63    �� Triggers 5!      �� 5"       �� Binding 6&   �� *IsDelivired+ �� Value True6F   .a�5#      �� 
 � ��
 ����6;   5$      �� 5%       6&   �� *IsPay+ True6@   .a�5&      �� 
 � ��
 �"�"�6;   5'      �� 5(       6&   �� *
IsNotPay+ True6C   .a�5)      �� 
 � ��
 �\\��6;   5*      �� 5+       6&   �� *
IsCancel+ True6C   .a�5,      �� 
 � ��
 �  ��6;   5-      �� 5.       6&   �� *
IsPickUp+ True6C   .a�5/      �� 
 � ��
 ��ح�6;   50      �� 51       6&   �� *	IsPoint+ True6B   .a�52      �� 
 � ��
 ���_�6;   53      55         Columns 57      0  *System.Windows.Controls.DataGridTextColumn  58      ,  &System.Windows.Controls.DataGridColumn   
Visibility$ Hidden=�6i   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *
IdDelivery+ �� UpdateSourceTrigger$ PropertyChanged=�6i     59      $ Hidden=�6e    6%   �� *CityId+$ PropertyChanged=�6e     5:      $ Hidden=�6e    6%   �� *UserId+$ PropertyChanged=�6e     5;      $ Hidden=�6l    6%   �� *StatusOrderId+$ PropertyChanged=�6l     5<         Header$ 
Город��6m      
IsReadOnly .  6%   �� *Cites.NameCity+$ PropertyChanged=�6|     5=      $ 
Адрес��6e    .  6%   �� *Adress+$ PropertyChanged=�6t     5>      $ Дата заказа��6c    .  6%   �� *Date+$ PropertyChanged=�6x     5?      $# Статус доставки��6~    .  6%   �� *!StatusDeliveres.NameStatusOrder+$ PropertyChanged=�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5@         Width$ 10 6)   $	 ↓��64      CellEditingTemplate 5A      �� 5B      .R�5C      ��  ��Name$ StatusDelivey��6'   -   5        $ NameStatusOrder��5C   P  $ IdStatusOrder��6v   �� Text 6\   �� *!StatusDeliveres.NameStatusOrder+ �� NotifyOnSourceUpdated .  �� Mode$ TwoWay=�$ PropertyChanged=� �� SelectedValue 6�   �� *StatusOrderId+ . $ TwoWay=�$ PropertyChanged=�6�  5D      5F        5G      $ Добавить чек��6)      CellTemplate 5H      �� 5I      .R�5J      �� $��Cheque��6%   -   5        .��5J   H   Добавить чек6V   5K      5M        5N      $ Подробнее��6)    5O      �� 5P      .R�5Q      �� $
��More��6%   -   5        .��5Q   D   Подробнее6O   5R      5T      5V      �� 5X   
   -   5        $��
Statistics��5X      $��Статистика��6#   $��20(�68   $��Center=�6F   $��0��6z   $��4��6�   $��6��6�   �� 5Y   
   -   5        $
��Exit��5Y      $��
Выйти��6   $��20(�6-   $��Center=�6;   $��0��6i   $��5��6y   $��6��6�   5Z      5[      