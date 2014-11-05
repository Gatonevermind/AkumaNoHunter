using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 8.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 9.81F;
    
   
    private Vector3 objectiveDirection;
    private Vector3 interpolateDirection;
    float acceleration = 0.2F;
   
    
    
    void Update()
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


        // Jump
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
                objectiveDirection = new Vector3(objectiveDirection.x, jumpSpeed, objectiveDirection.z);
        }
        else
            objectiveDirection += new Vector3(0, -gravity * 1.5f, 0) * Time.deltaTime;
       

        controller.Move(interpolateDirection * Time.deltaTime);
    }



    private void HorizontalMovement(float speed, float root)
    {
        // Assign a direction depending on the input introduced
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

        //transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);

        //x  transform.eulerAngles = new Vector3(0, Mathf.Lerp(transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 0.2F), 0);

        objectiveDirection = transform.TransformDirection(objectiveDirection);
    }
}