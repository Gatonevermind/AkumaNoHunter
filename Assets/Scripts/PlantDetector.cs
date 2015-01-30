using UnityEngine;
using System.Collections;

public class PlantDetector : MonoBehaviour {

	private Animator animator;
	private float time = 0;
	private float speed;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}
	void Update()
	{
		speed = 1 * Time.deltaTime;
		if (time >= 0)
		{
			time += speed;
			if(time >= 2)
			{
				animator.SetBool("Active", false);
				time = 0;
			}
		}
	}
	void OnTriggerStay (Collider plantCollider)
	{
		if(plantCollider.gameObject.tag == "Player")
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				animator.SetBool ("Active", true);
				time = 1;				
			}
			else if(Input.GetKeyDown(KeyCode.A))
			{
				animator.SetBool ("Active", true);
				time = 1;				
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				animator.SetBool ("Active", true);
				time = 1;				
			}
			else if(Input.GetKeyDown(KeyCode.S))
			{
				animator.SetBool ("Active", true);
				time = 1;				
			}

		}
	}
	void OnTriggerEnter (Collider plantCollider)
	{




		if(plantCollider.gameObject.tag == "Player")
		{
			animator.SetBool("Active", true);

			time = 1;
		}
	}
}
