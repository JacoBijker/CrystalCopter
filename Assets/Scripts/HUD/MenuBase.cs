using UnityEngine;
using System.Collections;

public class MenuBase : MonoBehaviour {

    protected float width;
    protected float height;
    protected float buttonHeight;
    protected float buttonPadding;
    protected float xPadding = 20;

	// Use this for initialization
    protected virtual void Start()
    {
        width = Screen.width / 2;
        height = Screen.height / 2;
        buttonHeight = height / 4;
        buttonPadding = buttonHeight / 4;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
