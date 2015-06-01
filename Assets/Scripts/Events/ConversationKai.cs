using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ConversationKai : MonoBehaviour 
{
    public Transform playerTransform;
	public Transform puente;
	public Transform campo;
	public Transform mercado;
	public Transform plazaBoss;


	public GameObject player;
	public GameObject loboTutorial00;
	public GameObject loboTutorial01;

	public GameObject loboCollider00;
	public GameObject loboCollider01;
    
    public static bool activeConversation;
    //GameObject[] conversationControl;

    public Camera cam1;
    public Camera cam2;
	public Camera camCampo;

	public bool combat;

	public GameObject colliderLobo03;
	public GameObject colliderLobo04;

	public Transform loboBoss;

	private Animator animator;

	private NavMeshAgent navAgent;

	public float kaiTimer;

	public bool mercadoPlayer;

	public bool choseWeapon;
	public static bool choseWeapon2;
	public GameObject choseWeaponText;

	public GameObject unsheatheTutorial;
	public GameObject attackTutorial;
	public GameObject forjaTrigger;

	public GameObject katana;
	public GameObject funda;


	//CANVAS
	public static float conversationState;
	public static float conversationTimer;

	public GameObject useE;

	public GameObject playerTalk;
	public GameObject skip;

	public GameObject puenteTalk01;
	public GameObject puenteTalk02;
	public GameObject puenteTalk03;

	public GameObject campoTalk01;
	public GameObject campoTalk02;

	public GameObject mercadoTalk01;
	public GameObject mercadoTalk02;

	private enum State {PUENTE, CAMPO, MERCADO, MERCADO2, BOSS, RUN, ATTACK};
	private State state;

    PlayerIndex playerIndex;
    GamePadState prevStateGamepad;
    GamePadState stateGamepad;

    void Start() 
    {
		conversationState = 0;
		conversationTimer = 0;

		choseWeapon = false;
		choseWeapon2 = false;

		mercadoPlayer = true;

        cam1.enabled = true;
        cam2.enabled = false;
		camCampo.enabled = false;

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
        prevStateGamepad = stateGamepad;
        stateGamepad = GamePad.GetState(playerIndex);

		switch (state)
		{
			case State.PUENTE:
			{	
				
				//GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
		        //LoadText loadText = disableConversation.GetComponent<LoadText>();
				if (!loboCollider01.activeSelf)
				{
					Objectives.objectivesCount = 2.5f;
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
		            
					if(conversationState == 0)
					{
						playerTalk.SetActive(true);

			            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
			            {	
							playerTalk.SetActive(false);
							puenteTalk01.SetActive(true);

							player.SetActive(false);
			                cam1.enabled = false;
			                cam2.enabled = true; 
							conversationState = 1;
			            }
					}
					else if (conversationState == 1)
					{

						if(conversationTimer <= 0.5f)
						{	
							Objectives.objectivesCount = 0.5f;
							animator.SetBool("TalkNormal",true);
							conversationTimer += Time.deltaTime;
						}
						else
						{
							animator.SetBool("TalkNormal", false);
							skip.SetActive(true);
                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								skip.SetActive (false);
								puenteTalk01.SetActive(false);
								puenteTalk02.SetActive(true);
								conversationTimer = 0;
								conversationState = 2;
							}
						}
					}
					else if (conversationState == 2)
					{
						if(conversationTimer < 0.5f)
						{
							animator.SetBool("TalkMark",true);
							conversationTimer += Time.deltaTime;
						}
						else
						{
							animator.SetBool("TalkMark",false);
							skip.SetActive(true);

                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								skip.SetActive(false);
								puenteTalk02.SetActive(false);
								puenteTalk03.SetActive(true);
								conversationTimer = 0;
								conversationState = 3;
							}
						}
					}
					else if (conversationState == 3)
					{
						
						if(conversationTimer < 0.5f)
							conversationTimer += Time.deltaTime;
						
						else
						{
							skip.SetActive(true);
                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								skip.SetActive(false);
								puenteTalk03.SetActive(false);
								conversationTimer = 0;
								conversationState = 4;
							}
						}
					}
					else if (conversationState == 4)
					{
						Objectives.objectivesCount = 1;
						player.SetActive(true);
						cam2.enabled = false;
						cam1.enabled = true;
						conversationTimer += Time.deltaTime;
						if(conversationTimer > 1)
						{
							conversationState = 0;
							conversationTimer = 0;
						}
					}
				}
				else
				{
					conversationState = 0;
					conversationTimer = 0;
					playerTalk.SetActive(false);
				}
				if (CampoEvent.weaponActive)
				{
					if(!choseWeapon)
					{
						useE.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
						{
							Objectives.objectivesCount = 1.5f;
							useE.SetActive(false);
							choseWeaponText.SetActive(true);
							player.SetActive(false);
							cam1.enabled = false;
							camCampo.enabled = true;

							choseWeapon = true;
							
						}
					}
					else
					{
                        if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
						{
							katana.SetActive(false);
							funda.SetActive(false);
							unsheatheTutorial.SetActive(true);
							choseWeaponText.SetActive(false);
							player.SetActive(true);

							choseWeapon2 = true;

							camCampo.enabled = false;
							cam1.enabled = true;
							
							Destroy (forjaTrigger);

							Objectives.objectivesCount = 2;
						}
					}
				}
				else
				{
					useE.SetActive(false);
				}
				
				if(Tutorials.tutorialCounter == 3.5)
				{
					if((Input.GetKeyDown(KeyCode.Q)) || ((prevStateGamepad.Buttons.Y == ButtonState.Released) && (stateGamepad.Buttons.Y == ButtonState.Pressed)))
					{
						loboTutorial00.SetActive(true);
					}
					
				}
				else if (Tutorials.tutorialCounter == 4)
				{
					attackTutorial.SetActive(true);
				}
				else if (Tutorials.tutorialCounter == 5)
				{
					if(!loboCollider00.activeSelf)
					{
						Tutorials.tutorialTimer += Time.deltaTime;
					}
				}
				else if (Tutorials.tutorialCounter == 5.5f)
				{
					loboTutorial01.SetActive (true);
				}
				
		    }
				break;

			case State.CAMPO:
			{	
				
				//GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
				//LoadText loadText = disableConversation.GetComponent<LoadText>();
				if(Vector3.Distance(transform.position, playerTransform.position) > 15)
				{
					navAgent.speed = 10;
				}
				else
				{
					Objectives.objectivesCount = 3;
					navAgent.speed = 4f;
				}

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
						
						if(conversationState == 0)
						{
							playerTalk.SetActive(true);

                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								playerTalk.SetActive(false);
								campoTalk01.SetActive(true);

								player.SetActive(false);
								cam1.enabled = false;
								cam2.enabled = true;   
								conversationState = 1;
							}
						}
						else if (conversationState == 1)
						{
							
							if(conversationTimer < 0.5f)
							{
								Objectives.objectivesCount = 3.5f;
								conversationTimer += Time.deltaTime;
							}
							else
							{
								skip.SetActive(true);
                                if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
								{
									skip.SetActive (false);
									campoTalk01.SetActive(false);
									campoTalk02.SetActive(true);
									conversationTimer = 0;
									conversationState = 2;
								}
							}
						}
						else if (conversationState == 2)
						{
							
							if(conversationTimer < 0.5f)
							{
								animator.SetBool ("TalkFollow", true);
								conversationTimer += Time.deltaTime;
							}
							else
							{
								animator.SetBool ("TalkFollow", false);
								skip.SetActive(true);

                                if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
								{
									skip.SetActive (false);
									campoTalk02.SetActive(false);
									conversationTimer = 0;
									conversationState = 3;
								}
							}
						}
						else if (conversationState == 3)
						{
							Objectives.objectivesCount = 4;
							player.SetActive(true);
							
							cam2.enabled = false;
							cam1.enabled = true;
							
							state = State.MERCADO;
						}
					}
					else
					{
						playerTalk.SetActive(false);
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
					Objectives.objectivesCount = 4.5f;
					state = State.MERCADO2;
				}
			}
				break;

			case State.MERCADO2:
			{
				//GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
				//LoadText loadText = disableConversation.GetComponent<LoadText>();

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
						Objectives.objectivesCount = 5;
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
					
					if(conversationState == 3)
					{
						playerTalk.SetActive(true);
                        if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
						{
							playerTalk.SetActive(false);
							player.SetActive(false);

							mercadoTalk01.SetActive(true);
							cam1.enabled = false;
							cam2.enabled = true;   
							conversationState = 4;
						}
					}
					else if (conversationState == 4)
					{
						
						if(conversationTimer < 0.5f)
						{
							Objectives.objectivesCount = 5.5f;
							animator.SetBool("TalkThanks", true);
							conversationTimer += Time.deltaTime;
						}
						else
						{
							animator.SetBool("TalkThanks", false);
							skip.SetActive(true);
                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								skip.SetActive (false);
								mercadoTalk01.SetActive(false);
								mercadoTalk02.SetActive(true);
								conversationTimer = 0;
								conversationState = 5;
							}
						}
					}
					else if (conversationState == 5)
					{
						
						if(conversationTimer < 0.5f)
						{
							animator.SetBool("TalkNormal", true);
							conversationTimer += Time.deltaTime;
						}
						else
						{
							animator.SetBool("TalkNormal", false);
							skip.SetActive(true);
                            if ((Input.GetKeyDown(KeyCode.E)) || ((prevStateGamepad.Buttons.X == ButtonState.Released) && (stateGamepad.Buttons.X == ButtonState.Pressed)))
							{
								skip.SetActive (false);
								mercadoTalk02.SetActive(false);
								conversationTimer = 0;
								conversationState = 6;
							}
						}
					}
					else if (conversationState == 6)
					{
						Objectives.objectivesCount = 6;
						player.SetActive(true);
						
						cam2.enabled = false;
						cam1.enabled = true;
						
						state = State.BOSS;
					}
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
						Objectives.objectivesCount = 6.5f;
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
					Objectives.objectivesCount = 7;
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
			/*
            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = true;
            Debug.Log("Enable");
            */

            activeConversation = true;

 
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
			animator.SetBool("Talk Bool", false);

            activeConversation = false;
			/*
            GameObject disableConversation = GameObject.FindGameObjectWithTag("ConversationKai");
            LoadText loadText = disableConversation.GetComponent<LoadText>();

            loadText.myText.enabled = false;
            */
        }
    }
}
