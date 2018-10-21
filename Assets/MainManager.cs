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
            toGame(1);

        if (Input.GetKeyDown(KeyCode.Escape))
            toQuit();
    }

	public void toGame(int scene)
    {
        Application.LoadLevel(scene);
    }
	
	public void toQuit()
    {
        Application.Quit();
    }
}
