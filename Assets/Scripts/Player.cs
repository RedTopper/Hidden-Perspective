using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int xMom;
    private int yMom;
    private int zMom;
    private bool onGround;
    

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = this.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            player.z++;
            this.transform.position = player;
        }

        if (Input.GetKey(KeyCode.A))
        {
            player.x--;
            this.transform.position = position;
        }

        if (Input.GetKey(KeyCode.S))
        {
            player.z--;
            this.transform.position = position;
        }

        if (Input.GetKey(KeyCode.D))
        {
            player.x++;
            this.transform.position = position;
        }

        if (Input.GetKey(KeyCode.Space) && onGround)
        {
            player.y++;
            this.transform.position = position;
        }
    }
}
