readme.md

# YubiKey Removal Behavior     


Licensed under the Apache License, Version 2.0 (the "License").

## ℹ️ About
The **YubiKey Removal Behavior** application is inspired by the native Windows "Smart Card Removal Behavior" feature and extends 
a similar level of control to FIDO2 Security Keys (YubiKeys) by locking a compatible Windows workstation OR logging out the
currently logged in user(s) when a YubiKey is removed. It does this by monitoring for YubiKey removal events and checking the 
value of the ```removalOption``` registry key:

- If the value is set to ```1```, the application will lock the workstation
- If the value is set to ```2```, the application will log out the user(s)

Control of the value is exercised using Group Policy, Registry or MDM.

⚠️ This application is provided "as-is" without any warranty of any kind, either expressed or implied.


## 💻 Prerequisites
The YubiKey Removal Behavior application is supported on ```64 bit``` **Windows 10** and **Windows 11**. 

## Installation
Run the provided MSI (no interaction is required) and reboot the computer for changes to take effect.

Here is an example of a running the installer from command line: 

```bash
msiexec /i /qn "YubiKey Removal Behavior Tool.msi"
```

## 📖 Usage
By default (no configuration required), the **YubiKey Removal Behavior** application will _lock_ the workstation on YubiKey removal. 
This behavior can be modified to instead _logout_ the user(s) or _disabling_ the functionality. Note further:

#### Removal from USB port
By default (no configuration required), the **YubiKey Removal Behavior** application will _lock_ the workstation if the YubiKey is removed from the USB port.
If ```logout``` behavior is configured, any currently logged in user will be logged out from Windows if the YubiKey is removed from the USB port.
To log back in, the user reinserts the YubiKey into the USB port and provides PIN and Touch.

#### Removal using NFC reader
**Note**: If you are using NFC you MUST set application behavior to ```logout``` (registry value ```2```).
When a supported NFC reader is attached to the workstation, the user can bring his/her YubiKey to the reader for a short duration
and achieve a "tap 'n go" type user experience. On successful read of the YubiKey, the **YubiKey Removal Behavior** application will
_logout_ any currently logged in user(s). To log back in, the user will again place the YubiKey on the NFC reader and provide PIN.

### Disable the functionality
The application can be "disabled" by changing the ```removalOption``` registry key to ```0``` or ```Disabled``` using Group Policy or local registry. 

**Note**: toggling the registry setting does not deactivate or uninstall the application.

## Administrative template (ADMX)
The accompanying administrative template (ADMX) adds the option to _enable_ (default) or _disable_ YubiKey removal behavior by setting a central (or local) GPO.The template is designed to augment the existing ```credentialprovider.admx``` with an additional setting. To use this template:

1. Copy the ADMX file ```YubiKeyRemovalBehavior.admx``` to location: ```C:\Windows\PolicyDefinitions``` the on your domain controller (DC) / member server
2. Copy the ADML file ```YubiKeyRemovalBehavior.adml``` to location: ```C:\Windows\PolicyDefinitions\en-US```
3. Open the GPO editor (restart if open previously) and navigate to **Computer Configuration** > **Policies** > **Administrative Templates** > **System** > **Logon**
4. Double-click on 'YubiKey removal behavior' and toggle policy: ```Enabled``` (ON) or ```Disabled``` (OFF)
5. Click **Apply** and **OK**.

**Note**: Since the setting applies to the computer object, computers must have read access to the GPO.

## Uninstalling the app
The application can be uninstalled from **Add/Remove Programs**, using **GPO** or **MDM**.

Here is an example of uninstalling from command line: 

```bash
msiexec /qn /x "YubiKey Removal Behavior Tool.msi"
```

## 🥅 Roadmap
Possible improvements includes:
- Provide script or instruction to toggle ```isEnabled``` with Microsoft Endpoint Manager
- Using variables and/or relative paths in the installer (paths, registry keys)
- Reducing overall footprint / size of of application
- Making the application launch sooner at logon
- Code signing

## 🥷🏻 Contributing
Any help on the above (see 'roadmap) is welcome.

## 📜 Release History
* 2023.08.26 `v2.0` Added option to logout of computer
* 2022.12.27 `v1.0` First release