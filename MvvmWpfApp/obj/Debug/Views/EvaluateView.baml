   M S B A M L   `   `   `  ����K  GRed Alert, Version=0.1.7204.26658, Culture=neutral, PublicKeyToken=null3clr-namespace:MvvmWpfApp.ViewsMvvmWpfApp.Views  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      #    MvvmWpfApp.Views.EvaluateView   5      U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:MvvmWpfApp.Views     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      .��5
      �   � RowDefinitions  5   
   �� 5      $
��100* �6   �� 5      $	��20* �6   .+�5   
   �� -   5         ��Name$ explanation��5      $
��Bold"�6(   {�.  $C��=Evaluation of k-Means for predicting place of fallen missiles��6M   $	��Top=�6�   
���   �$��0,20,0,0q�6�   $��Center=�6�   6�     LiveCharts.Wpf.PieChart  5   
   -   5          Name$ 
myPieChart��5      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�6+      	Hoverable .  $��20,60,20,10q�6�   
���   �   DataTooltip 6s   Z� 6�   �� 5   
   -   5         ��Name$ slider��5       � Maximum$
 1000Q�6!   $��50,10,50,43q�6S   $��1��6h    �� Ticks$0 *0,100,200,300,400,500,600,700,800,900,1000R�6u   	 �� IsSnapToTickEnabled	 . 
 �� TickPlacement$
 BottomRight=�6�    � 	Resources 5       �� Color��%�   (��       5   "   * SystemColors.HighlightBrushKey+)(��      5   "   *&$SystemColors.ControlDarkDarkBrushKey+)�� 5      $ Black��6T   �� 5      $ Silver��6Z   5      5      �� 5   
   -   5         ��Name$ numbersForTics��5      $���� 0                 100                200               300                400                500               600               700                 800                900            1000\n                                                                                                     Meters��6,   
�������$��50,40,43,9.667q�5   )   $��1��6A   $
��Wrap=�6N   5      5      