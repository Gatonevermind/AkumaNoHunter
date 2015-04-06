using UnityEngine;
using System.Collections;

public class IntroCinematic : MonoBehaviour 
{
    public Camera cam1;
    public Camera cam2;
    
    public float counterIntroCinematic;

    public GameObject Cinematic;

	void Start () 
    {
        cam1.enabled = true;
        cam2.enabled = false;

        counterIntroCinematic = 0;


    }

	void Update () 
    {
        counterIntroCinematic += Time.deltaTime;

        if (counterIntroCinematic <= 0.1f)
        {
            cam1.enabled = false;
            cam2.enabled = true;

        }

        if (counterIntroCinematic >= 15)
        {

        }

        if (counterIntroCinematic >= 20)
        {
            cam1.enabled = true;
            cam2.enabled = false;
            Cinematic.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
            counterIntroCinematic = 20;

    }
}
