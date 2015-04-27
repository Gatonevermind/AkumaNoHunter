using UnityEngine;
using System.Collections;

public class VisKatana : MonoBehaviour
{
	public GameObject katana;
	public GameObject katanaHand;
	public GameObject vaina;

	public static bool herreria;

	void Start()
	{
		herreria = false;
	}
	private void OnTriggerEnter (Collider other)
	{
		if(other.tag == "Forja")
		{
			katana.SetActive(true);
			vaina.SetActive(true);
			herreria = true;
		}
	}

	void Update()
	{
		if (PlayerMovement.combat)
		{
			if (PlayerMovement.seatheCooldown > 0.5f)
			{
				katana.SetActive(false);
				katanaHand.SetActive(true);
			}
		}
		else
		{
			if(PlayerMovement.seatheCooldown > 1.7f)
			{
				katana.SetActive(true);
				katanaHand.SetActive(false);
			}
		}
	}
}