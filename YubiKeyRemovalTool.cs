/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 1.1.0
* created by: Jonas Markström (https://swjm.blog)
* last updated on: 2023-08-232 by Jonas Markström
* see readme.md for more info.
*
* DESCRIPTION: This code listens for YubiKey removal events and takes action 
* when a YubiKey is removed from the computer. When a YubiKey is removed, 
* the code retrieves the value of the "isEnabled" registry key. 
* If the value of the "isEnabled" key is 1, the code locks the workstation. 
* If the value of the registry key is any other value, the code does nothing.
* The registry key is set either by an MSI installer and/or using Group Policy.
*
* Licensed under the Apache License, Version 2.0 (the "License").
* You may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
****************************************************************************
* DISCLAIMER: Unless required by applicable law or agreed to in writing, 
* software distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
****************************************************************************
*
*/

namespace Yubico
{
    using System.Runtime.InteropServices;
    using Yubico.YubiKey;
    using Microsoft.Win32;

    public class YubiKeyRemovalTool
    {

        // Registry key that stores the service settings, e.g. whether the service is enabled or not
        private const string REGISTRY_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Yubico\Lock workstation on YubiKey removal";

        // Import the LockWorkStation method from the user32.dll library
        //[DllImport("user32.dll")]
        //public static extern void LockWorkStation();
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason); //Per's code

        // Set the Device Listener from Yubico SDK
        private YubiKeyDeviceListener yubiKeyDeviceListener = YubiKeyDeviceListener.Instance;

        public YubiKeyRemovalTool()
        {
            // Register the YubiKeyRemoved method as the event handler for the Removed event
            yubiKeyDeviceListener.Removed += YubiKeyRemoved;
        }

        private void YubiKeyRemoved(object? sender, YubiKeyDeviceEventArgs eventArgs)
        {
            // Retrieve the value of the "isEnabled" registry key
            int isEnabled = (int)Registry.GetValue(REGISTRY_KEY, "isEnabled", 1);
            if (isEnabled == 1)
            {
                // Lock the workstation on YubiKey removal
                //LockWorkStation();

                /**
                * Log out the current user.
                **/
                ExitWindowsEx(0 | 0x00000004, 0);
            }
        }
    }
}