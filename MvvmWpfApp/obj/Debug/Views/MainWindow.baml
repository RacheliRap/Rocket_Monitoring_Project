   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   K GRed Alert, Version=0.1.7204.26658, Culture=neutral, PublicKeyToken=null3clr-namespace:MvvmWpfApp.ViewsMvvmWpfApp.Views 5   	   =#clr-namespace:MvvmWpfApp.ViewModelsMvvmWpfApp.ViewModels 5   	   5clr-namespace:MvvmWpfApp.ModelsMvvmWpfApp.Models 5	   	   'clr-namespace:MvvmWpfApp
MvvmWpfApp 5   	   !   MvvmWpfApp.Views.MainWindow   5   	   U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:MvvmWpfApp.Views     5   	   8localVMs#clr-namespace:MvvmWpfApp.ViewModels     5   	   7localModelsclr-namespace:MvvmWpfApp.Models     5	   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5
   	   R NMvvmDialogs, Version=5.2.2.0, Culture=neutral, PublicKeyToken=3d3b9708589bc73e8md/https://github.com/fantasticfiasco/mvvm-dialogs  5   	   V	 RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes 	 5   	   E
 ADragablz, Version=0.0.3.203, Culture=neutral, PublicKeyToken=null6dragablz'http://dragablz.net/winfx/xaml/dragablz 
 5   	   )
mvvmWpfAppclr-namespace:MvvmWpfApp 
 5   	   $  MvvmDialogs.DialogServiceViews    IsRegistered  .    MaterialDesignPaper#���   5   	     MaterialDesignFont#���  5   	    � Icon$ emergency.ico��5   	    � WindowState$ 	Maximized=�5   	    � Title 5   	   �� *Title+ � 	Resources 5        SampleTemplate��%2   &	       �� 5   
   .R�5      �� 5      5       J� InputBindings 5      �� 5!   
   
 �� Key$ S��61    �� 	Modifiers$
 Ctrlf�69    �� Command 6   �� *	SaveCmd+69   �� 5"   
   	 �� Gesture$	 Ctrl+Shift+S��63    6   �� *	SaveAsCmd+63   �� 5#   
   $	 Ctrl+O��61    6   �� *	OpenCmd+61   �� 5$   
   $	 Ctrl+N��60    6   �� *NewCmd+60   �� 5%   
   $	 Alt+F4��61    6   �� *	ExitCmd+61   .��5(      ]� .+�5*   
   � $	��Top=�6     PrimaryHueMidBrush#���  6%   
 � ColumnDefinitions
 5+      �� 5,      $
��Auto �6#   �� 5-      $
��Auto �6#   .+�5/      �� $��0��6    �� Orientation$ 
Horizontal=�6)   $	��Top=�6B   .+�50      �� -   5        
 J� Uid$ 0��50      $	��150��6!   $��CONTROL ROOM��6-   $��40��6D    ! PrimaryHueDarkForegroundBrush#��[ 6}   ��6P   Z� ��6f   Z� 6�   �� 51      -   5        $ 1��51      $	��150��6!   $��ANALYTIC��6-   $��40��6@   #��[ 6y   ��6L   Z� ��6b   Z� 6�   52      � 53      -   5         �Name$ 
GridCursor��53   #   $��0��6   $	��150��67   $��5��6C   $��Bottom=�6N   #��[ 6i   $
��Left=�6�   5?      ]� 5A   
   .+�5C      (  "MvvmWpfApp.Views.NewReportFormView  -   5          Name$ ReportFormView��5C   &      ReportFormVm 6>   �� *NewReportFormVm+  MvvmWpfApp.MapView  5D      -   5          Name$ MapView��5D   !      MapVm 62   �� *MapVm++  %MvvmWpfApp.Views.ChooseExplosionsView  5E      -   5          Name$ ChooseExplosionsView��5E   )   $y�	Collapsed=�6y      ChooseExplosionsVm 6G   �� *ChooseExplosionsVm+6y     MvvmWpfApp.GraphView  5F      -   5          Name$ 	GraphView��5F   #   $y�	Collapsed=�6R      GraphVm 66   �� *	GraphVm+6R   5G      5H      5I      