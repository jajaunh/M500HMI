   M S B A M L   `   `   `  ����   DPT_WPF$    DPT_WPF.ucCommon.ucCommonMotor   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .��� ����   �� ImageSource$"  /imgTab/4_moter/motor_bg.png�� � RowDefinitions �� .+�� $��0��$��-8,0,8,0q� � ColumnDefinitions �� $	��123 ��� $	��110 ��� $	��110 ��� $	��110 ��� $��90 ��� $��90 ��� $��90 ��� $��90 ��� $	��100 � �� $	��119 ��� $	��105 ��� $	��105 ��� $	��105 ��� $	��105 ��� $	��134 �.+��� -    ��Name$ BuildRoomBorder��$��0��$��8��$��1��
������ ���#ffecd0 J� OpacityMask
 ���� $��2q�$��10,0,0,0q�$
��auto�� J� InputBindings d�  d� Gesture$ 	LeftClicka� �� Command �� *BuildRoom_Focus+ �� Mode$ TwoWay=��� -   $ RecotorBorder��$��0��$��8��$��2��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *Recotor_Focus+$ TwoWay=��� -   $ FrontBorder��$��0��$��8��$��3��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *Front_Focus+$ TwoWay=��� -   $ 
RearBorder��$��0��$��8��$��4��
������ ���#ffecd0
 ���� $��2q�$��10,4,0,4q�$
��auto�� d� $ 	LeftClicka� �� *
Rear_Focus+$ TwoWay=��� -   	 ��Name$	 Motor_R_Position��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��1��$��2���� -   $	 Motor_Z_Position��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��1��$��3���� -   $	 Motor_S_Position��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��1��$��4���� -   $	 motor_R_Speed��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��2��$��2���� -	   $	 motor_Z_Speed��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��2��$��3���� -
   $	 motor_S_Speed��$��0�����#d2d7e1$��20(�$��Center=�$��Center=�$��	0,30,0,30q�$��2��$��4���� -   $	 
Distance_R��$��0��$��2��$��3��$��20(�$��Center=�$��Center=�$��	0,25,0,25q����� $1  +/imgTab/4_moter/build-feed room/textbox.png��
 j� Stretch$

 None=� d� $ 	LeftClicka� �� *MotorDistance_R_Command+�� -   $	 
Distance_Z��$��0��$��3��$��3��$��20(�$��Center=�$��Center=�$��	0,25,0,25q����� $1  +/imgTab/4_moter/build-feed room/textbox.png��$

 None=� d� $ 	LeftClicka� �� *MotorDistance_Z_Command+�� -   $	 
Distance_S��$��0��$��4��$��3��$��20(�$��Center=�$��Center=�$��	0,25,0,25q����� $1  +/imgTab/4_moter/build-feed room/textbox.png��$

 None=� d� $ 	LeftClicka� �� *MotorDistance_S_Command+�� -    ��Name$ btnimgAddNeg2��$��4��$��2��$
��None=�$��/imgM500/imgLeftArrow.png�� d� $ 	LeftClicka� �� *AddNeg2Command+�� -   $ btnimgAddNeg3��$��4��$��3��$
��None=�$��/imgM500/imgLeftArrow.png�� d� $ 	LeftClicka� �� *AddNeg3Command+�� -   $ btnimgAddNeg4��$��4��$��4��$
��None=�$��/imgM500/imgLeftArrow.png�� d� $ 	LeftClicka� �� *AddNeg4Command+�� -   $ btnimgAddPos2��$��5��$��2��$
��None=�$ ��/imgM500/imgRightArrow.png�� d� $ 	LeftClicka� �� *AddPos2Command+�� -   $ btnimgAddPos3��$��5��$��3��$
��None=�$ ��/imgM500/imgRightArrow.png�� d� $ 	LeftClicka� �� *AddPos3Command+�� -   $ btnimgAddPos4��$��5��$��4��$
��None=�$ ��/imgM500/imgRightArrow.png�� d� $ 	LeftClicka� �� *AddPos4Command+�� -   $ btnimgJogNeg2��$��6��$��2��$
��None=�$&�� /imgM500/imgLeftArrowDisable.png�� d� $ 	LeftClicka� �� *JogNeg2Command+�� -   $ btnimgJogNeg3��$��6��$��3��$
��None=�$&�� /imgM500/imgLeftArrowDisable.png�� d� $ 	LeftClicka� �� *JogNeg3Command+�� -   $ btnimgJogNeg4��$��6��$��4��$
��None=�$&�� /imgM500/imgLeftArrowDisable.png�� d� $ 	LeftClicka� �� *JogNeg4Command+�� -   $ btnimgJogPos2��$��7��$��2��$
��None=�$'��!/imgM500/imgRightArrowDisable.png�� d� $ 	LeftClicka� �� *JogPos2Command+�� -   $ btnimgJogPos3��$��7��$��3��$
��None=�$'��!/imgM500/imgRightArrowDisable.png�� d� $ 	LeftClicka� �� *JogPos3Command+�� -   $ btnimgJogPos4��$��7��$��4��$
��None=�$'��!/imgM500/imgRightArrowDisable.png�� d� $ 	LeftClicka� �� *JogPos4Command+�� -   $ 	imgHome_R��$��8��$��2��$
��None=�����  �� 	UriSource$7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?� d� $ 	LeftClicka� �� *Motor_R_HomeCommand+�� -   $ 	imgHome_Z��$��8��$��3��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?� d� $ 	LeftClicka� �� *Motor_Z_HomeCommand+�� -   $ 	imgHome_S��$��8��$��4��$
��None=����� $7 1/imgTab/4_moter/build-feed room/btn_home_bage.png?� d� $ 	LeftClicka� �� *Motor_S_HomeCommand+�� $��Position��$��Bottom=�$��18(����#959fb4$��1��$��1��$
��Bold"�$��34��$��79��$��
15,0,15,71q��� $��Speed��$��Bottom=�$��18(����#959fb4$��2��$��1��$
��Bold"�$��34��$��62��$��
24,0,24,71q��� $��Distance��$��Bottom=�$��18(����#959fb4$��3��$��1��$
��Bold"�$��34��$��86��$��
12,0,12,70q��� $��AddNeg��$��Bottom=�$��18(����#959fb4$��4��$��1��$
��Bold"�$��34��$��80��$��5,0,5,69q��� $��AddPos��$��Bottom=�$��18(����#959fb4$��5��$��1��$
��Bold"�$��34��$��75��$��7,0,7,69q��� $��JogNeg��$��Bottom=�$��18(����#959fb4$��6��$��1��$
��Bold"�$��34��$��80��$��Center=�$��5,0,5,71q��� $��JogPos��$��Bottom=�$��18(����#959fb4$��7��$��1��$
��Bold"�$��34��$��75��$��7,0,7,70q��� $
��Home��$��Bottom=�$��18(����#959fb4$��8��$��1��$
��Bold"�$��34��$��62��$��
19,0,19,70q��� $��Center=�$��Center=�$��0��$��2��$��Recoater��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��87��$��18,38,18,37q��� $��Center=�$��Center=�$��0��$��3��$��Build Room 
 (Up/Down)��$��18(�$
��Bold"����#a8b2c8$��58��$��Center=�$	��113��$��
10,29,0,19q��� $��Center=�$��Center=�$��0��$��4��$��Supply��$��18(�$
��Bold"����#a8b2c8$��34��$��Center=�$��76��$��24,43,23,34q��� $#��/imgTab/4_moter/title box.png��$	��180��$��50��$	��Top=�$
��Left=�$��	10,16,0,0q��� $��Build/Feed Room��$��18(����#959fb4$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$
��Bold"�$	��Top=�$
��Left=�$��	22,25,0,0q�