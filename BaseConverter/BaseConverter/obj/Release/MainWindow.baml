   M S B A M L   `   `   `  ����   BaseConverter    BaseConverter.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006       � 
ResizeMode$  NoResize=� � Title$ Base Converter��$��710.25��$��1235.333��.��� ���	#FF353A40.+��� -    ��Name$ conversionCombobox��$
��Left=�$��
553,17,0,0q�$	��Top=�$	��266��$��28��$��Rockwell)�$��Center=�$��Center=�
��������� -    ��Name$ value��$
��Left=�$��28��$��	93,17,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��454��$��Center=� � ToolTip$! Enter your conversion value�����	#FFD0F3C5�� $��Result��$
��Left=�$��
10,474,0,0q�$	��Top=�$��54��
�������$
��Bold"�$��14(��� -   $��Reset��$
��Left=�$��
281,50,0,0q�$	��Top=�$	��266��$��30�����	#FFE84949$��14(�
������� J� RenderTransformOrigin$ 
1.225,0.55&� � 	Resources  �� 
TargetType	 �� Property
 �� Value��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -   $ 	userGuess��$
��Left=�$��28��$��
93,440,0,0q�$
 Wrap=�$	��Top=�$	��261��$��Center=�$ Enter your guess�����	#FFABADB3���	#FFD0F3C5�� -    ��Name$ submitAnswer��$��Check��$
��Left=�$��359,439,0,0q�$	��Top=�$	��150��$��30�����	#FF4EDC61$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� $��Conversion Instructions��$
��Left=�$��30��$��	10,85,0,0q�$	��Top=�$	��176��
�������$
��Bold"�$��14(��� -    ��Name$ instructionsBox��$
��Left=�$	��272��$��
10,123,0,0q�$	��Top=�$	��809��$ Conversion instructions��$
��Auto=����	#FFFDC9C9�� -   $ stepsBox��$
��Left=�$	��149��$	��Top=�$	��809��$��
10,508,0,0q�$ Steps for conversion�����	#FFFDC9C9�� -   $��Convert��$
��Left=�$��	10,50,0,0q�$	��Top=�$	��266��$��30�����	#FF4EDC61$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -	   $
��Left=�$��21��$��1190,19,0,0q�$	��Top=�$��20��$��help.png��$ 	0.9,0.321&�$
 Help���� -
   $ workspaceBox��$
��Left=�$	��572��$��
838,50,0,0q�$
 Wrap=�$	��Top=�$	��372�� �� AcceptsReturn . $ Your workspace�����	#FFA6B6FF��. " �� VerticalScrollBarVisibility$
 Auto=��� $��	Workspace��$
��Left=�$��
838,11,0,0q�$	��Top=�$	��120��
�������$
��Bold"�$��14(��� -   $ submitAnswer_Copy��$��Clear Workspace��$
��Left=�$��838,627,0,0q�$	��Top=�$	��372��$��30�����	#FFE84949$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -   $��Conversion Table��$
��Left=�$��
689,85,0,0q�$	��Top=�$	��130��$��30�����	#FF5A8BD3$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -   $ showFinalAnswerButton��$��Show Final Answer��$
��Left=�$��669,439,0,0q�$	��Top=�$	��150��$��30�����	#FFE4A73D$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� $��1q� �� CornerRadius$ 7,0,0,7�����	#FFA85FF1$��10,439,1125,202q��� $��Your Answer��$
��Left=�$��28��$��
15,441,0,0q�$	��Top=�
��������� $��1q�$ 7,0,0,7�����	#FFA85FF1$��9,16,1126,625q��� $��Value��$
��Left=�$��28��$��	36,18,0,0q�$	��Top=�
��������� -   $ 
showAnswer��$��Show Answer��$
��Left=�$��514,439,0,0q�$	��Top=�$	��150��$��30�����	#FF85CDBC$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -   $��Random Conversion��$
��Left=�$��
553,50,0,0q�$	��Top=�$	��266��$��30�����	#FF5A8BD3$��14(�
�������$ 
1.225,0.55&� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4���� -   $ 
stepAnswer��$
��Left=�$��28��$��
93,402,0,0q�$
 Wrap=� �� 
IsReadOnly . $	��Top=�$	��726�����	#FF4D5280���	#FFABADB3$ Disabled=� �� MaxLines$ 1��$��Center=��� $��1q�$ 7,0,0,7�����	#FFA85FF1$��10,401,1125,240q��� $��Status��$
��Left=�$��28��$��
27,401,0,0q�$	��Top=�
��������� -   $��
Calculator��$
��Left=�$��
553,85,0,0q�$	��Top=�$	��131��$��30�����	#FF5A8BD3$��14(�
������� ��%9   '��       �� " ��.��� 	 �@��CornerRadius$
 4��