

__  __      __    _ __ __              ____                                  __   ____       __                _           
\ \/ /_  __/ /_  (_) //_/__  __  __   / __ \___  ____ ___  ____ _   ______ _/ /  / __ )___  / /_  ____ __   __(_)___  _____
 \  / / / / __ \/ / ,< / _ \/ / / /  / /_/ / _ \/ __ `__ \/ __ \ | / / __ `/ /  / __  / _ \/ __ \/ __ `/ | / / / __ \/ ___/
 / / /_/ / /_/ / / /| /  __/ /_/ /  / _, _/  __/ / / / / / /_/ / |/ / /_/ / /  / /_/ /  __/ / / / /_/ /| |/ / / /_/ / /    
/_/\__,_/_.___/_/_/ |_\___/\__, /  /_/ |_|\___/_/ /_/ /_/\____/|___/\__,_/_/  /_____/\___/_/ /_/\__,_/ |___/_/\____/_/     
                          /____/                                                                        version 2.0.0                                                                                         


						  
DISCLAIMER:
===========
This application was created for demonstration purposes only. It is NOT a product of Yubico and as such this application and 
the accompanying Microsoft Administrative Templates (ADMX) are provided "as-is" without any warranty of any kind, either 
expressed or implied.

About YubiKey Removal Behavior
==============================
The 'YubiKey Removal Behavior' application is inspired by the native Windows "Smart Card Removal Behavior" feature and extends 
a similar level of control to FIDO2 Security Keys (YubiKeys) by locking a compatible Windows workstation OR logging out the
currently logged in user(s) when a YubiKey is removed. It does this by monitoring for YubiKey removal events and checking the 
value of the 'removalOption' registry key:

- If the value is set to '1', the code will lock the workstation
- If the value is set to '2', the code will log out the user(s)

Control of the value is exercised using Group Policy, Registry or MDM.

Installation
============
Run the provided MSI (no interaction is required) and reboot the computer for changes to take effect.

Note: The YubiKey Removal Behavior application is supported on 64 bit Windows 10 and Windows 11. 
For the application to work the target system must also have been configured for Security Key sign-in.

The Microsoft Installer (MSI) will install the application in the C:\Program Files\Yubico\YubiKey Removal Behavior\ directory. 
Additionally, it will create two registry entries: 

	1. 'removalOption' with a default value of '1' (lock) under: "HKLM\Software\Policies\Yubico\YubiKey Removal Behavior" 
	2. A reference to the YubiKeyRemovalBehavior.exe is added to: "HKLM\Software\Microsoft\Windows\CurrentVersion\Run" 

NOTE: All registry keys are created in context of the machine in order to enforce functionality for ALL users.

Administrative template (ADMX)
------------------------------
The supplied administrative template (ADMX) adds the option to enable (default) or disable 'YubiKey Removal Behavior' by Group Policy.
The template is designed to augment the existing credentialprovider.admx with an additional setting. To use the template:

	1. Copy the ADMX file YubiKeyRemovalBehavior.admx to location: C:\Windows\PolicyDefinitions the on your domain controller (DC)
	2. Copy the ADML file YubiKeyRemovalBehavior.adml to location: C:\Windows\PolicyDefinitions\en-US
	3. Open the GPO editor and navigate to Computer Configuration > Policies > Administrative Templates > System > Logon
	4. Double-click on 'YubiKey removal behavior' and toggle the policy controls as required.
	5. Click Apply and OK.

Note: Since the setting applies to the computer object, computers must have read access to the GPO.

Usage
=====
By default (no configuration required), the YubiKey Removal Behavior application will lock the workstation on YubiKey removal. 
This behavior can be modified to instead logout the user or disabling the functionality.

Removal from USB port
---------------------
By default (no configuration required), the 'YubiKey Removal Behavior' application will logout the user (any/all) from
the workstation on YubiKey removal. To log back in, the user will reinsert the YubiKey into the USB port and provide PIN.

Removal using NFC reader
------------------------
When a supported NFC reader is attached to the workstation, the user can bring his/her YubiKey to the reader for a short duration
and achieve a "tap out" logout sequence. On successful read of the YubiKey, the 'YubiKey Removal Behavior' application will
logout any currently logged in user(s). To log back in, the user will again place the YubiKey on the NFC reader and provide PIN.

Disable the functionality
-------------------------
The application can be "disabled" by changing the `removalOption` registry key to '0' or 'Disabled' using Group Policy or local registry. 
Note that toggling the registry setting does not deactivate or uninstall the application.

Uninstall
---------
The application can be uninstalled from Add/Remove Programs, using GPO or MDM.

Roadmap
=======
Possible improvements includes:

	- Provide script or instruction to toggle ```isEnabled``` with Microsoft Endpoint Manager
	- Using variables and/or relative paths in the installer (paths, registry keys)
	- Reducing overall footprint / size of of application
	- Making the application launch sooner at logon
	- Code signing

Contributing
============
Any help on the above (see roadmap) is welcome. 
