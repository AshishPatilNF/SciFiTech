using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    int currentAmmo;

    int maxAmmo = 25;

    float speed = 5f;

    float jumpHeight = 20f;

    float customGravity = 1f;

    float yVelocity = 0;

    bool canDoubleJump = false;

    [SerializeField]
    GameObject muzzleFlash;

    [SerializeField]
    GameObject hitMarker;

    [SerializeField]
    AudioSource weaponAudioSource;

    CharacterController characterController;

    Coroutine reloadingAmmo = null;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        muzzleFlash.SetActive(false);
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetMouseButton(0) && currentAmmo > 0)
        {
            muzzleFlash.SetActive(true);
            currentAmmo--;

            if (!weaponAudioSource.isPlaying)
                weaponAudioSource.Play();
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("Hit :" + hitInfo.transform.name);
                GameObject newHitmarker = Instantiate(hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(newHitmarker, 6f);
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
            weaponAudioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (reloadingAmmo == null)
            {
                reloadingAmmo = StartCoroutine(ReloadingAmmo());
            }
        }

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

    IEnumerator ReloadingAmmo()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        currentAmmo = maxAmmo;
        reloadingAmmo = null;
    }
}
