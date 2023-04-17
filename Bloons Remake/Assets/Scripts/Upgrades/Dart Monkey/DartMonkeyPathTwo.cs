using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUpgrades.Paths
{
    public class DartMonkeyPathTwo : UpgradePath
    {
        public override void OnUpgrade(Upgrade currentUpgrade)
        {
            Player.AttackSpeed *= currentUpgrade.fValue;
            base.OnUpgrade(currentUpgrade);
        }
    }
}
