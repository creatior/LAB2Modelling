   M S B A M L   `   `   `  ����K  GMonteCarloMethod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TriangleAreaTriangleArea  5   	       MonteCarloMethod.Task2   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TriangleArea     5   	   R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5   	     � Title$  Задание 2��5	   	   $	��600��6   $	��800��6(   .��5
      � $	��584��6   $��Center=�6    � RowDefinitions 5   
   �� 5      $
��322* �6   �� 5      $	��68* �6   �� 5      $	��65* �6   �� 5      $	��68* �6   �� 5      $	��61* �6    � ColumnDefinitions 5   
   �� 5      $
��223* �6   �� 5      $
��214* �6   �� 5      $
��363* �6   .+�5   
     OxyPlot.Wpf.PlotView  -   5          Name$ PlotView��5      $��0��6)   $��0��66   $��3��6F   �� 5   
   -   5         ��Name$ calculate_button��5      $'��!Посчитать площадь��6+   $
��Left=�6G   $	��Top=�6b   $	��122��6z   $	��380��6�   $��2��6�   $��2��6�   $��26(�6�   $��	26,32,0,0q�6�   $��3��6�   �� 5   
   -   5         ��Name$ answer_textbox��5      $��2��6,   $��2��69   $��20(�6I    �� TextAlignment$ Center=�6W   $
��Left=�6n   6�   �� 5   
   -   5        $ absolute_error_textbox��5      $��3��64   $��2��6A   $��20(�6Q   $ Center=�6_   $
��Left=�6v   6�   �� 5   
   -   5        $ relative_error_textbox��5      $��4��64   $��2��6A   $��20(�6Q   $ Center=�6_   $
��Left=�6v   6�   �� 5   
   $5��/Введите количество точек:��6   $��Center=�64   $��1��6Q   $��Center=�6^   $��16(�6y   $��32��6�   $	��207��6�   �� 5   
   -   5         ��Name$ 	N_textbox��5      $��Center=�6%    �� TextWrapping$
 Wrap=�6B   $�� ��6V   $��Center=�6^   $��1��6y   $��1��6�   $	��100��6�   $��20��6�   5      5      