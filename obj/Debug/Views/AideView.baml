   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ComponentsAPPLICATION.Components  5           APPLICATION.Views.AideView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:APPLICATION.Views     5      ;
components$clr-namespace:APPLICATION.Components     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5        � 	Resources  5          BooleanToVisibilityConverter��%   &	        �� 5   
   6'   5      .��5      �   BackgroundBrush#���  6    � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   �� $��1��6   $��1��6   .��5      ��  �� Orientation$ 
Horizontal=�6   $	��300��62   .+�5       "  FontAwesome.WPF.ImageAwesome    	IconBrush   
Foreground# �  6"      Icon$ QuestionCircle=�6K   $��40��6a   �� 5!        	TextBrush �� 
Foreground# �  6    �� FontSize$ 40(�6F   $��30,0,0,0q�6T   $��Page d'aide��6f   6|   5#      5$      �� 5&   
   $��2��6   $��1��6   .��5(      �� $��20q�6   $ 
Horizontal=�6&   $	��600��6?   .+�5)      �� # �  6   $ 25(�6F   $��90,0,0,0q�6T   $(��"Comment pouvons-nous vous aidez ? ��6f   6�   5*      5+      �� 5-   
   -   5         ��Name$ Erreurs��5-   T   $��20q�6   $	��600��6   $��50��6+   $��3��67   $��1��6D   $��20(�6e   .@�5.      �� -   5        	 ��Name$	 SyntaxeErreur��5.      $��Erreur de syntaxe��62   �� 5/      -   5        $	 EntryErreur��5/      $��Operande binaire��62   51      �� 54   
   $��4��6   $��1��6"   $ 
Horizontal=�62   $��20q�6K   $	��600��6X   .+�55      �� -   5        
 ��Name$
 SyntaxeT��55      $
��Wrap=�6*   $��15(�6>   $p��jIl faut respecter la syntaxe introduise dans le dictionnaire , Essayez d'utiliser les bouttons associés .��6L   #���  6�   $��88��6  $	��597��6  y�6�   �� *Visibility.Visibility+6  �� 56      -   5        $
 EntryT��56      $
��Wrap=�6)   $��15(�6=   $M��G IL s'agit d'un opérateur binaire , introduisez le deuxième operande.��6K   #���  6�   $��88��6�   $	��597��6�   y�6�   �� *Visibility.Visibility+6�   5:      5=      5>      