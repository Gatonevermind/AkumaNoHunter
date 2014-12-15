using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public int maxHealth = 100;
    public int curHealth = 100;

    public float healthBarLenght;


	// Use this for initialization
	void Start () {
        healthBarLenght = Screen.width / 2;
	}
	
	// Update is called once per frame
	void Update () {
        AddjustCurrentHealth(0);

		if (curHealth <= 0) 
		{
			Destroy(GameObject.FindGameObjectWithTag("Enemy"));
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

        healthBarLenght = (Screen.width / 2) * (curHealth / (float)maxHealth);
    }
}
