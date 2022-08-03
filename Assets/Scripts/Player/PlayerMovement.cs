using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] Weapon weaponScript;
    [Header("Settings")]
    
    [SerializeField] float playerSpeed = 5.0f;
    [SerializeField] float sneakingSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;

    CharacterController controller;
    public bool groundedPlayer;
    bool isAiming;
    Vector3 playerVelocity;
    float movementSpeed;
    

  

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        isAiming = weaponScript.aiming;

        if (isSneaking || isAiming) movementSpeed = sneakingSpeed;
        else movementSpeed = playerSpeed;
        

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * movementSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public bool isSneaking {
        get {
            if (Input.GetKey(KeyCode.LeftShift)) return true;
            else return false;
        }
    }
}

