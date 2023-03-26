using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabGetter : MonoBehaviour
{
    public Projectile dartMonkeyProjectile;

    public static Projectile DartMonkeyProjectile { get; private set; }

    private void Awake()
    {
        DartMonkeyProjectile = dartMonkeyProjectile;
    }
}
