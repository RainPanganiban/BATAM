using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

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
        // Disable UI at start
        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);

        // Make sure the game isn't frozen
        Time.timeScale = 1f;
        isPaused = false;
        isGameOver = false;

        // Cursor state
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator Start()
    {
        // Wait a frame so all other scripts (PlayerController, etc.) finish initializing
        yield return null;

        // Ensure player references exist
        if (playerController == null)
            playerController = FindObjectOfType<PlayerController>();
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();

        // Force-enable movement
        if (playerController != null)
        {
            playerController.enabled = true;
            Debug.Log("PlayerController enabled at start");
        }

        // Confirm time is normal
        Time.timeScale = 1f;
    }

    private void Update()
    {
        // Only allow pausing if not in game over
        if (!isGameOver && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Pause game
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (pauseMenu != null) pauseMenu.SetActive(true);

            // Disable player input/movement
            if (playerController != null)
                playerController.enabled = false;
            if (playerInput != null)
                playerInput.enabled = false;
        }
        else
        {
            // Resume game
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (pauseMenu != null) pauseMenu.SetActive(false);

            // Re-enable player input/movement
            if (playerController != null)
                playerController.enabled = true;
            if (playerInput != null)
                playerInput.enabled = true;
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

        // Disable player completely
        if (playerController != null)
            playerController.enabled = false;
        if (playerInput != null)
            playerInput.enabled = false;
    }

    // Button functions
    public void OnResumeButton()
    {
        if (isPaused) TogglePause();
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
