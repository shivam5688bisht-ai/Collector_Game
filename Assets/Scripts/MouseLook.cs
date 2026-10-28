using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {Cursor.lockState = CursorLockMode.Locked;
    
    // 2. Makes the cursor invisible so it doesn't distract you
    Cursor.visible = false;
    }

void LateUpdate()
{
    // GetAxisRaw gets the pure, raw data from your mouse sensor without smoothing
    float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
    float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    playerBody.Rotate(Vector3.up * mouseX);
    }

}    


