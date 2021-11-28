using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public PlanetData planetData;

    public void Scan()
    {
        if (planetData.percentScanned < 100)
        {
            float newPercentScanned = planetData.percentScanned + planetData.percentScannedPerSec;
            if (newPercentScanned >= 100)
            {
                Discover();
                planetData.percentScanned = 100;
            }
            else
            {
                planetData.percentScanned += planetData.percentScannedPerSec;
            }
        }
    }

    private void Discover()
    {
        AudioManager.Instance.PlayPlanetScannedSound(gameObject);
        // TODO: Emit event to increment planet scanned counter.
    }

    private void OnMouseOver()
    {
        PlanetUIData planetUIData = new PlanetUIData
        {
            nameText = planetData.percentScanned >= 100 ? gameObject.name : "Unknown Planet",
            distanceText = "Distance: " + (int)Vector3.Distance(GameObject.Find(EditorConstants.GAME_OBJECT_NAME_SUN).transform.position, transform.position),
            percentScannedText = "Scanned: " + (int)planetData.percentScanned + "%",
        };
        UIManager.Instance.UpdatePlanetUIData(planetUIData);
    }

    private void OnMouseExit()
    {
        UIManager.Instance.ResetPlanetUIData();
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + " clicked.");
    }
}
