using UnityEngine.AI;
using UnityEngine;
using UnityEngine.ProBuilder;

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
    public Color color;
    public ProBuilderMesh pbm;
    public Mesh mesh;

    private NavMeshAgent agent;
    private BloonHirachy bloonHirachy;

    public BloonHirachy.BloonInfo BloonInfo { get; private set; }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GetBloonInfo();
    }

    private void GetBloonInfo()
    {
        bloonHirachy = Main.Current.Hirachy;

        if (!bloonHirachy) { return; }

        BloonInfo = bloonHirachy.GetBloonInfo(bloonType);
        SetInfo();
    }

    private void SetInfo()
    {
        speed = BloonInfo.speed;
        Health = BloonInfo.health;
        Index = BloonInfo.BloonIndex;
        if (!pbm) { return; }

        color = BloonInfo.color;

        mesh.colors[0] = color;
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
