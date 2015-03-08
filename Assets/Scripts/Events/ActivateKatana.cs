using UnityEngine;
using System.Collections;

public class ActivateKatana : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			other.transform.FindChild("Katana").GetComponent<VisKatana>().activeKatana = true;
			other.transform.FindChild("KatanaHand").GetComponent<VisKatanaHand>().activeKatanaHand = true;
			other.transform.FindChild("Vaina").GetComponent<VisVaina>().activeVaina = true;
            
            //other.transform.Find("samurai").GetComponent<PlayerAttack>().activate = true;
            //other.transform.Find("samurai").GetComponent<PlayerMovement>().activeEventCombat = true;
        }
    }
}
