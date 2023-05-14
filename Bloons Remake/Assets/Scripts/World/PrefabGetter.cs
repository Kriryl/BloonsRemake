using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGetter : MonoBehaviour
{
    public Projectile dartMonkeyProjectile;
    public Projectile dartMonkeySpikedBall;
    public Projectile dartMonketCrossbow;
    public Bloon baseBloon;

    public static Projectile DartMonkeyProjectile { get; private set; }

    public static Projectile DartMonkeySpikedBall { get; private set; }

    public static Projectile DartMonkeyCrossbow { get; private set; }

    public static Bloon BaseBloon { get; private set; }

    private void Awake()
    {
        DartMonkeyProjectile = dartMonkeyProjectile;
        DartMonkeySpikedBall = dartMonkeySpikedBall;
        DartMonkeyCrossbow = dartMonketCrossbow;
        BaseBloon = baseBloon;
    }
}
