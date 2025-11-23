using TMPro;
using UnityEngine;

public class UIPlanetsDiscovered : UIBase
{
    private TextMeshProUGUI planetDisplay;
    
    private void Awake()
    {
        planetDisplay = GetComponent<TextMeshProUGUI>();
        SolarSystem.OnPlanetDiscovered += HandlePlanetDiscovered;
    }

    private void HandlePlanetDiscovered(int numPlanetsDiscovered)
    {
        planetDisplay.SetText("Planets Discovered: " + numPlanetsDiscovered + "/8");
    }
}
