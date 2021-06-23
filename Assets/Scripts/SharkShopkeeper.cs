using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShopkeeper : MonoBehaviour
{
    [SerializeField]
    GameObject weaponForSale;

    [SerializeField]
    AudioClip boughtWeaponSound;

    public GameObject PurchasedWeapon()
    {
        AudioSource.PlayClipAtPoint(boughtWeaponSound, Camera.main.transform.position);
        return weaponForSale;
    }
}
