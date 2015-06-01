using UnityEngine;
using System.Collections;

public class faderCinematicas : MonoBehaviour {

	public float fadeSpeed = 1.5f;
	public Texture2D fadeOutTexture;
	public static bool activeFader;

	void Awake()
	{
		activeFader = false;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}
	
	// Update is called once per frame
	void Update () {

		if (activeFader) {
			Debug.Log("se ha activado active fader");
			FadeToBlack();
		}
	}

	void FadeToClear()
	{
		GUI.color = Color.Lerp(GUI.color, Color.clear, fadeSpeed*Time.deltaTime);
	}

	void FadeToBlack()
	{
		GUI.color = Color.Lerp(GUI.color, Color.black, fadeSpeed*Time.deltaTime);
		
	}
}
