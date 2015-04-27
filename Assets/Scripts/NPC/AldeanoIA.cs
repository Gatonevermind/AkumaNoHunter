using UnityEngine;
using System.Collections;

public class AldeanoIA : MonoBehaviour 
{
	private enum State {IDLE, RUN, RUNPLAYER, ATTACK, THANKS};
	private State state;

	public Transform player;
	public Transform enemy;
	public GameObject trigger;

	public float enemyHealth;

	private Animator animator;
	NavMeshAgent navAgent;

	public float aleatorio;

	public float aldeanoAttackTimer;

	void Start ()
	{
		animator = GetComponent<Animator> ();
		navAgent = GetComponent<NavMeshAgent> ();
		state = State.ATTACK;

	}
	void Update ()
	{
		if (!trigger.activeSelf)
		{
			state = State.THANKS;
			animator.SetBool ("Thanks", true);
		}
		switch (state)
		{
			case State.IDLE:
			{
				animator.SetBool("Attack", false);
				animator.SetBool("Idle", true);
			}
				break;

			case State.RUN:
			{	
				animator.SetBool ("Combat", false);

				navAgent.destination = enemy.position;

				if (Vector3.Distance(transform.position, enemy.position) < 1.5f)
				{
					animator.SetBool("Combat", true);
					state = State.ATTACK;
				}
			}
				break;
			case State.ATTACK:
			{
				aldeanoAttackTimer += Time.deltaTime;

				animator.SetBool("Run", false);

				navAgent.Stop ();
				
				Vector3 direction = enemy.position - transform.position;
				
				float angle = Vector3.Angle(direction, transform.forward);
				
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.position - transform.position), 5 * Time.deltaTime);
				
				if ((Vector3.Distance(transform.position, enemy.position) > 1.5f) && (aldeanoAttackTimer > 1))
				{
					aldeanoAttackTimer = 0;
					animator.SetBool("Run", true);
					state = State.RUN;
					navAgent.Resume();
				}
			}
				break;

//			case State.RUNPLAYER:
//			{
//			Debug.Log("RunPlayer");
//				navAgent.Resume();
//				animator.SetBool("Idle", false);
//				animator.SetBool("Attack", false);
//
//				if (Vector3.Distance(transform.position, player.position) > 2)
//				{
//					
//					animator.SetBool("Run", true);
//					navAgent.destination = player.position;
//				}
//				else
//					animator.SetBool("Thanks", true);
//					state = State.THANKS;
//					navAgent.Stop ();
//			}
//				break;
			case State.THANKS:
			{
				navAgent.Stop ();
				

				if (Vector3.Distance(transform.position, player.position) < 3)
				{
				Vector3 direction = player.position - transform.position;
				float angle = Vector3.Angle(direction, transform.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 5 * Time.deltaTime);

				}
				else
				{
				Vector3 direction = enemy.position - transform.position;
				float angle = Vector3.Angle(direction, transform.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemy.position - transform.position), 5 * Time.deltaTime);

				}				
			}
				break;
		}
	}

}


