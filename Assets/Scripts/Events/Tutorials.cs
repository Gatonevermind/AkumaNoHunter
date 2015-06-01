using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Tutorials : MonoBehaviour 
{
	public static float tutorialTimer;
	public float timer;
	public static float tutorialCounter;
	public float counter;

	public GameObject loboCollider00;
	public GameObject loboCollider01;

	public GameObject movementTutorial;
	public GameObject sprintTutorial;
	public GameObject unsheatheTutorial;
	public GameObject attackTutorial;
	public GameObject dashTutorial;
	public GameObject dashAttackTutorial;
	public GameObject canvasTutorial;
	public GameObject attackCombo;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

	void OnTriggerStay(Collider other)
	{

		if(tutorialCounter == 1)
		{
			if (other.tag == "MovementTutorial")
			{
				tutorialTimer += Time.deltaTime;
				if(tutorialTimer >= 0.3f)
				{	
					movementTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					tutorialCounter = 1.5f;

				}
			}
		}
		else if(tutorialCounter == 2)
		{
			if (other.tag == "SprintTutorial")
			{
				Objectives.objectivesCount = 1;
				tutorialTimer += Time.deltaTime;
				if(tutorialTimer >= 0.1f)
				{
					sprintTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					tutorialCounter = 2.5f;
				}
			}
		}
		else if(tutorialCounter == 3)
		{
			if (other.tag == "UnsheatheTutorial")
			{
				tutorialTimer += Time.deltaTime;
				if(tutorialTimer >= 0.7f)
				{
					unsheatheTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					tutorialCounter = 3.5f;
				}
			}
		}
		else if(tutorialCounter == 4)
		{
			if (other.tag == "AttackTutorial")
			{
				tutorialTimer += Time.deltaTime;

				if(tutorialTimer > 0.7f)
				{
					attackTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					attackCombo.SetActive(true);
					tutorialCounter = 4.5f;
				}
			}
		}
		else if(tutorialCounter == 5)
		{
			if(other.tag == "DashAttackTutorial")
			{
				if(tutorialTimer >= 0.7f)
				{
					dashAttackTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					tutorialCounter = 5.5f;
				}
			}
		}
		else if(tutorialCounter == 6)
		{
			if (other.tag == "DashTutorial")
			{
				tutorialTimer += Time.deltaTime;
				if(tutorialTimer >= 0.7f)
				{
					dashTutorial.SetActive(true);
					canvasTutorial.SetActive(true);
					tutorialCounter = 6.5f;
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(tutorialCounter == 1.5f)
		{
			if(other.tag == "MovementTutorial")
			{
				movementTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 2;
			}
		}
		else if(tutorialCounter == 2.5f)
		{
			if(other.tag == "SprintTutorial")
			{
				sprintTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 3;
			}
		}
	}

    void Start() 
    {
		movementTutorial.SetActive (false);
		canvasTutorial.SetActive(false);
		tutorialCounter = 1;
    }
    void Update()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

		if(tutorialCounter == 1.5f)
		{
			if(Input.GetKeyDown(KeyCode.E) || (state.Buttons.X == ButtonState.Pressed))
			{
				movementTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 2;
			}
		}

		else if(tutorialCounter == 2.5f)
		{
            if (Input.GetKeyDown(KeyCode.LeftShift) || (prevState.Buttons.LeftShoulder == ButtonState.Pressed))
			{
				sprintTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 3;
			}
		}
		else if(tutorialCounter == 3.5f)
		{
			if(Input.GetKeyDown(KeyCode.Q) || (state.Buttons.Y == ButtonState.Pressed))
			{
				unsheatheTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = -1;
				tutorialCounter = 4;
			}
		}
		else if(tutorialCounter == 4.5f)
		{
			if(!loboCollider00.activeSelf)
			{
				attackTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				attackCombo.SetActive(false);
				tutorialTimer = -1;
				tutorialCounter = 5;
			}
		}
		else if(tutorialCounter == 5.5f)
		{
			if(!loboCollider01.activeSelf)
			{
				dashAttackTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 6;
			}
		}
		else if(tutorialCounter == 6.5f)
		{
			if(Input.GetKeyDown(KeyCode.LeftControl) || (state.Buttons.Y == ButtonState.Pressed))
			{
				dashTutorial.SetActive(false);
				canvasTutorial.SetActive(false);
				tutorialTimer = 0;
				tutorialCounter = 7;
			}
		}
		counter = tutorialCounter;
		timer = tutorialTimer;
    }
  
}
