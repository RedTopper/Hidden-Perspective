﻿using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour
{
    //Body configuration
    public float offset = 0.5f;
    public float dist = -0.1f;

    //Player configuration
    public float speed = 0.2f;
    public float sensitivityMouse = 10f;
    public float sensitivityJoy = 10f;
    public float maxYAngle = 80f;

    private GameObject obs;
    private GameObject body;
    private GameObject cam;
    private Vector2 currentRotation;
    private bool VREnabled = false;

    void Start ()
    {
        body = GameObject.Find("Player/Body");
        cam = GameObject.Find("Player/Cam");
        obs = GameObject.Find("Observer/Cam");

        if (XRSettings.enabled && XRDevice.isPresent)
        {
            VREnabled = true;
        }
        else
        {
            XRSettings.enabled = false;
            VREnabled = false;
            obs.GetComponent<Camera>().rect = new Rect(0.78f, 0f, 0.22f, 1.0f);
        }
    }

    void Update()
    {
        Vector3 dir = new Vector3();
        if (VREnabled)
        {
            //movement
            Vector2 left = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            dir = new Vector3(left.x, 0.0f, left.y).normalized * speed;

            //look
            gameObject.transform.RotateAround(cam.transform.position, Vector3.up, OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x * sensitivityJoy);
        }
        else
        {
            //movement
            dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized * speed;

            //look
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivityMouse;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivityMouse;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
            cam.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        //Find yaw
        Vector3 pos = cam.transform.position;
        Vector3 angle = cam.transform.rotation.eulerAngles;
        angle.x = 0;
        angle.z = 0;
        Quaternion yaw = Quaternion.Euler(angle);
        
        //Move the body
        body.transform.position = pos + (yaw * Vector3.forward) * dist - new Vector3(0, offset, 0);

        //Move the player
        gameObject.GetComponent<Rigidbody>().velocity = yaw * dir;
    }

    public bool IsVREnabled()
    {
        return VREnabled;
    }
}
