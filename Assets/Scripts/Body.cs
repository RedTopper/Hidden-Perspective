using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {
    public GameObject head;
    public float offset;
    public float dist;

    void LateUpdate()
    {
        Vector3 pos = head.transform.position;
        Vector3 angle = head.transform.rotation.eulerAngles;
        angle.x = 0;
        angle.z = 0;
        gameObject.transform.position = pos + (Quaternion.Euler(angle) * Vector3.forward) * dist - new Vector3(0, offset, 0);
    }
}
