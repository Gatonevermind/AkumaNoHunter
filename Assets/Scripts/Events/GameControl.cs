using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour 
{
    public Transform playerPosition;
    public string levelname;
    public string resetlevel;

	// Use this for initialization
	void Start () 
    {
        Application.targetFrameRate = 60;
        
        GameObject player = GameObject.FindGameObjectWithTag ("Player");

        player.name = "Player";
        player.tag = "Player";

        //player.transform.position = new Vector3(27.03f, 120.5f, 73.2f);
        //player.transform.Rotate(0, 62.3f, 0);

        player.transform.position = playerPosition.transform.position;
        player.transform.rotation = playerPosition.transform.rotation;

        player.AddComponent<PlayerMovement>();
        player.AddComponent<PlayerHealth>();
		player.AddComponent <PlayerAttack>();

        Camera.main.gameObject.GetComponent<CustomCamera> ().Target = player.transform;   
	}
	
	void Update () 
    {
        GameObject counter = GameObject.Find("Cinematica_Despertar");
        IntroCinematic introCinematic = counter.GetComponent<IntroCinematic>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (introCinematic.counterIntroCinematic >= 15)
        {
            player.transform.position = new Vector3(30.70f, 120.5f, 73.45f);
        }

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
