/*
using UnityEngine;

public class FinalLevel : MonoBehaviour
{
    public Transform enemy;
    public bool activeFL;
    private GameObject[] finalLevelControl;
    public string loadlevel;

    private void Start()
    {
        activeFL = false;

        finalLevelControl = GameObject.FindGameObjectsWithTag("FinalLevelCanvas");
        foreach (GameObject finalLevel in finalLevelControl)
            finalLevel.SetActive(false);
    }

    private void Update()
    {
        EnemyHealth status = (EnemyHealth)enemy.GetComponent("EnemyHealth");
        MobBoss liveTime = (MobBoss)enemy.GetComponent("MobBoss");

        if (liveTime.finalDie >= 8)
        {
            activeFL = true;
        }

        if (liveTime.finalDie >= 15) Application.LoadLevel(loadlevel);
        if (activeFL)
        {
            foreach (GameObject finalLevel in finalLevelControl)
                finalLevel.SetActive(true);
        }
        else
            foreach (GameObject finalLevel in finalLevelControl)
                finalLevel.SetActive(false);
    }
}
*/