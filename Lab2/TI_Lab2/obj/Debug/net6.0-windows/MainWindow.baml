   M S B A M L   `   `   `  ����B  >TI_Lab2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TI_Lab2TI_Lab2  5   	       TI_Lab2.MainWindow   5   	   _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TI_Lab2     5   	     � WindowStartupLocation$  CenterScreen=�5       � Title$ LFSR шифрование��5	   	   $	��600��6!   $	��800��6.   $��JetBrains Mono NL)�6:   $	��500��6Y   $	��700��6i   .��5
      � -   5         �Name$ MainRoot��5
      
������� � RowDefinitions 5   
   �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $	��250 �6   �� 5      .+�5   
   #  MaterialDesignThemes.Wpf.Card  $��0��6   $��0��6+   $��3��6;   $��7q�6L   
�����_�$��3q�6n   .��5      �� .+�5      �� $��2q�6   $��DemiBold"�6#   .��69   86Начальное состояние регистра6W   �� 5      -   5         ��Name$ InitialString��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$< 6Начальное состояние регистра��61   $��5q�5       �� TextWrapping$
 Wrap=�6)    �� AcceptsReturn$
 True��6=   "	 �� VerticalScrollBarVisibility$
	 Auto=�5      6@   �� 5      -   5        
 ��Name$
 Panel��5       �� Orientation$ 
Horizontal=�6,   .+�5      �� $��1q�6   $��Center=�6'   $��Center=�6D   .��6_   31Количество символов ключа:6{   �� 5      -   5        $ 	KeyLength��5      $6 0Количество символов ключа��61   $��1q�5   !    �� 
IsReadOnly$
 True��6,   $��25��6>   $��20��6I   5       �� 5"      -   5         ��Name$ Encrypt_Button��5"      $��5q�62   $��32��6=   
�������.��6v   Зашифровать6�   �� 5#      -   5        $ Decrypt_Button��5#      $��5q�62   $��32��6=   
�������.��6v   Расшифровать6�   �� 5$      -   5        $ FormKey_Button��5$      $��5q�62   $��32��6=   
�������.��6z   %#Сгенерировать ключ6�   �� 5%      -   5        $ OpenFile_Button��5%      $��5q�62   $��32��6=   
�������.��6w   Выбрать файл6�   5&      5'        5)   
   $��0��6   $��1��6+   $��5q�6;   .��5*      �� -	   5        $ 	Plaintext��5*      $
 Wrap=�6)   $
 True��6=   $
	 Auto=�5+      $
 True��69   5,        5.   
   $��1��6   $��1��6+   $��5q�6;   .��5/      �� -
   5        $	 Key��5/      $
 Wrap=�6#   $
 True��67   $
	 Auto=�50      $
 True��69   51        53   
   $��2��6   $��1��6+   $��5q�6;   .��54      �� -   5        $ 
ResultText��54      $
 Wrap=�6*   $
 True��6>   $
	 Auto=�55      $
 True��69   56      57      58      