using UnityEngine;
using System.Collections;

public class audioPlayer : MonoBehaviour {

	public AudioClip step1Audio;
	private AudioSource source;
	public bool sound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update() {
		if(sound)
			source.PlayOneShot (step1Audio, 0.75f);
	}
}
