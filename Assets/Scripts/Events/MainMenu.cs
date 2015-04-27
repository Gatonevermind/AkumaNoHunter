using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public string playLevel;

    public bool optionsMenu = false;
    GameObject[] optionsControl;

    void Start()
    {

        optionsControl = GameObject.FindGameObjectsWithTag("Options");
        foreach (GameObject options in optionsControl)
            options.SetActive(false);
    }


    public void Update()
    {
        Time.timeScale = 1;

        
        if (optionsMenu)
        {
            optionsControl = GameObject.FindGameObjectsWithTag("Options");
            foreach (GameObject options in optionsControl)
                options.SetActive(true);
        }
        /*
        else
        {
            optionsControl = GameObject.FindGameObjectsWithTag("Options");
            foreach (GameObject options in optionsControl)
                options.SetActive(false);
        }
        */
    }

    public void Play()
    {
        Application.LoadLevel(playLevel);
    }

    public void Exit()
    {
        Application.Quit();  
    }

    public void Options()
    {
        optionsMenu = true;
    }
}
