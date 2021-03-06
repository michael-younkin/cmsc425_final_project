﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeTarget : MonoBehaviour {

    public const int MAX_TEXT_LENGTH = 19;

    public string command = "none";

    public string Text
    {
        get
        {
            return text.text;
        }

        set
        {
            if (value.Length > MAX_TEXT_LENGTH)
            {
                Debug.LogError(string.Format("Input text for type target is too long: \"{0}\"", value));
            }
            text.text = value;
        }
    }

    Text text;

	void Awake () {
        text = this.SafeFindChild("Text").SafeGetComponent<Text>();
	}
}
