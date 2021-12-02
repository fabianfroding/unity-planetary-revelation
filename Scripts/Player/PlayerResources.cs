using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    private static int resources = 0;

    public static int GetResources()
    {
        return resources;
    }

    public static void AddResources(int amount)
    {
        resources += amount;
        if (resources < 0)
        {
            resources = 0;
        }
        UIManager.Instance.UpdateResourcesText();
    }
}
