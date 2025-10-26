using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    [Header("Jumpscare Settings")]
    public AudioSource scareSound;       // Sound to play during jumpscare
    public Animator scareAnimator;       // Animator for the jumpscare object
    public string boolParameter = "isTriggered"; // Animator bool parameter name
    public bool oneTime = true;          // Should only trigger once?

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // Play the jumpscare sound
            if (scareSound != null)
                scareSound.Play();

            // Set the Animator boolean to true
            if (scareAnimator != null)
                scareAnimator.SetBool(boolParameter, true);

            // Prevent re-triggering if desired
            if (oneTime)
                hasTriggered = true;
        }
    }
}
