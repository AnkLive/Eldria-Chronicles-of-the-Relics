using UnityEngine;
using UnityEngine.EventSystems;

public class SelectInventoryItem : MonoBehaviour, IPointerDownHandler
{
    public UIItemIconSetter _slot;
    public InventoryManager _inventoryManager;
    public bool isEquipped = false;
    public Transform SwapItems;
    private SwapItems _swapItems;

    private void Start()
    {
        _slot = gameObject.GetComponent<UIItemIconSetter>();
        SwapItems = GameObject.Find("SwapItems").transform;
        _swapItems = SwapItems.gameObject.GetComponent<SwapItems>();
        _inventoryManager = FindObjectOfType<InventoryManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_inventoryManager.isCheckpoint)
        {
            return;
        }
        
        _inventoryManager.SetCurrentSelectedItem(_slot.SlotId);
        _swapItems.ExchangeSlotData(_slot.SlotId);
    }
}
