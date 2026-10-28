using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    void Awake()
    {
        // Check if an instance of this music player already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Delete the duplicate copy
            return;
        }

        // Set this object as the permanent master music player
        instance = this;

        // Crucial line: prevents the music from stopping when you switch levels/scenes
        DontDestroyOnLoad(gameObject);
    }
}

