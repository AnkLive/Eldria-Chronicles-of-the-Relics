using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SelectInventoryItem : MonoBehaviour, IPointerDownHandler
{
    private Controller _controller;
    private SlotInfo _slotInfo;
    [Inject] private InventoryManager _inventoryManager;

    public void Awake()
    {
        _controller = new Controller();
        _slotInfo = gameObject.GetComponent<SlotInfo>();
    }

    public void OnEnable()
    {
        _controller.Enable();
    }

    public void OnDisable()
    {
        _controller.Disable();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_inventoryManager.IsCheckpoint)
        {
            return;
        }

        if (_controller.Main.RightMouse.IsPressed())
        {
            _inventoryManager.SetCurrentSelectedItem(_slotInfo.GetSlotId());
        }
        else
        {
            _inventoryManager.Inventory.ExchangeSlotData(_slotInfo.GetSlotId(), _inventoryManager.CurrentInventorySection);
        }
    }
}
