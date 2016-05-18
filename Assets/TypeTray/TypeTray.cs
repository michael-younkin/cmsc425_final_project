using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TypeTray : MonoBehaviour {

    public delegate void CommandSubmitEvent(string command);
    public event CommandSubmitEvent CommandSubmit;

    public int maxTrayTextLength = 10;

    static HashSet<KeyCode> letters = new HashSet<KeyCode>(new KeyCode[] {
        KeyCode.A,
        KeyCode.B,
        KeyCode.C,
        KeyCode.D,
        KeyCode.E,
        KeyCode.F,
        KeyCode.G,
        KeyCode.H,
        KeyCode.I,
        KeyCode.J,
        KeyCode.K,
        KeyCode.L,
        KeyCode.M,
        KeyCode.N,
        KeyCode.O,
        KeyCode.P,
        KeyCode.Q,
        KeyCode.R,
        KeyCode.S,
        KeyCode.T,
        KeyCode.U,
        KeyCode.V,
        KeyCode.W,
        KeyCode.X,
        KeyCode.Y,
        KeyCode.Z,
    });

    static Dictionary<KeyCode, string> numbers = new Dictionary<KeyCode, string>();

    InputEmitterWrapper gameplayInputEmitter;

    Text trayText;

    static TypeTray()
    {
        // Setup the numbers dictionary
        KeyCode[] numberKeyCodes = new KeyCode[]
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,
        };
        foreach (var key in numberKeyCodes)
        {
            string name = key.ToString();
            numbers[key] = name.Substring(5);
        }
    }

	void Start () {
        var gameManager = GameUtil.GameManager;
        gameplayInputEmitter = gameManager.GameplayInputEmitter;
        gameplayInputEmitter.KeyUp += GameplayInputEmitter_KeyUp;

        trayText = this.SafeFindChild("Text").SafeGetComponent<Text>();
	}

    private void GameplayInputEmitter_KeyUp(KeyCode key)
    {
        int textLength = trayText.text.Length;
        // If they typed a letter or number and there aren't too many characters already, add it to the tray text
        if (textLength + 1 < maxTrayTextLength)
        {
            string addition = "";
            if (letters.Contains(key))
            {
                addition = key.ToString().ToLower();
            } else if (numbers.ContainsKey(key))
            {
                addition = numbers[key];
            } else if (key == KeyCode.Space)
            {
                addition = " ";
            }
            trayText.text += addition;
        // If they typed backspace, remove a character if there are any
        }
        if (key == KeyCode.Backspace && textLength > 0)
        {
            trayText.text = trayText.text.Substring(0, textLength - 1);
        }
        // If they typed enter, try to submit the command
        if (key == KeyCode.Return && textLength > 0)
        {
            string text = trayText.text;
            trayText.text = "";
            if (CommandSubmit != null)
            {
                CommandSubmit(text);
            }
        }
    }
}
