using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("UI Panels")]
    [SerializeField]
    private GameObject quitPanel;
    public GameObject StartPanel;
    public GameObject SettingPanel;
    public GameObject SettingPanelGameplay;
    public GameObject GameOverPanel;
    public GameObject HelpPanel;



    [Header("Buttons")]
    [SerializeField]
    private GameObject pauseButton;
    [SerializeField]
    private GameObject resumeButton;
    [SerializeField]
    private GameObject MusicOffButton;
    [SerializeField]
    private GameObject SoundOffButton;




    public TextMeshProUGUI ScoreBoard;
    public TextMeshProUGUI messageUI;
    public Canvas mainCanvas;

    private bool isPaused = false;
    private int score = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        MusicOffButton.SetActive(false);
        SoundOffButton.SetActive(false);
        SoundManager.Instance.Tap();
        SoundManager.Instance.Background();
        Time.timeScale = 0f;
    }


    void Update()
    {
        UpdateScoreUI();
    }
    public void PauseResumeGame()

    {

        SoundManager.Instance.Tap();

        if (!isPaused)
            PauseGame();
        else
            ResumeGame();

    }

    public void PauseGame()
    {
        // Pause the game (do not flip user sound settings)
        Time.timeScale = 0f;

        // show resume, hide pause
        if (resumeButton != null) resumeButton.SetActive(true);
        if (pauseButton != null) pauseButton.SetActive(false);

        // Temporarily pause audio
        SoundManager.Instance.PauseAllAudioTemporarily();

        isPaused = true;
    }

    public void ResumeGame()
    {
        // Resume the game
        Time.timeScale = 1f;

        // show pause, hide resume
        if (resumeButton != null) resumeButton.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);

        // Resume audio without changing user preferences
        SoundManager.Instance.ResumeAllAudioTemporarily();

        isPaused = false;
    }
    public void StartGame()
    {
        SoundManager.Instance.Tap();


        if (SoundManager.Instance.IsMusicOn())
        {
            SoundManager.Instance.FishSwim();
        }

        if (StartPanel != null)
            StartPanel.SetActive(false);

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        // When gameplay starts, show the pause button and hide resume
        if (resumeButton != null)
            resumeButton.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(true);
    }
    public void Retry()
    {
        SoundManager.Instance.Tap();
        if (GameOverPanel.activeSelf)
        {
            GameOverPanel.SetActive(false);
        }
        // Reset state
        Time.timeScale = 1f;
        isPaused = false;

        // Reset score
        score = 0;

        // Ensure correct buttons
        if (resumeButton != null) resumeButton.SetActive(false);
        if (pauseButton != null) pauseButton.SetActive(true);

        // Resume background/music if user wants it
        if (SoundManager.Instance.IsMusicOn())
        {
            SoundManager.Instance.FishSwim();
        }

        // Respawn player fish (fishSpawn now destroys previous fish)
        if (FishController.instance != null)
        {
            FishController.instance.fishSpawn();
        }
        FindObjectOfType<BombSpawner>().SpawnNextBomb();

    }
    public void Home()
    {
        SoundManager.Instance.Tap();

        Time.timeScale = 0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ShowGameOver()
    {




        if (SoundManager.Instance.IsMusicOn())
        {
            SoundManager.Instance.Background();
        }
        // play background loop


        if (GameOverPanel != null)
            GameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

        if (resumeButton != null)
            resumeButton.SetActive(false);

        if (pauseButton != null)
            pauseButton.SetActive(false);
    }
    public void SettingPanelOpen()
    {
        SoundManager.Instance.Tap();
        if (StartPanel.activeSelf)
        {
            StartPanel.SetActive(false);
        }
        SettingPanel.SetActive(true);
    }
    public void SettingPanelGameplayOpen()
    {
        SoundManager.Instance.Tap();
        Time.timeScale = 0f;
        isPaused = true;
        if (StartPanel.activeSelf)
        {
            StartPanel.SetActive(false);
        }
        SettingPanelGameplay.SetActive(true);
    }
    public void SettingBack()
    {
        SoundManager.Instance.Tap();
        if (SettingPanel.activeSelf)
        {
            SettingPanel.SetActive(false);
        }
        StartPanel.SetActive(true);
    }
    public void SettingBackGameplay()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SoundManager.Instance.Tap();
        if (SettingPanelGameplay.activeSelf)
        {
            SettingPanelGameplay.SetActive(false);
        }

    }
    public void Help()
    {
        HelpPanel.SetActive(true);
    }
    public void HelpBack()
    {
        HelpPanel.SetActive(false);
    }

    public void QuitPanel()
    {
        SoundManager.Instance.Tap();
        Time.timeScale = 0f;
        quitPanel.SetActive(true);
    }
    public void CancelQuit()
    {
        SoundManager.Instance.Tap();
        Time.timeScale = 1f;
        // Close quit panel and resume game explicitly (avoid toggling)

        quitPanel.SetActive(false);
    }
    public void QuitGame()
    {
        SoundManager.Instance.Tap();
        quitPanel.SetActive(false);
        //SoundManager.Instance.Tap();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    private void UpdateScoreUI()
    {
        ScoreBoard.text = score.ToString();

    }
    public void ToggleMusic()
    {
        SoundManager.Instance.Tap();

        SoundManager.Instance.ToggleMusic();


        MusicOffButton.SetActive(SoundManager.Instance.IsMusicOff());
    }

    public void ToggleSound()
    {
        SoundManager.Instance.Tap();

        SoundManager.Instance.ToggleSound();


        SoundOffButton.SetActive(SoundManager.Instance.IsSoundOff());
    }


    public void AddScore(int value)
    {
        score += value;

    }
    public int GetScore()
    {
        return score;
    }
    public void ShowScorePopup(string text, Vector3 worldPos)
    {

        TextMeshProUGUI popup = Instantiate(messageUI, mainCanvas.transform);

        popup.text = text;


        popup.transform.position = worldPos + new Vector3(0, 0.5f, 0); // float slightly above

        StartCoroutine(FloatAndFadePopup(popup));
    }

    private IEnumerator FloatAndFadePopup(TextMeshProUGUI popup)
    {
        float duration = 2f;
        float elapsed = 0f;
        Color originalColor = popup.color;
        Vector3 startPos = popup.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 1f, 0); // drift upward

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;

            // Fade out
            float alpha = Mathf.Lerp(1f, 0f, progress);
            popup.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);


            popup.transform.position = Vector3.Lerp(startPos, endPos, progress);

            yield return null;
        }

        Destroy(popup.gameObject);
    }

}
