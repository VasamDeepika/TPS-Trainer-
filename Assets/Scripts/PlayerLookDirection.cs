using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookDirection : MonoBehaviour
{
    private float rotateSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, (transform.localEulerAngles.y + mouseX)*rotateSpeed,
                                                 transform.localEulerAngles.z);
    }
}
