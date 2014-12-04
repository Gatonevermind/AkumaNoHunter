using UnityEngine;
using System.Collections;

public class VisibilityObjects : MonoBehaviour {
	public bool combat;
	// Use this for initialization
	void Start () 
	{
		combat = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			combat = !combat;
		}
		
		if (combat)
		{
			ShowChildren ();
		}
		else if (!combat)
		{
			
			HideChildren ();
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