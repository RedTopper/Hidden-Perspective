using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bind : MonoBehaviour {

    public OVRInput.Controller bind;

	// Use this for initialization
	void Update () {
        gameObject.transform.localPosition = OVRInput.GetLocalControllerPosition(bind);
        gameObject.transform.localRotation = OVRInput.GetLocalControllerRotation(bind);
	}
}
