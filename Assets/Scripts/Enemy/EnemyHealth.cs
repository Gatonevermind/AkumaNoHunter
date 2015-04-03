using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int curHealth = 100;
    private float deadCounter = 2;
    // Use this for initialization

    // Update is called once per frame
    private void Update()
    {
        AddjustCurrentHealth(0);
    }

    public void AddjustCurrentHealth(int adj)
    {
        curHealth += adj;

        if (curHealth < 0)
            curHealth = 0;

        if (curHealth > maxHealth)
            curHealth = maxHealth;

        if (maxHealth < 1)
            maxHealth = 1;
    }
}