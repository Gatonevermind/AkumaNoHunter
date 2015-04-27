using UnityEngine;
using System.Collections;

public class Detection : MonoBehaviour {

	public static float detection;
	// Use this for initialization
	void Start () {
		detection = 0;
	}
	public void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
			detection = 1;

	}
	// Update is called once per frame
	void Update () {
	
	}
}
