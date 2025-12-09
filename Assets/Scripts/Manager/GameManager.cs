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
    public GameObject GameOverPanel;



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

        {

            Time.timeScale = 0f;

            pauseButton.SetActive(true);

            resumeButton.SetActive(false);

            isPaused = true;


        }

        else

        {

            Time.timeScale = 1f;

            resumeButton.SetActive(true);

            pauseButton.SetActive(false);

            isPaused = false;


        }

    }
    public void StartGame()
    {
        SoundManager.Instance.Tap();
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.FishSwim();
        if (StartPanel != null)
            StartPanel.SetActive(false);

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        if (resumeButton != null)
            resumeButton.SetActive(true);

        if (pauseButton != null)
            pauseButton.SetActive(false);
    }
    public void Retry()
    {
        SoundManager.Instance.Tap();
        if (GameOverPanel.activeSelf)
        {
            GameOverPanel.SetActive(false);
        }
        Time.timeScale = 1f;
        SoundManager.Instance.StopMusic();
        SoundManager.Instance.FishSwim();
        FishController.instance.fishSpawn();

    }
    public void Home()
    {
        SoundManager.Instance.Tap();

        Time.timeScale = 0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void ShowGameOver()
    {
        SoundManager.Instance.StopMusic();       // stop fish swim
        SoundManager.Instance.Background();  // play background loop


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
    public void SettingBack()
    {
        SoundManager.Instance.Tap();
        if (SettingPanel.activeSelf)
        {
            SettingPanel.SetActive(false);
        }
        StartPanel.SetActive(true);
    }



    public void QuitPanel()
    {
        SoundManager.Instance.Tap();
        PauseResumeGame();
        quitPanel.SetActive(true);
    }
    public void CancelQuit()
    {
        SoundManager.Instance.Tap();

        PauseResumeGame();
        isPaused = false;
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
    public void SoundOnOff()
    {

    }
    public void MusicOnOff()
    {

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
