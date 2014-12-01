﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    public int maxHealth = 100;
    public int curHealth = 100;

    public string level;

    public float healthBarLenght;

    public bool invincible;
    public float active = 0;

	// Use this for initialization
	void Start () {
        healthBarLenght = Screen.width / 2;

        level = "Alpha";

        invincible = false;
	}
	
	// Update is called once per frame
	void Update () {
        AddjustCurrentHealth(0);

        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            if (invincible == true)
            {
                invincible = false;
                active = 0;
            }
            else if (invincible == false)
            {
                invincible = true;
                active = 1;
            }
        }
        
        if (invincible == false)
        {
            if (curHealth <= 0)
                Application.LoadLevel(level);
        }
        else if (invincible == true)
        {
            curHealth = 100;
        }



	}


    void OnGUI() {
        GUI.Box(new Rect(10, 10, healthBarLenght, 20), curHealth + "/" + maxHealth);
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
