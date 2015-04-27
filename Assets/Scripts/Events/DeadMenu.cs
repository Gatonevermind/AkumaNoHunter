using UnityEngine;

public class DeadMenu : MonoBehaviour
{
    public bool deadMenu = false;
    private GameObject[] deadControl;
    public string mainLevel;
    public string level;
    private float counterToMenu;

    private void Start()
    {
        deadControl = GameObject.FindGameObjectsWithTag("Dead");
        foreach (GameObject dead in deadControl)
            dead.SetActive(false);
    }

    private void Update()
    {
        if (PlayerHealth.curHealth <= 0)
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