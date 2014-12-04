using UnityEngine;
using System.Collections;

public class VisKatana : MonoBehaviour {

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
			HideChildren ();
		}
		else if (!combat)
		{
			
			ShowChildren ();
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