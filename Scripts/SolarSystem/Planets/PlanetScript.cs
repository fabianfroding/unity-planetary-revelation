using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    public PlanetData planetData;
    [SerializeField] private GameObject focusedCamPos;

    public GameObject GetFocusedCamPos()
    {
        return focusedCamPos;
    }

    private void Start()
    {
        planetData.planetName = gameObject.name;
    }

    private void OnMouseOver()
    {
        UIHoverPlanet.Instance.ShowUI(planetData);
    }

    private void OnMouseExit()
    {
        UIHoverPlanet.Instance.HideUI();
    }

    private void OnMouseDown()
    {
        planetData.size = GetSize();
        planetData.distanceToSun = GetDistanceToSun();
        UIPlanetDetails.Instance.ShowUI(planetData);

        if (focusedCamPos != null)
        {
            CameraController.Instance.SetFocusedObjectPos(focusedCamPos);
        }
    }

    private int GetSize()
    {
        return (int)transform.localScale.x * 1000;
    }

    private int GetDistanceToSun()
    {
        GameObject sun = GameObject.Find(EditorConstants.GAME_OBJECT_NAME_SUN);
        if (sun == null)
        {
            return 0;
        }
        return (int)Vector3.Distance(transform.position, sun.transform.position) * 1000000;
    }
}
