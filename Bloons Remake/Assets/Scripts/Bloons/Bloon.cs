using UnityEngine.AI;
using UnityEngine;

public enum BloonType { None, Red, Blue, Green, Yellow, Pink, Rainbow, Ceramic }

/// <summary>
/// Base class for all bloons.
/// </summary>
public class Bloon : MonoBehaviour
{
    public BloonType bloonType = BloonType.None;
    public float speed = 1f;
    public Transform target;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// The speed of the bloon.
    /// </summary>
    public float Speed { get; private set; }

    /// <summary>
    /// How much damage it takes to pop the bloon.
    /// </summary>
    public float Health { get; private set; } = 1;

    private void Update()
    {
        if (!agent || !target) { return; }

        agent.speed = speed;
        if (!agent.SetDestination(target.transform.position))
        {
            Debug.LogWarning(name + "failed to set destination.");
        }
    }
}
