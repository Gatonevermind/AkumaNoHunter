﻿using UnityEngine;
using System.Collections;

public class ControlVersion : MonoBehaviour
{

    public string stringToEdit = "v1.7";
    public GUIStyle myStyle;
    void OnGUI()
    {
        GUI.Label(new Rect(10, Screen.height - 15, 100, 30), stringToEdit, myStyle);
    }

}