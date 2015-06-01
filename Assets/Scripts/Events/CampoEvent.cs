using UnityEngine;
using System.Collections;

public class CampoEvent : MonoBehaviour 
{
	public static bool weaponActive;

	void Start()
	{
		weaponActive = false;
	}

	private void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			weaponActive = true;
		}
	}
	private void OnTriggerExit (Collider other)
	{
		if(other.tag == "Player")
		{
			weaponActive = false;
		}
	}

}
