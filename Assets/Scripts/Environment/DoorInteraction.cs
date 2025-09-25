using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [Header("Scene")]
    public string sceneToLoad;

    public void Interact()
    {
        if (ScreenFader.Instance != null)
        {
            ScreenFader.Instance.FadeOut(() =>
            {
                SceneManager.LoadScene(sceneToLoad);
            });
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public string GetPromptText()
    {
        return "Press E to Enter";
    }

}
