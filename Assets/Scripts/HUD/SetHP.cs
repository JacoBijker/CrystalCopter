using UnityEngine;
using System.Collections;

public class SetHP : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Restart()
    {
        UpdateHP(1);
    }

    void UpdateHP(int health)
    {
        var allSprites = this.transform.GetComponentsInChildren<SpriteRenderer>();

        foreach (var sprite in allSprites)
        {
            sprite.enabled = sprite.tag.ToString().Equals(health.ToString());
        }
    }
}
