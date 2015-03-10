using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{

    public bool pauseMenu = false;

    void Start() { 
    
    }

	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu = !pauseMenu;

            if (pauseMenu)
            {
                Debug.Log("pause");
                Time.timeScale = 0.0000000001f;
            }
            else {
                Debug.Log("play");
                Time.timeScale = 1;
            }

        }
            
	}
}
