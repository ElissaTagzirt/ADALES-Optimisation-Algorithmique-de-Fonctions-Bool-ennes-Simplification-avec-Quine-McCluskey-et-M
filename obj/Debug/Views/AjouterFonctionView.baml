   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ComponentsAPPLICATION.Components  5      +    %APPLICATION.Views.AjouterFonctionView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:APPLICATION.Views     5      ;
components$clr-namespace:APPLICATION.Components     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      .��5      � ��5   
   �� 5         BackgroungNum  �� ImageSource#  �   6    � ColumnDefinitions 5   
   �� 5      $	��400 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��150 �6   �� 5      $	��150 �6   �� 5      $	��150 �6   .+�5   
   �� $��2��6   .��5      ��  �� Orientation$ 
Horizontal=�6   $	��500��62   .+�5       "  FontAwesome.WPF.ImageAwesome    	IconBrush   
Foreground# �  6"      Icon$
 Plus=�6K   $��40��6W   �� 5!        	TextBrush �� 
Foreground# �  6    �� FontSize$ 40(�6F   $��30,0,0,0q�6T   $��Ajouter une fonction��6f   $	��500��6�   6�   5#      5$      �� 5'   
   $��1��6   $��2��6"   $ 
Horizontal=�66   $��Center=�6O   .+�5)      ��  
 Button#���  6   $��
Littérale��6e   $	��250��6y   $	��120��6�   $��0,0,10,0q�6�   $��17(�6�     Comfortaa-Regular#��[ 6�   ��68   �� *NavigateLitteraleCommand+ � ToolTip 5+      �� 5,      $%��Ajouter une fonction littérale��6    5.      �� 5/      #���  6   $��
Numérique��6e   $	��250��6z   $	��120��6�   $��10,0,0,0q�6�   $��17(�6�   #��[ 6�   ��68   �� *NavigateNumeriqueCommand+ 51      �� 52      $%��Ajouter une fonction numérique��6    54      56      59      5:      