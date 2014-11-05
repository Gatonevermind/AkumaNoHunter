using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public Transform target;
    public int moveSpeed = 2;
    public int rotationSpeed = 1;
    public float gravity = 9.81F;

    private Transform myTransform;

    void Awake() {
        myTransform = transform;
    }

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(target.position, myTransform.position, Color.yellow);

        //Look at target
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        //Move towards target
        myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
    }
}
