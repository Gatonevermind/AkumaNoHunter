using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    public float currentHealth;
    void Update()
    {
        currentHealth = PlayerHealth.curHealth/100;

        Image image = GetComponent<Image>();

        image.fillAmount = currentHealth;
    }

}
