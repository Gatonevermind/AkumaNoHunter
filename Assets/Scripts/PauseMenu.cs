using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
    public bool pauseMenu = false;
    GameObject[] pauseControl;
    public string mainLevel;

    void Start() 
    {
        pauseControl = GameObject.FindGameObjectsWithTag("Pause");
        foreach (GameObject pause in pauseControl)
            pause.SetActive(false);

    }

	void Update () 
    {

        if (pauseMenu)
            foreach (GameObject pause in pauseControl)
                pause.SetActive(true);
        else
            foreach (GameObject pause in pauseControl)
                pause.SetActive(false);
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu = !pauseMenu;
        }

        if (pauseMenu)
        {
            Time.timeScale = 0.00001f;
        }
        else
        {
            Time.timeScale = 1;
        }
            
	}

    public void Resume()
    {
        pauseMenu = !pauseMenu;
    }

    public void Exit()
    {
        Application.LoadLevel(mainLevel);
    }
}
