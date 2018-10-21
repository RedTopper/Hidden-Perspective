using System.Collections;
using UnityEngine;

public class MainManager : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Application.LoadLevel(1);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
