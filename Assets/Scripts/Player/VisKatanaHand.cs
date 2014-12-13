using UnityEngine;
using System.Collections;

public class VisKatanaHand : MonoBehaviour {

	public bool combat;
	private float show = 0;
	private float seatheCooldown = 0;
	// Use this for initialization
	void Start () 
	{
		combat = false;
		HideChildren ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(seatheCooldown == 0)
		{
			if (Input.GetKeyDown(KeyCode.Q))
			{
				combat = !combat;
				show += 0.1f;
				seatheCooldown += 0.1f;
			}
		}
		
		if (combat)
		{
			//Cooldown para desenvaine
			if((seatheCooldown > 0) && (seatheCooldown < 5))
			{
				seatheCooldown += 0.1f;
			}
			else if (seatheCooldown >=5)
			{
				seatheCooldown = 0;
			}
			
			//Temporizador para show/hide de la katana
			if((show > 0) && (show < 5))
			{
				HideChildren ();
				show += 0.1f;
			}
			if(show >= 5)
			{
				ShowChildren ();
				show = 0;
			}
		}
		else if (!combat)
		{
			//Cooldown para envaine
			if((seatheCooldown > 0) && (seatheCooldown < 7))
			{
				seatheCooldown += 0.1f;
			}
			else if (seatheCooldown >=7)
			{
				seatheCooldown = 0;
			}
			
			//Temporizador para show/hide de la katana
			if((show > 0) && (show < 5.9f))
			{
				ShowChildren ();
				show += 0.1f;
			}
			if(show >= 5.9f)
			{
				HideChildren ();
				show = 0;
			}
			
		}
	}
	
	void HideChildren()
	{
		Renderer[] lChildRenderers=gameObject.GetComponentsInChildren<Renderer>();
		foreach ( Renderer lRenderer in lChildRenderers)
		{
			lRenderer.enabled=false;
		}
	}
	void ShowChildren()
	{
		Renderer[] lChildRenderers=gameObject.GetComponentsInChildren<Renderer>();
		foreach ( Renderer lRenderer in lChildRenderers)
		{
			lRenderer.enabled=true;
		}
	}
}