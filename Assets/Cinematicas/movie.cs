using UnityEngine;
using System.Collections;

public class movie : MonoBehaviour {

	public MovieTexture movieTex;
	// Use this for initialization
	void Start () {

		movieTex.Play();
		movieTex.loop = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (movieTex.isPlaying == false) {
			Application.LoadLevel(1);
		}
	}
}
