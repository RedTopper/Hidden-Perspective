using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flicker : MonoBehaviour {

    private bool hit = false;
    private bool on = true;
    public int chanceOff;
    public int chanceOn;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("hit");
        if (col.gameObject.name == "cap_player")
        {
            hit = true;
            Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        if (hit) {
            int rand = Random.Range(0, 100);
            Renderer renderer = GetComponent<Renderer>();

            if (on && rand < chanceOff)
            {
                renderer.material.SetColor("_EmissionColor", new Color(0, 0, 0));
                on = false;
            }

            if (!on && rand < chanceOn)
            {
                renderer.material.SetColor("_EmissionColor", new Color(1, 1, 1));
                on = true;
            }
        }
    }
}
