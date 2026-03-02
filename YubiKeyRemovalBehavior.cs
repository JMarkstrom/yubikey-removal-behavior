/*
##########################################################################
# YubiKey Removal Behavior     
##########################################################################
* version: 2.4
* created by: Jonas Markström (https://swjm.blog)
* last updated on: 2026-02-25 by Jonas Markström

* see readme.md for more info.
*
* DESCRIPTION: This code listens for YubiKey removal events and takes action 
* when a YubiKey is removed from the computer. When a YubiKey is removed, 
* the code retrieves the value of the "action" registry key:
* 
*  - If the value of the "action" key is 'lock', the code locks the workstation. 
*  - If the value of the registry key is 'logout', the code logs out the user.
*  - If the value of the registry key is 'disabled', the code does nothing.
* 
* The registry key is set either by an MSI installer and/or using Group Policy.
*
* Proprietary software. See LICENSE file for details.
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

using System.Threading;

namespace Yubico;

// Class that listens for YubiKey removal events and takes action when a YubiKey is removed
class YubiKeyRemovalBehavior
{

    public static void Main()
    {
        new RemovalBehavior();
        Thread.Sleep(Timeout.Infinite);
    }

}