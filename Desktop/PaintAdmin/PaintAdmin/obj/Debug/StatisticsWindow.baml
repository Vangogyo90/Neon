   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   E APaintAdmin, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PaintAdmin
PaintAdmin 5   	   !   PaintAdmin.StatisticsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5   	   *localclr-namespace:PaintAdmin     5   	     � Title$
  Neon��5	   	   $	��800��6   $
��1200��6#    � WindowStartupLocation$ CenterScreen=�60   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      �� 5      �� 5      �� 5      �� 5      .+�5   
   �� -   5        $��
cbCategory��5      $��0��6%   $��5��62   $��20(�6F   $��Center=�6T   $��Center=�6v   .@�5      �� .��6   '%Продажи за все время61   �� 5      .��6   " Продажи за неделю6.   �� 5      .��6    Продажи за месяц6-   �� 5      .��6   Продажи за год6+   5      #   LiveCharts.Wpf.CartesianChart  5    
   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=�6A   $��1��6Y   $��5��6f   $��5��6z      Series 6   �� *SeriesCollection+   AxisY 5"         LiveCharts.Wpf.Axis     Title$ Sales��6      LabelFormatter 6)   �� *
YFormatter+6Q   	   AxisX	 5%        $ Month��6   
   Labels
 6)   �� *Labels+6E   5'      5(      5)      