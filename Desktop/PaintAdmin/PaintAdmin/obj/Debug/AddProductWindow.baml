   M S B A M L   `   `   `  ����E  APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin  5   	   !    PaintAdmin.AddProductWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   L HXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=null4xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   $localclr-namespace:PaintAdmin  5   	     � Title$
  Neon��5	   	   $	��600��6   $
��1000��6#    � WindowStartupLocation$ CenterScreen=�60    � 
ResizeMode$ NoResize=�6U   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��50 �6   .+�5"   
   �� -   5        $��
ColorLabel��5"      $��0��6"   $��3��62   $��2��6F   �� 5#   
   -   5        $	��Add��5#      $��Добавить��6   $��30(�6/   $��Center=�6=   $��0��6j   $��8��6z   $��9��6�   �� 5%   
   -   5        $��typeApplication��5%       �� Text$! Тип применения��6*    �� SelectedValuePath$ IdTypeApplication��6@    �� DisplayMemberPath$ NameTypeApplication��6f    �� 
IsEditable . $��3��6�   $��7��6�   $��Center=�6�   6�   �� 5&   
   -   5        $��tempPulverization��5&      $7 1Температура пульверизации��6,   $ IdTempPulverization��6M   $ Degree��6u    . $��3��6�   $��4��6�   $��Center=�6�   6�   �� 5'   
   -   5        $��shine��5'      $ 
Блеск��6    $ IdShine��6-   $ 	NameShine��6I    . $��3��6y   $��5��6�   $��Center=�6�   6�   �� 5(   
   -   5        $��typeSurface��5(      $# Тип поверхности��6&   $ IdTypeSurface��6=   $ NameTypeSurface��6_    . $��3��6�   $��6��6�   $��Center=�6�   6�   �� 5)   
   -   5        $��
RalCatalog��5)      $ RAL каталог��6%   $ IdRalCatalog��68   $ ColorRal��6Y    . $��3��6�   $��3��6�   $��Center=�6�   6�   �� 5*   
   $��2��6   $��Приоритет��6!   $��Center=�62   $��Center=�6M   �� 5+   
   -   5        $��Priority��5+      $��2��6!   $��Center=�6.   $��1��6I   $��2��6Y   	 � Minimum$	 0Q�6m   
 � Maximum$	
 100Q�6y   �� 5-   
   -	   5        $��imgColor��5-      $��3��6    $��3��60   $��5��6D   �� 5.   
   -
   5        $��AddImage��5.      $-��'Добавить изображение��6!   $��20(�6@   $��Center=�6N   $��3��6�   $��5��6�   $��3��6�   �� 50   
   -   5        $��documentImage��50      $��6��6%   $��3��65   $��7��6I   $��Images/upload.png��6Z   �� 51   
   -   5        $��AddDocument��51      $'��!Добавить документ��6$   $��20(�6@   $��Center=�6N   $��6��6�   $��7��6�   $��3��6�   52      53      