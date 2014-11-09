using UnityEngine;
using System.Collections;
	public class PlayerAttack : MonoBehaviour {
	public Transform target;
	public float attackTimer;
	public float coolDown;
	public int timecounter = 0;
	
	// Use this for initialization
	void Start () {

		attackTimer = 0;
		coolDown = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject go = GameObject.FindGameObjectWithTag("Enemy");
		target = go.transform;

		if(target.renderer.material.color == Color.red)
		{
			timecounter += 1;
			if(timecounter > 10)
			{
				target.renderer.material.color = Color.white;
				timecounter = 0;
			}
		}


		if(attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if(attackTimer < 0)
			attackTimer = 0;
		
		if(Input.GetKeyUp(KeyCode.Mouse0)) {
			if(attackTimer == 0) {
				Attack();
				attackTimer = coolDown;
			}
		}
		
	}
	
	private void Attack() {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		if(distance < 3f) {
			if(direction > 0) {
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AddjustCurrentHealth(-20);
				target.renderer.material.color = Color.red;
			}
		}
	}
}