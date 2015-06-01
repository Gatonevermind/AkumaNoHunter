/*using UnityEngine;
using System.Collections;

public class BossUIBar : MonoBehaviour 
{
    GameObject[] barControl;

	void Start () 
    {
        barControl = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject bar in barControl)
            bar.SetActive(false);
	}
	

	void Update () 
    {
        if (BossEvent.countToCinematic >= 39)
        {
            foreach (GameObject bar in barControl)
                bar.SetActive(true);

            Debug.Log("HEY!");
        }
	}
}*/
