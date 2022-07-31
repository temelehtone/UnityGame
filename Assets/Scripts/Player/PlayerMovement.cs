using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private bool onGround;

    

    [Header("Drag")]
    [SerializeField] private float drag;

    private Vector3 moveDirection;

    private float horizontalInput;
    private float verticalInput;

    private void Start() => rb.freezeRotation = true;

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        rb.drag = drag;

         if (Input.GetKeyDown("space") && onGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed * 100);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed, ForceMode.Acceleration);
        
    }

     void OnCollisionStay ()
    {
        onGround = true;
    }

}
