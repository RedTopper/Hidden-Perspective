using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour {
	
	// Update is called once per frame
	void Update ()
    {
        bool walking = gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.2f;
        if (walking && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().volume = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1);
            GetComponent<AudioSource>().Play();
        }

        if(!walking && GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Stop();
        }
	}
}
