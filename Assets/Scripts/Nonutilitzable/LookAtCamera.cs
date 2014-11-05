using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {
	public GameObject target;
	
	void LateUpdate() {
		transform.LookAt(target.transform);
	}
}