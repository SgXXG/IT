   M S B A M L   `   `   `  ����J  FSimplestCiphers, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:SimplestCiphersSimplestCiphers  5   	        SimplestCiphers.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:SimplestCiphers     5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962a:MD1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$%  Простейшие шифры��5	   	   $	��510��6"   $	��900��6/   $��24(�6;   $	��700��6I   $	��500��6X    � WindowStartupLocation$ CenterScreen=�5
   	   $��JetBrains Mono NL)�6.   .��5      �  � ColumnDefinitions 5   
   �� 5      $��22.191 �6   �� 5      $��30.435 �6   �� 5      $	��350 �6   �� 5       � RowDefinitions 5   
   �� 5      �� 5      .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��3��6   $��0��6"   $��5,5,5,5q�6/   
�������.��5      �� -   5         ��Name$ EncodingTextBox��5   9   $��10q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$, &Текст для Шифрования��6"    �� TextWrapping$
 Wrap=�6L    �� AcceptsReturn$
 True��6`   " �� VerticalScrollBarVisibility$
 Auto=�5      6T   5        5   
   $��3��6   $��1��6"   $��5,5,5,5q�6/   
�������.��5      �� -   5        $ DecodingTextBox��5   9   $��10q�5      $0 *Текст для Дешифрования��6"   $
 Wrap=�6N   $
 True��6b   $
 Auto=�5      6T   5        5!   
   $��0��6   $��2��6"   $��5q�63   
�������$��3��6U   .��5"      �� 	 �� Orientation$	 Vertical=�6   .+�5#      �� -   5        
 ��Name$
 CiphersComboBox��5#   K   $! Вид шифрования��6   $��10q�6?   .@�5$      �� $D��>Метод железнодорожной изгороди(En)��6#   $��12(�6P   �� 5%      $'��!Метод Плейфейра(En)��6#   $��12(�6A   �� 5&      $P��JМетод Виженера с прогрессивным ключем(Ru)��6#   $��12(�6W   5'      �� 5(      -   5        $ EncodingKeyTextBox��5(   K   $��10q�6   $# Ключ шифрования��6&   6K   �� 5)      -   5        $��5q�5)      $��50��6$   $��Зашифровать��60   
�����_�$��20(�6]   
 � Tag$ Encode��5*   -   $��2q�6:   �� 5+      -   5        $��5q�5+      $��50��6$   $��Расшифровать��60   
�����_�$��20(�6^   $ Decode��5,   -   $��2q�6:   �� 5-      $��10q�6   6*   �� 5.      -   5        $��5q�5.      $��50��6$   $%��Зашифровать файл��60   
�����_�$��20(�6b   $ Encode��5/   5   $��2q�6B   �� 50      -   5        $��5q�50      $��50��6$   $'��!Расшифровать файл��60   
�����_�$��20(�6c   $ Decode��51   5   $��2q�6B   �� 52      $��5q�6   6)   �� 53      -	   5        $ FilePathLabel��53       �� 
IsReadOnly$
 True��61   $ Путь к файлу��6C   $ False��6e   $ �� HorizontalScrollBarVisibility$
 Auto=�54      6=   55      56      58      59      