using UnityEngine;
using System.Collections;

public class BackgroundSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Switch(int value)
    {
        var allChildren = this.GetComponentsInChildren<SpriteRenderer>();
        foreach (var child in allChildren)
            child.enabled = child.tag.ToString().Equals(value.ToString());
    }

    void TerrainSwap(int value)
    {
        Switch(value);
    }

    void Restart()
    {
        Switch(1);
    }
}
