using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
    public float mass = 1f;
    public float damage = 1f;
    public float pierce = 1f;

    public Rigidbody RigidBody { get; set; }

    private void Start()
    {
        RigidBody = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (RigidBody)
        {
            RigidBody.mass = mass;
        }
    }

    public void Init(float speed, float mass, float damage, float pierce)
    {
        this.speed = speed;
        this.mass = mass;
        this.damage = damage;
        this.pierce = pierce;
    }

    public void Fire(Quaternion rotation)
    {
        RigidBody = GetComponentInChildren<Rigidbody>();

        if (!RigidBody) { return; }

        Vector3 direction = rotation * Vector3.forward * speed;

        RigidBody.AddForce(direction);
    }
}
