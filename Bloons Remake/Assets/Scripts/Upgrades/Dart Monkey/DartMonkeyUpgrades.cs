using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerUpgrades
{
    public class DartMonkeyUpgrades : Upgrades
    {
        [Header("Path One:")]
        public int pierceOne;
        public int pierceTwo;
        public int pierceThree;

        [Space]

        [Header("Path Two:")]
        public float attackSpeedOne;
        public float attackSpeedTwo;
        public float offsetTwo = 1f;

        [Space]

        [Header("Path Three:")]
        public float rangeOne;
        public float speedOne, damageTwo, speedTwo;

        public override void OnPathOneUpgrade(int index)
        {
            switch (index)
            {
                case 0:
                    Player.Pierce += pierceOne;
                    break;
                case 1:
                    Player.Pierce += pierceTwo;
                    break;
                case 2:
                    Player.Pierce += pierceThree;
                    Player.Projectile = PrefabGetter.DartMonkeySpikedBall;
                    break;
                case 3:
                    Player.Damage += 1f;
                    Player.ProjectileSpeed += 800f;
                    Player.AttackSpeed += 1f;
                    Player.Projectile = PrefabGetter.DartMonkeySpikedBall2;
                    break;
                default:
                    break;
            }
            base.OnPathOneUpgrade(index);
        }

        public override void OnPathTwoUpgrade(int index)
        {
            switch (index)
            {
                case 0:
                    Player.AttackSpeed += attackSpeedOne;
                    break;
                case 1:
                    Player.AttackSpeed += attackSpeedTwo;
                    break;
                case 2:
                    Vector3[] pos = { Vector3.right * offsetTwo, Vector3.left * offsetTwo };
                    Player.AddProjectile(pos);
                    break;
                default:
                    break;
            }

            base.OnPathTwoUpgrade(index);
        }

        public override void OnPathThreeUpgrade(int index)
        {
            switch (index)
            {
                case 0:
                    Player.ProjectileLifeTime += rangeOne;
                    Player.ProjectileSpeed += speedOne;
                    break;
                case 1:
                    Player.Damage += damageTwo;
                    Player.ProjectileSpeed += speedTwo;
                    Player.Projectile = PrefabGetter.DartMonkeyCrossbow;

                    break;
                case 2:
                    break;
                default:
                    break;
            }

            base.OnPathThreeUpgrade(index);
        }
    }
}
