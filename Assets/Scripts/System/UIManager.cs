using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider sanitySlider;
    public Slider staminaSlider;

    void Start()
    {
        sanitySlider.maxValue = GameManager.Instance.maxSanity;
        staminaSlider.maxValue = GameManager.Instance.maxStamina;
    }

    void Update()
    {
        sanitySlider.value = GameManager.Instance.currentSanity;
        staminaSlider.value = GameManager.Instance.currentStamina;
    }
}
