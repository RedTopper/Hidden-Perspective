using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    GameObject player;
    bool playing = false;

    private void Awake()
    {
        player = GameObject.Find("Player/Cam");
    }

    void Update()
    {
        float mag = (player.transform.position - transform.position).magnitude;
        bool near = mag < 5f;
        bool nearFurther = mag < 6f;

        if (!playing && near)
        {
            GetComponent<AudioSource>().Play();
            playing = true;
        }

        if (playing && !nearFurther)
        {
            GetComponent<AudioSource>().Stop();
            playing = false;
        }
    }
}
