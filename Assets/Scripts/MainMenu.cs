using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
    public string playLevel;

    public void Play()
    {
        Application.LoadLevel(playLevel);
    }

    public void Exit()
    {
        Application.Quit();  
    }
}
