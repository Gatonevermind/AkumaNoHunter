using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{ 
	public float speed = 3.0F;
	public float jumpSpeed = 5F;
	public float gravity = 9F;
	public float sprint = 4.5F;
	public float sprintLateral = 3.0F;
	public float lateral = 6.0F;
	public float back = 1.5F;
	public float dashTimer;
	public float coolDown;
	public float dash = 10.0F;
    public float active = 0;

	public float land = 0;

	public bool blocked;
	public bool combat;
	public float animationSpeed = 0;
	public float animationDirection = 10;
	public float transitionSpeed = 0.75f;

	public float seatheCooldown = 0;


    bool GodMode = false;

	private Animator animator;


	private Vector3 objectiveDirection;
	private Vector3 interpolateDirection;
	float acceleration = 0.2F;
	
	void Start()
	{
		dashTimer = 0;
		coolDown = 0.7f;
		
		animator = GetComponent<Animator> ();


	}

	void Update()
	{

        if (Input.GetKeyUp(KeyCode.Alpha9))
        {
            if (GodMode == true)
            {
                GodMode = false;
                active = 0;
            }
            else if (GodMode == false)
            {
                GodMode = true;
                active = 1;
            }
        }

        if (GodMode == false)
        {
            CharacterController controller = gameObject.GetComponent<CharacterController>();

            //moveDirection = transform.position - Camera.main.transform.position;
            //moveDirection.Normalize();
            //transform.rotation = moveDirection;

            // Calculates the module of the speed
            float root = Mathf.Sqrt(speed * speed / 2);


            // Acceleration and deceleration
            interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, acceleration),
                                               objectiveDirection.y,
                                               Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, acceleration));

            // Calculates the direction
            HorizontalMovement(speed, root);

            if (dashTimer > 0)
                dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                animator.SetBool("Dash", false);
                animator.SetBool("DashBack", false);
				animator.SetBool("DashRight", false);
				animator.SetBool("DashLeft", false);
                dashTimer = 0;
            }

            //desenvaine
            if (Input.GetKeyDown(KeyCode.Q))
            {
                combat = !combat;

				seatheCooldown += 0.1f;

            }

            if (combat)
            {
				if((seatheCooldown > 0) && (seatheCooldown < 5))
				{
					seatheCooldown += 0.1f;
					speed = 0;
					sprint = 0;
					jumpSpeed = 0;
				}
				else if (seatheCooldown >=5)
				{
					seatheCooldown = 0;
					speed = 3;
					sprint = 4.5f;
					jumpSpeed = 5;
				}
				animator.SetBool("Combat", true);
            }
            else if (!combat)
            {
				if((seatheCooldown > 0) && (seatheCooldown < 7))
				{
					seatheCooldown += 0.1f;
					speed = 0;
					sprint = 0;
					jumpSpeed = 0;
				}
				else if (seatheCooldown >= 7)
				{
					seatheCooldown = 0;
					speed = 3;
					sprint = 4.5f;
					jumpSpeed = 5;
				}

                animator.SetBool("Combat", false);
            }
            //sprint
            if (Input.GetKey(KeyCode.LeftShift))
            {

				if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A)))
				{
					
					animator.SetBool ("Sprint", false);
					speed = 3;
			
				}

				else if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D)))
				{
					
					animator.SetBool ("Sprint", false);
					speed = 3;
					
				}
                
				else if (Input.GetKey(KeyCode.W))
				{

					animator.SetBool ("Sprint", true);

					speed = sprint;
				}

            /*if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A)))
                if (speed < sprintLateral)
                {
                    speed++;
                }
                else if (speed >= sprintLateral)
                {
                    speed = sprintLateral;
                }


            if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D)))
                if (speed < sprintLateral)
                {
                    speed++;
                }
                else if (speed >= sprintLateral)
                {
                    speed = sprintLateral;
                }*/
				else
				{
					animator.SetBool ("Sprint", false);
				}

            }
            else 
            {
				animator.SetBool ("Sprint", false);
				//speed = 3;
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
                    }*/


            // Jump/Dash
            if (controller.isGrounded)
            {
				if (land >= 6)
				{
					animator.SetBool ("Land", true);
					speed = 0;
					sprint = 0;
					land += 0.1f;
					if(land >= 9)
					{
						speed = 3;
						sprint = 4.5f;
						land = 0;
						animator.SetBool ("Land", false);
					}
				}

                animator.SetBool("Jump", false);
				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speed = 3;
				}
                //jump
                if (Input.GetKeyDown(KeyCode.Space))
                {
					land = 0;
                    //animator.SetBool("Jump", true);

                    objectiveDirection = new Vector3(objectiveDirection.x, jumpSpeed, objectiveDirection.z);


                }

                //dash
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if ((Input.GetKey(KeyCode.C)) && (dashTimer == 0))
                    {
                        objectiveDirection = new Vector3((objectiveDirection.x) * 5, 0, (objectiveDirection.z) * 5);
                        dashTimer = coolDown;
                    }
                }
                else
                {
                    if (dashTimer == 0)
                    {

                        if ((Input.GetKey(KeyCode.C)) && (Input.GetKey(KeyCode.W)))
                        {
                            objectiveDirection = new Vector3((objectiveDirection.x) * 20f, 0, (objectiveDirection.z) * 20f);
                            dashTimer = coolDown;
                        }
                        else if ((Input.GetKey(KeyCode.C)) && (Input.GetKey(KeyCode.S)))
                        {
                            objectiveDirection = new Vector3((objectiveDirection.x) * 30, 0, (objectiveDirection.z) * 30);
                            dashTimer = coolDown;
                        }
						else if ((Input.GetKey(KeyCode.C)) && (Input.GetKey(KeyCode.A)))
						{
							objectiveDirection = new Vector3(0, (objectiveDirection.y) * 20, (objectiveDirection.z) * 20);
							dashTimer = coolDown;
						}
						else if ((Input.GetKey(KeyCode.C)) && (Input.GetKey(KeyCode.D)))
						{
							objectiveDirection = new Vector3(0, (objectiveDirection.y) * 20, (objectiveDirection.z) * 20);
							dashTimer = coolDown;
						}
                    }
                }

            }
            else
            {
				if(land < 6)
				{
					land += 0.1f;
				}

				if (Input.GetKeyUp(KeyCode.LeftShift))
				{
					speed = 3;
				}

				animator.SetBool ("Jump", true);
                objectiveDirection += new Vector3(objectiveDirection.x, -gravity * 1.5f, objectiveDirection.z) * Time.deltaTime;

            }



            controller.Move(interpolateDirection * Time.deltaTime);
        }

        if (GodMode == true)
        {
            CharacterController controller = gameObject.GetComponent<CharacterController>();

            // Calculates the module of the speed
            float root = Mathf.Sqrt(speed * speed / 2);


            // Acceleration and deceleration
            interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, acceleration),
                                                objectiveDirection.y,
                                                Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, acceleration));

            // Calculates the direction
            HorizontalMovement(speed, root);



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
	
	/*private void Blocked ()
	{
		speed = 0;
	}*/
	
	private void HorizontalMovement(float speed, float root)
	{
	

		animator.SetFloat ("Direction", animationDirection);
		// Assign a direction depending on the input introduced
		//NORMAL MOVEMENT
		if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A)))
		{
			animator.SetBool ("Run", true);

			if (animationDirection < 4.5f)
			{
				animationDirection += transitionSpeed;
			}
			else if(animationDirection > 5.5f)
			{
				animationDirection -= transitionSpeed;
			}

			objectiveDirection = new Vector3(-root, objectiveDirection.y, root);
			transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		}
		else if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D)))
		{
			animator.SetBool ("Run", true);

			if (animationDirection < 14.5f)
			{
				animationDirection += transitionSpeed;
			}
			else if(animationDirection > 15.5f)
			{
				animationDirection -= transitionSpeed;
			}

			objectiveDirection = new Vector3(root, objectiveDirection.y, root);
			transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		}
		/*else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.A)))
        {
            objectiveDirection = new Vector3(-root, objectiveDirection.y, -root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.D)))
        {
            objectiveDirection = new Vector3(root, objectiveDirection.y, -root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }*/
		else
		{
			if (Input.GetKey(KeyCode.D))
			{


				if(animationDirection <10)
				{
					speed = 0;
					animationDirection += transitionSpeed*2;


				}
				else if ((animationDirection <20) && (animationDirection >= 10))
				{
					speed = 3;
					animationDirection += transitionSpeed;
					animator.SetBool ("Run", true);
				}


				objectiveDirection = new Vector3(speed, objectiveDirection.y, 0);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if (Input.GetKey(KeyCode.A))
			{ 
				animator.SetBool ("Run", true);

				if(animationDirection >10)
				{
					speed = 0;
					animationDirection -= transitionSpeed*2;
				}
				else if((animationDirection >0) && (animationDirection <= 10))
				{
					speed = 3;
					animationDirection -= transitionSpeed;
				}


				objectiveDirection = new Vector3(-speed, objectiveDirection.y, 0);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if (Input.GetKey(KeyCode.W))
			{
				animator.SetBool ("Run", true);

				if (animationDirection < 9.5f)
				{
					animationDirection += transitionSpeed;
				}
				if(animationDirection > 10.5f)
				{
					animationDirection -= transitionSpeed;
				}

				if((animationDirection>=9.5f) && (animationDirection <=10.5f))
				{
					animationDirection = 10;
				}
			
				objectiveDirection = new Vector3(0, objectiveDirection.y, speed);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if (Input.GetKey(KeyCode.S))
			{

				objectiveDirection = new Vector3(0, objectiveDirection.y, -back);
				transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
			}
			else
			{
				animator.SetBool ("Run", false);
				/*
				if(animationDirection < 9.5f)
				{
					animationDirection += transitionSpeed;
				}
				else if(animationDirection > 10.5f)
				{
					animationDirection -= transitionSpeed;
				}
				 */
				objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
			}
			
		}

	
		//transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
		
		//x  transform.eulerAngles = new Vector3(0, Mathf.Lerp(transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 0.2F), 0);
		
		objectiveDirection = transform.TransformDirection(objectiveDirection);
	}
}