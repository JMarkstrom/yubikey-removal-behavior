/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 2.3
* created by: Jonas Markström (https://swjm.blog)
* last updated on: 2025-08-24 by Jonas Markström

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
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Win32;
    using Yubico.YubiKey;
    using System.Diagnostics;



    internal static class EventIds
{
    public const int APP_STARTED       = unchecked((int)0x40010001);
    public const int APP_STOPPED       = unchecked((int)0x40010002);
    public const int REMOVED_LOCK      = unchecked((int)0x40010100);
    public const int REMOVED_LOGOUT    = unchecked((int)0x40010101);
    public const int REMOVED_NOACTION  = unchecked((int)0x80010102);
}
    public class RemovalBehavior
    {
        // Policy path for settings (value name: "removalOption")
        private const string REGISTRY_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Yubico\YubiKey Removal Behavior";

        // Event Log source (must be created by MSI and mapped to your message DLL)
        private const string EVENT_LOG_SOURCE = "YubiKey Removal Behavior";

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void LockWorkStation();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        private readonly YubiKeyDeviceListener yubiKeyDeviceListener = YubiKeyDeviceListener.Instance;

        public RemovalBehavior()
        {
            // Log app starting to Event Viewer
            LogEvent(EventLogEntryType.Information, EventIds.APP_STARTED);
            yubiKeyDeviceListener.Removed += YubiKeyRemoved;
        }

        private void YubiKeyRemoved(object? sender, YubiKeyDeviceEventArgs eventArgs)
        {
            string removalOption = (string)(Registry.GetValue(REGISTRY_KEY, "removalOption", "lock") ?? "lock");

            if (string.Equals(removalOption, "lock", StringComparison.OrdinalIgnoreCase))
            {
                // Log removed with lock workstation action to Event Viewer
                LogEvent(EventLogEntryType.Information, EventIds.REMOVED_LOCK);
                LockWorkStation();
            }
            else if (string.Equals(removalOption, "logout", StringComparison.OrdinalIgnoreCase))
            {
                // Log removed with logout workstation action to Event Viewer
                LogEvent(EventLogEntryType.Information, EventIds.REMOVED_LOGOUT);
                ExitWindowsEx(0x00000000 | 0x00000004, 0);       // EWX_LOGOFF | EWX_FORCEIFHUNG
            }
            else
            {
                // Log removed with no action to Event Viewer
                LogEvent(EventLogEntryType.Warning, EventIds.REMOVED_NOACTION, removalOption);
            }
        }

        private static void LogEvent(EventLogEntryType type, int messageId, params string[] inserts)
        {
            try
            {
                var instance = new EventInstance(messageId, 0, type);
                object[] parameters = inserts?.Length > 0
                    ? Array.ConvertAll(inserts, s => (object)s)
                    : Array.Empty<object>();
                EventLog.WriteEvent(EVENT_LOG_SOURCE, instance, parameters);
            }
            catch { /* TODO optional: file fallback */ }
        }

    }
}
