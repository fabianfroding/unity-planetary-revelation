using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lookSpeedH = 2f;
    [SerializeField] private float lookSpeedV = 2f;
    [SerializeField] private float zoomSpeed = 200f;
    [SerializeField] private float dragSpeed = 60000f;

    [SerializeField] private GameObject sun;
    [SerializeField] private float maxDistanceFromSun = 3250f;

    private float yaw = 0f;
    private float pitch = 0f;
    private GameObject focusedObject;

    private static CameraController instance;
    public static CameraController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<CameraController>();
            }
            return instance;
        }
    }

    public void UpdateRotation()
    {
        yaw = transform.rotation.eulerAngles.y;
        pitch = transform.rotation.eulerAngles.x;
    }

    public void SetFocusedObjectPos(GameObject obj)
    {
        focusedObject = obj;
    }

    private void Start()
    {
        UpdateRotation();
    }

    private void Update()
    {
        if (focusedObject == null)
        {
            HandleInput();
        }
        else
        {
            FollowFocusedObject();
            UpdateRotation();
        }
    }

    private void HandleInput()
    {
        // Rotate
        if (Input.GetMouseButton(0))
        {
            yaw += lookSpeedH * Input.GetAxis(EditorConstants.INPUT_MOUSE_X);
            pitch -= lookSpeedV * Input.GetAxis(EditorConstants.INPUT_MOUSE_Y);

            /*if (pitch < 0f) pitch = 0f;
            if (pitch > 90f) pitch = 90f;*/
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }

        // Drag
        if (Input.GetMouseButton(1))
        {
            transform.Translate(-Input.GetAxisRaw(EditorConstants.INPUT_MOUSE_X) * Time.deltaTime * dragSpeed, -Input.GetAxisRaw(EditorConstants.INPUT_MOUSE_Y) * Time.deltaTime * dragSpeed, 0);
            SetCamMaxY();
            ClampDistanceFromSun(); 
        }

        // Face sun
        if (Input.GetMouseButton(2))
        {
            transform.LookAt(GameObject.Find(EditorConstants.GAME_OBJECT_NAME_SUN).transform);
            UpdateRotation();
        }

        // Zoom
        float scrollInput = Input.GetAxis(EditorConstants.INPUT_MOUSE_SCROLL_WHEEL);
        if (scrollInput != 0) {
            transform.Translate(0, 0, scrollInput * zoomSpeed, Space.Self);
            SetCamMaxY();
            ClampDistanceFromSun();
        }
        
    }

    private void SetCamMaxY()
    {
        if (transform.position.y < -100)
        {
            transform.position = new Vector3(transform.position.x, -100, transform.position.z);
        }
        if (transform.position.y > 2000)
        {
            transform.position = new Vector3(transform.position.x, 2000, transform.position.z);
        }
    }

    private void FollowFocusedObject()
    {
        transform.position = focusedObject.transform.position;
        transform.rotation = focusedObject.transform.rotation;
    }
    
    private void ClampDistanceFromSun()
    {
        if (sun == null) return;

        Vector3 direction = transform.position - sun.transform.position;
        float distance = direction.magnitude;

        if (distance > maxDistanceFromSun)
        {
            direction = direction.normalized * maxDistanceFromSun;
            transform.position = sun.transform.position + direction;
        }
    }
}
