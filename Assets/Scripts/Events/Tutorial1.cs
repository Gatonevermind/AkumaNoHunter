using UnityEngine;
using System.Collections;

public class Tutorial1 : MonoBehaviour 
{
    public bool activeT1;
    GameObject[] tutorial1Control;
    void Start() 
    {
        activeT1 = false;

        tutorial1Control = GameObject.FindGameObjectsWithTag("Tutorial1");
        foreach (GameObject tutorial1 in tutorial1Control)
            tutorial1.SetActive(false);
    }
    void Update()
    {
        if (activeT1)
        {
            foreach (GameObject tutorial1 in tutorial1Control)
                tutorial1.SetActive(true);
        }
        else
            foreach (GameObject tutorial1 in tutorial1Control)
                tutorial1.SetActive(false);
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT1 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT1 = false;
        }
    }
}
