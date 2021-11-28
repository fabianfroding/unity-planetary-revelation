using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Data/Planet Data")]
public class PlanetData : ScriptableObject
{
    [TextArea(15, 20)]
    public string description;
    [Range(0, 100)]
    public float percentScanned;
    [Range(1, 3)]
    public float percentScannedPerSec;
}
