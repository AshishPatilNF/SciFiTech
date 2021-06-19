using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f;

    float jumpHeight = 100f;

    float customGravity = 9.8f;

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
        characterController.Move(customVelocity * Time.deltaTime);
    }
}
