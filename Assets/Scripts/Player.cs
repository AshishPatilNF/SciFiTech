using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f;

    float jumpHeight = 20f;

    float customGravity = 1f;

    float yVelocity = 0;

    bool canDoubleJump = false;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementJump();
    }

    private void MovementJump()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 customVelocity = moveDirection * speed;

        if (characterController.isGrounded)
        {
            canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
                yVelocity = jumpHeight;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                canDoubleJump = false;
                yVelocity += jumpHeight;
            }

            yVelocity -= customGravity;
        }

        customVelocity.y = yVelocity;
        customVelocity = transform.TransformDirection(customVelocity);
        characterController.Move(customVelocity * Time.deltaTime);
    }
}
