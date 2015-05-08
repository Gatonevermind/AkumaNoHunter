using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBossBar : MonoBehaviour 
{
    public float currentHealth;
    void Update()
    {
        currentHealth = WolfIABOSS.enemyVida/2000;

        Image image = GetComponent<Image>();

        image.fillAmount = currentHealth;
    }

}
