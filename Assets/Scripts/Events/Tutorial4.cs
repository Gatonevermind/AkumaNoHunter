using UnityEngine;
using System.Collections;

public class Tutorial4 : MonoBehaviour 
{
    public bool activeT4;
    GameObject[] tutorial4Control;
    void Start() 
    {
        activeT4 = false;

        tutorial4Control = GameObject.FindGameObjectsWithTag("Tutorial4");
        foreach (GameObject tutorial4 in tutorial4Control)
            tutorial4.SetActive(false);
    }
    void Update()
    {
        if (activeT4)
        {
            foreach (GameObject tutorial4 in tutorial4Control)
                tutorial4.SetActive(true);
        }
        else
            foreach (GameObject tutorial4 in tutorial4Control)
                tutorial4.SetActive(false);
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT4 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT4 = false;
        }
    }
}
