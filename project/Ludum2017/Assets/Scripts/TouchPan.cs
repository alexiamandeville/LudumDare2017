using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPan : MonoBehaviour {

#if UNITY_ANDROID || UNITY_IPHONE
    // Update is called once per frame
    void Update () {
        foreach (Touch touch in Input.touches) //touch with phone
        {

            float rotationSpeed = 0.2f;
            Touch touch0 = Input.GetTouch(0);
            float mouseX = touch0.deltaPosition.x * rotationSpeed;
            float mouseY = touch0.deltaPosition.y * rotationSpeed;

            transform.localRotation = Quaternion.Euler(0, -mouseX, 0) * transform.localRotation;
            gameObject.transform.localRotation = Quaternion.Euler(mouseY, 0, 0) * gameObject.transform.localRotation;

        }
#endif

#if UNITY_EDITOR
        if (Input.GetMouseButton(0)) //mouse for desktop
        {
            float rotationSpeed = 1.8f;
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.localRotation = Quaternion.Euler(0, -mouseX, 0) * transform.localRotation;
            gameObject.transform.localRotation = Quaternion.Euler(mouseY, 0, 0) * gameObject.transform.localRotation;
        }
#endif
    }
}
