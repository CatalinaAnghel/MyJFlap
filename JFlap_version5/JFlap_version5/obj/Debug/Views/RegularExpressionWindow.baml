   M S B A M L   `   `   `  ����I  EJFlap_version5, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:JFlap_version5.ViewsJFlap_version5.Views  5   	   E'clr-namespace:JFlap_version5.ViewModelsJFlap_version5.ViewModels  5   	   2    ,JFlap_version5.Views.RegularExpressionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:JFlap_version5.Views     5   	   6vm'clr-namespace:JFlap_version5.ViewModels     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	     � Title$  RegularExpressionWindow��5
   	   $	��450��6)   $	��800��66    � 	Resources 5      .   (JFlap_version5.ViewModels.RegexViewModel   vm��%   &	          5   
   6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��20 �6   �� 5      $	��500 �6   �� 5      $��20 �6   �� 5      $��* �6   �� 5      $��20 �6    � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��20 �6   .+�5   
   �� $��REGULAR EXPRESSION:��6   $��1��6.   $��1��6;   
������� � ToolTip 5      _� 5       .��6   31This is the pattern used to recognize the string.6M   5"      �� 5#   
   $��1��6   $��1��6&   $��2��66   $��1��6C   $��3q�6T   
���   ���6|   ��  �� Path$ PatternField.Pattern� �� Mode$ OneWayToSource=� �� IsAsync . 6�   �� 5$   
   $��TEST STRING:��6   $��3��6'   $��1��64   
������� 5%      _� 5&      .��6   This is the tested string.66   5(      �� 5)   
   $��1��6   $��1��6&   $��4��66   $��3q�6C   
���   ���6k   �� $ StringField.Pattern�$ OneWayToSource=� .  �� UpdateSourceTrigger$ PropertyChanged=�.��6�   .  (System.Windows.Interactivity.Interaction	   Triggers	 5*      /  )System.Windows.Interactivity.EventTrigger  5+      
   	EventName$
 KeyUp��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5,      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *VerifyTextCommand+5-      .��5.   &   5/      �� 50   
   $��3��6   $��1��6    
�������.��6E   MATCH INFORMATION:6Y   �� 51   
   $��1��6   
�������$��3��6@   $��2��6P   $��3��6]   ��6n   �� $ ResultField.Pattern� .  �� FallbackValue$ 
Loading...��53      54      