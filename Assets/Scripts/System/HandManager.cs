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
        foreach (var hand in handLookup.Values)
        {
            if (hand != null) hand.SetActive(false);
        }

        // Disable special hands first (they’ll be re-enabled as needed)
        if (defaultHand != null) defaultHand.SetActive(false);
        if (prayingHand != null) prayingHand.SetActive(false);

        if (isPraying)
        {
            if (prayingHand != null)
                prayingHand.SetActive(true);

            currentActiveHand = prayingHand;
            return;
        }

        if (handLookup.TryGetValue(selectedItem, out GameObject targetHand))
        {
            if (targetHand != null)
            {
                targetHand.SetActive(true);
                currentActiveHand = targetHand;
                return;
            }
        }

        if (defaultHand != null)
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
