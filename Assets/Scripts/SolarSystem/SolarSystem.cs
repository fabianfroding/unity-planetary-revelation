using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> planets;

    private static SolarSystem instance;
    public static SolarSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find(EditorConstants.GAME_OBJECT_NAME_SOLAR_SYSTEM).GetComponent<SolarSystem>();
            }
            return instance;
        }
    }

    public List<GameObject> GetPlanets()
    {
        return planets;
    }
}
