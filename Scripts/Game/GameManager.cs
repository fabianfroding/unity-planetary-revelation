using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> planets; // TODO: Remove and replace references to solar system GO.

    private void Start()
    {
        Load();
        UIManager.Instance.ResetPlanetUIData(); // Hide default text.
        PlanetEventManager.Instance.OnPlanetScanComplete += UpdatePlanetsScanned;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void UpdatePlanetsScanned()
    {
        int numPlanetsFullyScanned = 0;
        for (int i = 0; i < planets.Count; i++)
        {
            if (planets[i].GetComponent<PlanetScript>().planetData.percentScanned >= 100)
            {
                numPlanetsFullyScanned++;
            }
        }
        UIManager.Instance.UpdatePlanetsScannedText(numPlanetsFullyScanned, planets.Count);

        if (numPlanetsFullyScanned >= planets.Count)
        {
            // TODO: Game complete.
            Debug.Log("Game complete.");
        }
    }

    private void Load()
    {
        PlayerPrefs.DeleteAll();
        LoadCameraPosition();
        LoadPlanets();
        LoadPlayerResources();
    }

    private void Save()
    {
        SaveCameraPosition();
        SavePlanets();
        SavePlayerResources();
    }

    private void LoadCameraPosition()
    {
        Vector3 pos = PlayerPrefsManager.LoadCameraPosition();
        if (pos != new Vector3())
        {
            Camera.main.transform.position = pos;
        }
    }

    private void LoadPlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            planets[i].GetComponent<PlanetScript>().planetData.percentScanned = PlayerPrefsManager.LoadPlanetDataPercentScanned(planets[i]);
            Vector3 pos = PlayerPrefsManager.LoadPlanetPosition(planets[i]);
            if (pos != new Vector3())
            {
                planets[i].transform.position = pos;
            }
        }
    }

    private void LoadPlayerResources()
    {
        PlayerResources.AddResources(PlayerPrefsManager.LoadPlayerResources());
    }

    private void SaveCameraPosition()
    {
        PlayerPrefsManager.SaveCameraPosition(Camera.main);
    }

    private void SavePlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            PlayerPrefsManager.SavePlanetData(planets[i]);
            PlayerPrefsManager.SavePlanetPosition(planets[i]);
        }
    }

    private void SavePlayerResources()
    {
        PlayerPrefsManager.SavePlayerResources(PlayerResources.GetResources());
    }
}
