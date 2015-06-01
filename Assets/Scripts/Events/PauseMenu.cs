using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseMenu : MonoBehaviour 
{
    public static bool pauseMenu = false;
    GameObject[] pauseControl;
    public string mainLevel;

    public bool optionsMenu = false;
    GameObject[] optionsControl;

	public bool controlsMenu = false;
	GameObject[] controlsControl;

	public GameObject disableOne; 
	public GameObject disableTwo; 
	public bool disableUI = false;

	public float countTransition;
	public bool activePlayMode;
	private bool exitMenu;

	private bool volumeChange;
	public GameObject mainCameraObject;

    PlayerIndex playerIndex;
    GamePadState prevState;
    GamePadState state;

    void Start() 
    {
		activePlayMode = false;	
		
        pauseControl = GameObject.FindGameObjectsWithTag("Pause");
        foreach (GameObject pause in pauseControl)
            pause.SetActive(false);

        optionsControl = GameObject.FindGameObjectsWithTag("Options");
        foreach (GameObject options in optionsControl)
            options.SetActive(false);

		controlsControl = GameObject.FindGameObjectsWithTag("Controls");
		foreach (GameObject controls in controlsControl)
			controls.SetActive(false);
    }

	void Update () 
    {
		/*if (activePlayMode) {
			float fadeTime = GameObject.Find ("GameMaster").GetComponent<fader> ().BeginFade (1);
			//yield return new WaitForSeconds (fadeTime);
			countTransition -= Time.time;
			Debug.Log("CACAPUTA");
			if (countTransition <= 0) {

			}
		}*/

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (pauseMenu)
            foreach (GameObject pause in pauseControl)
                pause.SetActive(true);
        else if (!pauseMenu && !optionsMenu)
            foreach (GameObject pause in pauseControl)
                pause.SetActive(false);
        
        if ((Input.GetKeyDown(KeyCode.Escape) || ((prevState.Buttons.Start == ButtonState.Released) && (state.Buttons.Start == ButtonState.Pressed))) && !optionsMenu)
        {
			volumeChange = !volumeChange;
            pauseMenu = !pauseMenu;
			disableUI = !disableUI;
        }

		if (disableUI) {
			disableOne.SetActive(false);
			if(BossEvent.countToCinematic >= 39)
			{
				disableTwo.SetActive(false);
			}

		}

		if ((!disableUI && IntroCinematic.intro)&& !BossEvent.bossCinematic) {
			disableOne.SetActive(true);
			//disableTwo.SetActive(true);
			if(BossEvent.countToCinematic >= 39)
			{
				disableTwo.SetActive(true);
			}
		}

		if (volumeChange) {
			mainCameraObject.GetComponent<AudioSource>().volume = 0.03f;
		}

		if (!volumeChange) {
			mainCameraObject.GetComponent<AudioSource>().volume = 0.1f;
		}

        if (pauseMenu)
        {
			Time.timeScale = 0;
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

		if (controlsMenu && pauseMenu)
		{
			foreach (GameObject controls in controlsControl)
				controls.SetActive(true);
			
			foreach (GameObject pause in pauseControl)
				pause.SetActive(false);
		}
		else if ((!controlsMenu && pauseMenu)&& !optionsMenu)
		{
			foreach (GameObject controls in controlsControl)
				controls.SetActive(false);
			
			foreach (GameObject pause in pauseControl)
				pause.SetActive(true);
		}

		/*if (exitMenu) {
			activePlayMode = true;
		}*/
	}

    public void Resume()
    {
		pauseMenu = !pauseMenu;
    }

	public void Controls()
	{
		controlsMenu = !controlsMenu;
	}

    public void Exit()
    {
		Application.LoadLevel(1);
		pauseMenu = !pauseMenu;
		//Time.timeScale = 1;
		//exitMenu = !exitMenu;
		//pauseMenu = !pauseMenu;
    }

    public void Options()
    {
        optionsMenu = !optionsMenu;
    }
}
