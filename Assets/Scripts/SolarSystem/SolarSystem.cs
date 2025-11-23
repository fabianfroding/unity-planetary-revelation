using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> planets = new();

    private List<string> planetsDiscovered = new();

    public static event Action<int> OnPlanetDiscovered;

    private static SolarSystem instance;
    public static SolarSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<SolarSystem>();
            }
            return instance;
        }
    }

    public List<GameObject> GetPlanets()
    {
        return planets;
    }

    public void AddToPlanetsDiscovered(string planetName)
    {
        if (planets == null)
            return;
        
        if (planetsDiscovered.Contains(planetName))
            return;
        
        planetsDiscovered.Add(planetName);
        OnPlanetDiscovered?.Invoke(planetsDiscovered.Count);
    }
}
