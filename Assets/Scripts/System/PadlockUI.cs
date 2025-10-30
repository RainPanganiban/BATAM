using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadlockUI : MonoBehaviour
{
    public static PadlockUI Instance;

    [Header("UI refs")]
    public GameObject padlockPanel;         // the parent panel (enable/disable)
    public WheelController[] wheels;        // assign 4 wheel controllers in order
    public TMP_Text feedbackText;           // "Incorrect code" / "Unlocked"
    public GameObject crosshair;            // optional
    public bool hideCrosshairOnOpen = true;

    private CombinationDoor currentDoor;
    private PlayerInput playerInput;

    private void Awake()
    {
        Instance = this;
        playerInput = FindObjectOfType<PlayerInput>();
        if (padlockPanel != null) padlockPanel.SetActive(false);
    }

    public void ShowPadlock(CombinationDoor door)
    {
        currentDoor = door;
        // reset wheels if you want:
        for (int i = 0; i < wheels.Length; i++)
            wheels[i].SetDigit(0);

        feedbackText.text = "";
        padlockPanel.SetActive(true);

        if (hideCrosshairOnOpen && crosshair != null)
            crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;

        if (playerInput != null)
            playerInput.DeactivateInput();
    }

    public void HidePadlock()
    {
        padlockPanel.SetActive(false);

        if (hideCrosshairOnOpen && crosshair != null)
            crosshair.SetActive(true);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;

        if (playerInput != null)
            playerInput.ActivateInput();
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
    }
}
