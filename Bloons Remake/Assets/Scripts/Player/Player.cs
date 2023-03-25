using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float attackSpeed = 1f;
    public float damage = 1f;
    public float pierce = 0f;
    public float jumpHeight = 2f;
    public float lookSensitivity = 1f;
    public float jumpDuration = 1f;
    public bool isGrounded = true;

    [Header("Projectile")]
    public Projectile projectile;
    public float projectileLifeTime = 5f;
    public float projectileSpeed = 800f;
    public float projectileMass = 0.6f;

    private Camera cam;
    private Rigidbody rb;
    private MouseLooker mouseLooker;
    private CapsuleCollider playerCollider;

    private float nextTimeToFire = 0f;

    public float AttackSpeedMultiplier { get; set; } = 1f;

    public float AttackSpeed => attackSpeed * AttackSpeedMultiplier;

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
        if (!projectile) { return; }

        Projectile newProjectile = Instantiate(projectile, cam.transform.position, transform.rotation);

        newProjectile.Init(projectileSpeed, projectileMass, damage, pierce);

        newProjectile.Fire(transform.rotation);
        Destroy(newProjectile.gameObject, projectileLifeTime);
    }

    private void MovePlayer()
    {
        if (!rb) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        Vector3 movement = new(horizontal * speed, 0f, vertical * speed);

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

    private void RotatePlayer()
    {
        mouseLooker.LookRotation(transform, cam.transform);
    }

    private void CheckIfGrounded()
    {
        if (!playerCollider) { return; }
    }
}
