using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType { None, DartMonkey }

public class Player : MonoBehaviour
{
    public PlayerType playerType;

    /// <summary>
    /// How fast the player moves.
    /// </summary>
    public virtual float Speed { get; } = 4f;
    /// <summary>
    /// How many times a second the player attacks.
    /// </summary>
    public virtual float AttackSpeed { get; } = 1f;
    /// <summary>
    /// The amount of damage each attack deals.
    /// </summary>
    public virtual float Damage { get; } = 1f;
    /// <summary>
    /// How many times an attack can pierce.
    /// </summary>
    public virtual float Pierce { get; } = 0f;
    /// <summary>
    /// How high the player jumps.
    /// </summary>
    public virtual float JumpHeight { get; } = 20f;

    public virtual Projectile Projectile { get; }

    public virtual float ProjectileLifeTime { get; } = 4f;

    public virtual float ProjectileSpeed { get; } = 2f;

    public virtual float ProjectileMass { get; } = 0.2f;

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

        Projectile newProjectile = Instantiate(Projectile, cam.transform.position, transform.rotation);

        newProjectile.Init(ProjectileSpeed, ProjectileMass, Damage, Pierce);

        newProjectile.Fire(transform.rotation);
        Destroy(newProjectile.gameObject, ProjectileLifeTime);
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
            rb.AddForce(jump * JumpHeight * Vector3.up);
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
}
