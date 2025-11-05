using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public ItemEffect itemEffect;


    [Tooltip("Chapter index this item belongs to (starts at 0).")]
    public int chapterIndex = 0;

    [Tooltip("Task index to complete when this item is picked up. Set to -1 if it does not trigger any task.")]
    public int taskIndex = -1;

    [Header("Special Behavior")]
    [Tooltip("If true, this item only triggers a task and will not be added to inventory.")]
    public bool triggerTaskOnly = false;
}
