using UnityEngine;
using System.Collections;

public class TerrainInitializationInfo {
    public float YStartPosition { get; set; }
    public float AnchorPoint { get; set; }
    public int Range { get; set; }
    public int AlternateMaterialIndex { get; set; }

    public TerrainInitializationInfo(float yStartPosition, float anchorPoint, int range, int AlternateMaterialIndex)
    {
        this.YStartPosition = yStartPosition;
        this.AnchorPoint = anchorPoint;
        this.Range = range;
        this.AlternateMaterialIndex = AlternateMaterialIndex;
    }
}
