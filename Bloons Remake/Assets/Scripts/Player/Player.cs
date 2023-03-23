using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 2f;
    public float lookSensitivity = 1f;
    public float jumpDuration = 1f;
    public bool isGrounded = true;

    private Camera cam;
    private Rigidbody rb;
    private MouseLooker mouseLooker;
    private CapsuleCollider playerCollider;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        mouseLooker = GetComponent<MouseLooker>();
        playerCollider = GetComponent<CapsuleCollider>();

        mouseLooker.Init(transform, cam.transform);
    }

    private void Update()
    {
        RotatePlayer();
        CheckIfGrounded();
        MovePlayer();
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
