   M S B A M L   `   `   `  ����  DPT_WPF"    DPT_WPF.ucCommon.ucCommonLED   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � ColumnDefinitions  5	   
   �� 5
      �� 5      .+�5   
   �� -   5         ��Name$ 
imgLeftLED��5      $��Uniform=�6$   $��0��66   $��Center=�6F   $*��$/ImgTab/1_home/LED/led_baseshape.png��6a   $	��356��6�   $��79��6�   �� 5   
   -   5        $ 
imgLightL1��5      $��0��6$   $ ��/imgTab/1_home/LED/on1.png��64   $��Uniform=�6X   $
��Left=�6j   $	��Top=�6�   $��51��6�   $��72��6�   $��	13,30,0,0q�6�    J� InputBindings 5      d� 5       d� Gesture$ 	LeftClicka�6    �� Command 63   �� *LED_Left_Command+5      �� 5   
   -   5        $ 
imgLightL2��5      $��0��6$   $ ��/imgTab/1_home/LED/on2.png��64   $��Uniform=�6X   $��Center=�6j   $	��Top=�6�   $��62��6�   $��84��6�   $��170,17,178,0q�6�    5      d� 5      $ 	LeftClicka�6    63   �� *LED_Left_Command+5      �� 5   
   -   5        $ 
imgLightL3��5      $��0��6$   $ ��/imgTab/1_home/LED/on3.png��64   $��Uniform=�6X   $��Right=�6j   $	��Top=�6�   $��70��6�   $��93��6�   $��0,8,14,0q�6�    5      d� 5      $ 	LeftClicka�6    63   �� *LED_Left_Command+5      �� 5   
   -   5        $ imgRightLED��5      $��Uniform=�6%   $��1��67   $��Center=�6G   $*��$/ImgTab/1_home/LED/led_baseshape.png��6b   $	��356��6�   $��79��6�   �� 5    
   -   5        $ 
imgLightR1��5       $��1��6$   $ ��/imgTab/1_home/LED/on1.png��64   $��Uniform=�6X   $
��Left=�6j   $	��Top=�6�   $��51��6�   $��72��6�   $��	13,30,0,0q�6�    5!      d� 5"      $ 	LeftClicka�6    63   �� *LED_Right_Command+5$      �� 5%   
   -   5        $ 
imgLightR2��5%      $��1��6$   $ ��/imgTab/1_home/LED/on2.png��64   $��Uniform=�6X   $��Center=�6j   $	��Top=�6�   $��62��6�   $��84��6�   $��170,17,178,0q�6�    5&      d� 5'      $ 	LeftClicka�6    63   �� *LED_Right_Command+5)      �� 5*   
   -   5        $ 
imgLightR3��5*      $��1��6$   $ ��/imgTab/1_home/LED/on3.png��64   $��Uniform=�6X   $��Right=�6j   $	��Top=�6�   $��70��6�   $��93��6�   $��0,8,14,0q�6�    5+      d� 5,      $ 	LeftClicka�6    63   �� *LED_Right_Command+5.      5/      50      