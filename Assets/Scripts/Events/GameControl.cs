using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GameControl : MonoBehaviour 
{
    public Transform playerPosition;
	public Transform despertarSpawn;
	public Transform playerBossSpawn;
    public string levelname;
    public string resetlevel;
   
    GameObject[] uiPlayerControl;
    GameObject[] interfaceControl;

    PlayerIndex playerIndex;
    GamePadState prevState;
    GamePadState state;

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
//		player.AddComponent <PlayerAttack>();

        Camera.main.gameObject.GetComponent<CustomCamera> ().Target = player.transform;

        uiPlayerControl = GameObject.FindGameObjectsWithTag("UIPlayer");
        foreach (GameObject uiPlayer in uiPlayerControl)
            uiPlayer.SetActive(false);

        interfaceControl = GameObject.FindGameObjectsWithTag("Interface");
        foreach (GameObject inter in interfaceControl)
            inter.SetActive(false);
	}
	
	void Update () 
    {
		GameObject counter = GameObject.Find("Cinematica_Despertar");
        IntroCinematic introCinematic = counter.GetComponent<IntroCinematic>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if ((introCinematic.counterIntroCinematic >= 20) && (introCinematic.counterIntroCinematic < 20.2f))
        {
            player.transform.position = despertarSpawn.position;
			player.transform.rotation = despertarSpawn.rotation;

            foreach (GameObject uiPlayer in uiPlayerControl)
                uiPlayer.SetActive(true);
        }

        if ((BossEvent.countToCinematic > 2) && (BossEvent.countToCinematic < 3))
        {
            foreach (GameObject uiPlayer in uiPlayerControl)
                uiPlayer.SetActive(false);
        }
        else if (BossEvent.countToCinematic >= 39)
        {
            foreach (GameObject uiPlayer in uiPlayerControl)
                uiPlayer.SetActive(true);
        }

        if (Input.GetKey(KeyCode.LeftAlt) || (state.Buttons.Back == ButtonState.Pressed))
        {
            foreach (GameObject inter in interfaceControl)
                inter.SetActive(true);
        }
        else
        {
            foreach (GameObject inter in interfaceControl)
                inter.SetActive(false);
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
