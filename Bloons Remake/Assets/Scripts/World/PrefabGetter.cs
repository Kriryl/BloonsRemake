using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGetter : MonoBehaviour
{
    public Projectile dartMonkeyProjectile;
    public Projectile dartMonkeySpikedBall;
    public Projectile dartMonkeySpikedBall2;
    public Projectile dartMonketCrossbow;
    public Bloon baseBloon;

    public static Projectile DartMonkeyProjectile { get; private set; }

    public static Projectile DartMonkeySpikedBall { get; private set; }

    public static Projectile DartMonkeySpikedBall2 { get; private set; }

    public static Projectile DartMonkeyCrossbow { get; private set; }

    public static Bloon BaseBloon { get; private set; }

    private void Awake()
    {
        DartMonkeyProjectile = dartMonkeyProjectile;
        DartMonkeySpikedBall = dartMonkeySpikedBall;
        DartMonkeySpikedBall2 = dartMonkeySpikedBall2;
        DartMonkeyCrossbow = dartMonketCrossbow;
        BaseBloon = baseBloon;
    }
}
