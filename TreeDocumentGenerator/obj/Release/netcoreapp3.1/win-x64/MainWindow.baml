   M S B A M L   `   `   `  ���� P  LTreeDocumentGenerator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:TreeDocumentGeneratorTreeDocumentGenerator  &     TreeDocumentGenerator.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:TreeDocumentGenerator     -     � Title$  
MainWindow��$	��530��$	��800�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�.���  � RowDefinitions �� $��30 ���  � ColumnDefinitions �� �� .+�� -   ���	#ff304351$��2�� �� �� $��25 ��� $��25 ��� $��4 �.+��� -   $��Close��$��2�����	#ff02D06C$��X��$��0q�$��Center=�$��Center=�$��20��$��20��
��������� -   $��Minimize��$��1�����	#ff02D06C$��20��$��20��$��0q�$��Center=�$��Center=�$��-��
��������� $��1��$��12q�.+��� $��Plaque image path��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"���  �� Orientation$ 
Horizontal=�.+��� -   $��PlaqueImagePath��$	��0 8q����	#fff6f6f8$��0q�$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� -   $��PlaqueImageBrows��$��1��$��8 0 0 0q�$��12(�$��Brows��$��Medium"�
�����������  �� 
TargetType" ��.���  �� Property � ��	 �� Value$	 Fonts/#Dosis)���  � ��	 �	#ff02D06C��  �@�Cursor$
	 Hand������ 
 �� 
TargetType"
 ��.R��� $��24��$��75��#��z�� �� CornerRadius$ 7��._��� $��Center=�$��Center=��� $��Name on the plaque��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -   $��NameOnThePlaque��$	��0 8q�$��0q����	#fff6f6f8$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $��CustomerName��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -   $��CustomerName��$	��0 8q�$��0q����	#fff6f6f8$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $��	Tree type��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -	   $��TreeType��$	��0 8q����	#fff6f6f8$��0q�$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $��Location��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -
   $��Location��$	��0 8q����	#fff6f6f8$��0q�$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $
��Date��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -   $
��Date��$	��0 8q����	#fff6f6f8$��0q�$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $��Tree id��$��Fonts/#Dosis)����	#ff304351$��0 8 0 0q�$��Medium"��� -   $��TreeId��$	��0 8q����	#fff6f6f8$��0q�$	��200��$��Fonts/#Dosis)�$��14(�$	��4 2q�$
��Left=��� $ 
Horizontal=�.+��� -   $��ResetButton��$��Reset��$��0 16 0 0q�$��14(�$
��Bold"����	#ff304351���� " ��.���  � ��$	 Fonts/#Dosis)���  � ��	 �	#fff6f6f8��  �@�Cursor$
	 Hand������ "
 ��.R��� $��24��$	��130��#��z��$ 7��._��� $��Center=�$��Center=��� -   $��
SaveButton��$
��Save��$��8 16 0 0q�$��14(�$
��Bold"�
����������� " ��.���  � ��$	 Fonts/#Dosis)���  � ��	 �	#ff02D06C��  �@�Cursor$
	 Hand������ "
 ��.R��� $��24��$	��130��#��z��$ 7��._��� $��Center=�$��Center=�� $��1��$��2�� �� �� $	��500 �.+��� -   $��OutputImage��$��1��$��Images/TreeDocument.png��