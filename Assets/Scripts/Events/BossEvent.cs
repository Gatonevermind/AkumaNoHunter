using UnityEngine;
using System.Collections;

public class BossEvent : MonoBehaviour 
{
    public static float countToCinematic;

	public GameObject colliderLobo01;
	public GameObject colliderLobo02;

	public GameObject lobo01;
	public GameObject lobo02;
	public GameObject loboKai;

    public GameObject Cinematic;
    
	public GameObject player;
    public GameObject kai;
    public GameObject boss;


	public Transform kaiTransform;
	public Transform playerTransform;

	public Transform playerSpawn;


	void Start () 
    {
        Cinematic.SetActive(false);
        boss.SetActive(false);
		lobo01.SetActive (false);
		lobo02.SetActive (false);
		loboKai.SetActive (false);

	}
	
	void Update () 
    {
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
            countToCinematic += Time.deltaTime;

            if ((countToCinematic > 2) && (countToCinematic < 3))
            {
				player.SetActive(false);
                Cinematic.SetActive(true);
                loboKai.SetActive(false);
                kai.SetActive(false);
				player.transform.position = playerSpawn.position;
				player.transform.rotation = playerSpawn.rotation;
            }

            if (countToCinematic >= 44)
            {
				player.SetActive (true);
                Cinematic.SetActive(false);
                boss.SetActive(true);
            }
        }

	}
}
