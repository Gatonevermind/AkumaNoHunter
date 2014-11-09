using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 9.81F;
	public float sprint = 14.0F;
	public float dashTimer;
	public float coolDown;
	public float dash = 25.0F;


    
   
    private Vector3 objectiveDirection;
    private Vector3 interpolateDirection;
    float acceleration = 0.2F;
   
    void Start()
	{
		dashTimer = 0;
		coolDown = 0.7f;
	}
    
    void Update()
    {
        
        CharacterController controller = gameObject.GetComponent<CharacterController>();
        
        //moveDirection = transform.position - Camera.main.transform.position;
        //moveDirection.Normalize();
        //transform.rotation = moveDirection;

        // Calculates the module of the speed
        float root = Mathf.Sqrt(speed * speed / 2);

		// Calculates the module of the sprint
		float rootSprint = Mathf.Sqrt(sprint * sprint / 2);

        // Acceleration and deceleration
        interpolateDirection = new Vector3(Mathf.Lerp(interpolateDirection.x, objectiveDirection.x, acceleration),
                                            objectiveDirection.y,
                                            Mathf.Lerp(interpolateDirection.z, objectiveDirection.z, acceleration));

        // Calculates the direction
        HorizontalMovement(speed, sprint, root, rootSprint);

		if (dashTimer > 0)
						dashTimer -= Time.deltaTime;
		if (dashTimer < 0)
						dashTimer = 0;
		/*if ((Input.GetKey (KeyCode.C)) && (dashTimer == 0))
		{
			objectiveDirection = new Vector3((objectiveDirection.x)*10, 0, (objectiveDirection.z)*10);
			dashTimer = coolDown;
		}*/
        // Jump/Dash
        if (controller.isGrounded)
        {
			//jump
			if (Input.GetKey(KeyCode.Space))
			{
				objectiveDirection = new Vector3(objectiveDirection.x, jumpSpeed, objectiveDirection.z);
			}

			//dash
			if(Input.GetKey(KeyCode.LeftShift))
			{
				if ((Input.GetKey (KeyCode.C)) && (dashTimer == 0))
				{
					objectiveDirection = new Vector3((objectiveDirection.x)*10.6f, 0, (objectiveDirection.z)*10.6f);
					dashTimer = coolDown;
				}
			}
			else
			{
				if ((Input.GetKey (KeyCode.C)) && (dashTimer == 0))
				{
					objectiveDirection = new Vector3((objectiveDirection.x)*20, 0, (objectiveDirection.z)*20);
					dashTimer = coolDown;
				}
			}

        }
        else
		{	
			objectiveDirection += new Vector3(objectiveDirection.x, -gravity * 1.5f,objectiveDirection.z) * Time.deltaTime;

		}
            
       

        controller.Move(interpolateDirection * Time.deltaTime);
    }



    private void HorizontalMovement(float speed, float sprint, float root, float rootSprint)
    {
        // Assign a direction depending on the input introduced
		//NORMAL MOVEMENT
		if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.A)))
        {
            objectiveDirection = new Vector3(-root, objectiveDirection.y, root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else if ((Input.GetKey(KeyCode.W)) && (Input.GetKey(KeyCode.D)))
        {
            objectiveDirection = new Vector3(root, objectiveDirection.y, root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.A)))
        {
            objectiveDirection = new Vector3(-root, objectiveDirection.y, -root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else if ((Input.GetKey(KeyCode.S)) && (Input.GetKey(KeyCode.D)))
        {
            objectiveDirection = new Vector3(root, objectiveDirection.y, -root);
            transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                objectiveDirection = new Vector3(speed, objectiveDirection.y, 0);
                transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                objectiveDirection = new Vector3(-speed, objectiveDirection.y, 0);
                transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                objectiveDirection = new Vector3(0, objectiveDirection.y, speed);
                transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                objectiveDirection = new Vector3(0, objectiveDirection.y, -speed);
                transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else
                objectiveDirection = new Vector3(0, objectiveDirection.y, 0);
        	}

		//SPRINT
		if (Input.GetKey (KeyCode.LeftShift))
		{
			if ((Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.A)))
			{
				objectiveDirection = new Vector3 (-rootSprint, objectiveDirection.y, rootSprint);
				transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if ((Input.GetKey (KeyCode.W)) && (Input.GetKey (KeyCode.D)))
			{
				objectiveDirection = new Vector3 (rootSprint, objectiveDirection.y, rootSprint);
				transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.A)))
			{
				objectiveDirection = new Vector3 (-rootSprint, objectiveDirection.y, -rootSprint);
				transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
			}
			else if ((Input.GetKey (KeyCode.S)) && (Input.GetKey (KeyCode.D)))
			{
				objectiveDirection = new Vector3 (rootSprint, objectiveDirection.y, -rootSprint);
				transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
			}
			else
			{
				if (Input.GetKey (KeyCode.W)) 
				{
					objectiveDirection = new Vector3 (0, objectiveDirection.y, sprint);
					transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
				}
				else if (Input.GetKey (KeyCode.S)) 
				{
					objectiveDirection = new Vector3 (0, objectiveDirection.y, -sprint);
					transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
				}
				else if (Input.GetKey (KeyCode.A)) 
				{
					objectiveDirection = new Vector3 (-sprint, objectiveDirection.y, 0);
					transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
				}
				else if (Input.GetKey (KeyCode.D)) 
				{
					objectiveDirection = new Vector3 (sprint, objectiveDirection.y, 0);
					transform.eulerAngles = new Vector3 (0, Camera.main.transform.eulerAngles.y, 0);
				}
			}
			
		}



        //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

        //x  transform.eulerAngles = new Vector3(0, Mathf.Lerp(transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 0.2F), 0);

        objectiveDirection = transform.TransformDirection(objectiveDirection);
    }
}