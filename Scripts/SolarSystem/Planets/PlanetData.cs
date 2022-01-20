using UnityEngine;

[CreateAssetMenu(fileName = "PlanetData", menuName = "Data/Planet Data")]
public class PlanetData : ScriptableObject
{
    public string planetName;
    [TextArea(15, 20)]
    public string description;
    public int size;
    public int distanceToSun;
}
