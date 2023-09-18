using UnityEngine;
using System.Collections;

public class TerrainPositioning : MonoBehaviour
{
    // Use this for initialization
    public void Initialize(float xPosition)
    {
        var currentPosition = this.transform.position;
        currentPosition.x = xPosition;
        transform.position = currentPosition;
    }
}
