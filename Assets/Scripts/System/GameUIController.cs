using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    public static GameUIController Instance;

    [Header("UI Panels")]
    public GameObject pauseMenu;
    public GameObject gameOverScreen;

    [Header("References")]
    public PlayerInput playerInput;
    public PlayerController playerController;

    private bool isPaused = false;
    private bool isGameOver = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        isGameOver = false;

        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);

        if (playerController != null)
            playerController.enabled = true;
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && !isGameOver)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (pauseMenu != null) pauseMenu.SetActive(true);
            if (playerController != null) playerController.enabled = false;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (pauseMenu != null) pauseMenu.SetActive(false);
            if (playerController != null) playerController.enabled = true;
        }
    }

    public void ShowGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;
    }

    // Button functions
    public void OnResumeButton()
    {
        TogglePause();
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
