using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIExitMenu exitMenu;
    [SerializeField] private UIPlanetsDiscovered uiPlanetsDiscovered;
    
    public UIExitMenu ExitMenu => exitMenu;
    public UIPlanetsDiscovered UIPlanetsDiscovered => uiPlanetsDiscovered;
    
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<UIManager>();
            }
            return instance;
        }
    }
    
    private void Update()
    {
        if (!exitMenu)
            return;

        if (exitMenu.gameObject.activeSelf)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.End))
        {
            exitMenu.gameObject.SetActive(true);
        }
    }
}
