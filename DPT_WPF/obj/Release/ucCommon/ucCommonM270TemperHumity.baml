   M S B A M L   `   `   `  ���� U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf   DPT_WPF/   )DPT_WPF.ucCommon.ucCommonM270TemperHumity   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     .��� ����   �� ImageSource$)  #/imgTab/7_temperature/TempShape.png�� � RowDefinitions �� $��80 ��� ��  � ColumnDefinitions �� �� $��1 ��� .+��� $
��None=�$��2��$��0������  �� 	UriSource$+ %/imgTab/7_temperature/title shape.png?��� $
��None=�$��0��$��0������ $+ %/imgTab/7_temperature/title shape.png?��� ���#D3D5D9$��0��$��0��$��Center=�$��Center=�$��Temperature��$
��Bold"�$��16(�$*��$/DPT_WPF;component/Font/#NanumGothic)��� ���#D3D5D9$��2��$��0��$��Center=�$��Center=�$��배관 온도��$
��Bold"�$��16(�$*��$/DPT_WPF;component/Font/#NanumGothic)�� $��1��$��0��.+��� $	��238��$	��238��$
��Fill=� �� Left$	 337��
 �� Top$ 28������ $) #/imgTab/7_temperature/hum shape.png?��� $��66��$��30��$
��Fill=�$��0,0,0,40q����� $* $/imgTab/7_temperature/tem  image.png?�   LiveCharts.Wpf.Gauge  -     Name$ temperatureGage��$	��180��$	��180��   From$ 0Q�	   To$	 100Q�	   InnerRadius$	 85X�
 �� ContentStringFormat$	
 Int��
������ $	 357��$ 49�� J� RenderTransformOrigin$	 0,0&�   Uses360Mode .    LabelsVisibility$ Hidden=�$��138,43,138,47q�   Value �� *IntTemperature+   GaugeActiveFill ��  �� EndPoint$ -60,1000&� � MappingMode$ Absolute=� �� 
StartPoint$ 99,1000&�.���  � Color$ #ff4268��   GaugeRenderTransform V� .~���  �� Angle$ 90Q��� $��Center=�$��34.76(����#ff4266$	 337��$	 101��$	��140��$��160,145,160,55q����� *StrTemperature+� $��1��$��2��.+��� $	��238��$	��238��$
��Fill=�$	 337��$ 28������ $) #/imgTab/7_temperature/hum shape.png?��� $��66��$��30��$
��Fill=�$��0,0,0,40q����� $* $/imgTab/7_temperature/tem  image.png?�  -   $ 
humityGage��$	��180��$	��180��$ 0Q�$	 100Q�$	 85X�$	
 Int��$	 357��$ 48��$	 0,0&� . 
������ $ Hidden=�$��138,43,138,47q� �� *	IntHumity+ �� $ 0,100&�$ Absolute=�$ 100,0&�.��� $ #ff4268�� V� .~��� $ 90Q��� $��Center=�$��34.76(����#ff4266$	 337��$	 101��$	��140��$��160,145,160,55q����� *	StrHumity+� $��2��$��0��$��3�� �� $	��36* ��� $
��241* � �� �� �� .+�� $��0��$��0��$��36��$	��Top=�.+��� $#��/imgTab/4_moter/title box.png��$	��148��$��41��$��Center=�$��Center=�$��
78,2,78,-7q��� $��Powder Samply��$��18(����#959fb4$��Bottom=�$��Center=�� $��0��$��1��.+��� $#��/imgTab/4_moter/title box.png��$	��148��$��41��$��Center=�$��Center=�$��
78,2,78,-7q��� $��Powder Left��$��18(����#959fb4$��Bottom=�$��Center=�� $��0��$��2��.+��� $#��/imgTab/4_moter/title box.png��$	��148��$��41��$��Center=�$��Center=�$��
78,2,78,-7q��� $��Powder��$��18(����#959fb4$��Bottom=�$��Center=�� $��1��$��0��.+��� $&�� /imgTab/4_moter/powder/case1.png��$	��143��$	��184��$��20,0,0,0q��� -    ��Name$ PowderSupplyIm�����#4D5665$	��120��$	��120��$��0�� �� RadiusX$ 0�� �� RadiusY$ 0��$��	20,0,0,40q�$��Bottom=�� $��1��$��1��.+��� $&�� /imgTab/4_moter/powder/case2.png��$	��235��$	��184��$��20,0,0,0q��� $&�� /imgTab/4_moter/powder/POINT.png��$��9��$��9��$��
0,0,250,60q��� $0��*/imgTab/4_moter/powder/POINT-highlight.png��$��31��$��31��$��0,150,250,0q��� -   $ PowderFLeft�����#4D5665$��80��$	��120��$��0��$ 0��$ 0��$��157,65,0,40q�$��Bottom=��� $0��*/imgTab/4_moter/powder/POINT-highlight.png��$��31��$��31��$��	20,0,0,60q��� $0��*/imgTab/4_moter/powder/POINT-highlight.png��$��31��$��31��$��
20,150,0,0q��� -   $ PowderRLeft�����#4D5665$��80��$	��120��$��0��$ 0��$ 0��$��54,0,171,40q�$��Bottom=�� $��1��$��2��.+��� $"��● 앞__파우더 공급량�����#959fb4$��14(�$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$��Center=�$
��Left=�$��20,12,0,179q�$��20���� $"��● 뒤__파우더 공급량�����#959fb4$��14(�$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$��Center=�$
��Left=�$��20,60,0,131q�$��20���� $#��출력시 파우더 공급량�����#959fb4$��14(�$/��)/DPT_WPF;component/Font/#NanumSquare Bold)�$��Center=�$
��Left=�$��20,110,0,81q�$��20���� $)��#/imgTab/4_moter/powder/text box.png��$	��105��$	��139��$��164,-74,36,0q��� $'��!/imgTab/4_moter/powder/botten.png��$	��218��$��57��$��Center=�$��Bottom=�$��0,0,0,7q� J� InputBindings d�  d� Gesture$ 	LeftClicka� �� Command �� *PowderApplyCommand+�� $	��105��$��40��
������ 
������ ���#FFFFFF$��18(�$��163,5,36,175q� �� TextAlignment$ Center=����� *PowderSupplyWindowCommand+�� $	��105��$��40��
������ 
������ ���#FFFFFF$��18(�$��163,5,36,73q�$ Center=����� *PowderSupplyWindowCommand2+�� $	��105��$��40��
������ 
������ ���#FFFFFF$��18(�$��163,10,36,-22q�$ Center=����� *PowderSupplyWindowCommand3+