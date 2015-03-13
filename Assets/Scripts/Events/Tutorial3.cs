using UnityEngine;
using System.Collections;

public class Tutorial3 : MonoBehaviour 
{
    public bool activeT3;
    GameObject[] tutorial3Control;
    void Start() 
    {
        activeT3 = false;

        tutorial3Control = GameObject.FindGameObjectsWithTag("Tutorial3");
        foreach (GameObject tutorial3 in tutorial3Control)
            tutorial3.SetActive(false);
    }
    void Update()
    {
        if (activeT3)
        {
            foreach (GameObject tutorial3 in tutorial3Control)
                tutorial3.SetActive(true);
        }
        else
            foreach (GameObject tutorial3 in tutorial3Control)
                tutorial3.SetActive(false);
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT3 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT3 = false;
        }
    }
}
