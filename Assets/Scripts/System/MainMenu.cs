using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Canvases")]
    public GameObject mainMenuCanvas;
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;

    void Start()
    {
        ShowMainMenu();
    }

    public void Play()
    {
        // Clean up any leftover managers from previous sessions
        if (GameManager.Instance != null)
            Destroy(GameManager.Instance.gameObject);
        if (TaskManager.Instance != null)
            Destroy(TaskManager.Instance.gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        mainMenuCanvas.SetActive(false);
        optionsCanvas.SetActive(true);
        creditsCanvas.SetActive(false);
    }

    public void Credits()
    {
        mainMenuCanvas.SetActive(false);
        creditsCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
    }

    public void BackToMainMenu()
    {
        ShowMainMenu();
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Application closed");
    }

    private void ShowMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        optionsCanvas.SetActive(false);
        creditsCanvas.SetActive(false);
    }
}