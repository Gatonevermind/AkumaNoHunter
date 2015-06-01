using UnityEngine;
using System.Collections;

public class MovementCredits : MonoBehaviour {

	public Vector3 positionCredits;
	public float speedCredits;

	void Start(){
		positionCredits = new Vector3 (0, -25, 0);
		transform.position = positionCredits;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * Time.deltaTime * speedCredits);

		if (!MainMenu.creditsMenu)
		{
			transform.position = positionCredits;
		}

	}
}
