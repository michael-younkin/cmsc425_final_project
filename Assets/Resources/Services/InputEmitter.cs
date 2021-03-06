﻿using UnityEngine;
using System.Collections;
using System;

public class InputEmitter : MonoBehaviour {
    public delegate void KeyEvent(KeyCode key);
    public event KeyEvent KeyUp;

    public void Update()
    {
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(code))
            {
                if (KeyUp != null)
                {
                    KeyUp(code);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}