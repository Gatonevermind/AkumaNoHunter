﻿using UnityEngine;
using System.Collections;

public class LoadingControl : MonoBehaviour 
{
    public Transform rhino;
    public Transform fruit_1;
    public Transform fruit_2;
    public Transform fruit_3;
    public Transform fruit_4;
    public Transform fruit_5;

    public GameObject fruit1;
    public GameObject fruit2;
    public GameObject fruit3;
    public GameObject fruit4;
    public GameObject fruit5;

    public int fruitEated;

    public AnimationClip run;
    public AnimationClip eat;
    public AnimationClip stay;

    public float counterEat;
    public float counterMove;

    //Vector3 initialPosition;

    public string levelName;

	// Use this for initialization
	void Start () 
    {
        fruitEated = 0;
        counterEat = 0;
        //initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (fruitEated == 0)
        {
            counterMove += Time.deltaTime;
            transform.position = Lerp(transform.position, fruit_1.position, 0.05f);
            //animation.Play(run.name);

            if (counterMove >= 1)
            {
                counterEat += Time.deltaTime;

                if (counterEat >= 1)
                {
                    GameObject.Destroy(fruit1);
                    fruitEated += 1;
                    counterEat = 0;
                    counterMove = 0;
                }
            }
        }
        else if (fruitEated == 1)
        {
            counterMove += Time.deltaTime;
            transform.position = Lerp(transform.position, fruit_2.position, 0.05f);

            if (counterMove >= 1)
            {
                counterEat += Time.deltaTime;


                if (counterEat >= 1)
                {
                    GameObject.Destroy(fruit2);
                    fruitEated += 1;
                    counterEat = 0;
                    counterMove = 0;
                }
            }
        }
        else if (fruitEated == 2)
        {
            counterMove += Time.deltaTime;
            transform.position = Lerp(transform.position, fruit_3.position, 0.05f);

            if (counterMove >= 1)
            {
                counterEat += Time.deltaTime;

                if (counterEat >= 1)
                {
                    GameObject.Destroy(fruit3);
                    fruitEated += 1;
                    counterEat = 0;
                    counterMove = 0;
                }
            }
        }
        else if (fruitEated == 3)
        {
            counterMove += Time.deltaTime;
            transform.position = Lerp(transform.position, fruit_4.position, 0.05f);

            if (counterMove >= 1)
            {
                counterEat += Time.deltaTime;

                if (counterEat >= 1)
                {
                    GameObject.Destroy(fruit4);
                    fruitEated += 1;
                    counterEat = 0;
                    counterMove = 0;
                    //transform.position = initialPosition;
                }
            }
        }
        else if (fruitEated == 4)
        {
            counterMove += Time.deltaTime;
            transform.position = Lerp(transform.position, fruit_5.position, 0.05f);

            if (counterMove >= 1)
            {
                Application.LoadLevel(levelName);
            }
        }
	}

    public static Vector3 Lerp(Vector3 start, Vector3 finish, float percentage)
    {
        //Make sure percentage is in the range [0.0, 1.0]
        percentage = Mathf.Clamp01(percentage);
 
        //(finish-start) is the Vector3 drawn between 'start' and 'finish'
        Vector3 startToFinish = finish - start;
 
        //Multiply it by percentage and set its origin to 'start'
        return start + startToFinish * percentage;
    }
}
