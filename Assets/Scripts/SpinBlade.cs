using UnityEngine;
using System.Collections;

public class SpinBlade : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var scale = this.transform.localScale;
        scale.x = -scale.x;
        this.transform.localScale = scale;
	}
}
