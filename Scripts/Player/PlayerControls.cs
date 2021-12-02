using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && PlayerCursor.Instance.playerObjectToPlace != null)
        {
            PlayerObject playerObject = PlayerCursor.Instance.playerObjectToPlace.GetComponent<PlayerObject>();
            if (PlayerResources.GetResources() >= playerObject.GetResourceCost())
            {
                PlayerResources.AddResources(-playerObject.GetResourceCost());
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(EditorConstants.TAG_PLANE))
                    {
                        GameObject playerObjectToPlace = GameObject.Instantiate(playerObject.gameObject);
                        playerObjectToPlace.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        AudioManager.Instance.PlayPlayerObjectPlacementSound(playerObjectToPlace);
                    }
                }
                PlayerCursor.Instance.ResetCursor();
            }
            else
            {
                Debug.Log("Insufficient resources to build " + playerObject.gameObject.name + "(" + playerObject.GetResourceCost() + ")");
            }
        }
        else if (Input.GetMouseButton(1))
        {
            PlayerCursor.Instance.ResetCursor();
        }
    }
}
