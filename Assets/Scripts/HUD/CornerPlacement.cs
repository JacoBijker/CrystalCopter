using UnityEngine;
using System.Collections;

public class CornerPlacement : MonoBehaviour {
    public Camera mainCamera;
	// Use this for initialization
	void Start () {
        var topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));

        topLeft.x += 0.5f;
        topLeft.y -= 0.5f;
        //topLeft.z = 10;
        this.transform.position = topLeft;

        float scale = Screen.height / 240f;
        float diff = scale - 1;
        scale = 1 + diff / 2f;
        this.transform.localScale = new Vector3(scale, scale, 1);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
