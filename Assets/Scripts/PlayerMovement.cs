using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    
    

    public float speed = 8f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        controller.height = 3f;

        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -1f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            controller.height = 1.5f;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.4f, transform.position.z);
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.height = 3f;
            
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 2f;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 8f;
        }
        
        



        velocity.y += gravity * 2f * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);



    }
}
