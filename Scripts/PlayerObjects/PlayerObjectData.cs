using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObjectData", menuName = "Data/Player Object Data")]
public class PlayerObjectData : ScriptableObject
{
    public Material lineRendererMaterial;
    [Range(300, 1200)]
    public float scanRange;
    [Range(1, 100)]
    public int resourceCost;
    [Range(1, 4)]
    public int maxTargets;
}
