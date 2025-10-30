using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WheelController : MonoBehaviour
{
    [Header("Wheel")]
    public int current = 0;                 // 0..9
    public TMP_Text display;
    public AudioSource tickSound;           // optional sound

    private void Start()
    {
        UpdateDisplay();
    }

    public void Increment()
    {
        current = (current + 1) % 10;
        UpdateDisplay();
        tickSound?.Play();
    }

    public void Decrement()
    {
        current = (current + 9) % 10;
        UpdateDisplay();
        tickSound?.Play();
    }

    public void SetDigit(int d)
    {
        current = Mathf.Clamp(d, 0, 9);
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (display != null)
            display.text = current.ToString();
    }

    // Optional if you add a Scroll handler (requires implementing IPointerEnter/Exit to enable)
    public void OnScroll(PointerEventData eventData)
    {
        if (eventData.scrollDelta.y > 0) Increment();
        else if (eventData.scrollDelta.y < 0) Decrement();
    }
}
