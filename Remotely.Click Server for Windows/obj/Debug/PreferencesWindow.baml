   M S B A M L   `   `   `  ����h  dRemotely.Click Server for Windows, Version=1.0.0.0, Culture=neutral, PublicKeyToken=84eed99c4a5b75dcU/clr-namespace:Remotely.Click_Server_for_Windows!Remotely.Click_Server_for_Windows  5   	   e7clr-namespace:Remotely.Click_Server_for_Windows.Support)Remotely.Click_Server_for_Windows.Support  5	   	   9    3Remotely.Click_Server_for_Windows.PreferencesWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Alocal/clr-namespace:Remotely.Click_Server_for_Windows     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   Esupport7clr-namespace:Remotely.Click_Server_for_Windows.Support  5	   	     � Title$5  /Preferences - Remotely.Click Server for Windows��5
   	   $	��520��6A   $	��600��6N   .��5      � .+�5   
   �� $��Stretch=�6   $��Stretch=�64   $��0,60,0,0q�6P    � 	Resources 5       �� 
TargetType �� Property �� Value��%V   '��       �� 5      " ��6   .�5      ��  � ��$ Center=�6<   5      5      .@�5      �� $��General��6   $��14(�6'   .��5      � 
������ .+�5      �� -   5        $��ShouldAutostart��5      $)��#Should start automatically at login��66   $
��Left=�6d   $��	50,50,0,0q�6   $	��Top=�6�    d� 	IsChecked$
 TrueY�6�   $	��500��6�   �� 5      -   5        $��ShouldServerAutostart��5      $2��,Should start server at launch of application��6<   $
��Left=�6s   $��	50,85,0,0q�6�   $	��Top=�6�   $	��500��6�   $
 TrueY�6�   6�   5      5      �� 5      $��Network��6   $��14(�6'   .��5      � 
������ .+�5      �� -   5        $��ShouldAutoDiscoverDevices��5      $-��'Auto discover network devices (Bonjour)��6@   $
��Left=�6r   $��	50,50,0,0q�6�   $	��Top=�6�   $	��500��6�   $
 TrueY�6�   6�   �� 5      -   5        $��DeviceDiscoverableName��5      $
��Left=�6<   $��23��6W   $��
50,120,0,0q�6c    �� TextWrapping$
 Wrap=�6w   $	��Top=�6�   $	��500��6�   .��6  .  (System.Windows.Interactivity.Interaction   	Behaviors 5      M   GRemotely.Click_Server_for_Windows.Support.TextBoxEnterKeyUpdateBehavior  5      .��5   3   5      �� 5       $
��Left=�6    $��	50,90,0,0q�6;   $
��Wrap=�6N   $��Discoverable Name��6b   $	��Top=�6{   $	��200��6�   $
��Bold"�6�   �� 5!      $
��Left=�6    $��
255,90,0,0q�6;   $
��Wrap=�6O   $"��(if empty use computer name)��6c   $	��Top=�6�   $	��295��6�   �� 5"      -   5        $��ServerIpAddress��5"      $
��Left=�65   $��23��6P   $��200,200,0,0q�6\   $
 Wrap=�6q   $��0.0.0.0��6�   $	��Top=�6�   $	��350��6�   {�.  .��6   5#        5$      .��5%   3   5&      �� 5'      $
��Left=�6    $��
50,160,0,0q�6;   $
��Wrap=�6O   $	��Top=�6c   $	��500��6{   $
��Bold"�6�   �� 6�   $�
Server Con��6�   �� 6�   $�
figuration��6�   6�   �� 5(      $
��Left=�6    $��
50,200,0,0q�6;   $
��Wrap=�6O   $��IP address:��6c   $	��Top=�6v   $	��150��6�   �� 5)      $
��Left=�6    $��
50,240,0,0q�6;   $
��Wrap=�6O   $��Port:��6c   $	��Top=�6p   �� 5*      -   5        $��ServerPortNumber��5*      $
��Left=�66   $��23��6Q   $��200,240,0,0q�6]   $
 Wrap=�6r   $��0��6�   $	��Top=�6�   $	��350��6�   {�.  .��6   5+        5,      .��5-   3   5.      �� 5/      -   5        $��ShouldUseCustomPortNumber��5/      $��use custom port number ��6@   $
��Left=�6b   $��200,280,0,0q�6}   $	��Top=�6�   $	��350��6�   6�   �� 50      -   5        $��ListenButton��50      $��Start listening��61   $
��Left=�6K   $��
50,320,0,0q�6f   $	��Top=�6z   $	��150��6�   6�   �� 51      -	   5        $��ServerStatusIcon��51      $
��Left=�64   $��28��6O   $��520,317,0,0q�6[   $	��Top=�6p   $��28��6�   $��Icons/red-icon.png��6�   52      53      �� 54      $��Security��6   $��14(�6(   .��55      � 
������ .+�56      �� $
��Left=�6    $��	50,50,0,0q�6;   $
��Wrap=�6N   $"��Set Password for Connection:��6b   $	��Top=�6�   $
��Bold"�6�   $	��500��6�   �� 57      $
��Left=�6    $
��Wrap=�6;   $��
Password: ��6O   $	��Top=�6a   $��
50,100,0,0q�6y   $	��200��6�   �� 58      $
��Left=�6    $��
50,150,0,0q�6;   $
��Wrap=�6O   $��Confirm password:��6c   $	��Top=�6|   $	��200��6�   G� 59      -
   5        $��PasswordBox��59   "   $
��Left=�65   $��23��6P   $��250,100,0,0q�6\    G� Password$  ��6q   $	��Top=�6}   $	��300��6�    5:      Q  KRemotely.Click_Server_for_Windows.Support.PasswordBoxEnterKeyUpdateBehavior  5;      5=      G� 5>      -   5        $��ConfirmPasswordBox��5>   "   $
��Left=�6<   $��23��6W   $��250,150,0,0q�6c   $  ��6x   $	��Top=�6�   $	��300��6�    5?        5@      5B      �� 5C      -   5        $��ApplyButton��5C      $��Apply��60   $
��Left=�6@   $��250,200,0,0q�6[   $	��Top=�6p   $	��140��6�   6�   �� 5D      -   5        $��ClearButton��5D      $��Clear��60   $
��Left=�6@   $��410,200,0,0q�6[   $	��Top=�6p   $	��140��6�   6�   5F      5G      5H      �� 5I   
   $��Center=�6   $��	0,10,0,10q�61   $
��Wrap=�6D   $��Preferences��6X   $	��Top=�6k   $��22(�6�   $
��Bold"�6�   5K      5L      