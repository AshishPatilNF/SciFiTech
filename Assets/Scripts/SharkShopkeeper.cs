using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkShopkeeper : MonoBehaviour
{
    [SerializeField]
    GameObject weaponForSale;

    public GameObject PurchasedWeapon()
    {
        return weaponForSale;
    }
}
