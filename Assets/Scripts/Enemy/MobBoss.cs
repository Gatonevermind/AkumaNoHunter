using UnityEngine;
using System.Collections;

public class MobBoss : MonoBehaviour 
{
	public CharacterController controller;
	
	public Transform enemy;
	public Transform samurai;
	
	public AnimationClip blow;
	public AnimationClip nibble;
	public AnimationClip waitingforbattle;
	public AnimationClip run;
	public AnimationClip idle;
	public AnimationClip die;
	public AnimationClip jump;
	public AnimationClip finaldie;
	
	public float range;
	public float speed;
	public float cdBlow;
	public float cdJump;
	public float cdNibble;
	public float chargeJump;
	public float timeJump;
	public float chargeBlow;
	public float chargeNibble;
	
	public float distance;
	public float finalDie;
	
	bool follow;
	bool statusHealth = true;
	
	AttackType attackCurrent;
	Vector3 positionAttack;
	
	enum AttackType { IDLE, JUMP, BLOW, NIBBLE, DEAD, COMBAT};
	
	void Chase()
	{
		transform.LookAt(samurai.position);
		controller.SimpleMove(transform.forward * speed);
	}
	
	bool InRange()
	{
		if (Vector3.Distance(transform.position, samurai.position) < range)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		attackCurrent = AttackType.IDLE;
		cdNibble = 5;
		cdBlow = 2;
		cdJump = 0;
		timeJump = 0;
		chargeJump = 5;
		chargeNibble = 0;
		chargeBlow = 0;
		finalDie = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = Vector3.Distance (samurai.transform.position, transform.position);
		
		Vector3 dir = (samurai.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot (dir, transform.forward);
		
		EnemyHealth status = (EnemyHealth)enemy.GetComponent("EnemyHealth");
		
		if (status.curHealth <= 0) 
		{
			statusHealth = false;
			attackCurrent = AttackType.DEAD;
		}
		
		if (statusHealth) 
		{
			
			cdNibble -= Time.deltaTime;
			cdBlow -= Time.deltaTime;
			cdJump -= Time.deltaTime;
			
			if (cdNibble < 0) cdNibble = 0;
			if (cdBlow < 0) cdBlow = 0;
			if (cdJump < 0) cdJump = 0;
			if (chargeJump < 0) chargeJump = 0;
			
			if (distance < 3) 
			{
				if (direction > 0) 
				{
					if (cdBlow == 0)
					{
						attackCurrent = AttackType.BLOW;
						cdBlow = 2;
					}
					else if (cdNibble == 0)
					{
						attackCurrent = AttackType.NIBBLE;
						cdNibble = 5;
					}
				}
			}
			if ((InRange()) && (distance > 8))
			{
				if (cdJump == 0)
				{
					positionAttack = samurai.transform.position;
					attackCurrent = AttackType.JUMP;
					cdJump = 5;
					chargeJump = 2;
				}
			}
			else if ((InRange()) && (distance > 4f)) animation.CrossFade(run.name);
		}
		
		switch (attackCurrent) 
		{
		case AttackType.IDLE:
		{
			follow = true;
			chargeJump = 0;
			chargeNibble = 0;
			chargeBlow = 0;
			timeJump = 0;
		}
			break;	
		case AttackType.BLOW:
		{
			follow = false;
			PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
			chargeBlow += Time.deltaTime;
			animation.CrossFade(blow.name);
			
			if (chargeBlow >= 2)
			{
				if (distance < 3f)
				{
					eh.AddjustCurrentHealth(-3);
					attackCurrent = AttackType.IDLE;
				}
				else if (distance > 3f)
				{
					eh.AddjustCurrentHealth(0);
					attackCurrent = AttackType.IDLE;
				}
			}
		}
			break;	
		case AttackType.NIBBLE:
		{
			follow = false;
			PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
			chargeNibble +=Time.deltaTime;
			animation.CrossFade(nibble.name);
			
			if (chargeNibble >= 2)
			{
				if (distance < 3f)
				{
					eh.AddjustCurrentHealth(-8);
					attackCurrent = AttackType.IDLE;
				}
				else if (distance > 3f)
				{
					eh.AddjustCurrentHealth(0);
					attackCurrent = AttackType.IDLE;
				}
			}
		}
			break;	
		case AttackType.JUMP:
		{
			follow = false;
			PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
			chargeJump -= Time.deltaTime;
			if (chargeJump <= 5) animation.CrossFade(jump.name);
			if (chargeJump <= 0)
			{
				transform.position = Vector3.Lerp(transform.position, positionAttack, 0.02f);
				timeJump += Time.deltaTime;   
				
			}
			if (timeJump >= 1)
			{
				attackCurrent = AttackType.IDLE;
				if (distance < 3f)
				{
					eh.AddjustCurrentHealth(-15);
				}
			}
		}
			break;
		case AttackType.DEAD:
		{
			animation.CrossFade (die.name);
			follow = false;
			finalDie += Time.deltaTime;
			
			if (finalDie >= 4) animation.Play(finaldie.name);
			
		}
			break;
		case AttackType.COMBAT:
		{
			animation.CrossFade(waitingforbattle.name);
		}
			break;
		}
		
		if (statusHealth) 
		{
			if (InRange ())
			{
				if (follow) Chase ();
			} 
			else 
			{
				attackCurrent = AttackType.IDLE;
				animation.CrossFade(idle.name);
			}
		}
	}
}
