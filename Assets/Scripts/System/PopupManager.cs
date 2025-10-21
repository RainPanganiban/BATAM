using TMPro;
using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    [Header("Popup UI")]
    public GameObject popupPanel;
    public TMP_Text popupText;
    public float defaultDuration = 2f;

    private Coroutine currentPopup;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    public void ShowMessage(string message, float duration = -1f)
    {
        if (popupText == null || popupPanel == null)
        {
            Debug.LogWarning("Popup UI references missing!");
            return;
        }

        if (duration <= 0f)
            duration = defaultDuration;

        if (currentPopup != null)
            StopCoroutine(currentPopup);

        currentPopup = StartCoroutine(ShowPopupCoroutine(message, duration));
    }

    private IEnumerator ShowPopupCoroutine(string message, float duration)
    {
        popupText.text = message;
        popupPanel.SetActive(true);

        yield return new WaitForSeconds(duration);

        popupPanel.SetActive(false);
        currentPopup = null;
    }
}
