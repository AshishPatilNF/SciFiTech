using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaleWeapon : MonoBehaviour
{
    int weaponCost = 20;

    SharkShopkeeper shopKeeper;

    private void Start()
    {
        shopKeeper = FindObjectOfType<SharkShopkeeper>();
    }

    public GameObject BuyWeapon()
    {
        Destroy(this.gameObject);
        return shopKeeper.PurchasedWeapon();
    }

    public int WeaponCost()
    {
        return weaponCost;
    }
}
