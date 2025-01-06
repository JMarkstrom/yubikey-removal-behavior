﻿/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 2.1
* created by: Jonas Markström (https://swjm.blog)
* last updated on: 2025-01-06 by Jonas Markström

* see readme.md for more info.
*
* DESCRIPTION: This code listens for YubiKey removal events and takes action 
* when a YubiKey is removed from the computer. When a YubiKey is removed, 
* the code retrieves the value of the "action" registry key:
* 
*  - If the value of the "action" key is 'lock', the code locks the workstation. 
*  - If the value of the registry key is 'logout', the code logs out the user.
*  - If the value of the registry key is any other value, the code does nothing.
* 
* The registry key is set either by an MSI installer and/or using Group Policy.
*
* Licensed under BSD 2-Clause License.
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
    using Microsoft.Win32;
    using Yubico.YubiKey;

    public class RemovalBehavior
    {

        // Registry key that stores the service settings, e.g. whether the service is enabled or not
        private const string REGISTRY_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Yubico\YubiKey Removal Behavior";

        // Import the LockWorkStation AND ExitWindows methods from the user32.dll library
        [DllImport("user32.dll")]
        public static extern void LockWorkStation();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        // Set the Device Listener from Yubico SDK
        private YubiKeyDeviceListener yubiKeyDeviceListener = YubiKeyDeviceListener.Instance;

        public RemovalBehavior()
        {
            // Register the YubiKeyRemoved method as the event handler for the Removed event
            yubiKeyDeviceListener.Removed += YubiKeyRemoved;
        }

        private void YubiKeyRemoved(object? sender, YubiKeyDeviceEventArgs eventArgs)
        {
            // Retrieve the value of the "removalOption" registry key
            string removalOption = (string)Registry.GetValue(REGISTRY_KEY, "removalOption", "lock");

            if (removalOption == "lock")
            {
                // Lock the workstation on YubiKey removal if the value in registry is 'lock'
                LockWorkStation();
            }

            else if (removalOption == "logout")
            {

                // Log out the current user if value in registry is 'logout'.
                ExitWindowsEx(0 | 0x00000004, 0);

                // If there is any other value, the application does nothing!
            }

        }
    }
}