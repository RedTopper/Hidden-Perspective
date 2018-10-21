using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {

    private bool on = true;
    private bool flicker = false;
    private int round = 1;
    private float init = 0;

    public int duration = 10;
    public int time = 90;
    public float chance = 0.05f;

    // Use this for initialization
    void Start () {
        init = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - init > time * round)
        {
            flicker = true;
        }

        if (Time.time - init > duration + time * round)
        {
            flicker = false;
            on = true;
            round++;
        }

        if (flicker && Random.Range(0f, 1f) < chance)
        {
            on = !on;
        }

        Renderer renderer = GetComponent<Renderer>();
        if (on)
        {
            renderer.material.SetColor("_EmissionColor", new Color(1, 1, 1));
            GameObject.Find("Flashlight/Light").GetComponent<Light>().enabled = true;
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", new Color(0, 0, 0));
            GameObject.Find("Flashlight/Light").GetComponent<Light>().enabled = false;
        }
    }
}
