using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int currentAmmo;

    int coins = 0;

    int maxAmmo = 25;

    float speed = 5f;

    float jumpHeight = 20f;

    float customGravity = 1f;

    float yVelocity = 0;

    float holdFireTime = 0.5f;

    float nextFire = 0;

    bool canDoubleJump = false;

    bool holdingWeapon = false;

    [SerializeField]
    GameObject activeWeapon;

    [SerializeField]
    GameObject muzzleFlash;

    [SerializeField]
    GameObject hitMarker;

    [SerializeField]
    AudioSource weaponAudioSource;

    CharacterController characterController;

    Coroutine reloadingAmmo = null;

    UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        uiManager = FindObjectOfType<UIManager>();
        Cursor.lockState = CursorLockMode.Locked;
        muzzleFlash.SetActive(false);
        currentAmmo = maxAmmo;
        uiManager.UpdateAmmo(currentAmmo);
        uiManager.UpdateCoinCount(coins);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.G) && activeWeapon.transform.childCount > 0)
        {
            if (activeWeapon.activeSelf)
            {
                activeWeapon.SetActive(false);
                holdingWeapon = false;
            }
            else
            {
                activeWeapon.SetActive(true);
                holdingWeapon = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitInfo;
            
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.GetComponent<Coins>() && hitInfo.distance < 2f)
                {
                    coins += hitInfo.transform.GetComponent<Coins>().GetValue();
                    uiManager.UpdateCoinCount(coins);
                    nextFire = Time.time + holdFireTime;
                }
                else if (hitInfo.transform.GetComponent<SaleWeapon>() && hitInfo.distance < 2f)
                {
                    if (hitInfo.transform.GetComponent<SaleWeapon>().WeaponCost() <= coins)
                    {
                        coins -= hitInfo.transform.GetComponent<SaleWeapon>().WeaponCost();
                        uiManager.UpdateCoinCount(coins);
                        GameObject newWeapon = hitInfo.transform.GetComponent<SaleWeapon>().BuyWeapon();
                        newWeapon.transform.parent = activeWeapon.transform;
                        newWeapon.transform.localPosition = Vector3.zero;
                        newWeapon.transform.localRotation = Quaternion.identity;
                        newWeapon.transform.localScale = Vector3.one;
                        activeWeapon.SetActive(true);
                        holdingWeapon = true;
                    }
                    nextFire = Time.time + holdFireTime;
                }
                else if (currentAmmo > 0 && holdingWeapon && Time.time > nextFire)
                {
                    GameObject newHitmarker = Instantiate(hitMarker, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    Destroy(newHitmarker, 6f);
                }
            }
            
            if (currentAmmo > 0 && holdingWeapon && Time.time > nextFire)
            {
                muzzleFlash.SetActive(true);
                currentAmmo--;
                uiManager.UpdateAmmo(currentAmmo);
                
                if (!weaponAudioSource.isPlaying)
                    weaponAudioSource.Play();
            }
            else
            {
                muzzleFlash.SetActive(false);
                weaponAudioSource.Stop();
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
        uiManager.UpdateAmmo(currentAmmo);
        reloadingAmmo = null;
    }
}
