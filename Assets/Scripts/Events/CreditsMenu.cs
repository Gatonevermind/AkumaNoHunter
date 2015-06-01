using UnityEngine;
using System.Collections;

public class CreditsMenu : MonoBehaviour {

	public GameObject credits;
	public GameObject creditsPosition;

	public Texture2D background;
	public Vector3 positionCredits;
	public float creditsTimer;
	// Use this for initialization

	void OnGUI()
	{
		if (MainMenu.creditsMenu) {
			//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), background);

			credits.SetActive(true);
		}

		if (!MainMenu.creditsMenu) {
			creditsTimer = 0;
			credits.SetActive(false);
		}
	}

	void Start () 
	{
		positionCredits = new Vector3 (0, -25, 0);
	}
	
	// Update is called once per frame
	void Update () {
		creditsTimer += Time.deltaTime;
		if (Input.GetKey (KeyCode.Escape)) 
		{
			MainMenu.creditsMenu = false;

		}

		if (creditsTimer > 75)
		{
			MainMenu.creditsMenu = false;
		}
	}
}
