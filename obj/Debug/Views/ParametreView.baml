   M S B A M L   `   `   `  ����A  =ADALES, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:APPLICATION.ViewsAPPLICATION.Views  5      ?$clr-namespace:APPLICATION.ComponentsAPPLICATION.Components  5      %    APPLICATION.Views.ParametreView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5      +localclr-namespace:APPLICATION.Views  5      5
components$clr-namespace:APPLICATION.Components  5      .��5      �    BackgroundBrush#���   6     � ColumnDefinitions  5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5   
   � .+�5      �� .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      "  FontAwesome.WPF.ImageAwesome    	IconBrush   
Foreground# �  6&      Icon$
 Gear=�6O   $��40��6[   $��7q�6f   �� 5        	TextBrush �� 
Foreground# �  6!    �� FontSize$ 40(�6K   $��	Parametre��6Y   6m   5      5       5!      �� 5#   
   $��1��6   $��2��6"   $��3��62   $��Center=�6F   .+�5$      	� -   5         	�Name$ 	Dark_mode��5$   C   #���  6   $��	Dark mode��6V   $��30��6j    	� 	GroupName$ modeselection��6x   $��1��6�   $��2��6�   	� 5'      -   5        $ 
Light_mode��5'   C   #���  6   $��
Light mode��6W   $��30��6l   	 d� 	IsChecked$
	 TrueY�6x   $ modeselection��6�   $��1��6�   $��3��6�   5(      5*      5+      