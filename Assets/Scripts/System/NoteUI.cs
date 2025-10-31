using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteUI : MonoBehaviour
{
    public static NoteUI Instance;
    public bool IsNoteOpen => notePanel != null && notePanel.activeSelf;

    [Header("UI Elements")]
    public GameObject notePanel;
    public TextMeshProUGUI noteTextField;
    public Image noteImageField;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        notePanel.SetActive(false);
    }

    public void OpenNote(string text, Sprite image)
    {
        notePanel.SetActive(true);
        Time.timeScale = 0f;

        var player = FindAnyObjectByType<PlayerController>();
        if (player != null)
            player.enabled = false;

        if (noteTextField)
        {
            noteTextField.gameObject.SetActive(!string.IsNullOrEmpty(text));
            noteTextField.text = text;
        }

        if (noteImageField)
        {
            noteImageField.gameObject.SetActive(image != null);
            noteImageField.sprite = image;
        }
    }

    public void CloseNote()
    {
        notePanel.SetActive(false);
        Time.timeScale = 1f;

        var player = FindAnyObjectByType<PlayerController>();
        if (player != null)
            player.enabled = true;
    }
}
