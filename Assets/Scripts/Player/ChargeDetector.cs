using UnityEngine;
using System.Collections;

public class ChargeDetector : MonoBehaviour
{
	public static bool hit;

	void Start()
	{
		hit = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy")
		{
			hit = true;
		}
	}
}