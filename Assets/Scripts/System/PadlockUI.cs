using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadlockUI : MonoBehaviour
{
    public static PadlockUI Instance;

    [Header("UI refs")]
    public GameObject padlockPanel;         // the parent panel (enable/disable)
    public WheelController[] wheels;        // assign 4 wheel controllers in order
    public TMP_Text feedbackText;           // "Incorrect code" / "Unlocked

    private CombinationDoor currentDoor;

    [Header("Player Control")]
    public PlayerController playerController;
    public PlayerInput playerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // optional — only if you want it to persist
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (padlockPanel != null)
            padlockPanel.SetActive(false);
    }

    public void ShowPadlock(CombinationDoor door)
    {
        currentDoor = door;
        // reset wheels if you want:
        for (int i = 0; i < wheels.Length; i++)
            wheels[i].SetDigit(0);

        feedbackText.text = "";
        padlockPanel.SetActive(true);

        if (playerInput != null)
            playerInput.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void HidePadlock()
    {
        padlockPanel.SetActive(false);

        if (playerInput != null)
            playerInput.enabled = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    // call from Unlock button
    public void TryUnlock()
    {
        string code = "";
        foreach (var w in wheels) code += w.current.ToString();

        Debug.Log("Padlock entered code: " + code);

        if (currentDoor != null)
        {
            if (code == currentDoor.correctCode)
            {
                feedbackText.text = "Unlocked!";
                currentDoor.TryUnlock(code);
                HidePadlock();
            }
            else
            {
                feedbackText.text = "Incorrect code";
                // optional: play shake animation / sound
            }
        }
        else
        {
            feedbackText.text = "No door target";
        }
    }

    public void PressExit()
    {
        HidePadlock();
        currentDoor = null;
    }
}
