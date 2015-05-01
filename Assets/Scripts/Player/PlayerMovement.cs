using UnityEngine;
using UnityEngine;
using System.Collections;
using XInputDotNetPure;
public class PlayerMovement : MonoBehaviour
{
	public static bool grounded;
	public static Vector3 interpolateDirection;
	public static float jumpSpeed = 7F;
	public static Vector3 objectiveDirection;
	public static float seatheCooldown = 0;
	public static float speedA = 3.0F;
	public static float speedD = 3.0F;
	public static float speedS = 1.55F;
	public static float speedW = 3.0F;
	public static float sprint = 4.5F;
	public static float diagAW = 2.1F;
	public static float diagDW = 2.1F;
	public float activeMov = 0;
	public float animationDirectionW = 10;
	public float animationDirectionA = 0;
	public float animationDirectionD = 20;
	public float animationDirectionAW = 5;
	public float animationDirectionDW = 15;
	public float animationSpeedBack = -2;
	public float animationSpeedIdle = 0;
	public float animationSpeedRun = 3;
	public float animationSpeedSprint = 4.5f;
	public float back = 1.5F;
	public bool blocked;
	public static bool combat;
	public float countRest;
	public static float stamina = 100;
	public float maxStamina = 100;
	public float dashCooldown;
	public float dashStamina = 500;
	public float dashTimer;
	public float fall = 0F;
	public float gravity = 18F;
	public float idleCount = 0;
	public bool fallBool = false;
	public bool knockBackBoss = false;
	public float land = 0;
	public float lateral = 6.0F;
	public float moveA = 0;
	public float moveD = 0;
	public float moveS = 0;
	public float moveW = 0;
	public bool rest;
	public bool sprintActive;
	public bool stun;
	//public float transitionSpeed = 25 * Time.deltaTime;
	float acceleration = 0.2F;
	public static Animator animator;
	bool GodMode = false;
	PlayerIndex playerIndex;
	bool playerIndexSet = false;
	GamePadState prevState;
	GamePadState state;

	public bool staminaReload;

	public static bool hit;
	public static bool enemyHit;
	public float hitCounter;

	public bool stepActive = false;

	public float attackTimer;
	public float stepCount;
	public float stepCooldown = 0.8f;
	public float stepCooldown2 = 1.2f;

	public float attackCount;
	public float clickCount;

	public bool sprintCooldown = false;


	public static void Blocked()
	{
		speedW = 0;
		speedA = 0;
		speedD = 0;
		speedS = 0;
		sprint = 0;
		diagAW = 0;
		diagDW = 0;
		jumpSpeed = 0;
	}
	public static void Unblocked()
	{
		speedW = 3;
		speedA = 3;
		speedD = 3;
		speedS = 1.55f;
		sprint = 4.5f;
		diagAW = 2.1f;
		diagDW = 2.1f;
		jumpSpeed = 7F;
	}
	private void HorizontalMovement(float speedW, float speedA, float speedD, float speedS, float rootA, float rootD)
	{
		//Idle extra
//		if (animationSpeed == 0)
//		{
//			idleCount += Time.deltaTime;
//			if (idleCount >= 10)
//				animator.SetBool("Idle", true);
//		}
//		else
//		{
//			idleCount = 0;
//			animator.SetBool("Idle", false);
//		}

		// 																				>>>>>>>>>>> PLAYER HIT FEEDBACK <<<<<<<<<<<<
		if (animator.GetBool("Hit") == true)
		{
			enemyHit = true;
		}
		if(enemyHit)
		{
			hitCounter += Time.deltaTime;

			objectiveDirection = new Vector3(0, 0, 0 );
			objectiveDirection = transform.TransformDirection(objectiveDirection);

			if(hitCounter >0.1f)
				animator.SetBool("Hit", false);
			if(hitCounter > 1f)
			{
				enemyHit = false;
				hitCounter = 0;
			}

		}

		// 																			>>>>>>>>>>>>>>> PLAYER MOVEMENT <<<<<<<<<<<<<<<<<<<
		else if ((dashTimer <= 0.3) && (stepCount == 0))
		{
            if (seatheCooldown == 0)
            {
                // Assign a direction depending on the input introduced
                //																	>>>>>>>>>>> DIAGONAL LEFT<<<<<<<<<<<<<<<<

                if ((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0)) && ((Input.GetKey(KeyCode.A) || (state.ThumbSticks.Left.X < 0))))
                {
                    if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive))
                    {
                        diagAW = 3.15f;
                        animator.SetFloat("Speed", animationSpeedSprint, 0.1f, Time.deltaTime);
                    }
                    else
                    {
                        diagAW = 2.1f;
                        animator.SetFloat("Speed", animationSpeedRun, 0.05f, Time.deltaTime);
                    }

                    animator.SetFloat("Direction", animationDirectionAW, 0.1f, Time.deltaTime);

                    objectiveDirection = new Vector3(-diagAW, objectiveDirection.y, diagAW);
                    transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                }
                //																	>>>>>>>>>>>> DIAGONAL RIGHT <<<<<<<<<<<<<<<<

                else if ((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0)) && ((Input.GetKey(KeyCode.D) || (state.ThumbSticks.Left.X > 0))))
                {
                    if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive))
                    {
                        diagDW = 3.15f;

                        animator.SetFloat("Speed", animationSpeedSprint, 0.1f, Time.deltaTime);
                    }
                    else
                    {
                        diagDW = 2.1f;

                        animator.SetFloat("Speed", animationSpeedRun, 0.05f, Time.deltaTime);
                    }

                    animator.SetFloat("Direction", animationDirectionDW, 0.1f, Time.deltaTime);

                    objectiveDirection = new Vector3(diagDW, objectiveDirection.y, diagDW);
                    transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                }

                // 														>>>>>>>>>>> RIGHT <<<<<<<<<<<<

                else
                {
                    if (Input.GetKey(KeyCode.D) || (state.ThumbSticks.Left.X > 0))
                    {
                        rest = false;


                        animator.SetFloat("Direction", animationDirectionD, 0.1f, Time.deltaTime);

                        if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive))
                        {
                            speedA = sprint;
                            animator.SetFloat("Speed", animationSpeedSprint, 0.1f, Time.deltaTime);
                        }
                        else
                        {
                            speedA = 3;
                            animator.SetFloat("Speed", animationSpeedRun, 0.05f, Time.deltaTime);
                        }

                        if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
                        {
                            speedA = 7;
                            animator.SetFloat("Direction", 20);
                        }
                        objectiveDirection = new Vector3(speedA, objectiveDirection.y, 0);
                        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                    }

                    //															>>>>>>>>>> LEFT <<<<<<<<<<

                    else if (Input.GetKey(KeyCode.A) || (state.ThumbSticks.Left.X < 0))
                    {
                        rest = false;


                        animator.SetFloat("Direction", animationDirectionA, 0.1f, Time.deltaTime);

                        if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive))
                        {
                            speedA = sprint;
                            animator.SetFloat("Speed", animationSpeedSprint, 0.1f, Time.deltaTime);
                        }
                        else
                        {
                            speedA = 3;
                            animator.SetFloat("Speed", animationSpeedRun, 0.05f, Time.deltaTime);
                        }

                        if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
                        {
                            speedA = 7;
                            animator.SetFloat("Direction", 0);
                        }
                        objectiveDirection = new Vector3(-speedA, objectiveDirection.y, 0);
                        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                    }

                    //														>>>>>>>>>>>>>> FRONT <<<<<<<<<<<<<

                    else if (Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0))
                    {
                        rest = false;

                        if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive))
                        {
                            speedW = sprint;
                            animator.SetFloat("Speed", animationSpeedSprint, 0.1f, Time.deltaTime);
                        }
                        else
                        {
                            speedW = 3;
                            animator.SetFloat("Speed", animationSpeedRun, 0.05f, Time.deltaTime);
                        }

                        animator.SetFloat("Direction", animationDirectionW, 0.1f, Time.deltaTime);

                        if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
                        {
                            animator.SetFloat("Direction", 10);
                            speedW = 7;
                        }

                        objectiveDirection = new Vector3(0, objectiveDirection.y, speedW);
                        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

                    }

                    //														>>>>>>>>>>> BACK <<<<<<<<<<<

                    else if (Input.GetKey(KeyCode.S) || (state.ThumbSticks.Left.Y < 0))
                    {
                        rest = false;

                        animator.SetFloat("Speed", animationSpeedBack, 0.05f, Time.deltaTime);

                        animator.SetFloat("Direction", animationDirectionW, 0.1f, Time.deltaTime);

                        if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
                        {
                            animator.SetFloat("Direction", 10);
                            speedS = 6;
                        }

                        objectiveDirection = new Vector3(0, objectiveDirection.y, -speedS);
                        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);


                    }
                    else
                    {
                        animator.SetBool("Run", false);

                        animator.SetFloat("Speed", animationSpeedIdle, 0.1f, Time.deltaTime);

                        objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
                        if (combat)
                        {
                            animator.SetFloat("Direction", animationDirectionW, 0.1f, Time.deltaTime);
                        }
                    }
                }
                objectiveDirection = transform.TransformDirection(objectiveDirection);

                if ((combat) && (animator.GetFloat("Speed") == 0))
                {
                    objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
                    transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
                    objectiveDirection = transform.TransformDirection(objectiveDirection);
                }

            }
            else
            {
                objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
            }
		}
	}

	void Start()
	{
		dashTimer = 0;
		dashCooldown = 0.8f;
		animator = GetComponent<Animator> ();
		countRest = 0;
		rest = false;
		stun = false;
		hit = false;
		hitCounter = 0;
	}
	void Update()
	{
		if (!playerIndexSet || !prevState.IsConnected)
		{
			for (int i = 0; i < 4; ++i)
			{
				PlayerIndex testPlayerIndex = (PlayerIndex)i;
				GamePadState testState = GamePad.GetState(testPlayerIndex);
				if (testState.IsConnected)
				{
					Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
					playerIndex = testPlayerIndex;
					playerIndexSet = true;
				}
			}
		}
		prevState = state;
		state = GamePad.GetState(playerIndex);
		
	
		if (Input.GetKeyUp(KeyCode.Alpha9))
		{
			if (GodMode == true)
			{
				GodMode = false;
				activeMov = 0;
			}
			else if (GodMode == false)
			{
				GodMode = true;
				activeMov = 1;
			}
		}
		if (GodMode == false)
		{
			CharacterController controller = gameObject.GetComponent<CharacterController>();
			float rootA = Mathf.Sqrt(speedA * speedW / 2);
			float rootD = Mathf.Sqrt(speedD * speedW / 2);
			// Acceleration and deceleration
			interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, acceleration),
			                                   objectiveDirection.y,
			                                   Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, acceleration));

			// 																							>>>>>>>>> REST HEALTH <<<<<<<<<<<
			if (Input.GetKeyDown(KeyCode.X))
			{
				rest = true;
			}
			
			if (rest)
			{
				animator.SetBool("Rest", true);
				countRest += Time.deltaTime;
				if (countRest >= 3) PlayerHealth.curHealth += 10*Time.deltaTime;
			}
			else if (!rest)
			{
				animator.SetBool("Rest", false);
				countRest = 0;
			}

			
			// Calculates the direction

			HorizontalMovement(speedW, speedA, speedD, speedS, rootA, rootD);
			
			if (dashTimer > 0)
				dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
			{
				animator.SetBool("Dash", false);
				animator.SetBool("DashBack", false);
				dashTimer = 0;
			}




			// 																				>>>>>>>>>>> SPRINT LOGIC <<<<<<<<<<<<
			if (!combat)
			{
				if ((Input.GetKey(KeyCode.LeftShift)) && (animator.GetFloat("Speed") > 1))
				{
					staminaReload= false;

					if ((stamina > 0) && (sprintCooldown == false))
					{
						sprintActive = true;
						stamina -= Time.deltaTime * 10;
					}

					else if (stamina <= 0)
					{
						sprintActive = false;
						sprintCooldown = true;
					}
					if (stamina > 20)
						sprintCooldown = false;

					if (sprintCooldown == true)
						stamina += Time.deltaTime * 10;


				}
				else if(Input.GetKeyUp(KeyCode.LeftShift))
				{
					staminaReload = true;

					sprintActive = false;

					if (stamina> 20)
						sprintCooldown = false;

					else if (stamina <= 20)
						sprintCooldown = true;

				}
			}

			//													>>>>>>>>>>>> STAMINA RELOAD <<<<<<<<<<<
			if (staminaReload)
			{
				if(stamina< maxStamina)
					stamina += Time.deltaTime * 15;
				
				if(stamina >= maxStamina) 
					stamina = maxStamina;

			}
			else
			{
				if(stamina <= 0)
					stamina = 0;
			}
			//  																		>>>>>>>>>>>>>>>> GROUNDED <<<<<<<<<<<<<<<<<<<
			if(controller.isGrounded)
			{
				fall = 0;
				animator.SetBool ("Fall", false);
				grounded = true;
				fallBool = false;
				//  																		>>>>>>>>>> SEATHE/UNSEATHE LOGIC <<<<<<<<<<<
				if(seatheCooldown == 0)
				{
					if (VisKatana.herreria)
					{
						if ((Input.GetKeyDown(KeyCode.Q)) || (state.Buttons.X == ButtonState.Pressed))
						{
							combat = !combat;
							seatheCooldown += 0.1f;
						}
					}
				}
				// 																			>>>>>>>>>>>>>> SEATHE/UNSEATHE LOGIC <<<<<<<<<<<
				if (combat)
				{
					if((seatheCooldown > 0) && (seatheCooldown < 0.8f))
					{
						
						seatheCooldown += Time.deltaTime;
						//Blocked ();
					}
					else if (seatheCooldown >=0.8f)
					{
						
						seatheCooldown = 0;
						//Unblocked ();
					}
					animator.SetBool("Combat", true);
				}
				else if (!combat)
				{
					if((seatheCooldown > 0) && (seatheCooldown < 2.3f))
					{
						seatheCooldown += Time.deltaTime;
						//Blocked ();
					}
					else if (seatheCooldown >= 2.3f)
					{
						seatheCooldown = 0;
						//Unblocked ();
					}
					animator.SetBool("Combat", false);
				}

				animator.SetBool ("Jump", false);
				animator.SetBool ("Grounded", true);
				if(land < 6)
				{
					land = 0;
				}
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speedW = 3;
				}

				//  																					>>>>>>>>>>>> JUMP LOGIC <<<<<<<<<<<<<
				if ((Input.GetKeyDown(KeyCode.Space)) || (state.Buttons.A == ButtonState.Pressed))
				{
					if(!enemyHit)
					{
						grounded = false;
						rest = false;
						animator.SetBool("Jump", true);
						objectiveDirection = new Vector3(objectiveDirection.x, jumpSpeed, objectiveDirection.z);
						//objectiveDirection.y = jumpSpeed;
					}
				}


				//  																		>>>>>>>>>>> DASH LOGIC <<<<<<<<<<<<
				if ((dashTimer == 0) && (stamina> 30))
				{
					if(attackCount == 0)
					{	
						if (animator.GetFloat("Speed") != 0)
						{
							if((Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.B == ButtonState.Pressed)))
							{
								dashTimer = dashCooldown;
								stamina -= 30;
							}
						}
					}
				}
				else if ( dashTimer > 0.1f)
				{
					staminaReload = false;

					if((animator.GetFloat("Speed") > 0)&& (Input.GetKey(KeyCode.S)))
					{
						animator.SetBool ("DashBack", true);
					}

					else if(animator.GetFloat("Speed") > 0)
						animator.SetBool("Dash", true);

					else if(animator.GetFloat("Speed") < 0)
					{
						animator.SetBool ("DashBack", true);
					}
				}
				
				else
				{
					//objectiveDirection = new Vector3((objectiveDirection.x), 0, (objectiveDirection.z));
					animator.SetBool ("Dash", false);
					animator.SetBool ("DashBack", false);
					staminaReload = true;
				}


				//  																	>>>>>>>>>>>> ATTACK MOVEMENT LOGIC <<<<<<
				attackCount = PlayerAttack.attackCount;
				clickCount = PlayerAttack.clickCount;
				attackTimer = PlayerAttack.attackTimer;
				stepCount= PlayerAttack.stepCount;

				if ((stepCount> 0) && (dashTimer == 0))
				{
					//animationSpeed = 0;
					//animationDirection = 10;
					objectiveDirection = new Vector3(0, objectiveDirection.y, speedW);
					objectiveDirection = transform.TransformDirection(objectiveDirection);

					if(attackTimer == 0)
					{
						transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
					}
				}

				if (combat)
				{
					if(stepCount == 0)
						staminaReload = true;
					else
						staminaReload = false;

					if(stepCount == 1)
					{
						if (attackTimer == 0)stamina -= 10;
						animator.SetFloat("Speed", 0);
						animator.SetFloat("Direction", 10);
					
						if(attackTimer < 0.15f)
						{
							if(hit)
								speedW = 0;
							else
								speedW = 5f;
						}
							//speedW = 0;
						else
						{
							hit = false;
							speedW = 0;
						}
					}
					else if(stepCount == 2)
					{
						if (attackTimer == 0)stamina -= 10;

						if(attackTimer < 0.15f)
						{
							if(hit)
								speedW = 0;
							else
								speedW = 7f;
						}
						else
						{
							//hit = false;
							speedW = 0;
						}
					}
					else if(stepCount == 3)
					{
						if (attackTimer == 0)stamina -= 10;

						if(attackTimer < 0.1f)
						{
							if(hit)
								speedW = 0;
							else
								speedW = 5f;
						}
						else if((attackTimer > 0.1f) && (attackTimer <0.3f))
						{
							hit = false;

							speedW = 0;
						}
						else if((attackTimer > 0.3f) && (attackTimer <0.43f))
						{
							if(hit)
								speedW = 0;
							else
								speedW = 6;
						}
						else
							speedW = 0;

					}
					else if(stepCount == 6)
					{
						animator.SetFloat("Speed", 0);
						animator.SetFloat("Direction", 10);

						transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
						if ((animator.GetBool ("Charge") == true) && (attackTimer < 1.5f))
							stamina -= 15*Time.deltaTime;

						speedW = 0;
					}
					else if(stepCount == 7)
					{
						speedW = 25;
					}
					else if(stepCount == 8)
					{
						speedW = 0;
					}
					
				}

			}
			else
			{
				animator.SetBool("Grounded", false);

				//reinicia la gravedad cuando te dejas caer (sin jump)
				fall += Time.deltaTime;

				if((fall <= 0.02f) && (objectiveDirection.y < 1))
				{
					objectiveDirection.y = 0;
					fallBool = true;
				}
				if ((fallBool) && ( fall > 0.2f))
					animator.SetBool ("Fall", true);

				objectiveDirection += new Vector3(objectiveDirection.x, -gravity , objectiveDirection.z) * Time.deltaTime;
				//comprueba cuanto rato llevas en el aire (controlar el Land)
				if(land < 6)
				{
					land += 0.5f * Time.deltaTime;
				}
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speedW = 3;
				}
				//fall += gravity;

			}
			if (knockBackBoss)
			{
				
			}
			else controller.Move(interpolateDirection * Time.deltaTime);
			
		}
		if (GodMode == true)
		{
			float speed = 5;
			CharacterController controller = gameObject.GetComponent<CharacterController>();
			// Calculates the module of the speed
			//float root = Mathf.Sqrt(speed * speed / 2);
			float rootA = Mathf.Sqrt(speedA * speedW / 2);
			float rootD = Mathf.Sqrt(speedD * speedW / 2);
			// Acceleration and deceleration
			interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, acceleration),
			                                   objectiveDirection.y,
			                                   Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, acceleration));
			// Calculates the direction
			HorizontalMovement(speedW, speedA, speedD, speedS, rootA, rootD);
			if (Input.GetKey(KeyCode.Space))
			{
				objectiveDirection = new Vector3(objectiveDirection.x, speed, objectiveDirection.z);
			}
			else if (Input.GetKey(KeyCode.LeftShift))
			{
				objectiveDirection = new Vector3(objectiveDirection.x, -speed, objectiveDirection.z); ;
			}
			else
			{
				objectiveDirection = new Vector3(objectiveDirection.x, 0, objectiveDirection.z);
			}
			//objectiveDirection += new Vector3(objectiveDirection.x, -gravity * 1.5f, objectiveDirection.z) * Time.deltaTime;
			controller.Move(interpolateDirection * Time.deltaTime);

		}
	}
}