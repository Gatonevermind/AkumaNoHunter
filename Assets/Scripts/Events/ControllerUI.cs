using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using XInputDotNetPure;

public class ControllerUI : MonoBehaviour 
{

	public List<Transform> OtherLists; // Used if there are more than one lists in the game
    public List<Transform> childButtons; // The buttons that are children of the gameobject

	private Transform selectedOject;
	private PointerEventData eventSystem;
    public int listNum = 0; // Tells what list is active
    public bool dPadInputY; // Trash bool that prevents the selection from jumping on the Y axis
	public bool dPadInputX; // Trash bool that prevents the selection from jumping on the X axis

    PlayerIndex playerIndex;
    bool playerIndexSet = false;
    GamePadState prevState;
    GamePadState state;

    public float Y;
    public float X;

	// Use this for initialization
	void Start () 
    {
		// We want the master lsit to be first
		if (!OtherLists.Contains (transform)) 
        {

			OtherLists.Insert (0, transform);
		}

		eventSystem = new PointerEventData (EventSystem.current);
	
		SelectList (0); // selects the first list

	
	
	}
	
	// Update is called once per frame
	void Update () 
    {
		selectedOject.GetComponent<Button> ().Select ();

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

        Y = state.ThumbSticks.Left.Y;
        X = state.ThumbSticks.Left.X;

		// selection of the correct list happens here
		if (OtherLists.Count > 1) {
			if (state.ThumbSticks.Left.X > 0.6f && dPadInputX == false) 
            { // This produces the selecetion for the buttons using your controller
				dPadInputX = true;
				if ((listNum) < childButtons.Count - 1) 
                {
					listNum = listNum + 1;
					SelectList (listNum);
				
				} 
                else 
                {
					listNum = 0;
					SelectList (listNum);
				}
			}
		
	
			if (state.ThumbSticks.Left.X < -0.6f && dPadInputX == false) 
            { // This produces the selecetion for the buttons using your controller
				dPadInputX = true;
				if (listNum != 0) 
                {
					listNum = listNum - 1;
					SelectList (listNum);
				} 
                else 
                {
					listNum = OtherLists.Count - 1;
					SelectList (listNum);
				}
			}
			if (state.ThumbSticks.Left.X == 0) 
            {
				dPadInputX = false;
			}
		}


		// The scrolling down the list and the selection of the button happens here
        if (state.ThumbSticks.Left.Y < -0.6f && dPadInputY == false)
        { // This produces the selecetion for the buttons using your controller
			dPadInputY = true;
			if ((childButtons.IndexOf (selectedOject) + 1) < childButtons.Count) 
            {
				selectedOject = childButtons [childButtons.IndexOf (selectedOject) + 1];
			} 
            else 
            {
				selectedOject = childButtons [0];
			
			}
		}

        if (state.ThumbSticks.Left.Y > 0.9f && dPadInputY == false)
        { // This produces the selecetion for the buttons using your controller
			dPadInputY = true;
			if (childButtons.IndexOf (selectedOject) != 0) 
            {
				selectedOject = childButtons [childButtons.IndexOf (selectedOject) - 1];
			} 
            else 
            {
				selectedOject = childButtons [childButtons.Count-1];
			}
		}
		if (state.ThumbSticks.Left.Y == 0) 
        {
			dPadInputY = false;
		}
			if (Input.GetButton ("Submit")) 
            {// Executes the Press
				ExecuteEvents.Execute(selectedOject.gameObject, eventSystem, ExecuteEvents.pointerDownHandler); 
			}
	}


	void SelectList(int Type)
    {
		if (childButtons.Count != 0) 
        {
			childButtons.Clear();
		}
		Transform[] _childButtons = OtherLists[Type].GetComponentsInChildren<Transform> (); // finds which objects are real buttons
		foreach (Transform trans in _childButtons) 
        {
			if(trans.GetComponent<Button>()!= null)
            {
				childButtons.Add(trans);
			}
		}
		if (childButtons.Count > 0)
        {
			selectedOject = childButtons[0];
		}
	}

	// This is a test fuction and for the example secene thorught the Button Parameters on the UI Button
	public void Click(GameObject button)
    {
		Debug.Log ("You pressed" + button.name);
	}

}





