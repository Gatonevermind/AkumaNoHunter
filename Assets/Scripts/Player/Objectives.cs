using UnityEngine;
using System.Collections;

public class Objectives : MonoBehaviour 
{
	public static float objectivesCount;
	public static float objectivesTimer;

	public GameObject talkKai;
	public GameObject talkKaiGreen;
	public GameObject findWeapon;
	public GameObject findWeaponGreen;
	public GameObject attack;
	public GameObject attackGreen;
	public GameObject defendMarket;
	public GameObject defendMarketGreen;
	public GameObject followKai;
	public GameObject followKaiGreen;

	public GameObject objectiveCanvas;

	public float objectives;

	// Use this for initialization

	
	// Update is called once per frame
	void Update () 
	{
		//objectives = objectivesCount;
		if (!PauseMenu.pauseMenu) 
		{
			if((BossEvent.countToCinematic <2) && (IntroCinematic.intro))
				objectiveCanvas.SetActive(true);
			else
				objectiveCanvas.SetActive(false);
		}
		else
			objectiveCanvas.SetActive(false);

		if(objectivesCount == 0)
		{
			if(IntroCinematic.intro)
				talkKai.SetActive(true);
		}
		if (objectivesCount == 0.5f)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive(true);
		}
		if (objectivesCount == 1)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive(false);
			findWeapon.SetActive(true);
		}
		else if (objectivesCount == 1.5f)
		{
			findWeapon.SetActive(false);
			findWeaponGreen.SetActive(true);
		}
		else if (objectivesCount == 2)
		{
			findWeaponGreen.SetActive(false);
			attack.SetActive(true);
		}
		else if (objectivesCount == 2.5f)
		{
			attack.SetActive(false);
			attackGreen.SetActive(true);
		}
		else if(objectivesCount == 3)
		{
			attackGreen.SetActive(false);
			talkKai.SetActive(true);
		}
		else if(objectivesCount == 3.5f)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive(true);
		}
		else if(objectivesCount == 4)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive(false);
			defendMarket.SetActive (true);
		}
		else if(objectivesCount == 4.5f)
		{
			defendMarket.SetActive (false);
			defendMarketGreen.SetActive (true);
		}
		else if(objectivesCount == 5)
		{
			defendMarket.SetActive(false);
			defendMarketGreen.SetActive(false);
			talkKai.SetActive (true);
		}
		else if(objectivesCount == 5.5f)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive (true);
		}
		else if(objectivesCount == 6)
		{
			talkKai.SetActive(false);
			talkKaiGreen.SetActive(false);
			followKai.SetActive (true);
		}
		else if(objectivesCount == 6.5f)
		{
			followKai.SetActive(false);
			followKaiGreen.SetActive (true);
		}
		else if(objectivesCount == 7)
		{
			followKai.SetActive(false);
			followKaiGreen.SetActive(false);
			attack.SetActive (true);
		}
		else if(objectivesCount == 7.5f)
		{
			attack.SetActive(false);
			attackGreen.SetActive (true);
		}
		else if(objectivesCount == 8)
		{
			objectiveCanvas.SetActive(false);
			attack.SetActive(false);
			attackGreen.SetActive(false);
		}
	}
}
