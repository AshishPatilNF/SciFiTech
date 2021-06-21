using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    int value = 10;

    [SerializeField]
    AudioClip coinSound;

    public int GetValue()
    {
        AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position, 0.5f);
        Destroy(this.gameObject);
        return value;
    }
}
