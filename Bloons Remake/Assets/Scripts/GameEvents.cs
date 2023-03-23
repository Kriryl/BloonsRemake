using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void Bloon(float bloon);

    /// <summary>
    /// Called when a bloon has been spawned.
    /// </summary>
    public event Bloon OnBloonSpawn;

    /// <summary>
    /// Called when any bloon has been popped.
    /// </summary>
    public event Bloon OnBloonPopped;
}
