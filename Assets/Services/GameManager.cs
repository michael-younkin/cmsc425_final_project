using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private InputEmitterWrapper gameplayInputEmitter;
    public InputEmitterWrapper GameplayInputEmitter
    {
        get
        {
            return gameplayInputEmitter;
        }
    }

	void Start () {
        gameplayInputEmitter = new InputEmitterWrapper(this.SafeGetComponent<InputEmitter>());
	}
	
	void Update () {
	
	}
}
