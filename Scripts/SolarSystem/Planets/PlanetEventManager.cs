using System;
using UnityEngine;

public class PlanetEventManager : MonoBehaviour
{
    private static PlanetEventManager instance;
    public static PlanetEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag(EditorConstants.TAG_PLANET_EVENT_MANAGER).GetComponent<PlanetEventManager>();
            }
            return instance;
        }
    }

    public event Action OnPlanetScanComplete;
    public void PlanetScanComplete()
    {
        OnPlanetScanComplete?.Invoke(); // Shorthand for checking if OnPlanetScanComplete != null.
    }
}
