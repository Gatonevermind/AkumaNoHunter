using UnityEngine;
using System.Collections;

public class ControlBehaviour : MonoBehaviour {

    public string levelname;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.F7))
        {
            Application.LoadLevel(levelname);
        }

    }
}
