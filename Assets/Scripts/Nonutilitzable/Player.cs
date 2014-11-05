using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float actualSpeed = 10.0F;
    public float speed = 4;
    public float jumpSpeed = 8.0F;
    public float turningSpeed = 50;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal") * actualSpeed * Time.deltaTime;
            transform.Translate(horizontal, 0, 0);

            float vertical = Input.GetAxis("Vertical") * actualSpeed * Time.deltaTime;
            transform.Translate(0, 0, vertical);


            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}