using UnityEngine;
using UnityEngine.UI;

public class UIItemIconSetter : MonoBehaviour
{
    public Image Icon { get; set; }
    public int SlotId { get; set; }
    public bool IsEmpty { get; set; } = true;
    private bool _initialized = false;

    private void Start()
    {
        if (!_initialized)
        {
            Icon = transform.GetChild(0).gameObject.GetComponent<Image>();
            _initialized = true;
        }
    }

    public void SetIcon(Sprite item)
    {
        Icon.sprite = item;
        Icon.color = new Color(1, 1, 1, 1);
    }
}