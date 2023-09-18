using UnityEngine;
using System.Collections;

public class Midpoint : IGeneration
{
    private int size;
    private float? startHeight;
    private int Range;

    public Midpoint(int size, int Range)
    {
        this.size = size;
        this.Range = Range;
    }

    void RecursiveMidpoint(ref float[] heightMap, int lowerPoint, int highPoint, float minHeight, float maxHeight)
    {
        int newPoint = (int)((lowerPoint + highPoint) / 2);
        if (newPoint == lowerPoint || newPoint == highPoint)
            return;

        //var MinDecayRate = (minHeight / (heightMap.Length / 2)) * minHeight;
        //var MaxDecayRate = (minHeight / (heightMap.Length / 2)) * maxHeight;

        heightMap[newPoint] = (heightMap[lowerPoint] + heightMap[highPoint]) / 2 + Random.Range(minHeight, maxHeight);
        RecursiveMidpoint(ref heightMap, lowerPoint, newPoint, minHeight / 2, maxHeight / 2);
        RecursiveMidpoint(ref heightMap, newPoint, highPoint, minHeight / 2, maxHeight / 2);
    }

    public float[] Generate()
    {
        float[] toReturn = new float[size];

        toReturn[0] = startHeight ?? Random.Range(0, Range);
        toReturn[size - 1] = Random.Range(0, Range);

        RecursiveMidpoint(ref toReturn, 0, size - 1, 0, Range);

        startHeight = null;

        return toReturn;
    }


    public void SetStartHeight(float height, int Range)
    {
        this.startHeight = height;
        this.Range = Range;
    }
}
