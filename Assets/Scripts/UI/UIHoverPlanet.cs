using TMPro;
using UnityEngine;

public class UIHoverPlanet : UIBase
{
    [SerializeField] private TextMeshProUGUI hoverText;

    private static UIHoverPlanet instance;
    public static UIHoverPlanet Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = FindFirstObjectByType<UIHoverPlanet>();
            }
            return instance;
        }
    }

    public void ShowUI(PlanetData planetData)
    {
        if (!UIPlanetDetails.Instance.IsActive() && !UIManager.Instance.ExitMenu.gameObject.activeSelf)
        {
            transform.position = Input.mousePosition;
            hoverText.text = planetData.planetName;
        }
        else
        {
            HideUI();
        }
    }

    public void HideUI()
    {
        hoverText.text = "";
    }
}
