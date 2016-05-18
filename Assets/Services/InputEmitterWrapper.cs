using UnityEngine;
using System.Collections;

public class InputEmitterWrapper {
    public delegate void KeyEvent(KeyCode key);
    public event KeyEvent KeyUp;

    InputEmitter root;

    public InputEmitterWrapper(InputEmitter root)
    {
        this.root = root;
        root.KeyUp += Root_KeyUp;
    }

    private void Root_KeyUp(KeyCode key)
    {
        if (KeyUp != null)
        {
            KeyUp(key);
        }
    }
}
