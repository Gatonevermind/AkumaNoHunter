using UnityEngine;
using System.Collections;

public class ConversationKai : MonoBehaviour 
{
    public Transform playerTransform;
	public Transform puente;
	public Transform campo;
	public Transform mercado;
	public Transform plazaBoss;

	public GameObject player;

	public GameObject loboColliderCampo;
    
    public bool activeConversation;
    //GameObject[] conversationControl;

    public Camera cam1;
    public Camera cam2;

	public bool combat;

	public GameObject colliderLobo03;
	public GameObject colliderLobo04;

	public Transform loboBoss;

	private Animator animator;

	private NavMeshAgent navAgent;

	public float kaiTimer;

	public bool mercadoPlayer;

	private enum State {PUENTE, CAMPO, MERCADO, MERCADO2, BOSS, RUN, ATTACK};
	private State state;

    void Start() 
    {
		mercadoPlayer = true;

        cam1.enabled = true;
        cam2.enabled = false;

		state = State.PUENTE;

		navAgent = GetComponent <NavMeshAgent> ();

		animator = GetComponent <Animator> ();

        activeConversation = false;

        //conversationControl = GameObject.FindGameObjectsWithTag("ConversationKai");

        //foreach (GameObject conversation in conversationControl)
            //conversation.SetActive(false);
    }
    void Update()
    {
		switch (state)
		{
			case State.PUENTE:
			{
		        GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
		        LoadText loadText = disableConversation.GetComponent<LoadText>();
				if (!loboColliderCampo.activeSelf)
				{
					state = State.CAMPO;
				}

				if (!IntroCinematic.intro)
				{
				animator.SetBool("Combat", true);
				}
		        //GetComponent<Animation>().CrossFade(idle.name);
				else if (IntroCinematic.intro)
				{
					if (Vector3.Distance(transform.position, playerTransform.position) > 0.5f)
					{
						navAgent.destination = puente.position;
						animator.SetBool ("Run", true);
						animator.SetBool ("Combat", false);
					}
				}

				if(Vector3.Distance(transform.position, puente.position) < 0.1f)
				{
					navAgent.Stop ();
					animator.SetBool ("Run", false);
				}


		        if (activeConversation)
		        {

					if(player.activeSelf)
					{
						Vector3 direction = playerTransform.position - transform.position;
						float angle = Vector3.Angle(direction, transform.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
					}

					//GetComponent<Animation>().CrossFade(talk.name);

		           // foreach (GameObject conversation in conversationControl)
		               // conversation.SetActive(true);
		            

		            if (Input.GetKeyDown(KeyCode.E))
		            {
						player.SetActive(false);
						animator.SetBool("Talk Bool", true);
		                cam1.enabled = false;
		                cam2.enabled = true;   
		            }
		        }
		        
		        if (loadText.myText.enabled == false)
		        {
					player.SetActive(true);

		            cam2.enabled = false;
		            cam1.enabled = true;
		        }
		        
		    }
				break;

			case State.CAMPO:
			{	
				
				GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
				LoadText loadText = disableConversation.GetComponent<LoadText>();

				if(Vector3.Distance(transform.position, campo.position) > 0.5f)
				{
				navAgent.speed = 6;
				animator.SetBool("Run", true);
				navAgent.Resume();
				navAgent.destination = campo.position;
				}

				else if(Vector3.Distance(transform.position, campo.position) < 0.5f)
				{
					navAgent.Stop ();
					animator.SetBool ("Run", false);
					if (activeConversation)
					{
						if(player.activeSelf)
						{
							Vector3 direction = playerTransform.position - transform.position;
							float angle = Vector3.Angle(direction, transform.forward);
							transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
						}
						
						//GetComponent<Animation>().CrossFade(talk.name);
						
						// foreach (GameObject conversation in conversationControl)
						// conversation.SetActive(true);
						
						
						if (Input.GetKeyDown(KeyCode.E))
						{
							player.SetActive(false);
							animator.SetBool("Talk Bool", true);
							cam1.enabled = false;
							cam2.enabled = true;   
						}
					}
					if ((loadText.myText.enabled == false) && (!player.activeSelf))
					{
						player.SetActive(true);
						
						cam2.enabled = false;
						cam1.enabled = true;

						state = State.MERCADO;
					}
				}
			}
			break;
			
			case State.MERCADO:
			{
				if(Vector3.Distance(transform.position, mercado.position) > 0.1f)
				{
					if(Vector3.Distance(transform.position, playerTransform.position) > 5)
					{
						kaiTimer = 0;
						navAgent.Stop ();
						animator.SetBool("Run", false);

						Vector3 direction = playerTransform.position - transform.position;
						float angle = Vector3.Angle(direction, transform.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
					}
					else if(Vector3.Distance(transform.position, playerTransform.position) < 5)
					{
						kaiTimer += Time.deltaTime;
						if(kaiTimer < 0.5f)
						{
							Vector3 direction = playerTransform.position - transform.position;
							float angle = Vector3.Angle(direction, transform.forward);
							transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
						}
						else
						{
							animator.SetBool("Run", true);
							navAgent.Resume();
							navAgent.destination = mercado.position;
						}
					}
					navAgent.speed = 3.5f;
					
				}
				
				else if(Vector3.Distance(transform.position, mercado.position) < 0.1f)
				{
					kaiTimer = 0;
					navAgent.Stop ();
					animator.SetBool ("Run", false);
				}
				if((!colliderLobo03.activeSelf) && (!colliderLobo04.activeSelf))
				{
					state = State.MERCADO2;
				}
			}
				break;

			case State.MERCADO2:
			{
				GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
				LoadText loadText = disableConversation.GetComponent<LoadText>();

				if(mercadoPlayer)
				{
					if(Vector3.Distance(transform.position, playerTransform.position) > 1.5f)
					{
						animator.SetBool("Run", true);
						navAgent.Resume();
						navAgent.destination = playerTransform.position;
					}
					
					else if(Vector3.Distance(transform.position, playerTransform.position) < 1.5f)
					{
						navAgent.Stop ();
						animator.SetBool ("Run", false);
						mercadoPlayer = false;
					}
				}
				else
				{
					if(Vector3.Distance(transform.position, playerTransform.position) < 2.5f)
					{
						Vector3 direction = playerTransform.position - transform.position;
						float angle = Vector3.Angle(direction, transform.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
					}
				}
				if(activeConversation)
				{
					if(player.activeSelf)
					{
						Vector3 direction = playerTransform.position - transform.position;
						float angle = Vector3.Angle(direction, transform.forward);
						transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.position - transform.position), 5 * Time.deltaTime);
					}
					
					//GetComponent<Animation>().CrossFade(talk.name);
					
					// foreach (GameObject conversation in conversationControl)
					// conversation.SetActive(true);
					
					
					if (Input.GetKeyDown(KeyCode.E))
					{
						player.SetActive(false);
						animator.SetBool("Talk Bool", true);
						cam1.enabled = false;
						cam2.enabled = true;   
					}
				}
				if ((loadText.myText.enabled == false) && (!player.activeSelf))
				{
					player.SetActive(true);
					
					cam2.enabled = false;
					cam1.enabled = true;
					
					state = State.BOSS;
				}
			}
				break;
				
			case State.BOSS:
			{
				if(Vector3.Distance(transform.position, plazaBoss.position) > 0.5f)
				{
					navAgent.Resume ();
					navAgent.destination = plazaBoss.position;
					animator.SetBool("Run", true);
				}
				else
				{
					navAgent.Stop ();
					animator.SetBool ("Run", false);
					if(Vector3.Distance(transform.position, playerTransform.position) < 2)
					{
						state = State.ATTACK;
					}
				}
			}
				break;

			case State.ATTACK:
			{
				if(Vector3.Distance(transform.position, loboBoss.position) > 1.5f)
				{
					navAgent.Resume();
					navAgent.destination = loboBoss.position;
					animator.SetBool("Run", true);
				}
				else 
		        {
					Vector3 direction = loboBoss.position - transform.position;
					float angle = Vector3.Angle(direction, transform.forward);
					transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(loboBoss.position - transform.position), 5 * Time.deltaTime);
					
					navAgent.Stop();
					animator.SetBool("Combat", true);
				}
			}
				break;
		}
	}
	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = true;
            Debug.Log("Enable");
            activeConversation = true;
 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
			animator.SetBool("Talk Bool", false);

            activeConversation = false;

            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = false;
        }
    }
}
