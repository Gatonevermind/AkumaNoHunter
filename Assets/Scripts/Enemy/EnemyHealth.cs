using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public int maxHealth = 100;
    public int curHealth = 100;
	float deadCounter = 200;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        AddjustCurrentHealth(0);

		if (curHealth <= 0) 
		{
			//Debug.Log (this.transform.name);
			deadCounter -= 2 * Time.deltaTime;

			if (deadCounter <= 0)
			{
				Destroy(this.gameObject);
			}
		}

	}

    public void AddjustCurrentHealth(int adj) {
        curHealth += adj;

        if(curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;
    }
}
