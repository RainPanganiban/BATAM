using TMPro;
using UnityEngine;

public class KeypadUI : MonoBehaviour
{
    public static KeypadUI Instance;

    [Header("UI References")]
    public GameObject keypadPanel;
    public TMP_Text codeDisplay;
    public GameObject crosshair;

    private string enteredCode = "";
    private CombinationDoor currentDoor;

    public bool IsActive => keypadPanel != null && keypadPanel.activeSelf;

    private void Awake()
    {
        Instance = this;
        HideKeypad();
    }

    public void ShowKeypad(CombinationDoor door)
    {
        currentDoor = door;
        enteredCode = "";
        codeDisplay.text = "";
        keypadPanel.SetActive(true);
        Time.timeScale = 0f; // pause game if desired

        if (crosshair != null)
            crosshair.SetActive(false);

        Debug.Log("Keypad UI shown for " + door.doorID);
    }

    public void HideKeypad()
    {
        keypadPanel.SetActive(false);

        if (crosshair != null)
            crosshair.SetActive(true);

        Time.timeScale = 1f;
    }

    public void PressNumber(string num)
    {
        Debug.Log($"Pressed number: {num}");

        if (enteredCode.Length < 4)
        {
            enteredCode += num;
            codeDisplay.text = enteredCode;
        }
    }

    public void PressClear()
    {
        enteredCode = "";
        codeDisplay.text = "";
    }

    public void PressEnter()
    {
        if (currentDoor != null)
        {
            currentDoor.TryUnlock(enteredCode);
        }
        HideKeypad();
    }
}
