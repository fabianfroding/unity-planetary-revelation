using System.Collections;
using TMPro;
using UnityEngine;

public class UIPlanetDetails : MonoBehaviour
{
    [SerializeField] private float bgRectWidth;
    [SerializeField] private float bgRectHeight;

    [SerializeField] private GameObject bgGO;
    [SerializeField] private GameObject planetNameTextGO;
    [SerializeField] private GameObject planetSizeTextGO;
    [SerializeField] private GameObject planetDistanceTextGO;
    [SerializeField] private GameObject planetDetailsTextGO;
    [SerializeField] private GameObject closeBTNGO;

    [SerializeField] private AudioSource menuOpenSound;
    [SerializeField] private AudioSource menuCloseSound;

    private RectTransform bgGoRect;
    private bool isActive = false;

    private static UIPlanetDetails instance;
    public static UIPlanetDetails Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIPlanetDetails>();
            }
            return instance;
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void ShowUI(PlanetData planetData)
    {
        if (!isActive)
        {
            isActive = true;
            bgGO.SetActive(true);
            bgGoRect = bgGO.GetComponent<RectTransform>();
            bgGoRect.sizeDelta = new Vector2(1f, 7.5f);
            menuOpenSound.Play();
            StartCoroutine(ExpandBG(planetData));
        }
    }

    public void HideUI()
    {
        ShowUIContents(false);
        CameraController.Instance.UpdateRotation();
        CameraController.Instance.SetFocusedObjectPos(null);
        menuCloseSound.Play();
        StartCoroutine(CollapseBG());
    }

    private IEnumerator ExpandBG(PlanetData planetData)
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);
            if (bgGoRect.sizeDelta.x < bgRectWidth)
            {
                bgGoRect.sizeDelta = new Vector2(bgGoRect.sizeDelta.x + 250f, bgGoRect.sizeDelta.y);
            }
            else if (bgGoRect.sizeDelta.y < bgRectHeight)
            {
                bgGoRect.sizeDelta = new Vector2(bgGoRect.sizeDelta.x, bgGoRect.sizeDelta.y + 125f);
            }
            else
            {
                bgGoRect.sizeDelta = new Vector2(bgRectWidth, bgRectHeight);
                ShowUIContents(true);
                SetUIContents(planetData);
                break;
            }
        }
    }

    private IEnumerator CollapseBG()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.04f);
            if (bgGoRect.sizeDelta.y >= 7.5f)
            {
                bgGoRect.sizeDelta = new Vector2(bgGoRect.sizeDelta.x, bgGoRect.sizeDelta.y - 125f);
            }
            else if (bgGoRect.sizeDelta.x >= 1f)
            {
                bgGoRect.sizeDelta = new Vector2(bgGoRect.sizeDelta.x - 125f, bgGoRect.sizeDelta.y);
            }
            else
            {
                isActive = false;
                bgGO.SetActive(false);
                break;
            }
        }
    }

    private void ShowUIContents(bool val)
    {
        planetNameTextGO.SetActive(val);
        planetSizeTextGO.SetActive(val);
        planetDistanceTextGO.SetActive(val);
        planetDetailsTextGO.SetActive(val);
        closeBTNGO.SetActive(val);
    }

    private void SetUIContents(PlanetData planetData)
    {
        planetNameTextGO.GetComponent<TextMeshProUGUI>().text = "Planet Name: " + planetData.planetName;
        planetSizeTextGO.GetComponent<TextMeshProUGUI>().text = "Size (Diameter): " + planetData.size.ToString() + "km";
        planetDistanceTextGO.GetComponent<TextMeshProUGUI>().text = 
            "Distance from sun: " + 
            (planetData.distanceToSun <= 0 ? "Unknown" : planetData.distanceToSun.ToString() + "km");
        planetDetailsTextGO.GetComponent<TextMeshProUGUI>().text = planetData.description;
    }
}
