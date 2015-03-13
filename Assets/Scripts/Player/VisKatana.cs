using UnityEngine;
using System.Collections;

public class VisKatana : MonoBehaviour
{
	
	public bool combat;
	public bool activeKatana;
	private float show = 0;
	private float seatheCooldown = 0;
	private float timeActive = 0;
	// Use this for initialization
	void Start()
	{
		combat = false;
		activeKatana = false;
		HideChildren();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (activeKatana)
		{
			timeActive += Time.deltaTime;
			
			if (timeActive <= 0.4f) ShowChildren();
			
			if (seatheCooldown == 0)
			{
				if ((Input.GetKeyDown(KeyCode.Q)) && (PlayerMovement.grounded == 0))
				{
					combat = !combat;
					show += Time.deltaTime;
					seatheCooldown += Time.deltaTime;
				}
			}
			
			if (combat)
			{
				//Cooldown para desenvaine
				if ((seatheCooldown > 0) && (seatheCooldown < 0.5f))
				{
					seatheCooldown += Time.deltaTime;
				}
				else if (seatheCooldown >= 0.5f)
				{
					seatheCooldown = 0;
				}
				
				//Temporizador para show/hide de la katana
				if ((show > 0) && (show < 0.5f))
				{
					ShowChildren();
					show += Time.deltaTime;
				}
				if (show >= 0.5f)
				{
					HideChildren();
					show = 0;
				}
			}
			else if (!combat)
			{
				//Cooldown para envaine
				if ((seatheCooldown > 0) && (seatheCooldown < 2))
				{
					seatheCooldown += Time.deltaTime;
				}
				else if (seatheCooldown >= 2)
				{
					seatheCooldown = 0;
				}
				
				//Temporizador para show/hide de la katana
				if ((show > 0) && (show < 2))
				{
					HideChildren();
					show += Time.deltaTime;
				}
				if (show >= 2)
				{
					ShowChildren();
					show = 0;
				}
			}
		}
	}
	
	void HideChildren()
	{
		Renderer[] lChildRenderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer lRenderer in lChildRenderers)
		{
			lRenderer.enabled = false;
		}
	}
	void ShowChildren()
	{
		Renderer[] lChildRenderers = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer lRenderer in lChildRenderers)
		{
			lRenderer.enabled = true;
		}
	}
}