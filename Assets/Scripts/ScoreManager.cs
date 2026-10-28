using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public TextMeshProUGUI scoreText;

    public AudioSource audioSource;
    public AudioClip coinSound;

    public int scoreToWin = 5; 
    public GameObject restartButton; 

    // 🔗 ADD THIS LINE AT THE TOP:
    public TouchLook touchLookScript; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Total orbs collected: " + score + "/5";
        }

        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        
        if (scoreText != null)
        {
            scoreText.text = "Total orbs collected: " + score + "/5";
        }

        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }

        if (score >= scoreToWin)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        if (scoreText != null)
        {
            scoreText.text = "You Win!";
        }

        if (restartButton != null)
        {
            restartButton.SetActive(true); 
        }

        // 🛑 CHANGE THIS BLOCK AT THE BOTTOM:
        if (touchLookScript != null)
        {
            touchLookScript.StopLooking();
        }
    }

    public void ClickRestart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
