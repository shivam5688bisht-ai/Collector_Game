using UnityEngine;
using UnityEngine.SceneManagement; // 🔄 Required to switch scenes

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // 🎮 This loads the scene right next to it in your Build Settings (Index 1)
        SceneManager.LoadScene(1); 
    }
}

