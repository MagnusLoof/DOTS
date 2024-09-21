using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }
    public Vector3 mousePosition;
    public Vector3 cameraPosition;

    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        // I know this code is awful, but I was having issues when using Viewport
        var localTransform = transform;
        cameraPosition = new Vector3(CameraMovementSystem.Instance.CameraPosition.x, CameraMovementSystem.Instance.CameraPosition.y, localTransform.position.z);
        localTransform.position = cameraPosition;
        cameraPosition.z = 0;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
