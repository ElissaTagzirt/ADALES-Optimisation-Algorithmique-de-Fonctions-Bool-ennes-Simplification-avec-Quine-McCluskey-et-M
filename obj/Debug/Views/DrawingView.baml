   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ConvertersAPPLICATION.Converters  6<   #    APPLICATION.Views.DrawingView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:APPLICATION.Views     5      ;
converters$clr-namespace:APPLICATION.Converters     6<   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5        � 	Resources  5
      /  )APPLICATION.Converters.ImagePathConverter   ImagePathConverter��%   &	          5   
   6(   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
�� 150 �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��1* �6   �� 5      $
��auto �6   .+�5   
   �� $��2��6   $��1��5      ��5   $   �� *DrawingPath+ �� 	Converter# [  5      �� 5   
   $��1��5      $��2��5       $��40��5!       
 Button#���  5"      $��20(�5#      ��5$      �� *GoBackCommand+ � ToolTip 5%      �� 5&      $��Retour��6   .��5(      ��  �� Orientation$ 
Horizontal=�5)      $��Center=�5*      $	��150��5+      $��10q�5,      .+�5-      "  FontAwesome.WPF.ImageAwesome     Icon$ 	ArrowLeft=�6"   $��20��63   �� 5.      $��Retour��6   $��30,0,0,0q�6+   $��Center=�6=   $��15(�6Y   5/      51      52      53      