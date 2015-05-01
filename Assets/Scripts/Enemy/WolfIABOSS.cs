using UnityEngine;
using System.Collections;

public class WolfIABOSS : MonoBehaviour {

	private enum State {IDLE, SEARCH, MORDISCO, ZARPAZO, JUMP, BACK, HOWL, DEATH};
	private State state;
	
	//public Transform enemy;
	private float distance;
	public Transform samurai;
	public Transform enemy;
	public Transform back;
	public Transform dashBack;
	public Transform front;
	public Transform random1;
	public Transform random2;

	public float random;

	public Vector3 lastBack;
	public Vector3 lastPosition;
	public Vector3 lastDashBack;
	public Vector3 lastFront;
	public Vector3 lastRandom1;
	public Vector3 lastRandom2;

	public GameObject collisionBox;
	public GameObject damageMordiscoBox;
	public GameObject damageZarpazoBox;
	public GameObject damageJumpBox1;
	public GameObject damageJumpBox2;
	public GameObject damageHowlSphere;

	public GameObject blood;

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

					blood.SetActive(true);

				}
				
				else if (PlayerAttack.attackCount == 2)
				{
					PlayerMovement.hit = true;
					enemyVida -= 30;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 2");

					blood.SetActive(true);
				}
				else if (PlayerAttack.attackCount ==3)
				{
					PlayerMovement.hit = true;
					enemyVida -= 50;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 3");

					blood.SetActive(true);
				}
				else if (PlayerAttack.attackCount ==8)
				{
					PlayerMovement.hit = true;
					PlayerMovement.stamina += 15;
					enemyVida -= 30;
					hitCounter = 0;
					playerAttackHit = true;
					Debug.Log ("HIT 8");

					blood.SetActive(true);
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
		{
			enemyAttackHit = false;
		}
		
		if (playerAttackTimer == 0)
		{
			blood.SetActive(false);
			playerAttackHit = false;
		}

		playerAttackCount = PlayerAttack.attackCount;
		playerAttackTimer = PlayerAttack.attackTimer;

		if (enemyVida <= 0)
		{
			navAgent.Stop();
			state = State.DEATH;
			enemyAttackTimer = 0;
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
					state = State.HOWL;
				}
			}
				break;
			case State.SEARCH:
			{	
				

				animator.SetBool("Search", false);

				detection = 100;
				
				Vector3 direction = samurai.position - transform.position;
				
				float angle = Vector3.Angle(direction, transform.forward);

				enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
				
				
				lastPosition = samurai.position;
				lastRandom1 = random1.position;
				lastRandom2 = random2.position;
				
			
				if(Vector3.Distance(transform.position, samurai.position) <= mordiscoRange)
				{
					if(angle < fieldOfViewAngle)
					{
						state = State.MORDISCO;
					}
				}

				else if((Vector3.Distance(transform.position, samurai.position) > mordiscoRange) && (Vector3.Distance(transform.position, samurai.position) < jumpRange))
			   	{
					if(angle < fieldOfViewAngle / 2)
					{
						state = State.ZARPAZO;
					}
				}

				else if(Vector3.Distance(transform.position, samurai.position) >= jumpRange)
				{
					if(angle < fieldOfViewAngle / 2)
					{
						state = State.JUMP;
					}
				}
			}
				break;


			case State.MORDISCO:
			{
			//Debug.Log("Mordisco");
				lastBack = back.position;

				enemyAttackTimer += Time.deltaTime;

				if (enemyAttackTimer >= 0.5f)
				{
				animator.SetBool("Mordisco", true);
				}

				if((enemyAttackTimer > 1.6f) && (enemyAttackTimer < 1.8f))
				{
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);
					lastPosition = samurai.position;

				}
				
				if (enemyAttackTimer >= 1.75f)
				{
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 15f;
					if(enemyAttackTimer >= 1.9f)
						navAgent.Stop ();

				}

				if(enemyAttackTimer >= 1.6f)
				{
					
					animator.SetBool ("Mordisco", false);
				}
				
				if((enemyAttackTimer > 1.9f) && (enemyAttackTimer < 2f))
					damageMordiscoBox.SetActive(true);		

				else if(enemyAttackTimer >= 2f)
					damageMordiscoBox.SetActive(false);

				if(enemyAttackTimer < 3.3f)
				{
					lastFront = front.position;
					lastDashBack = dashBack.position;
				}
//				
				else if (enemyAttackTimer >= 3.3f)
				{
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					
					if(angle < 50)
					{
						random = Random.Range(0, 10);
						
						if (random < 5)
						{
							animator.SetBool("Search", true);
							state = State.SEARCH;
							enemyAttackTimer = 0;
						}
						else if (random >= 5)
						{
							state = State.BACK;
							enemyAttackTimer = 0;
							
						}
				}
				
					if(angle > 50)
					{
						random = Random.Range(0, 10);
						
						if (random < 5)
						{
							animator.SetBool("Search", true);
							state = State.SEARCH;
							enemyAttackTimer = 0;
						}
						else if (random >= 5)
						{
							state = State.BACK;
							enemyAttackTimer = 0;
						}
					}
				}
			}
				break;

			case State.ZARPAZO:
			{
			//Debug.Log("Zarpazo");
				enemyAttackTimer += Time.deltaTime;
				//animator.SetBool ("Idle", false);
				
				if (enemyAttackTimer < 0.7f)
				{
					navAgent.Stop ();
				}

				if(enemyAttackTimer >= 0.3f)
					animator.SetBool("Zarpazo", true);
				
				if(enemyAttackTimer < 1.5f)
					lastPosition = samurai.position;

				if (enemyAttackTimer >= 1.55f)
				{
					navAgent.Resume();
					navAgent.destination = lastPosition;
					navAgent.speed = 20;
				}

				if (enemyAttackTimer > 2f)
				{
					navAgent.Stop ();
				}
				
				if(enemyAttackTimer >= 1f)
				{
					
					animator.SetBool ("Zarpazo", false);
				}
				
				if((enemyAttackTimer > 2f) && (enemyAttackTimer < 2.5f))
					damageZarpazoBox.SetActive(true);		
				
				else if(enemyAttackTimer >= 2.5f)
					damageZarpazoBox.SetActive(false);
				
				if (enemyAttackTimer < 4)
				{
					lastFront = front.position;
					lastDashBack = dashBack.position;
				}
				else if (enemyAttackTimer >= 4f)
				{
					Vector3 direction = samurai.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					
					if(angle < 50)
					{
						random = Random.Range(0, 10);
						
						if (random < 5)
						{
							animator.SetBool("Search", true);
							state = State.SEARCH;
							enemyAttackTimer = 0;
						}
						else if (random >= 5)
						{
							state = State.BACK;
							enemyAttackTimer = 0;
							
						}
					}
					
					if(angle > 50)
					{
						random = Random.Range(0, 10);
						
						if (random < 5)
						{
							animator.SetBool("Search", true);
							state = State.SEARCH;
							enemyAttackTimer = 0;
						}
						else if (random >= 5)
						{
							state = State.BACK;
							enemyAttackTimer = 0;
							
						}
						
					}
				}
			}
				break;
				
			case State.JUMP:
			{
				enemyAttackTimer += Time.deltaTime;

				if (enemyAttackTimer < 0.6)
				{
				Vector3 direction = samurai.position - transform.position;
				float angle = Vector3.Angle(direction, transform.forward);
				enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(samurai.position - enemy.position), rotationSearch * Time.deltaTime);

				animator.SetBool("Jump", true);
				lastRandom1 = random1.position;
				lastRandom2 = random2.position;

				}
				else if (enemyAttackTimer >= 0.6)
				{
					navAgent.Resume();
					navAgent.destination = lastRandom1;
					navAgent.speed = 20;
				}
				if(enemyAttackTimer >= 2.3f)
				{
					navAgent.Resume();
					navAgent.destination = lastRandom2;
					navAgent.speed = 15f;
				}
				
				if(enemyAttackTimer >= 0.5f)
				{
					animator.SetBool ("Jump", false);
				}
				
				if((enemyAttackTimer > 0.7f) && (enemyAttackTimer < 1f))
					damageJumpBox1.SetActive(true);		
				
				else if((enemyAttackTimer > 1.3f) && (enemyAttackTimer <= 2.5f))
					damageJumpBox1.SetActive(false);

				else if((enemyAttackTimer > 2.5f) && (enemyAttackTimer <= 3))
					damageJumpBox2.SetActive(true);

				else if(enemyAttackTimer > 3)
					damageJumpBox2.SetActive(false);
				
				if (enemyAttackTimer >= 5)
				{	
					animator.SetBool("Search", true);
					state = State.SEARCH;
					enemyAttackTimer = 0;
				}
			}
				break;

			case State.BACK:
			{
				enemyAttackTimer += Time.deltaTime;

				if(enemyAttackTimer > 0)
					animator.SetBool ("DashBack", true);

				if (enemyAttackTimer >= 0.2f)
				{
					Vector3 direction = lastFront - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					enemy.rotation = Quaternion.Slerp(enemy.rotation, Quaternion.LookRotation(lastFront - enemy.position), 15 * Time.deltaTime);
					
					navAgent.Resume();
					navAgent.destination = lastDashBack;
					navAgent.speed = 8f;
				}
				if (enemyAttackTimer > 1.9f)
				{
					random = Random.Range(0, 10);
					if(random < 7)
					{
						animator.SetBool("DashBack", false);
						animator.SetBool("Search", true);
						state = State.SEARCH;
						enemyAttackTimer = 0;
				}
					if(random >= 7)
					{
					animator.SetBool("DashBack", false);
					animator.SetBool("Howl", true);
					state = State.HOWL;
					enemyAttackTimer = 0;
					}
				}

			}
				break;

			case State.HOWL:
			{
				enemyAttackTimer+= Time.deltaTime;
				if(enemyAttackTimer < 0.5f)
				{
					animator.SetBool("Howl", true);
				}
				else if( enemyAttackTimer >= 0.5f)
				{
					animator.SetBool("Howl", false);
				}

				if((enemyAttackTimer> 1) && (enemyAttackTimer <= 3))
					damageHowlSphere.SetActive(true);

				if(enemyAttackTimer> 3)
					damageHowlSphere.SetActive(false);

				if (enemyAttackTimer > 4)
				{
					animator.SetBool("Search", true);
					enemyAttackTimer = 0;
					state = State.SEARCH;
				}
			}
				break;
			case State.DEATH:
			{
				if(enemyAttackTimer <= 0.5)
					enemyAttackTimer += Time.deltaTime;

				damageMordiscoBox.SetActive(false);
				damageZarpazoBox.SetActive(false);
				damageJumpBox1.SetActive(false);
				damageJumpBox2.SetActive(false);
				collider.enabled = false;
				collisionBox.SetActive(false);
				if (enemyAttackTimer < 0.5f)
				{
					animator.SetBool("Death", true);
				}
				else if (enemyAttackTimer >= 0.5f)
				{
					animator.SetBool("Death", false);
				}
			}
				break;
		}
	}
}
