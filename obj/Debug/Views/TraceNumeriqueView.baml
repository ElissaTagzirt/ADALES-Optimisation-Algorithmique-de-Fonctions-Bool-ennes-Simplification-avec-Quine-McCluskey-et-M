   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ComponentsAPPLICATION.Components  5      *    $APPLICATION.Views.TraceNumeriqueView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:APPLICATION.Views     5      ;
components$clr-namespace:APPLICATION.Components     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5	      .��5      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $	��150��6   $��60��5      $��0��5      $
��Left=�5       
  Button#���   5      $��20(�5      ��5      �� *GoBackCommand+ � ToolTip 5      �� 5      $��Retour��6    .��5!      �� $ 
Horizontal=�6   $��Center=�5"      $	��150��5#      $��10q�5$      .+�5%      "  FontAwesome.WPF.ImageAwesome     Icon$ 	ArrowLeft=�6&   $��20��67   �� 5&      $��Retour��6    $��30,0,0,0q�6.   $��Center=�6@   $��15(�6\   5'      5(      �� 5*        	TextBrush#���  5+      $��40(�5,      $��10 0 0 0q�5-      ��5*      �� *Header+ �� Mode$ OneWay=�5-      5.      �� 50   
   $��1��6   $��14(�51      $��0 5 0 0q�52      �� 53      $�Expression: ��6   �� 6(   �6,   �� *OriginalExpression+$ OneWay=�54      �� 56   
   $��3��6   $��0 10 0 0q�57      $
��Auto=�58      $��Disabled=�59      .��5:      ]� .+�5<      �� $	��Top=�6   $��0 0 0 1q�5=      
���   �._�5?      -   'APPLICATION.Components.StepEntryControl  5@      �� 5B      $	��Top=�5C      ��5B      �� *	StepsList+��5D      �� 5E      .R�5F      �� $��0 0 0 1q�6%   
���   �._�5H   "   �� ��6/   �� *	Entries+��5I   &   �� 5J   *   .R�5K   .        Groupe 6J   �� *Groupe+   Decimals 5L   F   �� *
Decimals+   Minterme 5M   F   �� *
Minterme+   Check 5N   F   �� *Check+5O   +   5Q   #   5R      5S      5U      �� 5W      $��2��6   $��20(�5X      $��0 22 0 0q�5Y      $	��Top=�5Z      �� 5[      $�Result: ��6   �� 6,   �60   �� *SimplifiedExpression+$ OneWay=�5\      5]      5^      5_      5`      