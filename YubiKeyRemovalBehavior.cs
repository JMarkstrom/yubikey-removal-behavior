/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 1.0
* created by: 
* last updated on: 2022-12-27 by Jonas Markström
* see readme.md for more info.
*
* DESCRIPTION: This code listens for YubiKey removal events and takes action 
* when a YubiKey is removed from the computer. When a YubiKey is removed, 
* the code retrieves the value of the "isEnabled" registry key located at: 
* 'HKEY_LOCAL_MACHINE\SOFTWARE\Yubico\Lock workstation on YubiKey removal'. 
* If the value of the "isEnabled" key is 1, the code locks the workstation. 
* If the value of the registry key is any other value, the code does nothing.
*
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


namespace Yubico;

// Class that listens for YubiKey removal events and takes action when a YubiKey is removed
class YubiKeyRemovalBehavior
{
  
    public static void Main()
    {
        new YubiKeyRemovalTool();
        while (true)
        {
            Thread.Sleep(2);
        }
    }

 }