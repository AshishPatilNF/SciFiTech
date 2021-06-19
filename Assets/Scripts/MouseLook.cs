using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    Transform player;

    float mouseSensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * mouseSensitivity * Time.deltaTime;
        Vector3 playerCustomEulers = player.localEulerAngles;
        Vector3 cameraCustomEulers = transform.localEulerAngles;

        playerCustomEulers.y += lookDirection.y;
        player.localEulerAngles = playerCustomEulers;

        cameraCustomEulers.x -= Mathf.Clamp(lookDirection.x, -90f, 90f);
        transform.localEulerAngles = cameraCustomEulers;
    }
}
