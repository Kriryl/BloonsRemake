using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUpgrades.Paths
{
    public class DartMonkeyPathOne : UpgradePath
    {
        public override void OnUpgrade(Upgrade currentUpgrade)
        {
            Player.Pierce += currentUpgrade.iValue;
            base.OnUpgrade(currentUpgrade);
        }
    }
}
