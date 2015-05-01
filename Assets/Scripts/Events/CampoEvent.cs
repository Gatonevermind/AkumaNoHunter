using UnityEngine;
using System.Collections;

public class CampoEvent : MonoBehaviour 
{
	public GameObject lobo;
	public GameObject loboCollider;


	private void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Player")
		{
			lobo.SetActive(true);
		}
	}
}
