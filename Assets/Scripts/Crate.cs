using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    GameObject brokenCrate;
 
    public void CrateHit()
    {
        GameObject newBrokenCrate = Instantiate(brokenCrate, transform.position, Quaternion.identity);
        Destroy(newBrokenCrate, 5f);
        Destroy(this.gameObject);
    }
}
