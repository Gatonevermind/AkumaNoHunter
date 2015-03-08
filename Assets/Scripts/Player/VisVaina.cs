using UnityEngine;
using System.Collections;

public class VisVaina : MonoBehaviour 
{	
    public bool activeVaina;
	// Use this for initialization
	void Start () 
	{
		activeVaina = false;
		HideChildren();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (activeVaina)
		{
			ShowChildren();
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