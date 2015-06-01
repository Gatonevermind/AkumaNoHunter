using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public bool optionsMenu = false;
    GameObject[] optionsControl;

	public static bool creditsMenu = false;

	public float countTransition;
	public bool activePlayMode;

    void Start()
    {
		activePlayMode = false;
        optionsControl = GameObject.FindGameObjectsWithTag("Options");
        foreach (GameObject options in optionsControl)
            options.SetActive(false);
    }


    public void Update()
    {
        Time.timeScale = 1;

        
        if (optionsMenu)
        {
            foreach (GameObject options in optionsControl)
                options.SetActive(true);
        }
        else
        {
            foreach (GameObject options in optionsControl)
                options.SetActive(false);
        }

		if (activePlayMode) {
			float fadeTime = GameObject.Find ("GameMaster").GetComponent<fader> ().BeginFade (1);
			//yield return new WaitForSeconds (fadeTime);
			countTransition -= Time.deltaTime;
			if (countTransition <= 0) {
				Application.LoadLevel (2);
			}
		}
    }

	public void Play()
	{
		activePlayMode = true;
	}

    public void Exit()
    {
        Application.Quit();  
    }

    public void Options()
    {
        optionsMenu = !optionsMenu;
    }

	public void Credits()
	{
		creditsMenu = !creditsMenu;

	}
}
