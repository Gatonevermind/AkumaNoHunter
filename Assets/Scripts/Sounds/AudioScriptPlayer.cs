using UnityEngine;
using System.Collections;

public class AudioScriptPlayer : MonoBehaviour {

	public AudioClip soundAttackWind1; 
	public AudioClip soundAttackWind2; 
	public AudioClip soundAttackWind3; 
	public AudioClip soundSheathe; 
	public AudioClip soundUnsheathe; 
	public AudioClip soundDashWind;
	public AudioClip soundDashCharge;
	public GameObject blood1;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		if (PlayerMovement.boolFXblood1)
			blood1.SetActive (true);
		else
			blood1.SetActive (false);
		if(PlayerMovement.boolAttackWind1)
			GetComponent<AudioSource>().PlayOneShot(soundAttackWind1, 1);

		if(PlayerMovement.boolAttackWind2)
			GetComponent<AudioSource>().PlayOneShot(soundAttackWind2, 1);

		if(PlayerMovement.boolAttackWind3)
			GetComponent<AudioSource>().PlayOneShot(soundAttackWind3, 1);

		if (PlayerMovement.boolSheathe)
		{
			if (PlayerMovement.combat) 
				GetComponent<AudioSource>().PlayOneShot(soundSheathe, 1);

			else 
				GetComponent<AudioSource>().PlayOneShot(soundUnsheathe, 1);
		}

		if(PlayerMovement.boolDashCharge)
			GetComponent<AudioSource>().PlayOneShot(soundDashCharge, 1);

		if(PlayerMovement.boolDashWind)
			GetComponent<AudioSource>().PlayOneShot(soundDashWind, 1);


	}
}
	