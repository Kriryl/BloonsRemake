using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUpgrades.Paths
{
    public class DartMonkeyPathOne : UpgradePath
    {
        public override void OnUpgrade(Upgrade currentUpgrade)
        {
            switch (Index)
            {
                case 2:
                    Player.Projectile = PrefabGetter.DartMonkeySpikedBall;
                    Player.AttackSpeed -= currentUpgrade.fValue;
                    break;
                default:
                    break;
            }

            Player.Pierce += currentUpgrade.iValue;
            base.OnUpgrade(currentUpgrade);
        }
    }
}
