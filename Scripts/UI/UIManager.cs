using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text planetsScannedText;
    [SerializeField] private Text resourcesText;
    [SerializeField] private Text planetText;
    [SerializeField] private Text percentScannedText;
    [SerializeField] private Text distanceText;

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag(EditorConstants.TAG_UI_MANAGER).GetComponent<UIManager>();
            }
            return instance;
        }
    }

    public void UpdatePlanetUIData(PlanetUIData planetUIData)
    {
        instance.UpdatePlanetUIDataTooltip(planetUIData);
    }

    public void UpdatePlanetsScannedText(int planetsScanned, int totalNumPlanets)
    {
        planetsScannedText.text = "Planets scanned: " + planetsScanned + "/" + totalNumPlanets;
    }

    public void UpdateResourcesText()
    {
        resourcesText.text = "Resources: " + PlayerResources.GetResources();
    }

    public void ResetPlanetUIData()
    {
        PlanetUIData planetUIData = new PlanetUIData()
        {
            nameText = "",
            distanceText = ""
        };
        instance.UpdatePlanetUIDataTooltip(planetUIData);
    }

    private void UpdatePlanetUIDataTooltip(PlanetUIData planetUIData)
    {
        planetText.GetComponent<Text>().text = planetUIData.nameText;
        distanceText.GetComponent<Text>().text = planetUIData.distanceText;
        percentScannedText.GetComponent<Text>().text = planetUIData.percentScannedText;
    }
}
