﻿using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour 
{

	public Transform target;
	public static float attackTimer;
	public static float attackMove;
	public float coolDown;
	public float attackCount;
	public int timecounter = 0;



	private Animator animator;

    public bool combatActivate = false;

	
	// Use this for initialization
	void Start () 
    {
		attackTimer = 0;
		coolDown = 1f;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
		if ((Input.GetKeyDown(KeyCode.Q)) && (PlayerMovement.grounded == 0))
        {
            combatActivate = !combatActivate;
        }

        if (combatActivate)
        {
            if (attackTimer > 0)
            {
                attackTimer += Time.deltaTime;

            }
			if(( attackTimer >= 0.6f) && (attackTimer < 0.8))
			{
				if(attackCount == 1)
				{
					animator.SetFloat ("Attack", 0);
					attackTimer = 0;
					attackCount = 0;
				}
					
			}
			else if(( attackTimer >= 1.4f) && (attackTimer <1.5f))
			{
				if(attackCount == 2)
				{
					animator.SetFloat ("Attack", 0);
					attackTimer = 0;
					attackCount = 0;
				}
			}
			else if( attackTimer >= 2.1f)
			{
				if(attackCount == 3)
				{
					animator.SetFloat ("Attack", 0);
					attackTimer = 0;
					attackCount = 0;
				}
			}

			if(PlayerMovement.seatheCooldown == 0)
			{
		        if ((Input.GetKeyDown(KeyCode.Mouse0)) && (attackCount == 0))
		        {
					animator.SetFloat("Attack", 1);
					attackCount = 1;
		            attackTimer =0.1f;
		        }
				else if ((Input.GetKeyDown(KeyCode.Mouse0)) && (attackCount == 1))
				{
					animator.SetFloat("Attack", 2);
					attackCount = 2;
				}
				else if ((Input.GetKeyDown(KeyCode.Mouse0)) && (attackCount == 2))
				{
					animator.SetFloat("Attack", 3);
					attackCount = 3;
				}
			}
        }
		else 
		{
			animator.SetFloat("Attack", 0);
			attackCount = 0;
		}
	}
}