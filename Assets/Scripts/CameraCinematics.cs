using UnityEngine;
using System.Collections;

public class CameraCinematics : MonoBehaviour 
{
    public Transform target;

	void Update () 
    {
        transform.LookAt(target);
	}
}
