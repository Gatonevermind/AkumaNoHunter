using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour 
{
    public float currentStamina;
    void Update()
    {
        currentStamina = PlayerMovement.stamina/100;

        Image image = GetComponent<Image>();

        image.fillAmount = currentStamina;
    }

}
