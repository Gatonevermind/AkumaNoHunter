using UnityEngine;
using System.Collections;

public class MarcaAgua : MonoBehaviour {

	public GameObject marcaAgua;
	private bool activeOrNot;

	// Use this for initialization
	void Start () {
		activeOrNot = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.F7)) {
			activeOrNot = !activeOrNot;
		}

		if (activeOrNot) {
			marcaAgua.SetActive(true);
		}

		if (!activeOrNot) {
			marcaAgua.SetActive(false);
		}
	}
}
