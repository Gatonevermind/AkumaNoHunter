using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GamepadController : MonoBehaviour 
{
    PlayerIndex playerIndex;
    bool playerIndexSet = false;
    GamePadState prevState;
    GamePadState state;

    public bool connected = false;
    
    // Main Menu Scene
    public GameObject desconnection1;
    //-------------------------------

    // Beta Scene
    public GameObject desconnection2;
    public GameObject desconnection3;
    public GameObject desconnection4;
    public GameObject desconnection5;
    //-------------------------------

	void Update () 
    {
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (state.IsConnected) connected = true;
        else connected = false;
        if (Application.loadedLevelName == "Menu")
        {
            if (connected)
            {
                desconnection1.GetComponent<ControllerUI>().enabled = true;
            }
            else
            {
                desconnection1.GetComponent<ControllerUI>().enabled = false;
            }
        }

        if (Application.loadedLevelName == "Beta")
        {
            if (connected)
            {
                desconnection2.GetComponent<ControllerUI>().enabled = true;
                desconnection3.GetComponent<ControllerUI>().enabled = true;
                desconnection4.GetComponent<ControllerUI>().enabled = true;
                desconnection5.GetComponent<ControllerUI>().enabled = true;
            }
            else
            {
                desconnection2.GetComponent<ControllerUI>().enabled = false;
                desconnection3.GetComponent<ControllerUI>().enabled = false;
                desconnection4.GetComponent<ControllerUI>().enabled = false;
                desconnection5.GetComponent<ControllerUI>().enabled = false;
            }
        }
	}
}
