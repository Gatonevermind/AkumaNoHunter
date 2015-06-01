using UnityEngine;
using System.Collections;

public class PauseBlur : MonoBehaviour {

	private float speedTransition;
	public float maxDis;
	public float minDis;
	private float currentDis;

	// Use this for initialization
	void Start () {

		maxDis = 80;
		minDis = 60;
		speedTransition = 5;
		//blur = GetComponent<BlurEffect>();


	}
	
	// Update is called once per frame
	void Update () {

		if(PauseMenu.pauseMenu)
		{
			transform.GetComponent<Blur>().enabled = true;
		}

		else
		{
			transform.GetComponent<Blur>().enabled = false;
		}

		if(PlayerMovement.sprintActive)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,maxDis,speedTransition * Time.deltaTime);
			transform.GetComponent<MotionBlur>().enabled = true;
			//GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,maxDis,speedTransition * Time.deltaTime);

		}
		
		else
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,minDis,speedTransition * Time.deltaTime);
			transform.GetComponent<MotionBlur>().enabled = false;
			//GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,minDis,speedTransition * Time.deltaTime);
		}

		if(PlayerAttack.attackCount == 7 || PlayerAttack.attackCount == 8)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,maxDis,speedTransition * Time.deltaTime);
			transform.GetComponent<MotionBlur>().enabled = true;
			//GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,maxDis,speedTransition * Time.deltaTime);
		}


		/*if(PlayerAttack.attackCount == 8)
		{
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,minDis,speedTransition * Time.deltaTime);
			transform.GetComponent<MotionBlur>().enabled = false;
			//GetComponent<Camera>().fieldOfView = Mathf.Lerp(Camera.main.fieldOfView,minDis,speedTransition * Time.deltaTime);
		}*/
	}
}
