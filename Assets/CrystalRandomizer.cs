using UnityEngine;
using System.Collections;

public class CrystalRandomizer : MonoBehaviour {

    public Sprite[] Crystals;

	// Use this for initialization
	void Start () {
        int index = Random.Range(0, Crystals.Length);
        this.GetComponent<SpriteRenderer>().sprite = Crystals[index];

        var scale = Random.Range(2.0f, 3.0f);
        this.transform.localScale = new Vector3(scale, scale, scale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
