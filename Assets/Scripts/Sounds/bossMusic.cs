using UnityEngine;
using System.Collections;

public class bossMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetComponent<AudioSource> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (BossEvent.countToCinematic >= 32) {
			transform.GetComponent<AudioSource> ().enabled = true;
		}
	
	}
}
