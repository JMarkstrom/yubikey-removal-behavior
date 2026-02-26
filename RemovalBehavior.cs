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
* Licensed under BSD 2-Clause License. See LICENSE file for details.
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
        public const int REMOVED_NOACTION  = unchecked((int)0x40010102);
        public const int LOCK_FAILED       = unchecked((int)0x80010103);
        public const int LOGOUT_FAILED     = unchecked((int)0x80010104);
    }
    public class RemovalBehavior
    {
        // Policy path for settings (value name: "removalOption")
        private const string REGISTRY_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Yubico\YubiKey Removal Behavior";

        // Event Log source (must be created by MSI and mapped to your message DLL)
        private const string EVENT_LOG_SOURCE = "YubiKey Removal Behavior";

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LockWorkStation();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        private const uint EWX_LOGOFF = 0x00;

        private readonly YubiKeyDeviceListener yubiKeyDeviceListener = YubiKeyDeviceListener.Instance;

        public RemovalBehavior()
        {
            // Log app starting to Event Viewer
            LogEvent(EventLogEntryType.Information, EventIds.APP_STARTED);
            yubiKeyDeviceListener.Removed += YubiKeyRemoved;
        }

        private void YubiKeyRemoved(object? sender, YubiKeyDeviceEventArgs eventArgs)
        {
            string option = Registry.GetValue(REGISTRY_KEY, "removalOption", null) as string
                            ?? "lock";

            switch (option.ToLowerInvariant())
            {
                case "logout":
                    LogEvent(EventLogEntryType.Information, EventIds.REMOVED_LOGOUT);
                    if (!ExitWindowsEx(EWX_LOGOFF, 0))
                    {
                        int err = Marshal.GetLastWin32Error();
                        LogEvent(EventLogEntryType.Warning, EventIds.LOGOUT_FAILED, err.ToString());
                    }
                    break;

                case "disabled":
                    LogEvent(EventLogEntryType.Information, EventIds.REMOVED_NOACTION);
                    break;

                default: // "lock" or any unrecognized value
                    LogEvent(EventLogEntryType.Information, EventIds.REMOVED_LOCK);
                    if (!LockWorkStation())
                    {
                        int err = Marshal.GetLastWin32Error();
                        LogEvent(EventLogEntryType.Warning, EventIds.LOCK_FAILED, err.ToString());
                    }
                    break;
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
            catch { /* TODO optional: fallback to log file */ }
        }

    }
}
