using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossDetection: MonoBehaviour 
{
	public RectTransform healthTransform;
	private float cachedYH;
	private float minXValueH;
	private float maxXValueH;
	private float currentHealth;

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

	void Start () 
	{
		cachedYH = healthTransform.position.y;
		maxXValueH = healthTransform.position.x + (healthTransform.rect.width);
		minXValueH = healthTransform.position.x;
		currentHealth = maxHealth;
	}

	void Update () 
	{
        HandleHealth();

        currentHealth = Mathf.Lerp(currentHealth, 0, 0.07f);
	}

    private void HandleHealth()
    {

        float xValueH = MapValues(currentHealth - maxHealth, 0, maxHealth, minXValueH, maxXValueH);
        //colocar transform de x en su vida
        healthTransform.position = new Vector3(xValueH, cachedYH, 0);
    }

	private float MapValues(float variableToChange, float inMin, float inMax, float outMin, float outMax)
	{
        return (variableToChange - inMin) * (outMax - outMin) / (inMax - inMin) + outMin; 
	}
}