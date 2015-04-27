using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour 
{
    public bool pauseMenu = false;
    GameObject[] pauseControl;
    public string mainLevel;

    public bool optionsMenu = false;
    GameObject[] optionsControl;

    void Start() 
    {
        pauseControl = GameObject.FindGameObjectsWithTag("Pause");
        foreach (GameObject pause in pauseControl)
            pause.SetActive(false);

        optionsControl = GameObject.FindGameObjectsWithTag("Options");
        foreach (GameObject options in optionsControl)
            options.SetActive(false);

    }

	void Update () 
    {

        if (pauseMenu)
            foreach (GameObject pause in pauseControl)
                pause.SetActive(true);
        else if (!pauseMenu && !optionsMenu)
            foreach (GameObject pause in pauseControl)
                pause.SetActive(false);
        
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenu)
        {
            pauseMenu = !pauseMenu;
        }

        if (pauseMenu)
        {
            Time.timeScale = 0.00001f;
        }
        else if (!pauseMenu && !optionsMenu)
        {
            Time.timeScale = 1;
        }

        if (optionsMenu && pauseMenu)
        {
            foreach (GameObject options in optionsControl)
                options.SetActive(true);

            foreach (GameObject pause in pauseControl)
                pause.SetActive(false);
        }
        else if (!optionsMenu && pauseMenu)
        {
            foreach (GameObject options in optionsControl)
                options.SetActive(false);

            foreach (GameObject pause in pauseControl)
                pause.SetActive(true);
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

    public void Options()
    {
        optionsMenu = !optionsMenu;
    }
}
