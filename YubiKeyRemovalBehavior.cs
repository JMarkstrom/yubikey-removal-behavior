/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 2.0.0
* created by: Jonas Markström (https://swjm.blog)
* last updated on: 2023-08-26 by Jonas Markström

* see readme.md for more info.
*
* DESCRIPTION: This code listens for YubiKey removal events and takes action 
* when a YubiKey is removed from the computer. When a YubiKey is removed, 
* the code retrieves the value of the "action" registry key:
* 
*  - If the value of the "action" key is '1', the code locks the workstation. 
*  - If the value of the registry key is '2', the code logs out the user.
*  - If the value of the registry key is any other value, the code does nothing.
* 
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


namespace Yubico;

// Class that listens for YubiKey removal events and takes action when a YubiKey is removed
class YubiKeyRemovalBehavior
{
  
    public static void Main()
    {
        //new YubiKeyRemovalTool();

        new RemovalBehavior();
        while (true)
        {
            Thread.Sleep(2);
        }
    }

 }