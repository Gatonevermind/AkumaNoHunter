using UnityEngine;
using System.Collections;

public class VisPlayer : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{
        GameObject controller = GameObject.Find("Cabeza");
        
	}
	
	// Update is called once per frame
    void Update()
    {
        GameObject counter = GameObject.Find("Cinematica_Despertar");
        IntroCinematic introCinematic = counter.GetComponent<IntroCinematic>();

        if (introCinematic.counterIntroCinematic >= 15)
        {
            GameObject controller = GameObject.Find("Cabeza");
            foreach (Transform controlPlayer in controller.transform)
                         controlPlayer.gameObject.SetActive (false);

            Destroy(GetComponent<VisPlayer>());
        }
    }
}