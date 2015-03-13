using UnityEngine;
using System.Collections;

public class Tutorial2 : MonoBehaviour 
{
    public bool activeT2;
    GameObject[] tutorial2Control;
    void Start() 
    {
        activeT2 = false;

        tutorial2Control = GameObject.FindGameObjectsWithTag("Tutorial2");
        foreach (GameObject tutorial2 in tutorial2Control)
            tutorial2.SetActive(false);
    }
    void Update()
    {
        if (activeT2)
        {
            foreach (GameObject tutorial2 in tutorial2Control)
                tutorial2.SetActive(true);
        }
        else
            foreach (GameObject tutorial2 in tutorial2Control)
                tutorial2.SetActive(false);
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT2 = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activeT2 = false;
        }
    }
}
