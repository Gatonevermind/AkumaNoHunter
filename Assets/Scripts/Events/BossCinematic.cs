using UnityEngine;
using System.Collections;

public class BossCinematic : MonoBehaviour 
{
    public Transform loboInicial;
    public float countToCinematic;

    public GameObject Cinematic;
    public GameObject lobo;
    public GameObject kai;
    public GameObject boss; 


	void Start () 
    {
        Cinematic.SetActive(false);
        boss.SetActive(false);
	}
	
	void Update () 
    {
        EnemyHealth eh = (EnemyHealth)loboInicial.GetComponent("EnemyHealth");

        if (eh.curHealth <= 0)
        {
            countToCinematic += Time.deltaTime;

            if (countToCinematic >= 2)
            {
                Cinematic.SetActive(true);
                lobo.SetActive(false);
                kai.SetActive(false);
            }

            if (countToCinematic >= 42)
            {
                Cinematic.SetActive(false);
                boss.SetActive(true);
            }
        }

	}
}
