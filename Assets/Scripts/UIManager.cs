using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Slider ammoSlider;

    [SerializeField]
    TextMeshProUGUI ammoText;

    public void UpdateAmmo(int ammoCount)
    {
        ammoText.text = "" + ammoCount;
        ammoSlider.value = ammoCount;
    }
}
