using UnityEngine;
using System.Collections;

public class ActivateKatana : MonoBehaviour 
{
    public VisKatana activeKatana;
    public VisKatanaHand activeKatanaHand;
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
        if (other.tag == "Forja")
        {
 
        }
    }
}
