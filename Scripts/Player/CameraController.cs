using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float scrollSensitivity = 2000f;
    [SerializeField] private float maxZoom = 4800f;
    [SerializeField] private float minZoom = 500;
    private bool rightMouseButtonDown = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            rightMouseButtonDown = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            rightMouseButtonDown = false;
        }

        if (rightMouseButtonDown && (Input.GetAxis(EditorConstants.INPUT_MOUSE_X) != 0 || Input.GetAxis(EditorConstants.INPUT_MOUSE_Y) != 0))
        {
            Camera.main.transform.position = new Vector3(
                Camera.main.transform.position.x + -Input.GetAxis(EditorConstants.INPUT_MOUSE_X) * (10 + Camera.main.transform.position.y / 40),
                Camera.main.transform.position.y, 
                Camera.main.transform.position.z + -Input.GetAxis(EditorConstants.INPUT_MOUSE_Y) * (10 + Camera.main.transform.position.y / 40));
        }

        CheckCameraScrollZoom();
    }

    private void CheckCameraScrollZoom()
    {
        if (Input.GetAxis(EditorConstants.INPUT_MOUSE_SCROLL_WHEEL) != 0)
        {
            Vector3 camPos = Camera.main.transform.position;
            float zoom = camPos.y;
            Camera.main.transform.position = new Vector3(
                camPos.x, 
                Mathf.Clamp(zoom + Input.GetAxis(EditorConstants.INPUT_MOUSE_SCROLL_WHEEL) * -scrollSensitivity, minZoom, maxZoom), 
                camPos.z);
        }
    }
}
