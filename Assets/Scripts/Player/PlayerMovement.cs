using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] bool onGround;

    [Header("Drag")]
    [SerializeField] float drag;

    Vector3 moveDirection;

    float horizontalInput;
    float verticalInput;

    void Start() => rb.freezeRotation = true;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

       if (!onGround) {
            rb.drag = drag;
       }

         if (Input.GetKeyDown("space") && onGround)
        {
            rb.AddForce(Vector3.up * jumpSpeed * 100);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed, ForceMode.Acceleration);
        
    }

     void OnCollisionStay ()
    {
        onGround = true;
    }

}
