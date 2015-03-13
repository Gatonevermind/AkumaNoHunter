using UnityEngine;
using System.Collections;

public class DeadMenu : MonoBehaviour 
{
    public bool deadMenu = false;
    GameObject[] deadControl;
    public string mainLevel;
    public string level;
    float counterToMenu;

    void Start() 
    {
        deadControl = GameObject.FindGameObjectsWithTag("Dead");
        foreach (GameObject dead in deadControl)
            dead.SetActive(false);

    }

	void Update () 
    {
        if (transform.GetComponent<PlayerHealth>().curHealth <= 0)
       {
            counterToMenu += Time.deltaTime;
            if (counterToMenu >= 1)
            {
                deadMenu = true;
            }

            if (deadMenu)
            {
                foreach (GameObject dead in deadControl)
                    dead.SetActive(true);
                Debug.Log("www");
            }
            else
            {
                foreach (GameObject dead in deadControl)
                    dead.SetActive(false);
            }
       }
	}

    public void Retry()
    {
        Application.LoadLevel(level);
    }

    public void Exit()
    {
        Application.LoadLevel(mainLevel);
    }
}
