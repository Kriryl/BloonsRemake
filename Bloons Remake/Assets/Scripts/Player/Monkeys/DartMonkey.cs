using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartMonkey : Player
{
    public override float AttackSpeed => 1f;

    public override float Damage => 1f;

    public override float Pierce => 1f;

    public override float Speed => 3f;

    public override float ProjectileSpeed => 1000f;

    public override Projectile Projectile => PrefabGetter.DartMonkeyProjectile;
}
