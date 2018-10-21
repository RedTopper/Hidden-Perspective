using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RenderSettings.fogColor = Camera.main.backgroundColor;
        RenderSettings.fogDensity = 1.00f;
        RenderSettings.fog = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
