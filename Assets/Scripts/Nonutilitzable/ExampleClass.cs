using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {
	public Transform target;
	void Update() {
		transform.LookAt(target);
	}
}