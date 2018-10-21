using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win: MonoBehaviour
{
    private float now;

    void Start()
    {
        now = Time.time;
    }

    void Update()
    {
        if (now + 5 < Time.time)
        {
            SceneManager.LoadScene("Main");
        }
    }
}