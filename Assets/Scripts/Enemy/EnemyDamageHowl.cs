﻿using UnityEngine;
using System.Collections;

public class EnemyDamageHowl : MonoBehaviour {
	
	public float damage;
	public bool hit;
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "HitBox")
		{
			
			
			//PlayerMovement.animator.SetBool("Hit", true);
			
			PlayerHealth.curHealth -= 10*Time.deltaTime;
			
			Debug.Log ("Damage HOWL");
			
			
			if (hit)
			{
				return;
			}
			hit = true;
			
		}
		
		
		
	}
	//	private void OnTriggerExit(Collider other)
	//	{
	//		if(other.tag == "HitBox")
	//		{
	//			if (!hit)
	//			{
	//				return;
	//			}
	//			hit = false;
	//		}
	//	}
	
	
	//	void Update()
	//	{
	//		//PlayerHealth health = (PlayerHealth)samurai.GetComponent("PlayerHealth");
	//		
	//		Vector3 fwd = transform.TransformDirection(Vector3.forward);
	//		if (Physics.Raycast(transform.position, fwd, 10))
	//			print("There is something in front of the object!");
	//			//health.curHealth -= 20;
	//
	//
	//	}
	//	// Update is called once per frame
	
}