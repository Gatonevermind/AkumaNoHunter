using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

    public GameObject target;

    public float attackTimer;
    public float coolDown;
    public int timecounter = 0;

	void Start () 
    {
        attackTimer = 0;
        coolDown = 3.0f;
	}
	
	void Update () 
    {
        gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        if (attackTimer < 0)
            attackTimer = 0;

        if (attackTimer == 0)
        {
            Attack();
            attackTimer = coolDown;
        }
	}

    private void Attack()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        Vector3 dir = (target.transform.position - transform.position).normalized;

        float direction = Vector3.Dot(dir, transform.forward);

        if (distance < 2.5f)
        {
            if (direction > 0)
            {
                PlayerHealth eh = (PlayerHealth)target.GetComponent("PlayerHealth");
                eh.AddjustCurrentHealth(-10);
                target.renderer.material.color = Color.red;
            }
        }
    }
}
