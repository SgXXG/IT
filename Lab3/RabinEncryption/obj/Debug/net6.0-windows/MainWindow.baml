   M S B A M L   `   `   `  ����J  FRabinEncryption, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:RabinEncryptionRabinEncryption  5   	        RabinEncryption.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:RabinEncryption     5   	     � Title$  RabinEncryption��5   	   $	��500��6!   $	��700��6.   $	��300��5	   	   $	��500��6    � WindowStartupLocation$ CenterScreen=�5
   	   $��JetBrains Mono)�5   	   $��16(�6%   .��5      � $��3q�6    � 	Resources 5   
    �� 
TargetType   TextBoxStyle �� Property �� Value��%�   &	        �� 5      " ��6   .�5      ��  � ��$ 3q�6+   �� 5      # �@��VerticalScrollBarVisibility$
 Auto=�6@   �� 5       �@��TextWrapping$
 Wrap=�61   �� 5       �@��AcceptsReturn$
 True��62   5      5       � RowDefinitions 5   
   �� 5      �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5       �� 5!       5$      �� 5%      $
��Auto �6    �� 5&      .+�5)      �� $��0��6   $��0��6%   $��Center=�65   $��Center=�5*      	Plaintext6>   �� 5+      $��0��6   $��1��6%   $��Center=�65   $��Center=�5,      
Ciphertext6?   �� 5.      -   5         ��Name$ EncryptedText��5.   3   $��1��6   $��0��6#   #��[  6J   	 �� TextWrapping$
	 Wrap=�5/      
 �� AcceptsReturn$

 True��6*   " �� VerticalScrollBarVisibility$
 Auto=�50       �� 
IsReadOnly$
 True��69   6M   �� 51      -   5        $ DecryptedText��51   3   $��1��6   $��1��6#   #��[  6J   $
	 Wrap=�52      $

 True��6*   $
 Auto=�53      $
 True��69   6M   55      � 57   
   $��1��6   $��3q�6    59      �� 5:      $
��Auto �6#   �� 5;      �� 5<      $
��Auto �6#   �� 5=      �� 5>      $
��Auto �6#   �� 5?      .+�5B      �� $��0��6   $	��3 0q�6(   Q:6;   �� 5C      -   5        $ QTextBox��5C   &   $��1��6   $ �� HorizontalScrollBarVisibility$
 Auto=�68   .��6]   196a   �� 5D      $��2��6   $	��3 0q�6(   P:6;   �� 5E      -   5        $ PTextBox��5E   &   $��3��6   $
 Auto=�68   .��6]   116a   �� 5F      $��4��6   $	��3 0q�6(   B:6;   �� 5G      -   5        $ BTextBox��5G   &   $��5��6   $
 Auto=�68   .��6]   1736b   5I      � 5K   
   $��2��6    5M      �� 5N      �� 5O      .+�5R      �� -   5         ��Name$ EncryptionButton��5R   0   $��0��6   $��3q�6%   $��Cipher��5S   7   6J   �� 5T      -   5        $ DecryptionButton��5T   0   $��1��6   $��3q�6%   $��Decipher��5U   7   6L   5W      5Y      5Z      