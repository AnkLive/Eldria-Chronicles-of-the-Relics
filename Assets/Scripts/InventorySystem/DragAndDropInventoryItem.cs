using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
    public class DragAndDropInventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        private IconSetter _oldSlot;
        private InventoryManager _inventoryManager;
        private InventoryUIManager _UIManager;
        private RectTransform _rectTransform;
        private Image _itemImage;

        private void Start()
        {
            _oldSlot = transform.GetComponentInParent<IconSetter>();
            _inventoryManager = FindObjectOfType<InventoryManager>();
            _UIManager = FindObjectOfType<InventoryUIManager>();
            _rectTransform = GetComponent<RectTransform>();
            _itemImage = GetComponentInChildren<Image>();
            _UIManager.OnCloseInventory += SetDefaultPosition;
        }

        private void SetDefaultPosition()
        {
            _itemImage.raycastTarget = true;
            transform.SetParent(_oldSlot.transform);
            transform.position = _oldSlot.transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_oldSlot.IsEmpty && _inventoryManager.isCheckpoint)
            {
                return;
            }

            _rectTransform.position = Input.mousePosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _inventoryManager.SetCurrentSelectedItem(_oldSlot.SlotId);
            
            if (_oldSlot.IsEmpty && _inventoryManager.isCheckpoint)
            {
                return;
            }
            
            SetItemImageTransparency(0.75f);
            _itemImage.raycastTarget = false;
            transform.SetParent(transform.parent.parent.parent.parent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_oldSlot.IsEmpty && _inventoryManager.isCheckpoint)
            {
                return;
            }

            SetItemImageTransparency(1f);
            _itemImage.raycastTarget = true;

            transform.SetParent(_oldSlot.transform);
            transform.position = _oldSlot.transform.position;

            var currentRaycast = eventData.pointerCurrentRaycast;
            
            if (currentRaycast.isValid)
            {
                var iconSetter = currentRaycast.gameObject.transform.parent.parent
                    .GetComponent<IconSetter>();
                
                if (iconSetter != null && ShouldItemBeMoved(currentRaycast.screenPosition))
                {
                    ExchangeSlotData(iconSetter);
                }
            }
        }

        private void ExchangeSlotData(IconSetter newSlot)
        {
            _inventoryManager.inventory.Sections[_inventoryManager.currentInventorySection]
                .MoveItem(_oldSlot.SlotId, newSlot.SlotId);
        }

        private void SetItemImageTransparency(float alpha)
        {
            var color = _itemImage.color;
            color.a = alpha;
            _itemImage.color = color;
        }

        private bool ShouldItemBeMoved(Vector3 currentMousePosition)
        {
            float distance = Vector3.Distance(_rectTransform.position, currentMousePosition);
            float threshold = 10f;
            return distance > threshold;
        }
    }
}
