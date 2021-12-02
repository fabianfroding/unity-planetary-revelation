using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public GameObject playerObjectToPlace;

    [SerializeField] private string defaultCursorTexturePath = "Textures/CursorDefault";

    private Texture2D defaultCursorTexture;
    private Texture2D activeCursorTexture;

    private static PlayerCursor instance;
    public static PlayerCursor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindGameObjectWithTag(EditorConstants.TAG_PLAYER_CONTROLS).GetComponent<PlayerCursor>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        defaultCursorTexture = Resources.Load<Texture2D>(defaultCursorTexturePath);
        activeCursorTexture = defaultCursorTexture;
        SetCursorTexture(defaultCursorTexture);
    }

    public bool IsCursorDefault()
    {
        return activeCursorTexture == defaultCursorTexture;
    }

    public void SetActivePlacementCursor(Texture2D texture, GameObject playerObjectToPlace)
    {
        activeCursorTexture = texture;
        SetCursorTexture(activeCursorTexture);
        this.playerObjectToPlace = playerObjectToPlace;
    }

    public void ResetCursor()
    {
        activeCursorTexture = defaultCursorTexture;
        SetCursorTexture(activeCursorTexture);
        playerObjectToPlace = null;
    }

    private void SetCursorTexture(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }
}
