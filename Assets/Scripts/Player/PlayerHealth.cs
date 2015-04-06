using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    public int maxHealth = 100;
    public float curHealth = 100;

    public bool invincible;
    public float activeHealth = 0;

	void Start () 
    {

        invincible = false;
	}
	
	void Update () 
    {
        AddjustCurrentHealth(0);



        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            if (invincible == true)
            {
                invincible = false;
                activeHealth = 0;
            }
            else if (invincible == false)
            {
                invincible = true;
                activeHealth = 1;
            }
        }
        
        if (invincible == false)
        {

        }
        else if (invincible == true)
        {
            curHealth = 100;
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
