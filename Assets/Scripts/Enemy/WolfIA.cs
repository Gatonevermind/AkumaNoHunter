using UnityEngine;
using System.Collections;

public class WolfIA : MonoBehaviour {

	private enum State {IDLE, SEARCH, CHASE, ATTACK, HIT, DEATH, ALDEANO};
	private State state;
	
	//public Transform enemy;
	private float distance;
	public Transform samurai;
	public Transform enemy;
	public Transform front;

	public Vector3 lastPosition;

	public GameObject collisionBox;
	public GameObject damageBox;

	private Animator animator;
	NavMeshAgent navAgent;

	public bool playerAttackHit;
	public float playerAttackTimer;
	public float playerAttackCount;

	public float enemyVida;

	public float preSpeed;
	public float speed;
	public float detection;
	public float premelee;
	public float melee;
	public float fieldOfViewAngle = 50;
	public float rotationSearch = 1;
	public float hitCounter;
	public float pull;

	public float hitTimer;


	public float aleatorio;

	public bool enemyAttackHit;
	public float enemyAttackTimer;

	public BoxCollider collider;

	// >>>>>>>>>>>> PLAYER DAMAGE TO WOLF <<<<<<<<<<<<
	private void OnTriggerEnter(Collider other)
	{

		if(playerAttackHit == false)
		{
			if (other.tag== "AttackBox")
			{
				if (PlayerAttack.attackCount == 1)
				{
					PlayerMovement.hit = true;
					enemyVida -= 30;
					//animator.SetBool ("Hit", true);
					hitCounter = 0;
					state = State.HIT;
					playerAttackHit = true;
					Debug.Log ("HIT 1");
				}
				
				else if (PlayerAttack.attackCount == 2)
				{
					PlayerMovement.hit = true;
					enemyVida -= 30;
					state = State.HIT;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 2");
				}
				else if (PlayerAttack.attackCount ==3)
				{
					PlayerMovement.hit = true;
					state = State.HIT;
					enemyVida -= 50;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 3");
				}
				else if (PlayerAttack.attackCount ==8)
				{
					PlayerMovement.hit = true;
					state = State.HIT;
					PlayerMovement.stamina += 15;
					enemyVida -= 30;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 8");
				}
				
				
				
				
			}
		}
	}



	// Use this for initialization
	void Start () 
	{
		collider = GetComponent <BoxCollider> ();
		state = State.IDLE;
		animator = GetComponent <Animator> ();
		playerAttackHit = false;
		navAgent = GetComponent<NavMeshAgent> (); 
		enemyAttackTimer = 0;
		preSpeed = 1.5f;
		speed = 3.5f;
	
	}

	void Update () 
	{
		if (enemyAttackTimer == 0)
			enemyAttackHit = false;

		if (playerAttackTimer == 0)
			playerAttackHit = false;
		
		playerAttackCount = PlayerAttack.attackCount;
		playerAttackTimer = PlayerAttack.attackTimer;


		if (enemyVida <= 0)
		{
			state = State.DEATH;

		}
		if((IntroCinematic.intro) && (enemy.tag == "Lobo Kai"))
		{
			state = State.DEATH;
		}

		switch (state)
		{
			case State.IDLE:
			{
				animator.SetBool("Attack", false);
				if (Vector3.Distance(transform.position, samurai.position) < detection)
				{
					
					state = State.SEARCH;
				}
		

			}
				break;

			case State.SEARCH:
			{
				animator.SetBool ("Idle", false);
				animator.SetBool ("Search", false);

				navAgent.updateRotation = true;
				
				Vector3 direction = samurai.position - transform.position;
				
				float angle = Vector3.Angle(direction, transform.forward);

				enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
				
				navAgent.Stop();
				
				if(Vector3.Distance(transform.position, samurai.position) > melee)
			   	{
					if(angle < fieldOfViewAngle * 2)
					{
						animator.SetBool("Run", true);
				   		state = State.CHASE;
					}
				}
				else if(Vector3.Distance(transform.position, samurai.position) <= melee)
			   	{
					if(angle < fieldOfViewAngle)
					{
						animator.SetBool("Attack", true);
						state = State.ATTACK;
					}
				}
			}
				break;

			case State.CHASE:
			{
			animator.SetBool ("Run", true);
			navAgent.Resume();
			navAgent.speed = speed;
			navAgent.destination = samurai.position;

			if (Vector3.Distance(transform.position, samurai.position) < premelee)
			{
				navAgent.speed = preSpeed;

				if (Vector3.Distance(transform.position, samurai.position) < melee)
				{
					animator.SetBool("Attack", true);
					state = State.ATTACK;
				}
			}


			else if (Vector3.Distance(transform.position, samurai.position) > detection)
			{
				animator.SetBool ("Run", false);

				state = State.IDLE;
			}

			}
				break;

			case State.ATTACK:
			{
				if( hitTimer <0.7f)
					hitTimer+=Time.deltaTime;

				enemyAttackTimer += Time.deltaTime;
				animator.SetBool ("Run", false);
				if (enemyAttackTimer < 0.75f)
				{
					navAgent.Stop ();

					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);

				lastPosition = front.position;
				}
				if (enemyAttackTimer >= 0.75f)
				{
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 5f;

				}

				if(enemyAttackTimer >= 0.5f)
				{
					
					animator.SetBool ("Attack", false);
				}
				
				if((enemyAttackTimer > 1.1f) && (enemyAttackTimer < 1.5f))
					damageBox.SetActive(true);		

				else if(enemyAttackTimer > 1.5f)
					damageBox.SetActive(false);
				

				if (enemyAttackTimer >= 1.8f)
				{
					animator.SetBool("Search", true);
					state = State.SEARCH;
					enemyAttackTimer = 0;
					hitTimer = 0;
				}
				
				
				
			}
				break;
			case State.HIT:
			{
				enemyAttackTimer = 0;
				navAgent.Stop();
				damageBox.SetActive(false);
				animator.SetBool ("Attack", false);
				if(hitCounter == 0) animator.SetBool("Hit", true);
				hitCounter += Time.deltaTime;
				if(hitCounter >0.1f)
				{
					animator.SetBool("Run", false);
					animator.SetBool("Hit", false);
					
					Vector3 direction = samurai.position - transform.position;
					
					float angle = Vector3.Angle(direction, transform.forward);

					if(hitCounter > 1.2f) 
				{
						if(angle > fieldOfViewAngle * 2)
								
						{
							animator.SetBool("Search", true);
							state = State.SEARCH;
							hitCounter = 0;
						}	
						else if(angle < fieldOfViewAngle * 2)
					                  
						{
							animator.SetBool("Attack", true);
							state = State.ATTACK;
							hitCounter = 0;
						}	
					}
				
				}
			}
				break;

			case State.DEATH:
			{
				damageBox.SetActive(false);
				animator.SetBool("Death", true);
				collider.enabled = false;
				collisionBox.SetActive(false);
				navAgent.enabled = false;

			}
				break;

			case State.ALDEANO:
			{
				
				if( enemyVida < pull)
				{
					if (Vector3.Distance(transform.position, samurai.position) < detection)
						state = State.SEARCH;
					else
					{
						state = State.IDLE;
					}
				}
				else 
				enemyAttackTimer += Time.deltaTime;
					if(enemyAttackTimer < 0.5f)
						animator.SetBool ("Attack", true);
					else if(enemyAttackTimer >= 0.5f)
			        {
			  		 	animator.SetBool ("Attack", false);
						if(enemyAttackTimer > aleatorio)
							enemyAttackTimer = 0;
					}
			}
				break;
		}
	}
}
