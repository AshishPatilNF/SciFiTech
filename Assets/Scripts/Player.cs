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

    [SerializeField]
    GameObject muzzleFlash;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        muzzleFlash.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetMouseButtonDown(0))
        {
            muzzleFlash.SetActive(true);
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
                Debug.Log("Hit :" + hitInfo.transform.name);
        }
        else if (Input.GetMouseButtonUp(0))
            muzzleFlash.SetActive(false);

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
