using UnityEngine;
using System.Collections;

public class AldeanoHealth : MonoBehaviour 
{
    public int maxHealth = 100;
    public float curHealth = 100;

	void Start () 
    {

	}
	
	void Update ()
    {
        AddjustCurrentHealth(0);
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
