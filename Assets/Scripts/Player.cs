using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    //Set in configuration
    public float speed;
    public float sensitivity = 10f;
    public float maxYAngle = 80f;
    private Vector2 currentRotation;

    private bool VREnabled = false;

    void Start ()
    {
        if (XRSettings.enabled && XRDevice.isPresent)
        {
            VREnabled = true;
        }
        else
        {
            XRSettings.enabled = false;
            VREnabled = false;
        }
    }

    

    void Update()
    {
        //movement
        Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * speed;
        transform.position += dir;

        //look
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
        GameObject.Find("CameraMain").transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public bool IsVREnabled()
    {
        return VREnabled;
    }
}
