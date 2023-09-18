using UnityEngine;
using System.Collections;

public class GravityGracePeriod : MonoBehaviour {

    public float SecondsGrace = 1;
    private float remainingSeconds;

    private bool isActive = false;

	// Use this for initialization
	void Start () {
        remainingSeconds = SecondsGrace;
        this.GetComponent<Rigidbody2D>().gravityScale = 0;
	}

    void Restart()
    {
        Start();
        isActive = true;
    }

	// Update is called once per frame
	void Update () {
        if (!isActive)
            return;

        remainingSeconds -= Time.deltaTime;

        if (remainingSeconds < 0 || this.GetComponent<Rigidbody2D>().velocity.y != 0)
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
	}
}
