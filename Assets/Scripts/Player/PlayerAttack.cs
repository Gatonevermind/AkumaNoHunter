using UnityEngine;
using System.Collections;
	public class PlayerAttack : MonoBehaviour {
	public Transform target;
	public float attackTimer;
	public float coolDown;
	public int timecounter = 0;

	private Animator animator;

    bool combatActivate = false;

	
	// Use this for initialization
	void Start () 
    {
		attackTimer = 0;
		coolDown = 0.7f;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            combatActivate = !combatActivate;
        }

        if (combatActivate)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Enemy");
            target = go.transform;

            if (target.renderer.material.color == Color.red)
            {
                timecounter += 1;
                if (timecounter > 10)
                {
                    target.renderer.material.color = Color.white;
                    timecounter = 0;
                }
            }


            if (attackTimer > 0)
            {
                animator.SetBool("Attack", false);
                attackTimer -= Time.deltaTime;
            }
            if (attackTimer < 0)
                attackTimer = 0;


            if (attackTimer == 0)
            {

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    animator.SetBool("Attack", true);
                    Attack();
                    attackTimer = coolDown;
                }
            }
        }
	}
	
	private void Attack() 
    {
		float distance = Vector3.Distance(target.transform.position, transform.position);
		
		Vector3 dir = (target.transform.position - transform.position).normalized;
		
		float direction = Vector3.Dot(dir, transform.forward);
		
		if(distance < 2f) {
			if(direction > 0) {
				EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
				eh.AddjustCurrentHealth(-20);
				target.renderer.material.color = Color.red;

			}
		}
	}
}