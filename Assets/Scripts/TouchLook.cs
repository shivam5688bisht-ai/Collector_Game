using UnityEngine;

public class TouchLook : MonoBehaviour
{
    [Header("Settings")]
    public Transform cameraTransform;
    public float sensitivity = 0.2f;
    public float minimumX = -60f;
    public float maximumX = 60f;

    private float xRotation = 0f;
    
    // 🛑 NEW: Track if the game has been won
    private bool hasWon = false;

    void Start()
    {
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        
        if (cameraTransform != null)
        {
            xRotation = cameraTransform.localRotation.eulerAngles.x;
            if (xRotation > 180) xRotation -= 360f;
        }
    }

    void Update()
    {
        // 🛑 NEW: Stop tracking input if the player won
        if (hasWon) return;

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x < Screen.width / 2)
                    {
                        continue; 
                    }
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    float mouseProfileY = touch.deltaPosition.y * sensitivity;
                    xRotation -= mouseProfileY; 
                    xRotation = Mathf.Clamp(xRotation, minimumX, maximumX);

                    if (cameraTransform != null)
                    {
                        cameraTransform.localRotation = Quaternion.Euler(xRotation, cameraTransform.localRotation.eulerAngles.y, 0f);
                    }
                }
            }
        }
    }

    // 🛑 NEW: Public function for ScoreManager to trigger
    public void StopLooking()
    {
        hasWon = true;
    }
}
