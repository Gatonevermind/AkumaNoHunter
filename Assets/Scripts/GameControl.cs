using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GameObject player = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        player.name = "Player";
        player.tag = "Player";

        player.transform.position = new Vector3(0, 2, 0);

        player.AddComponent<CharacterController>();
        player.AddComponent("PlayerMovement");
        player.AddComponent("PlayerHealth");
		player.AddComponent ("PlayerAttack");

        Camera.main.gameObject.GetComponent<CustomCamera> ().Target = player.transform;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
