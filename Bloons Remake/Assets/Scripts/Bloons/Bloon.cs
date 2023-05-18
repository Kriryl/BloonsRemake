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
    public int children = 0;
    public Transform target;
    public int Index = 0;
    public MeshRenderer mesh;

    private NavMeshAgent agent;
    private BloonHirachy bloonHirachy;
    private Tracker tracker;
    private Spawner spawner;

    public float SpeedMultiplier { get; private set; } = 1f;

    /// <summary>
    /// The Red Bloon Equilivant (RBA). How many lives does it take when collided with?
    /// </summary>
    public int RBE { get; private set; }

    public BloonHirachy.BloonInfo BloonInfo { get; private set; }

    public void Init(int ID = 0)
    {
        agent = GetComponent<NavMeshAgent>();
        target = Main.Current.SceneGrabber.Player.transform;
        tracker = GetComponent<Tracker>();
        spawner = FindObjectOfType<Spawner>();

        spawner.bloons.Add(this);

        SpeedMultiplier = Main.Current.globalSpeedMultiplier;

        if (ID == 0)
        {
            tracker.SetID(GetInstanceID());
        }
        else
        {
            tracker.SetID(ID);
        }

        bloonHirachy = Main.Current.Hirachy;

        GetBloonInfo();
    }

    private void GetBloonInfo()
    {
        if (!bloonHirachy) { return; }

        BloonInfo = bloonHirachy.GetBloonInfo(bloonType);
        SetInfo();
    }

    public void SetBloonInfo(BloonHirachy.BloonInfo info)
    {
        BloonInfo = info;
        SetInfo();
    }

    private void SetInfo()
    {
        if (BloonInfo == null) { return; }

        speed = BloonInfo.speed;
        Health = BloonInfo.health;
        Index = BloonInfo.BloonIndex;
        children = BloonInfo.numOfChilden;
        RBE = BloonInfo.rbe;

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

        agent.speed = speed * SpeedMultiplier;
        agent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile projectile = other.GetComponent<Projectile>();

        if (!projectile) { return; }

        if (projectile.trackIDs.Contains(tracker.bloonID))
        {
            tracker.bloonID = GetInstanceID();
            return;
        }
        projectile.trackIDs.Add(tracker.bloonID);

        OnBloonDamaged(projectile.damage);
    }

    public void OnBloonDamaged(float damage)
    {
        if (damage <= 0) { return; }

        float remaining = damage;

        for (int i = 0; i < damage; i++) // We loop through the amount of damage it takes
        {
            Health -= 1; // We subtract 1 health per loop
            if (Health <= 0) // We check if Health is zero
            {
                if (Index <= 0) // If this is the first bloon, destroy it
                {
                    OnBloonPopped();
                    break;
                }
                else
                {
                    if (children > 0) // If this bloon spawns more bloons we remove this bloon
                    {
                        remaining--;
                        OnBloonSplit(remaining);
                        break;
                    }
                    else
                    {
                        Index -= 1; // Index gets reduced by one
                        BloonInfo = bloonHirachy.Hirachy[Index]; // Then set current bloon to new index
                        remaining--;
                        Main.Current.money += 1f;
                        SetInfo();
                    }
                }
            }
        }
    }

    private void OnBloonSplit(float remainingDamage)
    {

        if (children > 0 && Index > 0)
        {
            if (Index - remainingDamage < 0)
            {
                Main.Current.money += remainingDamage;
                spawner.bloons.Remove(this);
                Destroy(gameObject);
                return;
            }

            BloonHirachy.BloonInfo info = bloonHirachy.Hirachy[Index - (int)remainingDamage - 1]; // Currently only gets the prievious index
            for (int i = 0; i < children; i++) // We loop through all the children
            {
                Bloon b = Instantiate(PrefabGetter.BaseBloon, transform.position, transform.rotation); // We create a new base bloon
                b.Init(tracker.bloonID); // Finally init the new bloon
                b.SetBloonInfo(info); // And set it to the info
            }
        }

        OnBloonPopped(1f + remainingDamage);
    }

    public void OnBloonPopped(float money = 1f)
    {
        Main.Current.money += money;
        spawner.bloons.Remove(this);
        Destroy(gameObject);
    }
}
