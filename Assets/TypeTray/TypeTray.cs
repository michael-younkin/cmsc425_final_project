using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TypeTray : MonoBehaviour {

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
        KeyCode.Z
    });

    InputEmitterWrapper gameplayInputEmitter;

    Text trayText;

	void Start () {
        var gameManager = GameUtil.GameManager;
        gameplayInputEmitter = gameManager.GameplayInputEmitter;
        gameplayInputEmitter.KeyUp += GameplayInputEmitter_KeyUp;

        trayText = this.SafeFindChild("Text").SafeGetComponent<Text>();
	}

    private void GameplayInputEmitter_KeyUp(KeyCode key)
    {
        int textLength = trayText.text.Length;
        // If they typed a letter and there aren't too many characters already, add it to the tray text
        if (letters.Contains(key) && textLength + 1 < maxTrayTextLength)
        {
            trayText.text += key.ToString().ToLower();
        // If they typed backspace, remove a character if there are any
        } else if (key == KeyCode.Backspace && textLength > 0)
        {
            trayText.text = trayText.text.Substring(0, trayText.text.Length - 1);
        }
    }
}
