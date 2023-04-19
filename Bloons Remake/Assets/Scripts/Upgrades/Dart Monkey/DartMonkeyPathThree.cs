using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUpgrades.Paths
{
    public class DartMonkeyPathThree : UpgradePath
    {
        public override void OnUpgrade(Upgrade currentUpgrade)
        {
            switch (Index)
            {
                case 0:
                    Player.ProjectileLifeTime += currentUpgrade.fValue;
                    break;
                case 1:
                    Player.Damage += 2;
                    Player.ProjectileSpeed += 500f;
                    break;
                default:
                    break;
            }
            base.OnUpgrade(currentUpgrade);
        }
    }
}
