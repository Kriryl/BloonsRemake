using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for grabbing objects from the scene.
/// </summary>
public class SceneGrabber : MonoBehaviour
{
    [SerializeField] private List<Collider> grounds = new();

    /// <summary>
    /// The list of all walkable platforms.
    /// </summary>
    public List<Collider> Grounds => grounds;
}
