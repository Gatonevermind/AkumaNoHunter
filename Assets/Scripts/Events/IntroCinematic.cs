using UnityEngine;
using System.Collections;

public class IntroCinematic : MonoBehaviour 
{
    public Camera cam1;
    public Camera cam2;

	public static bool intro;
    
    public float counterIntroCinematic;

	public GameObject bip;
	public GameObject cabeza;
	public GameObject cuerpo;

	void Start () 
    {
        cam1.enabled = true;
        cam2.enabled = false;

        counterIntroCinematic = 0;

		intro = false;


    }

	void Update () 
    {
		if(counterIntroCinematic < 22)
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
            bip.SetActive(false);
			cuerpo.SetActive(false);
			cabeza.SetActive(false);

			intro = true;
        }
		if(counterIntroCinematic <20)
		{
	        if (Input.GetKeyDown(KeyCode.F))
	            counterIntroCinematic = 20;
		}
    }
}
