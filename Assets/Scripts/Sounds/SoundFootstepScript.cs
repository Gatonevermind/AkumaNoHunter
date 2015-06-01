using UnityEngine;
using System.Collections;

public class SoundFootstepScript : MonoBehaviour {

	public AudioClip footstep1; 
	//public AudioClip footstep2;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Foot") {
			GetComponent<AudioSource>().PlayOneShot(footstep1, 0.5f);
		}
	}
}
