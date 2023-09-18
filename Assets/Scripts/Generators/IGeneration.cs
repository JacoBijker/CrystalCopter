using UnityEngine;
using System.Collections;

public interface IGeneration {

    float[] Generate();
    void SetStartHeight(float height, int Range);
}
