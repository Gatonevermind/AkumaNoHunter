using UnityEngine;
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
            //GameObject go = GameObject.FindGameObjectWithTag("Enemy");
            //target = go.transform;

            if (attackTimer > 0)
            {
                attackTimer += Time.deltaTime;
				//PlayerMovement.Attack();

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
		            //animator.SetBool("AttackBool", true);
					animator.SetFloat("Attack", 1);
					attackCount = 1;
		            //Attack();
		            attackTimer =0.1f;
		        }
				else if ((Input.GetKeyDown(KeyCode.Mouse0)) && (attackCount == 1))
				{
					//animator.SetBool("AttackBool", true);
					animator.SetFloat("Attack", 2);
					attackCount = 2;
					//Attack();
					//attackTimer = coolDown;
				}
				else if ((Input.GetKeyDown(KeyCode.Mouse0)) && (attackCount == 2))
				{
					//animator.SetBool("AttackBool", true);
					animator.SetFloat("Attack", 3);
					attackCount = 3;
					//Attack();
					//attackTimer = coolDown*1.7f;
				}
			}
        }
		else 
		{
			animator.SetFloat("Attack", 0);
			attackCount = 0;
		}
	}
	/*
	private void Attack() 
    {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		if(distance < 1f) {
			if(direction > 0) {
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AddjustCurrentHealth(-30);
			}
		}
	}
     */
}