using TMPro;
using UnityEngine;

public class KeypadUI : MonoBehaviour
{
    public static KeypadUI Instance;

    [Header("UI References")]
    public GameObject keypadPanel;
    public TMP_Text codeDisplay;

    private string enteredCode = "";
    private CombinationDoor currentDoor;

    private void Awake()
    {
        Instance = this;
        keypadPanel.SetActive(false);
    }

    public void ShowKeypad(CombinationDoor door)
    {
        currentDoor = door;
        enteredCode = "";
        codeDisplay.text = "";
        keypadPanel.SetActive(true);
        Time.timeScale = 0f; // pause game if desired
    }

    public void HideKeypad()
    {
        keypadPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PressNumber(string num)
    {
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
