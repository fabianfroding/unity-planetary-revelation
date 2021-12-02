using UnityEngine;

public class BTNUnitPlacement : MonoBehaviour
{
    [SerializeField] private GameObject playerObjectToPlacePrefab;
    [SerializeField] private string iconTexturePath;

    public void Click()
    {
        PlayerCursor.Instance.SetActivePlacementCursor(Resources.Load<Texture2D>(iconTexturePath), playerObjectToPlacePrefab);
        AudioManager.Instance.PlayerCursorClickSound();
    }
}
