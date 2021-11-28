using UnityEngine;

public class PlayerPrefsManager
{
    private static readonly string DB_KEY_CAMERA_POS_X = "CamPosX";
    private static readonly string DB_KEY_CAMERA_POS_Y = "CamPosY";
    private static readonly string DB_KEY_CAMERA_POS_Z = "CamPosZ";
    private static readonly string DB_KEY_PLANET_PERCENT_SCANNED = "PercentScanned";
    private static readonly string DB_KEY_PLANET_POS_X = "PlanetPosX";
    private static readonly string DB_KEY_PLANET_POS_Y = "PlanetPosY";
    private static readonly string DB_KEY_PLANET_POS_Z = "PlanetPosZ";

    public static void SaveCameraPosition(Camera camera)
    {
        PlayerPrefs.SetFloat(DB_KEY_CAMERA_POS_X, camera.transform.position.x);
        PlayerPrefs.SetFloat(DB_KEY_CAMERA_POS_Y, camera.transform.position.y);
        PlayerPrefs.SetFloat(DB_KEY_CAMERA_POS_Z, camera.transform.position.z);
    }

    public static void SavePlanetData(GameObject planet)
    {
        PlayerPrefs.SetFloat(planet.name + DB_KEY_PLANET_PERCENT_SCANNED, planet.GetComponent<PlanetScript>().planetData.percentScanned);
    }

    public static void SavePlanetPosition(GameObject planet)
    {
        PlayerPrefs.SetFloat(planet.name + DB_KEY_PLANET_POS_X, planet.transform.position.x);
        PlayerPrefs.SetFloat(planet.name + DB_KEY_PLANET_POS_Y, planet.transform.position.y);
        PlayerPrefs.SetFloat(planet.name + DB_KEY_PLANET_POS_Z, planet.transform.position.z);
    }

    public static Vector3 LoadCameraPosition()
    {
        if (PlayerPrefs.HasKey(DB_KEY_CAMERA_POS_X) && 
            PlayerPrefs.HasKey(DB_KEY_CAMERA_POS_Y) && 
            PlayerPrefs.HasKey(DB_KEY_CAMERA_POS_Z))
        {
            return new Vector3(
                PlayerPrefs.GetFloat(DB_KEY_CAMERA_POS_X),
                PlayerPrefs.GetFloat(DB_KEY_CAMERA_POS_Y),
                PlayerPrefs.GetFloat(DB_KEY_CAMERA_POS_Z));
        }
        return new Vector3();
    }

    public static float LoadPlanetDataPercentScanned(GameObject planet)
    {
        if (PlayerPrefs.HasKey(planet.name + DB_KEY_PLANET_PERCENT_SCANNED))
        {
            return PlayerPrefs.GetFloat(planet.name + DB_KEY_PLANET_PERCENT_SCANNED);
        }
        return 0;
    }

    public static Vector3 LoadPlanetPosition(GameObject planet)
    {
        if (PlayerPrefs.HasKey(planet.name + DB_KEY_PLANET_POS_X) &&
            PlayerPrefs.HasKey(planet.name + DB_KEY_PLANET_POS_Y) &&
            PlayerPrefs.HasKey(planet.name + DB_KEY_PLANET_POS_Z))
        {
            return new Vector3(
                PlayerPrefs.GetFloat(planet.name + DB_KEY_PLANET_POS_X),
                PlayerPrefs.GetFloat(planet.name + DB_KEY_PLANET_POS_Y),
                PlayerPrefs.GetFloat(planet.name + DB_KEY_PLANET_POS_Z));
        }
        return new Vector3();
        
    }
}
