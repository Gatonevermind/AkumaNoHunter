//using UnityEngine;
//using System.Collections;
//
//public class DeathAldeanoEvent : MonoBehaviour 
//{
//    public GameObject aldeano;
//
//    public bool invincible;
//
//    void Start () 
//    {
//        invincible = true;
//    }
//
//    void Update()
//    {         
//        if (invincible)
//        {
//            PlayerHealth ph = (PlayerHealth)aldeano.GetComponent("PlayerHealth");
//
//            ph.curHealth += 10;
//        }
//    }
//
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "Player")
//        {
//            invincible = false;
//            PlayerHealth ph = (PlayerHealth)aldeano.GetComponent("PlayerHealth");
//            ph.curHealth = 10;
//        }
//    }
//}
