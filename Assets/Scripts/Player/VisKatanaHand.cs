using UnityEngine;
using System.Collections;

public class VisKatanaHand : MonoBehaviour {

	public bool combat;
	private float show = 0;
	// Use this for initialization
	void Start () 
	{
		combat = false;
		HideChildren ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			combat = !combat;
			show += 0.1f;
		}
		
		if (combat)
		{
			if((show > 0) && (show < 5))
			{
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
			if((show > 0) && (show < 5.9f))
			{
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