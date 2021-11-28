using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject radarDroidPrefab;

    private int resources = 20000;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (resources > 50)
            {
                resources -= 50;
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.CompareTag(EditorConstants.TAG_PLANE))
                    {
                        GameObject radarDroid = GameObject.Instantiate(radarDroidPrefab);
                        radarDroid.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    }
                }
            }
        }
    }
}
