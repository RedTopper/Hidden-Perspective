using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
    
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && GetComponent<AudioSource>().isPlaying == false)
        {
            GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().Play();
        }
	}
}
