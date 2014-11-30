using UnityEngine;
using System.Collections;

public class MControl : MonoBehaviour {

    public string levelexit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.F7))
        {
            Application.LoadLevel(levelexit);
        }
	}
}
