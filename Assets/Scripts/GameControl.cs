using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public string levelname;
    public string resetlevel;

	// Use this for initialization
	void Start () 
    {
        
		GameObject player = GameObject.FindGameObjectWithTag ("Player");

        player.name = "Player";
        player.tag = "Player";

        player.transform.position = new Vector3(15, 2, 55);

        player.AddComponent("PlayerMovement");
        player.AddComponent("PlayerHealth");
		player.AddComponent ("PlayerAttack");
        

        Camera.main.gameObject.GetComponent<CustomCamera> ().Target = player.transform;
        
	}
	
	// Update is called once per frame
	void Update () 
    {

        if (Input.GetKey(KeyCode.F6))
        {
            Application.LoadLevel(levelname);
        }

        if (Input.GetKey(KeyCode.F1))
        {
            Application.LoadLevel(resetlevel);
        }

	}
}
