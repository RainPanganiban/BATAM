using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [System.Serializable]
    public class HandItem
    {
        public string itemName;       // Name of the inventory item (e.g. "Lamp", "Rosary")
        public GameObject handObject; // The hand GameObject for that item
    }

    [Header("Hand References")]
    public List<HandItem> handItems = new List<HandItem>();

    [Header("Special Hands")]
    public GameObject defaultHand;   // Always shown when nothing is selected
    public GameObject prayingHand;   // Shown while praying

    [Header("Lighting and Environment Settings")]
    public GameObject playerLight;        // The player’s light source (e.g., flashlight or lamp light)
    public bool enableFogControl = true;  // Optional toggle for fog control
    [Range(0f, 0.1f)] public float defaultFogDensity = 0.04f;
    [Range(0f, 0.1f)] public float lampFogDensity = 0.01f;

    private Dictionary<string, GameObject> handLookup = new Dictionary<string, GameObject>();
    private GameObject currentActiveHand;

    private bool isPraying = false;
    private string selectedItem = "";

    private void Awake()
    {
        // Build a quick lookup table for all item hands
        foreach (var handItem in handItems)
        {
            if (handItem != null && !handLookup.ContainsKey(handItem.itemName))
                handLookup.Add(handItem.itemName, handItem.handObject);
        }

        UpdateHandDisplay();
    }

    public void SetSelectedItem(string itemName)
    {
        selectedItem = itemName;
        UpdateHandDisplay();
    }

    public void SetPraying(bool praying)
    {
        isPraying = praying;
        UpdateHandDisplay();
    }

    private void UpdateHandDisplay()
    {
        // Disable all hands
        foreach (var hand in handLookup.Values)
            if (hand != null) hand.SetActive(false);

        if (defaultHand != null) defaultHand.SetActive(false);
        if (prayingHand != null) prayingHand.SetActive(false);

        // Reset fog and light
        if (enableFogControl)
            RenderSettings.fogDensity = defaultFogDensity;

        if (playerLight != null)
            playerLight.SetActive(false);

        // Praying hand overrides everything
        if (isPraying)
        {
            if (prayingHand != null)
            {
                prayingHand.SetActive(true);
                currentActiveHand = prayingHand;
            }
            return;
        }

        // Activate selected item hand
        if (!string.IsNullOrEmpty(selectedItem) && handLookup.TryGetValue(selectedItem, out GameObject targetHand))
        {
            if (targetHand != null)
            {
                targetHand.SetActive(true);
                currentActiveHand = targetHand;
            }
        }

        // Special behavior for Lamp
        if (selectedItem == "Lamp")
        {
            if (playerLight != null)
                playerLight.SetActive(true);

            if (enableFogControl)
                RenderSettings.fogDensity = lampFogDensity;
        }

        // Show default hand if nothing else active
        if (currentActiveHand == null && defaultHand != null)
        {
            defaultHand.SetActive(true);
            currentActiveHand = defaultHand;
        }
    }

    public GameObject GetActiveHand()
    {
        return currentActiveHand;
    }
}
