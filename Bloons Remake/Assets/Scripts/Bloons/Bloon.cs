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
    public int Index = 0;
    public MeshRenderer mesh;

    private NavMeshAgent agent;
    private BloonHirachy bloonHirachy;

    public BloonHirachy.BloonInfo BloonInfo { get; private set; }

    public void Init()
    {
        agent = GetComponent<NavMeshAgent>();
        target = Main.Current.SceneGrabber.Player.transform;

        bloonHirachy = Main.Current.Hirachy;
        GetBloonInfo();
    }

    private void GetBloonInfo()
    {
        if (!bloonHirachy) { return; }

        BloonInfo = bloonHirachy.GetBloonInfo(bloonType);
        SetInfo();
    }

    private void SetInfo()
    {
        if (BloonInfo == null) { return; }

        speed = BloonInfo.speed;
        Health = BloonInfo.health;
        Index = BloonInfo.BloonIndex;

        if (!mesh) { return; }

        mesh.material = BloonInfo.material;
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
            Debug.LogWarning(name + " failed to set destination.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponentInParent<Projectile>();

        if (!projectile) { return; }

        OnBloonDamaged(projectile.damage);
    }

    public void OnBloonDamaged(float damage)
    {
        if (damage <= 0) { return; }

        for (int i = 0; i < damage; i++) // We loop through the amount of damage it takes
        {
            Health -= 1; // We subtract 1 health per loop
            if (Health <= 0) // We check if Health is zero
            {
                if (Index <= 0) // If this is the first bloon, destroy it
                {
                    Destroy(gameObject);
                }
                else
                {
                    Index -= 1; // Index gets reduced by one
                    BloonInfo = bloonHirachy.Hirachy[Index]; // Then set current bloon to new index
                    SetInfo();
                    continue; // Continue loop
                }
            }
        }
    }
}
