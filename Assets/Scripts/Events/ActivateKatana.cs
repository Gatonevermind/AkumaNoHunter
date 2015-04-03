using UnityEngine;
using System.Collections;

public class ActivateKatana : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.FindChild("KatanaDefault").GetComponent<VisKatana>().activeKatana = true;
            other.transform.FindChild("KatanaDefaultHand").GetComponent<VisKatanaHand>().activeKatanaHand = true;
            other.transform.FindChild("KatanaLobo").GetComponent<VisKatana>().activeKatana = true;
            other.transform.FindChild("KatanaLoboHand").GetComponent<VisKatanaHand>().activeKatanaHand = true;

            other.transform.FindChild("VainaDefault").GetComponent<VisVaina>().activeVaina = true;
            other.transform.FindChild("VainaLobo").GetComponent<VisVaina>().activeVaina = true;

            //other.transform.Find("samurai").GetComponent<PlayerMovement>().activeEventCombat = true;
        }
    }

    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
}
