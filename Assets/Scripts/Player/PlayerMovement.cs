using System.Collections;
using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
	public static float grounded;
	public static Vector3 interpolateDirection;
	public static float jumpSpeed = 7F;
	public static Vector3 objectiveDirection;
	public static float seatheCooldown = 0;
	public static float speedA = 3.0F;
	public static float speedD = 3.0F;
	public static float speedS = 1.55F;
	public static float speedW = 3.0F;
	public static float sprint = 4.5F;
	public bool activeEventCombat;
	public float activeMov = 0;
	public float animationDirection = 10;
	public float animationSpeed = 0;
	public float back = 1.5F;
	public bool blocked;
	public float cdRest;
	public bool combat;
	public int curStam = 1000;
	public float dash = 0;
	public float dashCooldown;
	public float dashStamina = 500;
	public float dashTimer;
	public float fall = 0F;
	public float gravity = 18F;
	public float idleCount = 0;
	public float jumpCooldown = 0;
	public float land = 0;
	public float lateral = 6.0F;
	public int maxStam = 1000;
	public bool rest;
	public bool sprintActive;
	public float sprintLateral = 3.0F;
	public float staminaBarLenght;
	public float transitionSpeed = 0.75f;
	private float acceleration = 0.2F;
	private Animator animator;
	private bool GodMode = false;
	private PlayerIndex playerIndex;
	private bool playerIndexSet = false;
	private GamePadState prevState;
	private GamePadState state;
	
	public static void Attack()
	{
		speedW = 1;
		speedA = 0;
		speedD = 0;
		speedS = 0;
		sprint = 0;
		jumpSpeed = 0;
	}
	
	public static void Blocked()
	{
		speedW = 0;
		speedA = 0;
		speedD = 0;
		speedS = 0;
		sprint = 0;
		jumpSpeed = 0;
	}
	
	public static void Unblocked()
	{
		speedW = 3;
		speedA = 3;
		speedD = 3;
		speedS = 1.5f;
		sprint = 4.5f;
		jumpSpeed = 7F;
	}
	
	private void HorizontalMovement(float speedW, float speedA, float speedD, float speedS, float rootA, float rootD)
	{
		if (animationSpeed == 0)
		{
			idleCount += Time.deltaTime;
			if (idleCount >= 10)
				animator.SetBool("Idle", true);
		}
		else
		{
			idleCount = 0;
			animator.SetBool("Idle", false);
		}
		
		if (dashTimer <= 1)
		{
			animator.SetFloat("Direction", animationDirection);
			animator.SetFloat("Speed", animationSpeed);
			// Assign a direction depending on the input introduced
			//NORMAL MOVEMENT
			if ((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0)) && ((Input.GetKey(KeyCode.A) || (state.ThumbSticks.Left.X < 0))))
			{
				if (animationSpeed >= 3)
					animationSpeed = 3;
				else if (animationSpeed < 3)
					animationSpeed += 0.4f;
				
				if (animationDirection < 4.5f)
				{
					animationDirection += transitionSpeed;
				}
				else if (animationDirection > 5.5f)
				{
					animationDirection -= transitionSpeed;
				}
				else if ((animationDirection >= 4.5f) && (animationDirection <= 5.5))
				{
					animationDirection = 5.2f;
				}
				objectiveDirection = new Vector3(-rootA, objectiveDirection.y, rootA);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if ((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0)) && ((Input.GetKey(KeyCode.D) || (state.ThumbSticks.Left.X > 0))))
			{
				if (animationSpeed >= 3)
					animationSpeed = 3;
				else if (animationSpeed < 3)
					animationSpeed += 0.4f;
				
				if (animationDirection < 14.5f)
				{
					animationDirection += transitionSpeed;
				}
				else if (animationDirection > 15.5f)
				{
					animationDirection -= transitionSpeed;
				}
				else if ((animationDirection >= 14.5f) && (animationDirection <= 55.5))
				{
					animationDirection = 14.8f;
				}
				objectiveDirection = new Vector3(rootD, objectiveDirection.y, rootD);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else
			{
				if (Input.GetKey(KeyCode.D) || (state.ThumbSticks.Left.X > 0))
				{
					if (animationDirection < 10)
					{
						speedD = 0;
						animationDirection += transitionSpeed * 2;
					}
					else if ((animationDirection < 20) && (animationDirection >= 10))
					{
						if (seatheCooldown == 0)
						{
							speedD = 3;
						}
						animationDirection += transitionSpeed;
						if (animationSpeed >= 3)
							animationSpeed = 3;
						else if (animationSpeed < 3)
							animationSpeed += 0.4f;
					}
					else if (animationDirection > 20)
					{
						if (animationSpeed >= 3)
							animationSpeed = 3;
						else if (animationSpeed < 3)
							animationSpeed += 0.4f;
					}
					
					if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
					{
						speedD = 5;
					}
					
					objectiveDirection = new Vector3(speedD, objectiveDirection.y, 0);
					transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);        
					
				}
				else if (Input.GetKey(KeyCode.A) || (state.ThumbSticks.Left.X < 0))
				{
					if (animationDirection > 10)
					{
						speedA = 0;
						animationDirection -= transitionSpeed * 2;
					}
					else if ((animationDirection > 0) && (animationDirection <= 10))
					{
						if (seatheCooldown == 0)
						{
							speedA = 3;
						}
						animationDirection -= transitionSpeed;
						if (animationSpeed >= 3)
							animationSpeed = 3;
						else if (animationSpeed < 3)
							animationSpeed += 0.4f;
					}
					else if (animationDirection <= 0)
					{
						if (animationSpeed >= 3)
							animationSpeed = 3;
						else if (animationSpeed < 3)
							animationSpeed += 0.4f;
					}
					
					if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
					{
						speedA = 5;
					}
					objectiveDirection = new Vector3(-speedA, objectiveDirection.y, 0);
					transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
				}
				else if (Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0))
				{
					if ((Input.GetKey(KeyCode.LeftShift)) && (sprintActive == true))
					{
						speedW = sprint;
						if ((animationSpeed >= 3) && (animationSpeed <= 4.5f))
							animationSpeed += 0.1f;
						else if (animationSpeed < 8)
							animationSpeed += 0.4f;
					}
					else
					{
						if ((animationSpeed >= 3) && (animationSpeed <= 4.5f))
							animationSpeed = 3;
						else if (animationSpeed < 3)
							animationSpeed += 0.2f;
						else if (animationSpeed > 7.5f)
							animationSpeed -= 0.2f;
					}
					
					if (animationDirection < 9.5f)
					{
						animationDirection += transitionSpeed;
					}
					else if (animationDirection > 10.5f)
					{
						animationDirection -= transitionSpeed;
					}
					else if ((animationDirection >= 9.5f) && (animationDirection <= 10.5f))
					{
						animationDirection = 10;
					}
					if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
					{
						speedW = 5;
					}
					objectiveDirection = new Vector3(0, objectiveDirection.y, speedW);
					transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
				}
				else if (Input.GetKey(KeyCode.S) || (state.ThumbSticks.Left.Y < 0))
				{
					if (animationSpeed <= -2)
						animationSpeed = -2;
					else if (animationSpeed > -2)
						animationSpeed -= 0.1f;
					
					if ((animationDirection >= 9.5f) && (animationDirection <= 10.5f))
					{
						animationDirection = 10;
					}
					else if (animationDirection < 9.5f)
					{
						animationDirection += transitionSpeed;
					}
					else if (animationDirection > 10.5f)
					{
						animationDirection -= transitionSpeed;
					}
					
					if ((Input.GetKeyDown(KeyCode.LeftControl)) && (dashTimer == 0))
					{
						speedS = 5;
						animator.SetBool("DashBack", true);
					}
					objectiveDirection = new Vector3(0, objectiveDirection.y, -speedS);
					transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
				}
				else
				{
					animator.SetBool("Run", false);
					
					if ((animationSpeed >= -0.2f) && (animationSpeed <= 0.2f))
						animationSpeed = 0;
					else if (animationSpeed < -0.2f)
						animationSpeed += 0.1f;
					else if (animationSpeed > 0.2f)
						animationSpeed -= 0.25f;
					objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
				}
			}
			objectiveDirection = transform.TransformDirection(objectiveDirection);
		}
	}
	
	private void Start()
	{
		staminaBarLenght = Screen.width / 3;
		/*
        this.transform.GetComponent<CharacterController>().height = 0.29f;
        this.transform.GetComponent<CharacterController>().center = new Vector3(0, 01, 0);
         */
		
		dashTimer = 0;
		dashCooldown = 1.5f;
		
		animator = GetComponent<Animator>();
		
		activeEventCombat = true;
	}
	
	private void Update()
	{
		if (Input.GetKey(KeyCode.X))
			rest = true;
		
		if (rest)
		{
			cdRest += Time.deltaTime;
			animator.SetBool("Rest", true);
			if (cdRest >= 3)
			{
				transform.GetComponent<PlayerHealth>().curHealth += 10 * Time.deltaTime;      
			}
		}
		
		if (animationSpeed != 0)
		{
			rest = false;
			cdRest = 0;
			animator.SetBool("Rest", false);
		}
		
		if (transform.GetComponent<PlayerHealth>().curHealth <= 0)
		{
			Blocked();
			animator.SetBool("Death", true);
		}
		
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
			
			// Calculates the direction
			HorizontalMovement(speedW, speedA, speedD, speedS, rootA, rootD);
			
			if (dashTimer > 0)
				dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
			{
				animator.SetBool("Dash", false);
				dashTimer = 0;
				animator.SetBool("DashBack", false);
			}
			
			//Stamina Control
			
			if (speedW == 4.5F)
			{
				curStam -= 2;
			}
			else curStam += 2;
			
			if (curStam >= maxStam) curStam = maxStam;
			if (curStam <= 0) curStam = 0;
			
			if (combat)
			{
				if ((seatheCooldown > 0) && (seatheCooldown < 5))
				{
					PlayerAttack.attackTimer = 3;
					seatheCooldown += 0.1f;
					Blocked();
				}
				else if (seatheCooldown >= 5)
				{
					PlayerAttack.attackTimer = 0;
					seatheCooldown = 0;
					Unblocked();
				}
				animator.SetBool("Combat", true);
			}
			else if (!combat)
			{
				if ((seatheCooldown > 0) && (seatheCooldown < 7))
				{
					seatheCooldown += 0.1f;
					Blocked();
				}
				else if (seatheCooldown >= 7)
				{
					seatheCooldown = 0;
					Unblocked();
				}
				
				animator.SetBool("Combat", false);
			}
			
			//sprint
			if (Input.GetKey(KeyCode.LeftShift))
			{
				if (curStam > 200)
				{
					sprintActive = true;
					
					if (((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0))) && ((Input.GetKey(KeyCode.A) || (state.ThumbSticks.Left.X < 0))))
					{
						//animator.SetBool("Sprint", false);
						speedW = 3;
					}
					else if (((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0))) && ((Input.GetKey(KeyCode.D) || (state.ThumbSticks.Left.X > 0))))
					{
						//animator.SetBool("Sprint", false);
						speedW = 3;
					}
					else if ((Input.GetKey(KeyCode.W) || (state.ThumbSticks.Left.Y > 0)))
					{
						speedW = sprint;
					}
				}
				else if (curStam <= 0)
				{
					sprintActive = false;
					
					speedW = 3;
					animator.SetBool("Sprint", false);
				}
			}
			else
			{
				animator.SetBool("Sprint", false);
				//speedW = 3;
			}
			/*
            if(Input.GetKey(KeyCode.S))
                {
                    speed = back;
                }
             else if (Input.GetKey(KeyCode.D))
                    {
                        if (speed > back)
                        {
                            speed--;
                        }
                        else if (speed <= back)
                        {
                            speed = back;
                        }
                    }
             */
			
			// Jump/Dash
			if (controller.isGrounded)
			{
				grounded = 0;
				
				//desenvaine/envaine
				if (seatheCooldown == 0)
				{
					if (activeEventCombat)
					{
						if (((Input.GetKeyDown(KeyCode.Q)) || (state.Buttons.X == ButtonState.Pressed)) && (PlayerMovement.grounded == 0))
						{
							combat = !combat;
							
							seatheCooldown += 0.1f;
						}
						else if ((prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed) && (PlayerMovement.grounded == 0))
						{
							combat = !combat;
							
							seatheCooldown += 0.1f;
						}
					}
				}
				
				animator.SetBool("Jump", false);
				animator.SetBool("Grounded", true);
				
				if (land < 6)
				{
					land = 0;
				}
				//				else if (land >= 6)
				//				{
				//					animator.SetBool ("Land", true);
				//					Blocked ();
				//					land += 0.1f;
				//					if(land >= 11)
				//					{
				//						Unblocked ();
				//						land = 0;
				//						animator.SetBool ("Land", false);
				//					}
				//				}
				
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speedW = 3;
				}
				//jump
				if (((Input.GetKeyDown(KeyCode.Space)) || (state.Buttons.A == ButtonState.Pressed)) && (jumpCooldown == 0))
				{
					animator.SetBool("Jump", true);
					
					curStam -= 20;
					
					objectiveDirection = new Vector3(objectiveDirection.x, jumpSpeed, objectiveDirection.z);
					//objectiveDirection.y = jumpSpeed;
				}
				
				//dash
				
				if (dashTimer == 0)
				{
					dash = 0;
				}
				
				if ((dashTimer == 0) && (curStam > 400))
				{
					if ((animationSpeed >= 3) && (animationDirection == 10))
					{
						if ((Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.B == ButtonState.Pressed)))
						{
							//objectiveDirection = new Vector3((objectiveDirection.x) * 20f, 0, (objectiveDirection.z) * 20f);
							dash = 1;
							dashTimer = dashCooldown;
							curStam -= 400;
						}
					}
					else if ((animationSpeed == 3) && (animationDirection <= 5))
					{
						if ((Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.B == ButtonState.Pressed)))
						{
							//objectiveDirection = new Vector3((objectiveDirection.x) * 20f, 0, (objectiveDirection.z) * 20f);
							dash = 2;
							dashTimer = dashCooldown;
							curStam -= 400;
						}
					}
					else if ((animationSpeed == 3) && (animationDirection >= 15))
					{
						if ((Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.B == ButtonState.Pressed)))
						{
							//objectiveDirection = new Vector3((objectiveDirection.x) * 20f, 0, (objectiveDirection.z) * 20f);
							dash = 3;
							dashTimer = dashCooldown;
							curStam -= 400;
						}
					}
					else if (((Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.B == ButtonState.Pressed))) && (Input.GetKey(KeyCode.S) || (state.ThumbSticks.Left.Y < 0)))
					{
						//objectiveDirection = new Vector3(0, (objectiveDirection.y) * 20, (objectiveDirection.z) * 20);
						dash = 4;
						dashTimer = dashCooldown;
						curStam -= 400;
					}
				}
				else if (dashTimer > 1)
				{
					animator.SetBool("Dash", true);
				}
				else
				{
					//objectiveDirection = new Vector3((objectiveDirection.x), 0, (objectiveDirection.z));
					animator.SetBool("Dash", false);
				}
			}
			else
			{
				//reinicia la gravedad cuando te dejas caer (sin jump)
				grounded += 0.1f;
				
				animator.SetBool("Grounded", false);
				
				if ((objectiveDirection.y <= 0) && (grounded <= 0.2f))
				{
					objectiveDirection.y = -0.5f;
				}
				
				//comprueba cuanto rato llevas en el aire (controlar el Land)
				if (land < 6)
				{
					land += 0.1f;
				}
				
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speedW = 3;
				}
				
				//fall += gravity;
				
				objectiveDirection += new Vector3(objectiveDirection.x, -gravity, objectiveDirection.z) * Time.deltaTime;
			}
			
			controller.Move(interpolateDirection * Time.deltaTime);
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