   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ComponentsAPPLICATION.Components  6<   ?$clr-namespace:APPLICATION.ConvertersAPPLICATION.Converters  5   @   $    APPLICATION.Views.SyntheseView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:APPLICATION.Views     5      ;
components$clr-namespace:APPLICATION.Components     6<   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      5
converters$clr-namespace:APPLICATION.Converters  6@     � 	Resources  5      /  )APPLICATION.Converters.ImagePathConverter   ImagePathConverter��%   &	          5   
   6(   5      .��5      �   BackgroundBrush#���  6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��2��6   .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5       "  FontAwesome.WPF.ImageAwesome    	IconBrush   
Foreground# �  6#      Icon$ Pencil=�6L   $��40��6Z   $��7q�6e   �� 5!        	TextBrush �� 
Foreground# �  6    �� FontSize$ 40(�6F   $��Synthese��6T   6g   5#      5$      �� 5%   
   $��0��6   $��2��5&      $��2��5'      $
��Wrap=�5(       �� Padding$ 4q�5)      ���#eee�� 5+      $�Expression:��6   $��12(�5,      ���#63335-      �� 5.      �� 5/      $��22(�50      �5/      �� *ExpressionSimplifier+	 �� Mode$	 OneWay=�50      51      �� 52   
   $��1��53      ��52      �� *DrawingPath+
 �� 	Converter#
 [  53      �� 54   
   $��4��55      $��4��56      $��60��57       
 Button#���  58      $��20(�59      ��5:      �� *NavigateNumeriqueCommand+ � ToolTip 5;      �� 5<      $��Retour��6   .��5>      �� $ 
Horizontal=�5?      $��Center=�5@      $	��150��5A      $��10q�5B      .+�5C        $ 	ArrowLeft=�6"   $��20��63   �� 5D      $��Retour��6   $��30,0,0,0q�6*   $��Center=�6<   $��15(�6X   5E      5G      5H      5I      