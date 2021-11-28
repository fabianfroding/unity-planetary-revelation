using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> planets;

    private void Start()
    {
        Load();
        UIManager.Instance.ResetPlanetUIData(); // Hide default text.
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Load()
    {
        //PlayerPrefs.DeleteAll();
        LoadCameraPosition();
        LoadPlanets();
    }

    private void Save()
    {
        SaveCameraPosition();
        SavePlanets();
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
}
