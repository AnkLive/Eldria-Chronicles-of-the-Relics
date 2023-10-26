using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class SelectInventoryItem : MonoBehaviour, IPointerDownHandler
{
    private Controller _controller;
    private UIItemIconSetter _slot;
    [Inject] private InventoryManager _inventoryManager;
    [Inject] private SwapItems _swapItems;

    public void Awake()
    {
        _controller = new Controller();
        _slot = gameObject.GetComponent<UIItemIconSetter>();
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
        if (!_inventoryManager.isCheckpoint)
        {
            return;
        }

        if (_controller.Main.RightMouse.IsPressed())
        {
            _inventoryManager.SetCurrentSelectedItem(_slot.SlotId);
        }
        else
        {
            _swapItems.ExchangeSlotData(_slot.SlotId);
        }
    }
}
