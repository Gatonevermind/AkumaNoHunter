using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour 
{
    public AnimationClip attack;

    public float cdBlow;
    public float cdJump;
    public float cdNibble;
    public CharacterController controller;

    public AnimationClip die;

    public Transform enemy;

    public AnimationClip idle;

    public float range;

    public AnimationClip run;

    public Transform samurai;
    public float speed;
    public AnimationClip waitingforbattle;
    AttackType attackCurrent;
    float chargeJump;

    bool follow;

    private bool statusHealth = true;

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
        follow = true;
        cdNibble = 0;
        cdBlow = 0;
        cdJump = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = Vector3.Distance (samurai.transform.position, transform.position);

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

			cdNibble -= 60 * Time.deltaTime;
			cdBlow -= 60 * Time.deltaTime;
            cdJump -= 60 * Time.deltaTime;

			if (cdNibble < 0) cdNibble = 0;
			if (cdBlow < 0) cdBlow = 0;
            if (cdJump < 0) cdJump = 0;

			if (distance < 1f) 
			{
				if (direction > 0) 
				{
                    if (cdBlow == 0)
                    {
                        attackCurrent = AttackType.BLOW;
                        cdBlow = 130;
                    }
                    else if (cdNibble == 0)
                    {
                        attackCurrent = AttackType.NIBBLE;
                        cdNibble = 280;
                    }
				}
			}
			if ((InRange()) && (distance > 1f)) animation.CrossFade (run.name);
            if ((InRange()) && (distance > 4f))
            {
                if (cdJump == 0)
                {
                    attackCurrent = AttackType.JUMP;
                    cdJump = 500;
                }
            }
		}

		switch (attackCurrent) 
		{
			case AttackType.IDLE:
			{
                //animation.CrossFade(idle.name);
                follow = true;
			}
			break;	
			case AttackType.BLOW:
			{
				PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                follow = false;
                if (distance < 1f)
                {
                    eh.AddjustCurrentHealth(-3);
                }
				animation.CrossFade (attack.name);
                attackCurrent = AttackType.IDLE;
			}
			break;	
			case AttackType.NIBBLE:
			{
				PlayerHealth eh = (PlayerHealth)samurai.GetComponent("PlayerHealth");
                follow = false;
                if (distance < 1f)
                {
                    eh.AddjustCurrentHealth(-8);
                }
				animation.CrossFade (attack.name);
                attackCurrent = AttackType.IDLE;
			}
			break;	
			case AttackType.JUMP:
			{
      
                //transform.position = samurai.position;
                follow = true;
                transform.position = Vector3.Lerp(transform.position, samurai.position, 40 * Time.deltaTime);
                attackCurrent = AttackType.IDLE;
			}
			break;
			case AttackType.DEAD:
			{
				animation.CrossFade (die.name);
                follow = true;
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
				Chase ();
			} 
			else 
			{
				attackCurrent = AttackType.IDLE;
                animation.CrossFade(idle.name);
			}
		}
	}
}
