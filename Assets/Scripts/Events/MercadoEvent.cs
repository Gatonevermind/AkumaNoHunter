using UnityEngine;
using System.Collections;

public class MercadoEvent : MonoBehaviour 
{
	public GameObject lobo03;
	public GameObject lobo04;
	public GameObject collisionLobo01;
	public GameObject collisionLobo02;
	public GameObject collisionLobo03;
	public GameObject collisionLobo04;


	void Start ()
	{

	}
	

	void Update () 
	{
		if((!collisionLobo01.activeSelf) && (!collisionLobo02.activeSelf))
		{
			lobo03.SetActive(true);
			lobo04.SetActive(true);
		}
	}
}
