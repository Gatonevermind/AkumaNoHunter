using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CustomCamera : MonoBehaviour
{
    public Transform Target;
    float targetHeight = 0.8f;
    float distance = 1.5f;
    float maxDistance = 3.5f;
    float minDistance = 1.0f;
    float xSpeed = 250.0f;
    float ySpeed = 120.0f;
    float yMinLimit = -40f;
    float yMaxLimit = 80f;
    float zoomRate = 20f;
    //float rotationDampening = 3.0f;
    //LayerMask collisionMask = -1;
    private float x = 0.0f;
    private float y = 0.0f;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    int ignoreMask;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;


        if (rigidbody)
            rigidbody.freezeRotation = true;

        ignoreMask = 1 << 8;
    }

    void LateUpdate()
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
        
        
        if (!Target)
            return;

        if (!GameObject.Find("GameControl").GetComponent<PauseMenu>().pauseMenu)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            x += state.ThumbSticks.Right.X * xSpeed * 0.02f;
            y -= state.ThumbSticks.Right.Y * ySpeed * 0.02f;
        }

        distance -= (Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        // ROTATE CAMERA: 
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

        // POSITION CAMERA: 
        Vector3 position = Target.position - (rotation * Vector3.forward * distance + new Vector3(0, -targetHeight, 0));
        transform.position = position;

        // IS VIEW BLOCKED? 
        RaycastHit hit;
        Vector3 trueTargetPosition = Target.transform.position - new Vector3(0.15f, -targetHeight, 0);
        // Cast the line to check: 
        if (Physics.Linecast(trueTargetPosition, transform.position, out hit, ignoreMask))
        {
            // If so, shorten distance so camera is in front of object: 
            float tempDistance = Vector3.Distance(trueTargetPosition, hit.point) - 0.28f;
            // Finally, rePOSITION the CAMERA: 
            position = Target.position - (rotation * Vector3.forward * tempDistance + new Vector3(0, -targetHeight, 0));
            transform.position = position;
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);

    }
}

