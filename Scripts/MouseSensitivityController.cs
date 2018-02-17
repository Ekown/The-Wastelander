using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseSensitivityController : MonoBehaviour {

    public Slider sensitivityValue;

    enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }

    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX;
    private float sensitivityY;

    private float minimumX  = -360f;
    private float maximumX = 360f;
 
    private float minimumY = -60f;
    private float maximumY = 60f;

    private float rotationX;
    private float rotationY;

    private Quaternion originalRotation;
    private Quaternion xQuaternion;
    private Quaternion yQuaternion;

    void Start()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
        originalRotation = transform.localRotation;

        sensitivityX = sensitivityValue.value;
        sensitivityY = sensitivityValue.value;
    }

    // Update is called once per frame
    void Update ()
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}
