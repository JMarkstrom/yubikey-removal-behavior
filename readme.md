readme.md

# YubiKey Removal Behavior     

## ℹ️ About
The **YubiKey Removal Behavior** (YKRB) application is inspired by the native Windows "Smart Card Removal Behavior" feature and extends 
a similar level of control to FIDO2 Security Keys (YubiKeys) by locking a compatible Windows workstation OR logging out the
currently logged in user(s) when a YubiKey is removed. It does this by monitoring for YubiKey removal events and checking the 
value of the ```removalOption``` registry key:

- If the value is set to ```lock```, the application will lock the workstation
- If the value is set to ```logout```, the application will log out the user(s)

Control of the value is exercised using Group Policy, Registry or MDM (Intune).

⚠️ This application is provided "as-is" without any warranty of any kind, either expressed or implied.

**Note**: For cross-platform use, consider [Sciber YubiKey Locker](https://github.com/sciber-io/yubikey-locker)

## 💻 Prerequisites
The YubiKey Removal Behavior (YKRB) application is supported on ```64 bit``` **Windows 10** and **Windows 11**. 

## 💾 Installation
Run the provided MSI (no interaction is required) and reboot the computer for changes to take effect.

Here is an example of a running the installer from command line: 

```bash
msiexec /i /qn "YubiKey-Removal-Behavior.msi"
```
## 💾 Uninstalling the app
The application can be uninstalled from **Add/Remove Programs**, using **GPO** or **MDM**.

Here is an example of uninstalling from command line: 

```bash
msiexec /qn /x "YubiKey-Removal-Behavior.msi"
```
## 🛠️ Configuring and Monitoring the application

### 👮🏻 Administrative template (ADMX)
The accompanying administrative template (ADMX) adds the option to control YubiKey removal behavior by setting a central (or local) GPO.
To use this template with Microsoft Active Directory (or a local computer):

1. Copy the ```.admx``` file to location: ```C:\Windows\PolicyDefinitions```
2. Copy the ```.adml``` file to location: ```C:\Windows\PolicyDefinitions\en-US```
3. Open the GPO editor (restart if open previously) and navigate to **Computer Configuration** > **Policies** > **Administrative Templates** > **Yubico** >
4. Double-click on 'YubiKey Removal Behavior' and adjust settings as required
5. Click **Apply** and **OK**.

**Note**: To use the ADMX with Intune, please refer to instructions on [swjm.blog](https://swjm.blog/locking-the-workstation-on-fido2-security-key-removal-part-2-80962c944c78)

### ⚙️ Registry Keys

The YKRB installer creates the following registry entries.  
(You can modify these directly if needed, though using supported tools is recommended.)

```reg
Windows Registry Editor Version 5.00

; Add application to startup
[HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Run]
"YubiKey Removal Behavior"="C:\\Program Files\\Yubico\\YubiKey Removal Behavior\\YubiKeyRemovalBehavior.exe"

; Removal behavior options: "lock", "logout", "disabled"
[HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Yubico\YubiKey Removal Behavior]
"removalOption"="lock"

; Event Log source registration
[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\EventLog\Application\YubiKey Removal Behavior]
"TypesSupported"=dword:00000007
"EventMessageFile"=hex(2):43,00,3a,00,5c,00,50,00,72,00,6f,00,67,00,72,00,61,\
  00,6d,00,20,00,46,00,69,00,6c,00,65,00,73,00,5c,00,59,00,75,00,62,00,69,00,\
  63,00,6f,00,5c,00,59,00,75,00,62,00,69,00,4b,00,65,00,79,00,20,00,52,00,65,\
  00,6d,00,6f,00,76,00,61,00,6c,00,20,00,42,00,65,00,68,00,61,00,76,00,69,00,\
  6f,00,72,00,5c,00,59,00,6b,00,72,00,62,00,4d,00,73,00,67,00,36,00,34,00,2e,\
  00,64,00,6c,00,6c,00,00,00

```
### 📊 Event Viewer integration
The YKRB application logs removal events to the Windows Event Log, under **Windows Logs** > **Application** > **YubiKey Removal Behavior** (source). The following events are logged:

----------------------------------------------------------------------------------------------
| Event ID | Description                                                       | Level       |
|----------|-------------------------------------------------------------------|-------------|
| 1        | Application started.                                              | Information |
| 256      | YubiKey removed (locking the workstation).                        | Information |
| 257      | YubiKey removed (logging off the current user).                   | Information |
| 258      | YubiKey removed with no action taken.                             | Information |
----------------------------------------------------------------------------------------------

## 📖 Usage
By default (no configuration required), the application will _lock_ the workstation on YubiKey removal. 
This behavior can be modified to instead _log out_ the user(s) OR _disabling_ the functionality. 

_Note further with regards to U/X:_

#### Removal from USB port
- By default, the application will _lock_ the workstation if the YubiKey is removed from the USB port.
- If ```Log out user(s)``` is configured, any logged in user will be _logged out_ from Windows.
- To log back in, the user _reinserts_ the YubiKey into the USB port and provides PIN and Touch.

#### Removal using NFC reader
_With NFC it is possible to achieve a "tap 'n go" type user experience:_

⚠️ If you are using NFC you MUST set application behavior to ```Log out user(s)``` (registry value: ```logout```) else you will be logged out immediately on login(!)

- To log out, the user "taps" or places the YubiKey on the NFC reader.
- To log back in, the user will place the YubiKey on the NFC reader and provide PIN.

## 📖 Roadmap
Possible improvements includes:
- ~~Using variables and/or relative paths in the installer (paths, registry keys)~~
- ~~Reducing overall footprint / size of of application~~
- ~~Code signing~~

## 🥷🏻 Contributing
You can help by getting involved in the project, _or_ by donating (any amount!).   
Donations will support costs such as domain registration and code signing (planned).

[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/donate/?business=RXAPDEYENCPXS&no_recurring=1&item_name=Help+cover+costs+of+the+SWJM+blog+and+app+code+signing%2C+supporting+a+more+secure+future+for+all.&currency_code=USD)


## 📜 Release History
* 2025.08.24 `v2.3`
* 2025.01.06 `v2.2`
* 2023.08.30 `v2.1`
* 2023.08.26 `v2.0`
* 2022.12.27 `v1.0`
