using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandSound : MonoBehaviour {

    private int round = 1;
    private float init = 0;

	// Use this for initialization
	void Start () {
        init = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - init > (90 * round))
        {
            float rand = Random.Range(0, 1f);
            if (rand < 0.01f)
            {
                GetComponent<AudioSource>().Play();
                round++;
            }
        }
	}
}
