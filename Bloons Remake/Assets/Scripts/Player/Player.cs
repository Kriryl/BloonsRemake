using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { None, DartMonkey }

public class Player : MonoBehaviour
{
    public PlayerType playerType;
    public float jumpHeight;
    public int lives = 100;
    public float speed = 4f;

    /// <summary>
    /// How fast the player moves (In units/second).
    /// </summary>
    public float Speed => speed;

    /// <summary>
    /// How many times a second the player shoots.
    /// </summary>
    public float AttackSpeed { get; set; } = 1f;

    /// <summary>
    /// How many layers of damage the projectile deals.
    /// </summary>
    public float Damage { get; set; } = 1f;

    /// <summary>
    /// How many times the projectile can pierce.
    /// </summary>
    public int Pierce { get; set; } = 1;

    /// <summary>
    /// The projectile prefab.
    /// </summary>
    public Projectile Projectile { get; set; }

    /// <summary>
    /// How fast the projectile will be shot at.
    /// </summary>
    public float ProjectileSpeed { get; set; } = 1000f;

    /// <summary>
    /// The mass of the projectile.
    /// </summary>
    public float ProjectileMass { get; set; } = 0.5f;

    /// <summary>
    /// The lifetime of the projectile in seconds.
    /// </summary>
    public float ProjectileLifeTime { get; set; } = 1f;

    /// <summary>
    /// The location(s) the projectile will spawn
    /// </summary>
    public List<Transform> ProjectileSpawns { get; set; } = new();

    public bool isGrounded = true;

    private Camera cam;
    private Rigidbody rb;
    private MouseLooker mouseLooker;
    private CapsuleCollider playerCollider;

    private float nextTimeToFire = 0f;

    public float AttackSpeedMultiplier { get; set; } = 1f;

    public float TotalAttackSpeed => AttackSpeed * AttackSpeedMultiplier;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        mouseLooker = GetComponent<MouseLooker>();
        playerCollider = GetComponent<CapsuleCollider>();

        mouseLooker.Init(transform, cam.transform);

        nextTimeToFire = Time.time + (1 / AttackSpeed);

        ProjectileSpawns.Add(cam.transform);

        SetupPlayer();
    }

    public virtual void SetupPlayer()
    {

    }

    private void Update()
    {
        RotatePlayer();
        CheckIfGrounded();
        CheckForAttack();
        MovePlayer();
    }

    private void CheckForAttack()
    {
        if (Time.time >= nextTimeToFire && Input.GetMouseButton(0))
        {
            Fire();
            nextTimeToFire = Time.time + (1 / AttackSpeed);
        }
    }

    private void Fire()
    {
        if (!Projectile) { return; }

        foreach (Transform t in ProjectileSpawns)
        {
            Projectile newProjectile = Instantiate(Projectile, t.position, transform.rotation);

            newProjectile.Init(ProjectileSpeed, ProjectileMass, Damage, Pierce);

            newProjectile.Fire(transform.rotation, cam.transform.rotation);
            Destroy(newProjectile.gameObject, ProjectileLifeTime);
        }
    }

    private void MovePlayer()
    {
        if (!rb) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        Vector3 movement = new(horizontal * Speed, 0f, vertical * Speed);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= 2;
        }

        transform.Translate(movement * Time.deltaTime);

        if (jump > Mathf.Epsilon && isGrounded)
        {
            rb.AddForce(jump * jumpHeight * Vector3.up);
        }
    }

    public void AddProjectile(Vector3[] pos)
    {
        foreach (Vector3 p in pos)
        {
            GameObject g = new("Spawn");
            g.transform.SetParent(transform);
            g.transform.localPosition = p;

            ProjectileSpawns.Add(g.transform);
        }
    }

    private void RotatePlayer()
    {
        mouseLooker.LookRotation(transform, cam.transform);
    }

    private void CheckIfGrounded()
    {
        if (!playerCollider) { return; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Bloon b)) { return; }

        lives -= b.RBE;
        b.OnBloonPopped(0f);
    }
}
