using UnityEngine;
using System.Collections;

public class BossEvent : MonoBehaviour 
{
    public static float countToCinematic;
	public static bool bossCinematic;
	public static bool boolSoundCinematic;

	public GameObject colliderLobo01;
	public GameObject colliderLobo02;

	public GameObject lobo01;
	public GameObject lobo02;
	public GameObject loboKai;

    public GameObject Cinematic;
    
	public GameObject player;
    public GameObject kai;
    public GameObject boss;

	public GameObject katanaHand;
	public GameObject katana;

	public GameObject ambushText;
	public float ambushTextTimer;


	public Transform kaiTransform;
	public Transform playerTransform;

	public Transform playerSpawn;

	public GameObject cinematicBorder;

	//PLAYER HP BAR
	public GameObject playerUI;

	//BOSS HP BAR
	public GameObject bossUI;
	public float deathCounter;

	public GameObject congratulationsCanvas;

	void Start () 
    {
		ambushTextTimer = 0;
		boolSoundCinematic = false;

        Cinematic.SetActive(false);
        boss.SetActive(false);
		lobo01.SetActive (false);
		lobo02.SetActive (false);
		loboKai.SetActive (false);

		cinematicBorder.SetActive (false);
		bossCinematic = false;

		bossUI.SetActive (false);
	}
	
	void Update () 
    {
		if(lobo01.activeSelf)
		{
			ambushTextTimer += Time.deltaTime;
			if(ambushTextTimer < 2)
			{
				ambushText.SetActive(true);
			}
			else
				ambushText.SetActive(false);
		}
		if(Vector3.Distance(transform.position, kaiTransform.position) < 2)
		{
			if(Vector3.Distance(transform.position, playerTransform.position) < 2)
			{

				lobo01.SetActive (true);
				lobo02.SetActive (true);
				loboKai.SetActive (true);
			}
		}
		if ((!colliderLobo01.activeSelf) && (!colliderLobo02.activeSelf))
        {
			Objectives.objectivesCount = 8;
            countToCinematic += Time.deltaTime;

            if ((countToCinematic > 2) && (countToCinematic < 3))
            {
				lobo01.SetActive (false);
				lobo02.SetActive (false);

				boolSoundCinematic = true;
				bossCinematic = true;
				player.SetActive(false);
                Cinematic.SetActive(true);
                loboKai.SetActive(false);
                kai.SetActive(false);
				player.transform.position = playerSpawn.position;
				player.transform.rotation = playerSpawn.rotation;

				//bordes cinematica
				cinematicBorder.SetActive(true);
            }
			if(countToCinematic >= 5)
			{
				if(Input.GetKeyDown(KeyCode.F))
				{
					countToCinematic = 39;
				}
				katana.SetActive(true);
				katanaHand.SetActive(false);
			}

            if (countToCinematic >= 39)
            {
				if(PauseMenu.pauseMenu == false)
				{
					if(!WolfIABOSS.bossDeath)
					bossUI.SetActive (true);
				}
				bossCinematic = false;
				player.SetActive (true);
                Cinematic.SetActive(false);
                boss.SetActive(true);

				//borde cinematica
				cinematicBorder.SetActive(false);
            }
			if(WolfIABOSS.bossDeath)
			{
				if(deathCounter<=12)
					deathCounter += Time.deltaTime;

				if(deathCounter > 5)
				{
					playerUI.SetActive(false);
					bossUI.SetActive(false);
				}

				if(deathCounter > 6)
				{
					congratulationsCanvas.SetActive(true);
				}
			}
        }

	}
}
