using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public Transform ObjectToFollow;
    public float XDistance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPosition = this.transform.position;
		currentPosition.x = ObjectToFollow.transform.position.x + XDistance;
		this.transform.position = currentPosition;
	}
}
