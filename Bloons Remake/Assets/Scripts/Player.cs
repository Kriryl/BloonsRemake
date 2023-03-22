using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float jumpHeight = 2f;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!controller) { return; }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float jump = Input.GetAxis("Jump");

        Vector3 movement = new(horizontal * speed, jump * jumpHeight, vertical * speed);

        controller.SimpleMove(movement);

        print(movement);

        print($"X: {horizontal}, Y: {vertical}, Jump: {jump}");
    }
}
