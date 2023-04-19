using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkey : Player
{
    public override void SetupPlayer()
    {
        base.SetupPlayer();

        Projectile = PrefabGetter.DartMonkeyProjectile;
        ProjectileLifeTime = 1f;
        ProjectileMass = 0.2f;
        Pierce = 0;
    }
}
