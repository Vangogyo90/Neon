   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   E APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin 5	   	   #   PaintAdmin.AddRalColorsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Atoolkit-http://schemas.microsoft.com/wpf/2008/toolkit     5   	   L HXceed.Wpf.Toolkit, Version=2.6.0.0, Culture=neutral, PublicKeyToken=null4xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   0sys&clr-namespace:System;assembly=mscorlib  5   	   $localclr-namespace:PaintAdmin  5	   	     � Title$
  Neon��5   	   $	��450��6   $	��800��6#    � WindowStartupLocation$ CenterScreen=�6/    � 
ResizeMode$ NoResize=�6T   .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5      �� 5      $��50 �6   .+�5   
   �� -   5        $��
ColorLabel��5      $��0��6"   $��2��62   $��5��6F   �� 5   
   -   5        $	��Add��5      $��Добавить��6   $��30(�6/   $��Center=�6=   $��0��6j   $��5��6z   $��5��6�   �� 5    
   $��3��6   $��2��6!   $��HTML-цвет��61   $��Center=�6B   $��Center=�6_   $��20(�6z   6�   %  Xceed.Wpf.Toolkit.MaskedTextBox  5!   
   -   5        $
��HTML��5!      $��3��6)   $��3��66   $��2��6F   $��20(�6Z   $��Center=�6h   $��Center=�6�      Mask$ #aaaaaa��6�      
PromptChar$ #��6�   $��#��6�   6  �� 5#   
   $��1��6   $��2��6!   $��Ral-цвет��61   $��Center=�6A   $��Center=�6^   $��20(�6y   6�     5$   
   -   5        $	��Ral��5$      $��1��6(   $��3��65   $��2��6E   $��20(�6Y   $��Center=�6g   $��Center=�6�   $ RAL0000��6�   $ 0��6�   $	��RAL��6�   6�   �� 5%   
   $��2��6   $��2��6!   $��Название��61   $��Center=�6A   $��Center=�6^   $��20(�6y   6�   �� 5&   
   -   5        $
��Name��5&      $��2��6   $��3��6+   $��2��6;   $��20(�6O   $��Center=�6]   $��Center=�6x   6�   5'      5(      