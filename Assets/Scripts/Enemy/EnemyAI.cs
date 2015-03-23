using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    public Transform target;
    public int moveSpeed = 2;
    public int rotationSpeed = 1;
    public float gravity = 9.81F;
	public float attackTimer;
	public float coolDown;
	public int timecounter = 0;




    private Transform myTransform;

    void Awake() {
        myTransform = transform;
    }

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;

		attackTimer = 0;
		coolDown = 3.0f;



	}

	// Update is called once per frame
	void Update () {

        Debug.DrawLine(target.position, myTransform.position, Color.yellow);

        //Look at target
        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

		//Move towards target
		myTransform.position += new Vector3 (myTransform.forward.x, myTransform.forward.y * 0, myTransform.forward.z)/*myTransform.forward*/ * moveSpeed * Time.deltaTime;

		//myTransform.forward += new Vector3 (myTransform.forward.x, myTransform.forward.y * 0, myTransform.forward.z);
		/*if (target.position.y >= 2)
		{
			isGrounded = false;
			myTransform.forward += new Vector3(0, -gravity, 0)* Time.deltaTime;
		}*/

		//Attack Logic
		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if(attackTimer < 0)
			attackTimer = 0;
		
		if(attackTimer == 0) 
        {
			Attack();
			attackTimer = coolDown;
		}

		//color para que sea mas visual
		if(target.GetComponent<Renderer>().material.color == Color.red)
		{
			timecounter += 1;
			if(timecounter > 10)
			{
				target.GetComponent<Renderer>().material.color = Color.white;
				timecounter = 0;
			}
		}
		
		
	}

	private void Attack() 
    {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		if(distance < 2.5f) {
			if(direction > 0) {
				PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
				eh.AddjustCurrentHealth(-10);
				target.GetComponent<Renderer>().material.color = Color.red;
			}
		}
	}
	
	
}