using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDetection: MonoBehaviour 
{
	public RectTransform healthTransform;
	private float cachedYH;
	private float minXValueH;
	private float maxXValueH;
	private float currentHealth;

    public RectTransform staminaTransform;
    private float cachedYS;
    private float minXValueS;
    private float maxXValueS;
    private float currentStamina;

	public float CurrentHealth
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = value;
		}
	}

	public int maxHealth;
    public Image visualHealth;
    public int maxStamina;
    public Image visualStamina;

	void Start () 
	{
		cachedYH = healthTransform.position.y;
		maxXValueH = healthTransform.position.x + (healthTransform.rect.width);
		minXValueH = healthTransform.position.x;
		currentHealth = maxHealth;

        cachedYS = staminaTransform.position.y;
        maxXValueS = staminaTransform.position.x + (staminaTransform.rect.width);
        minXValueS = staminaTransform.position.x;
        currentStamina = maxStamina;
	}

	void Update () 
	{
        HandleHealth();

        HandleStamina();


        currentHealth = Mathf.Lerp(currentHealth, PlayerHealth.curHealth, 0.07f);

        currentStamina = Mathf.Lerp(currentStamina, PlayerMovement.stamina, 0.07f);
	}

    private void HandleHealth()
    {

        float xValueH = MapValues(currentHealth - maxHealth, 0, maxHealth, minXValueH, maxXValueH);
        //colocar transform de x en su vida
        healthTransform.position = new Vector3(xValueH, cachedYH, 0);
    }

    private void HandleStamina()
    {
        float xValueS = MapValues(currentStamina - maxStamina, 0, maxStamina, minXValueS, maxXValueS);
        //colocar transform de x en su stamina
        staminaTransform.position = new Vector3(xValueS, cachedYS, 0);
    }
	private float MapValues(float variableToChange, float inMin, float inMax, float outMin, float outMax)
	{
        return (variableToChange - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; 
	}
}