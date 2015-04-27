using UnityEngine;
using System.Collections;

public class WolfIABOSS : MonoBehaviour {

	private enum State {IDLE, SEARCH, MORDISCO, ZARPAZO, JUMP, HOWL,DEATH};
	private State state;
	
	//public Transform enemy;
	private float distance;
	public Transform samurai;
	public Transform enemy;
	public Transform backPedalling;

	public Vector3 lastPosition;

	public GameObject collisionBox;
	public GameObject damageMordiscoBox;
	public GameObject damageZarpazoBox;
	public GameObject damageJumpBox;

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
	public float fieldOfViewAngle = 30;
	public float rotationSearch = 1;
	public float hitCounter;
	public float pull;

	public float mordiscoRange;
	public float jumpRange;

	public float aleatorio;

	public bool enemyAttackHit;
	public float enemyAttackTimer;

	public CapsuleCollider collider;

	public static bool headView;

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
					playerAttackHit = true;
					Debug.Log ("HIT 1");
				}
				
				else if (PlayerAttack.attackCount == 2)
				{
					PlayerMovement.hit = true;
					enemyVida -= 30;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 2");
				}
				else if (PlayerAttack.attackCount ==3)
				{
					PlayerMovement.hit = true;
					enemyVida -= 50;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 3");
				}
				else if (PlayerAttack.attackCount ==8)
				{
					PlayerMovement.hit = true;
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
		collider = GetComponent <CapsuleCollider> ();
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

		switch (state)
		{
			case State.IDLE:
			{
				//Debug.Log("Idle");
				animator.SetBool("Mordisco", false);
				animator.SetBool("Zarpazo", false);
				detection = 15;

				if (Vector3.Distance(transform.position, samurai.position) < detection)
				{
					state = State.SEARCH;
				}
			}
				break;
			case State.SEARCH:
			{
				detection = 100;
				//animator.SetBool ("Idle", false);
				//Debug.Log("Search");
				animator.SetBool("Idle", true);
				
				Vector3 direction = samurai.position - transform.position;
				
				float angle = Vector3.Angle(direction, transform.forward);

				enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
				
				if (Vector3.Distance(transform.position, samurai.position) <= 3)
				{
					navAgent.speed = 6;
					navAgent.destination = backPedalling.position;
				}
				else
					navAgent.Stop ();
				//lastPosition = samurai.position;
			
				if(Vector3.Distance(transform.position, samurai.position) <= mordiscoRange)
				{
					if(angle < fieldOfViewAngle)
					{
						state = State.MORDISCO;
					}
				}

				else if((Vector3.Distance(transform.position, samurai.position) > mordiscoRange) && (Vector3.Distance(transform.position, samurai.position) < jumpRange))
			   	{
					if(angle < fieldOfViewAngle * 2)
					{
				   		state = State.ZARPAZO;
					}
				}

				else if(Vector3.Distance(transform.position, samurai.position) >= jumpRange)
				{
					if(angle < fieldOfViewAngle * 2)
					{
						state = State.JUMP;
					}
				}
			}
				break;


			case State.MORDISCO:
			{
			//Debug.Log("Mordisco");
				enemyAttackTimer += Time.deltaTime;
				animator.SetBool ("Idle", false);
				if (enemyAttackTimer <= 0.7f)
				{
					
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);

					lastPosition = samurai.position;
					
				}
				if (enemyAttackTimer >= 0.5f)
				{
					animator.SetBool("Mordisco", true);
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 6f;
					navAgent.stoppingDistance = 2.5f;

				}

				if(enemyAttackTimer >= 0.6f)
				{
					
					animator.SetBool ("Mordisco", false);
				}
				
				if((enemyAttackTimer > 0.5f) && (enemyAttackTimer < 1.2f))
					damageMordiscoBox.SetActive(true);		

				else if(enemyAttackTimer > 1.2f)
					damageMordiscoBox.SetActive(false);
				
				if (enemyAttackTimer >= 2.5f)
				{
					state = State.SEARCH;
					enemyAttackTimer = 0;
				}
			}
				break;

			case State.ZARPAZO:
			{
			//Debug.Log("Zarpazo");
				enemyAttackTimer += Time.deltaTime;
				animator.SetBool ("Idle", false);
				
				if (enemyAttackTimer < 0.7f)
				{
					navAgent.Stop ();
					animator.SetBool("Zarpazo", true);
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
					
					lastPosition = samurai.position;
				}
				if (enemyAttackTimer >= 1)
				{
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 15;
					
				}
				
				if(enemyAttackTimer >= 0.5f)
				{
					
					animator.SetBool ("Zarpazo", false);
				}
				
				if((enemyAttackTimer > 1f) && (enemyAttackTimer < 2f))
					damageZarpazoBox.SetActive(true);		
				
				else if(enemyAttackTimer > 2f)
					damageZarpazoBox.SetActive(false);
				
				if (enemyAttackTimer >= 4)
				{
					state = State.SEARCH;
					enemyAttackTimer = 0;
				}
			}
				break;

			case State.JUMP:
			{
			//Debug.Log ("Jump");
				
				animator.SetBool("Jump", true);
				enemyAttackTimer += Time.deltaTime;
				animator.SetBool ("Idle", false);
				if (enemyAttackTimer < 1)
				{
					
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
					
					lastPosition = samurai.position;
				}
				if (enemyAttackTimer >= 1)
				{
					
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 15f;
				}
				
				if(enemyAttackTimer >= 0.5f)
				{
					
					animator.SetBool ("Jump", false);
				}
				
				if((enemyAttackTimer > 1) && (enemyAttackTimer < 2))
					damageJumpBox.SetActive(true);		
				
				else if(enemyAttackTimer > 0.7f)
					damageJumpBox.SetActive(false);
				
				if (enemyAttackTimer >= 4)
				{
					state = State.SEARCH;
					enemyAttackTimer = 0;
				}
			}
				break;

			case State.DEATH:
			{
				damageMordiscoBox.SetActive(false);
				damageZarpazoBox.SetActive(false);
				damageJumpBox.SetActive(false);
				animator.SetBool("Death", true);
				collider.enabled = false;
				collisionBox.SetActive(false);
			}
				break;
		}
	}
}
