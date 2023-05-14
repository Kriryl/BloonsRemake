using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 2f;
    public float mass = 1f;
    public float damage = 1f;
    public float pierce = 1f;

    private float usedPierce = 0f;

    public Rigidbody RigidBody { get; set; }

    public List<int> trackIDs = new();

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody>();
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

    public void Fire(Quaternion playerRotation, Quaternion cameraRotation)
    {
        RigidBody = GetComponentInChildren<Rigidbody>();

        if (!RigidBody) { return; }

        Quaternion totRotation = new(cameraRotation.x, playerRotation.y, cameraRotation.z, playerRotation.w);

        Vector3 direction = totRotation * Vector3.forward * speed;

        RigidBody.AddForce(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Bloon>()) { return; }

        usedPierce++;
        if (usedPierce > pierce)
        {
            Destroy(gameObject);
        }
    }
}
