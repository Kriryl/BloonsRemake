using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public SceneGrabber SceneGrabber { get; private set; }
    public BloonHirachy Hirachy { get; private set; }

    public static Main Current => FindObjectOfType<Main>();

    private void Awake()
    {
        SceneGrabber = GetComponent<SceneGrabber>();
        Hirachy = GetComponent<BloonHirachy>();
    }
}
